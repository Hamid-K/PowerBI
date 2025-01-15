using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000053 RID: 83
	[DataContract]
	internal sealed class TelemetryItem
	{
		// Token: 0x06000218 RID: 536 RVA: 0x0000639B File Offset: 0x0000459B
		internal TelemetryItem(string name, ExpressionNode expr)
		{
			this._name = name;
			this._expr = expr;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000063B1 File Offset: 0x000045B1
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000063B9 File Offset: 0x000045B9
		internal ExpressionNode Expression
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x04000148 RID: 328
		private readonly string _name;

		// Token: 0x04000149 RID: 329
		private readonly ExpressionNode _expr;
	}
}
