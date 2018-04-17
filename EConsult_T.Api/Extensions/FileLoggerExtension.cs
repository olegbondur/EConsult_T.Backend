using EConsult_T.Api.Logging;
using Microsoft.Extensions.Logging;

namespace EConsult_T.Api.Extensions
{
    public static class FileLoggerExtension
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory,
                                        string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));
            return factory;
        }
    }
}
