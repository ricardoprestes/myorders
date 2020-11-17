﻿using System;
using System.Collections.Generic;

namespace MyOrders.Helpers
{
    public sealed class ServiceLocator
    {
        static readonly Lazy<ServiceLocator> instance = new Lazy<ServiceLocator>(() => new ServiceLocator());
        readonly Dictionary<Type, Lazy<object>> registeredServices = new Dictionary<Type, Lazy<object>>();

        public static ServiceLocator Instance => instance.Value;

        public void Register<TContract, TService>() where TService : new()
        {
            if (!registeredServices.ContainsKey(typeof(TContract)))
                registeredServices[typeof(TContract)] =
                    new Lazy<object>(() => Activator.CreateInstance(typeof(TService)));
        }

        public T Get<T>() where T : class
        {
            if (registeredServices.TryGetValue(typeof(T), out Lazy<object> service))
            {
                return (T)service.Value;
            }

            return null;
        }
    }
}
