using System;
using System.Collections.Generic;

namespace dotless.Core.Parameters
{
	// Token: 0x020000AB RID: 171
	public class ConsoleArgumentParameterSource : IParameterSource
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x00017329 File Offset: 0x00015529
		public IDictionary<string, string> GetParameters()
		{
			return ConsoleArgumentParameterSource.ConsoleArguments;
		}

		// Token: 0x040000F4 RID: 244
		public static IDictionary<string, string> ConsoleArguments = new Dictionary<string, string>();
	}
}
