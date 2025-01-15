using System;
using System.Collections.Generic;
using Microsoft.Owin.StaticFiles.Infrastructure;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x02000009 RID: 9
	public class DefaultFilesOptions : SharedOptionsBase<DefaultFilesOptions>
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002460 File Offset: 0x00000660
		public DefaultFilesOptions()
			: this(new SharedOptions())
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000246D File Offset: 0x0000066D
		public DefaultFilesOptions(SharedOptions sharedOptions)
			: base(sharedOptions)
		{
			this.DefaultFileNames = new List<string> { "default.htm", "default.html", "index.htm", "index.html" };
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024AD File Offset: 0x000006AD
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000024B5 File Offset: 0x000006B5
		public IList<string> DefaultFileNames { get; set; }
	}
}
