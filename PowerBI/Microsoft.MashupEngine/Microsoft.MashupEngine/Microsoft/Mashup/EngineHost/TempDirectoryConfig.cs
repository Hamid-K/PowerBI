using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001987 RID: 6535
	public sealed class TempDirectoryConfig : ITempDirectoryConfig
	{
		// Token: 0x0600A5BA RID: 42426 RVA: 0x00224AAE File Offset: 0x00222CAE
		public TempDirectoryConfig(string tempDirectoryPath, long tempDirectoryMaxSize)
		{
			this.tempDirectoryPath = tempDirectoryPath;
			this.tempDirectoryMaxSize = tempDirectoryMaxSize;
		}

		// Token: 0x17002A50 RID: 10832
		// (get) Token: 0x0600A5BB RID: 42427 RVA: 0x00224AC4 File Offset: 0x00222CC4
		public string TempDirectoryPath
		{
			get
			{
				return this.tempDirectoryPath;
			}
		}

		// Token: 0x17002A51 RID: 10833
		// (get) Token: 0x0600A5BC RID: 42428 RVA: 0x00224ACC File Offset: 0x00222CCC
		public long TempDirectoryMaxSize
		{
			get
			{
				return this.tempDirectoryMaxSize;
			}
		}

		// Token: 0x0400564D RID: 22093
		private readonly string tempDirectoryPath;

		// Token: 0x0400564E RID: 22094
		private readonly long tempDirectoryMaxSize;
	}
}
