using System;
using System.IO;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E37 RID: 7735
	public abstract class BufferedMessage : Message
	{
		// Token: 0x0600BE56 RID: 48726 RVA: 0x00268062 File Offset: 0x00266262
		public BufferedMessage()
		{
		}

		// Token: 0x0600BE57 RID: 48727 RVA: 0x0026806A File Offset: 0x0026626A
		public sealed override void Prepare()
		{
			this.buffer = BinarySerializer.Serialize(new Action<BinaryWriter>(this.Serialize));
		}

		// Token: 0x0600BE58 RID: 48728 RVA: 0x00268084 File Offset: 0x00266284
		public sealed override void WriteTo(BinaryWriter writer)
		{
			writer.Write(this.buffer);
		}

		// Token: 0x040060F3 RID: 24819
		private byte[] buffer;
	}
}
