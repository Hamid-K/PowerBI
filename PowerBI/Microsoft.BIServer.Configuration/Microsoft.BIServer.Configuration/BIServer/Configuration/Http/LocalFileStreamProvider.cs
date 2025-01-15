using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration.Http
{
	// Token: 0x02000039 RID: 57
	internal class LocalFileStreamProvider : MultipartFormDataStreamProvider
	{
		// Token: 0x060001DE RID: 478 RVA: 0x000080C8 File Offset: 0x000062C8
		public LocalFileStreamProvider(string localDir, string localPrefix)
			: base(localDir)
		{
			this._localFileName = localPrefix + Guid.NewGuid() + ".large";
			this.LocalFileFullPath = Path.Combine(localDir, this._localFileName);
			Logger.Debug("Creating LocalFile as stream destination: {0}", new object[] { this.LocalFileFullPath });
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008122 File Offset: 0x00006322
		public FileInfo GetUploadedFile()
		{
			return new FileInfo(this.LocalFileFullPath);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000812F File Offset: 0x0000632F
		public override string GetLocalFileName(HttpContentHeaders headers)
		{
			return this.LocalFileFullPath;
		}

		// Token: 0x0400019C RID: 412
		private readonly string _localFileName;

		// Token: 0x0400019D RID: 413
		public readonly string LocalFileFullPath;
	}
}
