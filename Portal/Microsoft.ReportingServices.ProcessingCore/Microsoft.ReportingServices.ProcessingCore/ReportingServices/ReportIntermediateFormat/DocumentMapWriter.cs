using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004C3 RID: 1219
	internal sealed class DocumentMapWriter
	{
		// Token: 0x06003D9A RID: 15770 RVA: 0x001079B0 File Offset: 0x00105BB0
		public DocumentMapWriter(Stream aChunkStream, OnDemandProcessingContext odpContext)
		{
			this.m_chunkStream = aChunkStream;
			this.m_writer = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter(this.m_chunkStream, DocumentMapWriter.m_docMapDeclarations, odpContext.GetActiveCompatibilityVersion(), odpContext.ProhibitSerializableValues);
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x001079E1 File Offset: 0x00105BE1
		public void WriteBeginContainer(string aLabel, string aId)
		{
			Global.Tracer.Assert(!this.m_isClosed, "(!m_isClosed)");
			this.m_level++;
			this.m_writer.Write(DocumentMapBeginContainer.Instance);
			this.WriteNode(aLabel, aId);
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x00107A24 File Offset: 0x00105C24
		public void WriteNode(string aLabel, string aId)
		{
			Global.Tracer.Assert(!this.m_isClosed, "(!m_isClosed)");
			if (this.m_node == null)
			{
				this.m_node = new DocumentMapNode();
			}
			this.m_node.Label = aLabel;
			this.m_node.Id = aId;
			this.m_writer.Write(this.m_node);
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x00107A88 File Offset: 0x00105C88
		public void WriteEndContainer()
		{
			Global.Tracer.Assert(!this.m_isClosed, "(!m_isClosed)");
			this.m_level--;
			Global.Tracer.Assert(this.m_level >= 0, "Mismatched EndContainer");
			this.m_writer.Write(DocumentMapEndContainer.Instance);
			if (this.m_level == 0)
			{
				this.Close();
			}
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x00107AF4 File Offset: 0x00105CF4
		public void Close()
		{
			Global.Tracer.Assert(this.m_level == 0, "Mismatched Container Structure.  There are still open containers");
			this.m_isClosed = true;
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x00107B15 File Offset: 0x00105D15
		public bool IsClosed()
		{
			return this.m_isClosed;
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x00107B1D File Offset: 0x00105D1D
		private static List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration> GetDocumentMapDeclarations()
		{
			return new List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration>(3)
			{
				DocumentMapNode.GetDeclaration(),
				DocumentMapBeginContainer.GetDeclaration(),
				DocumentMapEndContainer.GetDeclaration()
			};
		}

		// Token: 0x04001CBF RID: 7359
		private DocumentMapNode m_node;

		// Token: 0x04001CC0 RID: 7360
		private int m_level;

		// Token: 0x04001CC1 RID: 7361
		private Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter m_writer;

		// Token: 0x04001CC2 RID: 7362
		private Stream m_chunkStream;

		// Token: 0x04001CC3 RID: 7363
		private bool m_isClosed;

		// Token: 0x04001CC4 RID: 7364
		private static List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration> m_docMapDeclarations = DocumentMapWriter.GetDocumentMapDeclarations();
	}
}
