using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Doosy.Framework.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Doosy.Framework.Infrastructure
{
    public class ExcelExporter: IExcelExporter
    {
        ILogger<ExcelExporter> logger;
        IConfiguration settings;
        ISerializer serializer;
        public ExcelExporter(ILogger<ExcelExporter> logger, IConfiguration settings, ISerializer serializer)
        {
            this.logger = logger;
            this.settings = settings;
            this.serializer = serializer;
        }

        public string Export(string filename, object exportData)
        {
            var result = "";
            try
            {
                var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");

                if (!Directory.Exists(imagesDirectory))
                    Directory.CreateDirectory(imagesDirectory);

                filename = $"{filename}_{System.Guid.NewGuid()}.xlsx";

                var filePath = Path.Combine(imagesDirectory, filename);

                var table = (DataTable)serializer.Deserialize(serializer.Serialize(exportData), (typeof(DataTable)));

                using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Sheet-One");

                List<String> columns = new List<string>();
                IRow row = excelSheet.CreateRow(0);
                int columnIndex = 0;

                foreach (DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);
                    row.CreateCell(columnIndex).SetCellValue(column.ColumnName);
                    columnIndex++;
                }

                int rowIndex = 1;
                foreach (DataRow dsrow in table.Rows)
                {
                    row = excelSheet.CreateRow(rowIndex);
                    int cellIndex = 0;
                    foreach (String col in columns)
                    {
                        row.CreateCell(cellIndex).SetCellValue(dsrow[col].ToString());
                        cellIndex++;
                    }
                    rowIndex++;
                }
                workbook.Write(fs);

                result = $"{settings.GetSection("BaseUrl").Value}/files/{filename}";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }

            return result;
        }
    }
}
