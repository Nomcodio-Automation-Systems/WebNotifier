using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebNotifier
{
    class StringAddon
    {
        private int preDiff = 10;
        private int postDiff = 25;
        private int how_far = 10;
        delegate DiffItem InternDelegate(bool s);
        delegate int LookHeadDelegate(int a, int b);
        delegate bool CheckDelegate(int pos, int expect);

        public T GetSafeObjectFromDic<T>(Dictionary<string, T> dic, string key)
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }

            return default(T);
        }
        public void InsertSafeList<T>(List<T> list, T obj, int pos)
        {
            int numbers_count = list.Count;

            if ((pos + 1) > numbers_count)
            {

                if (((pos + 1) - numbers_count) == 1)
                {
                    list.Add(obj);
                    return;
                }
                for (int i = 0; i < (pos - numbers_count); i++)
                {
                    list.Add(default(T));
                }
                list.Add(obj);
                return;
            }
            else
            {
                list[pos] = obj;
            }

        }
        public T GetSafeFromList<T>(List<T> list, int index)
        {
            int numbers_count = list.Count;

            if ((index + 1) > numbers_count)
            {
                return default(T);
            }

            if (list[index] == null)
            {
                return default(T);
            }
            if (list[index] is string)
            {
                if (list[index].ToString() == "")
                {
                    return default(T);
                }
            }
            return list[index];
        }
        public void InsertSafeCollection(StringCollection collect, string str, int pos)
        {


            if (collect == null)
            {
                collect = new StringCollection();
            }
            int numbers_count = collect.Count;

            if ((pos + 1) > numbers_count)
            {

                if (((pos + 1) - numbers_count) == 1)
                {
                    collect.Add(str);
                    return;
                }
                for (int i = 0; i < (pos - numbers_count); i++)
                {
                    collect.Add(null);
                }
                collect.Add(str);
                return;
            }
            else
            {
                collect[pos] = str;
            }

        }
        public int CountCharDiffer(string alpha, string beta)
        {
            string diff_string = ShowDiff(alpha, beta);

            int len = diff_string.Length;
            char[] src = diff_string.ToCharArray();
            int dstIdx = 0;
            for (int i = 0; i < len; i++)
            {
                char ch = src[i];
                switch (ch)
                {
                    case '\u0020':
                    case '\u00A0':
                    case '\u1680':
                    case '\u2000':
                    case '\u2001':
                    case '\u2002':
                    case '\u2003':
                    case '\u2004':
                    case '\u2005':
                    case '\u2006':
                    case '\u2007':
                    case '\u2008':
                    case '\u2009':
                    case '\u200A':
                    case '\u202F':
                    case '\u205F':
                    case '\u3000':
                    case '\u2028':
                    case '\u2029':
                    case '\u0009':
                    case '\u000A':
                    case '\u000B':
                    case '\u000C':
                    case '\u000D':
                    case '\u0085':
                        continue;
                    default:
                        src[dstIdx++] = ch;
                        break;
                }
            }
            string result = new string(src, 0, dstIdx);

            return result.Count();
        }


        public int[] DifferI(string source1, string source2)
        {


            List<string> diff;
            IEnumerable<string> set1 = source1.Split(' ').Distinct();
            IEnumerable<string> set2 = source2.Split(' ').Distinct();
            //int[] diffArrayPos = new int[1024];
            List<int> diffListPos = new List<int>();

            if (set2.Count() > set1.Count())
            {
                diff = set2.Except(set1).ToList();
                System.Collections.IList c1 = set2.ToList();

                int i = 0;
                int j = 0;
                foreach (string value in c1)
                {
                    //{
                    //    if (j >= 1024)
                    //    {
                    //        break;
                    //    }
                    if ((diff.ToArray()).Length <= j)
                    {
                        break;
                    }
                    if (diff[j] == value)
                    {
                        diffListPos.Add(i);
                        // diffArrayPos[j] = i;
                        j++;
                    }

                    i++;
                }
            }
            else
            {
                diff = set1.Except(set2).ToList();

                System.Collections.IList c1 = set1.ToList();

                int i = 0;
                int j = 0;
                foreach (string value in c1)
                {
                    //if (j >= 1024)
                    //{
                    //    break;
                    //}
                    if ((diff.ToArray()).Length <= j)
                    {
                        break;
                    }
                    if (diff[j] == value)
                    {
                        // diffArrayPos[j] = i;
                        diffListPos.Add(i);
                    }

                    i++;
                }
            }
            return diffListPos.ToArray();
        }

        public List<DiffItem> Analyse(string source1, string source2)
        {
            int[] paku = DifferI(source1, source2);
            List<DiffItem> result = ShrinkAConvert(paku);
            return result;
        }
        public List<DiffItem> ShrinkAConvert(int[] list)
        {
            if (list.Length == 0)
            {
                List<DiffItem> dummy = new List<DiffItem>();
                DiffItem d1 = new DiffItem(0, 0)
                {
                    NoDiff = true
                };
                dummy.Add(d1);
                return dummy;

            }

            Stack<DiffItem> stack = new Stack<DiffItem>();
            int save_item = list[0];
            int save_index = 0;
            for (int i = 0; i < list.Length;)
            {

                DiffItem a = new DiffItem(0, 0)
                {
                    Start = save_item,
                    End = save_item
                };
                stack.Push(a);



                for (int w = save_index; w < list.Length; w++)
                {
                    save_index = w;
                    if (list[w] - save_item > how_far)
                    {
                        save_item = list[w];
                        a.End = list[w - 1];
                        break;
                    }

                }

                i = save_index;
                if (i >= list.Length - 1)
                {
                    break;
                }
            }
            Stack<DiffItem> stack_b = new Stack<DiffItem>();

            while (stack.Count > 0)
            {

                stack_b.Push(stack.Pop());
            }
            List<DiffItem> re = stack_b.ToList<DiffItem>();
            return re;
        }
        public List<DiffItem> ShrinkBConvert(int[] list)
        {
            if (list.Length == 0)
            {
                List<DiffItem> dummy = new List<DiffItem>();
                DiffItem d1 = new DiffItem(0, 0)
                {
                    NoDiff = true
                };
                dummy.Add(d1);
                return dummy;

            }
            // no checks here
            int begin = list[0];
            List<DiffItem> numbers = new List<DiffItem>();





            bool flagnew = false;
            InternDelegate new_number = delegate (bool b)
            {
                if (b == true)
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
                int result = value + (m);
                return result;
            };
            CheckDelegate check = delegate (int pos, int expect)
            {
                //if true continue if no continue start
                if (list[pos] != expect)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
            // change to for an make value
            for (int w = 0; w < list.Length; w++)
            {
                int value = list[w];
                DiffItem diff = new_number(flagnew);
                flagnew = false;

                diff.Start = value;
                //happens exactly once


                bool ended = false;
                int jump = 0;
                for (int leng = w; leng < (how_far + w) && (leng + w) < list.Length; leng++)
                {
                    int expect = look(value, leng);
                    //true to be wrong
                    if (check(leng + w, expect))
                    {


                        //result[counter] = value;
                        //counter = counter + 1;

                        jump = leng;
                        ended = true;
                    }
                    else
                    {
                        numbers.Add(diff);
                        diff = new_number(true);
                        diff.Start = value;
                        diff.End = value;
                        // result[counter] = sim;
                        // z = z + 1;
                        jump = leng;
                        if (ended == true)
                        {

                            ended = false;
                        }
                    }



                }
                if (ended == true)
                {
                    diff.End = value;
                    numbers.Add(diff);
                    flagnew = true;
                }
                // we need to over jump what we already have
                w = w + jump;

            }
            numbers = numbers.Distinct().ToList();
            return numbers;
        }
        public bool Compare2DiffItemLists(List<DiffItem> ld1, List<DiffItem> ld2)
        {
            bool result = true;

            // s1 should be the longer one


            DiffItem[] s1 = ld1.ToArray();
            DiffItem[] s2 = ld2.ToArray();

            for (int i = 0; i < s1.Length; i++)
            {

                // outside of index
                if ((i) >= (s2.Length))
                {
                    result = false;
                    break;
                }

                int diff = Math.Abs(s1[i].Start - s2[i].Start);
                if (diff > preDiff)
                {
                    bool result2 = false;

                    for (int j = 0; j < s2.Length; j++)
                    {

                        int diff2 = Math.Abs(s1[i].Start - s2[j].Start);

                        if (diff2 <= preDiff)
                        {
                            result2 = true;
                            i = j;
                            break;
                        }



                    }
                    if (result2 == false)
                    {
                        result = false;
                        break;
                    }

                }
                if ((i) >= (s1.Length))
                {
                    result = true;
                    break;
                }
                if ((i) >= (s2.Length))
                {
                    result = false;
                    break;
                }


                int diff3 = Math.Abs(s1[i].End - s2[i].End);
                if (diff3 > postDiff)
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
        public bool Compare2(int[] s1, int[] s2)
        {
            bool result = true;

            // s1 should be the longer one
            bool checker = true;

            for (int i = 0; i < s1.Length; i++)
            {

                // outside of index
                if ((i + 1) > (s2.Length))
                {
                    result = false;
                    break;
                }
                if (checker)
                {
                    int diff = Math.Abs(s1[i] - s2[i]);
                    if (diff > preDiff)
                    {
                        bool result2 = false;

                        for (int j = 0; j < s2.Length; j++)
                        {
                            int diff2 = Math.Abs(s1[j] - s2[i]);

                            if (diff2 <= preDiff)
                            {
                                result2 = true;
                                i = j;
                                break;
                            }



                        }
                        if (result2 == false)
                        {
                            result = false;
                            break;
                        }

                    }
                    checker = false;
                }
                else
                {
                    int diff = Math.Abs(s1[i] - s2[i]);
                    if (diff > postDiff)
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
        public int[] GetDiffIndex(string str1, string str2)
        {
            List<int> list = new List<int>();


            if (str1 == null || str2 == null)
            {
                return list.ToArray<int>();
            }

            int length = Math.Min(str1.Length, str2.Length);

            for (int index = 0; index < length; index++)
            {
                if (str1[index] != str2[index])
                {

                    list.Add(index);
                }
            }
            if (str1.Length != str2.Length)
            {

            }
            return list.ToArray<int>();
        }
        public string ShowDiff(string alpha, string beta)
        {

            List<string> diff;
            IEnumerable<string> set1 = alpha.Split(' ').Distinct();
            IEnumerable<string> set2 = beta.Split(' ').Distinct();

            if (set2.Count() > set1.Count())
            {
                diff = set2.Except(set1).ToList();
            }
            else
            {
                diff = set1.Except(set2).ToList();
            }

            StringBuilder builder = new StringBuilder();
            foreach (string value in diff)
            {
                builder.Append(value);
                builder.Append('.');
            }
            return builder.ToString();

        }
        public void MergeDiffIndex(int[] alpha, int[] beta)
        {
            List<int> list = new List<int>();

            for (int index = 0; index < alpha.Length; index++)
            {

                if (alpha[index] != beta[index])
                {


                }
                else
                {
                    list.Add(alpha[index]);
                }
            }
        }
    }
}
