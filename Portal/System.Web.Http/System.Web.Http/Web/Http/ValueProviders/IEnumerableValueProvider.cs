using System;
using System.Collections.Generic;

namespace System.Web.Http.ValueProviders
{
	// Token: 0x0200003A RID: 58
	public interface IEnumerableValueProvider : IValueProvider
	{
		// Token: 0x0600019D RID: 413
		IDictionary<string, string> GetKeysFromPrefix(string prefix);
	}
}
