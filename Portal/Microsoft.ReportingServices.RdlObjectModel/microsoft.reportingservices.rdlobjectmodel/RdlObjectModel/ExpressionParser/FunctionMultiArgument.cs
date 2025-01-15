using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000272 RID: 626
	internal abstract class FunctionMultiArgument : BaseInternalExpression
	{
		// Token: 0x060013FF RID: 5119 RVA: 0x0002F92C File Offset: 0x0002DB2C
		protected FunctionMultiArgument(IInternalExpression[] args)
		{
			this.Arguments = args;
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x0002F93B File Offset: 0x0002DB3B
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x0002F943 File Offset: 0x0002DB43
		public IInternalExpression[] Arguments
		{
			get
			{
				return this._args;
			}
			protected set
			{
				this._args = value ?? new IInternalExpression[0];
			}
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0002F958 File Offset: 0x0002DB58
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			if (this._args != null)
			{
				IInternalExpression[] args = this._args;
				for (int i = 0; i < args.Length; i++)
				{
					args[i].Traverse(callback);
				}
			}
		}

		// Token: 0x0400069B RID: 1691
		private IInternalExpression[] _args;
	}
}
