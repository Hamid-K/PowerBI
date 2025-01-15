using System;
using System.Text;
using Microsoft.HostIntegration.StrictResources.Nls;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x0200062A RID: 1578
	public class HisEncoder : Encoder
	{
		// Token: 0x06003520 RID: 13600 RVA: 0x000B1EA9 File Offset: 0x000B00A9
		internal HisEncoder(HisEncoding dest, HisConverter convert)
		{
			this.encoding = dest;
			this.converter = convert;
			this.Reset();
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000B1EC8 File Offset: 0x000B00C8
		public override void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			int num = 0;
			bool flag = false;
			bytesUsed = 0;
			charsUsed = 0;
			completed = false;
			int num2 = 0;
			if (this.remainingBuffer == null)
			{
				this.remainingBuffer = new byte[0];
			}
			if (charIndex < 0 || charIndex >= chars.Length)
			{
				throw new ArgumentException(SR.InvalidParameter("charIndex"));
			}
			if (charCount < 0)
			{
				throw new ArgumentException(SR.InvalidParameter("charCount"));
			}
			if (charCount + charIndex > chars.Length)
			{
				throw new ArgumentException(SR.InvalidParameter("charCount + charIndex"));
			}
			if (byteIndex < 0 || byteIndex >= bytes.Length)
			{
				throw new ArgumentException(SR.InvalidParameter("byteIndex"));
			}
			if (byteCount < 0)
			{
				throw new ArgumentException(SR.InvalidParameter("byteCount"));
			}
			if (byteCount + byteIndex > bytes.Length)
			{
				throw new ArgumentException(SR.InvalidParameter("byteCount + byteIndex"));
			}
			if (byteCount < this.converter.GetMinimumByteSizeRequired(ref this.parseState))
			{
				throw new ArgumentException(SR.BufferTooSmallThanRequiredSize);
			}
			byte[] array = this.converter.UnicodeToEbcdic(chars, charIndex, charCount, flush, ref num, ref this.byteCountState, ref this.byteState, ref flag);
			int num3 = this.remainingBuffer.Length + array.Length;
			if (flush && num3 > byteCount)
			{
				throw new ArgumentException(SR.BufferTooSmall(num3));
			}
			if (this.remainingBuffer != null && this.remainingBuffer.Length != 0)
			{
				num2 = 0;
				int num4 = ((this.remainingBuffer.Length < byteCount) ? this.remainingBuffer.Length : byteCount);
				this.converter.GetDoubleByteCutOffIndex(this.remainingBuffer, 0, num4, ref num2, ref this.parseState);
				Array.Copy(this.remainingBuffer, 0, bytes, byteIndex, num2);
				if (this.remainingBuffer.Length >= byteCount)
				{
					int num5 = this.remainingBuffer.Length - num2;
					Array.Resize<byte>(ref this.remainingBuffer, num5 + array.Length);
					Array.Copy(this.remainingBuffer, byteCount, this.remainingBuffer, 0, num5);
					Array.Copy(array, 0, this.remainingBuffer, num5, array.Length);
					charsUsed = charCount;
					bytesUsed = num2;
					return;
				}
				if (this.remainingBuffer.Length != num2)
				{
					throw new InvalidOperationException(SR.InvalidHoldBackBuffer);
				}
				this.remainingBuffer = null;
			}
			int num6 = byteCount - num2;
			int num7 = ((num6 < array.Length) ? num6 : array.Length);
			int num8 = 0;
			this.converter.GetDoubleByteCutOffIndex(array, 0, num7, ref num8, ref this.parseState);
			Array.Copy(array, 0, bytes, byteIndex + num2, num8);
			if (array.Length > num8)
			{
				int num9 = array.Length - num8;
				int num10 = 0;
				if (this.remainingBuffer == null)
				{
					this.remainingBuffer = new byte[num9];
				}
				else
				{
					num10 = this.remainingBuffer.Length;
					Array.Resize<byte>(ref this.remainingBuffer, num9 + this.remainingBuffer.Length);
				}
				Array.Copy(array, num8, this.remainingBuffer, num10, num9);
				if (flush)
				{
					throw new ArgumentException(SR.BufferTooSmall(array.Length));
				}
			}
			else
			{
				completed = true;
			}
			charsUsed = charCount;
			bytesUsed = num8 + num2;
			if (flush)
			{
				this.Reset();
			}
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000B21B0 File Offset: 0x000B03B0
		public override int GetByteCount(char[] chars, int index, int count, bool flush)
		{
			byte[] bytes = this.encoding.WindowsEncoding.GetBytes(chars, index, count);
			int num = this.converter.GetEbcdicByteCount(bytes, index, bytes.Length, flush, ref this.byteCountStatus, ref this.byteCountState, ref this.byteCountFlags);
			if (this.remainingBuffer != null)
			{
				num += this.remainingBuffer.Length;
			}
			return num;
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000B220C File Offset: 0x000B040C
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
		{
			int num = 0;
			bool flag = false;
			int num2 = 0;
			int num3 = bytes.Length - byteIndex;
			if (this.remainingBuffer == null)
			{
				this.remainingBuffer = new byte[0];
			}
			if (byteIndex > bytes.Length || byteIndex < 0 || charIndex < 0 || charCount > chars.Length || charIndex > chars.Length)
			{
				if (byteIndex > bytes.Length || byteIndex < 0)
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
			if (bytes.Length < this.converter.GetMinimumByteSizeRequired(ref this.parseState))
			{
				throw new ArgumentException(SR.BufferTooSmallThanRequiredSize);
			}
			byte[] array = this.converter.UnicodeToEbcdic(chars, charIndex, charCount, flush, ref num, ref this.byteCountState, ref this.byteState, ref flag);
			int num4 = this.remainingBuffer.Length + array.Length;
			if (flush && num4 > bytes.Length - byteIndex)
			{
				throw new ArgumentException(SR.BufferTooSmall(num4));
			}
			if (this.remainingBuffer != null && this.remainingBuffer.Length != 0)
			{
				num2 = 0;
				int num5 = ((this.remainingBuffer.Length < num3) ? this.remainingBuffer.Length : num3);
				this.converter.GetDoubleByteCutOffIndex(this.remainingBuffer, 0, num5, ref num2, ref this.parseState);
				Array.Copy(this.remainingBuffer, 0, bytes, byteIndex, num2);
				if (this.remainingBuffer.Length >= num3)
				{
					int num6 = this.remainingBuffer.Length - num2;
					Array.Resize<byte>(ref this.remainingBuffer, num6 + array.Length);
					Array.Copy(this.remainingBuffer, num3, this.remainingBuffer, 0, num6);
					Array.Copy(array, 0, this.remainingBuffer, num6, array.Length);
					return num2;
				}
				if (this.remainingBuffer.Length != num2)
				{
					throw new InvalidOperationException(SR.InvalidHoldBackBuffer);
				}
				this.remainingBuffer = null;
			}
			int num7 = num3 - num2;
			int num8 = ((num7 < array.Length) ? num7 : array.Length);
			int num9 = num2 + num8;
			int num10 = 0;
			this.converter.GetDoubleByteCutOffIndex(array, 0, num8, ref num10, ref this.parseState);
			Array.Copy(array, 0, bytes, byteIndex + num2, num10);
			if (array.Length > num10)
			{
				int num11 = array.Length - num10;
				int num12 = 0;
				if (this.remainingBuffer == null)
				{
					this.remainingBuffer = new byte[num11];
				}
				else
				{
					num12 = this.remainingBuffer.Length;
					Array.Resize<byte>(ref this.remainingBuffer, num11 + this.remainingBuffer.Length);
				}
				Array.Copy(array, num10, this.remainingBuffer, num12, num11);
				if (flush)
				{
					throw new ArgumentException(SR.BufferTooSmall(array.Length));
				}
			}
			if (flush)
			{
				this.Reset();
			}
			return num9;
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000B24B8 File Offset: 0x000B06B8
		public override void Reset()
		{
			this.byteCountState = HisConverter.StreamState.NotSet;
			this.byteState = HisConverter.StreamState.NotSet;
			this.parseState = HisConverter.StreamState.NotSet;
			this.byteCountStatus = HisConverter.StatusFlags.NoStatus;
			this.byteCountFlags = HisConverter.PositionFlags.AtSingleByte;
			this.remainingBuffer = null;
		}

		// Token: 0x04001E15 RID: 7701
		private HisEncoding encoding;

		// Token: 0x04001E16 RID: 7702
		private HisConverter converter;

		// Token: 0x04001E17 RID: 7703
		private HisConverter.StreamState byteCountState;

		// Token: 0x04001E18 RID: 7704
		private HisConverter.StreamState byteState;

		// Token: 0x04001E19 RID: 7705
		private HisConverter.StreamState parseState;

		// Token: 0x04001E1A RID: 7706
		private HisConverter.StatusFlags byteCountStatus;

		// Token: 0x04001E1B RID: 7707
		private HisConverter.PositionFlags byteCountFlags;

		// Token: 0x04001E1C RID: 7708
		private byte[] remainingBuffer;
	}
}
