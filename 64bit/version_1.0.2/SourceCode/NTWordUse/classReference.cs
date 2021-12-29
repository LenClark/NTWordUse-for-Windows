using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTWordUse
{
    public class classReference
    {
        bool isNT = true;
        int bookCode, chapterNo, verseNo;
        String givenChapterRef = "", givenVerseRef = "";

        public bool IsNT { get => isNT; set => isNT = value; }
        public int BookCode { get => bookCode; set => bookCode = value; }
        public int ChapterNo { get => chapterNo; set => chapterNo = value; }
        public int VerseNo { get => verseNo; set => verseNo = value; }
        public string GivenChapterRef { get => givenChapterRef; set => givenChapterRef = value; }
        public string GivenVerseRef { get => givenVerseRef; set => givenVerseRef = value; }
    }
}
