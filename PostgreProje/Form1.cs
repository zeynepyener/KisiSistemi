using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace PostgreProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Kişi bilgilerini çek
                    string query = "SELECT \"TCNo\", \"adi\", \"soyadi\", \"dogumTarihi\", \"anaAdi\", \"babaAdi\", \"medeniDurum\", \"iletisimKodu\" FROM \"Kisi\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'e verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"iletisimKodu\", \"telNo\", \"ePosta\", \"ilceKodu\" FROM \"Iletisim\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void bolgeleriGoster_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"bolgeKodu\", \"bolgeAdi\" FROM \"Bolge\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void Iller_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"ilKodu\", \"ilAdi\", \"bolgeKodu\" FROM \"Il\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void yeniKisiEkle_Click(object sender, EventArgs e)
        {
            // Formdaki TextBox'lardan veri al
            string tcNoStr = textBox1.Text;        // T.C. Kimlik Numarası
            string adi = textBox2.Text;            // Adı
            string soyadi = textBox3.Text;         // Soyadı
            string dogumTarihi = textBox4.Text;    // Doğum Tarihi
            string anaAdi = textBox5.Text;         // Ana Adı
            string babaAdi = textBox6.Text;        // Baba Adı
            string medeniDurumStr = textBox7.Text; // Medeni Durum (SMALLINT)
            string iletisimKoduStr = textBox8.Text; // İletişim Kodu (SERIAL, ama manuel değer alıyoruz)

            // T.C. Kimlik Numarası'nı int türüne dönüştür
            int tcNo;
            if (!int.TryParse(tcNoStr, out tcNo))
            {
                MessageBox.Show("Geçerli bir T.C. Kimlik Numarası girin.", "Hata");
                return;
            }

            // Medeni Durum'u short türüne dönüştür
            short medeniDurum;
            if (!short.TryParse(medeniDurumStr, out medeniDurum))
            {
                MessageBox.Show("Geçerli bir medeni durum girin (örneğin: 1, 2, 3).", "Hata");
                return;
            }

            // Doğum tarihini doğru formatta DateTime türüne dönüştür
            DateTime dogumTarihiDate;
            if (!DateTime.TryParse(dogumTarihi, out dogumTarihiDate))
            {
                MessageBox.Show("Geçerli bir doğum tarihi girin (örneğin: YYYY-AA-GG).", "Hata");
                return;
            }

            // İletişim Kodu'nu int türüne dönüştür
            int iletisimKodu;
            if (!int.TryParse(iletisimKoduStr, out iletisimKodu))
            {
                MessageBox.Show("Geçerli bir İletişim Kodu girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Veritabanına veri ekleme komutu
                    string query = "INSERT INTO \"Kisi\" (\"TCNo\", \"adi\", \"soyadi\", \"dogumTarihi\", \"anaAdi\", \"babaAdi\", \"medeniDurum\", \"iletisimKodu\") " +
                                   "VALUES (@tcNo, @adi, @soyadi, @dogumTarihi, @anaAdi, @babaAdi, @medeniDurum, @iletisimKodu)";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        // Parametreleri ekle
                        command.Parameters.AddWithValue("@tcNo", tcNo);
                        command.Parameters.AddWithValue("@adi", adi);
                        command.Parameters.AddWithValue("@soyadi", soyadi);
                        command.Parameters.AddWithValue("@dogumTarihi", dogumTarihiDate);
                        command.Parameters.AddWithValue("@anaAdi", anaAdi);
                        command.Parameters.AddWithValue("@babaAdi", babaAdi);
                        command.Parameters.AddWithValue("@medeniDurum", medeniDurum);
                        command.Parameters.AddWithValue("@iletisimKodu", iletisimKodu);

                        // Komutu çalıştır
                        command.ExecuteNonQuery();
                    }
                }

                // Girilen verileri text kısımlarından kaldırıyoruz
                textBox1.Text=null;        
                textBox2.Text= null;           
                textBox3.Text= null;         
                textBox4.Text= null;    
                textBox5.Text = null;         
                textBox6.Text = null;        
                textBox7.Text = null; 
                textBox8.Text = null;
                MessageBox.Show("Kişi başarıyla eklendi.", "Başarılı");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void bakanliklariGoster_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"bakanlikNo\", \"bakanlikAdi\", \"yonetenKisi\" FROM \"Bakanlik\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void sirketleriGoster_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"sirketNo\", \"sirketAdi\", \"sektor\", \"sirketTuru\", \"iletisimKodu\" FROM \"Sirket\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void devletSirketleriGoster_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"sirketNo\", \"kurulusTarihi\", \"bakanlikNo\" FROM \"DevletSirketi\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }

        }

        private void ozelSirketleriGoster_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"sirketNo\", \"sahipTCNo\", \"calisanSayisi\" FROM \"OzelSirket\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void medeniDurumGoster_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"medeniDurumNo\", \"medeniDurumu\" FROM \"MedeniDurum\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void calisanlarGoster_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"calisanNo\", \"sirketNo\" , \"TCNo\" ,\"pozisyonNo\", \"hisseMiktari\"  , \"maas\"  FROM \"Calisan\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void pozisyonlariGoster_Click(object sender, EventArgs e)
        {

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"pozisyonNo\", \"pozisyon\"  FROM \"Pozisyon\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }

        }

        private void bankaHesaplariGoster_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"hesapNumarasi\", \"bakiye\", \"olusturulmaTarihi\", \"hesabiKullananTCNo\", \"hesapTuru\"  FROM \"BankaHesabi\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void sirketHesaplariGoster_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"hesapNumarasi\", \"hesapAcilisAmaci\", \"sirketNumarasi\"  FROM \"SirketHesabi\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void kisiselHesaplariGoster_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"hesapNumarasi\", \"anneKizlikSoyadi\", \"TCNo\"  FROM \"KisiselHesap\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void devletSirketiEkle_Click(object sender, EventArgs e)
        {
            // Formdaki TextBox'lardan veri al
            string sirketNumarasiStr = sirketNo.Text;        // şirket numarası
            string sirketAd = sirketAdi.Text;            // şirket adı
            string sektoru = sektor.Text;         // sektor
            string sirketTurStr = sirketTuru.Text;    // şirket türü
            string iletisimKodStr = iletisimKoduu.Text;         // iletişim kodu
            string kurulusTarihStr = kurulusTarihiii.Text;        // kuruluş tarihi
            string bakanlikNoStr = bakanlikNoo.Text; // bakanlık numarası



            // bakanlık numarası doğru formatta INT türüne dönüştür
            int bakanlikNo;
            if (!int.TryParse(bakanlikNoStr, out bakanlikNo))
            {
                MessageBox.Show("Geçerli bir bakanlik numarası girin.", "Hata");
                return;
            }



            // Şirket numarasını doğru formatta INT türüne dönüştür
            int sirketNumarasi;
            if (!int.TryParse(sirketNumarasiStr, out sirketNumarasi))
            {
                MessageBox.Show("Geçerli bir şirket numarası girin.", "Hata");
                return;
            }



            // iletişim kodu doğru formatta INT türüne dönüştür
            int iletisimKod;
            if (!int.TryParse(iletisimKodStr, out iletisimKod))
            {
                MessageBox.Show("Geçerli bir iletişim kodu girin.", "Hata");
                return;
            }




            //sirketTur'ü char türüne dönüştür
            char sirketTur;
            if (!char.TryParse(sirketTurStr, out sirketTur))
            {
                MessageBox.Show("Geçerli bir Şirket Numarası girin.", "Hata");
                return;
            }



            // Doğum tarihini doğru formatta DateTime türüne dönüştür
            DateTime kurulusTarih;
            if (!DateTime.TryParse(kurulusTarihStr, out kurulusTarih))
            {
                MessageBox.Show("Geçerli bir kuruluş tarihi girin (örneğin: 01-01-1990).", "Hata");
                return;
            }



            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";


            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Veritabanına veri ekleme komutu
                        string query = "INSERT INTO \"Sirket\" (\"sirketNo\", \"sirketAdi\", \"sektor\", \"sirketTuru\", \"iletisimKodu\") " +
                                   "VALUES (@sirketNo, @sirketAdi, @sektor, @sirketTuru, @iletisimKodu)";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@sirketNo", sirketNumarasi);
                            command.Parameters.AddWithValue("@sirketAdi", sirketAd);
                            command.Parameters.AddWithValue("@sektor", sektoru);
                            command.Parameters.AddWithValue("@sirketTuru", sirketTur);
                            command.Parameters.AddWithValue("@iletisimKodu", iletisimKod);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }




                        string query1 = "INSERT INTO \"DevletSirketi\" (\"sirketNo\", \"kurulusTarihi\", \"bakanlikNo\") " +
                                       "VALUES (@sirketNo, @kurulusTarihi, @bakanlikNo)";

                        using (var command = new NpgsqlCommand(query1, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@sirketNo", sirketNumarasi);
                            command.Parameters.AddWithValue("@kurulusTarihi", kurulusTarih);
                            command.Parameters.AddWithValue("@bakanlikNo", bakanlikNo);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        //// Girilen verileri text kısımlarından kaldırıyoruz.
                        sirketNo.Text = null;        
                        sirketAdi.Text = null;       
                        sektor.Text = null;         
                        sirketTuru.Text = null;    
                        iletisimKoduu.Text = null;   
                        kurulusTarihiii.Text = null; 
                        bakanlikNoo.Text = null;
                        MessageBox.Show("Devlet Şirketi başarıyla eklendi.", "Başarılı");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                    }
                }
            }
        }

        private void ozelSirketEkle_Click(object sender, EventArgs e)
        {
            // Formdaki TextBox'lardan veri al
            string sirketNumarasiStr = sirketNo.Text;        // Şirket Numarası
            string sirketAd = sirketAdi.Text;            // şirket adı
            string sektoru = sektor.Text;         // sektör
            string sirketTurStr = sirketTuru.Text;    // şirket türü
            string iletisimKodStr = iletisimKoduu.Text;         // iletişim kodu
            string sahipTCNoStr = sahipTCNoo.Text;        // sahibinin TC numarası
            string calisanSayisiStr = calisanSayisii.Text; // çalışan sayısı




            // sahibinin TC numarası doğru formatta INT türüne dönüştür
            int sahipTCNo;
            if (!int.TryParse(sahipTCNoStr, out sahipTCNo))
            {
                MessageBox.Show("Geçerli bir TC numarası girin.", "Hata");
                return;
            }

            
            //Çalışasn sayısı doğru formatta INT türüne dönüştür
            int calisanSayisi;
            if (!int.TryParse(calisanSayisiStr, out calisanSayisi))
            {
                MessageBox.Show("Geçerli bir çalışan sayısı girin.", "Hata");
                return;
            }


            // Şirket numarasını doğru formatta INT türüne dönüştür
            int sirketNumarasi;
            if (!int.TryParse(sirketNumarasiStr, out sirketNumarasi))
            {
                MessageBox.Show("Geçerli bir şirket numarası girin.", "Hata");
                return;
            }



            // İletişim kodu doğru formatta INT türüne dönüştür
            int iletisimKod;
            if (!int.TryParse(iletisimKodStr, out iletisimKod))
            {
                MessageBox.Show("Geçerli bir iletişim kodu girin.", "Hata");
                return;
            }




            //sirketTur'ü char türüne dönüştür
            char sirketTur;
            if (!char.TryParse(sirketTurStr, out sirketTur))
            {
                MessageBox.Show("Geçerli bir Şirket Türü girin.", "Hata");
                return;
            }





            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";


            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Veritabanına veri ekleme komutu
                        string query = "INSERT INTO \"Sirket\" (\"sirketNo\", \"sirketAdi\", \"sektor\", \"sirketTuru\", \"iletisimKodu\") " +
                               "VALUES (@sirketNo, @sirketAdi, @sektor, @sirketTuru, @iletisimKodu)";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@sirketNo", sirketNumarasi);
                            command.Parameters.AddWithValue("@sirketAdi", sirketAd);
                            command.Parameters.AddWithValue("@sektor", sektoru);
                            command.Parameters.AddWithValue("@sirketTuru", sirketTur);
                            command.Parameters.AddWithValue("@iletisimKodu", iletisimKod);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }




                        string query1 = "INSERT INTO \"OzelSirket\" (\"sirketNo\", \"sahipTCNo\", \"calisanSayisi\") " +
                                       "VALUES (@sirketNo, @sahipTCNo, @calisanSayisi)";

                        using (var command = new NpgsqlCommand(query1, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@sirketNo", sirketNumarasi);
                            command.Parameters.AddWithValue("@sahipTCNo", sahipTCNo);
                            command.Parameters.AddWithValue("@calisanSayisi", calisanSayisi);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        //// Girilen verileri text kısımlarından kaldırıyoruz
                        sirketNo.Text = null;        
                        sirketAdi.Text = null;       
                        sektor.Text = null;         
                        sirketTuru.Text = null;    
                        iletisimKoduu.Text = null;  
                        sahipTCNoo.Text = null;     
                        calisanSayisii.Text = null;
                        MessageBox.Show("Özel Şirket başarıyla eklendi.", "Başarılı");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                    }
                }
            }
        }

        private void calisanEkle_Click(object sender, EventArgs e)
        {
            // Formdaki TextBox'lardan veri al
            string calisanNoo = calisanNo1.Text;        // Çalışan Numarası
            string sirketNoo = sirketNo1.Text;            // Şirket Numarası
            string TCNoo = calisanTCNo1.Text;         // TC numarası
            string pozisyonNoo = pozisyonNo1.Text;    // Pozisyon Numarası
            string maasStr = maasNo1.Text;            // Maaş
            string hisseMiktariStr = hisseMiktarii.Text;  // Hisse Miktarı


            int hisseMiktari;
            if (!int.TryParse(hisseMiktariStr, out hisseMiktari))
            {
                MessageBox.Show("Geçerli bir hisse miktarı numarası girin.", "Hata");
                return;
            }


            // Çalışan numarasını int türüne dönüştür
            int calisanNo;
            if (!int.TryParse(calisanNoo, out calisanNo))
            {
                MessageBox.Show("Geçerli bir Çalışan Numarası girin.", "Hata");
                return;
            }

            int maas;
            if (!int.TryParse(maasStr, out maas))
            {
                MessageBox.Show("Geçerli bir maaş girin.", "Hata");
                return;
            }

            // Şirket numarasını short türüne dönüştür
            short sirketNo;
            if (!short.TryParse(sirketNoo, out sirketNo))
            {
                MessageBox.Show("Geçerli bir şirket numarası girin.", "Hata");
                return;
            }

            int TCNo;
            if (!int.TryParse(TCNoo, out TCNo))
            {
                MessageBox.Show("Geçerli bir TC Numarası girin.", "Hata");
                return;
            }

            short pozisyonNo;
            if (!short.TryParse(pozisyonNoo, out pozisyonNo))
            {
                MessageBox.Show("Geçerli bir Pozisyon Numarası girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Veritabanına veri ekleme komutu
                    string query = "INSERT INTO \"Calisan\" (\"calisanNo\", \"sirketNo\", \"TCNo\", \"hisseMiktari\", \"pozisyonNo\", \"maas\" ) " +
                                   "VALUES (@calisanNo, @sirketNo, @TCNo, @hisseMiktari, @pozisyonNo, @maas)";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        // Parametreleri ekle
                        command.Parameters.AddWithValue("@calisanNo", calisanNo);
                        command.Parameters.AddWithValue("@sirketNo", sirketNo);
                        command.Parameters.AddWithValue("@TCNo", TCNo);
                        command.Parameters.AddWithValue("@hisseMiktari", hisseMiktari);
                        command.Parameters.AddWithValue("@pozisyonNo", pozisyonNo);
                        command.Parameters.AddWithValue("@maas", maas);

                        // Komutu çalıştır
                        command.ExecuteNonQuery();
                    }
                }
                // Girilen verileri text kısımlarından kaldırıyoruz
                calisanNo1.Text = null;        
                sirketNo1.Text = null;            
                calisanTCNo1.Text = null;         
                pozisyonNo1.Text = null;    
                maasNo1.Text = null;
                hisseMiktarii.Text = null;
                MessageBox.Show("Çalışan başarıyla eklendi.", "Başarılı");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void sirketHesabiAc_Click_1(object sender, EventArgs e)
        {
            // Formdaki TextBox'lardan veri al
            string hesapNumarasiStr = hesapNumarasiNo2.Text;        // Hesap numarası
            string bakiyeStr = bakiye2.Text;            // Bakiye
            string olusturulmaTarihiStr = olusturulmaTarihi2.Text;         // Oluşturulma Tarihi
            string hesabiKullananTCNoStr = hesabiKullananTCNo2.Text;    // Hesabı kullananın TC numarası
            string hesapTuruStr = hesapTuru2.Text;         // Hesap türü
            string hesapAcilisAmaci = hesapAcilisAmac2.Text; // Hesap açılış amacı
            string sirketNumarasiStr = sirketNo2.Text; // Şirket Numarası




            // Hesap numarasını doğru formatta INT türüne dönüştür
            int hesapNumarasi;
            if (!int.TryParse(hesapNumarasiStr, out hesapNumarasi))
            {
                MessageBox.Show("Geçerli bir Hesap Numarası  girin.", "Hata");
                return;
            }

            int hesabiKullananTCNo;
            if (!int.TryParse(hesabiKullananTCNoStr, out hesabiKullananTCNo))
            {
                MessageBox.Show("Geçerli bir TC Numarası girin.", "Hata");
                return;
            }

            int bakiye;
            if (!int.TryParse(bakiyeStr, out bakiye))
            {
                MessageBox.Show("Geçerli bir bakiye miktarı girin.", "Hata");
                return;
            }

            int sirketNumarasi;
            if (!int.TryParse(sirketNumarasiStr, out sirketNumarasi))
            {
                MessageBox.Show("Geçerli bir şirket numarası girin.", "Hata");
                return;
            }

            DateTime olusturulmaTarihi;
            if (!DateTime.TryParse(olusturulmaTarihiStr, out olusturulmaTarihi))
            {
                MessageBox.Show("Geçerli bir tarih girin.", "Hata");
                return;
            }


            // Şirket numarasını doğru formatta INT türüne dönüştür
            char hesapTuru;
            if (!char.TryParse(hesapTuruStr, out hesapTuru))
            {
                MessageBox.Show("Geçerli bir şirket numarası girin.", "Hata");
                return;
            }



            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Veritabanına veri ekleme komutu
                        string query = "INSERT INTO \"BankaHesabi\" (\"hesapNumarasi\", \"bakiye\", \"olusturulmaTarihi\", \"hesabiKullananTCNo\", \"hesapTuru\") " +
                                   "VALUES (@hesapNumarasi, @bakiye, @olusturulmaTarihi, @hesabiKullananTCNo, @hesapTuru)";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@hesapNumarasi", hesapNumarasi);
                            command.Parameters.AddWithValue("@bakiye", bakiye);
                            command.Parameters.AddWithValue("@olusturulmaTarihi", olusturulmaTarihi);
                            command.Parameters.AddWithValue("@hesabiKullananTCNo", hesabiKullananTCNo);
                            command.Parameters.AddWithValue("@hesapTuru", hesapTuru);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }




                        string query1 = "INSERT INTO \"SirketHesabi\" (\"hesapNumarasi\", \"hesapAcilisAmaci\", \"sirketNumarasi\") " +
                                       "VALUES (@hesapNumarasi, @hesapAcilisAmaci, @sirketNumarasi)";

                        using (var command = new NpgsqlCommand(query1, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@hesapNumarasi", hesapNumarasi);
                            command.Parameters.AddWithValue("@hesapAcilisAmaci", hesapAcilisAmaci);
                            command.Parameters.AddWithValue("@sirketNumarasi", sirketNumarasi);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        hesapNumarasiNo2.Text = null;        
                        bakiye2.Text = null;            
                        olusturulmaTarihi2.Text = null;         
                        hesabiKullananTCNo2.Text = null;    
                        hesapTuru2.Text = null;         
                        hesapAcilisAmac2.Text = null; 
                        sirketNo2.Text = null;
                        MessageBox.Show("Şirket banka hesabı başarıyla açıldı.", "Başarılı");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                    }
                }
            }
        }

        private void kisiselHesapAc_Click(object sender, EventArgs e)
        {
            // Formdaki TextBox'lardan veri al
            string hesapNumarasiStr = hesapNumarasiNo2.Text;        // Hesap numarası
            string bakiyeStr = bakiye2.Text;            // Bakiye
            string olusturulmaTarihiStr = olusturulmaTarihi2.Text;         // Oluşturulma Tarihi
            string hesabiKullananTCNoStr = hesabiKullananTCNo2.Text;    // Hesabı kullanan TC numarası
            string hesapTuruStr = hesapTuru2.Text;         // Hesap türü
            string anneKizlikSoyadi = anneKizlikSoyad.Text; // Anne kızlık soyadı
            string TCNoStr = TCNo2.Text; // TC numarası




            // Hesap numarasını doğru formatta INT türüne dönüştür
            int hesapNumarasi;
            if (!int.TryParse(hesapNumarasiStr, out hesapNumarasi))
            {
                MessageBox.Show("Geçerli bir Hesap Numarası  girin.", "Hata");
                return;
            }

            int hesabiKullananTCNo;
            if (!int.TryParse(hesabiKullananTCNoStr, out hesabiKullananTCNo))
            {
                MessageBox.Show("Geçerli bir TC No girin.", "Hata");
                return;
            }

            int bakiye;
            if (!int.TryParse(bakiyeStr, out bakiye))
            {
                MessageBox.Show("Geçerli bir bakiye miktarı  girin.", "Hata");
                return;
            }

            int TCNo;
            if (!int.TryParse(TCNoStr, out TCNo))
            {
                MessageBox.Show("Geçerli bir TC numarası girin.", "Hata");
                return;
            }

            DateTime olusturulmaTarihi;
            if (!DateTime.TryParse(olusturulmaTarihiStr, out olusturulmaTarihi))
            {
                MessageBox.Show("Geçerli bir tarih girin.", "Hata");
                return;
            }


            // Hesap türünü doğru formatta INT türüne dönüştür
            char hesapTuru;
            if (!char.TryParse(hesapTuruStr, out hesapTuru))
            {
                MessageBox.Show("Geçerli bir hesap türü girin.", "Hata");
                return;
            }



            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";



            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {


                        // Veritabanına veri ekleme komutu
                        string query = "INSERT INTO \"BankaHesabi\" (\"hesapNumarasi\", \"bakiye\", \"olusturulmaTarihi\", \"hesabiKullananTCNo\", \"hesapTuru\") " +
                                   "VALUES (@hesapNumarasi, @bakiye, @olusturulmaTarihi, @hesabiKullananTCNo, @hesapTuru)";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@hesapNumarasi", hesapNumarasi);
                            command.Parameters.AddWithValue("@bakiye", bakiye);
                            command.Parameters.AddWithValue("@olusturulmaTarihi", olusturulmaTarihi);
                            command.Parameters.AddWithValue("@hesabiKullananTCNo", hesabiKullananTCNo);
                            command.Parameters.AddWithValue("@hesapTuru", hesapTuru);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }




                        string query1 = "INSERT INTO \"KisiselHesap\" (\"hesapNumarasi\", \"anneKizlikSoyadi\", \"TCNo\") " +
                                       "VALUES (@hesapNumarasi, @anneKizlikSoyadi, @TCNo)";

                        using (var command = new NpgsqlCommand(query1, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@hesapNumarasi", hesapNumarasi);
                            command.Parameters.AddWithValue("@anneKizlikSoyadi", anneKizlikSoyadi);
                            command.Parameters.AddWithValue("@TCNo", TCNo);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        // Girilen verileri text kısımlarından kaldırıyoruz
                        hesapNumarasiNo2.Text = null;        
                        bakiye2.Text = null;            
                        olusturulmaTarihi2.Text = null;         
                        hesabiKullananTCNo2.Text = null;    
                        hesapTuru2.Text = null;         
                        anneKizlikSoyad.Text = null; 
                        TCNo2.Text = null; 
                        MessageBox.Show("Kişisel Banka Hesabı başarıyla eklendi.", "Başarılı");


                    }

                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                    }
                }
            }
        }

        private void kisiSil_Click(object sender, EventArgs e)
        {

            // Silinecek kişi için TCNO alınıyor
            string tcnoStr = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(tcnoStr))
            {
                MessageBox.Show("Lütfen silmek istediğiniz kişinin TC numarasını girin.", "Hata");
                return;
            }

            int tcno;
            if (!int.TryParse(tcnoStr, out tcno))
            {
                MessageBox.Show("Geçerli bir TC numarası girin.", "Hata");
                return;
            }



            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Silme sorgusunu oluştur
                    string deleteQuery = "DELETE FROM \"Kisi\" WHERE \"TCNo\" = @tcno";

                    using (var command = new NpgsqlCommand(deleteQuery, connection))
                    {
                        // Parametre ekleniyor
                        command.Parameters.AddWithValue("@tcno", tcno);

                        // Komutu çalıştır
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            textBox1.Text = null;
                            textBox2.Text = null;
                            textBox3.Text = null;
                            textBox4.Text = null;    
                            textBox5.Text = null;
                            textBox6.Text = null;
                            textBox7.Text = null;
                            textBox8.Text = null;

                            MessageBox.Show("Kişi başarıyla silindi.", "Başarılı");


                        }
                        else
                        {
                            MessageBox.Show("Silinecek kişi bulunamadı. TCNO'yu kontrol edin.", "Uyarı");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void devletSirketiSil_Click(object sender, EventArgs e)
        {
            
            string sirketNoo = sirketNo.Text.Trim();

            if (string.IsNullOrEmpty(sirketNoo))
            {
                MessageBox.Show("Lütfen silmek istediğiniz devlet şirketinin numarasını girin.", "Hata");
                return;
            }

            int sirketNo1;
            if (!int.TryParse(sirketNoo, out sirketNo1))
            {
                MessageBox.Show("Geçerli bir devlet şirket numarası girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Silme sorgusunu oluştur
                            string deleteQuery1 = "DELETE FROM \"DevletSirketi\" WHERE \"sirketNo\" = @sirketNo";
                            string deleteQuery2 = "DELETE FROM \"Sirket\" WHERE \"sirketNo\" = @sirketNo";

                            // İlk silme işlemi için komut
                            using (var command1 = new NpgsqlCommand(deleteQuery1, connection))
                            {
                                command1.Parameters.AddWithValue("@sirketNo", sirketNo1);

                                int rowsAffected1 = command1.ExecuteNonQuery();
                                if (rowsAffected1 == 0)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Silinecek devlet şirketi bulunamadı. Numarayı kontrol edin.", "Uyarı");
                                    return;
                                }
                            }

                            // İkinci silme işlemi için komut
                            using (var command2 = new NpgsqlCommand(deleteQuery2, connection))
                            {
                                command2.Parameters.AddWithValue("@sirketNo", sirketNo1);

                                int rowsAffected2 = command2.ExecuteNonQuery();
                                if (rowsAffected2 == 0)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Silinecek devlet şirketi bulunamadı. Numarayı kontrol edin.", "Uyarı");
                                    return;
                                }
                            }

                            // Başarılı silme işlemi sonrasında işlem tamamlanıyor
                            transaction.Commit();
                            iletisimKoduu.Text = null;
                            sirketNo.Text = null;
                            sirketAdi.Text = null;
                            sektor.Text = null;
                            sirketTuru.Text = null;
                            kurulusTarihiii.Text = null;
                            bakanlikNoo.Text = null;
                            MessageBox.Show("Devlet Şirketi başarıyla silindi.", "Başarılı");
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda işlem geri alınıyor
                            transaction.Rollback();
                            MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Veritabanı bağlantısı sırasında bir hata oluştu: {ex.Message}", "Hata");
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void bankaHesabiniSil_Click(object sender, EventArgs e)
        {
            
            string hesapNoo = hesapNumarasiNo2.Text.Trim();

            if (string.IsNullOrEmpty(hesapNoo))
            {
                MessageBox.Show("Lütfen silmek istediğiniz banka hesap numarasını girin.", "Hata");
                return;
            }

            int hesapNo;
            if (!int.TryParse(hesapNoo, out hesapNo))
            {
                MessageBox.Show("Geçerli bir banka hesap numarasını girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Silme sorgusunu oluştur
                            string deleteQuery1 = "DELETE FROM \"BankaHesabi\" WHERE \"hesapNumarasi\" = @hesapNumarasi";

                            // İlk silme işlemi için komut
                            using (var command1 = new NpgsqlCommand(deleteQuery1, connection))
                            {
                                command1.Parameters.AddWithValue("@hesapNumarasi", hesapNo);

                                int rowsAffected1 = command1.ExecuteNonQuery();
                                if (rowsAffected1 == 0)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Silinecek banka hesabı bulunamadı. Hesap numarayı kontrol edin.", "Uyarı");
                                    return;
                                }
                            }



                            // Başarılı silme işlemi sonrasında işlem tamamlanıyor
                            transaction.Commit();
                            hesapNumarasiNo2.Text = null;
                            bakiye2.Text = null;
                            olusturulmaTarihi2.Text = null;
                            hesabiKullananTCNo2.Text = null;
                            hesapTuru2.Text = null;
                            hesapAcilisAmac2.Text = null;
                            anneKizlikSoyad.Text = null;
                            TCNo2.Text = null;
                            sirketNo2.Text = null;
                            MessageBox.Show("Banka hesabı başarıyla silindi.", "Başarılı");
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda işlem geri alınıyor
                            transaction.Rollback();
                            MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Veritabanı bağlantısı sırasında bir hata oluştu: {ex.Message}", "Hata");
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        private void calisaniSil_Click(object sender, EventArgs e)
        {
            
            string calisanTCNoo = calisanTCNo1.Text.Trim();

            if (string.IsNullOrEmpty(calisanTCNoo))
            {
                MessageBox.Show("Lütfen silmek istediğiniz çalışanın TC numarasını girin.", "Hata");
                return;
            }

            int calisanTCNo;
            if (!int.TryParse(calisanTCNoo, out calisanTCNo))
            {
                MessageBox.Show("Geçerli bir çalışan TC numarası girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Silme sorgusunu oluştur
                            string deleteQuery1 = "DELETE FROM \"Calisan\" WHERE \"TCNo\" = @tcNo";

                            // İlk silme işlemi için komut
                            using (var command1 = new NpgsqlCommand(deleteQuery1, connection))
                            {
                                command1.Parameters.AddWithValue("@tcNo", calisanTCNo);

                                int rowsAffected1 = command1.ExecuteNonQuery();
                                if (rowsAffected1 == 0)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Silinecek çalışan bulunamadı. TC Numarayı kontrol edin.", "Uyarı");
                                    return;
                                }
                            }

                            // Başarılı silme işlemi sonrasında işlem tamamlanıyor
                            transaction.Commit();
                            calisanNo1.Text = null;
                            sirketNo1.Text = null;
                            calisanTCNo1.Text = null;
                            pozisyonNo1.Text = null;
                            maasNo1.Text = null;
                            hisseMiktarii.Text = null;
                            MessageBox.Show("Çalışan başarıyla silindi.", "Başarılı");
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda işlem geri alınıyor
                            transaction.Rollback();
                            MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Veritabanı bağlantısı sırasında bir hata oluştu: {ex.Message}", "Hata");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ozelSirketiSil_Click(object sender, EventArgs e)
        {
           
            string sirketNoo = sirketNo.Text.Trim();

            if (string.IsNullOrEmpty(sirketNoo))
            {
                MessageBox.Show("Lütfen silmek istediğiniz özel şirketinin numarasını girin.", "Hata");
                return;
            }

            int sirketNo1;
            if (!int.TryParse(sirketNoo, out sirketNo1))
            {
                MessageBox.Show("Geçerli bir özel şirket numarası girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Silme sorgusunu oluştur
                            string deleteQuery1 = "DELETE FROM \"OzelSirket\" WHERE \"sirketNo\" = @sirketNo";
                            string deleteQuery2 = "DELETE FROM \"Sirket\" WHERE \"sirketNo\" = @sirketNo";

                            // İlk silme işlemi için komut
                            using (var command1 = new NpgsqlCommand(deleteQuery1, connection))
                            {
                                command1.Parameters.AddWithValue("@sirketNo", sirketNo1);

                                int rowsAffected1 = command1.ExecuteNonQuery();
                                if (rowsAffected1 == 0)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Silinecek özel şirket bulunamadı. Numarayı kontrol edin.", "Uyarı");
                                    return;
                                }
                            }

                            // İkinci silme işlemi için komut
                            using (var command2 = new NpgsqlCommand(deleteQuery2, connection))
                            {
                                command2.Parameters.AddWithValue("@sirketNo", sirketNo1);

                                int rowsAffected2 = command2.ExecuteNonQuery();
                                if (rowsAffected2 == 0)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Silinecek özel şirket bulunamadı. Numarayı kontrol edin.", "Uyarı");
                                    return;
                                }
                            }

                            // Başarılı silme işlemi sonrasında işlem tamamlanıyor
                            transaction.Commit();
                            sirketNo.Text = null;
                            sirketAdi.Text = null;
                            sektor.Text = null; 
                            sirketTuru.Text = null; 
                            iletisimKoduu.Text = null;  
                            sahipTCNoo.Text = null; 
                            calisanSayisii.Text = null;
                            MessageBox.Show("Ozel Şirket başarıyla silindi.", "Başarılı");
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda işlem geri alınıyor
                            transaction.Rollback();
                            MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Veritabanı bağlantısı sırasında bir hata oluştu: {ex.Message}", "Hata");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void kisiyiAra_Click(object sender, EventArgs e)
        {

            // T.C. Kimlik Numarası'nı al
            string tcNoStr = kisiSilText.Text;
            int tcNo;
            if (!int.TryParse(tcNoStr, out tcNo))
            {
                MessageBox.Show("Geçerli bir T.C. Kimlik Numarası girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Kişiyi veritabanında arama sorgusu
                    string query = "SELECT \"TCNo\",\"adi\", \"soyadi\", \"dogumTarihi\", \"anaAdi\", \"babaAdi\", \"medeniDurum\", \"iletisimKodu\" FROM \"Kisi\" WHERE \"TCNo\" = @tcNo";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        // Parametreyi ekle
                        command.Parameters.AddWithValue("@tcNo", tcNo);

                        // Sonuçları oku
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Verileri TextBox'lara yükle
                                textBox1.Text = reader["TCNo"].ToString();
                                textBox2.Text = reader["adi"].ToString();
                                textBox3.Text = reader["soyadi"].ToString();
                                textBox4.Text = reader["dogumTarihi"].ToString();
                                textBox5.Text = reader["anaAdi"].ToString();
                                textBox6.Text = reader["babaAdi"].ToString();
                                textBox7.Text = reader["medeniDurum"].ToString();
                                textBox8.Text = reader["iletisimKodu"].ToString();
                                kisiSilText.Text = null;
                            }
                            else
                            {
                                MessageBox.Show("Kişi bulunamadı.", "Hata");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void duzenlenecekKisi_Click(object sender, EventArgs e)
        {

            // T.C. Kimlik Numarası'nı al
            string tcNoStr = textBox1.Text;
            int tcNo;
            if (!int.TryParse(tcNoStr, out tcNo))
            {
                MessageBox.Show("Geçerli bir T.C. Kimlik Numarası girin.", "Hata");
                return;
            }

            // Diğer TextBox'lardan verileri al
            string adi = textBox2.Text;
            string soyadi = textBox3.Text;
            string dogumTarihi = textBox4.Text;
            string anaAdi = textBox5.Text;
            string babaAdi = textBox6.Text;
            string medeniDurumStr = textBox7.Text;
            string iletisimKoduStr = textBox8.Text;

            short medeniDurum;
            if (!short.TryParse(medeniDurumStr, out medeniDurum))
            {
                MessageBox.Show("Geçerli bir medeni durum girin.", "Hata");
                return;
            }

            DateTime dogumTarihiDate;
            if (!DateTime.TryParse(dogumTarihi, out dogumTarihiDate))
            {
                MessageBox.Show("Geçerli bir doğum tarihi girin.", "Hata");
                return;
            }

            int iletisimKodu;
            if (!int.TryParse(iletisimKoduStr, out iletisimKodu))
            {
                MessageBox.Show("Geçerli bir İletişim Kodu girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantısı ve transaction başlatma
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    // Veri güncelleme komutu
                    string query = "UPDATE \"Kisi\" SET \"adi\" = @adi, \"soyadi\" = @soyadi, \"dogumTarihi\" = @dogumTarihi, " +
                                   "\"anaAdi\" = @anaAdi, \"babaAdi\" = @babaAdi, \"medeniDurum\" = @medeniDurum, \"iletisimKodu\" = @iletisimKodu " +
                                   "WHERE \"TCNo\" = @tcNo";

                    using (var command = new NpgsqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@tcNo", tcNo);
                        command.Parameters.AddWithValue("@adi", adi);
                        command.Parameters.AddWithValue("@soyadi", soyadi);
                        command.Parameters.AddWithValue("@dogumTarihi", dogumTarihiDate);
                        command.Parameters.AddWithValue("@anaAdi", anaAdi);
                        command.Parameters.AddWithValue("@babaAdi", babaAdi);
                        command.Parameters.AddWithValue("@medeniDurum", medeniDurum);
                        command.Parameters.AddWithValue("@iletisimKodu", iletisimKodu);

                        // Komutu çalıştır
                        command.ExecuteNonQuery();
                    }

                    // Onayla ve işlemi başarıyla tamamla. Girilen text yerlerini de temizle.
                    transaction.Commit();
                    textBox1.Text = null;
                    textBox2.Text = null;
                    textBox3.Text = null;
                    textBox4.Text = null;
                    textBox5.Text = null;
                    textBox6.Text = null;
                    textBox7.Text = null;
                    textBox8.Text = null;
                    MessageBox.Show("Kişi başarıyla güncellendi.", "Başarılı");
                }
                catch (Exception ex)
                {
                    // Hata durumunda işlemi geri al
                    transaction.Rollback();
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                }
            }
        }

        private void sirketiBulVeGetir_Click(object sender, EventArgs e)
        {
            
            string sirketNoo = silinecekDevletSirketiText.Text;
            int sirketNo1;
            if (!int.TryParse(sirketNoo, out sirketNo1))
            {
                MessageBox.Show("Geçerli bir Devlet şirketi numarası girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {


                        // Kişiyi veritabanında arama sorgusu
                        string query = "SELECT \"sirketNo\",\"sirketAdi\", \"sektor\", \"sirketTuru\", \"sirketTuru\", \"iletisimKodu\" FROM \"Sirket\" WHERE \"sirketNo\" = @sirketNo";
                        string query1 = "SELECT \"sirketNo\",\"kurulusTarihi\", \"bakanlikNo\" FROM \"DevletSirketi\" WHERE \"sirketNo\" = @sirketNo";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreyi ekle
                            command.Parameters.AddWithValue("@sirketNo", sirketNo1);

                            // Sonuçları oku
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Verileri TextBox'lara yükle
                                    sirketNo.Text = reader["sirketNo"].ToString();
                                    sirketAdi.Text = reader["sirketAdi"].ToString();
                                    sektor.Text = reader["sektor"].ToString();
                                    sirketTuru.Text = reader["sirketTuru"].ToString();
                                    iletisimKoduu.Text = reader["iletisimKodu"].ToString();
                                }
                                else
                                {
                                    transaction.Rollback();

                                    MessageBox.Show(" Devlet şirketi bulunamadı.", "Hata");
                                }
                            }
                        }
                        using (var command = new NpgsqlCommand(query1, connection))
                        {
                            command.Parameters.AddWithValue("@sirketNo", sirketNo1);
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Verileri TextBox'lara yükle
                                    kurulusTarihiii.Text = reader["kurulusTarihi"].ToString();
                                    bakanlikNoo.Text = reader["bakanlikNo"].ToString();

                                }
                                else
                                {
                                    transaction.Rollback();
                                    MessageBox.Show(" Devlet şirketi bulunamadı.", "Hata");
                                }

                            }
                        }
                        silinecekDevletSirketiText.Text = null;
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }

        }

        private void devletSirketininDuzenlemesi_Click(object sender, EventArgs e)
        {
            // Formdaki TextBox'lardan veri al
            string sirketNumarasiStr = sirketNo.Text;        // Şirket numarası
            string sirketAd = sirketAdi.Text;            // şirket adı
            string sektoru = sektor.Text;         // sektör
            string sirketTurStr = sirketTuru.Text;    // şirket türü
            string iletisimKodStr = iletisimKoduu.Text;         // iletişim kodu
            string kurulusTarihStr = kurulusTarihiii.Text;        // kuruluş tarihi
            string bakanlikNoStr = bakanlikNoo.Text; // bakanlık numarası



            // Bakanlık numarası doğru formatta INT türüne dönüştür
            int bakanlikNo;
            if (!int.TryParse(bakanlikNoStr, out bakanlikNo))
            {
                MessageBox.Show("Geçerli bir bakanlik numarası girin.", "Hata");
                return;
            }



            // Şirket numarasını doğru formatta INT türüne dönüştür
            int sirketNumarasi;
            if (!int.TryParse(sirketNumarasiStr, out sirketNumarasi))
            {
                MessageBox.Show("Geçerli bir şirket numarası girin.", "Hata");
                return;
            }



            // İletişim kodu doğru formatta INT türüne dönüştür
            int iletisimKod;
            if (!int.TryParse(iletisimKodStr, out iletisimKod))
            {
                MessageBox.Show("Geçerli bir iletişim kodu girin.", "Hata");
                return;
            }




            //sirket Tür'ü char türüne dönüştür
            char sirketTur;
            if (!char.TryParse(sirketTurStr, out sirketTur))
            {
                MessageBox.Show("Geçerli bir Şirket Türü girin.", "Hata");
                return;
            }



            // Kuruluş tarihini doğru formatta DateTime türüne dönüştür
            DateTime kurulusTarih;
            if (!DateTime.TryParse(kurulusTarihStr, out kurulusTarih))
            {
                MessageBox.Show("Geçerli bir kuruluş tarihi girin (örneğin: 01-01-1990).", "Hata");
                return;
            }



            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";


            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Veritabanına veri ekleme komutu
                        string query = "UPDATE \"Sirket\" SET \"sirketNo\" = @sirketNo, \"sirketAdi\" = @sirketAdi, \"sektor\" = @sektor, " +
                                   "\"sirketTuru\" = @sirketTuru, \"iletisimKodu\" = @iletisimKodu " +
                                   "WHERE \"sirketNo\" = @sirketNo";


                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@sirketNo", sirketNumarasi);
                            command.Parameters.AddWithValue("@sirketAdi", sirketAd);
                            command.Parameters.AddWithValue("@sektor", sektoru);
                            command.Parameters.AddWithValue("@sirketTuru", sirketTur);
                            command.Parameters.AddWithValue("@iletisimKodu", iletisimKod);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }




                        string query1 = "UPDATE \"DevletSirketi\" SET \"sirketNo\" = @sirketNo, \"kurulusTarihi\" = @kurulusTarihi, \"bakanlikNo\" = @bakanlikNo " +
                                   "WHERE \"sirketNo\" = @sirketNo";

                        using (var command = new NpgsqlCommand(query1, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@sirketNo", sirketNumarasi);
                            command.Parameters.AddWithValue("@kurulusTarihi", kurulusTarih);
                            command.Parameters.AddWithValue("@bakanlikNo", bakanlikNo);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }
                        //İşlem başarıyla tamamlandı ve text'teki veriler silindi.
                        transaction.Commit();
                        sirketNo.Text = null;        
                        sirketAdi.Text = null;            
                        sektor.Text = null;         
                        sirketTuru.Text = null;    
                        iletisimKoduu.Text = null;         
                        kurulusTarihiii.Text = null;        
                        bakanlikNoo.Text = null;

                        MessageBox.Show("Devlet Şirketi başarıyla düzenlendi.", "Başarılı");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                    }
                }
            }
        }

        private void ozelSirketiBulVeGetir_Click(object sender, EventArgs e)
        {
            
            string sirketNoo = ozelSirketiSilText.Text;
            int sirketNo1;
            if (!int.TryParse(sirketNoo, out sirketNo1))
            {
                MessageBox.Show("Geçerli bir özel şirket numarası girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {


                        // Kişiyi veritabanında arama sorgusu
                        string query = "SELECT \"sirketNo\",\"sirketAdi\", \"sektor\", \"sirketTuru\", \"iletisimKodu\" FROM \"Sirket\" WHERE \"sirketNo\" = @sirketNo";
                        string query1 = "SELECT \"sirketNo\",\"sahipTCNo\",\"calisanSayisi\" FROM \"OzelSirket\" WHERE \"sirketNo\" = @sirketNo";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreyi ekle
                            command.Parameters.AddWithValue("@sirketNo", sirketNo1);

                            // Sonuçları oku
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Verileri TextBox'lara yükle
                                    sirketNo.Text = reader["sirketNo"].ToString();
                                    sirketAdi.Text = reader["sirketAdi"].ToString();
                                    sektor.Text = reader["sektor"].ToString();
                                    sirketTuru.Text = reader["sirketTuru"].ToString();
                                    iletisimKoduu.Text = reader["iletisimKodu"].ToString();
                                }
                                else
                                {
                                    transaction.Rollback();

                                    MessageBox.Show("Özel şirket bulunamadı.", "Hata");
                                }
                            }
                        }
                        using (var command = new NpgsqlCommand(query1, connection))
                        {
                            command.Parameters.AddWithValue("@sirketNo", sirketNo1);
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Verileri TextBox'lara yükle
                                    sahipTCNoo.Text = reader["sahipTCNo"].ToString();
                                    calisanSayisii.Text = reader["calisanSayisi"].ToString();

                                }
                                else
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Özel Şirketi bulunamadı.", "Hata");
                                }

                            }
                        }
                        ozelSirketiSilText.Text = null;
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void ozelSirketinDuzenlenmesi_Click(object sender, EventArgs e)
        {
            // Formdaki TextBox'lardan veri al
            string sirketNumarasiStr = sirketNo.Text;        // Şirket numarası
            string sirketAd = sirketAdi.Text;            // şirket adı
            string sektoru = sektor.Text;         // sektör
            string sirketTurStr = sirketTuru.Text;    // şirket türü
            string iletisimKodStr = iletisimKoduu.Text;         // iletişim kodu
            string sahipTCNoStr = sahipTCNoo.Text;        // sahibinin tc numarası
            string calisanSayisiStr = calisanSayisii.Text; // çalışan sayısı

            // sahibinin tc numarası doğru formatta INT türüne dönüştür
            int sahipTCNo;
            if (!int.TryParse(sahipTCNoStr, out sahipTCNo))
            {
                MessageBox.Show("Geçerli bir TC numarası girin.", "Hata");
                return;
            }

            

            int calisanSayisi;
            if (!int.TryParse(calisanSayisiStr, out calisanSayisi))
            {
                MessageBox.Show("Geçerli bir çalışan sayısı girin.", "Hata");
                return;
            }

            // Şirket numarasını doğru formatta INT türüne dönüştür
            int sirketNumarasi;
            if (!int.TryParse(sirketNumarasiStr, out sirketNumarasi))
            {
                MessageBox.Show("Geçerli bir şirket numarası girin.", "Hata");
                return;
            }

            // Şirket numarasını doğru formatta INT türüne dönüştür
            int iletisimKod;
            if (!int.TryParse(iletisimKodStr, out iletisimKod))
            {
                MessageBox.Show("Geçerli bir iletişim kodu girin.", "Hata");
                return;
            }

            // SirketTur'ü char türüne dönüştür
            char sirketTur;
            if (!char.TryParse(sirketTurStr, out sirketTur))
            {
                MessageBox.Show("Geçerli bir Şirket Türü girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Sirket tablosunu güncelle
                        string query = "UPDATE \"Sirket\" SET \"sirketNo\" = @sirketNo, \"sirketAdi\" = @sirketAdi, \"sektor\" = @sektor, " +
                                    "\"sirketTuru\" = @sirketTuru, \"iletisimKodu\" = @iletisimKodu " +
                                    "WHERE \"sirketNo\" = @sirketNo";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@sirketNo", sirketNumarasi);
                            command.Parameters.AddWithValue("@sirketAdi", sirketAd);
                            command.Parameters.AddWithValue("@sektor", sektoru);
                            command.Parameters.AddWithValue("@sirketTuru", sirketTur);
                            command.Parameters.AddWithValue("@iletisimKodu", iletisimKod);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }

                        // OzelSirket tablosunu güncelle
                        string query1 = "UPDATE \"OzelSirket\" SET \"sirketNo\" = @sirketNo, \"sahipTCNo\" = @sahipTCNo, " +
                                    "\"calisanSayisi\" = @calisanSayisi WHERE \"sirketNo\" = @sirketNo";

                        using (var command = new NpgsqlCommand(query1, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@sirketNo", sirketNumarasi);
                            command.Parameters.AddWithValue("@sahipTCNo", sahipTCNo);
                            command.Parameters.AddWithValue("@calisanSayisi", calisanSayisi);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }
                        // İşlem başarıyla tamamlandı ve textteki veriler silindi.
                        transaction.Commit();
                        sirketNo.Text = null;
                        sirketAdi.Text = null;
                        sektor.Text = null;
                        sirketTuru.Text = null;
                        iletisimKoduu.Text = null;
                        sahipTCNoo.Text = null;
                        calisanSayisii.Text = null;
                        MessageBox.Show("Özel Şirket başarıyla güncellendi.", "Başarılı");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Hata durumunda rollback
                        MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                    }
                }
            }

        }

        private void calisaniBulVeGetir_Click(object sender, EventArgs e)
        {
            // T.C. Kimlik Numarası'nı al
            string calisanTCNo3 = calisaniSilText.Text;
            int calisanTCNo;
            if (!int.TryParse(calisanTCNo3, out calisanTCNo))
            {
                MessageBox.Show("Geçerli bir çalışan tc numarası girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {


                        // Kişiyi veritabanında arama sorgusu
                        string query = "SELECT \"Calisan\",\"calisanNo\", \"sirketNo\", \"TCNo\", \"pozisyonNo\",  \"hisseMiktari\", \"maas\" FROM \"Calisan\" WHERE \"TCNo\" = @TCNo";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreyi ekle
                            command.Parameters.AddWithValue("@TCNo", calisanTCNo);

                            // Sonuçları oku
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Verileri TextBox'lara yükle
                                    calisanNo1.Text = reader["calisanNo"].ToString();
                                    sirketNo1.Text = reader["sirketNo"].ToString();
                                    calisanTCNo1.Text = reader["TCNo"].ToString();
                                    pozisyonNo1.Text = reader["pozisyonNo"].ToString();
                                    hisseMiktarii.Text = reader["hisseMiktari"].ToString();
                                    maasNo1.Text = reader["maas"].ToString();
                                }
                                else
                                {
                                    transaction.Rollback();

                                    MessageBox.Show("Çalışan bulunamadı.", "Hata");
                                }
                            }
                        }
                        calisaniSilText.Text = null;
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void calisaninDuzenlenmesi_Click(object sender, EventArgs e)
        {
            // Formdaki TextBox'lardan veri al
            string calisanNoo = calisanNo1.Text;        //Çalışan numarası
            string sirketNoo = sirketNo1.Text;            // Şirket numarası
            string TCNoo = calisanTCNo1.Text;         // Tc numarası
            string pozisyonNoo = pozisyonNo1.Text;    // pozisyon numarası
            string maasStr = maasNo1.Text; // Maaş
            string hisseMiktariStr = hisseMiktarii.Text; //Hisse miktarı


            // Çalışan numarasını int türüne dönüştür
            int calisanNo;
            if (!int.TryParse(calisanNoo, out calisanNo))
            {
                MessageBox.Show("Geçerli bir Çalışan Numarası girin.", "Hata");
                return;
            }


            int hisseMiktari;
            if (!int.TryParse(hisseMiktariStr, out hisseMiktari))
            {
                MessageBox.Show("Geçerli bir hisse miktarı girin.", "Hata");
                return;
            }

            int maas;
            if (!int.TryParse(maasStr, out maas))
            {
                MessageBox.Show("Geçerli bir maaş girin.", "Hata");
                return;
            }

           
            short sirketNo;
            if (!short.TryParse(sirketNoo, out sirketNo))
            {
                MessageBox.Show("Geçerli bir şirket numarası girin.", "Hata");
                return;
            }

            int TCNo;
            if (!int.TryParse(TCNoo, out TCNo))
            {
                MessageBox.Show("Geçerli bir TC Numarası girin.", "Hata");
                return;
            }

            short pozisyonNo;
            if (!short.TryParse(pozisyonNoo, out pozisyonNo))
            {
                MessageBox.Show("Geçerli bir Pozisyon Numarası girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Veritabanına veri ekleme komutu
                    string query = "UPDATE \"Calisan\" SET \"calisanNo\" = @calisanNo, \"sirketNo\" = @sirketNo, \"hisseMiktari\" = @hisseMiktari, \"TCNo\" = @TCNo, " +
                                    "\"pozisyonNo\" = @pozisyonNo, \"maas\" = @maas " +
                                    "WHERE \"TCNo\" = @TCNo";
                  

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        // Parametreleri ekle
                        command.Parameters.AddWithValue("@calisanNo", calisanNo);
                        command.Parameters.AddWithValue("@sirketNo", sirketNo);
                        command.Parameters.AddWithValue("@TCNo", TCNo);
                        command.Parameters.AddWithValue("@pozisyonNo", pozisyonNo);
                        command.Parameters.AddWithValue("@hisseMiktari", hisseMiktari);
                        command.Parameters.AddWithValue("@maas", maas);

                        // Komutu çalıştır
                        command.ExecuteNonQuery();
                    }
                }
                calisanNo1.Text = null;
                sirketNo1.Text = null;
                calisanTCNo1.Text = null;
                pozisyonNo1.Text = null;
                maasNo1.Text = null;
                hisseMiktarii.Text = null;
                MessageBox.Show("Çalışan başarıyla Duzenlendi.", "Başarılı");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void sirketHesabiniBulVeGetir_Click(object sender, EventArgs e)
        {
           
            string sirketBankaHesapNoo = bankaHesabiniSilText.Text;
            int sirketBankaHesapNo1;
            if (!int.TryParse(sirketBankaHesapNoo, out sirketBankaHesapNo1))
            {
                MessageBox.Show("Geçerli bir şirket hesap numarası girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {

                        
                        // Kişiyi veritabanında arama sorgusu
                        string query = "SELECT \"hesapNumarasi\", \"bakiye\", \"olusturulmaTarihi\", \"hesabiKullananTCNo\", \"hesapTuru\"  FROM \"BankaHesabi\" WHERE \"hesapNumarasi\" = @hesapNumarasi";
                        string query1 = "SELECT \"hesapNumarasi\",\"hesapAcilisAmaci\", \"sirketNumarasi\" FROM \"SirketHesabi\" WHERE \"hesapNumarasi\" = @hesapNumarasi";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreyi ekle
                            command.Parameters.AddWithValue("@hesapNumarasi", sirketBankaHesapNo1);

                            // Sonuçları oku
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Verileri TextBox'lara yükle
                                    hesapNumarasiNo2.Text = reader["hesapNumarasi"].ToString();
                                    bakiye2.Text = reader["bakiye"].ToString();
                                    olusturulmaTarihi2.Text = reader["olusturulmaTarihi"].ToString();
                                    hesabiKullananTCNo2.Text = reader["hesabiKullananTCNo"].ToString();
                                    hesapTuru2.Text = reader["hesapTuru"].ToString();
                                }
                                else
                                {
                                    transaction.Rollback();

                                    MessageBox.Show(" Şirket hesabı bulunamadı.", "Hata");
                                }
                            }
                        }
                        using (var command = new NpgsqlCommand(query1, connection))
                        {
                            command.Parameters.AddWithValue("@hesapNumarasi", sirketBankaHesapNo1);
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Verileri TextBox'lara yükle
                                    hesapAcilisAmac2.Text = reader["hesapAcilisAmaci"].ToString();
                                    sirketNo2.Text = reader["sirketNumarasi"].ToString();

                                }
                                else
                                {
                                    transaction.Rollback();
                                    MessageBox.Show(" Şirket hesabı bulunamadı.", "Hata");
                                }

                            }
                        }
                        transaction.Commit();
                        bankaHesabiniSilText.Text = null;             
                        anneKizlikSoyad.Text = null;
                        TCNo2.Text = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void sirketHesabiniDuzenle_Click(object sender, EventArgs e)
        {
            // Formdaki TextBox'lardan veri al
            string hesapNumarasiStr = hesapNumarasiNo2.Text;        // Hesap Numarası
            string bakiyeStr = bakiye2.Text;            // Bakiye
            string olusturulmaTarihiStr = olusturulmaTarihi2.Text;         // Oluşturulma Tarihi
            string hesabiKullananTCNoStr = hesabiKullananTCNo2.Text;    // Hesabı Kullanan TC No
            string hesapTuruStr = hesapTuru2.Text;         // Hesap Türü
            string hesapAcilisAmaci = hesapAcilisAmac2.Text; // Hesap Açılış Amacı
            string sirketNumarasiStr = sirketNo2.Text; // Şirket Numarası

            // Veri türlerine dönüştürme işlemleri
            int hesapNumarasi;
            if (!int.TryParse(hesapNumarasiStr, out hesapNumarasi))
            {
                MessageBox.Show("Geçerli bir Hesap Numarası girin.", "Hata");
                return;
            }

            int hesabiKullananTCNo;
            if (!int.TryParse(hesabiKullananTCNoStr, out hesabiKullananTCNo))
            {
                MessageBox.Show("Geçerli bir TC No girin.", "Hata");
                return;
            }

            int bakiye;
            if (!int.TryParse(bakiyeStr, out bakiye))
            {
                MessageBox.Show("Geçerli bir bakiye miktarı girin.", "Hata");
                return;
            }

            int sirketNumarasi;
            if (!int.TryParse(sirketNumarasiStr, out sirketNumarasi))
            {
                MessageBox.Show("Geçerli bir şirket numarası girin.", "Hata");
                return;
            }

            DateTime olusturulmaTarihi;
            if (!DateTime.TryParse(olusturulmaTarihiStr, out olusturulmaTarihi))
            {
                MessageBox.Show("Geçerli bir tarih girin.", "Hata");
                return;
            }

            char hesapTuru;
            if (!char.TryParse(hesapTuruStr, out hesapTuru))
            {
                MessageBox.Show("Geçerli bir hesap türü girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // BankaHesabi tablosuna veri güncelleme
                        string query = "UPDATE \"BankaHesabi\" SET \"hesapNumarasi\" = @hesapNumarasi, \"bakiye\" = @bakiye, \"olusturulmaTarihi\" = @olusturulmaTarihi, " +
                                       "\"hesabiKullananTCNo\" = @hesabiKullananTCNo, \"hesapTuru\" = @hesapTuru " +
                                       "WHERE \"hesapNumarasi\" = @hesapNumarasi";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@hesapNumarasi", hesapNumarasi);
                            command.Parameters.AddWithValue("@bakiye", bakiye);
                            command.Parameters.AddWithValue("@olusturulmaTarihi", olusturulmaTarihi);
                            command.Parameters.AddWithValue("@hesabiKullananTCNo", hesabiKullananTCNo);
                            command.Parameters.AddWithValue("@hesapTuru", hesapTuru);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }

                        // SirketHesabi tablosuna veri güncelleme
                        string query1 = "UPDATE \"SirketHesabi\" SET \"hesapNumarasi\" = @hesapNumarasi, \"hesapAcilisAmaci\" = @hesapAcilisAmaci, \"sirketNumarasi\" = @sirketNumarasi " +
                                        "WHERE \"hesapNumarasi\" = @hesapNumarasi";

                        using (var command = new NpgsqlCommand(query1, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@hesapNumarasi", hesapNumarasi);
                            command.Parameters.AddWithValue("@hesapAcilisAmaci", hesapAcilisAmaci);
                            command.Parameters.AddWithValue("@sirketNumarasi", sirketNumarasi);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }

                        // İşlemleri commit et
                        transaction.Commit();
                        hesapNumarasiNo2.Text = null;
                        bakiye2.Text = null;
                        olusturulmaTarihi2.Text = null;
                        hesabiKullananTCNo2.Text = null;
                        hesapTuru2.Text = null;
                        hesapAcilisAmac2.Text = null;
                        sirketNo2.Text = null;
                        MessageBox.Show("Şirket banka hesabı başarıyla düzenlendi.", "Başarılı");
                    }
                    catch (Exception ex)
                    {
                        // İşlem hatasında rollback yap
                        transaction.Rollback();
                        MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                    }
                }
            }

        }

        private void kisiselHesabiBulVeGetir_Click(object sender, EventArgs e)
        {
            // T.C. Kimlik Numarası'nı al
            string kisiselBankaHesapNoo = bankaHesabiniSilText.Text;
            int kisiselBankaHesapNo1;
            if (!int.TryParse(kisiselBankaHesapNoo, out kisiselBankaHesapNo1))
            {
                MessageBox.Show("Geçerli bir kişisel hesap banka numarası girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {


                        // Kişiyi veritabanında arama sorgusu
                        string query = "SELECT \"hesapNumarasi\", \"bakiye\", \"olusturulmaTarihi\", \"hesabiKullananTCNo\", \"hesapTuru\"  FROM \"BankaHesabi\" WHERE \"hesapNumarasi\" = @hesapNumarasi";
                        string query1 = "SELECT \"hesapNumarasi\",\"anneKizlikSoyadi\", \"TCNo\" FROM \"KisiselHesap\" WHERE \"hesapNumarasi\" = @hesapNumarasi";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreyi ekle
                            command.Parameters.AddWithValue("@hesapNumarasi", kisiselBankaHesapNo1);

                            // Sonuçları oku
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Verileri TextBox'lara yükle
                                    hesapNumarasiNo2.Text = reader["hesapNumarasi"].ToString();
                                    bakiye2.Text = reader["bakiye"].ToString();
                                    olusturulmaTarihi2.Text = reader["olusturulmaTarihi"].ToString();
                                    hesabiKullananTCNo2.Text = reader["hesabiKullananTCNo"].ToString();
                                    hesapTuru2.Text = reader["hesapTuru"].ToString();
                                }
                                else
                                {
                                    transaction.Rollback();

                                    MessageBox.Show(" Kişisel banka hesabı bulunamadı.", "Hata");
                                }
                            }
                        }
                        using (var command = new NpgsqlCommand(query1, connection))
                        {
                            command.Parameters.AddWithValue("@hesapNumarasi", kisiselBankaHesapNo1);
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Verileri TextBox'lara yükle
                                    anneKizlikSoyad.Text = reader["anneKizlikSoyadi"].ToString();
                                    TCNo2.Text = reader["TCNo"].ToString();

                                }
                                else
                                {
                                    transaction.Rollback();
                                    MessageBox.Show(" Kişisel banka hesabı bulunamadı.", "Hata");
                                }

                            }
                        }
                        transaction.Commit();
                        bankaHesabiniSilText.Text = null;                      
                        hesapAcilisAmac2.Text = null;                 
                        sirketNo2.Text = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }

        }

        private void kisiselHesabiniDuzenle_Click(object sender, EventArgs e)
        {
            // Formdaki TextBox'lardan veri al
            string hesapNumarasiStr = hesapNumarasiNo2.Text;
            string bakiyeStr = bakiye2.Text;
            string olusturulmaTarihiStr = olusturulmaTarihi2.Text;
            string hesabiKullananTCNoStr = hesabiKullananTCNo2.Text;
            string hesapTuruStr = hesapTuru2.Text;
            string anneKizlikSoyadi = anneKizlikSoyad.Text;
            string TCNoStr = TCNo2.Text;

            // Verileri doğru türdeki verilere dönüştür
            int hesapNumarasi;
            if (!int.TryParse(hesapNumarasiStr, out hesapNumarasi))
            {
                MessageBox.Show("Geçerli bir Kişisel hesap Numarası girin.", "Hata");
                return;
            }

            int hesabiKullananTCNo;
            if (!int.TryParse(hesabiKullananTCNoStr, out hesabiKullananTCNo))
            {
                MessageBox.Show("Geçerli bir TC No girin.", "Hata");
                return;
            }

            int bakiye;
            if (!int.TryParse(bakiyeStr, out bakiye))
            {
                MessageBox.Show("Geçerli bir bakiye miktarı girin.", "Hata");
                return;
            }

            int TCNo;
            if (!int.TryParse(TCNoStr, out TCNo))
            {
                MessageBox.Show("Geçerli bir TC numarası girin.", "Hata");
                return;
            }

            DateTime olusturulmaTarihi;
            if (!DateTime.TryParse(olusturulmaTarihiStr, out olusturulmaTarihi))
            {
                MessageBox.Show("Geçerli bir tarih girin.", "Hata");
                return;
            }

            // Hesap türünü doğru formatta kontrol et
            char hesapTuru;
            if (string.IsNullOrEmpty(hesapTuruStr) || !char.TryParse(hesapTuruStr, out hesapTuru))
            {
                MessageBox.Show("Geçerli bir hesap türü girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            // Veritabanına bağlan ve işlem yap
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Banka Hesabı güncelleme sorgusu
                        string query = "UPDATE \"BankaHesabi\" SET \"hesapNumarasi\" = @hesapNumarasi, \"bakiye\" = @bakiye, \"olusturulmaTarihi\" = @olusturulmaTarihi, " +
                                       "\"hesabiKullananTCNo\" = @hesabiKullananTCNo, \"hesapTuru\" = @hesapTuru " +
                                       "WHERE \"hesapNumarasi\" = @hesapNumarasi";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@hesapNumarasi", hesapNumarasi);
                            command.Parameters.AddWithValue("@bakiye", bakiye);
                            command.Parameters.AddWithValue("@olusturulmaTarihi", olusturulmaTarihi);
                            command.Parameters.AddWithValue("@hesabiKullananTCNo", hesabiKullananTCNo);
                            command.Parameters.AddWithValue("@hesapTuru", hesapTuru);

                            command.ExecuteNonQuery();
                        }

                        // Kişisel Hesap güncelleme sorgusu
                        string query1 = "UPDATE \"KisiselHesap\" SET \"hesapNumarasi\" = @hesapNumarasi, \"anneKizlikSoyadi\" = @anneKizlikSoyadi, \"TCNo\" = @TCNo " +
                                        "WHERE \"hesapNumarasi\" = @hesapNumarasi";

                        using (var command = new NpgsqlCommand(query1, connection))
                        {
                            command.Parameters.AddWithValue("@hesapNumarasi", hesapNumarasi);
                            command.Parameters.AddWithValue("@anneKizlikSoyadi", anneKizlikSoyadi);
                            command.Parameters.AddWithValue("@TCNo", TCNo);

                            command.ExecuteNonQuery();
                        }

                        // Commit işlemi
                        transaction.Commit();
                        hesapNumarasiNo2.Text = null;
                        bakiye2.Text = null;
                        olusturulmaTarihi2.Text = null;
                        hesabiKullananTCNo2.Text = null;
                        hesapTuru2.Text = null;
                        anneKizlikSoyad.Text = null;
                        TCNo2.Text = null;

                        MessageBox.Show("Kişisel Banka Hesabı başarıyla Düzenlendi.", "Başarılı");
                    }
                    catch (Exception ex)
                    {
                        // Hata durumunda geri al
                        transaction.Rollback();
                        MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                    }
                }
            }

        }

        private void iletisimEkle_Click(object sender, EventArgs e)
        {
            string iletisimKodu1Str = iletisimKoduText.Text;
            string telNo1 = telNo.Text;
            string ePosta1 = ePosta.Text;
            string ilceKodu1Str = ilceKodu.Text;

            // Verileri doğru türdeki verilere dönüştür
            int iletisimKodu1;
            if (!int.TryParse(iletisimKodu1Str, out iletisimKodu1))
            {
                MessageBox.Show("Geçerli bir iletişim kodu girin.", "Hata");
                return;
            }

            int ilceKodu1;
            if (!int.TryParse(ilceKodu1Str, out ilceKodu1))
            {
                MessageBox.Show("Geçerli ilçe kodu girin.", "Hata");
                return;
            }


            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";



            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {


                        // Veritabanına veri ekleme komutu
                        string query = "INSERT INTO \"Iletisim\" ( \"iletisimKodu\", \"telNo\", \"ePosta\", \"ilceKodu\") " +
                                   "VALUES (@iletisimKodu, @telNo, @ePosta, @ilceKodu)";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@iletisimKodu", iletisimKodu1);
                            command.Parameters.AddWithValue("@telNo", telNo1);
                            command.Parameters.AddWithValue("@ePosta", ePosta1);
                            command.Parameters.AddWithValue("@ilceKodu", ilceKodu1);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        iletisimKoduText.Text = null;
                        telNo.Text = null;
                        ePosta.Text = null;
                        ilceKodu.Text = null;
                        MessageBox.Show(" İletişim Bilgisi başarıyla eklendi.", "Başarılı");


                    }

                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                    }
                }
            
        }
        }

        private void iletisimiBulVeGetir_Click(object sender, EventArgs e)
        {
            string iletisimKodu1Str = iletisimKodu.Text;

            // İletişim kodunu doğru türde bir değere dönüştür
            int iletisimKodu1;
            if (!int.TryParse(iletisimKodu1Str, out iletisimKodu1))
            {
                MessageBox.Show("Geçerli bir iletişim kodu girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    // Veritabanında iletişim bilgilerini bulma sorgusu
                    string query = "SELECT \"iletisimKodu\",\"telNo\", \"ePosta\", \"ilceKodu\" FROM \"Iletisim\" WHERE \"iletisimKodu\" = @iletisimKodu";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        // Parametreyi ekle
                        command.Parameters.AddWithValue("@iletisimKodu", iletisimKodu1);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Eğer veri bulunursa
                            {
                                // Veritabanından gelen değerleri TextBox'lara doldur
                                iletisimKoduText.Text= reader["iletisimKodu"].ToString();
                                telNo.Text = reader["telNo"].ToString();
                                ePosta.Text = reader["ePosta"].ToString();
                                ilceKodu.Text = reader["ilceKodu"].ToString();

                                iletisimKodu.Text = null;
                                MessageBox.Show("İletişim bilgisi başarıyla getirildi.", "Başarılı");
                            }
                            else
                            {
                                MessageBox.Show("Belirtilen iletişim koduyla eşleşen bir kayıt bulunamadı.", "Bilgi");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                }
            }


        }

        private void iletisimiSil_Click(object sender, EventArgs e)
        {
            // Formdaki TextBox'lardan iletişim kodunu al
            string iletisimKodu1Str = iletisimKoduText.Text;

            // İletişim kodunu doğru türde bir değere dönüştür
            int iletisimKodu1;
            if (!int.TryParse(iletisimKodu1Str, out iletisimKodu1))
            {
                MessageBox.Show("Geçerli bir iletişim kodu girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    // Silme sorgusu
                    string query = "DELETE FROM \"Iletisim\" WHERE \"iletisimKodu\" = @iletisimKodu";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        // Parametreyi ekle
                        command.Parameters.AddWithValue("@iletisimKodu", iletisimKodu1);

                        // Komutu çalıştır ve etkilenen satır sayısını al
                        int affectedRows = command.ExecuteNonQuery();

                        if (affectedRows > 0) // Eğer silme işlemi başarılı olduysa
                        {
                            iletisimKoduText.Text = null;
                            telNo.Text = null;
                            ePosta.Text = null;
                            ilceKodu.Text = null;  

                            MessageBox.Show("İletişim bilgisi başarıyla silindi.", "Başarılı");
                        }
                        else
                        {
                            MessageBox.Show("Belirtilen iletişim koduyla eşleşen bir kayıt bulunamadı.", "Bilgi");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                }
            }


        }

        private void iletisimiDuzenle_Click(object sender, EventArgs e)
        {
            string iletisimKodu1Str = iletisimKoduText.Text;
            string telNo1 = telNo.Text;
            string ePosta1 = ePosta.Text;
            string ilceKodu1Str = ilceKodu.Text;

            // Verileri doğru türdeki verilere dönüştür
            int iletisimKodu1;
            if (!int.TryParse(iletisimKodu1Str, out iletisimKodu1))
            {
                MessageBox.Show("Geçerli bir iletişim kodu girin.", "Hata");
                return;
            }

            int ilceKodu1;
            if (!int.TryParse(ilceKodu1Str, out ilceKodu1))
            {
                MessageBox.Show("Geçerli ilçe kodu girin.", "Hata");
                return;
            }


            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";



            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {


                        // Veritabanına veri ekleme komutu
                        string query = "UPDATE \"Iletisim\" SET \"iletisimKodu\" = @iletisimKodu, \"telNo\" = @telNo, \"ePosta\" = @ePosta, \"ilceKodu\"=@ilceKodu " +
                                        "WHERE \"iletisimKodu\" = @iletisimKodu";


                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@iletisimKodu", iletisimKodu1);
                            command.Parameters.AddWithValue("@telNo", telNo1);
                            command.Parameters.AddWithValue("@ePosta", ePosta1);
                            command.Parameters.AddWithValue("@ilceKodu", ilceKodu1);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        iletisimKoduText.Text = null;
                        telNo.Text = null;
                        ePosta.Text = null;
                        ilceKodu.Text = null;
                        MessageBox.Show(" İletişim Bilgisi başarıyla düzenlendi.", "Başarılı");


                    }

                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                    }
                }
            }
            }

        private void ilceleriGoster_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // İletişim bilgilerini çek
                    string query = "SELECT \"ilceKodu\", \"ilceAdi\", \"ilKodu\"  FROM \"Ilce\"";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();


                    string query = "SELECT * FROM evlilikyuzdesigetir()";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();


                    string query = "SELECT * FROM kisiSayisiniGetir()";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'i sıfırla ve yeni verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string hesapNumarasiStr = hesaptakiParayiGetirText.Text;
            int hesapNumarasi;

            if (!int.TryParse(hesapNumarasiStr, out hesapNumarasi))
            {
                MessageBox.Show("Geçerli bir hesap numarası girin.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM hesaptakiparayigetir(@hesapNumarasi)";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        // Parametre ekleme
                        command.Parameters.AddWithValue("@hesapNumarasi", hesapNumarasi);

                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // DataGridView'i sıfırla ve yeni verileri ata
                            dataGridView1.DataSource = dataTable;
                            hesaptakiParayiGetirText.Text = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            string bakanlikAdi = bakanlikAdiText.Text;


            if (string.IsNullOrEmpty(bakanlikAdi))
            {
                MessageBox.Show("Lütfen bakanlık ismini giriniz.", "Hata");
                return;
            }

            // PostgreSQL bağlantı dizesi
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=yeni";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM sirketsayisinigetir(@bakanlikAdi)";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        // Parametre ekleme
                        command.Parameters.AddWithValue("@bakanlikAdi", bakanlikAdi);

                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // DataGridView'i sıfırla ve yeni verileri ata
                            dataGridView1.DataSource = dataTable;
                            bakanlikAdiText.Text=null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void bakanlikAdiText_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void silinecekDevletSirketiText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}






