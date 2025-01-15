using System;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B5A RID: 2906
	public static class FuzzyUtils
	{
		// Token: 0x06005074 RID: 20596 RVA: 0x0010D56C File Offset: 0x0010B76C
		public static double TruncateSimilarity(double similarityScore)
		{
			double num = 100.0;
			return Math.Truncate(similarityScore * num) / num;
		}

		// Token: 0x06005075 RID: 20597 RVA: 0x0010D590 File Offset: 0x0010B790
		public static void ValidateTextColumns(RecordValue recordValue, int[] columnKeys)
		{
			foreach (int num in columnKeys)
			{
				if (!FuzzyUtils.IsTextOrNullType(recordValue[num].Type.TypeKind))
				{
					throw ValueException.NewExpressionError<Message1>(Strings.FuzzyUtilInvalidColumnKeyType(recordValue.Keys[num]), null, null);
				}
			}
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x0010D5E0 File Offset: 0x0010B7E0
		public static void ValidateTextColumns(Query query, int[] columnKeys)
		{
			foreach (int num in columnKeys)
			{
				if (!FuzzyUtils.IsTextOrNullType(query.GetColumnType(num).TypeKind))
				{
					throw ValueException.NewExpressionError<Message1>(Strings.FuzzyUtilInvalidColumnKeyType(query.Columns[num]), null, null);
				}
			}
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x0010D62C File Offset: 0x0010B82C
		public static void ValidateTextColumns(TableValue tableValue, int[] columnKeys)
		{
			foreach (int num in columnKeys)
			{
				if (!FuzzyUtils.IsTextOrNullType(tableValue.GetColumnType(num).TypeKind))
				{
					throw ValueException.NewExpressionError<Message1>(Strings.FuzzyUtilInvalidColumnKeyType(tableValue.Columns[num]), null, null);
				}
			}
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x0010D677 File Offset: 0x0010B877
		private static bool IsTextOrNullType(ValueKind kind)
		{
			return kind == ValueKind.Text || kind == ValueKind.Null;
		}
	}
}
