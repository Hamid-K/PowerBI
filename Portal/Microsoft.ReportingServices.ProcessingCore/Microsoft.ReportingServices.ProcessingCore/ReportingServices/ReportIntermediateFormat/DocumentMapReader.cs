using System;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004C2 RID: 1218
	internal sealed class DocumentMapReader
	{
		// Token: 0x06003D95 RID: 15765 RVA: 0x00107874 File Offset: 0x00105A74
		public DocumentMapReader(Stream chunkStream)
		{
			this.m_chunkStream = chunkStream;
			ProcessingRIFObjectCreator processingRIFObjectCreator = new ProcessingRIFObjectCreator(null, null);
			this.m_rifReader = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader(this.m_chunkStream, processingRIFObjectCreator);
			this.m_startIndex = this.m_rifReader.ObjectStartPosition;
			this.m_level = 1;
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x001078C0 File Offset: 0x00105AC0
		public bool MoveNext()
		{
			if (this.m_rifReader.EOS)
			{
				return false;
			}
			this.m_currentNode = null;
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable persistable = this.m_rifReader.ReadRIFObject();
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType = persistable.GetObjectType();
			if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DocumentMapNode)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DocumentMapNode documentMapNode = (Microsoft.ReportingServices.ReportIntermediateFormat.DocumentMapNode)persistable;
				this.m_currentNode = new Microsoft.ReportingServices.OnDemandReportRendering.DocumentMapNode(documentMapNode.Label, documentMapNode.Id, this.m_level);
				return true;
			}
			if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DocumentMapBeginContainer)
			{
				bool flag = this.MoveNext();
				this.m_level++;
				return flag;
			}
			if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DocumentMapEndContainer)
			{
				Global.Tracer.Assert(false);
				return false;
			}
			this.m_level--;
			return this.MoveNext();
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x0010796C File Offset: 0x00105B6C
		public void Reset()
		{
			this.m_chunkStream.Seek(this.m_startIndex, SeekOrigin.Begin);
			this.m_level = 1;
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x00107988 File Offset: 0x00105B88
		public void Close()
		{
			this.m_chunkStream.Close();
		}

		// Token: 0x17001A45 RID: 6725
		// (get) Token: 0x06003D99 RID: 15769 RVA: 0x00107995 File Offset: 0x00105B95
		public Microsoft.ReportingServices.OnDemandReportRendering.DocumentMapNode Current
		{
			get
			{
				if (this.m_currentNode == null)
				{
					throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
				}
				return this.m_currentNode;
			}
		}

		// Token: 0x04001CBA RID: 7354
		private Microsoft.ReportingServices.OnDemandReportRendering.DocumentMapNode m_currentNode;

		// Token: 0x04001CBB RID: 7355
		private Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader m_rifReader;

		// Token: 0x04001CBC RID: 7356
		private int m_level;

		// Token: 0x04001CBD RID: 7357
		private long m_startIndex;

		// Token: 0x04001CBE RID: 7358
		private Stream m_chunkStream;
	}
}
