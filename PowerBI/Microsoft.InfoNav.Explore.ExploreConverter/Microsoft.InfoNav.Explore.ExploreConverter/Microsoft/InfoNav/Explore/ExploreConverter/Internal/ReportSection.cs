using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000AD RID: 173
	internal sealed class ReportSection
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x000137BA File Offset: 0x000119BA
		internal ReportSection(string name, string dataSourceName, List<ReportItem> reportItems, ReportSize width, ReportSize height, Style style, BackgroundImage backgroundImage)
		{
			this._name = name;
			this._dataSourceName = dataSourceName;
			this._reportItems = reportItems;
			this._width = width;
			this._height = height;
			this._style = style;
			this._backgroundImage = backgroundImage;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x000137F7 File Offset: 0x000119F7
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003AA RID: 938 RVA: 0x000137FF File Offset: 0x000119FF
		public List<ReportItem> ReportItems
		{
			get
			{
				return this._reportItems;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00013807 File Offset: 0x00011A07
		public ReportSize Width
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0001380F File Offset: 0x00011A0F
		public ReportSize Height
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00013817 File Offset: 0x00011A17
		public Style Style
		{
			get
			{
				return this._style;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0001381F File Offset: 0x00011A1F
		public BackgroundImage BackgroundImage
		{
			get
			{
				return this._backgroundImage;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00013827 File Offset: 0x00011A27
		public string DataSourceName
		{
			get
			{
				return this._dataSourceName;
			}
		}

		// Token: 0x04000234 RID: 564
		private readonly string _name;

		// Token: 0x04000235 RID: 565
		private readonly string _dataSourceName;

		// Token: 0x04000236 RID: 566
		private readonly List<ReportItem> _reportItems;

		// Token: 0x04000237 RID: 567
		private readonly ReportSize _width;

		// Token: 0x04000238 RID: 568
		private readonly ReportSize _height;

		// Token: 0x04000239 RID: 569
		private readonly Style _style;

		// Token: 0x0400023A RID: 570
		private readonly BackgroundImage _backgroundImage;
	}
}
