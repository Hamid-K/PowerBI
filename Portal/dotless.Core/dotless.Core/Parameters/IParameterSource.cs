using System;
using System.Collections.Generic;

namespace dotless.Core.Parameters
{
	// Token: 0x020000AC RID: 172
	public interface IParameterSource
	{
		// Token: 0x06000503 RID: 1283
		IDictionary<string, string> GetParameters();
	}
}
