using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200017D RID: 381
	internal interface IConfigurationConvention<TMemberInfo> : IConvention where TMemberInfo : MemberInfo
	{
		// Token: 0x060016EC RID: 5868
		void Apply(TMemberInfo memberInfo, ModelConfiguration modelConfiguration);
	}
}
