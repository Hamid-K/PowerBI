using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000069 RID: 105
	public struct EmbeddingReference
	{
		// Token: 0x060001AA RID: 426 RVA: 0x00002FBF File Offset: 0x000011BF
		public EmbeddingReference(string embedding, TextRange range)
		{
			this.embedding = embedding;
			this.range = range;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00002FCF File Offset: 0x000011CF
		public string Embedding
		{
			get
			{
				return this.embedding;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00002FD7 File Offset: 0x000011D7
		public TextRange Range
		{
			get
			{
				return this.range;
			}
		}

		// Token: 0x04000142 RID: 322
		private readonly string embedding;

		// Token: 0x04000143 RID: 323
		private readonly TextRange range;
	}
}
