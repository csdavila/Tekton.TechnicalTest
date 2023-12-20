using Audit.Core;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Tekton.TechnicalTest.Shared.Common.Attributes;

namespace Tekton.TechnicalTest.Shared.Common.Behaviors
{

    public class AuditLogsBehavior<TRequest, TResponse>(
        ILogger<AuditLogsBehavior<TRequest, TResponse>> logger,
        IConfiguration config) : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<AuditLogsBehavior<TRequest, TResponse>> _logger = logger;
        private readonly IConfiguration _config = config;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{RequetsName}: with request {@Request}",
                 typeof(TRequest).Name, request);

            IAuditScope? scope = null;

            var auditLogAttributes = request.GetType().GetCustomAttributes<AuditLogAttribute>();
            if (auditLogAttributes.Any() && _config.GetValue<bool>("AuditLogs:Enabled"))
            {
                // El IRequest cuenta con el atributo [AuditLog] para ser auditado
                scope = AuditScope.Create(_ => _
                    .EventType(typeof(TRequest).Name)
                    .ExtraFields(new
                    {
                        Request = request
                    }));
            }

            var result = await next();

            if (scope is not null)
            {
                await scope.DisposeAsync();
            }

            return result;
        }
    }
}