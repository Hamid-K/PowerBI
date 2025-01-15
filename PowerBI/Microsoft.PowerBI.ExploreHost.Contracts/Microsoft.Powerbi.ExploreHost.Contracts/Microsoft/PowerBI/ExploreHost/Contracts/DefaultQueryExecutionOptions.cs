using System;

namespace Microsoft.PowerBI.ExploreHost.Contracts
{
	// Token: 0x02000009 RID: 9
	public sealed class DefaultQueryExecutionOptions : QueryExecutionOptionsBase
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002418 File Offset: 0x00000618
		private DefaultQueryExecutionOptions()
		{
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002420 File Offset: 0x00000620
		public override int ConnectionAttempts
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x04000032 RID: 50
		public static readonly DefaultQueryExecutionOptions Instance = new DefaultQueryExecutionOptions();
	}
}
