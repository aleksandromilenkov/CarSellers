using System.Threading.Tasks;
namespace CarSellers.Interface
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string emailAddress, string subject, string message);
    }
}
