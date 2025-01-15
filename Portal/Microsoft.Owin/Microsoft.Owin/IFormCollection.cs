using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Owin
{
	// Token: 0x0200000A RID: 10
	public interface IFormCollection : IReadableStringCollection, IEnumerable<KeyValuePair<string, string[]>>, IEnumerable
	{
	}
}
