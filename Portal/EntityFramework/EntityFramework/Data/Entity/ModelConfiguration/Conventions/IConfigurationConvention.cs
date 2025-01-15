using System;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200017C RID: 380
	internal interface IConfigurationConvention : IConvention
	{
		// Token: 0x060016EB RID: 5867
		void Apply(ModelConfiguration modelConfiguration);
	}
}
