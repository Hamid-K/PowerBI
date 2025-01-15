using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200017E RID: 382
	internal interface IConfigurationConvention<TMemberInfo, TConfiguration> : IConvention where TMemberInfo : MemberInfo where TConfiguration : ConfigurationBase
	{
		// Token: 0x060016ED RID: 5869
		void Apply(TMemberInfo memberInfo, Func<TConfiguration> configuration, ModelConfiguration modelConfiguration);
	}
}
