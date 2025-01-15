using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x0200014C RID: 332
	internal class AssociationSetDiscoveryConvention : INavigationSourceConvention, IConvention
	{
		// Token: 0x06000C40 RID: 3136 RVA: 0x0002FD94 File Offset: 0x0002DF94
		public void Apply(NavigationSourceConfiguration configuration, ODataModelBuilder model)
		{
			IList<Tuple<StructuralTypeConfiguration, IList<MemberInfo>, NavigationPropertyConfiguration>> list = new List<Tuple<StructuralTypeConfiguration, IList<MemberInfo>, NavigationPropertyConfiguration>>();
			Stack<MemberInfo> stack = new Stack<MemberInfo>();
			model.FindAllNavigationProperties(configuration.EntityType, list, stack);
			foreach (Tuple<StructuralTypeConfiguration, IList<MemberInfo>, NavigationPropertyConfiguration> tuple in list)
			{
				NavigationSourceConfiguration targetNavigationSource = AssociationSetDiscoveryConvention.GetTargetNavigationSource(tuple.Item3, model);
				if (targetNavigationSource != null)
				{
					configuration.AddBinding(tuple.Item3, targetNavigationSource, tuple.Item2);
				}
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002FE18 File Offset: 0x0002E018
		internal static NavigationSourceConfiguration GetTargetNavigationSource(NavigationPropertyConfiguration navigationProperty, ODataModelBuilder model)
		{
			EntityTypeConfiguration entityTypeConfiguration = model.StructuralTypes.OfType<EntityTypeConfiguration>().SingleOrDefault((EntityTypeConfiguration e) => e.ClrType == navigationProperty.RelatedClrType);
			if (entityTypeConfiguration == null)
			{
				throw Error.InvalidOperation(SRResources.TargetEntityTypeMissing, new object[]
				{
					navigationProperty.Name,
					TypeHelper.GetReflectedType(navigationProperty.PropertyInfo).FullName
				});
			}
			bool flag = navigationProperty.PropertyInfo.GetCustomAttributes<SingletonAttribute>().Any<SingletonAttribute>();
			return AssociationSetDiscoveryConvention.GetDefaultNavigationSource(entityTypeConfiguration, model, flag);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002FEA8 File Offset: 0x0002E0A8
		private static NavigationSourceConfiguration GetDefaultNavigationSource(EntityTypeConfiguration targetEntityType, ODataModelBuilder model, bool isSingleton)
		{
			if (targetEntityType == null)
			{
				return null;
			}
			NavigationSourceConfiguration[] array2;
			if (isSingleton)
			{
				NavigationSourceConfiguration[] array = model.Singletons.Where((SingletonConfiguration e) => e.EntityType == targetEntityType).ToArray<SingletonConfiguration>();
				array2 = array;
			}
			else
			{
				NavigationSourceConfiguration[] array = model.EntitySets.Where((EntitySetConfiguration e) => e.EntityType == targetEntityType).ToArray<EntitySetConfiguration>();
				array2 = array;
			}
			if (array2.Length > 1)
			{
				if (model.BindingOptions == NavigationPropertyBindingOption.Auto)
				{
					return array2[0];
				}
				return null;
			}
			else
			{
				if (array2.Length == 1)
				{
					return array2[0];
				}
				return AssociationSetDiscoveryConvention.GetDefaultNavigationSource(targetEntityType.BaseType, model, isSingleton);
			}
		}
	}
}
