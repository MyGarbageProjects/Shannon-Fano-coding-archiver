using System;
using System.Collections.Generic;
using System.Text;

namespace Compression_Data
{
    class ProcessSF
    {
        SF sf;
        List<node> table;
        #region Публичные методы
        public string EncodeSF(string text)
        {
            sf = new SF(text);
            table = sf.Table;
            return toString(text);//Объемная
        }
        public StringBuilder DecodeSF(StringBuilder input, string table)
        {
            Tree tree = new Tree();
            tree.creat_tree(table);
            return tree.tree_search(input);

        }
        public List<node> Table
        {
            get
            {
                return table;
            }
            set
            {
                table = value;
            }
        }
        public override string ToString()
        {
            if (Table != null)
            {
                string tostring = "";
                for (int i = 0; i < Table.Count; i++)
                    tostring += Table[i].letter + " - " + Table[i].code + "\r\n";
                return tostring;
            }
            else
                return "Таблица пуста";
        }
        #endregion
        #region Приватные методы
        private string toString(string text)
        {
            string str = text;
            #region Проверяем на наличие 0 и 1 в таблице

            int ITzero = check_on_zero();
            int ITone = check_on_one();
            if(ITzero != -1 || ITone != -1)
            {
                int start = 0;
                for (int i = 0; i < Table[ITzero].count + Table[ITone].count; i++)
                {
                    int Index = str.IndexOfAny(new char[] { '0', '1' }, start);
                    if (str[Index] == '0')
                    { str = str.Insert(Index + 1, Table[ITzero].code); str = str.Remove(Index, 1); start = Index + Table[ITzero].code.Length; }
                    else if (str[Index] == '1')
                    { str = str.Insert(Index + 1, Table[ITone].code); str = str.Remove(Index, 1); start = Index + Table[ITone].code.Length; }
                }
            }
            #endregion
            StringBuilder asd = new StringBuilder(str);
            if (table != null)
            {
                for (int i = table.Count-1; i >= 0; i--)
                {
                    if (Table[i].letter == "0" || Table[i].letter == "1")
                        continue;
                    str = str.Replace(table[i].letter, table[i].code);
                }
                return str;
            }
            return "";

        }
        private int check_on_zero()
        {
            for (int i = 0; i < table.Count; i++)
                if (table[i].letter == "0")
                    return i;
            return -1;
        }
        private int check_on_one()
        {
            for (int i = 0; i < table.Count; i++)
                if (table[i].letter == "1")
                    return i;
            return -1;
        }
        #endregion
    }//Shannon-Fano public class
}
public class node
{
    public string code;
    public string letter;
    public int count;
    public double pi;
}
class SF
{
    protected class branch
    {
        public branch left;
        public branch right;
    }
    List<node> mainlist;
    public SF(string text)
    {
        mainlist = new List<node>();
        count_duplicate_letters(text);//Объемная
        calculation_of_the_letter_code(mainlist);
    }
    public List<node> Table
    {
        get
        {
            return mainlist;
        }
    }
    private void calculation_of_the_letter_code(List<node> list)
    {
        if (list.Count > 1)
        {
            List<node> _left = list;
            double total = total_count_pi(_left);
            _left = Cut(_left, total / 2);
            List<node> _right = new List<node>();
            for (int i = _left.Count; i < list.Count; i++)
                _right.Add(list[i]);
            add_branch(_left, '0');
            add_branch(_right, '1');
            if(_left.Count > 1)
                calculation_of_the_letter_code(_left);
            if(_right.Count > 1)
                calculation_of_the_letter_code(_right);
        }
    }
    private void add_branch(List<node> list, char set)
    {
        int _index = indexOf(mainlist, list[0].letter);
        for (int i = _index; i < list.Count + _index; i++)
            mainlist[i].code += set;
    }
    private List<node> Cut(List<node> list, double sum)
    {
        double _tempSum = 0;
        List<node> _tempList = new List<node>();
        int _index = -1;
        for (int i = 0; i < list.Count; i++)
        {
            if (_tempSum >= sum) { _index = i - 1; break; }
            _tempList.Add(list[i]);
            _tempSum += list[i].pi;
        }
        if (list.Count <= 2)
            return _tempList;
        double leftSum = _tempSum;
        double asdasd = total_count_pi(list);
        double rightSum = asdasd - leftSum;
        double Ldifference = leftSum - rightSum;
        double Rdifference = (leftSum - list[_index].pi) - (rightSum + list[_index].pi);
        if (Math.Abs(Ldifference) < Math.Abs(Rdifference))
        {
            return _tempList;
        }
        _tempList.RemoveAt(_index);
        return _tempList;
    }
    private int total_count_letters(List<node> list)
    {
        int _total = 0;
        for (int i = 0; i < list.Count; i++)
            _total += list[i].count;
        return _total;
    }
    private double total_count_pi(List<node> list)
    {
        double _total = 0.0f;
        for (int i = 0; i < list.Count; i++)
            _total += list[i].pi;
        return _total;
    }
    private void count_duplicate_letters(string input)
    {
        while (input.Length != 0)
        {
            mainlist.Add(new node { code = "", count = 0, letter = input[0].ToString() });
            int count_letter = input.Length - input.Replace(input[0].ToString(), "").Length;
            mainlist[mainlist.Count - 1].count = count_letter;
            input = input.Replace(input[0].ToString(), "");
        }
        int total = total_count_letters(mainlist);
        for (int i = 0; i < mainlist.Count; i++)
            mainlist[i].pi = (double)mainlist[i].count / total;

        mainlist = sort(mainlist);
    }
    private List<node> sort(List<node> list)
    {
        for (int i = 0; i < list.Count; i++)
            for (int j = 0; j < list.Count - i - 1; j++)
                if (list[j].pi <= list[j + 1].pi)
                {
                    node _temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = _temp;
                }
        return list;
    }
    private int indexOf(List<node> list, string value)
    {
        for (int i = 0; i < list.Count; i++) if (list[i].letter.IndexOf(value) != -1) return i;
        return -1;
    }
}//Shannon-Fano private class