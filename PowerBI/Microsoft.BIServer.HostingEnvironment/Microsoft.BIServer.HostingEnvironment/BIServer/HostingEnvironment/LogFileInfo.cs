using System;
using System.IO;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000017 RID: 23
	public class LogFileInfo
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00003A88 File Offset: 0x00001C88
		public LogFileInfo(string fullName, DateTime lastWriteTime)
		{
			this.FullName = fullName;
			this.LastWriteTime = lastWriteTime;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003A9E File Offset: 0x00001C9E
		public LogFileInfo(FileInfo fileInfo)
		{
			this.FullName = fileInfo.FullName;
			this.LastWriteTime = fileInfo.LastWriteTime;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003ABE File Offset: 0x00001CBE
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003AC6 File Offset: 0x00001CC6
		public string FullName { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003ACF File Offset: 0x00001CCF
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003AD7 File Offset: 0x00001CD7
		public DateTime LastWriteTime { get; private set; }
	}
}
