using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x0200166E RID: 5742
	public class RoundDateTimeCacheItem
	{
		// Token: 0x170020CA RID: 8394
		// (get) Token: 0x0600C002 RID: 49154 RVA: 0x00296312 File Offset: 0x00294512
		// (set) Token: 0x0600C003 RID: 49155 RVA: 0x0029631A File Offset: 0x0029451A
		public RoundDateTimeDescriptor Descriptor { get; set; }

		// Token: 0x170020CB RID: 8395
		// (get) Token: 0x0600C004 RID: 49156 RVA: 0x00296323 File Offset: 0x00294523
		// (set) Token: 0x0600C005 RID: 49157 RVA: 0x0029632B File Offset: 0x0029452B
		public DateTime Result { get; set; }

		// Token: 0x170020CC RID: 8396
		// (get) Token: 0x0600C006 RID: 49158 RVA: 0x00296334 File Offset: 0x00294534
		// (set) Token: 0x0600C007 RID: 49159 RVA: 0x0029633C File Offset: 0x0029453C
		public DateTime Subject { get; set; }

		// Token: 0x0600C008 RID: 49160 RVA: 0x00296345 File Offset: 0x00294545
		public override string ToString()
		{
			return string.Format("Descriptor={0}, Result={1}, Subject={2}", this.Descriptor, this.Result, this.Subject);
		}
	}
}
