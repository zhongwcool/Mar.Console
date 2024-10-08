﻿using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Mar.Cheese;

public class EmailUtil
{
    /// <summary>
    ///     发送邮件
    /// </summary>
    /// <param name="from">发件人邮箱</param>
    /// <param name="to">收件人邮箱</param>
    /// <param name="subject">邮件主题</param>
    /// <param name="body">邮件正文</param>
    /// <param name="smtpServer">SMTP 服务器地址</param>
    /// <param name="smtpPort">SMTP 服务器端口</param>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <param name="attachments">附件列表</param>
    public static async Task<bool> SendEmailAsync(string from, string to, string subject, string body,
        string smtpServer,
        int smtpPort, string username, string password, List<string>? attachments = null)
    {
        var result = false;
        await Task.Run(() =>
        {
            using var mail = new MailMessage(from, to, subject, body);
            mail.CC.Add(from);

            if (attachments != null)
            {
                foreach (var file in attachments)
                {
                    if (!File.Exists(file))
                    {
                        Console.WriteLine($"附件不存在：{file}");
                        continue;
                    }

                    var attachment = new Attachment(file);
                    mail.Attachments.Add(attachment);
                }
            }

            using var smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.Credentials = new NetworkCredential(username, password);

            try
            {
                smtpClient.Send(mail);
                result = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"发送邮件时出错：{ex.Message}");
                result = false;
            }
        });
        return result;
    }
}