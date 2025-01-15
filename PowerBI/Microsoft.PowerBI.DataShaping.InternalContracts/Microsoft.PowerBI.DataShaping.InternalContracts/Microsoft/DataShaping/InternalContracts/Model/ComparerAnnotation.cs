using System;
using System.Globalization;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Common.DaxComparer;
using Microsoft.InfoNav;

namespace Microsoft.DataShaping.InternalContracts.Model
{
	// Token: 0x02000024 RID: 36
	public class ComparerAnnotation
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00003755 File Offset: 0x00001955
		public ComparerAnnotation(IConceptualSchema schema)
		{
			this.Comparer = ComparerAnnotation.BuildComparer(schema.ConceptualCollation.Culture, schema.ConceptualCollation.CompareOptions);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000377E File Offset: 0x0000197E
		public ComparerAnnotation(IDataComparer comparer)
		{
			this.Comparer = comparer;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000378D File Offset: 0x0000198D
		public static IDataComparer BuildComparer(string culture, CompareOptions compareOptions)
		{
			return new DaxDataComparer(CompareInfo.GetCompareInfo(culture ?? string.Empty), compareOptions, true);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000037A5 File Offset: 0x000019A5
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x000037AD File Offset: 0x000019AD
		public IDataComparer Comparer { get; private set; }
	}
}
