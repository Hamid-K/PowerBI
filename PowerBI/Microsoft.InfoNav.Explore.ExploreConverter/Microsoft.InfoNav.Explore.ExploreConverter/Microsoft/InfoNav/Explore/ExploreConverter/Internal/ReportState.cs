using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000B0 RID: 176
	internal sealed class ReportState
	{
		// Token: 0x060003BA RID: 954 RVA: 0x00013958 File Offset: 0x00011B58
		internal ReportState(List<ReportSectionState> reportSections, double fontScale, string theme, FilterAreaVisibility filterAreaVisibility)
		{
			this._reportSections = reportSections;
			this._fontScale = fontScale;
			this._theme = theme;
			this._filterAreaVisibility = filterAreaVisibility;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0001397D File Offset: 0x00011B7D
		// (set) Token: 0x060003BC RID: 956 RVA: 0x00013984 File Offset: 0x00011B84
		public static FilterAreaVisibility DefaultFilterAreaVisibility
		{
			get
			{
				return ReportState._defaultFilterAreaVisibility;
			}
			set
			{
				ReportState._defaultFilterAreaVisibility = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0001398C File Offset: 0x00011B8C
		public List<ReportSectionState> ReportSections
		{
			get
			{
				return this._reportSections;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00013994 File Offset: 0x00011B94
		public double FontScale
		{
			get
			{
				return this._fontScale;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0001399C File Offset: 0x00011B9C
		public string Theme
		{
			get
			{
				return this._theme;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x000139A4 File Offset: 0x00011BA4
		public FilterAreaVisibility FilterAreaVisibility
		{
			get
			{
				return this._filterAreaVisibility;
			}
		}

		// Token: 0x04000240 RID: 576
		private static FilterAreaVisibility _defaultFilterAreaVisibility;

		// Token: 0x04000241 RID: 577
		private readonly List<ReportSectionState> _reportSections;

		// Token: 0x04000242 RID: 578
		private readonly double _fontScale;

		// Token: 0x04000243 RID: 579
		private readonly string _theme;

		// Token: 0x04000244 RID: 580
		private readonly FilterAreaVisibility _filterAreaVisibility;
	}
}
