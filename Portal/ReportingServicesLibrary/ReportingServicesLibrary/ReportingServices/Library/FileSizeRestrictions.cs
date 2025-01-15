using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000043 RID: 67
	internal class FileSizeRestrictions : IFileSizeRestrictions
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000F90B File Offset: 0x0000DB0B
		public bool ServerRestrictionSet
		{
			get
			{
				return CachedSystemProperties.MaxFileSizeMb >= 0;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000F918 File Offset: 0x0000DB18
		public int ServerSizeInMb
		{
			get
			{
				return CachedSystemProperties.MaxFileSizeMb;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000F91F File Offset: 0x0000DB1F
		public int ServerSizeInBytes
		{
			get
			{
				return this.ServerSizeInMb * 1000000;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000F92D File Offset: 0x0000DB2D
		public void ThrowIfSizeIsOutOfLimits(long size)
		{
			if (!this.SizeInBytesWithinLimits(size))
			{
				throw new FileSizeException();
			}
			if (size > 2000000000L)
			{
				throw new FileSizeNotSupportedException();
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000F94D File Offset: 0x0000DB4D
		public void ThrowIfSizeIsOutOfLimits(byte[] data)
		{
			if (data != null)
			{
				this.ThrowIfSizeIsOutOfLimits((long)data.Length);
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000F95B File Offset: 0x0000DB5B
		public bool SizeInBytesWithinLimits(long size)
		{
			return !this.ServerRestrictionSet || size <= (long)this.ServerSizeInBytes;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000F974 File Offset: 0x0000DB74
		public bool SizeWithinLimits(byte[] data)
		{
			return data == null || this.SizeInBytesWithinLimits((long)data.Length);
		}

		// Token: 0x04000145 RID: 325
		private const int BytesInMegabytes = 1000000;

		// Token: 0x04000146 RID: 326
		public const long CatalogSizeLimitInBytes = 2000000000L;
	}
}
