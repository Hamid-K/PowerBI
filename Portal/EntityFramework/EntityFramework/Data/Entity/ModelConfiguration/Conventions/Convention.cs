using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Conventions.Sets;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200019D RID: 413
	public class Convention : IConvention
	{
		// Token: 0x0600174B RID: 5963 RVA: 0x0003E0BC File Offset: 0x0003C2BC
		public Convention()
		{
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0003E0D4 File Offset: 0x0003C2D4
		internal Convention(ConventionsConfiguration conventionsConfiguration)
		{
			this._conventionsConfiguration = conventionsConfiguration;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0003E0F3 File Offset: 0x0003C2F3
		public TypeConventionConfiguration Types()
		{
			return new TypeConventionConfiguration(this._conventionsConfiguration);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0003E100 File Offset: 0x0003C300
		public TypeConventionConfiguration<T> Types<T>() where T : class
		{
			return new TypeConventionConfiguration<T>(this._conventionsConfiguration);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0003E10D File Offset: 0x0003C30D
		public PropertyConventionConfiguration Properties()
		{
			return new PropertyConventionConfiguration(this._conventionsConfiguration);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0003E11C File Offset: 0x0003C31C
		public PropertyConventionConfiguration Properties<T>()
		{
			if (!typeof(T).IsValidEdmScalarType())
			{
				throw Error.ModelBuilder_PropertyFilterTypeMustBePrimitive(typeof(T));
			}
			return new PropertyConventionConfiguration(this._conventionsConfiguration).Where(delegate(PropertyInfo p)
			{
				Type type;
				p.PropertyType.TryUnwrapNullableType(out type);
				return type == typeof(T);
			});
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0003E179 File Offset: 0x0003C379
		internal virtual void ApplyModelConfiguration(ModelConfiguration modelConfiguration)
		{
			this._conventionsConfiguration.ApplyModelConfiguration(modelConfiguration);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0003E187 File Offset: 0x0003C387
		internal virtual void ApplyModelConfiguration(Type type, ModelConfiguration modelConfiguration)
		{
			this._conventionsConfiguration.ApplyModelConfiguration(type, modelConfiguration);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0003E196 File Offset: 0x0003C396
		internal virtual void ApplyTypeConfiguration<TStructuralTypeConfiguration>(Type type, Func<TStructuralTypeConfiguration> structuralTypeConfiguration, ModelConfiguration modelConfiguration) where TStructuralTypeConfiguration : StructuralTypeConfiguration
		{
			this._conventionsConfiguration.ApplyTypeConfiguration<TStructuralTypeConfiguration>(type, structuralTypeConfiguration, modelConfiguration);
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0003E1A6 File Offset: 0x0003C3A6
		internal virtual void ApplyPropertyConfiguration(PropertyInfo propertyInfo, ModelConfiguration modelConfiguration)
		{
			this._conventionsConfiguration.ApplyPropertyConfiguration(propertyInfo, modelConfiguration);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0003E1B5 File Offset: 0x0003C3B5
		internal virtual void ApplyPropertyConfiguration(PropertyInfo propertyInfo, Func<PropertyConfiguration> propertyConfiguration, ModelConfiguration modelConfiguration)
		{
			this._conventionsConfiguration.ApplyPropertyConfiguration(propertyInfo, propertyConfiguration, modelConfiguration);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0003E1C5 File Offset: 0x0003C3C5
		internal virtual void ApplyPropertyTypeConfiguration<TStructuralTypeConfiguration>(PropertyInfo propertyInfo, Func<TStructuralTypeConfiguration> structuralTypeConfiguration, ModelConfiguration modelConfiguration) where TStructuralTypeConfiguration : StructuralTypeConfiguration
		{
			this._conventionsConfiguration.ApplyPropertyTypeConfiguration<TStructuralTypeConfiguration>(propertyInfo, structuralTypeConfiguration, modelConfiguration);
		}

		// Token: 0x04000A31 RID: 2609
		private readonly ConventionsConfiguration _conventionsConfiguration = new ConventionsConfiguration(new ConventionSet());
	}
}
