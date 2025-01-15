using System;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001C2 RID: 450
	internal class ConfigurationTypeActivator
	{
		// Token: 0x060017E3 RID: 6115 RVA: 0x00040AE0 File Offset: 0x0003ECE0
		public virtual TStructuralTypeConfiguration Activate<TStructuralTypeConfiguration>(Type type) where TStructuralTypeConfiguration : StructuralTypeConfiguration
		{
			if (type.GetDeclaredConstructor(new Type[0]) == null)
			{
				throw new InvalidOperationException(Strings.CreateConfigurationType_NoParameterlessConstructor(type.Name));
			}
			return (TStructuralTypeConfiguration)((object)typeof(StructuralTypeConfiguration<>).MakeGenericType(new Type[] { type.TryGetElementType(typeof(StructuralTypeConfiguration<>)) }).GetDeclaredProperty("Configuration").GetValue(Activator.CreateInstance(type, true), null));
		}
	}
}
