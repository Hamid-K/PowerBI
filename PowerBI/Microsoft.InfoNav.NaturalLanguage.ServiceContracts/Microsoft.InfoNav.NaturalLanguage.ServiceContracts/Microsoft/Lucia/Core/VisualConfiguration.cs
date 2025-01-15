using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200014A RID: 330
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class VisualConfiguration
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0000B5B5 File Offset: 0x000097B5
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x0000B5BD File Offset: 0x000097BD
		[DataMember(IsRequired = true, Order = 10)]
		public VisualizationType VisualizationType { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0000B5C6 File Offset: 0x000097C6
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x0000B5CE File Offset: 0x000097CE
		[DataMember(IsRequired = false, Order = 20)]
		public DataShapeBinding DataShapeBinding { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0000B5D7 File Offset: 0x000097D7
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x0000B5DF File Offset: 0x000097DF
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<VisualElement> VisualElements { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0000B5E8 File Offset: 0x000097E8
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x0000B5F0 File Offset: 0x000097F0
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public QueryDefinition Query { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0000B5F9 File Offset: 0x000097F9
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0000B601 File Offset: 0x00009801
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public QueryMetadata QueryMetadata { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0000B60A File Offset: 0x0000980A
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x0000B612 File Offset: 0x00009812
		internal double Score { get; set; }

		// Token: 0x06000690 RID: 1680 RVA: 0x0000B61B File Offset: 0x0000981B
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "VisualConfiguration: {0}", this.VisualizationType);
		}
	}
}
