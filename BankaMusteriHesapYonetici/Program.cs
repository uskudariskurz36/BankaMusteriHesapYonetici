namespace BankaMusteriHesapYonetici
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string secim = "";
            string[] musteriler = new string[2];
            string[] history = new string[0];

            musteriler[0] = $"Murat|Başeren|12345678912|1000|1234-567892";
            musteriler[1] = $"Belinay|Başeren|12345678914|2000|1234-555555";

            do
            {
                secim = MenuYaz();

                switch (secim)
                {
                    case "1":
                        MusteriHesapNoEkleme(ref musteriler, ref history);

                        Console.WriteLine("Müşteri eklendi.");
                        Console.ReadKey();

                        break;

                    case "2":
                        HavaleIslemi(musteriler, history);

                        Console.WriteLine("Havale yapıldı.");
                        Console.ReadKey();

                        break;

                    case "3":

                        GecmisListeleme(history);

                        Console.WriteLine("Geçmiş listelendi.");
                        Console.ReadKey();

                        break;

                    default:
                        break;
                }

            } while (secim != "0");
        }

        private static void GecmisListeleme(string[] history)
        {
            Console.WriteLine("Geçmiş Bilgileri");
            Console.WriteLine("=================");
            Console.WriteLine();

            foreach (string item in history)
            {
                Console.WriteLine(item);
            }
        }

        private static void HavaleIslemi(string[] musteriler, string[] history)
        {
            MusteriBilgileriYaz(musteriler);

            Console.Write("Gönderen Hesap No : ");
            string gonderen = Console.ReadLine();

            Console.Write("Alıcı Hesap No : ");
            string alici = Console.ReadLine();

            Console.Write("Tutar : ");
            decimal tutar = decimal.Parse(Console.ReadLine());

            string gonderenBilgi = "";
            string aliciBilgi = "";

            foreach (string musteri in musteriler)
            {
                string[] gonderenInfo = musteri.Split('|');

                if (musteri.Contains(gonderen))
                {
                    decimal bakiye = decimal.Parse(gonderenInfo[3]);
                    bakiye -= tutar;

                    gonderenInfo[3] = bakiye.ToString();

                    int index = Array.IndexOf(musteriler, musteri);
                    musteriler[index] = string.Join('|', gonderenInfo);

                    gonderenBilgi = musteriler[index];
                }

                string[] alicaInfo = musteri.Split('|');

                if (musteri.Contains(alici))
                {
                    decimal bakiye = decimal.Parse(alicaInfo[3]);
                    bakiye += tutar;

                    alicaInfo[3] = bakiye.ToString();

                    int index = Array.IndexOf(musteriler, musteri);
                    musteriler[index] = string.Join('|', alicaInfo);

                    aliciBilgi = musteriler[index];
                }
            }

            Array.Resize(ref history, history.Length + 1);
            history[history.Length - 1] = $"{DateTime.Now}-Havale işlemi-{tutar}-{gonderenBilgi}-{aliciBilgi}";
        }

        private static void MusteriBilgileriYaz(string[] musteriler)
        {
            Console.WriteLine("Müşteri Bilgileri");
            Console.WriteLine("=================");
            Console.WriteLine();

            foreach (string musteri in musteriler)
            {
                string[] strings = musteri.Split("|");

                Console.WriteLine($"[{strings[4]}]-{strings[0]} {strings[1]}");
            }

            Console.WriteLine();
        }

        private static void MusteriHesapNoEkleme(ref string[] musteriler, ref string[] history)
        {
            Console.Write("Müşteri adı : ");
            string ad = Console.ReadLine();

            Console.Write("Müşteri soyad :");
            string soyad = Console.ReadLine();

            Console.Write("TC No : ");
            string tc = Console.ReadLine();

            while (true)
            {
                char sonRakam = tc[tc.Length - 1];
                int sonRakamInt = int.Parse(sonRakam.ToString());
                //bool sonRakamCiftMi = sonRakamInt % 2 == 0;

                if (tc.Length == 11 && sonRakamInt % 2 == 0)
                {
                    //foreach (string musteri in musteriler)
                    //{
                    //    string[] strings = musteri.Split('|');
                    //    string musteriTc = strings[2];
                    //    string musteriAd = strings[0];
                    //    string musteriSoyad = strings[1];

                    //    if (musteriTc == tc)
                    //    {
                    //        if (ad == musteriAd && soyad == musteriSoyad)
                    //        {
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            HataVer("Hatalı TC No ya ait kimlik bilgisi uyuşmuyor!!");
                    //        }
                    //    }
                    //}

                    break;
                }
                else
                {
                    HataVer("Hatalı TC No!!");

                    Console.Write("TC No : ");
                    tc = Console.ReadLine();
                }


            }

            Console.Write("Hesap No : ");
            string hesapNo = Console.ReadLine();

            while (true)
            {
                string[] hesapNoParcalar = hesapNo.Split('-');

                if (hesapNoParcalar[0].Length == 4 && hesapNoParcalar[1].Length == 6)
                {
                    bool valid = true;

                    foreach (string musteri in musteriler)
                    {
                        string[] musteriInfo = musteri.Split('|');
                        if (musteriInfo[4] == hesapNo)
                        {
                            HataVer("Kayıtlı Müşteri Hesap No!!");

                            Console.Write("Hesap No : ");
                            hesapNo = Console.ReadLine();
                            valid = false;
                            break;
                        }
                    }

                    if (valid)
                    {
                        break;
                    }
                }
                else
                {
                    HataVer("Hatalı Hesap No!!");

                    Console.Write("Hesap No : ");
                    hesapNo = Console.ReadLine();
                }
            }

            Console.Write("Bakiye : ");
            string bakiye = Console.ReadLine();

            string item = $"{ad}|{soyad}|{tc}|{bakiye}|{hesapNo}";
            Array.Resize(ref musteriler, musteriler.Length + 1);
            musteriler[musteriler.Length - 1] = item;

            Array.Resize(ref history, history.Length + 1);
            history[history.Length - 1] = $"{DateTime.Now}-Yeni müşteri kaydı-{item}";
        }

        private static void HataVer(string hataMesaji)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(hataMesaji);
            Console.ResetColor();
        }

        private static string MenuYaz()
        {
            string secim;
            Console.Clear();

            Console.WriteLine("Menü");
            Console.WriteLine("====");
            Console.WriteLine();

            Console.WriteLine("[1] – Yeni müşteri hesabı tanımlama");
            Console.WriteLine("[2] – Havale işlemi yapma");
            Console.WriteLine("[3] – İşlem geçmişi inceleme");
            Console.WriteLine("[0] – Çıkış");
            Console.WriteLine();

            Console.Write("Seçiminiz : ");
            secim = Console.ReadLine();
            return secim;
        }
    }
}