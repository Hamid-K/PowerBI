using System;
using System.ComponentModel;

namespace Microsoft.Identity.Client.Extensibility
{
	// Token: 0x02000294 RID: 660
	public static class AcquireTokenOnBehalfOfParameterBuilderExtensions
	{
		// Token: 0x06001922 RID: 6434 RVA: 0x00052C4C File Offset: 0x00050E4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static AcquireTokenOnBehalfOfParameterBuilder WithSearchInCacheForLongRunningProcess(this AcquireTokenOnBehalfOfParameterBuilder builder, bool searchInCache = true)
		{
			builder.Parameters.SearchInCacheForLongRunningObo = searchInCache;
			return builder;
		}
	}
}
