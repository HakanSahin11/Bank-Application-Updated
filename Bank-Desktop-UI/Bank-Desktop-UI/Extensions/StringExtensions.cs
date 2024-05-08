using Bank_Desktop_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Desktop_UI.Extensions
{
    public static class StringExtensions
    {
        public static string Anonymize(this string Content, int AnnonymizeLength, int InitialIndex = 0, int InitialLength = 0, int EndIndex = 0, int EndLength = 0)
        {
            var initial = Content.Substring(InitialIndex, InitialLength);
            var last = Content.Substring(EndIndex, EndLength);
            var annoymizedData = "";
            for (int i = 0; i < AnnonymizeLength; i++)
                annoymizedData += "*";

            return initial + annoymizedData + last;
        }
    }
}
