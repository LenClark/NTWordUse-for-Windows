using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTWordUse
{
    class classProcesses
    {
        /**********************************************************************************************************
         *                                                                                                        *
         *                                           classProcesses                                               *
         *                                           ==============                                               *
         *                                                                                                        *
         *  This class will handle all, repeating processing of raw data.  It will be created at the start of the *
         *  application and will be used for all words.  This means that a single instance will exist for the     *
         *  lifetime of the application.  If we created the methods in this class as part of the raw data         *
         *  classes, it would involve significant duplication of code.                                            *
         *                                                                                                        *
         **********************************************************************************************************/

        SortedDictionary<Char, Char> initialConv = new SortedDictionary<char, char>();

        public classProcesses()
        {
            int idx, totalChars;
            Char[] possibleCaps = { 'Α', 'Ἀ', 'Ἁ', 'Ἂ', 'Ἃ', 'Ἄ', 'Ἅ', 'Β', 'Γ', 'Δ', 'Ε', 'Ἐ', 'Ἑ', 'Ἒ', 'Ἓ', 'Ἔ', 'Ἕ', 'Ζ', 'Η', 'Ἠ', 'Ἡ', 'Ἢ', 'Ἣ', 'Ἤ', 'Ἥ', 'Θ',
                                  'Ι', 'Ἰ', 'Ἱ', 'Ἲ', 'Ἳ', 'Ἴ', 'Ἵ', 'Κ', 'Λ', 'Μ', 'Ν', 'Ξ', 'Ο', 'Ὀ', 'Ὁ', 'Ὂ', 'Ὃ', 'Ὄ', 'Ὅ', 'Π', 'Ρ', 'Σ', 'Τ',
                                  'Υ', 'Ὑ', 'Ὓ', 'Ὕ', 'Φ', 'Χ', 'Ψ', 'Ω', 'Ὠ', 'Ὡ', 'Ὢ', 'Ὣ', 'Ὤ', 'Ὥ' };
            Char[] replacementMins = { 'α', 'ἀ', 'ἁ', 'ἂ', 'ἃ', 'ἄ', 'ἅ', 'β', 'γ', 'δ', 'ε', 'ἐ', 'ἑ', 'ἒ', 'ἓ', 'ἔ', 'ἕ', 'ζ', 'η', 'ἠ', 'ἡ', 'ἢ', 'ἣ', 'ἤ', 'ἥ', 'θ',
                                     'ι', 'ἰ', 'ἱ', 'ἲ', 'ἳ', 'ἴ', 'ἵ', 'κ', 'λ', 'μ', 'ν', 'ξ', 'ο', 'ὀ', 'ὁ', 'ὂ', 'ὃ', 'ὄ', 'ὅ', 'π', 'ρ', 'σ', 'τ',
                                     'υ', 'ὑ', 'ὓ', 'ὕ', 'φ', 'χ', 'ψ', 'ω', 'ὠ', 'ὡ', 'ὢ', 'ὣ', 'ὤ', 'ὥ' };

            //            allReferences = new FullReference(globalData);
            totalChars = possibleCaps.Length;
            for (idx = 0; idx < totalChars; idx++)
            {
                initialConv.Add(possibleCaps[idx], replacementMins[idx]);
            }
        }

        public int getParseInfo(String inParseInfo)
        {
            /*==================================================*
             *                                                  *
             *  1 = "Noun",                                     *
             *  2 = "Verb",                                     *
             *  3 = "Adjective",                                *
             *  4 = "Adverb",                                   *
             *  5 = "Preposition",                              *
             *  6 = "Article",                                  *
             *  8 = "Definite Pronoun",                         *
             *  9 = "Indefinite Pronoun",                       *
             * 10 = "Personal Pronoun",                         *
             * 11 = "Relative Pronoun",                         *
             * 12 = "Interjection",                             *
             * 13 = "Exclamation"                               *
             *                                                  *
             *==================================================*/

            int parseCode = 0;

            switch (inParseInfo[0])
            {
                case '1':
                    if (inParseInfo[5] == 'S')
                    {
                        parseCode = 1;
                    }
                    else
                    {
                        parseCode = 4;
                    }
                    break;
                case '2':
                    if (inParseInfo[5] == 'S')
                    {
                        parseCode = 2;
                    }
                    else
                    {
                        parseCode = 5;
                    }
                    break;
                case '3':
                    if (inParseInfo[5] == 'S')
                    {
                        parseCode = 3;
                    }
                    else
                    {
                        parseCode = 6;
                    }
                    break;
                case '-':
                    if (inParseInfo[5] == 'S')
                    {
                        parseCode = 7;
                    }
                    if (inParseInfo[5] == 'P')
                    {
                        parseCode = 8;
                    }
                    break;
            }
            switch (inParseInfo[4])
            {
                case 'N': parseCode += 10000; break;    // Nominative
                case 'V': parseCode += 20000; break;    // Vocative
                case 'A': parseCode += 30000; break;    // Accusative
                case 'G': parseCode += 40000; break;    // Genitive
                case 'D': parseCode += 50000; break;    // Dative
                default: break;
            }
            switch (inParseInfo[6])
            {
                case 'M': parseCode += 100000; break;    // Masculine
                case 'N': parseCode += 200000; break;    // Neuter
                case 'F': parseCode += 300000; break;    // Feminine
                default: break;
            }
            switch (inParseInfo[1])
            {
                case 'P': parseCode += 10; break;    // Present
                case 'I': parseCode += 20; break;    // Imperfect
                case 'A': parseCode += 30; break;    // Aorist
                case 'X': parseCode += 40; break;    // Perfect
                case 'Y': parseCode += 50; break;    // Pluperfect
                case 'F': parseCode += 60; break;    // Future
                default: break;
            }
            switch (inParseInfo[3])
            {
                case 'I': parseCode += 100; break;    // Indicative
                case 'D': parseCode += 200; break;    // Imperative
                case 'S': parseCode += 300; break;    // Subjunctive
                case 'O': parseCode += 400; break;    // Optative
                case 'N': parseCode += 500; break;    // Infinitive
                case 'P': parseCode += 600; break;    // Participle
                default: break;
            }
            switch (inParseInfo[2])
            {
                case 'A': parseCode += 1000; break;    // Active
                case 'M': parseCode += 2000; break;    // Middle
                case 'P': parseCode += 3000; break;    // Passive
                default: break;
            }
            switch (inParseInfo[7])
            {
                case 'C': parseCode += 1000000; break;    // Comparative
                case 'S': parseCode += 2000000; break;    // Superlative
                default: break;
            }
            return parseCode;
        }

        public int getLxxParseInfo(int categoryCode, String inParseInfo)
        {
            /*==================================================*
             *                                                  *
             *  Possible Category Code values:                  *
             *                                                  *
             *  1 = "Noun",                                     *
             *  2 = "Verb",                                     *
             *  3 = "Adjective",                                *
             *  4 = "Adverb",                                   *
             *  5 = "Preposition",                              *
             *  6 = "Article",                                  *
             *  7 = "Demonstrative Pronoun",                    *
             *  8 = "Indefinite Pronoun",                       *
             *  9 = "Personal Pronoun",                         *
             * 10 = "Relative Pronoun",                         *
             * 11 = "Particle",                                 *
             * 12 = "Conjunction"                               *
             * 13 = "Interjection",                             *
             * Not used because omitted                         *
             * 14 = "ὅστις",                                    *
             * 15 = "Indeclinable Number"                       *
             *                                                  *
             *==================================================*/

            int parseCode = 0;

            switch (categoryCode)
            {
                case 0: // Nouns
                case 2: // Adjectives
                case 5: // Articles
                case 6: // Demonstrative Pronouns
                case 7: // Indefinite Pronoun
                case 8: // Personal Pronoun
                case 9: // Relative Pronoun
                case 10: // ὅστις
                    if (inParseInfo.Length > 0)
                    {
                        switch (inParseInfo[0])
                        {
                            case 'N': parseCode += 10000; break;    // Nominative
                            case 'V': parseCode += 20000; break;    // Vocative
                            case 'A': parseCode += 30000; break;    // Accusative
                            case 'G': parseCode += 40000; break;    // Genitive
                            case 'D': parseCode += 50000; break;    // Dative
                            default: break;
                        }
                    }
                    if (inParseInfo.Length > 1)
                    {
                        switch (inParseInfo[1])
                        {
                            case 'S': parseCode += 7; break;
                            case 'D': parseCode += 8; break;
                            case 'P': parseCode += 8; break;
                            default: break;
                        }
                    }
                    if (inParseInfo.Length > 2)
                    {
                        switch (inParseInfo[2])
                        {
                            case 'M': parseCode += 100000; break;    // Masculine
                            case 'N': parseCode += 200000; break;    // Neuter
                            case 'F': parseCode += 300000; break;    // Feminine
                            default: break;
                        }
                    }
                    if (categoryCode == 2)
                    {
                        if (inParseInfo.Length > 3)
                        {
                            if (inParseInfo[3] == 'C') parseCode += 1000000;     // Comparative
                            if (inParseInfo[3] == 'S') parseCode += 2000000;    // Superlative
                        }
                    }
                    break;
                case 1: // Verbs
                    switch (inParseInfo[0])
                    {
                        case 'P': parseCode += 10; break;    // Present
                        case 'I': parseCode += 20; break;    // Imperfect
                        case 'A': parseCode += 30; break;    // Aorist
                        case 'X': parseCode += 40; break;    // Perfect
                        case 'Y': parseCode += 50; break;    // Pluperfect
                        case 'F': parseCode += 60; break;    // Future
                        default: break;
                    }
                    switch (inParseInfo[1])
                    {
                        case 'A': parseCode += 1000; break;    // Active
                        case 'M': parseCode += 2000; break;    // Middle
                        case 'P': parseCode += 3000; break;    // Passive
                        default: break;
                    }
                    switch (inParseInfo[2])
                    {
                        case 'I': parseCode += 100; break;    // Indicative
                        case 'D': parseCode += 200; break;    // Imperative
                        case 'S': parseCode += 300; break;    // Subjunctive
                        case 'O': parseCode += 400; break;    // Optative
                        case 'N': parseCode += 500; break;    // Infinitive
                        case 'P': parseCode += 600; break;    // Participle
                        default: break;
                    }
                    if (inParseInfo[2] == 'P')
                    {
                        switch (inParseInfo[3])
                        {
                            case 'N': parseCode += 10000; break;    // Nominative
                            case 'V': parseCode += 20000; break;    // Vocative
                            case 'A': parseCode += 30000; break;    // Accusative
                            case 'G': parseCode += 40000; break;    // Genitive
                            case 'D': parseCode += 50000; break;    // Dative
                            default: break;
                        }
                        switch (inParseInfo[4])
                        {
                            case 'S': parseCode += 7; break;
                            case 'D': parseCode += 8; break;
                            case 'P': parseCode += 8; break;
                            default: break;
                        }
                        switch (inParseInfo[5])
                        {
                            case 'M': parseCode += 100000; break;    // Masculine
                            case 'N': parseCode += 200000; break;    // Neuter
                            case 'F': parseCode += 300000; break;    // Feminine
                            default: break;
                        }
                    }
                    else
                    {
                        if (inParseInfo.Length > 3)
                        {
                            switch (inParseInfo[3])
                            {
                                case '1':
                                    if (inParseInfo[4] == 'S')
                                    {
                                        parseCode += 1;
                                    }
                                    else
                                    {
                                        parseCode += 4;
                                    }
                                    break;
                                case '2':
                                    if (inParseInfo[4] == 'S')
                                    {
                                        parseCode += 2;
                                    }
                                    else
                                    {
                                        parseCode += 5;
                                    }
                                    break;
                                case '3':
                                    if (inParseInfo[4] == 'S')
                                    {
                                        parseCode += 3;
                                    }
                                    else
                                    {
                                        parseCode += 6;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                default: break;
            }
            return parseCode;
        }

        public String cleanTextWord(String givenWord, Char rootInitial)
        {
            Char firstLetter, newLetter;
            String newWord = "";

            firstLetter = givenWord[0];
            if ((initialConv.ContainsKey(firstLetter)) && (!initialConv.ContainsKey(rootInitial)))
            {
                initialConv.TryGetValue(firstLetter, out newLetter);
                newWord = newLetter.ToString() + givenWord.Substring(1, givenWord.Length - 1);
            }
            else
            {
                newWord = givenWord;
            }
            return newWord;
        }
        public int getReferenceCode(int bookNo, int chapNo, int verseNo)
        {
            int refCode;

            refCode = bookNo * 1000000 + chapNo * 1000 + verseNo;
            return refCode;
        }
    }
}
