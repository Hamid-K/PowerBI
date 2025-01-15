using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Privacy
{
	// Token: 0x020000D2 RID: 210
	public static class ContainsMarkedInformationExtensions
	{
		// Token: 0x0600107F RID: 4223 RVA: 0x000456AA File Offset: 0x000438AA
		[NullableContext(1)]
		public static string ToMarkedString(this IContainsMarkedInformation markedInfo)
		{
			if (markedInfo == null)
			{
				return string.Empty;
			}
			return markedInfo.BuildMarkedString();
		}
	}
}
