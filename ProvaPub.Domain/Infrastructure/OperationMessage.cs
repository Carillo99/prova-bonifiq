using ProvaPub.Domain.Enum;

namespace ProvaPub.Domain.Infrastructure
{
    public class OperationMessage
    {
        public OperationMessage()
        {

        }

        public OperationMessage(EnumOperationMessage type, string description)
        {
            Description = description;
            Type = type;
        }

        public string StackTrace { get; set; }

        public Exception InnerException { get; set; }

        public string Description { get; set; }

        public string DescriptionType { get; set; }

        public EnumOperationMessage Type { get; set; }

        public object Data { get; set; }
    }
}
