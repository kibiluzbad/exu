using System;
using System.ServiceModel;
using System.ServiceModel.Description;

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
            return new ChannelFactory<TServiceContract>(ConfigurationName).CreateChannel();
        }

        protected abstract string ConfigurationName { get; }
    }
}