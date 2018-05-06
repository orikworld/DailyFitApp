using System;
using System.Collections.Generic;

namespace DailyFitNative.Infrastructure.DependencyInjection.Interfaces
{
    public interface ICustomContainer : IDisposable
    {
        void RegisterInstance<TInterface>(TInterface instance);

        void RegisterInstance<TInterface>(TInterface instance, LifetimeCycle lifetimeCycle);

        void RegisterInstance<TInterface>(string name, TInterface instance);

        void RegisterInstance<TInterface>(string name, TInterface instance, LifetimeCycle lifetimeCycle);

        void RegisterDependency<TFrom, TTo>() where TTo : TFrom;

        void RegisterDependency<TFrom, TTo>(string name) where TTo : TFrom;

        void RegisterDependency<TFrom, TTo>(LifetimeCycle lifetimeManager) where TTo : TFrom;

        void RegisterDependency<TFrom, TTo>(string name, LifetimeCycle lifetimeManager) where TTo : TFrom;

        void RegisterDependency<TFrom, TTo>(string name, KeyValuePair<string, Type> constructorInjectionParameters)
            where TTo : TFrom;

        void RegisterDependency<TFrom, TTo>(
            string name,
            KeyValuePair<string, Type> constructorInjectionParameters,
            LifetimeCycle lifetimeCycle) where TTo : TFrom;

        void RegisterDependency<TFrom, TTo>(string name, Type typeToBeInjected, string typeIdentifier = null)
            where TTo : TFrom;

        void RegisterDependency<TFrom, TTo, TToBeInjected>(string name, string typeIdentifier = null)
            where TTo : TFrom;

        void RegisterDependency<TFrom, TTo>(
            string name,
            Type typeToBeInjected,
            LifetimeCycle lifetimeCycle,
            string typeIdentifier = null) where TTo : TFrom;

        void RegisterDependency<TFrom, TTo, TToBeInjected>(
            string name,
            LifetimeCycle lifetimeCycle,
            string typeIdentifier = null)
            where TTo : TFrom;

        TService Resolve<TService>(string key, KeyValuePair<string, object> parameter);

        TService Resolve<TService>();
    }
}
