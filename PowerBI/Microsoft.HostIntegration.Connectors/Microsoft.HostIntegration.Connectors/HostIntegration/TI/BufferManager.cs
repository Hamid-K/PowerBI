using System;
using Microsoft.HostIntegration.StrictResources.TIGlobals;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000715 RID: 1813
	public class BufferManager
	{
		// Token: 0x06003985 RID: 14725 RVA: 0x000C5B90 File Offset: 0x000C3D90
		public BufferManager()
		{
			this.IOBuffer = new byte[65536];
			this.IOBufferSize = 65536;
			this.IOBufferHeaderSize = 0;
			this.IOBufferTrailerSize = 0;
			this.IOBufferCurrentIndex = 0;
			this.IOBufferCurrentDataIndex = 0;
			this.IOBufferDataSize = this.IOBufferSize;
			this.IsBufferInited = false;
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000C5BF0 File Offset: 0x000C3DF0
		public BufferManager(byte[] useBuffer)
		{
			this.IOBuffer = useBuffer;
			this.IOBufferSize = useBuffer.Length;
			this.IOBufferHeaderSize = 0;
			this.IOBufferTrailerSize = 0;
			this.IOBufferCurrentIndex = 0;
			this.IOBufferCurrentDataIndex = 0;
			this.IOBufferDataSize = this.IOBufferSize;
			this.IsBufferInited = false;
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000C5C44 File Offset: 0x000C3E44
		public BufferManager(byte InitValue)
		{
			this.IOBuffer = new byte[65536];
			this.IOBufferSize = 65536;
			this.IOBufferHeaderSize = 0;
			this.IOBufferTrailerSize = 0;
			this.IOBufferCurrentIndex = 0;
			this.IOBufferCurrentDataIndex = 0;
			this.IOBufferDataSize = this.IOBufferSize;
			this.InitBuffer(ref this.IOBuffer, InitValue);
			this.IsBufferInited = true;
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000C5CB0 File Offset: 0x000C3EB0
		public BufferManager(int BufferSize)
		{
			if (BufferSize < 128)
			{
				BufferSize = 128;
			}
			this.IOBuffer = new byte[BufferSize];
			this.IOBufferSize = BufferSize;
			this.IOBufferHeaderSize = 0;
			this.IOBufferTrailerSize = 0;
			this.IOBufferCurrentIndex = 0;
			this.IOBufferCurrentDataIndex = 0;
			this.IOBufferDataSize = this.IOBufferSize;
			this.IsBufferInited = false;
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000C5D14 File Offset: 0x000C3F14
		public BufferManager(int BufferSize, byte InitValue)
		{
			if (BufferSize < 128)
			{
				BufferSize = 128;
			}
			this.IOBuffer = new byte[BufferSize];
			this.IOBufferSize = BufferSize;
			this.IOBufferHeaderSize = 0;
			this.IOBufferTrailerSize = 0;
			this.IOBufferCurrentIndex = 0;
			this.IOBufferCurrentDataIndex = 0;
			this.IOBufferDataSize = this.IOBufferSize;
			this.InitBuffer(ref this.IOBuffer, InitValue);
			this.IsBufferInited = true;
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x000C5D88 File Offset: 0x000C3F88
		public BufferManager(int BufferSize, int HeaderSize, int TrailerSize)
		{
			if (BufferSize < 128)
			{
				BufferSize = 128;
			}
			this.IOBuffer = new byte[BufferSize];
			this.IOBufferSize = BufferSize;
			this.IOBufferCurrentIndex = 0;
			this.IOBufferHeaderSize = HeaderSize;
			this.IOBufferTrailerSize = TrailerSize;
			this.IOBufferCurrentDataIndex = HeaderSize;
			this.IOBufferDataSize = BufferSize - HeaderSize - TrailerSize;
			this.IsBufferInited = false;
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000C5DEC File Offset: 0x000C3FEC
		public BufferManager(int BufferSize, int HeaderSize, int TrailerSize, byte InitValue)
		{
			if (BufferSize < 128)
			{
				BufferSize = 128;
			}
			this.IOBuffer = new byte[BufferSize];
			this.IOBufferSize = BufferSize;
			this.IOBufferCurrentIndex = 0;
			this.IOBufferHeaderSize = HeaderSize;
			this.IOBufferTrailerSize = TrailerSize;
			this.IOBufferCurrentDataIndex = HeaderSize;
			this.IOBufferDataSize = BufferSize - HeaderSize - TrailerSize;
			this.InitBuffer(ref this.IOBuffer, InitValue);
			this.IsBufferInited = true;
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000C5E60 File Offset: 0x000C4060
		public unsafe void InitBuffer(ref byte[] IOBuffer, byte InitValue)
		{
			byte[] array;
			byte* ptr;
			if ((array = IOBuffer) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte* ptr2 = ptr;
			long num;
			byte* ptr3 = (byte*)(&num);
			int i;
			for (i = 0; i < 8; i++)
			{
				*ptr3 = InitValue;
				ptr3++;
			}
			ptr3 = (byte*)(&num);
			for (i = 0; i < this.IOBufferSize / 8; i++)
			{
				*(long*)ptr2 = *(long*)ptr3;
				ptr2 += 8;
			}
			for (i *= 8; i < this.IOBufferSize; i++)
			{
				*ptr2 = InitValue;
				ptr2++;
			}
			array = null;
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x000C5EE7 File Offset: 0x000C40E7
		// (set) Token: 0x0600398E RID: 14734 RVA: 0x000C5EEF File Offset: 0x000C40EF
		public int BufferCurrentPosition
		{
			get
			{
				return this.IOBufferCurrentIndex;
			}
			set
			{
				if (value > this.IOBufferSize)
				{
					throw new Exception(SR.BufferPositionTooBig);
				}
				this.IOBufferCurrentIndex = value;
				this.IOBufferCurrentDataIndex = value + this.IOBufferHeaderSize;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x0600398F RID: 14735 RVA: 0x000C5F1A File Offset: 0x000C411A
		// (set) Token: 0x06003990 RID: 14736 RVA: 0x000C5F22 File Offset: 0x000C4122
		public int BufferCurrentDataPosition
		{
			get
			{
				return this.IOBufferCurrentDataIndex;
			}
			set
			{
				if (value > this.IOBufferDataSize + this.IOBufferHeaderSize)
				{
					throw new Exception(SR.BufferDataPositionTooBig);
				}
				if (value < this.IOBufferHeaderSize)
				{
					throw new Exception(SR.BufferDataPositionTooSmall);
				}
				this.IOBufferCurrentDataIndex = value;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06003991 RID: 14737 RVA: 0x000C5F5A File Offset: 0x000C415A
		// (set) Token: 0x06003992 RID: 14738 RVA: 0x000036A9 File Offset: 0x000018A9
		public int BufferStartDataPosition
		{
			get
			{
				return this.IOBufferHeaderSize;
			}
			set
			{
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000C5F5A File Offset: 0x000C415A
		// (set) Token: 0x06003994 RID: 14740 RVA: 0x000C5F64 File Offset: 0x000C4164
		public int HeaderSize
		{
			get
			{
				return this.IOBufferHeaderSize;
			}
			set
			{
				if (value > this.IOBufferSize)
				{
					throw new Exception(SR.HeaderSizeTooBig);
				}
				this.IOBufferCurrentIndex = 0;
				this.IOBufferHeaderSize = value;
				this.IOBufferCurrentDataIndex = value;
				this.IOBufferDataSize = this.BufferSize - value - this.TrailerSize;
				this.IOBufferHeaderSize = value;
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06003995 RID: 14741 RVA: 0x000C5FB6 File Offset: 0x000C41B6
		// (set) Token: 0x06003996 RID: 14742 RVA: 0x000C5FBE File Offset: 0x000C41BE
		public int TrailerSize
		{
			get
			{
				return this.IOBufferTrailerSize;
			}
			set
			{
				if (value > this.IOBufferSize - this.IOBufferDataSize - this.IOBufferHeaderSize)
				{
					throw new Exception(SR.TrailerSizeTooBig);
				}
				this.IOBufferTrailerSize = value;
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06003997 RID: 14743 RVA: 0x000C5FE9 File Offset: 0x000C41E9
		// (set) Token: 0x06003998 RID: 14744 RVA: 0x000036A9 File Offset: 0x000018A9
		public int BufferSize
		{
			get
			{
				return this.IOBufferSize;
			}
			set
			{
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x000C5FF1 File Offset: 0x000C41F1
		// (set) Token: 0x0600399A RID: 14746 RVA: 0x000036A9 File Offset: 0x000018A9
		public int BufferDataSize
		{
			get
			{
				return this.IOBufferDataSize;
			}
			set
			{
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x0600399B RID: 14747 RVA: 0x000C5FF9 File Offset: 0x000C41F9
		// (set) Token: 0x0600399C RID: 14748 RVA: 0x000036A9 File Offset: 0x000018A9
		public int CurrentRemainingBufferSize
		{
			get
			{
				return this.IOBufferSize - this.IOBufferCurrentIndex;
			}
			set
			{
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x000C6008 File Offset: 0x000C4208
		// (set) Token: 0x0600399E RID: 14750 RVA: 0x000036A9 File Offset: 0x000018A9
		public int CurrentRemainingDataBufferSize
		{
			get
			{
				return this.IOBufferDataSize + this.IOBufferHeaderSize - this.IOBufferCurrentDataIndex;
			}
			set
			{
			}
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x000C6020 File Offset: 0x000C4220
		public unsafe void GrowBuffer(int NeededSize, out int NewBufferSize)
		{
			int num = this.IOBufferSize;
			if (this.IOBufferSize * 2 < NeededSize)
			{
				num = NeededSize;
			}
			num *= 2;
			byte[] array = new byte[num];
			int num2;
			if (!this.IsBufferInited)
			{
				num2 = this.IOBufferCurrentDataIndex;
			}
			else
			{
				num2 = this.IOBufferDataSize;
			}
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			byte[] array3;
			byte* ptr2;
			if ((array3 = this.IOBuffer) == null || array3.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array3[0];
			}
			byte* ptr3 = ptr;
			byte* ptr4 = ptr2;
			int i;
			for (i = 0; i < num2 / 8; i++)
			{
				*(long*)ptr3 = *(long*)ptr4;
				ptr3 += 8;
				ptr4 += 8;
			}
			for (i *= 8; i < num2; i++)
			{
				*ptr3 = *ptr4;
				ptr3++;
				ptr4++;
			}
			array2 = null;
			array3 = null;
			if (this.IOBufferTrailerSize > 0)
			{
				byte* ptr5;
				if ((array3 = array) == null || array3.Length == 0)
				{
					ptr5 = null;
				}
				else
				{
					ptr5 = &array3[0];
				}
				byte* ptr6;
				if ((array2 = this.IOBuffer) == null || array2.Length == 0)
				{
					ptr6 = null;
				}
				else
				{
					ptr6 = &array2[0];
				}
				byte* ptr7 = ptr5;
				byte* ptr8 = ptr6;
				ptr8 += this.IOBufferSize - this.IOBufferTrailerSize;
				ptr7 += num - this.IOBufferTrailerSize;
				for (i = 0; i < this.IOBufferTrailerSize / 8; i++)
				{
					*(long*)ptr7 = *(long*)ptr8;
					ptr7 += 8;
					ptr8 += 8;
				}
				for (i *= 8; i < this.IOBufferTrailerSize; i++)
				{
					*ptr7 = *ptr8;
					ptr7++;
					ptr8++;
				}
				array3 = null;
				array2 = null;
			}
			this.IOBuffer = array;
			NewBufferSize = num;
			this.IOBufferSize = NewBufferSize;
			this.IOBufferDataSize = this.IOBufferSize - this.HeaderSize - this.TrailerSize;
		}

		// Token: 0x040021AC RID: 8620
		public byte[] IOBuffer;

		// Token: 0x040021AD RID: 8621
		private int IOBufferDataSize;

		// Token: 0x040021AE RID: 8622
		private int IOBufferCurrentDataIndex;

		// Token: 0x040021AF RID: 8623
		private int IOBufferSize;

		// Token: 0x040021B0 RID: 8624
		private int IOBufferCurrentIndex;

		// Token: 0x040021B1 RID: 8625
		private int IOBufferHeaderSize;

		// Token: 0x040021B2 RID: 8626
		private int IOBufferTrailerSize;

		// Token: 0x040021B3 RID: 8627
		private bool IsBufferInited;
	}
}
