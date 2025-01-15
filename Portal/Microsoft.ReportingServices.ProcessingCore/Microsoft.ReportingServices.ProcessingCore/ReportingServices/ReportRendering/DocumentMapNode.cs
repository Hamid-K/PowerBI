using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200005A RID: 90
	internal sealed class DocumentMapNode
	{
		// Token: 0x06000681 RID: 1665 RVA: 0x000191E8 File Offset: 0x000173E8
		internal DocumentMapNode(DocumentMapNode underlyingNode)
		{
			Global.Tracer.Assert(underlyingNode != null, "The document map node being wrapped cannot be null.");
			this.m_underlyingNode = underlyingNode;
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001920A File Offset: 0x0001740A
		public string Label
		{
			get
			{
				return this.m_underlyingNode.Label;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00019217 File Offset: 0x00017417
		public string UniqueName
		{
			get
			{
				return this.m_underlyingNode.Id;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00019224 File Offset: 0x00017424
		public int Page
		{
			get
			{
				return this.m_underlyingNode.Page;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00019231 File Offset: 0x00017431
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x00019239 File Offset: 0x00017439
		public object NonPersistedRenderingInfo
		{
			get
			{
				return this.m_nonPersistedRenderingInfo;
			}
			set
			{
				this.m_nonPersistedRenderingInfo = value;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00019244 File Offset: 0x00017444
		public DocumentMapNode[] Children
		{
			get
			{
				if (this.m_childrenWrappers == null)
				{
					DocumentMapNode[] children = this.m_underlyingNode.Children;
					if (children != null)
					{
						this.m_childrenWrappers = new DocumentMapNode[children.Length];
						for (int i = 0; i < children.Length; i++)
						{
							this.m_childrenWrappers[i] = new DocumentMapNode(children[i]);
						}
					}
				}
				return this.m_childrenWrappers;
			}
		}

		// Token: 0x040001AF RID: 431
		private DocumentMapNode m_underlyingNode;

		// Token: 0x040001B0 RID: 432
		private DocumentMapNode[] m_childrenWrappers;

		// Token: 0x040001B1 RID: 433
		private object m_nonPersistedRenderingInfo;
	}
}
