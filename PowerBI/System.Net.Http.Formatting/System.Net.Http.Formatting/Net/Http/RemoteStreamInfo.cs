using System;
using System.IO;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000008 RID: 8
	public class RemoteStreamInfo
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000026F0 File Offset: 0x000008F0
		public RemoteStreamInfo(Stream remoteStream, string location, string fileName)
		{
			if (remoteStream == null)
			{
				throw Error.ArgumentNull("remoteStream");
			}
			if (location == null)
			{
				throw Error.ArgumentNull("location");
			}
			if (fileName == null)
			{
				throw Error.ArgumentNull("fileName");
			}
			this.FileName = fileName;
			this.RemoteStream = remoteStream;
			this.Location = location;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002742 File Offset: 0x00000942
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000274A File Offset: 0x0000094A
		public string FileName { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002753 File Offset: 0x00000953
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000275B File Offset: 0x0000095B
		public string Location { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002764 File Offset: 0x00000964
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000276C File Offset: 0x0000096C
		public Stream RemoteStream { get; private set; }
	}
}
