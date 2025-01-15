using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Owin
{
	// Token: 0x0200000F RID: 15
	public interface IReadableStringCollection : IEnumerable<KeyValuePair<string, string[]>>, IEnumerable
	{
		// Token: 0x1700003A RID: 58
		string this[string key] { get; }

		// Token: 0x060000A0 RID: 160
		string Get(string key);

		// Token: 0x060000A1 RID: 161
		IList<string> GetValues(string key);
	}
}
