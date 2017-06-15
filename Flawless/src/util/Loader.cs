using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using _Flawless.actors;

namespace _Flawless.util
{
    public static class Loader
    {
        
        public static bool LoadFromFile(string filePath, List<IActable> actors)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var data = new byte[fs.Length];
                    for (var i = 0; i < data.Length; i++)
                        data[i] = (byte) fs.ReadByte();

                    return ApplyLoadingData(actors, data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        /***
         * BitConverter.ToSingle uses Little Endian
         * eg: 5.0f: Big Endian: 40 A0 00 00
         *           Little End; 00 00 A0 40
         */

        private static bool ApplyLoadingData(ICollection<IActable> actors, byte[] data)
        {
            var pos = 0;

            while (pos < data.LongLength)
            {
                int id = data[pos++];
                int x;
                int y;

                //ID: 1 Byte
                switch (id)
                {
                    //HEX: AA      Test EnemyA
                    //2 Bytes: X-Pos; 2 Bytes: Y-Pos
                    case 170:
                        
                        x = Convert.ToInt32(BitConverter.ToString(data.Skip(pos).Take(2).ToArray()).Replace("-", ""),16);
                        pos += 2;
                        y = Convert.ToInt32(BitConverter.ToString(data.Skip(pos).Take(2).ToArray()).Replace("-", ""), 16);
                        pos += 2;
                        actors.Add(EnemyFactory.GetEnemy("A", x, y));
                        break;

                    //HEX: 01      Textbox with starttime
                    //2 Bytes: x-Pos, 2 Bytes: Y-Pos; n Bytes: Message (end by '00'); n Bytes: Font (end by '00'); n Bytes: Portrait (end by '00'); 4 Bytes: starttime as float in sec 
                    case 1:
                        x = Convert.ToInt32(BitConverter.ToString(data.Skip(pos).Take(2).ToArray()).Replace("-", ""), 16);
                        pos += 2;
                        y = Convert.ToInt32(BitConverter.ToString(data.Skip(pos).Take(2).ToArray()).Replace("-", ""), 16);
                        pos += 2;
                        var tbMt = Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray());
                        pos += tbMt.Length + 1;
                        var tbFpT = Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray());
                        pos += tbFpT.Length + 1;
                        var tbPpT = Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray());
                        pos += tbPpT.Length + 1;
                        var tbST = BitConverter.ToSingle(data, pos);
                        pos += 4;
                        actors.Add(new TextBox(new SFML.System.Vector2f(x, y), tbMt, tbFpT, tbPpT, tbST));
                        break;

                    //HEX: 02      TextBox without starttime
                    //2 Bytes: X-Pos; 2 Bytes: Y-Pos; n Bytes: Message (end by '00'); n Bytes: Font (end by '00'); n Bytes: Portrait (end by '00')
                    case 2:
                        x = Convert.ToInt32(BitConverter.ToString(data.Skip(pos).Take(2).ToArray()).Replace("-", ""), 16);
                        pos += 2;
                        y = Convert.ToInt32(BitConverter.ToString(data.Skip(pos).Take(2).ToArray()).Replace("-", ""), 16);
                        pos += 2;
                        var textboxmessage = Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray());
                        pos += textboxmessage.Length + 1;
                        var tbFontPath = Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray());
                        pos += tbFontPath.Length + 1;
                        var tbPortraitPath = Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray());
                        pos += tbPortraitPath.Length + 1;
                        actors.Add(new TextBox(new SFML.System.Vector2f(x, y), textboxmessage, tbFontPath, tbPortraitPath));
                        break;


                    default:

                        break;
                }
            }

            return true;
        }

        public static bool SaveToFile(string filePath, List<IActable> actors)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    var data = GetData(actors.OfType<ISaveable>());
                    fs.Write(data, 0, data.Length);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
            return true;
        }

        private static byte[] GetData(IEnumerable<ISaveable> objects)
        {
            var data = new List<byte>();
            foreach (var saveable in objects)
            {
                var temp = saveable.GetData();
                data.Add(temp.Item1);
                data.AddRange(temp.Item2);
            }
            return data.ToArray();
        }
    }
}
