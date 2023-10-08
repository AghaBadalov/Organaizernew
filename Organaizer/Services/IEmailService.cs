using Organaizer.Models;

namespace Organaizer.Services
{
    public interface IEmailService
    {
        void Send(Message message);
    }
}
