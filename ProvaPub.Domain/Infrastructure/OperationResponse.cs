using ProvaPub.Domain.Enum;

namespace ProvaPub.Domain.Infrastructure
{
    public class OperationResponse<T>
    {
        public OperationResponse()
        {
            if (typeof(T) == typeof(string))
                Data = default(T);
            else
                Data = System.Activator.CreateInstance<T>();

            this.Messages = new List<OperationMessage>();
        }

        public bool IsSucceed { get { return !this.Messages.Any(p => p.Type == EnumOperationMessage.Error || p.Type == EnumOperationMessage.ErrorNotFound); } }

        public T Data { get; set; }

        public List<OperationMessage> Messages { get; set; }

        public OperationResponse<T> AddMessage(IEnumerable<OperationMessage> messages)
        {
            this.Messages.AddRange(messages);
            return this;
        }

        public OperationResponse<T> AddMessage(OperationMessage message)
        {
            this.Messages.Add(message);
            return this;
        }

        public OperationResponse<T> AddSuccess(string description, object data)
        {
            var message = new OperationMessage
            {
                Data = data,
                Description = description,
                DescriptionType = System.Enum.GetName(typeof(EnumOperationMessage), EnumOperationMessage.Success),
                Type = EnumOperationMessage.Success
            };

            this.Messages.Add(message);
            return this;
        }

        public OperationResponse<T> AddError(string description, string stackTrace, Exception innerException, object data)
        {
            var message = new OperationMessage
            {
                Data = data,
                Description = description,
                DescriptionType = System.Enum.GetName(typeof(EnumOperationMessage), EnumOperationMessage.Error),
                Type = EnumOperationMessage.Error,
                StackTrace = stackTrace,
                InnerException = innerException
            };

            this.Messages.Add(message);
            return this;
        }

        public OperationResponse<T> AddError(string description, object data)
        {
            var message = new OperationMessage
            {
                Data = data,
                Description = description,
                DescriptionType = System.Enum.GetName(typeof(EnumOperationMessage), EnumOperationMessage.Error),
                Type = EnumOperationMessage.Error
            };

            this.Messages.Add(message);
            return this;
        }

        public OperationResponse<T> AddErrorNotFound(string description, object data)
        {
            var message = new OperationMessage
            {
                Data = data,
                Description = description,
                DescriptionType = System.Enum.GetName(typeof(EnumOperationMessage), EnumOperationMessage.ErrorNotFound),
                Type = EnumOperationMessage.ErrorNotFound
            };

            this.Messages.Add(message);
            return this;
        }

        public OperationResponse<T> AddMessage(EnumOperationMessage type, string description, params object[] descriptionParams)
        {
            this.Messages.Add(new OperationMessage { Type = type, Description = description });

            foreach (var item in descriptionParams)
            {
                this.Messages.Add(new OperationMessage { Type = type, Description = item as string });
            }
            return this;
        }
    }
}
