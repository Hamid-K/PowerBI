using System;
using System.Collections.Generic;

namespace Owin
{
	// Token: 0x02000002 RID: 2
	public interface IAppBuilder
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		IDictionary<string, object> Properties { get; }

		// Token: 0x06000002 RID: 2
		IAppBuilder Use(object middleware, params object[] args);

		// Token: 0x06000003 RID: 3
		object Build(Type returnType);

		// Token: 0x06000004 RID: 4
		IAppBuilder New();
	}
}
