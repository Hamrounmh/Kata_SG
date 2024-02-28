using Kata.Models;

namespace Kata.Services
{
    public interface ICsvAccountReaderService
    {
        Account DeserializeCsv();
    }
}
