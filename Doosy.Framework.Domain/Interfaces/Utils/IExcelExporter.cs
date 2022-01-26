using System;
namespace Doosy.Framework.Domain
{
    public interface IExcelExporter
    {
        string Export(string fileName, Object exportData);
    }
}
