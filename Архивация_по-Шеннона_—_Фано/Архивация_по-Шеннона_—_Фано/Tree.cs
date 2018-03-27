using System;
using System.Text;

namespace Compression_Data
{
    class Tree
    {
        protected class node
        {
            public node lbranch;
            public node rbranch;
            private char letter;
            public node(node lchild, node rchild)
            {
                lbranch = lchild;
                rbranch = rchild;
            }
            public char Word
            {
                set
                {
                    letter = value;
                }
                get
                {
                    return letter;
                }
            }
        }
        node root = new node(null,null);
        public void creat_tree(string input)
        {
            string _temp = "";
            while (input != "")
            {
                try
                {
                    _temp = input.Remove(input.IndexOf("~"));
                }
                catch (Exception)
                {
                    add_node(input[0], input.Remove(0, 1));
                    break;
                }

                try
                {
                    add_node(_temp[0], _temp.Remove(0, 1));
                    input = input.Remove(0, _temp.Length + 1);
                }
                catch (Exception)
                {
                    input = input.Remove(0, 1);
                    _temp+="~" + input.Remove(input.IndexOf("~"));
                    add_node(_temp[0], _temp.Remove(0, 1));
                    input = input.Remove(0, _temp.Length);
                }
            }
        }
        private void add_node(char letter, string route)
        {
            node _node = root;
            for (int i = 0; i < route.Length; i++)
            {
                if(route[i] == '0')
                {
                    if(_node.lbranch == null)
                        _node.lbranch = new node(null, null);
                    _node = _node.lbranch;
                }
                else if(route[i] == '1')
                {
                    if(_node.rbranch == null)
                        _node.rbranch = new node(null, null);
                    _node = _node.rbranch;
                }
                else
                {
                    root = null;
                }
            }
            _node.Word = letter;
        }
        public StringBuilder tree_search(System.Text.StringBuilder input)
        {
            StringBuilder _text = new StringBuilder();
            node _node = root;
            for (int i = 0;;)
            {
                if (i == input.Length)
                    break;
                if (_node.lbranch == null && _node.rbranch == null)
                {
                    _text.Append(_node.Word);
                    _node = root;
                }
                else if (input[i] == '0')
                {
                    _node = _node.lbranch;
                    input.Remove(0, 1);
                }
                else if(input[i] == '1')
                {
                    _node = _node.rbranch;
                    input.Remove(0, 1);
                }
            }
            return _text;
        }
    }
}
