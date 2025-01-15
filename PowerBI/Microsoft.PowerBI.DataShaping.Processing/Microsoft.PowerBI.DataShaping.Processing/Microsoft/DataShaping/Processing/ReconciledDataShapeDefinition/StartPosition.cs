using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000052 RID: 82
	internal sealed class StartPosition
	{
		// Token: 0x06000215 RID: 533 RVA: 0x00006375 File Offset: 0x00004575
		internal StartPosition(IList<object> values, IList<ExpressionNode> expressions)
		{
			this._values = values;
			this._expressions = expressions;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000638B File Offset: 0x0000458B
		public IList<object> Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00006393 File Offset: 0x00004593
		internal IList<ExpressionNode> Expressions
		{
			get
			{
				return this._expressions;
			}
		}

		// Token: 0x04000146 RID: 326
		private readonly IList<object> _values;

		// Token: 0x04000147 RID: 327
		private readonly IList<ExpressionNode> _expressions;
	}
}
