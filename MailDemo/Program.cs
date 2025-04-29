using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

var message = new MimeMessage();
var from = new MailboxAddress("Alice", "alice@example.com");
message.From.Add(from);

var to = new MailboxAddress("Bob", "bob@examle.com");
message.To.Add(to);
message.Subject = "Hi Bob!";
message.Body = new TextPart(TextFormat.Plain)
{
    Text = """
           Hi Bob,
           
           This is that email thing I was telling you about.
           
           Pretty neat, huh?
           
           -Alice
           """
};

using var smtp = new SmtpClient();
await smtp.ConnectAsync("localhost", 1025);
await smtp.SendAsync(message);
await smtp.DisconnectAsync(true);

Console.WriteLine($"Mail sent to {to.Address}");