using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000101 RID: 257
	[DataContract]
	public sealed class AdditionalMessage
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0000EAC2 File Offset: 0x0000CCC2
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0000EACA File Offset: 0x0000CCCA
		[DataMember(Name = "Code", IsRequired = true, Order = 0)]
		public string Code { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0000EAD3 File Offset: 0x0000CCD3
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0000EADB File Offset: 0x0000CCDB
		[DataMember(Name = "Severity", IsRequired = true, Order = 10)]
		public string Severity { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0000EAE4 File Offset: 0x0000CCE4
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		[DataMember(Name = "Message", IsRequired = true, Order = 20)]
		public string Message { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0000EAF5 File Offset: 0x0000CCF5
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0000EAFD File Offset: 0x0000CCFD
		[DataMember(Name = "ObjectType", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string ObjectType { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0000EB06 File Offset: 0x0000CD06
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x0000EB0E File Offset: 0x0000CD0E
		[DataMember(Name = "ObjectName", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public string ObjectName { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0000EB17 File Offset: 0x0000CD17
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x0000EB1F File Offset: 0x0000CD1F
		[DataMember(Name = "PropertyName", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public string PropertyName { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0000EB28 File Offset: 0x0000CD28
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x0000EB30 File Offset: 0x0000CD30
		[DataMember(Name = "Line", IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public int? Line { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0000EB39 File Offset: 0x0000CD39
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0000EB41 File Offset: 0x0000CD41
		[DataMember(Name = "Position", IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public int? Position { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0000EB4A File Offset: 0x0000CD4A
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0000EB52 File Offset: 0x0000CD52
		[DataMember(Name = "AffectedItems", IsRequired = false, EmitDefaultValue = false, Order = 80)]
		public string[] AffectedItems { get; set; }
	}
}
