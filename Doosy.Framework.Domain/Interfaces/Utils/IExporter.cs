using System;

namespace Doosy.Framework.Domain
{
    public interface IExporter
    {
        string Export(string fileName,Object exportData);
    }
}
