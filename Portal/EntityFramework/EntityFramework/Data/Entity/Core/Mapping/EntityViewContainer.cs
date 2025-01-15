using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200052F RID: 1327
	[Obsolete("The mechanism to provide pre-generated views has changed. Implement a class that derives from System.Data.Entity.Infrastructure.MappingViews.DbMappingViewCache and has a parameterless constructor, then associate it with a type that derives from DbContext or ObjectContext by using System.Data.Entity.Infrastructure.MappingViews.DbMappingViewCacheTypeAttribute.", true)]
	public abstract class EntityViewContainer
	{
		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x0600417C RID: 16764 RVA: 0x000DD502 File Offset: 0x000DB702
		internal IEnumerable<KeyValuePair<string, string>> ExtentViews
		{
			get
			{
				int num;
				for (int i = 0; i < this.ViewCount; i = num + 1)
				{
					yield return this.GetViewAt(i);
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x0600417D RID: 16765
		protected abstract KeyValuePair<string, string> GetViewAt(int index);

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x0600417E RID: 16766 RVA: 0x000DD512 File Offset: 0x000DB712
		// (set) Token: 0x0600417F RID: 16767 RVA: 0x000DD51A File Offset: 0x000DB71A
		public string EdmEntityContainerName { get; set; }

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06004180 RID: 16768 RVA: 0x000DD523 File Offset: 0x000DB723
		// (set) Token: 0x06004181 RID: 16769 RVA: 0x000DD52B File Offset: 0x000DB72B
		public string StoreEntityContainerName { get; set; }

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06004182 RID: 16770 RVA: 0x000DD534 File Offset: 0x000DB734
		// (set) Token: 0x06004183 RID: 16771 RVA: 0x000DD53C File Offset: 0x000DB73C
		public string HashOverMappingClosure { get; set; }

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06004184 RID: 16772 RVA: 0x000DD545 File Offset: 0x000DB745
		// (set) Token: 0x06004185 RID: 16773 RVA: 0x000DD54D File Offset: 0x000DB74D
		public string HashOverAllExtentViews { get; set; }

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06004186 RID: 16774 RVA: 0x000DD556 File Offset: 0x000DB756
		// (set) Token: 0x06004187 RID: 16775 RVA: 0x000DD55E File Offset: 0x000DB75E
		public int ViewCount { get; protected set; }
	}
}
