using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200053A RID: 1338
	internal sealed class FunctionImportNormalizedEntityTypeMapping
	{
		// Token: 0x060041D2 RID: 16850 RVA: 0x000DEFDC File Offset: 0x000DD1DC
		internal FunctionImportNormalizedEntityTypeMapping(FunctionImportStructuralTypeMappingKB parent, List<FunctionImportEntityTypeMappingCondition> columnConditions, BitArray impliedEntityTypes)
		{
			this.ColumnConditions = new ReadOnlyCollection<FunctionImportEntityTypeMappingCondition>(columnConditions.ToList<FunctionImportEntityTypeMappingCondition>());
			this.ImpliedEntityTypes = impliedEntityTypes;
			this.ComplementImpliedEntityTypes = new BitArray(this.ImpliedEntityTypes).Not();
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x000DF012 File Offset: 0x000DD212
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Values={0}, Types={1}", new object[]
			{
				StringUtil.ToCommaSeparatedString(this.ColumnConditions),
				StringUtil.ToCommaSeparatedString(this.ImpliedEntityTypes)
			});
		}

		// Token: 0x040016D3 RID: 5843
		internal readonly ReadOnlyCollection<FunctionImportEntityTypeMappingCondition> ColumnConditions;

		// Token: 0x040016D4 RID: 5844
		internal readonly BitArray ImpliedEntityTypes;

		// Token: 0x040016D5 RID: 5845
		internal readonly BitArray ComplementImpliedEntityTypes;
	}
}
