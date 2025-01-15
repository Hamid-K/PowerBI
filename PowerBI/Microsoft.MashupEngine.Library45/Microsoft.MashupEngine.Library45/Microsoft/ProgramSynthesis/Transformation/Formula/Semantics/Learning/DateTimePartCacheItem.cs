using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001669 RID: 5737
	public class DateTimePartCacheItem
	{
		// Token: 0x170020BD RID: 8381
		// (get) Token: 0x0600BFD4 RID: 49108 RVA: 0x0029603E File Offset: 0x0029423E
		// (set) Token: 0x0600BFD5 RID: 49109 RVA: 0x00296046 File Offset: 0x00294246
		public DateTimePartKind Kind { get; set; }

		// Token: 0x170020BE RID: 8382
		// (get) Token: 0x0600BFD6 RID: 49110 RVA: 0x0029604F File Offset: 0x0029424F
		// (set) Token: 0x0600BFD7 RID: 49111 RVA: 0x00296057 File Offset: 0x00294257
		public decimal Result { get; set; }

		// Token: 0x170020BF RID: 8383
		// (get) Token: 0x0600BFD8 RID: 49112 RVA: 0x00296060 File Offset: 0x00294260
		// (set) Token: 0x0600BFD9 RID: 49113 RVA: 0x00296068 File Offset: 0x00294268
		public DateTime Subject { get; set; }

		// Token: 0x0600BFDA RID: 49114 RVA: 0x00296071 File Offset: 0x00294271
		public override string ToString()
		{
			return string.Format("Kind={0}, Result={1}, Subject={2}", this.Kind, this.Result, this.Subject);
		}
	}
}
