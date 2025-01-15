using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200010D RID: 269
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public abstract class TermBinding
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00009940 File Offset: 0x00007B40
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00009948 File Offset: 0x00007B48
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string Text { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00009951 File Offset: 0x00007B51
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00009959 File Offset: 0x00007B59
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 15)]
		public string TemplateSchema { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00009962 File Offset: 0x00007B62
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0000996A File Offset: 0x00007B6A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public bool Placeholder { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00009973 File Offset: 0x00007B73
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0000997B File Offset: 0x00007B7B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public bool Corrected { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00009984 File Offset: 0x00007B84
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0000998C File Offset: 0x00007B8C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public SpanMatchType MatchType { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00009995 File Offset: 0x00007B95
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0000999D File Offset: 0x00007B9D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public ResultConfidenceLevel ConfidenceLevel { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x000099A6 File Offset: 0x00007BA6
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x000099AE File Offset: 0x00007BAE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public double Score { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x000099B7 File Offset: 0x00007BB7
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x000099BF File Offset: 0x00007BBF
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public IList<TextSegment> MatchedSegments { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000099C8 File Offset: 0x00007BC8
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x000099D0 File Offset: 0x00007BD0
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 80)]
		public TermBindingContainer OriginalTermBinding { get; set; }

		// Token: 0x06000571 RID: 1393 RVA: 0x000099D9 File Offset: 0x00007BD9
		public override string ToString()
		{
			if (Math.Round(this.Score, 2) != 1.0)
			{
				return StringUtil.FormatInvariant("{0} [{1:0.00}]", this.Text, this.Score);
			}
			return this.Text;
		}

		// Token: 0x06000572 RID: 1394
		public abstract T Accept<T>(TermBindingVisitor<T> visitor);
	}
}
