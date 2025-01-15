using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x0200004A RID: 74
	internal sealed class FilterExpressionInfo
	{
		// Token: 0x0600030B RID: 779 RVA: 0x00009102 File Offset: 0x00007302
		internal FilterExpressionInfo(Expression expression, ExpressionContext context, IReadOnlyList<Calculation> referencedCalculations)
		{
			this.m_expression = expression;
			this.m_context = context;
			this.ReferencedCalculations = referencedCalculations;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000911F File Offset: 0x0000731F
		public Expression Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00009127 File Offset: 0x00007327
		public ExpressionContext Context
		{
			get
			{
				return this.m_context;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000912F File Offset: 0x0000732F
		public IReadOnlyList<Calculation> ReferencedCalculations { get; }

		// Token: 0x0600030F RID: 783 RVA: 0x00009138 File Offset: 0x00007338
		public string ToDebugString()
		{
			string text = "E:{0},RC:{1}";
			object[] array = new object[2];
			array[0] = this.Expression.OriginalNode.ToString(ExpressionStringBuilderFactory.Create(null, false));
			array[1] = string.Join(",", this.ReferencedCalculations.Select((Calculation c) => c.Id.Value));
			return StringUtil.FormatInvariant(text, array);
		}

		// Token: 0x040000D7 RID: 215
		private readonly Expression m_expression;

		// Token: 0x040000D8 RID: 216
		private readonly ExpressionContext m_context;
	}
}
