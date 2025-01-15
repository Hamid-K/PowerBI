using System;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000253 RID: 595
	public interface IObjectContextAdapter
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001EC0 RID: 7872
		ObjectContext ObjectContext { get; }
	}
}
