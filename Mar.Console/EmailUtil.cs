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
    public static async Task<bool> SendEmail(string from, string to, string subject, string body, string smtpServer,
        int smtpPort, string username, string password, string[]? attachments = null)
    {
        await Task.Run(() =>
        {
            // 创建邮件消息对象
            var mail = new MailMessage(from, to, subject, body);
            mail.CC.Add(from);

            // 添加附件
            foreach (var file in attachments!)
            {
                if (File.Exists(file) == false)
                {
                    $"附件不存在：{file}".PrintYellow();
                    continue;
                }

                var attachment = new Attachment(file);
                mail.Attachments.Add(attachment);
            }

            // 创建 SMTP 客户端
            var smtpClient = new SmtpClient(smtpServer, smtpPort);

            // 如果需要身份验证，则设置用户名和密码
            smtpClient.Credentials = new NetworkCredential(username, password);

            try
            {
                // 发送邮件
                smtpClient.Send(mail);
                "邮件发送成功！".PrintGreen();
            }
            catch (Exception ex)
            {
                $"邮件发送失败：{ex.Message}".PrintErr();
                return false;
            }
            finally
            {
                // 清理资源
                mail.Dispose();
                smtpClient.Dispose();
            }

            return true;
        });

        return true;
    }
}