using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Compression_Data
{
    class Shannon_Fano
    {
        ProcessSF sf = new ProcessSF();
        public void EncodeText(string pathToFile, string fileName,string pathToSave, bool ToLower)
        {
            _encodeText(pathToFile, fileName, pathToSave, ToLower);
        }
        public string EncodeText(string text,bool ToLower)
        {
           return _encodeText(text,ToLower);
        }
        public void DecodeText(string pathToFile, string fileName, string pathToSave)
        {
            _decodeText(pathToFile, fileName, pathToSave);
        }
        public double Efficiency
        {
            get
            {
                if (sf.Table != null)
                {
                    double _effic = 0.0D;
                    for (int i = 0; i < sf.Table.Count; i++)
                    {
                        _effic += -sf.Table[i].pi * Math.Log(sf.Table[i].pi);
                    }
                    return _effic;
                }
                else
                    return 0.0D;
            }
        }

        public string Table
        {
            get
            {
                List<node> table = sf.Table;

                string _table = "";

                for (int i = 0; i < table.Count; i++)
                    _table += table[i].letter + " - " + table[i].code + "\r";
                return _table;

            }
        }
        #region Приватные методы
        private void _encodeText(string pathToFile, string fileName, string pathToSave, bool ToLower)
        {
            string _readTextFile;
            using (StreamReader sr = new StreamReader(pathToFile + fileName, System.Text.Encoding.Default))
            {
                _readTextFile = sr.ReadToEnd();
            }
            string binarySF = ToLower == true ? sf.EncodeSF(_readTextFile.ToLower()) : sf.EncodeSF(_readTextFile);
            string tail = binarySF.Length % 8 == 0 ? "":binarySF.Remove(0, binarySF.Length - (binarySF.Length % 8));
            if(tail != "")
                binarySF = binarySF.Remove(binarySF.Length - (binarySF.Length % 8));
            byte[] bytes = new byte[binarySF.Length / 8];

            for (int i = 0; i < bytes.Length; ++i)
                bytes[i] = Convert.ToByte(binarySF.Substring(8 * i, 8), 2);//надо ускорить 78 мс

            string translate = System.Text.Encoding.Default.GetString(bytes);//Unicode.GetString(bytes);

            List<node> table = sf.Table;

            string _table = "";

            for (int i = 0; i < table.Count; i++)
                _table += table[i].letter + table[i].code + "~";
            _table += tail;

            translate += "**" + _table;
            File.WriteAllText(pathToSave + fileName + "sf", translate, Encoding.Default);
        }
        private string _encodeText(string text, bool ToLower)
        {
            return ToLower == true ? sf.EncodeSF(text.ToLower()) : sf.EncodeSF(text);
        }
        private void _decodeText(string pathToFile, string fileName, string pathToSave)
        {
            string _readTextFile;
            using (StreamReader sr = new StreamReader(pathToFile + fileName, System.Text.Encoding.Default))
            {
                _readTextFile = sr.ReadToEnd();
            }

            string text = _readTextFile.Remove(_readTextFile.LastIndexOf("**"));
            _readTextFile = _readTextFile.Replace(text + "**", "");
            string tail = _readTextFile.Remove(0, _readTextFile.LastIndexOf("~") + 1);
            _readTextFile = _readTextFile.Remove(_readTextFile.LastIndexOf("~"));
            byte[] back = System.Text.Encoding.Default.GetBytes(text);

            StringBuilder binarySF = byte_to_bit(back);
            binarySF.Append(tail);

            binarySF = sf.DecodeSF(binarySF, _readTextFile);

            File.WriteAllText(pathToSave +"(1)"+ fileName.Remove(fileName.Length - 2), binarySF.ToString(), System.Text.Encoding.Default);//Unicode);, dec, System.Text.Encoding.Default);//Unicode);
        }
        private StringBuilder byte_to_bit(byte[] bytes)
        {
            StringBuilder _b = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                _b.Append(Convert.ToString(bytes[i], 2).PadLeft(8, '0'));
            return _b;
        }
        #endregion
    }
}
