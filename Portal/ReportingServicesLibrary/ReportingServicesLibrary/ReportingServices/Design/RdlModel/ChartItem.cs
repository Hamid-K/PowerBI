using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003BD RID: 957
	public sealed class ChartItem : ReportItem
	{
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x0007DC62 File Offset: 0x0007BE62
		// (set) Token: 0x06001ED6 RID: 7894 RVA: 0x0007DC6A File Offset: 0x0007BE6A
		public ChartTypes Type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x0007DC73 File Offset: 0x0007BE73
		// (set) Token: 0x06001ED8 RID: 7896 RVA: 0x0007DC7B File Offset: 0x0007BE7B
		public ChartSubTypes Subtype
		{
			get
			{
				return this.m_Subtype;
			}
			set
			{
				this.m_Subtype = value;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x0007DC84 File Offset: 0x0007BE84
		// (set) Token: 0x06001EDA RID: 7898 RVA: 0x0007DC8C File Offset: 0x0007BE8C
		public Title Title
		{
			get
			{
				return this.m_Title;
			}
			set
			{
				this.m_Title = value;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x0007DC95 File Offset: 0x0007BE95
		// (set) Token: 0x06001EDC RID: 7900 RVA: 0x0007DC9D File Offset: 0x0007BE9D
		public Palettes Palette
		{
			get
			{
				return this.m_Palette;
			}
			set
			{
				this.m_Palette = value;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x0007DCA6 File Offset: 0x0007BEA6
		// (set) Token: 0x06001EDE RID: 7902 RVA: 0x0007DCAE File Offset: 0x0007BEAE
		public ThreeDProperties ThreeDProperties
		{
			get
			{
				return this.m_ThreeDProperties;
			}
			set
			{
				this.m_ThreeDProperties = value;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x0007DCB7 File Offset: 0x0007BEB7
		// (set) Token: 0x06001EE0 RID: 7904 RVA: 0x0007DCBF File Offset: 0x0007BEBF
		public Legend Legend
		{
			get
			{
				return this.m_Legend;
			}
			set
			{
				this.m_Legend = value;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x0007DCC8 File Offset: 0x0007BEC8
		// (set) Token: 0x06001EE2 RID: 7906 RVA: 0x0007DCD0 File Offset: 0x0007BED0
		public CategoryAxis CategoryAxis
		{
			get
			{
				return this.m_CategoryAxis;
			}
			set
			{
				this.m_CategoryAxis = value;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x0007DCD9 File Offset: 0x0007BED9
		// (set) Token: 0x06001EE4 RID: 7908 RVA: 0x0007DCE1 File Offset: 0x0007BEE1
		public ValueAxis ValueAxis
		{
			get
			{
				return this.m_ValueAxis;
			}
			set
			{
				this.m_ValueAxis = value;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x0007DCEA File Offset: 0x0007BEEA
		// (set) Token: 0x06001EE6 RID: 7910 RVA: 0x0007DCF2 File Offset: 0x0007BEF2
		public List<SeriesGrouping> SeriesGroupings
		{
			get
			{
				return this.m_SeriesGroupings;
			}
			set
			{
				this.m_SeriesGroupings = value;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x0007DCFB File Offset: 0x0007BEFB
		// (set) Token: 0x06001EE8 RID: 7912 RVA: 0x0007DD03 File Offset: 0x0007BF03
		public List<CategoryGrouping> CategoryGroupings
		{
			get
			{
				return this.m_CategoryGroupings;
			}
			set
			{
				this.m_CategoryGroupings = value;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x0007DD0C File Offset: 0x0007BF0C
		// (set) Token: 0x06001EEA RID: 7914 RVA: 0x0007DD14 File Offset: 0x0007BF14
		public List<ChartSeries> ChartData
		{
			get
			{
				return this.m_ChartData;
			}
			set
			{
				this.m_ChartData = value;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x0007DD1D File Offset: 0x0007BF1D
		// (set) Token: 0x06001EEC RID: 7916 RVA: 0x0007DD25 File Offset: 0x0007BF25
		public Filters Filters
		{
			get
			{
				return this.m_filters;
			}
			set
			{
				this.m_filters = value;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x0007DD2E File Offset: 0x0007BF2E
		// (set) Token: 0x06001EEE RID: 7918 RVA: 0x0007DD3B File Offset: 0x0007BF3B
		public PlotArea PlotArea
		{
			get
			{
				return new PlotArea(this.m_PlotStyle);
			}
			set
			{
				this.m_PlotStyle = value.Style;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x0007DD49 File Offset: 0x0007BF49
		// (set) Token: 0x06001EF0 RID: 7920 RVA: 0x0007DD51 File Offset: 0x0007BF51
		[DefaultValue(0)]
		public int PointWidth
		{
			get
			{
				return this.m_PointWidth;
			}
			set
			{
				this.m_PointWidth = value;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x0007DD5A File Offset: 0x0007BF5A
		// (set) Token: 0x06001EF2 RID: 7922 RVA: 0x0007DD62 File Offset: 0x0007BF62
		[DefaultValue("")]
		public string DataSetName
		{
			get
			{
				return this.m_dataSetName;
			}
			set
			{
				this.m_dataSetName = value;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x0007DD6B File Offset: 0x0007BF6B
		// (set) Token: 0x06001EF4 RID: 7924 RVA: 0x0007DD73 File Offset: 0x0007BF73
		[DefaultValue(false)]
		public bool PageBreakAtStart
		{
			get
			{
				return this.m_pageBreakAtStart;
			}
			set
			{
				this.m_pageBreakAtStart = value;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x0007DD7C File Offset: 0x0007BF7C
		// (set) Token: 0x06001EF6 RID: 7926 RVA: 0x0007DD84 File Offset: 0x0007BF84
		[DefaultValue(false)]
		public bool PageBreakAtEnd
		{
			get
			{
				return this.m_pageBreakAtEnd;
			}
			set
			{
				this.m_pageBreakAtEnd = value;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x0007DD8D File Offset: 0x0007BF8D
		// (set) Token: 0x06001EF8 RID: 7928 RVA: 0x0007DD95 File Offset: 0x0007BF95
		[DefaultValue("")]
		public string NoRows
		{
			get
			{
				return this.m_noRows;
			}
			set
			{
				this.m_noRows = value;
			}
		}

		// Token: 0x04000D63 RID: 3427
		private ChartTypes m_Type;

		// Token: 0x04000D64 RID: 3428
		private ChartSubTypes m_Subtype;

		// Token: 0x04000D65 RID: 3429
		private Title m_Title;

		// Token: 0x04000D66 RID: 3430
		private Palettes m_Palette;

		// Token: 0x04000D67 RID: 3431
		private ThreeDProperties m_ThreeDProperties;

		// Token: 0x04000D68 RID: 3432
		private Legend m_Legend;

		// Token: 0x04000D69 RID: 3433
		private CategoryAxis m_CategoryAxis;

		// Token: 0x04000D6A RID: 3434
		private ValueAxis m_ValueAxis;

		// Token: 0x04000D6B RID: 3435
		private List<SeriesGrouping> m_SeriesGroupings;

		// Token: 0x04000D6C RID: 3436
		private List<CategoryGrouping> m_CategoryGroupings;

		// Token: 0x04000D6D RID: 3437
		private List<ChartSeries> m_ChartData;

		// Token: 0x04000D6E RID: 3438
		private Style m_PlotStyle;

		// Token: 0x04000D6F RID: 3439
		private string m_dataSetName;

		// Token: 0x04000D70 RID: 3440
		private bool m_pageBreakAtStart;

		// Token: 0x04000D71 RID: 3441
		private bool m_pageBreakAtEnd;

		// Token: 0x04000D72 RID: 3442
		private string m_noRows;

		// Token: 0x04000D73 RID: 3443
		private Filters m_filters;

		// Token: 0x04000D74 RID: 3444
		private int m_PointWidth;
	}
}
