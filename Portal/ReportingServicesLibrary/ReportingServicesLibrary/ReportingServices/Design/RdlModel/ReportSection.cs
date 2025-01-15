using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000404 RID: 1028
	public abstract class ReportSection
	{
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060020C3 RID: 8387
		[XmlIgnore]
		public abstract SectionType Type { get; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x0007FB36 File Offset: 0x0007DD36
		// (set) Token: 0x060020C5 RID: 8389 RVA: 0x0007FB3E File Offset: 0x0007DD3E
		public Unit Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				Utils.ValidateValueRange("Height", value, new Unit(0), Constants.MaxSectionHeightUnits);
				this.m_height = value;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x0007FB67 File Offset: 0x0007DD67
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style Style
		{
			get
			{
				return this.m_style;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x0007FB6F File Offset: 0x0007DD6F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ReportItemCollection ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x0007FB77 File Offset: 0x0007DD77
		protected ReportSection()
		{
			this.m_reportItems = new ReportItemCollection();
			this.m_style = new Style();
		}

		// Token: 0x04000E53 RID: 3667
		private Unit m_height;

		// Token: 0x04000E54 RID: 3668
		private ReportItemCollection m_reportItems;

		// Token: 0x04000E55 RID: 3669
		private Style m_style;
	}
}
