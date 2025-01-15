using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000255 RID: 597
	[Serializable]
	internal sealed class FunctionAggrRunningValue : FunctionAggr
	{
		// Token: 0x06001384 RID: 4996 RVA: 0x0002EBC4 File Offset: 0x0002CDC4
		public FunctionAggrRunningValue(List<IInternalExpression> args)
			: base(args)
		{
			Identifier identifier = (Identifier)args[1];
			this._AggrName = identifier.Value;
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x0002EBF1 File Offset: 0x0002CDF1
		public string AggregateName
		{
			get
			{
				return this._AggrName;
			}
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0002EBFC File Offset: 0x0002CDFC
		public override string WriteSource(NameChanges nameChanges)
		{
			if (this._Expr == null)
			{
				return "";
			}
			string text = string.Empty;
			if (base.Scope != null)
			{
				text = base.GetScopeAsStringForWrite(nameChanges);
				return string.Concat(new string[]
				{
					"RunningValue(",
					this._Expr.WriteSource(nameChanges),
					", ",
					this.AggregateName,
					", ",
					text,
					")"
				});
			}
			return string.Concat(new string[]
			{
				"RunningValue(",
				this._Expr.WriteSource(nameChanges),
				", ",
				this.AggregateName,
				")"
			});
		}

		// Token: 0x04000690 RID: 1680
		private readonly string _AggrName;
	}
}
