using System;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000398 RID: 920
	internal class ByteBuffer
	{
		// Token: 0x060020AF RID: 8367 RVA: 0x00063F2C File Offset: 0x0006212C
		internal ByteBuffer(byte[][] buffer)
		{
			this._buffer = buffer;
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00063F3C File Offset: 0x0006213C
		public override string ToString()
		{
			if (this._buffer == null || this._buffer.Length == 0)
			{
				return null;
			}
			int num = 1;
			int num2 = Math.Min(num * this._buffer[0].Length * 4, 65536);
			StringBuilder stringBuilder = new StringBuilder(num2);
			stringBuilder.AppendLine();
			for (int i = 0; i < num; i++)
			{
				int num3 = 0;
				while (num3 < this._buffer[i].Length && stringBuilder.Length < num2)
				{
					stringBuilder.Append(this._buffer[i][num3]);
					stringBuilder.Append(';');
					num3++;
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400131F RID: 4895
		private byte[][] _buffer;
	}
}
