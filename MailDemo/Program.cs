using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;

var message = new MimeMessage();
var from = new MailboxAddress("Alice", "alice@example.com");
message.From.Add(from);

var to = new MailboxAddress("Bob", "bob@examle.com");
message.To.Add(to);
message.Subject = "Hi Bob!";

var bb = new BodyBuilder
{ 
    TextBody = "Hello Bob in plain test"
};
var imageEntity = bb.LinkedResources.Add("cat.jpg");
imageEntity.ContentId = MimeUtils.GenerateMessageId();

bb.HtmlBody = $"""
              <p>Hello Bob!</p>
              <p><img src="cid:{imageEntity.ContentId}" alt="Cat!" /></p>
              """;

message.Body = bb.ToMessageBody();

using var smtp = new SmtpClient();
await smtp.ConnectAsync("localhost", 1025);
await smtp.SendAsync(message);
await smtp.DisconnectAsync(true);

Console.WriteLine($"Mail sent to {to.Address}");