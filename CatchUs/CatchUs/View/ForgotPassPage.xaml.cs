using CatchUs.Data;
using CatchUs.Model;
using System;
using System.Net;
using System.Net.Mail;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatchUs.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPassPage : ContentPage
    {
        Repository repo = new Repository();

        Random rd = new Random();
        int? code; // Do zmiany na pozniej

        public ForgotPassPage()
        {
            InitializeComponent();

            //Usuwanie do pomocy przy pisaniu aplikacji
            foreach (PasswordRecovery p in repo.GetAllPasswordsRecovery())
            {
                if ((DateTime.Now.Date.Subtract(repo.GetPasswordRecoveryByEmail(Preferences.Get("resetPass_Email", "")).Time)).Minutes > 5)
                    repo.DeletePasswordRecovery(p.Id);
            }
        }

        async void SendEmail_Clicked(object sender, EventArgs e)
        {
            code = rd.Next(1000, 9999);

            if (repo.IsUserEmailExist(Email.Text))
            {
                if (!repo.IsPasswordRecoveryEmailExist(Email.Text))
                {
                    PasswordRecovery newPasswordRecovery = new PasswordRecovery() { Email = Email.Text, Code = code.ToString(), Time = DateTime.Now.Date };

                    LabelEmail.Text = "We sent veryfication code to your e-mail. It can take up to 5 minutes.";

                    Send(code.ToString());

                    Preferences.Set("resetPass_Email", Email.Text); //dodaje referencje
                    Preferences.Set("resetPass_Code", code.ToString()); //dodaje referencje
                    repo.InsertPasswordRecovery(newPasswordRecovery); // dodaje do bazy

                    await Navigation.PushModalAsync(new VerifyCodePage());
                }
                else
                    LabelEmail.Text = "We already sent you a veryfication code. Check your mail-box.";
            }
            else
                LabelEmail.Text = "Given e-mail dosen't exist.";
        }

        #region Send Email With Code
        void Send(string code)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("xamarinemailtest@gmail.com", "Bazilla11")
            };
            MailMessage mail = new MailMessage
            {
                From = new MailAddress("xamarinemailtest@gmail.com"), // ---------------------------------------------------- do zmiany na przyszlosc
                Subject = "CatchUs - Reset your password",
                Body = BodyMessage()
            };
            mail.To.Add("s19468@pjwstk.edu.pl"); // ---------------------------------------------------- do zmiany na przyszlosc

            SmtpServer.Send(mail);
        }

        string BodyMessage()
        {
            return "Please use this code to reset the password for the CatchUs application.\n \n"
                + "Here is your code: \n"
                + code + "\n \n "
                + "If you didn't send any reset password request, just ignore that message. \n \n "
                + "Thanks, \n CatchUs Team.";
        }
        #endregion
    }
}