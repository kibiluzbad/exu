using System;
using System.ServiceModel;

namespace Exu.RouteService.Infra.Query
{
    public abstract class WcfBasicHttpQueryBase<TResult, TServiceContract> 
        : IQuery<TResult>
    {
        public virtual TResult Execute()
        {
           return GetExecuteMethod(CreateChannelFactory());
        }

        protected abstract Func<TServiceContract, TResult> GetExecuteMethod { get; }

        protected virtual TServiceContract CreateChannelFactory()
        {
            var binding = new BasicHttpBinding(ConfigurationName);
            
             return new ChannelFactory<TServiceContract>(binding).CreateChannel();
        }

        protected abstract string ConfigurationName { get; }
    }
}