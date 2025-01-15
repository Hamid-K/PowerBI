using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200029B RID: 667
	[Serializable]
	internal sealed class ChunkHeader
	{
		// Token: 0x06001848 RID: 6216 RVA: 0x0006294C File Offset: 0x00060B4C
		internal ChunkHeader(string chunkName, int chunkType, ChunkFlags chunkFlag, string mimeType, short version, long chunkSize)
		{
			this.m_chunkName = chunkName;
			this.m_chunkType = chunkType;
			this.m_chunkFlags = chunkFlag;
			this.m_mimeType = mimeType;
			this.m_version = version;
			this.m_chunkSize = chunkSize;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00062981 File Offset: 0x00060B81
		internal ChunkHeader(ChunkHeader baseHeader)
			: this(baseHeader.ChunkName, baseHeader.ChunkType, baseHeader.ChunkFlag, baseHeader.MimeType, baseHeader.Version, baseHeader.ChunkSize)
		{
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x000629AD File Offset: 0x00060BAD
		internal string MimeType
		{
			get
			{
				return this.m_mimeType;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x000629B5 File Offset: 0x00060BB5
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x000629BD File Offset: 0x00060BBD
		internal string ChunkName
		{
			get
			{
				return this.m_chunkName;
			}
			set
			{
				this.m_chunkName = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x000629C6 File Offset: 0x00060BC6
		internal int ChunkType
		{
			get
			{
				return this.m_chunkType;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x000629CE File Offset: 0x00060BCE
		internal ChunkFlags ChunkFlag
		{
			get
			{
				return this.m_chunkFlags;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x000629D6 File Offset: 0x00060BD6
		internal long ChunkSize
		{
			get
			{
				return this.m_chunkSize;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x000629DE File Offset: 0x00060BDE
		internal short Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x040008C0 RID: 2240
		internal static readonly short MissingVersion = 1;

		// Token: 0x040008C1 RID: 2241
		internal static readonly short CurrentVersion = 2;

		// Token: 0x040008C2 RID: 2242
		private ChunkFlags m_chunkFlags;

		// Token: 0x040008C3 RID: 2243
		private int m_chunkType;

		// Token: 0x040008C4 RID: 2244
		private string m_chunkName;

		// Token: 0x040008C5 RID: 2245
		private string m_mimeType;

		// Token: 0x040008C6 RID: 2246
		private long m_chunkSize;

		// Token: 0x040008C7 RID: 2247
		private short m_version;
	}
}
