using System;
using System.Collections.Generic;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x0200000D RID: 13
	internal static class DataTransformResultFactory
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002446 File Offset: 0x00000646
		internal static DataTransformResult OriginalResultWithWarnings(IEnumerable<IDataRow> inputRows, IReadOnlyList<object> newColumns = null, IReadOnlyList<DataTransformMessage> messages = null)
		{
			if (newColumns == null || newColumns.Count == 0)
			{
				return new DataTransformResult(inputRows, messages);
			}
			return new DataTransformResult(DataTransformResultFactory.AddColumns(inputRows, newColumns), messages);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002468 File Offset: 0x00000668
		private static IEnumerable<IDataRow> AddColumns(IEnumerable<IDataRow> inputRows, IReadOnlyList<object> newColumns)
		{
			foreach (IDataRow dataRow in inputRows)
			{
				yield return dataRow.AddColumns(newColumns);
			}
			IEnumerator<IDataRow> enumerator = null;
			yield break;
			yield break;
		}
	}
}
