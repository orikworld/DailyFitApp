using CRSTNative.Infrastructure.DependencyInjection.Interfaces;
using System;
using System.Collections.Generic;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;
using Unity.ServiceLocation;

namespace CRSTNative.Infrastructure.DependencyInjection.Implementation
{
    public class UnityImplementation : IDependencyContainerProvider
    {
        #region Private Fields

        /// <summary>
        /// The container
        /// </summary>
        private readonly UnityContainer _container;

        /// <summary>
        /// The service locator
        /// </summary>
        private readonly UnityServiceLocator _serviceLocator;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="UnityImplementation"/> class from being created.
        /// </summary>
        public UnityImplementation()
        {
            _container = new UnityContainer();
            _serviceLocator = new UnityServiceLocator(_container);
        }

        #endregion

        #region IServiceLocator Implementation

        /// <summary>
        /// Gets the service using type
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>Service object</returns>
        public object GetService(Type serviceType)
        {
            return _serviceLocator.GetService(serviceType);
        }

        /// <summary>
        /// Gets the instance using Type
        /// </summary>
        /// <param name="instanceType">Instance type</param>
        /// <returns>Instance</returns>
        public object GetInstance(Type instanceType)
        {
            return _serviceLocator.GetInstance(instanceType);
        }

        /// <summary>
        /// Gets the instance using instance type and specific key
        /// </summary>
        /// <param name="instanceType">Type of the instance</param>
        /// <param name="key">Specific key</param>
        /// <returns>Instance</returns>
        public object GetInstance(Type instanceType, string key)
        {
            return _serviceLocator.GetInstance(instanceType, key);
        }

        /// <summary>
        /// Gets all instances using type
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>All instances</returns>
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _serviceLocator.GetAllInstances(serviceType);
        }

        /// <summary>
        /// Gets instance using TSevice type
        /// </summary>
        /// <typeparam name="TService">Service type</typeparam>
        /// <returns>Instance</returns>
        public TService GetInstance<TService>()
        {
            return _serviceLocator.GetInstance<TService>();
        }

        /// <summary>
        /// Gets instance using TSevice type by specific key
        /// </summary>
        /// <typeparam name="TService">Service type</typeparam>
        /// <returns>Instance</returns>
        public TService GetInstance<TService>(string key)
        {
            return _serviceLocator.GetInstance<TService>(key);
        }

        /// <summary>
        /// Resolve dependency with specified constructor parameter
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>Instance of the Resolved Type</returns>
        public TService Resolve<TService>()
        {
            return _container.Resolve<TService>();
        }

        /// <summary>
        /// Resolve dependency with default constructor parameter
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="parameter"></param>
        /// <returns>Instance of the Resolved Type</returns>
        public TService Resolve<TService>(string key, KeyValuePair<string, object> parameter)
        {
            return _container.Resolve<TService>(key, new ParameterOverride(parameter.Key, parameter.Value));
        }

        /// <summary>
        /// Gets all instances using TService type
        /// </summary>
        /// <returns>All instances</returns>
        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _serviceLocator.GetAllInstances<TService>();
        }

        #endregion

        #region ICustomContainer Implementation

        /// <summary>
        /// Registers TInterface dependency as singleton
        /// </summary>
        /// <typeparam name="TInterface">interface type</typeparam>
        /// <param name="instance">Instance</param>
        public void RegisterInstance<TInterface>(TInterface instance)
        {
            _container.RegisterInstance(instance);
        }

        /// <summary>
        /// Registers TInterface dependency with provided lifetime manager
        /// </summary>
        /// <typeparam name="TInterface">TInterface type</typeparam>
        /// <param name="instance">Instance</param>
        /// <param name="lifetimeCycle">Lifetime manager</param>
        public void RegisterInstance<TInterface>(TInterface instance, LifetimeCycle lifetimeCycle)
        {
            _container.RegisterInstance(instance, GetLifetimeManager(lifetimeCycle));
        }

        /// <summary>
        /// Registers instance of TInterface using unique name as singleton
        /// </summary>
        /// <typeparam name="TInterface">TInterface type</typeparam>
        /// <param name="name">Some name</param>
        /// <param name="instance">Instance</param>
        public void RegisterInstance<TInterface>(string name, TInterface instance)
        {
            _container.RegisterInstance(name, instance);
        }

        /// <summary>
        /// Registers instance of TInterface using unique name with provided lifetime cycle manager
        /// </summary>
        /// <typeparam name="TInterface">TInterface type</typeparam>
        /// <param name="name">Some name</param>
        /// <param name="instance">Instance</param>
        public void RegisterInstance<TInterface>(string name, TInterface instance, LifetimeCycle lifetimeCycle)
        {
            _container.RegisterInstance(name, instance, GetLifetimeManager(lifetimeCycle));
        }

        /// <summary>
        /// Registers dependency where TTo derived from TFrom 
        /// </summary>
        /// <typeparam name="TFrom">TFrom type</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        public void RegisterDependency<TFrom, TTo>() where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>();
        }

        /// <summary>
        /// Registers type using name
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <param name="name">Name as key</param>
        public void RegisterDependency<TFrom, TTo>(string name) where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(name);
        }


        /// <summary>
        /// Registers dependency where TTo derived from TFrom with specified lifetime manager
        /// </summary>
        /// <typeparam name="TFrom">TFrom type</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        public void RegisterDependency<TFrom, TTo>(LifetimeCycle lifetimeCycle) where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(GetLifetimeManager(lifetimeCycle));
        }

        /// <summary>
        /// Registers type using name with specified lifetime manager
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="lifetimeCycle">Specific lifetime manager</param>
        public void RegisterDependency<TFrom, TTo>(string name, LifetimeCycle lifetimeCycle) where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(name, GetLifetimeManager(lifetimeCycle));
        }

        /// <summary>
        /// Register dependency with specified constructor injection parameter
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="constructorInjectionParameters">Constructor injection parameter</param>
        public void RegisterDependency<TFrom, TTo>(string name, KeyValuePair<string, Type> constructorInjectionParameters) where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(name,
                new InjectionConstructor(new ResolvedParameter(
                    constructorInjectionParameters.Value,
                    constructorInjectionParameters.Key)));
        }

        /// <summary>
        /// Register dependency with specified constructor injection parameter
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="constructorInjectionParameters">Constructor injection parameter</param>
        /// <param name="lifetimeCycle">Lifetime cycle to applied</param>
        public void RegisterDependency<TFrom, TTo>(
            string name,
            KeyValuePair<string, Type> constructorInjectionParameters,
            LifetimeCycle lifetimeCycle) where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(
                name,
                GetLifetimeManager(lifetimeCycle),
                new InjectionConstructor(new ResolvedParameter(
                    constructorInjectionParameters.Value,
                    constructorInjectionParameters.Key)));
        }

        /// <summary>
        /// Register dependency with specified constructor injection parameter
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="typeToBeInjected">Type to be resolved into constructor</param>
        /// <param name="typeIdentifier">Identifier of type to be injected, in case null - uses name parameter</param>
        public void RegisterDependency<TFrom, TTo>(string name, Type typeToBeInjected, string typeIdentifier = null) where TTo : TFrom
        {
            RegisterDependency<TFrom, TTo>(name, new KeyValuePair<string, Type>(typeIdentifier ?? name, typeToBeInjected));
        }

        /// <summary>
        /// Register dependency with specified constructor injection parameters
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <typeparam name="TToBeInjected">Type to be resolved into constructor</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="typeIdentifier">Identifier of type to be injected, in case null - uses name parameter</param>
        public void RegisterDependency<TFrom, TTo, TToBeInjected>(string name, string typeIdentifier = null) where TTo : TFrom
        {
            RegisterDependency<TFrom, TTo, TToBeInjected>(
                name,
                LifetimeCycle.NewInstance,
                typeIdentifier);
        }

        /// <summary>
        /// Register dependency with specified constructor injection parameters
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="typeToBeInjected">Type to be resolved into constructor</param>
        /// <param name="lifetimeCycle">Lifetime cycle to applied</param>
        /// <param name="typeIdentifier">Identifier of type to be injected, in case null - uses name parameter</param>
        public void RegisterDependency<TFrom, TTo>(
            string name,
            Type typeToBeInjected,
            LifetimeCycle lifetimeCycle,
            string typeIdentifier = null) where TTo : TFrom
        {
            RegisterDependency<TFrom, TTo>(
                name,
                new KeyValuePair<string, Type>(typeIdentifier ?? name, typeToBeInjected),
                lifetimeCycle);
        }

        /// <summary>
        /// Register dependency with specified constructor injection parameters
        /// </summary>
        /// <typeparam name="TFrom">TFrom</typeparam>
        /// <typeparam name="TTo">To type</typeparam>
        /// <typeparam name="TToBeInjected">Type to be resolved into constructor</typeparam>
        /// <param name="name">Name as key</param>
        /// <param name="lifetimeCycle">Lifetime cycle to applied</param>
        /// <param name="typeIdentifier">Identifier of type to be injected, in case null - uses name parameter</param>
        public void RegisterDependency<TFrom, TTo, TToBeInjected>(
            string name,
            LifetimeCycle lifetimeCycle,
            string typeIdentifier = null) where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(
                name,
                GetLifetimeManager(lifetimeCycle),
                new InjectionConstructor(new ResolvedParameter<TToBeInjected>(typeIdentifier ?? name)));
        }

        #endregion

        #region IDisposible

        /// <summary>
        ///  Disposes container
        /// </summary>
        public void Dispose()
        {
            _container.Dispose();
            _serviceLocator.Dispose();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the lifetime manager.
        /// </summary>
        /// <param name="lifetimeCycle">The lifetime cycle.</param>
        /// <returns></returns>
        private LifetimeManager GetLifetimeManager(LifetimeCycle lifetimeCycle)
        {
            LifetimeManager lifetimeManager = null;

            switch(lifetimeCycle)
            {
                case LifetimeCycle.NewInstance:
                    lifetimeManager = new TransientLifetimeManager();
                    break;
                case LifetimeCycle.SingletonInstance:
                    lifetimeManager = new ContainerControlledLifetimeManager();
                    break;
                case LifetimeCycle.PerThreadInstance:
                    lifetimeManager = new PerThreadLifetimeManager();
                    break;
            }

            return lifetimeManager;
        }

        #endregion
    }
}
