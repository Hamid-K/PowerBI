using System;
using System.IO;
using System.Text;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000036 RID: 54
	public abstract class RenderedOutputFile
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600009E RID: 158
		public abstract string FileName { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600009F RID: 159
		public abstract string Type { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000A0 RID: 160
		public abstract Stream Data { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000A1 RID: 161
		public abstract string Extension { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A2 RID: 162
		public abstract Encoding Encoding { get; }
	}
}
