using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTWordUse
{
    public class classChapter
    {
        int noOfVerses = 0;
        SortedList<String, classVerse> verseList = new SortedList<String, classVerse>();
        SortedList<int, String> verseLookup = new SortedList<int, String>();

        public int NoOfVerses { get => noOfVerses; set => noOfVerses = value; }

        public classVerse addVerse(String verseNo)
        {
            classVerse currentVerse;

            if( verseList.ContainsKey( verseNo))
            {
                verseList.TryGetValue(verseNo, out currentVerse);
            }
            else
            {
                currentVerse = new classVerse();
                verseList.Add(verseNo, currentVerse);
                verseLookup.Add(++noOfVerses, verseNo);
            }
            return currentVerse;
        }

        public String getVerseText(int verseSeq)
        {
            String verseRef;
            classVerse currentVerse = null;

            if( verseLookup.ContainsKey( verseSeq))
            {
                verseLookup.TryGetValue(verseSeq, out verseRef);
                if( verseList.ContainsKey( verseRef))
                {
                    verseList.TryGetValue(verseRef, out currentVerse);
                }
            }
            if (currentVerse == null) return "";
            else return currentVerse.TextOfVerse;
        }

        public String getVerseRefBySequence( int seq)
        {
            String verseRef = "";

            if( verseLookup.ContainsKey( seq))
            {
                verseLookup.TryGetValue(seq, out verseRef);
            }
            return verseRef;
        }
    }
}
