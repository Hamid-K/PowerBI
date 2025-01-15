using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001612 RID: 5650
	public class CountEvent
	{
		// Token: 0x1700206C RID: 8300
		// (get) Token: 0x0600BC4C RID: 48204 RVA: 0x00288123 File Offset: 0x00286323
		// (set) Token: 0x0600BC4D RID: 48205 RVA: 0x0028812B File Offset: 0x0028632B
		public string Category { get; set; }

		// Token: 0x1700206D RID: 8301
		// (get) Token: 0x0600BC4E RID: 48206 RVA: 0x00288134 File Offset: 0x00286334
		// (set) Token: 0x0600BC4F RID: 48207 RVA: 0x0028813C File Offset: 0x0028633C
		public int Count { get; set; } = 1;

		// Token: 0x1700206E RID: 8302
		// (get) Token: 0x0600BC50 RID: 48208 RVA: 0x00288145 File Offset: 0x00286345
		// (set) Token: 0x0600BC51 RID: 48209 RVA: 0x0028814D File Offset: 0x0028634D
		public string Name { get; set; }
	}
}
