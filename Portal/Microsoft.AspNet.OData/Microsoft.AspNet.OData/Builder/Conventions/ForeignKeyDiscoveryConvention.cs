using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x02000147 RID: 327
	internal class ForeignKeyDiscoveryConvention : IEdmPropertyConvention<NavigationPropertyConfiguration>, IEdmPropertyConvention, IConvention
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x0002F87C File Offset: 0x0002DA7C
		public void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = edmProperty as NavigationPropertyConfiguration;
			if (navigationPropertyConfiguration != null)
			{
				this.Apply(navigationPropertyConfiguration, structuralTypeConfiguration, model);
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002F8AC File Offset: 0x0002DAAC
		public void Apply(NavigationPropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (structuralTypeConfiguration == null)
			{
				throw Error.ArgumentNull("structuralTypeConfiguration");
			}
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			EntityTypeConfiguration entityTypeConfiguration = model.StructuralTypes.OfType<EntityTypeConfiguration>().FirstOrDefault((EntityTypeConfiguration e) => e.ClrType == edmProperty.RelatedClrType);
			if (entityTypeConfiguration == null)
			{
				return;
			}
			if (edmProperty.DependentProperties.Any<PropertyInfo>() || edmProperty.Multiplicity == EdmMultiplicity.Many)
			{
				return;
			}
			EntityTypeConfiguration entityTypeConfiguration2 = structuralTypeConfiguration as EntityTypeConfiguration;
			if (entityTypeConfiguration2 == null)
			{
				return;
			}
			IDictionary<PrimitivePropertyConfiguration, PrimitivePropertyConfiguration> foreignKeys = ForeignKeyDiscoveryConvention.GetForeignKeys(entityTypeConfiguration, entityTypeConfiguration2);
			if (foreignKeys.Any<KeyValuePair<PrimitivePropertyConfiguration, PrimitivePropertyConfiguration>>() && foreignKeys.Count<KeyValuePair<PrimitivePropertyConfiguration, PrimitivePropertyConfiguration>>() == entityTypeConfiguration.Keys.Count<PrimitivePropertyConfiguration>())
			{
				foreach (KeyValuePair<PrimitivePropertyConfiguration, PrimitivePropertyConfiguration> keyValuePair in foreignKeys)
				{
					edmProperty.HasConstraint(keyValuePair.Key.PropertyInfo, keyValuePair.Value.PropertyInfo);
				}
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002F9C4 File Offset: 0x0002DBC4
		private static IDictionary<PrimitivePropertyConfiguration, PrimitivePropertyConfiguration> GetForeignKeys(EntityTypeConfiguration principalEntityType, EntityTypeConfiguration dependentEntityType)
		{
			IDictionary<PrimitivePropertyConfiguration, PrimitivePropertyConfiguration> dictionary = new Dictionary<PrimitivePropertyConfiguration, PrimitivePropertyConfiguration>();
			foreach (PrimitivePropertyConfiguration primitivePropertyConfiguration in principalEntityType.Keys)
			{
				foreach (PrimitivePropertyConfiguration primitivePropertyConfiguration2 in dependentEntityType.Properties.OfType<PrimitivePropertyConfiguration>())
				{
					if ((Nullable.GetUnderlyingType(primitivePropertyConfiguration2.PropertyInfo.PropertyType) ?? primitivePropertyConfiguration2.PropertyInfo.PropertyType) == primitivePropertyConfiguration.PropertyInfo.PropertyType)
					{
						if (string.Equals(primitivePropertyConfiguration2.Name, principalEntityType.Name + primitivePropertyConfiguration.Name, StringComparison.Ordinal))
						{
							dictionary.Add(primitivePropertyConfiguration2, primitivePropertyConfiguration);
						}
						else if (string.Equals(primitivePropertyConfiguration2.Name, primitivePropertyConfiguration.Name, StringComparison.Ordinal) && string.Equals(primitivePropertyConfiguration.Name, principalEntityType.Name + "Id", StringComparison.OrdinalIgnoreCase))
						{
							dictionary.Add(primitivePropertyConfiguration2, primitivePropertyConfiguration);
						}
					}
				}
			}
			return dictionary;
		}
	}
}
