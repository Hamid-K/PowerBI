using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200017E RID: 382
	internal abstract class CalculationExpressionMap
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x0003782E File Offset: 0x00035A2E
		protected CalculationExpressionMap()
		{
			this.m_idToExpressions = new Dictionary<Identifier, List<ExpressionId>>();
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00037841 File Offset: 0x00035A41
		protected CalculationExpressionMap(CalculationExpressionMap map)
		{
			this.m_idToExpressions = map.m_idToExpressions;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00037858 File Offset: 0x00035A58
		public ReadOnlyCollection<ExpressionId> GetExpressions(Calculation calc)
		{
			ReadOnlyCollection<ExpressionId> readOnlyCollection;
			if (this.TryGetExpressions(calc, out readOnlyCollection))
			{
				return readOnlyCollection;
			}
			return Util.EmptyReadOnlyCollection<ExpressionId>();
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00037878 File Offset: 0x00035A78
		public bool TryGetExpressions(Calculation calc, out ReadOnlyCollection<ExpressionId> expressions)
		{
			List<ExpressionId> list;
			if (this.m_idToExpressions.TryGetValue(calc.Id, out list))
			{
				expressions = list.AsReadOnly();
				return true;
			}
			expressions = null;
			return false;
		}

		// Token: 0x040006A0 RID: 1696
		protected readonly Dictionary<Identifier, List<ExpressionId>> m_idToExpressions;
	}
}
