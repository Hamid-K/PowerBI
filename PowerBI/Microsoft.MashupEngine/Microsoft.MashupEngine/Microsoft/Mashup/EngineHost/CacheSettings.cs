using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001962 RID: 6498
	public class CacheSettings
	{
		// Token: 0x0600A4B4 RID: 42164 RVA: 0x00221BCC File Offset: 0x0021FDCC
		public CacheSettings()
		{
			this.CacheMode = PersistentCacheMode.Disk;
		}

		// Token: 0x170029FB RID: 10747
		// (get) Token: 0x0600A4B5 RID: 42165 RVA: 0x00221BDB File Offset: 0x0021FDDB
		// (set) Token: 0x0600A4B6 RID: 42166 RVA: 0x00221BE3 File Offset: 0x0021FDE3
		public string Identity { get; set; }

		// Token: 0x170029FC RID: 10748
		// (get) Token: 0x0600A4B7 RID: 42167 RVA: 0x00221BEC File Offset: 0x0021FDEC
		// (set) Token: 0x0600A4B8 RID: 42168 RVA: 0x00221BF4 File Offset: 0x0021FDF4
		public string BaseCachePath { get; set; }

		// Token: 0x170029FD RID: 10749
		// (get) Token: 0x0600A4B9 RID: 42169 RVA: 0x00221BFD File Offset: 0x0021FDFD
		// (set) Token: 0x0600A4BA RID: 42170 RVA: 0x00221C05 File Offset: 0x0021FE05
		public bool RefreshData { get; set; }

		// Token: 0x170029FE RID: 10750
		// (get) Token: 0x0600A4BB RID: 42171 RVA: 0x00221C0E File Offset: 0x0021FE0E
		// (set) Token: 0x0600A4BC RID: 42172 RVA: 0x00221C16 File Offset: 0x0021FE16
		public long MaxCacheSize { get; set; }

		// Token: 0x170029FF RID: 10751
		// (get) Token: 0x0600A4BD RID: 42173 RVA: 0x00221C1F File Offset: 0x0021FE1F
		// (set) Token: 0x0600A4BE RID: 42174 RVA: 0x00221C27 File Offset: 0x0021FE27
		public long? MaxCacheEntrySize { get; set; }

		// Token: 0x17002A00 RID: 10752
		// (get) Token: 0x0600A4BF RID: 42175 RVA: 0x00221C30 File Offset: 0x0021FE30
		// (set) Token: 0x0600A4C0 RID: 42176 RVA: 0x00221C38 File Offset: 0x0021FE38
		public PersistentCacheMode CacheMode { get; set; }

		// Token: 0x17002A01 RID: 10753
		// (get) Token: 0x0600A4C1 RID: 42177 RVA: 0x00221C41 File Offset: 0x0021FE41
		// (set) Token: 0x0600A4C2 RID: 42178 RVA: 0x00221C49 File Offset: 0x0021FE49
		public string CacheEncryptionCertificateThumbprint { get; set; }

		// Token: 0x17002A02 RID: 10754
		// (get) Token: 0x0600A4C3 RID: 42179 RVA: 0x00221C52 File Offset: 0x0021FE52
		// (set) Token: 0x0600A4C4 RID: 42180 RVA: 0x00221C5A File Offset: 0x0021FE5A
		public TimeSpan? CacheTTL { get; set; }

		// Token: 0x0600A4C5 RID: 42181 RVA: 0x00221C64 File Offset: 0x0021FE64
		public CacheSettings Clone()
		{
			return new CacheSettings
			{
				Identity = this.Identity,
				BaseCachePath = this.BaseCachePath,
				RefreshData = this.RefreshData,
				MaxCacheSize = this.MaxCacheSize,
				MaxCacheEntrySize = this.MaxCacheEntrySize,
				CacheMode = this.CacheMode,
				CacheEncryptionCertificateThumbprint = this.CacheEncryptionCertificateThumbprint,
				CacheTTL = this.CacheTTL
			};
		}
	}
}
