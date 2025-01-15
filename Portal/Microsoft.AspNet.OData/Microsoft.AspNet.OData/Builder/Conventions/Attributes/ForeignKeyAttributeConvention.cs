using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200016D RID: 365
	internal class ForeignKeyAttributeConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C8A RID: 3210 RVA: 0x0003168C File Offset: 0x0002F88C
		public ForeignKeyAttributeConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(ForeignKeyAttribute), false)
		{
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000316B4 File Offset: 0x0002F8B4
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (structuralTypeConfiguration == null)
			{
				throw Error.ArgumentNull("structuralTypeConfiguration");
			}
			if (attribute == null)
			{
				throw Error.ArgumentNull("attribute");
			}
			EntityTypeConfiguration entityTypeConfiguration = structuralTypeConfiguration as EntityTypeConfiguration;
			if (entityTypeConfiguration == null)
			{
				return;
			}
			ForeignKeyAttribute foreignKeyAttribute = (ForeignKeyAttribute)attribute;
			PropertyKind kind = edmProperty.Kind;
			if (kind != PropertyKind.Primitive)
			{
				if (kind == PropertyKind.Navigation)
				{
					ForeignKeyAttributeConvention.ApplyNavigation((NavigationPropertyConfiguration)edmProperty, entityTypeConfiguration, foreignKeyAttribute);
					return;
				}
			}
			else
			{
				ForeignKeyAttributeConvention.ApplyPrimitive((PrimitivePropertyConfiguration)edmProperty, entityTypeConfiguration, foreignKeyAttribute);
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00031728 File Offset: 0x0002F928
		private static void ApplyNavigation(NavigationPropertyConfiguration navProperty, EntityTypeConfiguration entityType, ForeignKeyAttribute foreignKeyAttribute)
		{
			if (navProperty.AddedExplicitly || navProperty.Multiplicity == EdmMultiplicity.Many)
			{
				return;
			}
			EntityTypeConfiguration entityTypeConfiguration = entityType.ModelBuilder.StructuralTypes.OfType<EntityTypeConfiguration>().FirstOrDefault((EntityTypeConfiguration e) => e.ClrType == navProperty.RelatedClrType);
			if (entityTypeConfiguration == null)
			{
				return;
			}
			using (IEnumerator<string> enumerator = (from p in foreignKeyAttribute.Name.Split(new char[] { ',' })
				select p.Trim()).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string dependentPropertyName = enumerator.Current;
					if (!string.IsNullOrWhiteSpace(dependentPropertyName))
					{
						PrimitivePropertyConfiguration primitivePropertyConfiguration = entityType.Properties.OfType<PrimitivePropertyConfiguration>().SingleOrDefault((PrimitivePropertyConfiguration p) => p.Name.Equals(dependentPropertyName, StringComparison.Ordinal));
						if (primitivePropertyConfiguration != null)
						{
							Type dependentType = Nullable.GetUnderlyingType(primitivePropertyConfiguration.PropertyInfo.PropertyType) ?? primitivePropertyConfiguration.PropertyInfo.PropertyType;
							PrimitivePropertyConfiguration primitivePropertyConfiguration2 = entityTypeConfiguration.Keys.FirstOrDefault((PrimitivePropertyConfiguration k) => k.PropertyInfo.PropertyType == dependentType && navProperty.PrincipalProperties.All((PropertyInfo p) => p != k.PropertyInfo));
							if (primitivePropertyConfiguration2 != null)
							{
								navProperty.HasConstraint(primitivePropertyConfiguration.PropertyInfo, primitivePropertyConfiguration2.PropertyInfo);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x000318B8 File Offset: 0x0002FAB8
		private static void ApplyPrimitive(PrimitivePropertyConfiguration dependent, EntityTypeConfiguration entityType, ForeignKeyAttribute foreignKeyAttribute)
		{
			string navName = foreignKeyAttribute.Name.Trim();
			NavigationPropertyConfiguration navProperty = entityType.NavigationProperties.FirstOrDefault((NavigationPropertyConfiguration n) => n.Name.Equals(navName, StringComparison.Ordinal));
			if (navProperty == null)
			{
				return;
			}
			if (navProperty.Multiplicity == EdmMultiplicity.Many || navProperty.AddedExplicitly)
			{
				return;
			}
			EntityTypeConfiguration entityTypeConfiguration = entityType.ModelBuilder.StructuralTypes.OfType<EntityTypeConfiguration>().FirstOrDefault((EntityTypeConfiguration e) => e.ClrType == navProperty.RelatedClrType);
			if (entityTypeConfiguration == null)
			{
				return;
			}
			Type dependentType = Nullable.GetUnderlyingType(dependent.PropertyInfo.PropertyType) ?? dependent.PropertyInfo.PropertyType;
			PrimitivePropertyConfiguration primitivePropertyConfiguration = entityTypeConfiguration.Keys.FirstOrDefault((PrimitivePropertyConfiguration k) => k.PropertyInfo.PropertyType == dependentType && navProperty.PrincipalProperties.All((PropertyInfo p) => p != k.PropertyInfo));
			if (primitivePropertyConfiguration != null)
			{
				navProperty.HasConstraint(dependent.PropertyInfo, primitivePropertyConfiguration.PropertyInfo);
			}
		}
	}
}
