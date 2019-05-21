using Xamarin.Forms;

namespace People
{
    public partial class App : Application
    {
        public static PersonRepository PersonRepo { get; set; }

        public App(string dbPath)
        {
            InitializeComponent();

            PersonRepo = new PersonRepository(dbPath);

            MainPage = new MainPage();
        }
    }
}
