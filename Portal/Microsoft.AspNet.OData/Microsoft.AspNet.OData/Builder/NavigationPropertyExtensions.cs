using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000100 RID: 256
	internal static class NavigationPropertyExtensions
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x00025ABE File Offset: 0x00023CBE
		public static void FindAllNavigationProperties(this ODataModelBuilder builder, StructuralTypeConfiguration configuration, IList<Tuple<StructuralTypeConfiguration, IList<MemberInfo>, NavigationPropertyConfiguration>> navigations, Stack<MemberInfo> path)
		{
			builder.FindAllNavigationPropertiesRecursive(configuration, navigations, path, new HashSet<Type>());
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00025AD0 File Offset: 0x00023CD0
		private static void FindAllNavigationPropertiesRecursive(this ODataModelBuilder builder, StructuralTypeConfiguration configuration, IList<Tuple<StructuralTypeConfiguration, IList<MemberInfo>, NavigationPropertyConfiguration>> navigations, Stack<MemberInfo> path, HashSet<Type> typesAlreadyProcessed)
		{
			if (builder == null)
			{
				throw Error.ArgumentNull("builder");
			}
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (navigations == null)
			{
				throw Error.ArgumentNull("navigations");
			}
			if (path == null)
			{
				throw Error.ArgumentNull("path");
			}
			foreach (StructuralTypeConfiguration structuralTypeConfiguration in configuration.ThisAndBaseTypes())
			{
				builder.FindNavigationProperties(structuralTypeConfiguration, navigations, path, typesAlreadyProcessed);
			}
			using (IEnumerator<StructuralTypeConfiguration> enumerator = builder.DerivedTypes(configuration).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StructuralTypeConfiguration config = enumerator.Current;
					if (!path.OfType<Type>().Any((Type p) => p == config.ClrType))
					{
						path.Push(TypeHelper.AsMemberInfo(config.ClrType));
						builder.FindNavigationProperties(config, navigations, path, typesAlreadyProcessed);
						path.Pop();
					}
				}
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00025BE0 File Offset: 0x00023DE0
		private static void FindNavigationProperties(this ODataModelBuilder builder, StructuralTypeConfiguration configuration, IList<Tuple<StructuralTypeConfiguration, IList<MemberInfo>, NavigationPropertyConfiguration>> navs, Stack<MemberInfo> path, HashSet<Type> typesAlreadyProcessed)
		{
			foreach (PropertyConfiguration propertyConfiguration in configuration.Properties)
			{
				path.Push(propertyConfiguration.PropertyInfo);
				NavigationPropertyConfiguration navigationPropertyConfiguration = propertyConfiguration as NavigationPropertyConfiguration;
				ComplexPropertyConfiguration complexPropertyConfiguration = propertyConfiguration as ComplexPropertyConfiguration;
				CollectionPropertyConfiguration collectionPropertyConfiguration = propertyConfiguration as CollectionPropertyConfiguration;
				if (navigationPropertyConfiguration != null)
				{
					IList<MemberInfo> list = path.Reverse<MemberInfo>().ToList<MemberInfo>();
					navs.Add(new Tuple<StructuralTypeConfiguration, IList<MemberInfo>, NavigationPropertyConfiguration>(configuration, list, navigationPropertyConfiguration));
				}
				else if (complexPropertyConfiguration != null && !typesAlreadyProcessed.Contains(complexPropertyConfiguration.RelatedClrType))
				{
					StructuralTypeConfiguration structuralTypeConfiguration = builder.GetTypeConfigurationOrNull(complexPropertyConfiguration.RelatedClrType) as StructuralTypeConfiguration;
					typesAlreadyProcessed.Add(complexPropertyConfiguration.RelatedClrType);
					builder.FindAllNavigationPropertiesRecursive(structuralTypeConfiguration, navs, path, typesAlreadyProcessed);
					typesAlreadyProcessed.Remove(complexPropertyConfiguration.RelatedClrType);
				}
				else if (collectionPropertyConfiguration != null && !typesAlreadyProcessed.Contains(collectionPropertyConfiguration.ElementType))
				{
					IEdmTypeConfiguration typeConfigurationOrNull = builder.GetTypeConfigurationOrNull(collectionPropertyConfiguration.ElementType);
					if (typeConfigurationOrNull != null && typeConfigurationOrNull.Kind == EdmTypeKind.Complex)
					{
						StructuralTypeConfiguration structuralTypeConfiguration2 = (StructuralTypeConfiguration)typeConfigurationOrNull;
						typesAlreadyProcessed.Add(collectionPropertyConfiguration.ElementType);
						builder.FindAllNavigationPropertiesRecursive(structuralTypeConfiguration2, navs, path, typesAlreadyProcessed);
						typesAlreadyProcessed.Remove(collectionPropertyConfiguration.ElementType);
					}
				}
				path.Pop();
			}
		}
	}
}
