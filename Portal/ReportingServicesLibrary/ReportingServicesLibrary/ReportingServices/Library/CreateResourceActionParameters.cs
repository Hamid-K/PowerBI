using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001CC RID: 460
	internal sealed class CreateResourceActionParameters : CreateItemActionParameters
	{
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x000390A4 File Offset: 0x000372A4
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x000390AC File Offset: 0x000372AC
		public byte[] Content
		{
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x000390B5 File Offset: 0x000372B5
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x000390BD File Offset: 0x000372BD
		public string MimeType
		{
			get
			{
				return this.m_mimeType;
			}
			set
			{
				this.m_mimeType = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x000390C6 File Offset: 0x000372C6
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x000390CE File Offset: 0x000372CE
		public string SubType
		{
			get
			{
				return this.m_subType;
			}
			set
			{
				this.m_subType = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x000390D8 File Offset: 0x000372D8
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}, {3}, {4}", new object[] { base.ItemName, base.ParentPath, base.Overwrite, this.MimeType, this.SubType });
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0003912C File Offset: 0x0003732C
		internal override void Validate()
		{
			if (base.ItemName == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "Name" : "Resource");
			}
			if (base.ParentPath == null)
			{
				throw new MissingParameterException("Parent");
			}
			if (this.Content == null)
			{
				throw new MissingParameterException("Contents");
			}
			if (this.MimeType == null)
			{
				throw new MissingParameterException("MimeType");
			}
		}

		// Token: 0x0400064C RID: 1612
		private byte[] m_content;

		// Token: 0x0400064D RID: 1613
		private string m_mimeType;

		// Token: 0x0400064E RID: 1614
		private string m_subType;
	}
}
