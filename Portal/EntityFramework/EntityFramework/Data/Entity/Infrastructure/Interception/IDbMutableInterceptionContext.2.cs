using System;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000293 RID: 659
	internal interface IDbMutableInterceptionContext<TResult> : IDbMutableInterceptionContext
	{
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060020E6 RID: 8422
		InterceptionContextMutableData<TResult> MutableData { get; }
	}
}
