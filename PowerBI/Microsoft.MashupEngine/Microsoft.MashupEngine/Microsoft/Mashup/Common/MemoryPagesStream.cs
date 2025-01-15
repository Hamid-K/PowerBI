using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C03 RID: 7171
	public sealed class MemoryPagesStream : PagesStream
	{
		// Token: 0x0600B2FD RID: 45821 RVA: 0x00246DF3 File Offset: 0x00244FF3
		public MemoryPagesStream()
			: this(65536)
		{
		}

		// Token: 0x0600B2FE RID: 45822 RVA: 0x00246E00 File Offset: 0x00245000
		public MemoryPagesStream(int pageSize)
			: this(pageSize, 0L, new List<byte[]>())
		{
		}

		// Token: 0x0600B2FF RID: 45823 RVA: 0x00246E10 File Offset: 0x00245010
		public MemoryPagesStream(long totalLength, List<byte[]> pages)
			: this(65536, totalLength, pages)
		{
		}

		// Token: 0x0600B300 RID: 45824 RVA: 0x00246E1F File Offset: 0x0024501F
		public MemoryPagesStream(int pageSize, long totalLength, List<byte[]> pages)
			: base(pageSize, totalLength)
		{
			this.pages = pages;
		}

		// Token: 0x0600B301 RID: 45825 RVA: 0x00246E30 File Offset: 0x00245030
		public List<byte[]> GetPages()
		{
			return this.pages;
		}

		// Token: 0x17002CE0 RID: 11488
		// (get) Token: 0x0600B302 RID: 45826 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002CE1 RID: 11489
		// (get) Token: 0x0600B303 RID: 45827 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002CE2 RID: 11490
		// (get) Token: 0x0600B304 RID: 45828 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B305 RID: 45829 RVA: 0x00246E38 File Offset: 0x00245038
		protected override int Read(int page, int pageOffset, int count, byte[] buffer, int bufferOffset)
		{
			if (page < this.pages.Count)
			{
				Buffer.BlockCopy(this.pages[page], pageOffset, buffer, bufferOffset, count);
				return count;
			}
			return 0;
		}

		// Token: 0x0600B306 RID: 45830 RVA: 0x00246E64 File Offset: 0x00245064
		protected override void Write(int page, int pageOffset, int count, byte[] buffer, int bufferOffset)
		{
			if (page == 0)
			{
				if (this.pages.Count == 0)
				{
					this.pages.Add(EmptyArray<byte>.Instance);
				}
				byte[] array = this.pages[0];
				if (array.Length < pageOffset + count)
				{
					byte[] array2 = new byte[Math.Min(MemoryPagesStream.GetPowerOf2Size(pageOffset + count), this.pageSize)];
					Buffer.BlockCopy(array, 0, array2, 0, array.Length);
					this.pages[0] = array2;
				}
			}
			else
			{
				while (page >= this.pages.Count)
				{
					this.pages.Add(new byte[this.pageSize]);
				}
			}
			Buffer.BlockCopy(buffer, bufferOffset, this.pages[page], pageOffset, count);
		}

		// Token: 0x0600B307 RID: 45831 RVA: 0x00246F18 File Offset: 0x00245118
		private static int GetPowerOf2Size(int length)
		{
			length--;
			int num = 256;
			while (length >= 256)
			{
				num *= 2;
				length /= 2;
			}
			return num;
		}

		// Token: 0x04005B60 RID: 23392
		public const int DefaultPageSize = 65536;

		// Token: 0x04005B61 RID: 23393
		private readonly List<byte[]> pages;
	}
}
