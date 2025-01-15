using System;
using System.Collections.Generic;

namespace dotless.Core.Parameters
{
	// Token: 0x020000AD RID: 173
	public class NullParameterSource : IParameterSource
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x00017344 File Offset: 0x00015544
		public IDictionary<string, string> GetParameters()
		{
			return new Dictionary<string, string>();
		}
	}
}
