using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001C1 RID: 449
	public class ConfigurationRegistrar
	{
		// Token: 0x060017DA RID: 6106 RVA: 0x00040A2C File Offset: 0x0003EC2C
		internal ConfigurationRegistrar(ModelConfiguration modelConfiguration)
		{
			this._modelConfiguration = modelConfiguration;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00040A3B File Offset: 0x0003EC3B
		public virtual ConfigurationRegistrar AddFromAssembly(Assembly assembly)
		{
			Check.NotNull<Assembly>(assembly, "assembly");
			new ConfigurationTypesFinder().AddConfigurationTypesToModel(assembly.GetAccessibleTypes(), this._modelConfiguration);
			return this;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00040A60 File Offset: 0x0003EC60
		public virtual ConfigurationRegistrar Add<TEntityType>(EntityTypeConfiguration<TEntityType> entityTypeConfiguration) where TEntityType : class
		{
			Check.NotNull<EntityTypeConfiguration<TEntityType>>(entityTypeConfiguration, "entityTypeConfiguration");
			this._modelConfiguration.Add((EntityTypeConfiguration)entityTypeConfiguration.Configuration);
			return this;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00040A85 File Offset: 0x0003EC85
		public virtual ConfigurationRegistrar Add<TComplexType>(ComplexTypeConfiguration<TComplexType> complexTypeConfiguration) where TComplexType : class
		{
			Check.NotNull<ComplexTypeConfiguration<TComplexType>>(complexTypeConfiguration, "complexTypeConfiguration");
			this._modelConfiguration.Add((ComplexTypeConfiguration)complexTypeConfiguration.Configuration);
			return this;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00040AAA File Offset: 0x0003ECAA
		internal virtual IEnumerable<Type> GetConfiguredTypes()
		{
			return this._modelConfiguration.ConfiguredTypes.ToList<Type>();
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00040ABC File Offset: 0x0003ECBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00040AC4 File Offset: 0x0003ECC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00040ACD File Offset: 0x0003ECCD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00040AD5 File Offset: 0x0003ECD5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A47 RID: 2631
		private readonly ModelConfiguration _modelConfiguration;
	}
}
