using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000052 RID: 82
	internal sealed class ExpressionProcessInfoCollection : ReadOnlyItemCollectionBase<ExpressionProcessInfo>, IQPExpressionInfoCollection
	{
		// Token: 0x17000089 RID: 137
		IQPExpressionInfo IQPExpressionInfoCollection.this[int index]
		{
			[DebuggerStepThrough]
			get
			{
				return base[index];
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0001126A File Offset: 0x0000F46A
		int IQPExpressionInfoCollection.Count
		{
			[DebuggerStepThrough]
			get
			{
				return base.Count;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00011272 File Offset: 0x0000F472
		internal override void Insert(int index, ExpressionProcessInfo item)
		{
			this.m_exprToExprProcInfo.Add(item.ObjKey, item);
			base.Insert(index, item);
		}

		// Token: 0x1700008B RID: 139
		internal ExpressionProcessInfo this[Expression objKey]
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_exprToExprProcInfo[objKey];
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001129C File Offset: 0x0000F49C
		internal bool Contains(Expression objKey)
		{
			return this.m_exprToExprProcInfo.ContainsKey(objKey);
		}

		// Token: 0x040001B6 RID: 438
		private readonly Dictionary<Expression, ExpressionProcessInfo> m_exprToExprProcInfo = new Dictionary<Expression, ExpressionProcessInfo>();
	}
}
