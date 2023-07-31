using System;
using static System.Net.Mime.MediaTypeNames;

namespace AssetTrackingWeek30
{
    //Defines text style
    public enum TextType
        {
            Default, //White
            InstructionGold, //Text is Gold
            InstructionBlue,// Blue
            SuccessMessage,//Green
            TableHead, //Green
            TableData, //White
            TableDataRed,//Red
            TableDataYellow,//Yellow
            TableSearchResult,//Purple
            PageHeader,//White
            MenuHeader,//White
            Menu,//White
            Input, //White
            Error //Red
        }

    //Defines lines style
    public enum LineType
        {
            SingleDot,//.......
            DoubleDot,//:::::::
            SingleLine,//------
            DoubleLine //======
        }

    //This file handles styles of console and its text.
    public class ConsoleStyler
    {
        public void StyleText(string printText, TextType textType)
        {
            switch (textType)
            {
                case TextType.InstructionGold:
                    PrintInstructionGold(printText);
                    break;
                case TextType.InstructionBlue:
                    PrintInstructionBlue(printText);
                    break;
                case TextType.SuccessMessage:
                    PrintSuccessMessage(printText);
                    break;
                case TextType.Default:
                    PrintWithDefaultStyle(printText);
                    break;
                case TextType.PageHeader:
                    PrintPageHeader(printText);
                    break;
                case TextType.TableHead:
                    PrintTableHeader(printText);
                    break;
                case TextType.TableData:
                    PrintTableData(printText);
                    break;
                case TextType.TableDataRed:
                    PrintTableDataRed(printText);
                    break;
                case TextType.TableDataYellow:
                    PrintTableDataYellow(printText);
                    break;
                case TextType.TableSearchResult:
                    PrintTableSearchResult(printText);
                    break;
                case TextType.MenuHeader:
                    PrintMenuHeader(printText);
                    break;
                case TextType.Input:
                    PrintInputInstruction(printText);
                    break;
                 case TextType.Error:
                    PrintWithErrorStyle(printText);
                    break;
                default:
                    Console.WriteLine(printText); // Default style if textType is not recognized
                    break;
            }
        }

        private void PrintWithDefaultStyle(string printText)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(printText);
            Console.ResetColor();
        }

        private void PrintWithSuccessStyle(string printText)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(printText);
            Console.ResetColor();
        }

        private void PrintWithErrorStyle(string printText)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(printText);
            Console.ResetColor();
        }

        private void PrintInstructionGold(string printText)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(printText);
            Console.ResetColor();
        }

        private void PrintInstructionBlue(string printText)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(printText);
            Console.ResetColor();
        }

        private void PrintSuccessMessage(string printText)
        {
            int len = printText.Length;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(printText);
            PrintLine(LineType.SingleLine, ConsoleColor.Black, len);
            Console.ResetColor();
        }

        private void PrintPageHeader(string printText)
        {
            int len = printText.Length;
            Console.ForegroundColor = ConsoleColor.Black;
            PrintLine(LineType.DoubleLine, ConsoleColor.Black, len + 16); 
            Console.WriteLine("\t{0}",printText);
            PrintLine(LineType.DoubleLine, ConsoleColor.Black, len + 16);
            Console.ResetColor();
        }

        private void PrintTableHeader(string printText)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            int len = 20;
            Console.Write(printText.PadRight(len));
            Console.ResetColor();
        }

        private void PrintTableData(string printText)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            int len =  20;
            Console.Write(printText.PadRight(len));
            Console.ResetColor();
        }

        private void PrintTableDataRed(string printText)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            int len = 20;
            Console.Write(printText.PadRight(len));
            Console.ResetColor();
        }

        private void PrintTableDataYellow(string printText)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            int len = 20;
            Console.Write(printText.PadRight(len));
            Console.ResetColor();
        }

        private void PrintTableSearchResult(string printText)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            int len = Math.Max(printText.Length + 5, 15);
            Console.Write($"\x1b[1m{printText.PadRight(len)}\x1b[0m");
            Console.ResetColor();
        }

        public void PrintHeader(string[] headers)
        {
            // Calculate column widths based on the longest data in each column
            int[] columnWidths = new int[headers.Length];

            // Print headers with underline
            for (int i = 0; i < headers.Length; i++)
            {
                columnWidths[i] = headers[i].Length;
                StyleText(headers[i], TextType.TableHead);
            }
            Console.WriteLine();
            for (int i = 0; i < headers.Length; i++)
            {
                StyleText((new string('-', columnWidths[i]) + "  "), TextType.TableHead);
            }
            Console.WriteLine();
        }

        private void PrintInputInstruction(string printText)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(printText);
            Console.ResetColor();
        }

        private void PrintMenuHeader(string printText)
        {           
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"\n{printText}");
            PrintLine(LineType.SingleLine,ConsoleColor.DarkBlue, 15);
            Console.ResetColor();
        }

        public void PrintLine(LineType lineType, ConsoleColor color, int lineLength = 50)
        {
            string line = lineType switch
            {
                LineType.SingleDot => new string('.', lineLength),
                LineType.DoubleDot => new string(':', lineLength),
                LineType.SingleLine => new string('-', lineLength),
                LineType.DoubleLine => new string('=', lineLength),
                _ => new string('*', lineLength),
            };

            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ResetColor();
        }
    }
}

