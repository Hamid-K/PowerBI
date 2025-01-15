using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000147 RID: 327
	[DataContract(Name = "GenerateUtteranceResult", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class GenerateUtteranceResult
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0000B52E File Offset: 0x0000972E
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x0000B536 File Offset: 0x00009736
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string Utterance { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0000B53F File Offset: 0x0000973F
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x0000B547 File Offset: 0x00009747
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public IList<Term> Spans { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0000B550 File Offset: 0x00009750
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0000B558 File Offset: 0x00009758
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public GenerateUtteranceWarnings Warnings { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0000B561 File Offset: 0x00009761
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0000B569 File Offset: 0x00009769
		public object SemanticExpression { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0000B572 File Offset: 0x00009772
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x0000B57A File Offset: 0x0000977A
		public QueryDefinition QueryDefinition { get; set; }
	}
}
