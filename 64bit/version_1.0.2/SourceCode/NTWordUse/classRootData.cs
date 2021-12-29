using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTWordUse
{
    class classRootData
    {
        /**************************************************************************************************
         *                                                                                                *
         *                                            RootData                                            *
         *                                            ========                                            *
         *                                                                                                *
         *  There is a possibility (albeit small) that the same word form can be used for two words of    *
         *    different grammatical category.  In order to manage this possibility, the Key for this      *
         *    Class is root + category code.  This is a bit cumbersome but all other options seemed to    *
         *    lead into extreme complexities.                                                             *
         *                                                                                                *
         **************************************************************************************************/

        bool isFoundInNT = false;
        int catCode;
        String rootWord;

        /*=====================================================================================================*
         *                                                                                                     *
         *                                          parsedDetail                                               *
         *                                          ============                                               *
         *                                                                                                     *
         *  This is a bad name but I wasn't really sure what to call it.                                       *
         *  Most grammatical categories can have a variety of forms.  For example, a noun varies according to  *
         *  number and class.  Verbs are more complex.  The parsedDetail is a list of all such variants found  *
         *  in the source texts.                                                                               *
         *                                                                                                     *
         *  Key:   An integer value calculated from all elements of the grammatical variations                 *
         *  Value: An instance of classParsedItem                                                              *
         *                                                                                                     *
         *=====================================================================================================*/
        SortedList<int, classParsedItem> parsedDetail = new SortedList<int, classParsedItem>();
        classProcesses processRawData;

        public bool IsFoundInNT { get => isFoundInNT; set => isFoundInNT = value; }
        public int CatCode { get => catCode; set => catCode = value; }
        public string RootWord { get => rootWord; set => rootWord = value; }
        internal classProcesses ProcessRawData { get => processRawData; set => processRawData = value; }

        public void addParsedText(int parseCode, String associatedWord, int bookNo, int chapNo, int verseNo, String givenChapRef, String givenVerseRef, bool isNT)
        {
            /*=====================================================================================*
             *                                                                                     *
             *                                   addParsedText                                     *
             *                                   =============                                     *
             *                                                                                     *
             *  This method effectively performs two functions:                                    *
             *                                                                                     *
             *   a) It identifies one of one or more parse structures for a root;                  *
             *   b) It provides  a reference (for reporting back).                                 *
             *                                                                                     *
             *  Parameters:                                                                        *
             *  ==========                                                                         *
             *                                                                                     *
             *  parseCode:                   Calculated in processRawData                          *
             *  associatedWord:              Effectively, the root noun, verb, etc.                *
             *  bookNo, chapNo, verseNo:     Calculated values, used to index data                 *
             *  givenChapRef, givenVerseRef: Values provided by the source data (only LXX)         *
             *  isNT:                        An obvious distinguishing flag                        *
             *                                                                                     *
             *=====================================================================================*/

            classParsedItem currentParse = null;

            if (isNT) isFoundInNT = true;
            if (!parsedDetail.ContainsKey(parseCode))
            {
                currentParse = new classParsedItem();
                currentParse.ParseCode = parseCode;
                parsedDetail.Add(parseCode, currentParse);
            }
            else
            {
                parsedDetail.TryGetValue(parseCode, out currentParse);
            }
            if (isNT) currentParse.IsInNT = true;
            else currentParse.IsInLXX = true;
            currentParse.addReference(processRawData.getReferenceCode(bookNo, chapNo, verseNo), bookNo, chapNo, verseNo, givenChapRef, givenVerseRef, isNT);
            currentParse.setWord(associatedWord);
        }

        public SortedList<int, classParsedItem> getParseList()
        {
            return parsedDetail;
        }
    }
}
