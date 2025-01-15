using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000039 RID: 57
	internal sealed class GetExcelWorkbookContentsActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000E813 File Offset: 0x0000CA13
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000E81B File Offset: 0x0000CA1B
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000E824 File Offset: 0x0000CA24
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000E82C File Offset: 0x0000CA2C
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000E835 File Offset: 0x0000CA35
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000E83D File Offset: 0x0000CA3D
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000E845 File Offset: 0x0000CA45
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

		// Token: 0x060001BF RID: 447 RVA: 0x0000E84E File Offset: 0x0000CA4E
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
		}

		// Token: 0x04000131 RID: 305
		private string m_itemPath;

		// Token: 0x04000132 RID: 306
		private byte[] m_content;

		// Token: 0x04000133 RID: 307
		private string m_mimeType;
	}
}
