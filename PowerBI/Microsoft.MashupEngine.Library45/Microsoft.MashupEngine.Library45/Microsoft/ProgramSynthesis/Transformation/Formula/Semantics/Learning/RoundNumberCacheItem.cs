using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001672 RID: 5746
	public class RoundNumberCacheItem
	{
		// Token: 0x170020D8 RID: 8408
		// (get) Token: 0x0600C023 RID: 49187 RVA: 0x0029648C File Offset: 0x0029468C
		// (set) Token: 0x0600C024 RID: 49188 RVA: 0x00296494 File Offset: 0x00294694
		public RoundNumberDescriptor Descriptor { get; set; }

		// Token: 0x170020D9 RID: 8409
		// (get) Token: 0x0600C025 RID: 49189 RVA: 0x0029649D File Offset: 0x0029469D
		// (set) Token: 0x0600C026 RID: 49190 RVA: 0x002964A5 File Offset: 0x002946A5
		public decimal Result { get; set; }

		// Token: 0x170020DA RID: 8410
		// (get) Token: 0x0600C027 RID: 49191 RVA: 0x002964AE File Offset: 0x002946AE
		// (set) Token: 0x0600C028 RID: 49192 RVA: 0x002964B6 File Offset: 0x002946B6
		public decimal Subject { get; set; }

		// Token: 0x0600C029 RID: 49193 RVA: 0x002964BF File Offset: 0x002946BF
		public override string ToString()
		{
			return string.Format("Descriptor={0}, Result={1}, Subject={2}", this.Descriptor, this.Result, this.Subject);
		}
	}
}
