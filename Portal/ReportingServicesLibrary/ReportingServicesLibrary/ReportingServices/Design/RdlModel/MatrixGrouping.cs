using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003ED RID: 1005
	public abstract class MatrixGrouping : GroupingBase
	{
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x0007EF74 File Offset: 0x0007D174
		// (set) Token: 0x06001FF3 RID: 8179 RVA: 0x0007EFCC File Offset: 0x0007D1CC
		protected DynamicColumnsRows DynamicElements
		{
			get
			{
				if (this.m_isStatic)
				{
					return null;
				}
				return new DynamicColumnsRows
				{
					Grouping = base.Grouping,
					Sorting = base.Sorting,
					Visibility = base.Visibility,
					Subtotal = this.m_subtotal,
					ReportItem = this.m_reportItem
				};
			}
			set
			{
				this.m_isStatic = false;
				base.Grouping = value.Grouping;
				base.Sorting = value.Sorting;
				base.Visibility = value.Visibility;
				this.Subtotal = value.Subtotal;
				this.m_reportItem = value.ReportItem;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x0007F01C File Offset: 0x0007D21C
		// (set) Token: 0x06001FF5 RID: 8181 RVA: 0x0007F02E File Offset: 0x0007D22E
		protected List<StaticColumnRow> StaticElements
		{
			get
			{
				if (!this.m_isStatic)
				{
					return null;
				}
				return this.m_staticElements;
			}
			set
			{
				this.m_isStatic = true;
				this.m_staticElements = value;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x0007F03E File Offset: 0x0007D23E
		// (set) Token: 0x06001FF7 RID: 8183 RVA: 0x0007F046 File Offset: 0x0007D246
		[XmlIgnore]
		public ReportItem ReportItem
		{
			get
			{
				return this.m_reportItem;
			}
			set
			{
				this.m_reportItem = value;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x0007F04F File Offset: 0x0007D24F
		// (set) Token: 0x06001FF9 RID: 8185 RVA: 0x0007F057 File Offset: 0x0007D257
		[XmlIgnore]
		public Subtotal Subtotal
		{
			get
			{
				return this.m_subtotal;
			}
			set
			{
				this.m_subtotal = value;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x0007F060 File Offset: 0x0007D260
		[XmlIgnore]
		public bool IsStatic
		{
			get
			{
				return this.m_isStatic;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x0007F068 File Offset: 0x0007D268
		// (set) Token: 0x06001FFC RID: 8188 RVA: 0x0007F070 File Offset: 0x0007D270
		[DefaultValue(false)]
		public bool FixedHeader
		{
			get
			{
				return this.m_fixedHeader;
			}
			set
			{
				this.m_fixedHeader = value;
			}
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x0007F079 File Offset: 0x0007D279
		public MatrixGrouping()
		{
		}

		// Token: 0x04000DF0 RID: 3568
		private bool m_isStatic;

		// Token: 0x04000DF1 RID: 3569
		private ReportItem m_reportItem;

		// Token: 0x04000DF2 RID: 3570
		private Subtotal m_subtotal;

		// Token: 0x04000DF3 RID: 3571
		private bool m_fixedHeader;

		// Token: 0x04000DF4 RID: 3572
		protected DynamicColumnsRows m_dynamicElements;

		// Token: 0x04000DF5 RID: 3573
		protected List<StaticColumnRow> m_staticElements;
	}
}
