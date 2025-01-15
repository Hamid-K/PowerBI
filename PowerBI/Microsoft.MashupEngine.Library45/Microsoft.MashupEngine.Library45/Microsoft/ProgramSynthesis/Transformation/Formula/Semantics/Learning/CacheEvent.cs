using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001613 RID: 5651
	public class CacheEvent
	{
		// Token: 0x1700206F RID: 8303
		// (get) Token: 0x0600BC53 RID: 48211 RVA: 0x00288165 File Offset: 0x00286365
		// (set) Token: 0x0600BC54 RID: 48212 RVA: 0x0028816D File Offset: 0x0028636D
		public string Category { get; set; }

		// Token: 0x17002070 RID: 8304
		// (get) Token: 0x0600BC55 RID: 48213 RVA: 0x00288176 File Offset: 0x00286376
		// (set) Token: 0x0600BC56 RID: 48214 RVA: 0x0028817E File Offset: 0x0028637E
		public int HitCount { get; set; }

		// Token: 0x17002071 RID: 8305
		// (get) Token: 0x0600BC57 RID: 48215 RVA: 0x00288187 File Offset: 0x00286387
		// (set) Token: 0x0600BC58 RID: 48216 RVA: 0x0028818F File Offset: 0x0028638F
		public int MissCount { get; set; }

		// Token: 0x17002072 RID: 8306
		// (get) Token: 0x0600BC59 RID: 48217 RVA: 0x00288198 File Offset: 0x00286398
		// (set) Token: 0x0600BC5A RID: 48218 RVA: 0x002881A0 File Offset: 0x002863A0
		public string Name { get; set; }
	}
}
