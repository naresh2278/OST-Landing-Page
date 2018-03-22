using Ninject.Modules;
using Ninject.Extensions.Conventions;
using MediatR;

namespace Dell.B2BOnlineTools.Common
{
    public class NinjectConfigModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(x =>
                x.FromThisAssembly()
                .SelectAllClasses()
                .InheritedFromAny(new[] {
                     typeof(IRequestHandler<,>),
                     typeof(INotificationHandler<>)
                 })
                .BindAllInterfaces()
            );
        }
    }
}