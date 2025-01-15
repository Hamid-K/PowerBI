using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000019 RID: 25
	[DataContract]
	internal sealed class LayoutContext
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003BE9 File Offset: 0x00001DE9
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003BF1 File Offset: 0x00001DF1
		[DataMember]
		public string ChartLayoutType { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003BFA File Offset: 0x00001DFA
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003C02 File Offset: 0x00001E02
		[DataMember]
		public string Type { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003C0B File Offset: 0x00001E0B
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003C13 File Offset: 0x00001E13
		[DataMember]
		public string VerticalAlignment { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003C1C File Offset: 0x00001E1C
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003C24 File Offset: 0x00001E24
		[DataMember]
		public List<PVParagraph> Paragraphs { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003C2D File Offset: 0x00001E2D
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003C35 File Offset: 0x00001E35
		[DataMember]
		public string Theme { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003C3E File Offset: 0x00001E3E
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003C46 File Offset: 0x00001E46
		[DataMember]
		public string ResourceId { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003C4F File Offset: 0x00001E4F
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003C57 File Offset: 0x00001E57
		[DataMember]
		public string CardStyles { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003C60 File Offset: 0x00001E60
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003C68 File Offset: 0x00001E68
		[DataMember]
		public bool? IsLegendHidden { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003C71 File Offset: 0x00001E71
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003C79 File Offset: 0x00001E79
		[DataMember]
		public bool? IsChartTitleHidden { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003C82 File Offset: 0x00001E82
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003C8A File Offset: 0x00001E8A
		[DataMember]
		public bool? AreLabelsVisible { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003C93 File Offset: 0x00001E93
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003C9B File Offset: 0x00001E9B
		[DataMember]
		public string LegendPosition { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003CA4 File Offset: 0x00001EA4
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003CAC File Offset: 0x00001EAC
		[DataMember]
		public string LabelsPosition { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003CB5 File Offset: 0x00001EB5
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003CBD File Offset: 0x00001EBD
		[DataMember]
		public List<PVColumnInfo> Columns { get; set; }
	}
}
