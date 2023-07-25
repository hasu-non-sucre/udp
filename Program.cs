using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace UdpSenderExample
{
    class Program
    {
        static void Main()
        {
            string remoteIPAddress = "192.168.11.7"; // 送信先のIPアドレス
            int remotePort = 8888; // 送信先のポート番号

            string filePath = "../../SerialReadCSVOut/test.csv"; // csvファイルのパスを指定

            UdpClient udpClient = new UdpClient();
            try
            {
                Console.WriteLine("UDPクライアントを開始しました。");

                

                while (true)
                {
                    // Console.WriteLine("送信する文字列を入力してください（exitで終了）:");
                    // string input = Console.ReadLine();

                    // if (input.ToLower() == "exit")
                    //     break;

                    // byte[] data = System.Text.Encoding.UTF8.GetBytes(input);
                    // udpClient.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(remoteIPAddress), remotePort));


                    // try
                    // {
                    //     // テキストファイルから内容を読み取る
                    //     string content = File.ReadAllText(filePath);
                    //     Console.Write("テキストファイルの内容:");
                    //     Console.WriteLine(content);
                    //     byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
                    //     udpClient.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(remoteIPAddress), remotePort));
                    // }
                    // catch (FileNotFoundException)
                    // {
                    //     Console.WriteLine($"ファイル '{filePath}' が見つかりませんでした。");
                    // }
                    // catch (IOException ex)
                    // {
                    //     Console.WriteLine($"入出力エラーが発生しました: {ex.Message}");
                    // }

                    // // 1秒待機してから再試行する
                    // System.Threading.Thread.Sleep(1000);

                    try
                    {
                        using (StreamReader sr = new StreamReader(filePath))
                        {
                            while(!sr.EndOfStream)
                            {
                                string line = sr.ReadLine(); // 行を読み取る
                                string[] speeds = line.Split(',');

                                // string msg = speeds[0] + ", 距離: " + speeds[1];

                                foreach(string item in speeds)
                                {
                                    Console.Write(item + "\t");

                                    byte[] data = System.Text.Encoding.UTF8.GetBytes(line);
                                    udpClient.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(remoteIPAddress), remotePort));
                                    System.Threading.Thread.Sleep(500);
                                }
                                // byte[] data = System.Text.Encoding.UTF8.GetBytes(msg);
                                // udpClient.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(remoteIPAddress), remotePort));
                                // System.Threading.Thread.Sleep(500);
                                Console.WriteLine();
                            }
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine($"ファイル '{filePath}' が見つかりませんでした。");
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"入出力エラーが発生しました: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラーが発生しました: " + ex.ToString());
            }
            finally
            {
                udpClient.Close();
            }
        }
    }
}
