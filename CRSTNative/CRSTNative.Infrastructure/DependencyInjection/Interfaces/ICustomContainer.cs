using System;
using System.Collections.Generic;

namespace CRSTNative.Infrastructure.DependencyInjection.Interfaces
{
    /// <summary>
    /// Declares logic that container should implement
    /// </summary>
    public interface ICustomContainer : IDisposable
    {

        /// <summary>
        /// Registers TInterface dependency as singleton
        /// </summary>
        /// <typeparam name="TInterface">interface type</typeparam>
        /// <param name="instance">Instance</param>
        void RegisterInstance<TInterface>(TInterface instance);

        /// <summary>
        /// Registers TInterface dependency with provided lifetime manager
        /// </summary>
        /// <typeparam name="TInterface">TInterface type</typeparam>
        /// <param name="instance">Instance</param>
        /// <param name="lifetimeCycle">Lifetime manager</param>
        void RegisterInstance<TInterface>(TInterface instance, LifetimeCycle lifetimeCycle);

        /// <summary>
        /// Registers instance of TInterface using unique name as singleton
        /// </summary>
        /// <typeparam name="TInterface">TInterface type</typeparam>
        /// <param name="name">Some name</param>
        /// <param name="instance">Instance</param>
        void RegisterInstance<TInterface>(string name, TInterface instance);

        /// <summary>
        /// Registers instance of TInterface using unique name with provided lifetime cycle manager
        /// </summary>
        /// <typeparam name="TInterface">TInterface type</typeparam>
        /// <param name="name">Some name</param>
        /// <param name="instance">Instance</param>
        void RegisterInstance<TInterface>(string name, TInterface instance, LifetimeCycle lifetimeCycle);

        /// <summary>
        /// Registers dependency where TTo derived from TFrom 
        /// </summary>
        /// <typeparam name="TFrom">TFrom type</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        void RegisterDependency<TFrom, TTo>() where TTo : TFrom;

        /// <summary>
        /// Registers type using name
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <param name="name">Name as key</param>
        void RegisterDependency<TFrom, TTo>(string name) where TTo : TFrom;

        /// <summary>
        /// Registers dependency where TTo derived from TFrom with specified lifetime manager
        /// </summary>
        /// <typeparam name="TFrom">TFrom type</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        void RegisterDependency<TFrom, TTo>(LifetimeCycle lifetimeManager) where TTo : TFrom;

        /// <summary>
        /// Registers type using name with specified lifetime manager
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="lifetimeManager">Lifetime manager</param>
        void RegisterDependency<TFrom, TTo>(string name, LifetimeCycle lifetimeManager) where TTo : TFrom;

        /// <summary>
        /// Register dependency with specified constructor injection parameter
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="constructorInjectionParameters">Constructor injection parameter</param>
        void RegisterDependency<TFrom, TTo>(string name, KeyValuePair<string, Type> constructorInjectionParameters)
            where TTo : TFrom;

        /// <summary>
        /// Register dependency with specified constructor injection parameter
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="constructorInjectionParameters">Constructor injection parameter</param>
        /// <param name="lifetimeCycle">Lifetime cycle to be applied</param>
        void RegisterDependency<TFrom, TTo>(
            string name,
            KeyValuePair<string, Type> constructorInjectionParameters,
            LifetimeCycle lifetimeCycle) where TTo : TFrom;

        /// <summary>
        /// Register dependency with specified constructor injection parameters
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="typeToBeInjected">Type to be resolved into constructor</param>
        /// <param name="typeIdentifier">Identifier of type to be injected, in case null - uses name parameter</param>
        void RegisterDependency<TFrom, TTo>(string name, Type typeToBeInjected, string typeIdentifier = null)
            where TTo : TFrom;

        /// <summary>
        /// Register dependency with specified constructor injection parameters
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <typeparam name="TToBeInjected">Type to be resolved into constructor</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="typeIdentifier">Identifier of type to be injected, in case null - uses name parameter</param>
        void RegisterDependency<TFrom, TTo, TToBeInjected>(string name, string typeIdentifier = null)
            where TTo : TFrom;

        /// <summary>
        /// Register dependency with specified constructor injection parameters
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="typeToBeInjected">Type to be resolved into constructor</param>
        /// <param name="lifetimeCycle">Lifetime cycle to applied</param>
        /// <param name="typeIdentifier">Identifier of type to be injected, in case null - uses name parameter</param>
        void RegisterDependency<TFrom, TTo>(
            string name,
            Type typeToBeInjected,
            LifetimeCycle lifetimeCycle,
            string typeIdentifier = null) where TTo : TFrom;

        /// <summary>
        /// Register dependency with specified constructor injection parameters
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <typeparam name="TToBeInjected">Type to be resolved into constructor</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="lifetimeCycle">Lifetime cycle to applied</param>
        /// <param name="typeIdentifier">Identifier of type to be injected, in case null - uses name parameter</param>
        void RegisterDependency<TFrom, TTo, TToBeInjected>(
            string name,
            LifetimeCycle lifetimeCycle,
            string typeIdentifier = null)
            where TTo : TFrom;

        /// <summary>
        /// Resolve dependency with specified constructor parameter
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="parameter"></param>
        /// <returns>Instance of the Resolved Type</returns>
        TService Resolve<TService>(string key, KeyValuePair<string, object> parameter);

        /// <summary>
        /// Resolve dependency with specified constructor parameter
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>Instance of the Resolved Type</returns>
        TService Resolve<TService>();
    }
}
