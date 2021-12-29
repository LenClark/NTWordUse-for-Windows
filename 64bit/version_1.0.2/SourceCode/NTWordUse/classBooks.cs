using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTWordUse
{
    public class classBooks
    {
        bool isNT = true;
        int noOfChapters = 0, actualBookNumber;
        String shortName, commonName, lxxName, fileName;
        SortedList<String, classChapter> chapterList = new SortedList<String, classChapter>();
        SortedList<int, String> chapterLookup = new SortedList<int, String>();

        public int NoOfChapters { get => noOfChapters; set => noOfChapters = value; }
        public int ActualBookNumber { get => actualBookNumber; set => actualBookNumber = value; }
        public string ShortName { get => shortName; set => shortName = value; }
        public string CommonName { get => commonName; set => commonName = value; }
        public string LxxName { get => lxxName; set => lxxName = value; }
        public string FileName { get => fileName; set => fileName = value; }

        public classChapter addChapter(String chapterNo )
        {
            classChapter currentChapter;

            if( chapterList.ContainsKey( chapterNo ) )
            {
                chapterList.TryGetValue(chapterNo, out currentChapter);
            }
            else
            {
                currentChapter = new classChapter();
                chapterList.Add(chapterNo, currentChapter);
                chapterLookup.Add(++noOfChapters, chapterNo);
            }
            return currentChapter;
        }

        public classChapter getChapterBySequence( int seq)
        {
            String chapNo;
            classChapter currentChapter = null;

            if( chapterLookup.ContainsKey( seq))
            {
                chapterLookup.TryGetValue(seq, out chapNo);
                if( chapterList.ContainsKey( chapNo))
                {
                    chapterList.TryGetValue(chapNo, out currentChapter);
                }
            }
            return currentChapter;
        }
    }
}
