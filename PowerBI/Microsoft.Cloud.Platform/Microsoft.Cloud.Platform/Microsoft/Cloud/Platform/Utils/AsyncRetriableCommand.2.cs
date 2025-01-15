using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000193 RID: 403
	public abstract class AsyncRetriableCommand<T> : AsyncRetriableCommandBase
	{
		// Token: 0x06000A5D RID: 2653 RVA: 0x00023D3D File Offset: 0x00021F3D
		public AsyncRetriableCommand(int numberOfAttempts, string commandDescription)
			: base(numberOfAttempts, commandDescription)
		{
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00023D47 File Offset: 0x00021F47
		public AsyncRetriableCommand(int numberOfAttempts, string commandDescription, TimeSpan timeBetweenAttempts)
			: base(numberOfAttempts, commandDescription, timeBetweenAttempts)
		{
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00023D52 File Offset: 0x00021F52
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x00023D5A File Offset: 0x00021F5A
		public T Result { get; protected set; }
	}
}
