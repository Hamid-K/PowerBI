using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x0200166A RID: 5738
	public class TimePartCacheItem
	{
		// Token: 0x170020C0 RID: 8384
		// (get) Token: 0x0600BFDC RID: 49116 RVA: 0x0029609E File Offset: 0x0029429E
		// (set) Token: 0x0600BFDD RID: 49117 RVA: 0x002960A6 File Offset: 0x002942A6
		public TimePartKind Kind { get; set; }

		// Token: 0x170020C1 RID: 8385
		// (get) Token: 0x0600BFDE RID: 49118 RVA: 0x002960AF File Offset: 0x002942AF
		// (set) Token: 0x0600BFDF RID: 49119 RVA: 0x002960B7 File Offset: 0x002942B7
		public decimal Result { get; set; }

		// Token: 0x170020C2 RID: 8386
		// (get) Token: 0x0600BFE0 RID: 49120 RVA: 0x002960C0 File Offset: 0x002942C0
		// (set) Token: 0x0600BFE1 RID: 49121 RVA: 0x002960C8 File Offset: 0x002942C8
		public Time Subject { get; set; }

		// Token: 0x0600BFE2 RID: 49122 RVA: 0x002960D1 File Offset: 0x002942D1
		public override string ToString()
		{
			return string.Format("Kind={0}, Result={1}, Subject={2}", this.Kind, this.Result, this.Subject);
		}
	}
}
