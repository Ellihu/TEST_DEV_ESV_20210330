using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace Base_Comun
{
    public class ExcelHelper : IDisposable
    {
        HSSFWorkbook workbook;
        MemoryStream memoryStream;

        private HSSFSheet currentSheet;
        ICellStyle estiloCeldaDefault;

        private int currentRow = -1;
        private int currentColumn = -1;


        public ExcelHelper()
        {
            workbook = new HSSFWorkbook();
            memoryStream = new MemoryStream();
        }
        public HSSFSheet AgregarHoja()
        {
            currentRow = -1;
            currentColumn = -1;
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();
            currentSheet = sheet;
            return sheet;
        }

        public IRow AgregarFila()
        {
            currentRow++;
            currentColumn = -1;
            HSSFRow newRow = (HSSFRow)currentSheet.CreateRow(currentRow);

            return newRow;
        }

        public IRow AgregaEncabezados(List<String> listValues, float tamanioFuente)
        {
            currentRow++;
            currentColumn = -1;

            HSSFRow newRow = (HSSFRow)currentSheet.CreateRow(currentRow);


            foreach (var val in listValues)
            {
                var cell = AgregarCelda(val, tamanioFuente, true);
            }
            currentSheet.CreateFreezePane(0, 1);

            currentSheet.SetAutoFilter(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, listValues.Count - 1));

            return newRow;
        }

        public ICell AgregarCelda(double Value)
        {
            currentColumn++;
            ICell Cell = currentSheet.GetRow(currentRow).CreateCell(currentColumn);
            Cell.SetCellValue(Value);
            Cell.CellStyle = estiloCeldaDefault;
            Cell.SetCellType(CellType.Numeric);
            return Cell;
        }
        public ICell AgregarCelda(int Value)
        {
            currentColumn++;
            ICell Cell = currentSheet.GetRow(currentRow).CreateCell(currentColumn);
            Cell.SetCellValue(Value);
            Cell.CellStyle = estiloCeldaDefault;
            Cell.SetCellType(CellType.Numeric);
            return Cell;
        }
        public ICell AgregarCelda(string Value)
        {
            currentColumn++;
            ICell Cell = currentSheet.GetRow(currentRow).CreateCell(currentColumn);
            Cell.SetCellValue(Value);
            Cell.CellStyle = estiloCeldaDefault;
            Cell.SetCellType(CellType.String);
            return Cell;
        }

        public ICell AgregarCelda(String texto, float tamanioFuente, bool negrita)
        {
            currentColumn++;

            IFont font = workbook.CreateFont();
            font.IsBold = negrita;
            font.FontHeightInPoints = tamanioFuente;
            font.Color = (short)ColorExcel.Black;

            ICellStyle style = workbook.CreateCellStyle();
            style.SetFont(font);
            style.FillBackgroundColor = (short)ColorExcel.White;

            var row = currentSheet.GetRow(currentRow);
            ICell cell = row.CreateCell(currentColumn);
            cell.SetCellValue(texto);
            cell.CellStyle = style;
            cell.CellStyle = style;

            return cell;
        }

        public void AjustarTamanioColumnasHojaActual()
        {
            for (int i = 0; i <= 20; i++) currentSheet.AutoSizeColumn(i);

        }

        public MemoryStream ObtenerArchivo()
        {

            workbook.Write(memoryStream);

            return memoryStream;
        }


        public void Dispose()
        {
            memoryStream.Close();
        }


    }

    public enum ColorExcel
    {
        Tan = 47,
        Gold = 51,
        Yellow = 13,
        BrightGreen = 11,
        Turquoise = 15,
        SkyBlue = 40,
        Plum = 61,
        Grey25Percent = 22,
        Rose = 45,
        LightYellow = 43,
        White = 9,
        LightTurquoise = 41,
        PaleBlue = 44,
        Lavender = 46,
        Pink = 14,
        CornflowerBlue = 24,
        LemonChiffon = 26,
        Maroon = 25,
        Orchid = 28,
        Coral = 29,
        RoyalBlue = 30,
        LightGreen = 42,
        Grey40Percent = 55,
        Lime = 50,
        LightBlue = 48,
        Black = 8,
        Brown = 60,
        OliveGreen = 59,
        DarkGreen = 58,
        DarkTeal = 56,
        DarkBlue = 18,
        Indigo = 62,
        Grey80Percent = 63,
        DarkRed = 16,
        Orange = 53,
        DarkYellow = 19,
        Green = 17,
        Teal = 21,
        Blue = 12,
        BlueGrey = 54,
        Grey50Percent = 23,
        Red = 10,
        LightOrange = 52,
        LightCornflowerBlue = 31,
        SeaGreen = 57,
        Aqua = 49,
        Violet = 20,
        Automatic = 64
    }

}
