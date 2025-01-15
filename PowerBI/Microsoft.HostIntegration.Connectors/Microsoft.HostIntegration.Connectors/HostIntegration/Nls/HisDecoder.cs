using System;
using System.Text;
using Microsoft.HostIntegration.StrictResources.Nls;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000629 RID: 1577
	public class HisDecoder : Decoder
	{
		// Token: 0x06003519 RID: 13593 RVA: 0x000B17A4 File Offset: 0x000AF9A4
		internal HisDecoder(HisEncoding src, HisConverter convert)
		{
			this.encoding = src;
			this.converter = convert;
			this.Reset();
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x000B17C0 File Offset: 0x000AF9C0
		public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			int num = 0;
			bool flag = false;
			int num2 = byteCount;
			int num3 = byteIndex;
			bytesUsed = 0;
			charsUsed = 0;
			completed = false;
			int num4 = 0;
			if (byteCount > bytes.Length || byteIndex > byteCount || byteIndex < 0 || charIndex < 0 || charCount > chars.Length || charIndex > chars.Length)
			{
				if (byteCount > bytes.Length)
				{
					throw new ArgumentException(SR.InvalidParameter("byteCount"));
				}
				if (byteIndex > byteCount || byteIndex < 0)
				{
					throw new ArgumentException(SR.InvalidParameter("byteIndex"));
				}
				if (charIndex > chars.Length || charIndex < 0)
				{
					throw new ArgumentException(SR.InvalidParameter("charIndex"));
				}
				if (charCount > chars.Length)
				{
					throw new ArgumentException(SR.InvalidParameter("charCount"));
				}
			}
			if (this.remainingBuffer == null)
			{
				this.remainingBuffer = new char[0];
			}
			byte[] array;
			if (this.remainingBytesBuffer != null && this.remainingBytesBuffer.Length != 0)
			{
				array = new byte[byteCount + 1];
				Array.Copy(this.remainingBytesBuffer, 0, array, 0, 1);
				Array.Copy(bytes, byteIndex, array, 1, byteCount);
				num2 = byteCount + 1;
				num3 = 0;
				this.remainingBytesBuffer = null;
			}
			else
			{
				array = bytes;
			}
			char[] array2 = this.converter.EbcdicToUnicode(array, num3, num2, flush, ref num, ref this.byteCountState, ref this.byteState, ref flag);
			this.converter.GetDoubleByteCutOffIndex(array, num3, num2, ref num4, ref this.parseState);
			if (num2 > num4)
			{
				if (this.remainingBytesBuffer == null)
				{
					this.remainingBytesBuffer = new byte[1];
				}
				else
				{
					Array.Resize<byte>(ref this.remainingBytesBuffer, 1);
				}
				this.remainingBytesBuffer[0] = array[num2];
			}
			num4 = 0;
			int num5 = this.remainingBuffer.Length + array2.Length;
			if (flush && num5 > charCount)
			{
				throw new ArgumentException(SR.BufferTooSmall(num5));
			}
			if (this.remainingBuffer != null && this.remainingBuffer.Length != 0)
			{
				num4 = ((this.remainingBuffer.Length < charCount) ? this.remainingBuffer.Length : charCount);
				Array.Copy(this.remainingBuffer, 0, chars, charIndex, num4);
				if (this.remainingBuffer.Length >= charCount)
				{
					int num6 = this.remainingBuffer.Length - num4;
					Array.Resize<char>(ref this.remainingBuffer, num6 + array2.Length);
					Array.Copy(this.remainingBuffer, charCount, this.remainingBuffer, 0, num6);
					Array.Copy(array2, 0, this.remainingBuffer, num6, array2.Length);
					charsUsed = num4;
					bytesUsed = num2;
					return;
				}
				if (this.remainingBuffer.Length != num4)
				{
					throw new InvalidOperationException(SR.InvalidHoldBackBuffer);
				}
				this.remainingBuffer = null;
			}
			int num7 = charCount - num4;
			int num8 = ((num7 < array2.Length) ? num7 : array2.Length);
			Array.Copy(array2, 0, chars, charIndex + num4, num8);
			if (array2.Length > num8)
			{
				int num9 = array2.Length - num8;
				int num10 = 0;
				if (this.remainingBuffer == null)
				{
					this.remainingBuffer = new char[num9];
				}
				else
				{
					num10 = this.remainingBuffer.Length;
					Array.Resize<char>(ref this.remainingBuffer, num9 + this.remainingBuffer.Length);
				}
				Array.Copy(array2, num8, this.remainingBuffer, num10, num9);
				if (flush)
				{
					throw new ArgumentException(SR.BufferTooSmall(array2.Length));
				}
			}
			else
			{
				completed = true;
			}
			bytesUsed = num2;
			charsUsed = num8 + num4;
			if (flush)
			{
				this.Reset();
			}
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x000B1AE5 File Offset: 0x000AFCE5
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.GetCharCount(bytes, index, count, true);
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000B1AF4 File Offset: 0x000AFCF4
		public override int GetCharCount(byte[] bytes, int index, int count, bool flush)
		{
			int num = index;
			int num2 = count;
			byte[] array;
			if (this.remainingBytesBuffer != null && this.remainingBytesBuffer.Length != 0)
			{
				array = new byte[count + 1];
				Array.Copy(this.remainingBytesBuffer, 0, array, 0, 1);
				Array.Copy(bytes, index, array, 1, count);
				num = 0;
				num2++;
			}
			else
			{
				array = bytes;
			}
			int num3 = this.converter.GetUnicodeCharCount(array, num, num2, flush, ref this.byteCountStatus, ref this.byteCountState, ref this.byteCountFlags);
			if (this.remainingBuffer != null)
			{
				num3 += this.remainingBuffer.Length;
			}
			return num3;
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x000B1B7B File Offset: 0x000AFD7B
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, true);
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000B1B8C File Offset: 0x000AFD8C
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
		{
			int num = 0;
			bool flag = false;
			int num2 = byteCount;
			int num3 = byteIndex;
			int num4 = 0;
			int num5 = chars.Length - charIndex;
			if (byteCount > bytes.Length || byteIndex > byteCount || byteIndex < 0 || charIndex < 0 || charIndex > chars.Length)
			{
				if (byteCount > bytes.Length)
				{
					throw new ArgumentException(SR.InvalidParameter("byteCount"));
				}
				if (byteIndex > byteCount || byteIndex < 0)
				{
					throw new ArgumentException(SR.InvalidParameter("byteIndex"));
				}
				if (charIndex > chars.Length || charIndex < 0)
				{
					throw new ArgumentException(SR.InvalidParameter("charIndex"));
				}
			}
			if (this.remainingBuffer == null)
			{
				this.remainingBuffer = new char[0];
			}
			byte[] array;
			if (this.remainingBytesBuffer != null && this.remainingBytesBuffer.Length != 0)
			{
				array = new byte[byteCount + 1];
				Array.Copy(this.remainingBytesBuffer, 0, array, 0, 1);
				Array.Copy(bytes, byteIndex, array, 1, byteCount);
				num2 = byteCount + 1;
				num3 = 0;
				this.remainingBytesBuffer = null;
			}
			else
			{
				array = bytes;
			}
			char[] array2 = this.converter.EbcdicToUnicode(array, num3, num2, flush, ref num, ref this.byteCountState, ref this.byteState, ref flag);
			this.converter.GetDoubleByteCutOffIndex(array, num3, num2, ref num4, ref this.parseState);
			if (num2 > num4)
			{
				if (this.remainingBytesBuffer == null)
				{
					this.remainingBytesBuffer = new byte[1];
				}
				else
				{
					Array.Resize<byte>(ref this.remainingBytesBuffer, 1);
				}
				this.remainingBytesBuffer[0] = array[num2];
			}
			num4 = 0;
			int num6 = this.remainingBuffer.Length + array2.Length;
			if (flush && num6 > chars.Length - charIndex)
			{
				throw new ArgumentException(SR.BufferTooSmall(num6));
			}
			if (this.remainingBuffer != null && this.remainingBuffer.Length != 0)
			{
				num4 = ((this.remainingBuffer.Length < num5) ? this.remainingBuffer.Length : num5);
				Array.Copy(this.remainingBuffer, 0, chars, charIndex, num4);
				if (this.remainingBuffer.Length >= num5)
				{
					int num7 = this.remainingBuffer.Length - num4;
					Array.Resize<char>(ref this.remainingBuffer, num7 + array2.Length);
					Array.Copy(this.remainingBuffer, num5, this.remainingBuffer, 0, num7);
					Array.Copy(array2, 0, this.remainingBuffer, num7, array2.Length);
					return num4;
				}
				if (this.remainingBuffer.Length != num4)
				{
					throw new InvalidOperationException(SR.InvalidHoldBackBuffer);
				}
				this.remainingBuffer = null;
			}
			int num8 = num5 - num4;
			int num9 = ((num8 < array2.Length) ? num8 : array2.Length);
			int num10 = num4 + num9;
			Array.Copy(array2, 0, chars, charIndex + num4, num9);
			if (array2.Length > num9)
			{
				int num11 = array2.Length - num9;
				int num12 = 0;
				if (this.remainingBuffer == null)
				{
					this.remainingBuffer = new char[num11];
				}
				else
				{
					num12 = this.remainingBuffer.Length;
					Array.Resize<char>(ref this.remainingBuffer, num11 + this.remainingBuffer.Length);
				}
				Array.Copy(array2, num9, this.remainingBuffer, num12, num11);
				if (flush)
				{
					throw new ArgumentException(SR.BufferTooSmall(array2.Length));
				}
			}
			if (flush)
			{
				this.Reset();
			}
			return num10;
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000B1E7D File Offset: 0x000B007D
		public override void Reset()
		{
			this.byteCountState = HisConverter.StreamState.NotSet;
			this.byteState = HisConverter.StreamState.NotSet;
			this.parseState = HisConverter.StreamState.NotSet;
			this.byteCountStatus = HisConverter.StatusFlags.NoStatus;
			this.byteCountFlags = HisConverter.PositionFlags.AtSingleByte;
			this.remainingBuffer = null;
		}

		// Token: 0x04001E0C RID: 7692
		private HisEncoding encoding;

		// Token: 0x04001E0D RID: 7693
		private HisConverter converter;

		// Token: 0x04001E0E RID: 7694
		private HisConverter.StreamState byteCountState;

		// Token: 0x04001E0F RID: 7695
		private HisConverter.StreamState byteState;

		// Token: 0x04001E10 RID: 7696
		private HisConverter.StreamState parseState;

		// Token: 0x04001E11 RID: 7697
		private HisConverter.StatusFlags byteCountStatus;

		// Token: 0x04001E12 RID: 7698
		private HisConverter.PositionFlags byteCountFlags;

		// Token: 0x04001E13 RID: 7699
		private byte[] remainingBytesBuffer;

		// Token: 0x04001E14 RID: 7700
		private char[] remainingBuffer;
	}
}
