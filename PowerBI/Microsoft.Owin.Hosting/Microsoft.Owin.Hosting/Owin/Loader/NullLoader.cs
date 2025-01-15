using System;
using System.Collections.Generic;

namespace Owin.Loader
{
	// Token: 0x02000004 RID: 4
	internal class NullLoader
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002B70 File Offset: 0x00000D70
		public static Func<string, IList<string>, Action<IAppBuilder>> Instance
		{
			get
			{
				return new Func<string, IList<string>, Action<IAppBuilder>>(NullLoader.Singleton.Load);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002B82 File Offset: 0x00000D82
		public Action<IAppBuilder> Load(string startup, IList<string> errors)
		{
			return null;
		}

		// Token: 0x0400000B RID: 11
		private static readonly NullLoader Singleton = new NullLoader();
	}
}
