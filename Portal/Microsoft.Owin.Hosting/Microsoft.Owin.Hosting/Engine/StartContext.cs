using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting.ServerFactory;
using Microsoft.Owin.Hosting.Utilities;
using Owin;

namespace Microsoft.Owin.Hosting.Engine
{
	// Token: 0x02000029 RID: 41
	public class StartContext
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00004A16 File Offset: 0x00002C16
		public StartContext(StartOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			SettingsLoader.LoadFromConfig(options.Settings);
			this.Options = options;
			this.EnvironmentData = new List<KeyValuePair<string, object>>();
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004A49 File Offset: 0x00002C49
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00004A51 File Offset: 0x00002C51
		public StartOptions Options { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004A5A File Offset: 0x00002C5A
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00004A62 File Offset: 0x00002C62
		public IServerFactoryAdapter ServerFactory { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004A6B File Offset: 0x00002C6B
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00004A73 File Offset: 0x00002C73
		public IAppBuilder Builder { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004A7C File Offset: 0x00002C7C
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00004A84 File Offset: 0x00002C84
		public Func<IDictionary<string, object>, Task> App { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004A8D File Offset: 0x00002C8D
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00004A95 File Offset: 0x00002C95
		public Action<IAppBuilder> Startup { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004A9E File Offset: 0x00002C9E
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00004AA6 File Offset: 0x00002CA6
		public TextWriter TraceOutput { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004AAF File Offset: 0x00002CAF
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00004AB7 File Offset: 0x00002CB7
		public IList<KeyValuePair<string, object>> EnvironmentData { get; private set; }
	}
}
