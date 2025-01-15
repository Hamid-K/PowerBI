using System;

namespace NLog.Internal
{
	// Token: 0x02000118 RID: 280
	internal class FileCharacteristics
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x00024668 File Offset: 0x00022868
		public FileCharacteristics(DateTime creationTimeUtc, DateTime lastWriteTimeUtc, long fileLength)
		{
			this.CreationTimeUtc = creationTimeUtc;
			this.LastWriteTimeUtc = lastWriteTimeUtc;
			this.FileLength = fileLength;
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x00024685 File Offset: 0x00022885
		// (set) Token: 0x06000EAE RID: 3758 RVA: 0x0002468D File Offset: 0x0002288D
		public DateTime CreationTimeUtc { get; private set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00024696 File Offset: 0x00022896
		// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x0002469E File Offset: 0x0002289E
		public DateTime LastWriteTimeUtc { get; private set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x000246A7 File Offset: 0x000228A7
		// (set) Token: 0x06000EB2 RID: 3762 RVA: 0x000246AF File Offset: 0x000228AF
		public long FileLength { get; private set; }
	}
}
