using System;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019B0 RID: 6576
	internal class DirectorySize
	{
		// Token: 0x0600A6A2 RID: 42658 RVA: 0x002276A6 File Offset: 0x002258A6
		public DirectorySize(string directoryPath, string searchPattern)
		{
			this.directoryPath = directoryPath;
			this.searchPattern = searchPattern;
			this.count = -1;
			this.size = -1L;
		}

		// Token: 0x17002A85 RID: 10885
		// (get) Token: 0x0600A6A3 RID: 42659 RVA: 0x002276CB File Offset: 0x002258CB
		public int Count
		{
			get
			{
				if (this.count == -1)
				{
					this.size = DirectorySizeCalculator.Compute(this.directoryPath, this.searchPattern, out this.count);
				}
				return this.count;
			}
		}

		// Token: 0x17002A86 RID: 10886
		// (get) Token: 0x0600A6A4 RID: 42660 RVA: 0x002276F9 File Offset: 0x002258F9
		public long Size
		{
			get
			{
				if (this.size == -1L)
				{
					this.size = DirectorySizeCalculator.Compute(this.directoryPath, this.searchPattern, out this.count);
				}
				return this.size;
			}
		}

		// Token: 0x0600A6A5 RID: 42661 RVA: 0x00227728 File Offset: 0x00225928
		public void Clear()
		{
			this.size = -1L;
			this.count = -1;
		}

		// Token: 0x0600A6A6 RID: 42662 RVA: 0x00227739 File Offset: 0x00225939
		public void Add(long size)
		{
			this.size = this.Size + size;
			this.count++;
		}

		// Token: 0x0600A6A7 RID: 42663 RVA: 0x00227757 File Offset: 0x00225957
		public void Update(long size)
		{
			this.size = this.Size + size;
		}

		// Token: 0x0600A6A8 RID: 42664 RVA: 0x00227767 File Offset: 0x00225967
		public static long Compute(string directoryPath, string searchPattern)
		{
			return new DirectorySize(directoryPath, searchPattern).Size;
		}

		// Token: 0x0600A6A9 RID: 42665 RVA: 0x00227775 File Offset: 0x00225975
		public static long PhysicalSize(long length)
		{
			return (length / 1024L + 1L) * 1024L;
		}

		// Token: 0x040056B2 RID: 22194
		private readonly string directoryPath;

		// Token: 0x040056B3 RID: 22195
		private readonly string searchPattern;

		// Token: 0x040056B4 RID: 22196
		private int count;

		// Token: 0x040056B5 RID: 22197
		private long size;
	}
}
