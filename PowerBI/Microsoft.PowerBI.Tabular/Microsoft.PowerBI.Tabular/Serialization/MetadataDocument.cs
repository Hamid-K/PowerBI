using System;
using System.IO;
using System.Text;
using Microsoft.AnalysisServices.Extensions;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000180 RID: 384
	public sealed class MetadataDocument
	{
		// Token: 0x060017E1 RID: 6113 RVA: 0x000A2D51 File Offset: 0x000A0F51
		internal MetadataDocument(MetadataSerializationStyle style, string logicalPath, MemoryStream content)
		{
			this.Style = style;
			this.LogicalPath = logicalPath;
			content.Seek(0L, SeekOrigin.Begin);
			this.content = content.ToArray();
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x000A2D7D File Offset: 0x000A0F7D
		public MetadataSerializationStyle Style { get; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x000A2D85 File Offset: 0x000A0F85
		public string LogicalPath { get; }

		// Token: 0x060017E4 RID: 6116 RVA: 0x000A2D8D File Offset: 0x000A0F8D
		public void WriteTo(Stream document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			if (!document.CanWrite)
			{
				throw new ArgumentException(TomSR.Exception_InvalidStreamNoWrite, "document");
			}
			this.SaveImpl(document);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x000A2DBC File Offset: 0x000A0FBC
		public void WriteTo(TextWriter writer, Encoding encoding = null)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.SaveImpl(writer, encoding);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000A2DD4 File Offset: 0x000A0FD4
		internal MemoryStream GetContent()
		{
			return new MemoryStream(this.content, false);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x000A2DE4 File Offset: 0x000A0FE4
		internal void SaveImpl(Stream document)
		{
			using (MemoryStream memoryStream = new MemoryStream(this.content, false))
			{
				memoryStream.CopyTo(document);
			}
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x000A2E24 File Offset: 0x000A1024
		internal void SaveImpl(TextWriter writer, Encoding encoding)
		{
			using (MemoryStream memoryStream = new MemoryStream(this.content, false))
			{
				using (StreamReader streamReader = new StreamReader(memoryStream, encoding ?? MetadataFormattingOptions.GetEffectiveEncoding(), true, 1024, true))
				{
					streamReader.CopyTo(writer, false);
				}
			}
		}

		// Token: 0x04000458 RID: 1112
		internal const int DefaultBufferSize = 1024;

		// Token: 0x04000459 RID: 1113
		private readonly byte[] content;
	}
}
