using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;

namespace Microsoft.DataShaping.QueryDesignModel.Common
{
	// Token: 0x020000C3 RID: 195
	internal class DaxUniqueNameGenerator : UniqueStringGenerator
	{
		// Token: 0x06000C69 RID: 3177 RVA: 0x000209EC File Offset: 0x0001EBEC
		public DaxUniqueNameGenerator()
			: base(DaxUniqueNameGenerator.DaxNameComparer)
		{
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000209F9 File Offset: 0x0001EBF9
		internal DaxUniqueNameGenerator(IEnumerable<string> stringsToRegister)
			: base(stringsToRegister, DaxUniqueNameGenerator.DaxNameComparer)
		{
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00020A07 File Offset: 0x0001EC07
		internal static string MakeUniqueColumnName(string columnName, IReadOnlyList<DaxResultColumn> otherColumns)
		{
			return UniqueStringGenerator.MakeUniqueString(columnName, otherColumns.Select((DaxResultColumn c) => c.DaxColumnRef.ColumnName), DaxUniqueNameGenerator.DaxNameComparer);
		}

		// Token: 0x0400096B RID: 2411
		private static readonly IEqualityComparer<string> DaxNameComparer = DaxRef.NameComparer;
	}
}
