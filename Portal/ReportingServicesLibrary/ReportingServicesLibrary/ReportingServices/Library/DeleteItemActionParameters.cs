using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000AF RID: 175
	internal sealed class DeleteItemActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00020BF4 File Offset: 0x0001EDF4
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x00020BFC File Offset: 0x0001EDFC
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

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00020C05 File Offset: 0x0001EE05
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x00020C0D File Offset: 0x0001EE0D
		internal bool SkipSecurityCheck
		{
			get
			{
				return this.m_skipSecurityCheck;
			}
			set
			{
				this.m_skipSecurityCheck = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x00020C16 File Offset: 0x0001EE16
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00020C1E File Offset: 0x0001EE1E
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Item");
			}
		}

		// Token: 0x0400040E RID: 1038
		private string m_itemPath;

		// Token: 0x0400040F RID: 1039
		private bool m_skipSecurityCheck;
	}
}
