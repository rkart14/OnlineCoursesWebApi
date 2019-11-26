using MediatR;
using OnlineCourses.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineCourses.Application.PipelineBehaviors
{
    public class DomainEventPublishBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IDomainEventPublishPipeline
    {
            private readonly IDomainEventContext _dbContext;

            public DomainEventPublishBehaviour(IDomainEventContext dbContext, IMediator mediator)
            {
                _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            }

            public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
                RequestHandlerDelegate<TResponse> next)
            {
                TResponse response = await next();

                var events = _dbContext.GetDomainEvents().ToList();

                foreach (var @event in events)
                {
                    //@publish those events somewhere
                }

                return response;
            }
        }
}
