using System;

namespace NLog.Common
{
	// Token: 0x020001B9 RID: 441
	// (Invoke) Token: 0x06001380 RID: 4992
	public delegate void AsynchronousAction<in T>(T argument, AsyncContinuation asyncContinuation);
}
