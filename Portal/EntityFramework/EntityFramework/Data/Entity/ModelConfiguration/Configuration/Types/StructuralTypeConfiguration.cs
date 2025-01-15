using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Types
{
	// Token: 0x020001FF RID: 511
	internal abstract class StructuralTypeConfiguration : ConfigurationBase
	{
		// Token: 0x06001B14 RID: 6932 RVA: 0x00049ED0 File Offset: 0x000480D0
		internal static Type GetPropertyConfigurationType(Type propertyType)
		{
			propertyType.TryUnwrapNullableType(out propertyType);
			if (propertyType == typeof(string))
			{
				return typeof(StringPropertyConfiguration);
			}
			if (propertyType == typeof(decimal))
			{
				return typeof(DecimalPropertyConfiguration);
			}
			if (propertyType == typeof(DateTime) || propertyType == typeof(TimeSpan) || propertyType == typeof(DateTimeOffset))
			{
				return typeof(DateTimePropertyConfiguration);
			}
			if (propertyType == typeof(byte[]))
			{
				return typeof(BinaryPropertyConfiguration);
			}
			if (!propertyType.IsValueType() && !(propertyType == typeof(HierarchyId)) && !(propertyType == typeof(DbGeography)) && !(propertyType == typeof(DbGeometry)))
			{
				return typeof(NavigationPropertyConfiguration);
			}
			return typeof(PrimitivePropertyConfiguration);
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x00049FD1 File Offset: 0x000481D1
		internal StructuralTypeConfiguration()
		{
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00049FEF File Offset: 0x000481EF
		internal StructuralTypeConfiguration(Type clrType)
		{
			this._clrType = clrType;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0004A014 File Offset: 0x00048214
		internal StructuralTypeConfiguration(StructuralTypeConfiguration source)
		{
			source._primitivePropertyConfigurations.Each(delegate(KeyValuePair<PropertyPath, PrimitivePropertyConfiguration> c)
			{
				this._primitivePropertyConfigurations.Add(c.Key, c.Value.Clone());
			});
			this._ignoredProperties.AddRange(source._ignoredProperties);
			this._clrType = source._clrType;
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x0004A071 File Offset: 0x00048271
		internal virtual IEnumerable<PropertyInfo> ConfiguredProperties
		{
			get
			{
				return this._primitivePropertyConfigurations.Keys.Select((PropertyPath p) => p.Last<PropertyInfo>());
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0004A0A2 File Offset: 0x000482A2
		internal IEnumerable<PropertyInfo> IgnoredProperties
		{
			get
			{
				return this._ignoredProperties;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0004A0AA File Offset: 0x000482AA
		internal Type ClrType
		{
			get
			{
				return this._clrType;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x0004A0B2 File Offset: 0x000482B2
		internal IEnumerable<KeyValuePair<PropertyPath, PrimitivePropertyConfiguration>> PrimitivePropertyConfigurations
		{
			get
			{
				return this._primitivePropertyConfigurations;
			}
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0004A0BA File Offset: 0x000482BA
		public void Ignore(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			this._ignoredProperties.Add(propertyInfo);
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0004A0D8 File Offset: 0x000482D8
		internal PrimitivePropertyConfiguration Property(PropertyPath propertyPath, OverridableConfigurationParts? overridableConfigurationParts = null)
		{
			return this.Property<PrimitivePropertyConfiguration>(propertyPath, delegate
			{
				PrimitivePropertyConfiguration primitivePropertyConfiguration = (PrimitivePropertyConfiguration)Activator.CreateInstance(StructuralTypeConfiguration.GetPropertyConfigurationType(propertyPath.Last<PropertyInfo>().PropertyType));
				if (overridableConfigurationParts != null)
				{
					primitivePropertyConfiguration.OverridableConfigurationParts = overridableConfigurationParts.Value;
				}
				return primitivePropertyConfiguration;
			});
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0004A111 File Offset: 0x00048311
		internal virtual void RemoveProperty(PropertyPath propertyPath)
		{
			this._primitivePropertyConfigurations.Remove(propertyPath);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0004A120 File Offset: 0x00048320
		internal TPrimitivePropertyConfiguration Property<TPrimitivePropertyConfiguration>(PropertyPath propertyPath, Func<TPrimitivePropertyConfiguration> primitivePropertyConfigurationCreator) where TPrimitivePropertyConfiguration : PrimitivePropertyConfiguration
		{
			PrimitivePropertyConfiguration primitivePropertyConfiguration;
			if (!this._primitivePropertyConfigurations.TryGetValue(propertyPath, out primitivePropertyConfiguration))
			{
				primitivePropertyConfiguration = primitivePropertyConfigurationCreator();
				primitivePropertyConfiguration.TypeConfiguration = this;
				this._primitivePropertyConfigurations.Add(propertyPath, primitivePropertyConfiguration);
			}
			return (TPrimitivePropertyConfiguration)((object)primitivePropertyConfiguration);
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0004A164 File Offset: 0x00048364
		internal void ConfigurePropertyMappings(IList<Tuple<ColumnMappingBuilder, EntityType>> propertyMappings, DbProviderManifest providerManifest, bool allowOverride = false)
		{
			foreach (KeyValuePair<PropertyPath, PrimitivePropertyConfiguration> keyValuePair in this.PrimitivePropertyConfigurations)
			{
				PropertyPath propertyPath = keyValuePair.Key;
				keyValuePair.Value.Configure(propertyMappings.Where((Tuple<ColumnMappingBuilder, EntityType> pm) => propertyPath.Equals(new PropertyPath(from p in pm.Item1.PropertyPath.Skip(pm.Item1.PropertyPath.Count - propertyPath.Count)
					select p.GetClrPropertyInfo()))), providerManifest, allowOverride, false);
			}
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0004A1E0 File Offset: 0x000483E0
		internal void ConfigureFunctionParameters(IList<ModificationFunctionParameterBinding> parameterBindings)
		{
			foreach (KeyValuePair<PropertyPath, PrimitivePropertyConfiguration> keyValuePair in this.PrimitivePropertyConfigurations)
			{
				PropertyPath propertyPath = keyValuePair.Key;
				PrimitivePropertyConfiguration value = keyValuePair.Value;
				IEnumerable<FunctionParameter> enumerable = from pb in parameterBindings.Where(delegate(ModificationFunctionParameterBinding pb)
					{
						if (pb.MemberPath.AssociationSetEnd == null)
						{
							return propertyPath.Equals(new PropertyPath(from m in pb.MemberPath.Members.Skip(pb.MemberPath.Members.Count - propertyPath.Count)
								select m.GetClrPropertyInfo()));
						}
						return false;
					})
					select pb.Parameter;
				value.ConfigureFunctionParameters(enumerable);
			}
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0004A280 File Offset: 0x00048480
		internal void Configure(string structuralTypeName, IEnumerable<EdmProperty> properties, ICollection<MetadataProperty> dataModelAnnotations)
		{
			dataModelAnnotations.SetConfiguration(this);
			foreach (KeyValuePair<PropertyPath, PrimitivePropertyConfiguration> keyValuePair in this._primitivePropertyConfigurations)
			{
				PropertyPath key = keyValuePair.Key;
				PrimitivePropertyConfiguration value = keyValuePair.Value;
				StructuralTypeConfiguration.Configure(structuralTypeName, properties, key, value);
			}
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0004A2EC File Offset: 0x000484EC
		private static void Configure(string structuralTypeName, IEnumerable<EdmProperty> properties, IEnumerable<PropertyInfo> propertyPath, PrimitivePropertyConfiguration propertyConfiguration)
		{
			EdmProperty edmProperty = properties.SingleOrDefault((EdmProperty p) => p.GetClrPropertyInfo().IsSameAs(propertyPath.First<PropertyInfo>()));
			if (edmProperty == null)
			{
				throw Error.PropertyNotFound(propertyPath.First<PropertyInfo>().Name, structuralTypeName);
			}
			if (edmProperty.IsUnderlyingPrimitiveType)
			{
				propertyConfiguration.Configure(edmProperty);
				return;
			}
			StructuralTypeConfiguration.Configure(edmProperty.ComplexType.Name, edmProperty.ComplexType.Properties, new PropertyPath(propertyPath.Skip(1)), propertyConfiguration);
		}

		// Token: 0x04000AB0 RID: 2736
		private readonly Dictionary<PropertyPath, PrimitivePropertyConfiguration> _primitivePropertyConfigurations = new Dictionary<PropertyPath, PrimitivePropertyConfiguration>();

		// Token: 0x04000AB1 RID: 2737
		private readonly HashSet<PropertyInfo> _ignoredProperties = new HashSet<PropertyInfo>();

		// Token: 0x04000AB2 RID: 2738
		private readonly Type _clrType;
	}
}
