using System;
using System.IO;
using Microsoft.Mashup.Common;

namespace Microsoft.Internal
{
	// Token: 0x020001C6 RID: 454
	internal class ReadFullBufferStream : DelegatingStream
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x0000FF57 File Offset: 0x0000E157
		public ReadFullBufferStream(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x000116A0 File Offset: 0x0000F8A0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			while (count > 0)
			{
				int num2 = this.Stream.Read(buffer, offset, count);
				if (num2 == 0)
				{
					break;
				}
				num += num2;
				offset += num2;
				count -= num2;
			}
			return num;
		}
	}
}
