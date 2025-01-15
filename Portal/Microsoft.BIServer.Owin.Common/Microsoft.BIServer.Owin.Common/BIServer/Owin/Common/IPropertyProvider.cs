using System;
using System.Collections.Generic;

namespace Microsoft.BIServer.Owin.Common
{
	// Token: 0x02000007 RID: 7
	public interface IPropertyProvider
	{
		// Token: 0x06000012 RID: 18
		IDictionary<string, string> GetProperties(IEnumerable<string> properties);
	}
}
