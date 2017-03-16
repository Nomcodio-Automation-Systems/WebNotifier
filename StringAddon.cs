using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebNotifier
{
    class StringAddon
    {
        private static int preDiff = 5;
        private static int postDiff = 25;
        private static int how_far = 8;
        delegate DiffItem InternDelegate(bool s);
        delegate int LookHeadDelegate(int a, int b);
        delegate bool CheckDelegate(int pos, int expect);

        public static int[] DifferI(string source1, string source2)
        {


            List<string> diff;
            IEnumerable<string> set1 = source1.Split(' ').Distinct();
            IEnumerable<string> set2 = source2.Split(' ').Distinct();
            int[] diffArrayPos = new int[ 1024 ];

            if ( set2.Count() > set1.Count() )
            {
                diff = set2.Except(set1).ToList();
                System.Collections.IList c1 = set2.ToList();

                int i = 0;
                int j = 0;
                foreach ( string value in c1 )
                {
                    if ( j >= 1024 )
                    {
                        break;
                    }
                    if ( ( diff.ToArray() ).Length <= j )
                    {
                        break;
                    }
                    if ( diff[ j ] == value )
                    {
                        diffArrayPos[ j ] = i;
                        j++;
                    }

                    i++;
                }
            }
            else
            {
                diff = set1.Except(set2).ToList();
                diff = set2.Except(set1).ToList();
                System.Collections.IList c1 = set1.ToList();

                int i = 0;
                int j = 0;
                foreach ( string value in c1 )
                {
                    if ( j >= 1024 )
                    {
                        break;
                    }
                    if ( ( diff.ToArray() ).Length <= j )
                    {
                        break;
                    }
                    if ( diff[ j ] == value )
                    {
                        diffArrayPos[ j ] = i;
                        j++;
                    }

                    i++;
                }
            }
            return diffArrayPos;
        }

        public static LinkedList<DiffItem> Analyse(string source1, string source2)
        {
            int[] paku = DifferI(source1, source2);
            LinkedList<DiffItem> result = Shrink(paku);
            return result;
        }

        public static LinkedList<DiffItem> Shrink(int[] list)
        {
            // no checks here
            int begin = list[ 0 ];
            LinkedList<DiffItem> numbers = new LinkedList<DiffItem>();



        

            bool flagnew = false;
            InternDelegate new_number = delegate (bool b)
            {
                if ( b == true )
                {
                    return numbers.Last();
                }
                else
                {
                    return new DiffItem(0, 0);
                }

            };
            //??
            LookHeadDelegate look = delegate (int value, int m)
            {
                int result = value + ( m );
                return result;
            };
            CheckDelegate check = delegate (int pos, int expect)
            {
                //if true coutinue if no coutinue start
                if ( list[ pos ] != expect )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
            // change to for an make value
            for ( int w = 0 ; w < list.Length ; w++ )
            {
                int value = list[ w ];
                DiffItem diff = new_number(flagnew);
                flagnew = false;

                diff.Start = value;
                //happens exactly once


                bool ended = false;
                int jump = 0;
                for ( int leng = 0 ; leng < how_far ; leng++ )
                {
                    int expect = look(value, leng);
                    //true to be wrong
                    if ( check(leng + w, expect) )
                    {


                        //result[counter] = value;
                        //counter = counter + 1;

                        jump = leng;
                        ended = true;
                    }
                    else
                    {
                        numbers.AddLast(diff);
                        diff = new_number(true);
                        diff.Start = value;
                        diff.End = value;
                        // result[counter] = sim;
                        // z = z + 1;
                        jump = leng;
                        if ( ended == true )
                        {

                            ended = false;
                        }
                    }



                }
                if ( ended == true )
                {
                    diff.End = value;
                    numbers.AddLast(diff);
                    flagnew = true;
                }
                // we need to over jump what we already have
                w = w + jump;

            }
            return numbers;
        }
        public static bool Compare(LinkedList<DiffItem> ld1, LinkedList<DiffItem> ld2)
        {
            bool result = true;

            // s1 should be the longer one
         

            DiffItem[] s1 = ld1.ToArray();
            DiffItem[] s2 = ld2.ToArray();

            for ( int i = 0 ; i < s1.Length ; i++ )
            {

                // outside of index
                if ( ( i ) >= ( s2.Length ) )
                {
                    result = false;
                    break;
                }

                int diff = Math.Abs(s1[ i ].Start - s2[ i ].Start);
                if ( diff > preDiff )
                {
                    bool result2 = false;

                    for ( int j = 0 ; j < s2.Length ; j++ )
                    {

                        int diff2 = Math.Abs(s1[ i ].Start - s2[ j ].Start);

                        if ( diff2 <= preDiff )
                        {
                            result2 = true;
                            i = j;
                            break;
                        }



                    }
                    if ( result2 == false )
                    {
                        result = false;
                        break;
                    }

                }
                if ( ( i ) >= ( s1.Length ) )
                {
                    result = true;
                    break;
                }
                if ( ( i ) >= ( s2.Length ) )
                {
                    result = false;
                    break;
                }
               

                int diff3 = Math.Abs(s1[ i ].End - s2[ i ].End);
                if ( diff3 > postDiff )
                {
                    //bool result2 = false;
                    //j = 0;
                    //for (int j = 0; j < s2.Length; j++)
                    //{
                    //    int diff2 = Math.Abs(s1[j] - s2[i]);

                    //    if (diff2 <= postDiff)
                    //    {
                    //        result2 = true;
                    //        i = j;
                    //        break;
                    //    }



                    //}
                    //if (result2 == false)
                    //{
                    result = false;
                    break;
                    //}

                }

            }


            return !result;
        }
        public static bool Compare2(int[] s1, int[] s2)
        {
            bool result = true;

            // s1 should be the longer one
            bool checker = true;

            for ( int i = 0 ; i < s1.Length ; i++ )
            {

                // outside of index
                if ( ( i + 1 ) > ( s2.Length ) )
                {
                    result = false;
                    break;
                }
                if ( checker )
                {
                    int diff = Math.Abs(s1[ i ] - s2[ i ]);
                    if ( diff > preDiff )
                    {
                        bool result2 = false;

                        for ( int j = 0 ; j < s2.Length ; j++ )
                        {
                            int diff2 = Math.Abs(s1[ j ] - s2[ i ]);

                            if ( diff2 <= preDiff )
                            {
                                result2 = true;
                                i = j;
                                break;
                            }



                        }
                        if ( result2 == false )
                        {
                            result = false;
                            break;
                        }

                    }
                    checker = false;
                }
                else
                {
                    int diff = Math.Abs(s1[ i ] - s2[ i ]);
                    if ( diff > postDiff )
                    {
                        //bool result2 = false;
                        //j = 0;
                        //for (int j = 0; j < s2.Length; j++)
                        //{
                        //    int diff2 = Math.Abs(s1[j] - s2[i]);

                        //    if (diff2 <= postDiff)
                        //    {
                        //        result2 = true;
                        //        i = j;
                        //        break;
                        //    }



                        //}
                        //if (result2 == false)
                        //{
                        result = false;
                        break;
                        //}

                    }
                    checker = true;
                }

            }
            return !result;
        }
        public static int[] GetDiffIndex(string str1, string str2)
        {
            List<int> list = new List<int>();


            if ( str1 == null || str2 == null )
            {
                return list.ToArray<int>();
            }

            int length = Math.Min(str1.Length, str2.Length);

            for ( int index = 0 ; index < length ; index++ )
            {
                if ( str1[ index ] != str2[ index ] )
                {

                    list.Add(index);
                }
            }
            if ( str1.Length != str2.Length )
            {

            }
            return list.ToArray<int>();
        }
        public static string ShowDiff(string alpha, string beta)
        {

            List<string> diff;
            IEnumerable<string> set1 = alpha.Split(' ').Distinct();
            IEnumerable<string> set2 = beta.Split(' ').Distinct();

            if ( set2.Count() > set1.Count() )
            {
                diff = set2.Except(set1).ToList();
            }
            else
            {
                diff = set1.Except(set2).ToList();
            }

            StringBuilder builder = new StringBuilder();
            foreach ( string value in diff )
            {
                builder.Append(value);
                builder.Append('.');
            }
            return builder.ToString();

        }
        public static void MergeDiffIndex(int[] alpha, int[] beta)
        {
            List<int> list = new List<int>();

            for ( int index = 0 ; index < alpha.Length ; index++ )
            {

                if ( alpha[ index ] != beta[ index ] )
                {


                }
                else
                {
                    list.Add(alpha[ index ]);
                }
            }
        }
    }
}
