using System.Text;

namespace ChatAggregator.Domain.Enums
{
    public static class StringBuilderExtensions
    {
        public static void AppendLineIf(this StringBuilder sb, bool condition, string value)
        {
            if (condition)
            {
                sb.AppendLine(value);
            }
        }
    }
}