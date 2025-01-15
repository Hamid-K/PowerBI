using System;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000E0 RID: 224
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class SuggestedVisualization
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00008435 File Offset: 0x00006635
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0000843D File Offset: 0x0000663D
		[DataMember(IsRequired = true, Order = 1)]
		public VisualizationType VisualizationType { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00008446 File Offset: 0x00006646
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0000844E File Offset: 0x0000664E
		[DataMember(IsRequired = true, Order = 2)]
		public bool IsMultiple { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00008457 File Offset: 0x00006657
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0000845F File Offset: 0x0000665F
		[DataMember(IsRequired = true, Order = 3)]
		public VisualizationSuggestionSource SuggestionSource { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00008468 File Offset: 0x00006668
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00008470 File Offset: 0x00006670
		[DataMember(IsRequired = true, Order = 4)]
		public double Score { get; set; }

		// Token: 0x06000464 RID: 1124 RVA: 0x0000847C File Offset: 0x0000667C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Visualization Type: ");
			stringBuilder.Append(this.VisualizationType);
			stringBuilder.Append(" Source: ");
			stringBuilder.Append(this.SuggestionSource);
			stringBuilder.Append(" Score: ");
			stringBuilder.Append(this.Score);
			return stringBuilder.ToString();
		}
	}
}
