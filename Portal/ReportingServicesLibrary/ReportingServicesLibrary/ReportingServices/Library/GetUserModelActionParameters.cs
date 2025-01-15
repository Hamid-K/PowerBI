using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200014A RID: 330
	internal sealed class GetUserModelActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0002F4EE File Offset: 0x0002D6EE
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x0002F4F6 File Offset: 0x0002D6F6
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

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0002F4FF File Offset: 0x0002D6FF
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x0002F507 File Offset: 0x0002D707
		public string PerspectiveID
		{
			get
			{
				return this.m_perspectiveID;
			}
			set
			{
				this.m_perspectiveID = value;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0002F510 File Offset: 0x0002D710
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.ItemPath, this.PerspectiveID);
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002F52D File Offset: 0x0002D72D
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("Model");
			}
		}

		// Token: 0x04000526 RID: 1318
		private string m_itemPath;

		// Token: 0x04000527 RID: 1319
		private string m_perspectiveID;
	}
}
