using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B42 RID: 6978
	internal class TempFileManager : IDisposable
	{
		// Token: 0x0600AEA4 RID: 44708 RVA: 0x0023C39B File Offset: 0x0023A59B
		public TempFileManager(IEngineHost engineHost)
		{
			this.tempDirectoryService = engineHost.QueryService<ITempDirectoryService>();
			this.tempFileStreamSharers = new Dictionary<string, StreamSharer>();
		}

		// Token: 0x0600AEA5 RID: 44709 RVA: 0x0023C3BC File Offset: 0x0023A5BC
		public string GenerateKey()
		{
			return Guid.NewGuid().ToString();
		}

		// Token: 0x0600AEA6 RID: 44710 RVA: 0x0023C3DC File Offset: 0x0023A5DC
		public Stream Create(string key)
		{
			StreamSharer streamSharer = new StreamSharer(this.tempDirectoryService.CreateFile());
			this.tempFileStreamSharers.Add(key, streamSharer);
			return streamSharer.Create();
		}

		// Token: 0x0600AEA7 RID: 44711 RVA: 0x0023C40D File Offset: 0x0023A60D
		public Stream Open(string key)
		{
			return this.tempFileStreamSharers[key].Open();
		}

		// Token: 0x0600AEA8 RID: 44712 RVA: 0x0023C420 File Offset: 0x0023A620
		public void Dispose()
		{
			foreach (StreamSharer streamSharer in this.tempFileStreamSharers.Values)
			{
				streamSharer.Close();
			}
		}

		// Token: 0x04005A0C RID: 23052
		private ITempDirectoryService tempDirectoryService;

		// Token: 0x04005A0D RID: 23053
		private readonly Dictionary<string, StreamSharer> tempFileStreamSharers;
	}
}
