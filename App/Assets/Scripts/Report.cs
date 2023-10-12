using UnityEngine;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections;

public class Report : MonoBehaviour
{

    private Database database;

    private string smtpHost = "smtp.gmail.com"; // Endereço do servidor SMTP
    private int smtpPort = 587; // Porta SMTP (587 é uma porta comum para TLS)
    private string senderEmail = "reportteacog@gmail.com";
    private string senderPassword = "nayxiyudarbxroqa";
    private string recipientEmail;
    private string subject = "Relatório Paciente - TESTE BOTAO NOVO 1 - TEACOG - ";
    //private string body = "teste";

    void Start() {
        database = this.gameObject.AddComponent<Database>();

        recipientEmail = database.GetEmail();
        subject = subject + database.GetUserName();
    }

    public void SendEmail(string bodyText)
    {
        MailMessage mail = new MailMessage(senderEmail, recipientEmail);
        mail.Subject = subject;
        mail.Body = bodyText;
        

        SmtpClient smtpServer = new SmtpClient(smtpHost);
        smtpServer.Port = smtpPort;
        smtpServer.Credentials = new NetworkCredential(senderEmail, senderPassword);
        smtpServer.EnableSsl = true; // Use SSL/TLS para criptografar a conexão

        // Certificado de segurança para conexão SSL/TLS (não é recomendado usar este certificado de forma rasa em produção)
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

        try
        {
            smtpServer.Send(mail);
            Debug.Log("E-mail enviado com sucesso!");
        }
        catch (Exception e)
        {
            Debug.LogError("Erro ao enviar o e-mail: " + e.Message);
        }

    }

}
