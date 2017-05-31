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
            Byte[] data;
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    data = new Byte[fs.Length];
                    for (var i = 0; i < data.Length; i++)
                        data[i] = (byte) fs.ReadByte();

                    //String[] ausw = BitConverter.ToString(data).Split('-');


                    return ApplyData(actors, data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }

            actors.Add(new Player());
            return true;
        }

        private static bool ApplyData(List<IActable> actors, Byte[] data)
        {
            int pos = 0;
            int id;

            while (pos < data.LongLength)
            {
                id = data[pos++];
                int x;
                int y;
                switch (id)
                {
                    ///HEX: AA      Test EnemyA
                    ///2 Bytes: X-Pos; 2 Bytes: Y-Pos
                    case 170:
                        
                        x = Convert.ToInt32(BitConverter.ToString(data.Skip(pos).Take(2).ToArray()).Replace("-", ""),16);
                        pos += 2;
                        y = Convert.ToInt32(BitConverter.ToString(data.Skip(pos).Take(2).ToArray()).Replace("-", ""), 16);
                        pos += 2;
                        actors.Add(EnemyFactory.GetEnemy("A", x, y));
                        break;

                    ///HEX: 02      TextBox
                    ///2 Bytes: X-Pos; 2 Bytes: Y-Pos; n Bytes: Message (end by '00'); n Bytes: Font (end by '00'); n Bytes: Portrait (end by '00')
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
                }
            }

            return true;
        }
    }
}
