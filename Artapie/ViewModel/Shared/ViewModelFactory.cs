namespace ViewModel.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Dal;

    using DalContract.Models;

    public static class ViewModelFactory
    {
        private static readonly IEnumerable<Type> allViewModelTypes;

        static ViewModelFactory()
        {
            allViewModelTypes = GetAllEntityViewModelTypes();
        }

        public static BaseEntityViewModel<T> Create<T>(T entity, ModelContext context) where T : class, IModel, new()
        {
            var type = allViewModelTypes.FirstOrDefault(t => t.IsSubclassOfGenericType(typeof(BaseEntityViewModel<>), typeof(T)));

            var constructor = type?.GetConstructor(new[] { typeof(T), typeof(ModelContext) });

            return constructor?.Invoke(new object[] { entity, context }) as BaseEntityViewModel<T>;
        }

        private static IEnumerable<Type> GetAllEntityViewModelTypes()
        {
            var allTypes = Assembly.GetExecutingAssembly().ExportedTypes;

            var result = new List<Type>();

            foreach (var type in allTypes)
            {
                if (type.IsSubclassOfGenericType(typeof(BaseEntityViewModel<>)))
                {
                    result.Add(type);
                }
            }

            return result;
        }
    }
}
