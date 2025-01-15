using System;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000116 RID: 278
	public class SingletonConfiguration : NavigationSourceConfiguration
	{
		// Token: 0x0600098C RID: 2444 RVA: 0x000281E4 File Offset: 0x000263E4
		public SingletonConfiguration()
		{
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x000281EC File Offset: 0x000263EC
		public SingletonConfiguration(ODataModelBuilder modelBuilder, Type entityClrType, string name)
			: base(modelBuilder, entityClrType, name)
		{
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x000281F7 File Offset: 0x000263F7
		public SingletonConfiguration(ODataModelBuilder modelBuilder, EntityTypeConfiguration entityType, string name)
			: base(modelBuilder, entityType, name)
		{
		}
	}
}
