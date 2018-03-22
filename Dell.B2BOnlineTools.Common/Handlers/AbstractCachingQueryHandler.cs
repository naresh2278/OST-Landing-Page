using MediatR;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dell.B2BOnlineTools.Common.Handlers
{
    public abstract class AbstractCachingQueryHandler<TRequest, TResponse>
        : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : new()
    {
        protected IMediator _mediator;
        [Inject]
        public IMediator Mediator
        {
            get { return _mediator; }
            set { _mediator = value; }
        }
        
        protected virtual TResponse GetResponseFromCache(TRequest request) => Mediator.Send<TResponse>(request).Result;        
        protected virtual TResponse GetResponseFromExternalService(TRequest request) => Mediator.Send<TResponse>(request).Result;        
        protected abstract void UpdateCache(TResponse response);

        protected virtual bool ValidateRequest(TRequest request)
        {
            if (request == null) return false;            
            return true;
        }
        protected virtual bool ValidateResponse(TResponse response)
        {
            if (response == null) return false;
            return true;
        }

        public TResponse Handle(TRequest message)
        {
            TResponse response;
            if(!ValidateRequest(message)) return new TResponse();

            response = GetResponseFromCache(message);
            if (response == null)
                response = GetResponseFromExternalService(message);
            if (!ValidateResponse(response)) return new TResponse();
            UpdateCache(response);
            return response;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            Task<TResponse> t1 = Task.Factory.StartNew(() =>
            {
                var response = GetResponseFromExternalService(request);
                return response;
            });
            return t1;
        }
    }
}
