using JParts.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace JParts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //using (var jpContext = new DBContext.JPartsContext())
            //{
            //    IAuthenticationService authentication = new AuthenticationService(new UnitOfWork.UnitOfWork(jpContext));
            //    authentication.Register("test", "John Doe", "+375296417774", "Melezha-4", "test@gmail.com", "test", "1234567", "1234567");
            //}
            base.OnStartup(e);
        }
    }
}
