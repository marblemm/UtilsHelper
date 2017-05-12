using System.IO;

namespace UtilsHelper.XmlHelper
{
    public class FileHelper
    {
        public static string ReadAllText(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// 如果你要读取的文件内容不是很多，可以使用 File.ReadAllText(FilePath) 或指定编码方式 File.ReadAllText(FilePath, Encoding)的方法。
        /// 它们都一次将文本内容全部读完，并返回一个包含全部文本内容的字符串string str = File.ReadAllText(@"c:\temp\ascii.txt");
        /// 也可以指定编码方式 string str2 = File.ReadAllText(@"c:\temp\ascii.txt", Encoding.ASCII);
        /// 也可以使用方法File.ReadAllLines。该方法返回一个字符串数组。每一行都是一个数组元素。string[] strs = File.ReadAllLines(@"c:\temp\ascii.txt");
        /// 也可以指定编码方式 string[] strs2 = File.ReadAllLines(@"c:\temp\ascii.txt", Encoding.ASCII);
        /// 当文本的内容比较大时，我们就不要将文本内容一次读完，而应该采用流（Stream）的方式来读取内容。
        /// .Net为我们封装了StreamReader类。初始化StreamReader类有很多种方式。
        /// 下面我罗列出几种StreamReader sr1 = new StreamReader(@"c:\temp\utf-8.txt");
        // 同样也可以指定编码方式 StreamReader sr2 = new StreamReader(@"c:\temp\utf-8.txt", Encoding.UTF8);
        /// FileStream fs = new FileStream(@"C:\temp\utf-8.txt", FileMode.Open, FileAccess.Read, FileShare.None);
        /// StreamReader sr3 = new StreamReader(fs);
        /// StreamReader sr4 = new StreamReader(fs, Encoding.UTF8);FileInfo myFile = new FileInfo(@"C:\temp\utf-8.txt");
        /// OpenText 创建一个UTF-8 编码的StreamReader对象 StreamReader sr5 = myFile.OpenText();
        /// OpenText 创建一个UTF-8 编码的StreamReader对象 StreamReader sr6 = File.OpenText(@"C:\temp\utf-8.txt");
        /// 初始化完成之后，你可以每次读一行，也可以每次读一个字符 ，还可以每次读几个字符，甚至也可以一次将所有内容读完。
        /// 读一行 string nextLine = sr.ReadLine();
        /// 读一个字符 int nextChar = sr.Read();
        /// 读100个字符 int nChars = 100;
        /// char[] charArray = new char[nChars];
        /// int nCharsRead = sr.Read(charArray, 0, nChars);
        /// 全部读完 string restOfStream = sr.ReadToEnd();
        /// 使用完StreamReader之后，不要忘记关闭它： sr.Closee();假如我们需要一行一行的读，将整个文本文件读完，
        /// 下面看一个完整的例子：
        /// StreamReader sr = File.OpenText(@"C:\temp\ascii.txt");
        /// string nextLine; 
        /// while ((nextLine = sr.ReadLine()) != null) 
        /// { 
        ///     Console.WriteLine(nextLine); 
        /// }
        /// sr.Close();
        /// 
        /// 从头到尾以流的方式读出文本文件
        /// /该方法会一行一行读出文本
        /// using (System.IO.StreamReader sr = new System.IO.StreamReader(@"C:\testDir\test.txt"))
        /// {
        ///     string str;
        ///     while ((str = sr.ReadLine()) != null)
        ///     {
        ///         Console.WriteLine(str);
        ///     }
        /// } 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string[] ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        public static void WriteAllText(string filePath, string text)
        {
            File.WriteAllText(filePath, text);
        }

        /// <summary>
        /// 写文件和读文件一样，如果你要写入的内容不是很多，可以使用File.WriteAllText方法来一次将内容全部写如文件。 
        /// 如果你要将一个字符串的内容写入文件，可以用File.WriteAllText(FilePath) 或指定编码方式 File.WriteAllText(FilePath, Encoding)方法。
        /// string str1 = "Good Morning!"; File.WriteAllText(@"c:\temp\test\ascii.txt", str1); // 
        /// 也可以指定编码方式 File.WriteAllText(@"c:\temp\test\ascii-2.txt", str1, Encoding.ASCII);
        /// 如果你有一个字符串数组，你要将每个字符串元素都写入文件中，可以用File.WriteAllLines方法：
        /// string[] strs = { "Good Morning!", "Good Afternoon!" }; 
        /// File.WriteAllLines(@"c:\temp\ascii.txt", strs); 
        /// File.WriteAllLines(@"c:\temp\ascii-2.txt", strs, Encoding.ASCII);
        /// 使用File.WriteAllText或File.WriteAllLines方法时，如果指定的文件路径不存在，会创建一个新文件；如果文件已经存在，则会覆盖原文件。
        /// 当要写入的内容比较多时，同样也要使用流（Stream）的方式写入。.Net封装的类是StreamWriter。初始化StreamWriter类同样有很多方式：
        /// 如果文件不存在，创建文件； 如果存在，覆盖文件 
        /// StreamWriter sw1 = new StreamWriter(@"c:\temp\utf-8.txt");
        /// 也可以指定编码方式 
        /// true 是 append text, false 为覆盖原文件 
        /// StreamWriter sw2 = new StreamWriter(@"c:\temp\utf-8.txt", true, Encoding.UTF8);
        /// // FileMode.CreateNew: 如果文件不存在，创建文件；如果文件已经存在，抛出异常 
        /// FileStream fs = new FileStream(@"C:\temp\utf-8.txt", FileMode.CreateNew, FileAccess.Write, FileShare.Read);
        /// UTF-8 为默认编码 
        /// StreamWriter sw3 = new StreamWriter(fs);
        /// StreamWriter sw4 = new StreamWriter(fs, Encoding.UTF8);
        /// 如果文件不存在，创建文件； 如果存在，覆盖文件 
        /// FileInfo myFile = new FileInfo(@"C:\temp\utf-8.txt");
        /// StreamWriter sw5 = myFile.CreateText();
        /// 初始化完成后，可以用StreamWriter对象一次写入一行，一个字符，一个字符数组，甚至一个字符数组的一部分。
        /// 写一个字符     sw.Write('a');
        /// 写一个字符数组 char[] charArray = new char[100];
        /// initialize these characters 
        /// sw.Write(charArray);
        /// 写一个字符数组的一部分 
        /// sw.Write(charArray, 10, 15);
        /// 同样，StreamWriter对象使用完后，不要忘记关闭。sw.Close(); 最后来看一个完整的使用StreamWriter一次写入一行的例子：
        /// FileInfo myFile = new FileInfo(@"C:\temp\utf-8.txt");
        ///  StreamWriter sw = myFile.CreateText();
        /// string[] strs = { "早上好", "下午好" };        
        /// foreach (var s in strs) 
        /// { 
        ///     sw.WriteLine(s); 
        /// }
        /// sw.Close();
        /// 
        /// 
        /// 在将文本写入文件前，处理文本行
        /// //StreamWriter一个参数默认覆盖
        /// //StreamWriter第二个参数为false覆盖现有文件，为true则把文本追加到文件末尾
        /// using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\testDir\test2.txt", true))
        /// {
        ///     foreach (string line in lines)
        ///     {
        ///         if (!line.Contains("second"))
        ///         {
        ///             file.Write(line);//直接追加文件末尾，不换行
        ///             file.WriteLine(line);// 直接追加文件末尾，换行   
        ///         }
        ///     }
        /// }
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="text"></param>
        public static void WriteAllLines(string filePath, string[] text)
        {
            File.WriteAllLines(filePath, text);
        }
    }
}