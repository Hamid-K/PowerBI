using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200012F RID: 303
	internal static class EdmTypeConfigurationExtensions
	{
		// Token: 0x06000A6D RID: 2669 RVA: 0x0002AAF6 File Offset: 0x00028CF6
		public static IEnumerable<PropertyConfiguration> DerivedProperties(this StructuralTypeConfiguration structuralType)
		{
			if (structuralType == null)
			{
				throw Error.ArgumentNull("structuralType");
			}
			if (structuralType.Kind == EdmTypeKind.Entity)
			{
				return ((EntityTypeConfiguration)structuralType).DerivedProperties();
			}
			if (structuralType.Kind == EdmTypeKind.Complex)
			{
				return ((ComplexTypeConfiguration)structuralType).DerivedProperties();
			}
			return Enumerable.Empty<PropertyConfiguration>();
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002AB35 File Offset: 0x00028D35
		public static IEnumerable<PropertyConfiguration> DerivedProperties(this EntityTypeConfiguration entity)
		{
			if (entity == null)
			{
				throw Error.ArgumentNull("entity");
			}
			for (EntityTypeConfiguration baseType = entity.BaseType; baseType != null; baseType = baseType.BaseType)
			{
				foreach (PropertyConfiguration propertyConfiguration in baseType.Properties)
				{
					yield return propertyConfiguration;
				}
				IEnumerator<PropertyConfiguration> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002AB45 File Offset: 0x00028D45
		public static IEnumerable<PropertyConfiguration> DerivedProperties(this ComplexTypeConfiguration complex)
		{
			if (complex == null)
			{
				throw Error.ArgumentNull("complex");
			}
			for (ComplexTypeConfiguration baseType = complex.BaseType; baseType != null; baseType = baseType.BaseType)
			{
				foreach (PropertyConfiguration propertyConfiguration in baseType.Properties)
				{
					yield return propertyConfiguration;
				}
				IEnumerator<PropertyConfiguration> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002AB58 File Offset: 0x00028D58
		public static IEnumerable<PropertyConfiguration> Keys(this EntityTypeConfiguration entity)
		{
			if (entity.Keys.Any<PrimitivePropertyConfiguration>() || entity.EnumKeys.Any<EnumPropertyConfiguration>())
			{
				return entity.Keys.OfType<PropertyConfiguration>().Concat(entity.EnumKeys);
			}
			if (entity.BaseType == null)
			{
				return Enumerable.Empty<PropertyConfiguration>();
			}
			return entity.BaseType.Keys();
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002ABAF File Offset: 0x00028DAF
		public static IEnumerable<StructuralTypeConfiguration> ThisAndBaseTypes(this StructuralTypeConfiguration structuralType)
		{
			return structuralType.BaseTypes().Concat(new StructuralTypeConfiguration[] { structuralType });
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002ABC6 File Offset: 0x00028DC6
		public static IEnumerable<StructuralTypeConfiguration> ThisAndBaseAndDerivedTypes(this ODataModelBuilder modelBuilder, StructuralTypeConfiguration structuralType)
		{
			return structuralType.BaseTypes().Concat(new StructuralTypeConfiguration[] { structuralType }).Concat(modelBuilder.DerivedTypes(structuralType));
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002ABE9 File Offset: 0x00028DE9
		public static IEnumerable<StructuralTypeConfiguration> BaseTypes(this StructuralTypeConfiguration structuralType)
		{
			if (structuralType.Kind == EdmTypeKind.Entity)
			{
				EntityTypeConfiguration entity = (EntityTypeConfiguration)structuralType;
				for (entity = entity.BaseType; entity != null; entity = entity.BaseType)
				{
					yield return entity;
				}
				entity = null;
			}
			if (structuralType.Kind == EdmTypeKind.Complex)
			{
				ComplexTypeConfiguration complex = (ComplexTypeConfiguration)structuralType;
				for (complex = complex.BaseType; complex != null; complex = complex.BaseType)
				{
					yield return complex;
				}
				complex = null;
			}
			yield break;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002ABFC File Offset: 0x00028DFC
		public static IEnumerable<StructuralTypeConfiguration> DerivedTypes(this ODataModelBuilder modelBuilder, StructuralTypeConfiguration structuralType)
		{
			if (modelBuilder == null)
			{
				throw Error.ArgumentNull("modelBuilder");
			}
			if (structuralType == null)
			{
				throw Error.ArgumentNull("structuralType");
			}
			if (structuralType.Kind == EdmTypeKind.Entity)
			{
				return modelBuilder.DerivedTypes((EntityTypeConfiguration)structuralType);
			}
			if (structuralType.Kind == EdmTypeKind.Complex)
			{
				return modelBuilder.DerivedTypes((ComplexTypeConfiguration)structuralType);
			}
			return Enumerable.Empty<StructuralTypeConfiguration>();
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002AC56 File Offset: 0x00028E56
		public static IEnumerable<EntityTypeConfiguration> DerivedTypes(this ODataModelBuilder modelBuilder, EntityTypeConfiguration entity)
		{
			if (modelBuilder == null)
			{
				throw Error.ArgumentNull("modelBuilder");
			}
			if (entity == null)
			{
				throw Error.ArgumentNull("entity");
			}
			IEnumerable<EntityTypeConfiguration> enumerable = from e in modelBuilder.StructuralTypes.OfType<EntityTypeConfiguration>()
				where e.BaseType == entity
				select e;
			foreach (EntityTypeConfiguration derivedType in enumerable)
			{
				yield return derivedType;
				foreach (EntityTypeConfiguration entityTypeConfiguration in modelBuilder.DerivedTypes(derivedType))
				{
					yield return entityTypeConfiguration;
				}
				IEnumerator<EntityTypeConfiguration> enumerator2 = null;
				derivedType = null;
			}
			IEnumerator<EntityTypeConfiguration> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002AC6D File Offset: 0x00028E6D
		public static IEnumerable<ComplexTypeConfiguration> DerivedTypes(this ODataModelBuilder modelBuilder, ComplexTypeConfiguration complex)
		{
			if (modelBuilder == null)
			{
				throw Error.ArgumentNull("modelBuilder");
			}
			if (complex == null)
			{
				throw Error.ArgumentNull("complex");
			}
			IEnumerable<ComplexTypeConfiguration> enumerable = from e in modelBuilder.StructuralTypes.OfType<ComplexTypeConfiguration>()
				where e.BaseType == complex
				select e;
			foreach (ComplexTypeConfiguration derivedType in enumerable)
			{
				yield return derivedType;
				foreach (ComplexTypeConfiguration complexTypeConfiguration in modelBuilder.DerivedTypes(derivedType))
				{
					yield return complexTypeConfiguration;
				}
				IEnumerator<ComplexTypeConfiguration> enumerator2 = null;
				derivedType = null;
			}
			IEnumerator<ComplexTypeConfiguration> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002AC84 File Offset: 0x00028E84
		public static bool IsAssignableFrom(this StructuralTypeConfiguration baseStructuralType, StructuralTypeConfiguration structuralType)
		{
			if (structuralType.Kind == EdmTypeKind.Entity && baseStructuralType.Kind == EdmTypeKind.Entity)
			{
				for (EntityTypeConfiguration entityTypeConfiguration = (EntityTypeConfiguration)structuralType; entityTypeConfiguration != null; entityTypeConfiguration = entityTypeConfiguration.BaseType)
				{
					if (baseStructuralType == entityTypeConfiguration)
					{
						return true;
					}
				}
			}
			else if (structuralType.Kind == EdmTypeKind.Complex && baseStructuralType.Kind == EdmTypeKind.Complex)
			{
				for (ComplexTypeConfiguration complexTypeConfiguration = (ComplexTypeConfiguration)structuralType; complexTypeConfiguration != null; complexTypeConfiguration = complexTypeConfiguration.BaseType)
				{
					if (baseStructuralType == complexTypeConfiguration)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
