/*
 Programmed by Xeon Stin / HJF
 mkcpp+
 A tool for lazy guys.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web;

namespace mkcpp_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = " ";
            textBox2.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string targetPath = textBox1.Text + '\\';

            if (Directory.Exists(targetPath))
            {
                textBox2.Text = " Exist";
                textBox2.Update();
                return;
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(targetPath);
            directoryInfo.Create();

            string targetFile = targetPath + textBox1.Text + ".in";
            FileStream myFs = new FileStream(targetFile, FileMode.Create);
            myFs.Close();

            targetFile = targetPath + textBox1.Text + ".cpp";
            myFs = new FileStream(targetFile, FileMode.Create);
            StreamWriter mySw = new StreamWriter(myFs);

            mySw.WriteLine("#include<iostream>");
            mySw.WriteLine("#include<algorithm>");
            mySw.WriteLine("#include<cstdio>");
            mySw.WriteLine("");
            mySw.WriteLine("using namespace std;");
            if (checkFileIO.Checked == true || checkFastPower.Checked == true)
            {
                mySw.WriteLine("");
                if (checkFileIO.Checked == true)
                {
                    mySw.WriteLine("inline void set_file_IO(string);");
                    mySw.WriteLine("inline void close_IO(void);");
                }
                if (checkFastPower.Checked == true)
                {
                    mySw.WriteLine("inline long long fast_power(long long, long long, long long);");
                }
                if (checkFastRead.Checked == true)
                {
                    mySw.WriteLine("inline int read(void);");
                }
                mySw.WriteLine("inline void work(void);");
            }
            if (checkBinaryIndexTree.Checked == true)
            {
                mySw.WriteLine("");
                mySw.WriteLine("class binary_index_tree {");
                mySw.WriteLine("    private:");
                mySw.WriteLine("        static const int binaryIndexTreeSize = 1100000;");
                mySw.WriteLine("        int sum[binaryIndexTreeSize];");
                mySw.WriteLine("        virtual inline int lowbit(int index) {");
                mySw.WriteLine("            return index & -index;");
                mySw.WriteLine("        }");
                mySw.WriteLine("        virtual inline int getSum(int index) {");
                mySw.WriteLine("            int result = 0;");
                mySw.WriteLine("            for (; index; index -= lowbit(index)) {");
                mySw.WriteLine("                result += sum[index];");
                mySw.WriteLine("            }");
                mySw.WriteLine("            return result;");
                mySw.WriteLine("        }");
                mySw.WriteLine("    public:");
                mySw.WriteLine("        virtual inline int query(int leftBound, int rightBound) {");
                mySw.WriteLine("            return getSum(rightBound) - getSum(leftBound-1);");
                mySw.WriteLine("        }");
                mySw.WriteLine("        virtual inline int query(int index) {");
                mySw.WriteLine("            int result = sum[index], lca = index - lowbit(index);");
                mySw.WriteLine("            for (--index; index != lca; index -= lowbit(index)) {");
                mySw.WriteLine("                result -= sum[index];");
                mySw.WriteLine("            }");
                mySw.WriteLine("            return result;");
                mySw.WriteLine("        }");
                mySw.WriteLine("        virtual inline void edit(int index, int delta, int n) {");
                mySw.WriteLine("            for (;index <= n; index += lowbit(index)) {");
                mySw.WriteLine("                sum[index] += delta;");
                mySw.WriteLine("            }");
                mySw.WriteLine("        }");
                mySw.WriteLine("};");
            }
            if (checkSegmentTree.Checked == true)
            {
                mySw.WriteLine("");
                mySw.WriteLine("class segment_tree {");
                mySw.WriteLine("    private:");
                mySw.WriteLine("        static const int segmentTreeSize = 300000;");
                mySw.WriteLine("        struct meta {");
                mySw.WriteLine("            int leftBound, rightBound, tag, value;");
                mySw.WriteLine("        } node[segmentTreeSize];");
                mySw.WriteLine("        virtual inline void downTag(int index) {");
                mySw.WriteLine("            if (!node[index].tag) {");
                mySw.WriteLine("                return;");
                mySw.WriteLine("            }");
                mySw.WriteLine("            node[index << 1].tag   += node[index].tag;");
                mySw.WriteLine("            node[index << 1].value += node[index].tag;");
                mySw.WriteLine("            node[(index << 1) + 1].tag   += node[index].tag;");
                mySw.WriteLine("            node[(index << 1) + 1].value += node[index].tag;");
                mySw.WriteLine("            node[index].tag = 0;");
                mySw.WriteLine("        }");
                mySw.WriteLine("    public:");
                mySw.WriteLine("        virtual inline void build(int index, int leftBound, int rightBound) {");
                mySw.WriteLine("            node[index].leftBound  = leftBound;");
                mySw.WriteLine("            node[index].rightBound = rightBound;");
                mySw.WriteLine("            node[index].value      = 0;");
                mySw.WriteLine("            node[index].tag        = 0;");
                mySw.WriteLine("            if (leftBound == rightBound) {");
                mySw.WriteLine("                return;");
                mySw.WriteLine("            }");
                mySw.WriteLine("            int mid = (leftBound + rightBound) >> 1;");
                mySw.WriteLine("            build( index << 1     , leftBound, mid       );");
                mySw.WriteLine("            build((index << 1) + 1, mid + 1  , rightBound);");
                mySw.WriteLine("        }");
                mySw.WriteLine("        virtual inline void edit(int index, int leftBound, int rightBound, int delta) {");
                mySw.WriteLine("            if (leftBound <= node[index].leftBound && node[index].rightBound <= rightBound) {");
                mySw.WriteLine("                node[index].value += delta;");
                mySw.WriteLine("                node[index].tag   += delta;");
                mySw.WriteLine("                return;");
                mySw.WriteLine("            }");
                mySw.WriteLine("            downTag(index);");
                mySw.WriteLine("            int mid = (node[index].leftBound + node[index].rightBound) >> 1;");
                mySw.WriteLine("            if (leftBound <= mid) {");
                mySw.WriteLine("                edit(index << 1, leftBound, rightBound, delta);");
                mySw.WriteLine("            }");
                mySw.WriteLine("            if (rightBound > mid) {");
                mySw.WriteLine("                edit((index << 1) + 1, leftBound, rightBound, delta);");
                mySw.WriteLine("            }");
                mySw.WriteLine("            node[index].value = node[index << 1].value + node[(index << 1) + 1].value;");
                mySw.WriteLine("        }");
                mySw.WriteLine("        virtual inline int request(int index, int leftBound, int rightBound) {");
                mySw.WriteLine("            if (leftBound <= node[index].leftBound && node[index].rightBound <= rightBound) {");
                mySw.WriteLine("                return node[index].value;");
                mySw.WriteLine("            }");
                mySw.WriteLine("            downTag(index);");
                mySw.WriteLine("            int mid = (node[index].leftBound + node[index].rightBound) >> 1, result = 0;");
                mySw.WriteLine("            if (leftBound <= mid) {");
                mySw.WriteLine("                result += request(index << 1, leftBound, rightBound);");
                mySw.WriteLine("            }");
                mySw.WriteLine("            if (rightBound > mid) {");
                mySw.WriteLine("                result += request((index << 1) + 1, leftBound, rightBound);");
                mySw.WriteLine("            }");
                mySw.WriteLine("            return result;");
                mySw.WriteLine("        }");
                mySw.WriteLine("};");
            }
            if (checkSTTable.Checked == true)
            {
                mySw.WriteLine("");
                mySw.WriteLine("#include<utility>");
                mySw.WriteLine("#include<algorithm>");
                mySw.WriteLine("#include<cstdio>");
                mySw.WriteLine("#include<cctype>");
                mySw.WriteLine("#include<cmath>");
                mySw.WriteLine("");
                mySw.WriteLine("class st_table {");
                mySw.WriteLine("    private:");
                mySw.WriteLine("        static const int STTableSize = 220000;");
                mySw.WriteLine("        int fmax[STTableSize][20], fmin[STTableSize][20];");
                mySw.WriteLine("        virtual inline void initialize(int n) {");
                mySw.WriteLine("            for (int j=1; j<=20; ++j) {");
                mySw.WriteLine("                for (int i=1; i+(1<<j)-1<=n; ++i) {");
                mySw.WriteLine("                    fmin[i][j] = std::min(fmin[i][j-1], fmin[i+(1<<(j-1))][j-1]);");
                mySw.WriteLine("                    fmax[i][j] = std::max(fmax[i][j-1], fmax[i+(1<<(j-1))][j-1]);");
                mySw.WriteLine("                }");
                mySw.WriteLine("            }");
                mySw.WriteLine("        }");
                mySw.WriteLine("        virtual inline int read(void) {");
                mySw.WriteLine("            char ch;");
                mySw.WriteLine("            while (!(isdigit(ch = getchar()) || ch == '-'));");
                mySw.WriteLine("            int res = 0, flg = 1;");
                mySw.WriteLine("            if (ch == '-') flg = -1;");
                mySw.WriteLine("            else res = ch - '0';");
                mySw.WriteLine("            while (isdigit(ch = getchar()))");
                mySw.WriteLine("                (res *= 10) += ch - '0';");
                mySw.WriteLine("            return res * flg;");
                mySw.WriteLine("        }");
                mySw.WriteLine("    public:");
                mySw.WriteLine("        inline std::pair<int, int> query(int leftBound, int rightBound) {");
                mySw.WriteLine("            int t = int(log2(rightBound - leftBound + 1.));");
                mySw.WriteLine("            return std::make_pair(std::min(fmin[leftBound][t], fmin[rightBound-(1<<t)+1][t]), std::max(fmax[leftBound][t], fmax[rightBound-(1<<t)+1][t]));");
                mySw.WriteLine("        }");
                mySw.WriteLine("        inline void read(int n) {");
                mySw.WriteLine("            for (int i=1; i<=n; ++i) {");
                mySw.WriteLine("                fmin[i][0] = fmax[i][0] = read();");
                mySw.WriteLine("            }");
                mySw.WriteLine("            initialize(n);");
                mySw.WriteLine("        }");
                mySw.WriteLine("};");
            }
            if (checkUnionFindSet.Checked == true)
            {
                mySw.WriteLine("");
                mySw.WriteLine("class union_find_set {");
                mySw.WriteLine("    private:");
                mySw.WriteLine("        static const int unionFindSetSize = 100;");
                mySw.WriteLine("        int f[unionFindSetSize];");
                mySw.WriteLine("        virtual inline int find(int x) {");
                mySw.WriteLine("            return (f[x] == x)? x : f[x] = find(f[x]);");
                mySw.WriteLine("        }");
                mySw.WriteLine("    public:");
                mySw.WriteLine("        virtual inline void initialize(int n) {");
                mySw.WriteLine("            for (int i=1; i<=n; ++i) {");
                mySw.WriteLine("                f[i] = i;");
                mySw.WriteLine("            }");
                mySw.WriteLine("        }");
                mySw.WriteLine("        virtual inline void merge(int x, int y) {");
                mySw.WriteLine("            f[find(x)] = find(y);");
                mySw.WriteLine("        }");
                mySw.WriteLine("        virtual inline bool judge(int x, int y) {");
                mySw.WriteLine("            return find(x) == find(y);");
                mySw.WriteLine("        }");
                mySw.WriteLine("};");
            }
            if (checkHugeint.Checked == true)
            {
                mySw.WriteLine("");
                mySw.WriteLine("#include<cstring>");
                mySw.WriteLine("#include<cmath>");
                mySw.WriteLine("#include<cstdio>");
                mySw.WriteLine("#include<algorithm>");
                mySw.WriteLine("");
                mySw.WriteLine("class hugeint {");
                mySw.WriteLine("    private:");
                mySw.WriteLine("        static const unsigned long long hugeintBASE = 10000000000000000LL;");
                mySw.WriteLine("        static const int hugeintSize = 1000;");
                mySw.WriteLine("        int size;");
                mySw.WriteLine("        unsigned long long data[hugeintSize];");
                mySw.WriteLine("    public:");
                mySw.WriteLine("        friend inline hugeint operator +(hugeint x, hugeint y) {");
                mySw.WriteLine("            hugeint result;");
                mySw.WriteLine("            short i;");
                mySw.WriteLine("            unsigned long long temp;");
                mySw.WriteLine("            bool up = 0;");
                mySw.WriteLine("            result.size = std::max(x.size, y.size);");
                mySw.WriteLine("            for (i=1; i<=result.size; ++i) {");
                mySw.WriteLine("                temp = x.data[i] + y.data[i] + up;");
                mySw.WriteLine("                result.data[i] = temp % hugeintBASE;");
                mySw.WriteLine("                up  = temp >= hugeintBASE ? 1:0;");
                mySw.WriteLine("            }");
                mySw.WriteLine("            if (up) {");
                mySw.WriteLine("                result.data[ ++result.size ] = up;");
                mySw.WriteLine("            }");
                mySw.WriteLine("            return result;");
                mySw.WriteLine("        }");
                mySw.WriteLine("        friend inline bool operator <= (hugeint x, hugeint y) {");
                mySw.WriteLine("            if (x.size != y.size) {");
                mySw.WriteLine("                return x.size < y.size;");
                mySw.WriteLine("            }");
                mySw.WriteLine("            int i;");
                mySw.WriteLine("            for (i=x.size; i; --i) {");
                mySw.WriteLine("                if (x.data[i] != y.data[i]) {");
                mySw.WriteLine("                    return x.data[i] < y.data[i];");
                mySw.WriteLine("                }");
                mySw.WriteLine("            }");
                mySw.WriteLine("            return true;");
                mySw.WriteLine("        }");
                mySw.WriteLine("        virtual inline void print(void) {");
                mySw.WriteLine("            short i;");
                mySw.WriteLine("            printf(\"%llu\", data[size]);");
                mySw.WriteLine("            for (i=size-1; i>0; --i) {");
                mySw.WriteLine("                printf(\"%016llu\", data[i]);");
                mySw.WriteLine("            }");
                mySw.WriteLine("        }");
                mySw.WriteLine("        virtual inline void read(void) {");
                mySw.WriteLine("            memset(data, 0, sizeof(data));");
                mySw.WriteLine("            scanf(\" \");");
                mySw.WriteLine("            char temp[hugeintSize];");
                mySw.WriteLine("            scanf(\"%s\", temp);");
                mySw.WriteLine("            short i, j, pointer = strlen(temp), flag;");
                mySw.WriteLine("            size = short(ceil(double(pointer)/16.0));");
                mySw.WriteLine("            if (pointer % 16 == 0) {");
                mySw.WriteLine("                flag = 4;");
                mySw.WriteLine("            } else {");
                mySw.WriteLine("                flag = pointer % 16;");
                mySw.WriteLine("            }");
                mySw.WriteLine("            pointer = 0;");
                mySw.WriteLine("            for (i=1; i<=flag; ++i) {");
                mySw.WriteLine("                data[size] = data[size]*10 + temp[pointer++] - '0';");
                mySw.WriteLine("            }");
                mySw.WriteLine("            for (i=size-1; i>0; --i) {");
                mySw.WriteLine("                for (j=1; j<=16; ++j) {");
                mySw.WriteLine("                    data[i] = data[i]*10 + temp[pointer++] - '0';");
                mySw.WriteLine("                }");
                mySw.WriteLine("            }");
                mySw.WriteLine("        }");
                mySw.WriteLine("};");
            }
            mySw.WriteLine("");
            mySw.WriteLine("int main(void) {");
            if (checkFileIO.Checked == true)
            {
                mySw.WriteLine("    #ifndef ONLINE_JUDGE");
                mySw.Write("        set_file_IO(\""); mySw.Write(textBox1.Text); mySw.WriteLine("\");");
                mySw.WriteLine("    #endif");
            }
            mySw.WriteLine("    //ios::sync_with_stdio(false);");
            mySw.WriteLine("    work();");
            if (checkFileIO.Checked == true)
            {
                mySw.WriteLine("    #ifndef ONLINE_JUDGE");
                mySw.WriteLine("        close_IO();");
                mySw.WriteLine("    #endif");
            }
            mySw.WriteLine("    return 0;");
            mySw.WriteLine("}");
            mySw.WriteLine("");
            mySw.WriteLine("inline void work(void) {");
            mySw.WriteLine("    ");
            mySw.WriteLine("}");
            if (checkFileIO.Checked == true)
            {
                mySw.WriteLine("");
                mySw.WriteLine("inline void set_file_IO(string name) {");
                mySw.WriteLine("    freopen((name + \".in\" ).c_str(), \"r\", stdin );");
                mySw.WriteLine("    freopen((name + \".out\").c_str(), \"w\", stdout);");
                mySw.WriteLine("}");
                mySw.WriteLine("");
                mySw.WriteLine("inline void close_IO(void) {");
                mySw.WriteLine("    fclose(stdin);");
                mySw.WriteLine("    fclose(stdout);");
                mySw.WriteLine("}");
            }
            if (checkFastPower.Checked == true)
            {
                mySw.WriteLine("");
                mySw.WriteLine("inline long long fast_power(long long base, long long power, long long BASE) {");
                mySw.WriteLine("    long long res = 1;");
                mySw.WriteLine("    while (power) {");
                mySw.WriteLine("        if (power & 1) res = (res * base) % BASE;");
                mySw.WriteLine("        power = power >> 1;");
                mySw.WriteLine("        base = (base * base) % BASE;");
                mySw.WriteLine("    }");
                mySw.WriteLine("    return res;");
                mySw.WriteLine("}");
            }
            if (checkFastRead.Checked == true)
            {
                mySw.WriteLine("");
                mySw.WriteLine("#include<cctype>");
                mySw.WriteLine("");
                mySw.WriteLine("inline int read(void) {");
                mySw.WriteLine("    char ch;");
                mySw.WriteLine("    while (!(isdigit(ch = getchar()) || ch == '-'));");
                mySw.WriteLine("    int res = 0, flg = 1;");
                mySw.WriteLine("    if (ch == '-') flg = -1;");
                mySw.WriteLine("    else res = ch - '0';");
                mySw.WriteLine("    while (isdigit(ch = getchar()))");
                mySw.WriteLine("        (res *= 10) += ch - '0';");
                mySw.WriteLine("    return res * flg;");
                mySw.WriteLine("}");
			}
			mySw.WriteLine("");
			mySw.Close();
            myFs.Close();
            textBox2.Text = " Completed";
            textBox2.Update();
            if (checkExit.Checked == true)
            {
                this.Close();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.button1_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
