using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200067E RID: 1662
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class DocumentMapNodeInfo
	{
		// Token: 0x1700204D RID: 8269
		// (get) Token: 0x06005B20 RID: 23328 RVA: 0x00176966 File Offset: 0x00174B66
		public string Label
		{
			get
			{
				return this.m_label;
			}
		}

		// Token: 0x1700204E RID: 8270
		// (get) Token: 0x06005B21 RID: 23329 RVA: 0x0017696E File Offset: 0x00174B6E
		public string Id
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x1700204F RID: 8271
		// (get) Token: 0x06005B22 RID: 23330 RVA: 0x00176976 File Offset: 0x00174B76
		public DocumentMapNodeInfo[] Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x0017697E File Offset: 0x00174B7E
		internal DocumentMapNodeInfo(DocumentMapNode docMapNode, DocumentMapNodeInfo[] children)
		{
			this.m_id = docMapNode.Id;
			this.m_label = docMapNode.Label;
			this.m_children = children;
		}

		// Token: 0x04002F6B RID: 12139
		private string m_id;

		// Token: 0x04002F6C RID: 12140
		private string m_label;

		// Token: 0x04002F6D RID: 12141
		private DocumentMapNodeInfo[] m_children;
	}
}
