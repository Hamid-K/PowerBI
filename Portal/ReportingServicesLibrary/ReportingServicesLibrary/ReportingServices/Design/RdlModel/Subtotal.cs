using System;
using System.ComponentModel;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003F2 RID: 1010
	public class Subtotal
	{
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x0007F191 File Offset: 0x0007D391
		// (set) Token: 0x0600201B RID: 8219 RVA: 0x0007F199 File Offset: 0x0007D399
		[XmlParentElement("ReportItems")]
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

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x0007F1A2 File Offset: 0x0007D3A2
		// (set) Token: 0x0600201D RID: 8221 RVA: 0x0007F1AA File Offset: 0x0007D3AA
		[DefaultValue("After")]
		public string Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				StringListConverter.ValidReportString("Position", SubtotalPositionStringConverter.StringValuesArray, "After", value, ref this.m_position);
				this.m_position = value;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x0007F1CE File Offset: 0x0007D3CE
		// (set) Token: 0x0600201F RID: 8223 RVA: 0x0007F1D6 File Offset: 0x0007D3D6
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

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x0007F1DF File Offset: 0x0007D3DF
		// (set) Token: 0x06002021 RID: 8225 RVA: 0x0007F1E7 File Offset: 0x0007D3E7
		[DefaultValue(GroupingDataElementOutputs.NoOutput)]
		public GroupingDataElementOutputs DataElementOutput
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

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x0007F1F0 File Offset: 0x0007D3F0
		// (set) Token: 0x06002023 RID: 8227 RVA: 0x0007F1F8 File Offset: 0x0007D3F8
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

		// Token: 0x04000DFE RID: 3582
		private ReportItem m_reportItem;

		// Token: 0x04000DFF RID: 3583
		private string m_position = "After";

		// Token: 0x04000E00 RID: 3584
		private Style m_style;

		// Token: 0x04000E01 RID: 3585
		private string m_dataElementName;

		// Token: 0x04000E02 RID: 3586
		private GroupingDataElementOutputs m_dataElementOutput = GroupingDataElementOutputs.NoOutput;
	}
}
