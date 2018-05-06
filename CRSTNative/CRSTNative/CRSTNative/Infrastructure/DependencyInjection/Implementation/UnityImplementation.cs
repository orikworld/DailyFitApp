﻿using System;
using System.Collections.Generic;
using DailyFitNative.Infrastructure.DependencyInjection.Interfaces;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;
using Unity.ServiceLocation;

namespace DailyFitNative.Infrastructure.DependencyInjection.Implementation
{
    internal class UnityImplementation : IDependencyContainerProvider
    {
        #region Private fields

        private readonly UnityContainer _container;

        private readonly UnityServiceLocator _serviceLocator;

        #endregion

        #region Constructors

        public UnityImplementation()
        {
            _container = new UnityContainer();
            _serviceLocator = new UnityServiceLocator(_container);
        }

        #endregion

        #region IServiceLocator Implementation

        public object GetService(Type serviceType)
        {
            return _serviceLocator.GetService(serviceType);
        }

        public object GetInstance(Type instanceType)
        {
            return _serviceLocator.GetInstance(instanceType);
        }

        public object GetInstance(Type instanceType, string key)
        {
            return _serviceLocator.GetInstance(instanceType, key);
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _serviceLocator.GetAllInstances(serviceType);
        }

        public TService GetInstance<TService>()
        {
            return _serviceLocator.GetInstance<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            return _serviceLocator.GetInstance<TService>(key);
        }

        public TService Resolve<TService>()
        {
            return _container.Resolve<TService>();
        }

        public TService Resolve<TService>(string key, KeyValuePair<string, object> parameter)
        {
            return _container.Resolve<TService>(key, new ParameterOverride(parameter.Key, parameter.Value));
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _serviceLocator.GetAllInstances<TService>();
        }

        #endregion

        #region ICustomContainer Implementation

        public void RegisterInstance<TInterface>(TInterface instance)
        {
            _container.RegisterInstance(instance);
        }

        public void RegisterInstance<TInterface>(TInterface instance, LifetimeCycle lifetimeCycle)
        {
            _container.RegisterInstance(instance, GetLifeTimeManager(lifetimeCycle));
        }

        public void RegisterInstance<TInterface>(string name, TInterface instance)
        {
            _container.RegisterInstance(name, instance);
        }

        public void RegisterInstance<TInterface>(string name, TInterface instance, LifetimeCycle lifetimeCycle)
        {
            _container.RegisterInstance(name, instance, GetLifeTimeManager(lifetimeCycle));
        }

        public void RegisterDependency<TFrom, TTo>() where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>();
        }

        public void RegisterDependency<TFrom, TTo>(string name) where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(name);
        }

        public void RegisterDependency<TFrom, TTo>(LifetimeCycle lifetimeCycle) where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(GetLifeTimeManager(lifetimeCycle));
        }

        public void RegisterDependency<TFrom, TTo>(string name, LifetimeCycle lifetimeCycle) where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(name, GetLifeTimeManager(lifetimeCycle));
        }

        public void RegisterDependency<TFrom, TTo>(string name, KeyValuePair<string, Type> constructorInjectionParameters) where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(name,
                new InjectionConstructor(new ResolvedParameter(
                    constructorInjectionParameters.Value,
                    constructorInjectionParameters.Key)));
        }

        public void RegisterDependency<TFrom, TTo>(
            string name,
            KeyValuePair<string, Type> constructorInjectionParameters,
            LifetimeCycle lifetimeCycle) where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(
                name,
                GetLifeTimeManager(lifetimeCycle),
                new InjectionConstructor(new ResolvedParameter(
                    constructorInjectionParameters.Value,
                    constructorInjectionParameters.Key)));
        }

        public void RegisterDependency<TFrom, TTo>(string name, Type typeToBeInjected, string typeIdentifier = null) where TTo : TFrom
        {
            RegisterDependency<TFrom, TTo>(name, new KeyValuePair<string, Type>(typeIdentifier ?? name, typeToBeInjected));
        }

        public void RegisterDependency<TFrom, TTo, TToBeInjected>(string name, string typeIdentifier = null) where TTo : TFrom
        {
            RegisterDependency<TFrom, TTo, TToBeInjected>(
                name,
                LifetimeCycle.NewInstance,
                typeIdentifier);
        }

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

        public void RegisterDependency<TFrom, TTo, TToBeInjected>(
            string name,
            LifetimeCycle lifetimeCycle,
            string typeIdentifier = null) where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(
                name,
                GetLifeTimeManager(lifetimeCycle),
                new InjectionConstructor(new ResolvedParameter<TToBeInjected>(typeIdentifier ?? name)));
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _serviceLocator.Dispose();
            _container.Dispose();
        }

        #endregion

        #region Private Methods

        private LifetimeManager GetLifeTimeManager(LifetimeCycle lifetimeCycle)
        {
            LifetimeManager lifetimeManager = null;

            switch (lifetimeCycle)
            {
                case LifetimeCycle.NewInstance:
                    {
                        lifetimeManager = new TransientLifetimeManager();
                        break;
                    }
                case LifetimeCycle.SingletonInstance:
                    {
                        lifetimeManager = new ContainerControlledLifetimeManager();
                        break;
                    }
                case LifetimeCycle.PerThreadInstance:
                    {
                        lifetimeManager = new PerThreadLifetimeManager();
                        break;
                    }
            }

            return lifetimeManager;
        }

        #endregion
    }
}
