using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Interface
{
	// Token: 0x02001B66 RID: 7014
	public sealed class PersistentCacheConfig
	{
		// Token: 0x0600AF86 RID: 44934 RVA: 0x0023ECF4 File Offset: 0x0023CEF4
		public PersistentCacheConfig(PersistentCacheMode mode, string directory, long maxCacheSize, long trimCacheSize, long maxEntryLength, int maxObjectCacheSize, int trimObjectCacheSize, bool refreshData, bool cancelCommitsOnDispose, bool userSpecific, DateTime maxStaleness, CacheVersion diskMinVersion, string encryptionCertificateThumbprint, int implementationVersion)
		{
			this.mode = mode;
			this.directory = directory;
			this.maxCacheSize = maxCacheSize;
			this.trimCacheSize = trimCacheSize;
			this.maxEntryLength = maxEntryLength;
			this.maxObjectCacheSize = maxObjectCacheSize;
			this.trimObjectCacheSize = trimObjectCacheSize;
			this.refreshData = refreshData;
			this.cancelCommitsOnDispose = cancelCommitsOnDispose;
			this.userSpecific = userSpecific;
			this.maxStaleness = maxStaleness;
			this.diskMinVersion = diskMinVersion;
			this.encryptionCertificateThumbprint = encryptionCertificateThumbprint;
			this.implementationVersion = implementationVersion;
		}

		// Token: 0x17002BFD RID: 11261
		// (get) Token: 0x0600AF87 RID: 44935 RVA: 0x0023ED74 File Offset: 0x0023CF74
		public PersistentCacheMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x17002BFE RID: 11262
		// (get) Token: 0x0600AF88 RID: 44936 RVA: 0x0023ED7C File Offset: 0x0023CF7C
		public string Directory
		{
			get
			{
				return this.directory;
			}
		}

		// Token: 0x17002BFF RID: 11263
		// (get) Token: 0x0600AF89 RID: 44937 RVA: 0x0023ED84 File Offset: 0x0023CF84
		public long MaxCacheSize
		{
			get
			{
				return this.maxCacheSize;
			}
		}

		// Token: 0x17002C00 RID: 11264
		// (get) Token: 0x0600AF8A RID: 44938 RVA: 0x0023ED8C File Offset: 0x0023CF8C
		public long TrimCacheSize
		{
			get
			{
				return this.trimCacheSize;
			}
		}

		// Token: 0x17002C01 RID: 11265
		// (get) Token: 0x0600AF8B RID: 44939 RVA: 0x0023ED94 File Offset: 0x0023CF94
		public long MaxEntryLength
		{
			get
			{
				return this.maxEntryLength;
			}
		}

		// Token: 0x17002C02 RID: 11266
		// (get) Token: 0x0600AF8C RID: 44940 RVA: 0x0023ED9C File Offset: 0x0023CF9C
		public int MaxObjectCacheSize
		{
			get
			{
				return this.maxObjectCacheSize;
			}
		}

		// Token: 0x17002C03 RID: 11267
		// (get) Token: 0x0600AF8D RID: 44941 RVA: 0x0023EDA4 File Offset: 0x0023CFA4
		public int TrimObjectCacheSize
		{
			get
			{
				return this.trimObjectCacheSize;
			}
		}

		// Token: 0x17002C04 RID: 11268
		// (get) Token: 0x0600AF8E RID: 44942 RVA: 0x0023EDAC File Offset: 0x0023CFAC
		public bool RefreshData
		{
			get
			{
				return this.refreshData;
			}
		}

		// Token: 0x17002C05 RID: 11269
		// (get) Token: 0x0600AF8F RID: 44943 RVA: 0x0023EDB4 File Offset: 0x0023CFB4
		public bool CancelCommitsOnDispose
		{
			get
			{
				return this.cancelCommitsOnDispose;
			}
		}

		// Token: 0x17002C06 RID: 11270
		// (get) Token: 0x0600AF90 RID: 44944 RVA: 0x0023EDBC File Offset: 0x0023CFBC
		public bool UserSpecific
		{
			get
			{
				return this.userSpecific;
			}
		}

		// Token: 0x17002C07 RID: 11271
		// (get) Token: 0x0600AF91 RID: 44945 RVA: 0x0023EDC4 File Offset: 0x0023CFC4
		public DateTime MaxStaleness
		{
			get
			{
				return this.maxStaleness;
			}
		}

		// Token: 0x17002C08 RID: 11272
		// (get) Token: 0x0600AF92 RID: 44946 RVA: 0x0023EDCC File Offset: 0x0023CFCC
		public CacheVersion DiskMinVersion
		{
			get
			{
				return this.diskMinVersion;
			}
		}

		// Token: 0x17002C09 RID: 11273
		// (get) Token: 0x0600AF93 RID: 44947 RVA: 0x0023EDD4 File Offset: 0x0023CFD4
		public string EncryptionCertificateThumbprint
		{
			get
			{
				return this.encryptionCertificateThumbprint;
			}
		}

		// Token: 0x17002C0A RID: 11274
		// (get) Token: 0x0600AF94 RID: 44948 RVA: 0x0023EDDC File Offset: 0x0023CFDC
		public int ImplementationVersion
		{
			get
			{
				return this.implementationVersion;
			}
		}

		// Token: 0x0600AF95 RID: 44949 RVA: 0x0023EDE4 File Offset: 0x0023CFE4
		public PersistentCacheConfig ReplaceMinVersion(CacheVersion newDiskMinVersion)
		{
			return new PersistentCacheConfig(this.mode, this.directory, this.maxCacheSize, this.trimCacheSize, this.maxEntryLength, this.maxObjectCacheSize, this.trimObjectCacheSize, this.refreshData, this.cancelCommitsOnDispose, this.userSpecific, this.maxStaleness, newDiskMinVersion, this.encryptionCertificateThumbprint, this.implementationVersion);
		}

		// Token: 0x0600AF96 RID: 44950 RVA: 0x0023EE48 File Offset: 0x0023D048
		public PersistentCacheConfig Qualify(string suffix)
		{
			return new PersistentCacheConfig(this.mode, Path.Combine(this.directory, suffix), this.maxCacheSize, this.trimCacheSize, this.maxEntryLength, this.maxObjectCacheSize, this.trimObjectCacheSize, this.refreshData, this.cancelCommitsOnDispose, this.userSpecific, this.maxStaleness, this.diskMinVersion, this.encryptionCertificateThumbprint, this.implementationVersion);
		}

		// Token: 0x0600AF97 RID: 44951 RVA: 0x0023EEB4 File Offset: 0x0023D0B4
		public PersistentCacheConfig ToLocalConfig()
		{
			return new PersistentCacheConfig(this.mode & ~PersistentCacheMode.Remote, this.directory, this.maxCacheSize, this.trimCacheSize, this.maxEntryLength, this.maxObjectCacheSize, this.trimObjectCacheSize, this.refreshData, this.cancelCommitsOnDispose, this.userSpecific, this.maxStaleness, this.diskMinVersion, this.encryptionCertificateThumbprint, this.implementationVersion);
		}

		// Token: 0x04005A73 RID: 23155
		private readonly PersistentCacheMode mode;

		// Token: 0x04005A74 RID: 23156
		private readonly string directory;

		// Token: 0x04005A75 RID: 23157
		private readonly long maxCacheSize;

		// Token: 0x04005A76 RID: 23158
		private readonly long trimCacheSize;

		// Token: 0x04005A77 RID: 23159
		private readonly long maxEntryLength;

		// Token: 0x04005A78 RID: 23160
		private readonly int maxObjectCacheSize;

		// Token: 0x04005A79 RID: 23161
		private readonly int trimObjectCacheSize;

		// Token: 0x04005A7A RID: 23162
		private readonly bool refreshData;

		// Token: 0x04005A7B RID: 23163
		private readonly bool cancelCommitsOnDispose;

		// Token: 0x04005A7C RID: 23164
		private readonly bool userSpecific;

		// Token: 0x04005A7D RID: 23165
		private readonly DateTime maxStaleness;

		// Token: 0x04005A7E RID: 23166
		private readonly CacheVersion diskMinVersion;

		// Token: 0x04005A7F RID: 23167
		private readonly string encryptionCertificateThumbprint;

		// Token: 0x04005A80 RID: 23168
		private readonly int implementationVersion;
	}
}
