using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Security;

namespace MakeChart
{
    public partial class Form1 : Form
    {
        #region member

        Excel.Application excelApp;  // Excel  인스턴스 
        Excel.Workbook workBook; // Workbook 인스턴스
        Excel.Worksheet workSheet; //Worksheet 인스턴스
        Excel.Chart chart; // 차트
        Excel.Range currentRange; //Worksheet current범위 인스턴스
        Excel.Range voltRange; //Worksheet volt범위 인스턴스
        Excel.Range addRange; // 행 추가 범위 

        Excel.Range chartRange;
    

        private decimal current; // 엑셀 절대값을 취한 전류값
        private float[] volt;// 엑셀 전압값
        int valueCount;

        private bool isOpened = false; //  엑셀파일 오픈 체크 flag  

        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        private void Add_button_Click(object sender, EventArgs e) //csv파일 선택 
        {
            string[] image_folder_path = Select_Excel_File();

            fileListBox.Items.Clear();
            try
            {
                foreach (var item in image_folder_path)
                {
                    fileListBox.Items.Add(item);
                }

            }

            catch (System.NullReferenceException error)
            {

            }
        }
        private bool CheckFileLocked(string filePath)  // 파일 열려있는지 확인
        {
            try
            {
                FileInfo file = new FileInfo(filePath);

                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }
        private string[] Select_Excel_File() // 파일선택 다이얼로그 
        {
            int file_number = 0;
            string[] file_path = new string[file_number]; // 선택한 이미지들 파일 배열 

            OpenFileDialog imagedirectory_dialog = new OpenFileDialog() // 오픈파일 다이어로그 객체 생성
            {
                FileName = "Select a csv file",        // 필터 지정 
                Filter = "csv files (*.csv;)|*.csv;",
                Title = "All files(*.*)|*.*",
            };
            imagedirectory_dialog.Multiselect = true; // 다중선택 가능 

            if (imagedirectory_dialog.ShowDialog() == DialogResult.OK)  // 다이어로그 ok 버튼 클릭시
            {
                file_number = imagedirectory_dialog.FileNames.Length; //선택한 이미지 파일 개수
                file_path = new string[file_number];  //선택한 이미지 파일 목록
                try
                {
                    int i = 0; // file_path index 초기값
                    foreach (var item in imagedirectory_dialog.FileNames)
                    {
                        file_path[i] = item;
                        i++;
                    }
                    return file_path;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
            else
            {
                file_path = null;
                return file_path;
            }
            return file_path;

        }

        private void Draw_Chart_button_click(object sender, EventArgs e)
        {
            string[] values;
            int sheetCount;

            object misValue = System.Reflection.Missing.Value; // todo namespace : System.Reflection ,class : Missing -> 누락된 object  ,유일한 필드로 Value 를 가지고 있음 
            excelApp = new Excel.Application(); // 엑셀앱 초기화 
           

            Object[] fileList = new string[] { };

            MessageBox.Show(fileListBox.Items.ToString());
            //fileList = FileListBox.Items;

            try
            {
                foreach (var item in fileListBox.Items)
                {
              

                    bool isOpenCheck = CheckFileLocked(item.ToString());

                    if (File.Exists(item.ToString()))  // 엑셀파일 존재할때 
                    {
                       
                        workBook = excelApp.Workbooks.Open(item.ToString());

                        sheetCount = workBook.Sheets.Count;

                        for (int i = 1; i < sheetCount + 1; i++)
                        {
                            Excel.Range absRange;
                            workSheet = workBook.Worksheets.get_Item(i);
                            valueCount = workSheet.UsedRange.Rows.Count;
                            voltRange = workSheet.Range["C5", $"C{valueCount + 3}"];
                            currentRange = workSheet.Range["B5", $"B{valueCount + 3}"];
                            addRange = workSheet.Range["D1", $"D{valueCount + 3}"];
                            addRange.Columns.Insert(Excel.XlInsertShiftDirection.xlShiftToRight);
                            absRange = workSheet.Range["D5", $"D{valueCount + 3}"];
                            absRange = currentRange.get_Value();
                            for (int j = 1; j < valueCount; j++)
                            {
                                current = Convert.ToDecimal(currentRange.get_Value());
                                MessageBox.Show(current.ToString());
                            }


                        }
                        for (int k = 0; k < valueCount; k++)
                        {
                            
                           
                        }




                        isOpened = true;//flag

                        //values = currentRange.Cells.Value2.ToString();
                        //foreach (var value in values)
                        //{
                        //    MessageBox.Show(value);
                        //}


                        //var current = from 


                    }
                    else
                    {
                        workBook = excelApp.Workbooks.Add(misValue);
                    }
                    if (isOpened == true && isOpenCheck == false)
                    {
                        MessageBox.Show("열려있는 엑셀을 닫아주세요");
                        return;
                    }
                    workBook.SaveAs(@"D:\asdf.csv");
                    workBook.Close(true);
                    excelApp.Workbooks.Close();
                    excelApp.Quit();
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.ToString());
            }
            finally
            {
                //백그라운드 excel을 release
                ReleaseExcelObject(workSheet);
                ReleaseExcelObject(workBook);
                ReleaseExcelObject(excelApp);
            }
            

        }
        private static void ReleaseExcelObject(object obj) // 백그라운드 excel 해제  
        {
            try
            {
                if (obj != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);//todo  //관리되지 않는 메모리를 할당하고, 관리되지 않는 메모리 블록을 복사하고, 관리되는 형식을 관리되지 않는 형식으로 변환하는 메서드의 컬렉션 및 비관리 코드와 상호 작용할 때 사용되는 기타 메서드의 컬렉션을 제공합니다.
                    obj = null;  //리소스에 참조를 보유 하는 개체를  null
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect(); // 가비지 컬렉터로 사용되지 않는 메모리 수집
            }
        }
    }
}
