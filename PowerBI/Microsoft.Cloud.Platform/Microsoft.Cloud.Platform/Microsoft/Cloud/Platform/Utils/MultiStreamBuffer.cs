using System;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000294 RID: 660
	public class MultiStreamBuffer
	{
		// Token: 0x060011B1 RID: 4529 RVA: 0x0003DC24 File Offset: 0x0003BE24
		public MultiStreamBuffer([NotNull] byte[] buffer, int position, int count, string delimiter)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<byte[]>(buffer, "buffer");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(position, "position");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(count, "count");
			ExtendedDiagnostics.EnsureArgumentIsBetween(position, 0, buffer.Length - 1, "position");
			ExtendedDiagnostics.EnsureArgumentIsBetween(count, 0, buffer.Length - position, "count");
			this.m_buffer = buffer;
			this.m_position = position;
			Encoding encoding = ExtendedStream.GuessEncoding(buffer, position, out this.m_position);
			this.m_delimiter = encoding.GetBytes(delimiter);
			this.m_positionEnd = position + count;
			this.m_delimiterOffset = ((this.m_delimiter.Length >= 2 && this.m_delimiter[0] == 0) ? 1 : 0);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0003DCCC File Offset: 0x0003BECC
		public MultiStreamBuffer([NotNull] byte[] buffer, int position, int count, byte[] delimiter)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<byte[]>(buffer, "buffer");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(position, "position");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(count, "count");
			ExtendedDiagnostics.EnsureArgumentIsBetween(position, 0, buffer.Length - 1, "position");
			ExtendedDiagnostics.EnsureArgumentIsBetween(count, 0, buffer.Length - position, "count");
			this.m_buffer = buffer;
			this.m_delimiter = delimiter;
			this.m_position = position;
			this.m_positionEnd = position + count;
			this.m_delimiterOffset = ((this.m_delimiter.Length >= 2) ? 1 : 0);
			if (this.m_position < this.m_positionEnd && buffer[this.m_position] == 255)
			{
				this.m_position++;
				if (this.m_position < this.m_positionEnd && buffer[this.m_position] == 254)
				{
					this.m_position++;
				}
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0003DDAC File Offset: 0x0003BFAC
		public Stream TryGetNextStream()
		{
			MemoryStream memoryStream = null;
			while (this.m_position < this.m_positionEnd)
			{
				int num = this.FindDelimiter(this.m_position);
				if (num == -1)
				{
					memoryStream = new MemoryStream(this.m_buffer, this.m_position, this.m_positionEnd - this.m_position, false, false);
					this.m_position = this.m_positionEnd;
					break;
				}
				if (num != this.m_position)
				{
					memoryStream = new MemoryStream(this.m_buffer, this.m_position, num - this.m_position, false, false);
					this.m_position = num + this.m_delimiter.Length;
					break;
				}
				this.m_position += this.m_delimiter.Length;
			}
			return memoryStream;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0003DE60 File Offset: 0x0003C060
		private int FindDelimiter(int position)
		{
			int num = this.m_positionEnd - this.m_delimiter.Length + 1;
			byte b = this.m_delimiter[this.m_delimiterOffset];
			for (int i = position + this.m_delimiterOffset; i < this.m_buffer.Length; i++)
			{
				if (this.m_buffer[i] == b)
				{
					if (i >= num + this.m_delimiterOffset)
					{
						return -1;
					}
					bool flag = true;
					int j = 0;
					int num2 = i - this.m_delimiterOffset;
					while (j < this.m_delimiter.Length)
					{
						if (this.m_buffer[num2] != this.m_delimiter[j])
						{
							flag = false;
							break;
						}
						j++;
						num2++;
					}
					if (flag)
					{
						return i - this.m_delimiterOffset;
					}
				}
			}
			return -1;
		}

		// Token: 0x04000695 RID: 1685
		private readonly byte[] m_buffer;

		// Token: 0x04000696 RID: 1686
		private readonly byte[] m_delimiter;

		// Token: 0x04000697 RID: 1687
		private int m_position;

		// Token: 0x04000698 RID: 1688
		private readonly int m_positionEnd;

		// Token: 0x04000699 RID: 1689
		private readonly int m_delimiterOffset;
	}
}
