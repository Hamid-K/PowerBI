using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeBindingResolution
{
	// Token: 0x02000120 RID: 288
	public static class DataShapeBindingResolutionMessages
	{
		// Token: 0x06000782 RID: 1922 RVA: 0x0000F722 File Offset: 0x0000D922
		internal static QueryResolutionMessage CouldNotParseRestartToken()
		{
			return new QueryResolutionMessage("The DataShapeBinding contains a restart token with an invalid value.", null);
		}
	}
}
