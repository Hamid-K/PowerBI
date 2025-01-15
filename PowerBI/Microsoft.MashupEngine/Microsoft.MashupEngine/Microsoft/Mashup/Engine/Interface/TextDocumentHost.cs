using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200002B RID: 43
	public sealed class TextDocumentHost : ICacheableDocumentHost, IDocumentHost
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00002F19 File Offset: 0x00001119
		public TextDocumentHost(string text)
			: this(SegmentedString.New(text))
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002F27 File Offset: 0x00001127
		public TextDocumentHost(SegmentedString text)
		{
			this.segmentedText = text;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000020FA File Offset: 0x000002FA
		public string UniqueID
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00002F36 File Offset: 0x00001136
		public object CacheIdentity
		{
			get
			{
				return this.segmentedText;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002F43 File Offset: 0x00001143
		public SegmentedString SegmentedText
		{
			get
			{
				return this.segmentedText;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00002F4C File Offset: 0x0000114C
		public string Text
		{
			get
			{
				return this.segmentedText.ToString();
			}
		}

		// Token: 0x04000086 RID: 134
		private readonly SegmentedString segmentedText;
	}
}
