using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000198 RID: 408
	internal sealed class GetReportDefinitionActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x000366C5 File Offset: 0x000348C5
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x000366CD File Offset: 0x000348CD
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x000366D6 File Offset: 0x000348D6
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x000366DE File Offset: 0x000348DE
		public byte[] ReportDefinition
		{
			get
			{
				return this.m_reportDefinition;
			}
			set
			{
				this.m_reportDefinition = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x000366E7 File Offset: 0x000348E7
		// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x000366EF File Offset: 0x000348EF
		public ItemType ItemType
		{
			get
			{
				return this.m_itemType;
			}
			set
			{
				this.m_itemType = value;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x000366F8 File Offset: 0x000348F8
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00036700 File Offset: 0x00034900
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
		}

		// Token: 0x04000611 RID: 1553
		private string m_itemPath;

		// Token: 0x04000612 RID: 1554
		private byte[] m_reportDefinition;

		// Token: 0x04000613 RID: 1555
		private ItemType m_itemType;
	}
}
