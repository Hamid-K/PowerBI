using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000023 RID: 35
	internal sealed class ExpressionIdGenerator
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x000069FC File Offset: 0x00004BFC
		public ExpressionId NewId()
		{
			int nextId = this.m_nextId;
			this.m_nextId = nextId + 1;
			return new ExpressionId(nextId);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00006A1F File Offset: 0x00004C1F
		public int IdCount
		{
			get
			{
				return this.m_nextId;
			}
		}

		// Token: 0x0400005B RID: 91
		private int m_nextId;
	}
}
