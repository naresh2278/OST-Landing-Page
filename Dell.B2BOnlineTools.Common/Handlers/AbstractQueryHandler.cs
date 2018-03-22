using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dell.B2BOnlineTools.Common.Handlers
{
    public abstract class AbstractQueryHandler<TRequest, TResponse> 
        : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public TResponse Handle(TRequest message)
        {         
            if (!ValidateRequest(message))
            {
                throw new Exception();
            }
            return HandleRequest(message);
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            Task<TResponse> t1 = Task.Factory.StartNew(() =>
            {
                var response = HandleRequest(request);
                return response;
            });
            return t1;
        }

        public abstract TResponse HandleRequest(TRequest request);
        public virtual bool ValidateRequest(TRequest request)
        {
            return true;
        }

        public virtual bool ValidateResponse(TResponse response)
        {
            return true;
        }       
    }
}