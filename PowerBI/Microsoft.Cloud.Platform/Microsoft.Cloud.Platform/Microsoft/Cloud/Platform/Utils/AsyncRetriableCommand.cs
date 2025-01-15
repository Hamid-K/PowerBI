using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000192 RID: 402
	public abstract class AsyncRetriableCommand : AsyncRetriableCommandBase
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x00023D3D File Offset: 0x00021F3D
		public AsyncRetriableCommand(int numberOfAttempts, string commandDescription)
			: base(numberOfAttempts, commandDescription)
		{
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00023D47 File Offset: 0x00021F47
		public AsyncRetriableCommand(int numberOfAttempts, string commandDescription, TimeSpan timeBetweenAttempts)
			: base(numberOfAttempts, commandDescription, timeBetweenAttempts)
		{
		}
	}
}
