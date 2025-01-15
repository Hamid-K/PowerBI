using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001670 RID: 5744
	public class MatchInstanceCacheItem : MatchCacheItem
	{
		// Token: 0x170020D1 RID: 8401
		// (get) Token: 0x0600C013 RID: 49171 RVA: 0x002963B1 File Offset: 0x002945B1
		// (set) Token: 0x0600C014 RID: 49172 RVA: 0x002963B9 File Offset: 0x002945B9
		public int Instance { get; set; }

		// Token: 0x170020D2 RID: 8402
		// (get) Token: 0x0600C015 RID: 49173 RVA: 0x002963C2 File Offset: 0x002945C2
		// (set) Token: 0x0600C016 RID: 49174 RVA: 0x002963CA File Offset: 0x002945CA
		public int InstanceFromEnd { get; set; }
	}
}
