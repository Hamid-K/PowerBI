using System;
using System.Threading.Tasks;

namespace AngleSharp
{
	// Token: 0x0200000C RID: 12
	public interface ICancellable<T> : ICancellable
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000063 RID: 99
		Task<T> Task { get; }
	}
}
