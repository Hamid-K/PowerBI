using System;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000114 RID: 276
	public class SingletonConfiguration<TEntityType> : NavigationSourceConfiguration<TEntityType> where TEntityType : class
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x000279DF File Offset: 0x00025BDF
		internal SingletonConfiguration(ODataModelBuilder modelBuilder, string name)
			: base(modelBuilder, new SingletonConfiguration(modelBuilder, typeof(TEntityType), name))
		{
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000279F9 File Offset: 0x00025BF9
		internal SingletonConfiguration(ODataModelBuilder modelBuilder, SingletonConfiguration configuration)
			: base(modelBuilder, configuration)
		{
		}
	}
}
