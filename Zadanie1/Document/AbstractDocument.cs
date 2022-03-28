﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.Document
{
    public abstract class AbstractDocument : IDocument
    {
        private string fileName;
        public AbstractDocument(string fileName) => this.fileName = fileName;
        public string GetFileName() => fileName;
        public void ChangeFileName(string newFileName) => this.fileName = newFileName;
        public abstract IDocument.FormatType GetFormatType();
    }
}
