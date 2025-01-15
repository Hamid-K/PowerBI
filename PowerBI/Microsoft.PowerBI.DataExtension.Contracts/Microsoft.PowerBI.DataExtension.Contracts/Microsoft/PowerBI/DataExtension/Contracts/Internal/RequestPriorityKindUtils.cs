using System;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x02000025 RID: 37
	public static class RequestPriorityKindUtils
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00002FA9 File Offset: 0x000011A9
		public static bool IsValid(this RequestPriorityKind requestPriority)
		{
			return requestPriority >= RequestPriorityKind.Normal && requestPriority <= RequestPriorityKind.Low;
		}
	}
}
