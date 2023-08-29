namespace TestProject1
{
    [TestClass]
    public class UnitTestBackupSpiegeln
    {
       

        public UnitTestBackupSpiegeln() 
        {
            string FromPath = @"C:\Users\Funkers Tronic\Desktop\From";
            string ToPath = @"C:\Users\Funkers Tronic\Desktop\To";

            Directory.CreateDirectory(FromPath);
            for (int i = 0; i < 10; i++)             
                for (int j = 0; j < 10; j++)
                {
                    Directory.CreateDirectory(FromPath + "\\" + i.ToString() + "\\" + j.ToString());
                    File.Create(FromPath + "\\" + i.ToString() + "\\" + j.ToString() + ".bin");
                }             
            Directory.CreateDirectory(ToPath);
        }
        [TestMethod]
        public void TestMethod1()
        {
            string result = Path.GetPathRoot("C:\\Users\\Funkers Tronic\\Desktop\\To\\Backup\\Users\\Funkers Tronic\\Desktop\\From");

            Assert.AreEqual<string>("C:\\", result);
        }
    }
}