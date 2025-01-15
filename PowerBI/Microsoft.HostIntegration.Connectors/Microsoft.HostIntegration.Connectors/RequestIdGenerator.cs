using System;
using System.Threading;

// Token: 0x02000374 RID: 884
internal class RequestIdGenerator
{
	// Token: 0x06001F49 RID: 8009 RVA: 0x0005F975 File Offset: 0x0005DB75
	public static int GetNewRequestId()
	{
		return Interlocked.Increment(ref RequestIdGenerator._input);
	}

	// Token: 0x04001208 RID: 4616
	private static int _input;
}
