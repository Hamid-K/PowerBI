using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CF3 RID: 7411
	internal class MessageBasedOutputStream : ChunkedOutputStream
	{
		// Token: 0x0600B907 RID: 47367 RVA: 0x002581D0 File Offset: 0x002563D0
		public MessageBasedOutputStream(IMessageChannel channel)
			: base(16384, 65536)
		{
			this.channel = channel;
		}

		// Token: 0x0600B908 RID: 47368 RVA: 0x002581E9 File Offset: 0x002563E9
		protected override void WriteNextChunk(byte[] buffer)
		{
			if (this.channel == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			this.channel.Post(new MessageBasedOutputStream.BinaryChunkMessage
			{
				Chunk = buffer
			});
		}

		// Token: 0x0600B909 RID: 47369 RVA: 0x0025821C File Offset: 0x0025641C
		protected override void Dispose(bool disposing)
		{
			try
			{
				base.Dispose(disposing);
				if (disposing && this.channel != null)
				{
					this.WriteNextChunk(new byte[0]);
				}
			}
			finally
			{
				this.channel = null;
			}
		}

		// Token: 0x04005E32 RID: 24114
		public const int StartChunkSize = 16384;

		// Token: 0x04005E33 RID: 24115
		private const int maxChunkSize = 65536;

		// Token: 0x04005E34 RID: 24116
		private IMessageChannel channel;

		// Token: 0x02001CF4 RID: 7412
		public sealed class BinaryChunkMessage : UnbufferedMessage
		{
			// Token: 0x17002DC7 RID: 11719
			// (get) Token: 0x0600B90A RID: 47370 RVA: 0x00258264 File Offset: 0x00256464
			// (set) Token: 0x0600B90B RID: 47371 RVA: 0x0025826C File Offset: 0x0025646C
			public byte[] Chunk { get; set; }

			// Token: 0x0600B90C RID: 47372 RVA: 0x00258275 File Offset: 0x00256475
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteByteArray(this.Chunk);
			}

			// Token: 0x0600B90D RID: 47373 RVA: 0x00258283 File Offset: 0x00256483
			public override void Deserialize(BinaryReader reader)
			{
				this.Chunk = reader.ReadByteArray();
			}
		}
	}
}
