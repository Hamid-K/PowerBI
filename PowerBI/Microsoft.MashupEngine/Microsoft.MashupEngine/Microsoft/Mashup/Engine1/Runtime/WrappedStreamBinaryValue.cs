using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001292 RID: 4754
	internal class WrappedStreamBinaryValue : StreamedBinaryValue
	{
		// Token: 0x06007CEE RID: 31982 RVA: 0x001ACA19 File Offset: 0x001AAC19
		public WrappedStreamBinaryValue(BinaryValue binaryValue, Func<Stream, Stream> adapter)
		{
			this.binaryValue = binaryValue;
			this.adapter = adapter;
		}

		// Token: 0x06007CEF RID: 31983 RVA: 0x001ACA30 File Offset: 0x001AAC30
		public override Stream Open()
		{
			Stream stream = this.binaryValue.Open();
			Stream stream2;
			try
			{
				stream2 = this.adapter(stream);
			}
			catch
			{
				stream.Close();
				throw;
			}
			return stream2;
		}

		// Token: 0x06007CF0 RID: 31984 RVA: 0x001ACA74 File Offset: 0x001AAC74
		public override bool TryGetLength(out long length)
		{
			length = 0L;
			return false;
		}

		// Token: 0x040044DD RID: 17629
		private readonly BinaryValue binaryValue;

		// Token: 0x040044DE RID: 17630
		private readonly Func<Stream, Stream> adapter;
	}
}
