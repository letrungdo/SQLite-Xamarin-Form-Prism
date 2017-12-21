using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteDemo
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
   
}
