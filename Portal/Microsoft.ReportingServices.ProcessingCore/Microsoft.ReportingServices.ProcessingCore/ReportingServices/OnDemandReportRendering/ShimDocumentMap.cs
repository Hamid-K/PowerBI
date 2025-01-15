using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002B8 RID: 696
	internal sealed class ShimDocumentMap : DocumentMap, IDocumentMap, IEnumerator<OnDemandDocumentMapNode>, IDisposable, IEnumerator
	{
		// Token: 0x06001A90 RID: 6800 RVA: 0x0006B019 File Offset: 0x00069219
		internal ShimDocumentMap(DocumentMapNode aOldDocMap)
		{
			this.m_oldDocMap = aOldDocMap;
			this.Reset();
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0006B02E File Offset: 0x0006922E
		public override void Close()
		{
			this.m_oldDocMap = null;
			this.m_isClosed = true;
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x0006B03E File Offset: 0x0006923E
		OnDemandDocumentMapNode IEnumerator<OnDemandDocumentMapNode>.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0006B046 File Offset: 0x00069246
		public override DocumentMapNode Current
		{
			get
			{
				if (this.m_current == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return this.m_current;
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0006B061 File Offset: 0x00069261
		public override void Dispose()
		{
			this.Close();
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0006B06C File Offset: 0x0006926C
		public override bool MoveNext()
		{
			this.m_current = null;
			if (this.m_nodeInfoStack == null)
			{
				this.m_nodeInfoStack = new Stack<IEnumerator<DocumentMapNode>>();
				this.m_current = new DocumentMapNode(this.m_oldDocMap.Label, this.m_oldDocMap.Id, this.m_nodeInfoStack.Count + 1);
				this.m_nodeInfoStack.Push(((IEnumerable<DocumentMapNode>)this.m_oldDocMap.Children).GetEnumerator());
				return true;
			}
			if (this.m_nodeInfoStack.Count == 0)
			{
				return false;
			}
			while (this.m_nodeInfoStack.Count > 0 && !this.m_nodeInfoStack.Peek().MoveNext())
			{
				this.m_nodeInfoStack.Pop();
			}
			if (this.m_nodeInfoStack.Count == 0)
			{
				return false;
			}
			DocumentMapNode documentMapNode = this.m_nodeInfoStack.Peek().Current;
			this.m_current = new DocumentMapNode(documentMapNode.Label, documentMapNode.Id, this.m_nodeInfoStack.Count + 1);
			if (documentMapNode.Children != null && documentMapNode.Children.Length != 0)
			{
				this.m_nodeInfoStack.Push(((IEnumerable<DocumentMapNode>)documentMapNode.Children).GetEnumerator());
			}
			return true;
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0006B185 File Offset: 0x00069385
		public override void Reset()
		{
			this.m_current = null;
			this.m_nodeInfoStack = null;
		}

		// Token: 0x04000D38 RID: 3384
		private DocumentMapNode m_oldDocMap;

		// Token: 0x04000D39 RID: 3385
		private DocumentMapNode m_current;

		// Token: 0x04000D3A RID: 3386
		private Stack<IEnumerator<DocumentMapNode>> m_nodeInfoStack;
	}
}
