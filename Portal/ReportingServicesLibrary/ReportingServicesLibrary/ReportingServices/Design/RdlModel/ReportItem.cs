using System;
using System.ComponentModel;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003FF RID: 1023
	[XmlElementClass("Textbox", typeof(TextboxItem))]
	[XmlElementClass("Rectangle", typeof(RectangleItem))]
	[XmlElementClass("List", typeof(ListItem))]
	[XmlElementClass("Table", typeof(TableItem))]
	[XmlElementClass("Image", typeof(Image))]
	[XmlElementClass("Line", typeof(LineItem))]
	[XmlElementClass("Subreport", typeof(SubreportItem))]
	[XmlElementClass("Chart", typeof(ChartItem))]
	[XmlElementClass("Matrix", typeof(MatrixItem))]
	public abstract class ReportItem
	{
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x0007F839 File Offset: 0x0007DA39
		// (set) Token: 0x06002092 RID: 8338 RVA: 0x0007F841 File Offset: 0x0007DA41
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x0007F84A File Offset: 0x0007DA4A
		// (set) Token: 0x06002094 RID: 8340 RVA: 0x0007F852 File Offset: 0x0007DA52
		public Unit Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				if (this.m_width != value)
				{
					Utils.ValidateValueRange("Width", value, new Unit(0), Constants.MaxUnits);
					this.m_width = value;
				}
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x0007F889 File Offset: 0x0007DA89
		// (set) Token: 0x06002096 RID: 8342 RVA: 0x0007F891 File Offset: 0x0007DA91
		public Unit Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				if (this.m_height != value)
				{
					Utils.ValidateValueRange("Height", value, new Unit(0), Constants.MaxUnits);
					this.m_height = value;
				}
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x0007F8C8 File Offset: 0x0007DAC8
		// (set) Token: 0x06002098 RID: 8344 RVA: 0x0007F8D0 File Offset: 0x0007DAD0
		public Unit Left
		{
			get
			{
				return this.m_left;
			}
			set
			{
				if (this.m_left != value)
				{
					Utils.ValidateValueRange("Left", value, new Unit(0), Constants.MaxUnits);
					this.m_left = value;
				}
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x0007F907 File Offset: 0x0007DB07
		// (set) Token: 0x0600209A RID: 8346 RVA: 0x0007F90F File Offset: 0x0007DB0F
		public Unit Top
		{
			get
			{
				return this.m_top;
			}
			set
			{
				if (this.m_top != value)
				{
					Utils.ValidateValueRange("Top", value, new Unit(0), Constants.MaxSectionHeightUnits);
					this.m_top = value;
				}
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x0007F946 File Offset: 0x0007DB46
		// (set) Token: 0x0600209C RID: 8348 RVA: 0x0007F94E File Offset: 0x0007DB4E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style Style
		{
			get
			{
				return this.m_style;
			}
			set
			{
				this.m_style = value;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x0007F957 File Offset: 0x0007DB57
		// (set) Token: 0x0600209E RID: 8350 RVA: 0x0007F95F File Offset: 0x0007DB5F
		[DefaultValue("")]
		public string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x0007F968 File Offset: 0x0007DB68
		// (set) Token: 0x060020A0 RID: 8352 RVA: 0x0007F970 File Offset: 0x0007DB70
		[DefaultValue("")]
		public string Bookmark
		{
			get
			{
				return this.m_bookmark;
			}
			set
			{
				this.m_bookmark = value;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x0007F979 File Offset: 0x0007DB79
		// (set) Token: 0x060020A2 RID: 8354 RVA: 0x0007F981 File Offset: 0x0007DB81
		[DefaultValue("")]
		public string RepeatWith
		{
			get
			{
				return this.m_repeatWith;
			}
			set
			{
				this.m_repeatWith = value;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x0007F98A File Offset: 0x0007DB8A
		// (set) Token: 0x060020A4 RID: 8356 RVA: 0x0007F992 File Offset: 0x0007DB92
		[DefaultValue("")]
		public string ToolTip
		{
			get
			{
				return this.m_toolTip;
			}
			set
			{
				this.m_toolTip = value;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x0007F99B File Offset: 0x0007DB9B
		// (set) Token: 0x060020A6 RID: 8358 RVA: 0x0007F9A3 File Offset: 0x0007DBA3
		[DefaultValue(0)]
		public int ZIndex
		{
			get
			{
				return this.m_zindex;
			}
			set
			{
				Utils.ValidateValueRange("ZIndex", value, 0, null);
				this.m_zindex = value;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x0007F9BE File Offset: 0x0007DBBE
		// (set) Token: 0x060020A8 RID: 8360 RVA: 0x0007F9C6 File Offset: 0x0007DBC6
		public Visibility Visibility
		{
			get
			{
				return this.m_visibility;
			}
			set
			{
				this.m_visibility = value;
				if (this.m_visibility != null)
				{
					this.m_visibility.Owner = this;
				}
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x0007F9E3 File Offset: 0x0007DBE3
		// (set) Token: 0x060020AA RID: 8362 RVA: 0x0007F9EB File Offset: 0x0007DBEB
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x0007F9F4 File Offset: 0x0007DBF4
		// (set) Token: 0x060020AC RID: 8364 RVA: 0x0007F9FC File Offset: 0x0007DBFC
		[DefaultValue(DataElementOutputs.Auto)]
		public DataElementOutputs DataElementOutput
		{
			get
			{
				return this.m_dataElementOutput;
			}
			set
			{
				this.m_dataElementOutput = value;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x0007FA05 File Offset: 0x0007DC05
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customProperties == null)
				{
					this.m_customProperties = new CustomPropertyCollection();
				}
				return this.m_customProperties;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x0007F84A File Offset: 0x0007DA4A
		// (set) Token: 0x060020AF RID: 8367 RVA: 0x0007FA20 File Offset: 0x0007DC20
		internal Unit RawWidth
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x0007F889 File Offset: 0x0007DA89
		// (set) Token: 0x060020B1 RID: 8369 RVA: 0x0007FA29 File Offset: 0x0007DC29
		internal Unit RawHeight
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x0007FA32 File Offset: 0x0007DC32
		protected ReportItem()
		{
			this.m_style = new Style();
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000053DC File Offset: 0x000035DC
		protected virtual bool ShouldSerializeLeft()
		{
			return true;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000053DC File Offset: 0x000035DC
		protected virtual bool ShouldSerializeTop()
		{
			return true;
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000053DC File Offset: 0x000035DC
		protected virtual bool ShouldSerializeWidth()
		{
			return true;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000053DC File Offset: 0x000035DC
		protected virtual bool ShouldSerializeHeight()
		{
			return true;
		}

		// Token: 0x04000E3D RID: 3645
		private string m_name;

		// Token: 0x04000E3E RID: 3646
		private Style m_style;

		// Token: 0x04000E3F RID: 3647
		private Unit m_height;

		// Token: 0x04000E40 RID: 3648
		private Unit m_width;

		// Token: 0x04000E41 RID: 3649
		private Unit m_left;

		// Token: 0x04000E42 RID: 3650
		private Unit m_top;

		// Token: 0x04000E43 RID: 3651
		private string m_label;

		// Token: 0x04000E44 RID: 3652
		private string m_bookmark;

		// Token: 0x04000E45 RID: 3653
		private string m_repeatWith;

		// Token: 0x04000E46 RID: 3654
		private string m_toolTip;

		// Token: 0x04000E47 RID: 3655
		private int m_zindex;

		// Token: 0x04000E48 RID: 3656
		private Visibility m_visibility;

		// Token: 0x04000E49 RID: 3657
		private string m_dataElementName;

		// Token: 0x04000E4A RID: 3658
		private DataElementOutputs m_dataElementOutput;

		// Token: 0x04000E4B RID: 3659
		private CustomPropertyCollection m_customProperties;
	}
}
