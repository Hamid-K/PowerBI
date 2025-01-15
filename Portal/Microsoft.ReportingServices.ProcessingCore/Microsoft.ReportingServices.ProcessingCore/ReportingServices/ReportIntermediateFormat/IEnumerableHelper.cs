using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003BE RID: 958
	internal class IEnumerableHelper
	{
		// Token: 0x060026D1 RID: 9937 RVA: 0x000B9D0A File Offset: 0x000B7F0A
		internal static IEnumerable<T> ConvertToTyped<T>(IEnumerable aEnum)
		{
			foreach (object obj in aEnum)
			{
				yield return (T)((object)obj);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}
	}
}
