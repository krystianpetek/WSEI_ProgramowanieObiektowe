﻿using Zadanie3.Document;

namespace Zadanie3.Device
{
    public class Copier
    {
        Printer printer;
        Scanner scanner;

        public Copier()
        {
            printer.state = IDevice.State.OFF;
            scanner.state = IDevice.State.OFF;
            
            state = IDevice.State.OFF;
        }

        public int PrintCounter;
        public int ScanCounter;

        public void ScanAndPrint()
        {
            if (GetState() == IDevice.State.ON)
            {
                IDocument dokument;
                Scan(out dokument);
                Print(dokument);
            }
        }

        public new void PowerOn()
        {
            if (GetState() == IDevice.State.OFF)
            {
                Counter++;
                state = IDevice.State.ON;
            }
        }

        public new void PowerOff()
        {
            if (GetState() == IDevice.State.ON)
            {
                state = IDevice.State.OFF;
            }
        }

        public void Print(in IDocument document)
        {
            if (GetState() == IDevice.State.ON)
            {
                PrintCounter++;

                DateTime x = DateTime.Now;
                Console.Write($"{x} Print: {document.GetFileName()}\n");
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            document = new TextDocument("");

            if (GetState() == IDevice.State.ON)
            {
                ScanCounter++;
                switch (formatType)
                {
                    case IDocument.FormatType.TXT:
                        document = new TextDocument($"TextScan{ScanCounter}.txt");
                        break;

                    case IDocument.FormatType.PDF:
                        document = new PDFDocument($"PDFScan{ScanCounter}.pdf");
                        break;

                    case IDocument.FormatType.JPG:
                    default:
                        document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
                        break;
                }
                DateTime x = DateTime.Now;
                Console.Write($"{x} Scan: {document.GetFileName()}\n");
            }
        }
    }
}