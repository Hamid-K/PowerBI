using System;
using System.Runtime.Serialization;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200025E RID: 606
	[DataContract]
	public class BatchQueryConstraintTelemetry
	{
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x0004857A File Offset: 0x0004677A
		// (set) Token: 0x06001A4B RID: 6731 RVA: 0x00048582 File Offset: 0x00046782
		[DataMember(Name = "Id", EmitDefaultValue = false, Order = 10)]
		internal string Id { get; set; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x0004858B File Offset: 0x0004678B
		// (set) Token: 0x06001A4D RID: 6733 RVA: 0x00048593 File Offset: 0x00046793
		[DataMember(Name = "GroupEntities", EmitDefaultValue = false, Order = 20)]
		internal int GroupEntityCount { get; set; }

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x0004859C File Offset: 0x0004679C
		// (set) Token: 0x06001A4F RID: 6735 RVA: 0x000485A4 File Offset: 0x000467A4
		[DataMember(Name = "HasUnrelatedG", EmitDefaultValue = false, Order = 30)]
		internal bool HasUnrelatedGroups { get; set; }

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x000485AD File Offset: 0x000467AD
		// (set) Token: 0x06001A51 RID: 6737 RVA: 0x000485B5 File Offset: 0x000467B5
		[DataMember(Name = "TotalM", EmitDefaultValue = false, Order = 40)]
		internal int TotalMeasureCount { get; set; }

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x000485BE File Offset: 0x000467BE
		// (set) Token: 0x06001A53 RID: 6739 RVA: 0x000485C6 File Offset: 0x000467C6
		[DataMember(Name = "RelatedM", EmitDefaultValue = false, Order = 50)]
		internal int RelatedMeasureCount { get; set; }

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x000485CF File Offset: 0x000467CF
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x000485D7 File Offset: 0x000467D7
		[DataMember(Name = "UnrelatedM", EmitDefaultValue = false, Order = 60)]
		internal int UnrelatedMeasureCount { get; set; }

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x000485E0 File Offset: 0x000467E0
		// (set) Token: 0x06001A57 RID: 6743 RVA: 0x000485E8 File Offset: 0x000467E8
		[DataMember(Name = "PartialRelatedM", EmitDefaultValue = false, Order = 70)]
		internal int PartiallyRelatedMeasureCount { get; set; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x000485F1 File Offset: 0x000467F1
		// (set) Token: 0x06001A59 RID: 6745 RVA: 0x000485F9 File Offset: 0x000467F9
		[DataMember(Name = "UnrelatedMIgnored", EmitDefaultValue = false, Order = 80)]
		internal bool UnrelatedMeasuresIgnored { get; set; }
	}
}
