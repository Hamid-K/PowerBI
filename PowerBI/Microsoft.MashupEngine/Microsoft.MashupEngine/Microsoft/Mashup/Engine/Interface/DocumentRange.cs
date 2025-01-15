using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000019 RID: 25
	public struct DocumentRange : IEquatable<DocumentRange>
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002BCC File Offset: 0x00000DCC
		public DocumentRange(IDocument document, ISyntaxNode syntaxNode)
		{
			this.document = document;
			this.syntaxNode = syntaxNode;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002BDC File Offset: 0x00000DDC
		public IDocument Document
		{
			get
			{
				return this.document;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public TokenRange Range
		{
			get
			{
				return this.syntaxNode.Range;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002BF1 File Offset: 0x00000DF1
		public ISyntaxNode SyntaxNode
		{
			get
			{
				return this.syntaxNode;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002BF9 File Offset: 0x00000DF9
		public override bool Equals(object obj)
		{
			return obj is DocumentRange && this.Equals((DocumentRange)obj);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002C14 File Offset: 0x00000E14
		public override int GetHashCode()
		{
			return this.document.GetHashCode() ^ this.Range.GetHashCode();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002C41 File Offset: 0x00000E41
		public bool Equals(DocumentRange other)
		{
			return this.document == other.Document && this.Range == other.Range;
		}

		// Token: 0x04000075 RID: 117
		private IDocument document;

		// Token: 0x04000076 RID: 118
		private ISyntaxNode syntaxNode;
	}
}
