using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000247 RID: 583
	[Serializable]
	internal abstract class FunctionAggr : BaseInternalExpression
	{
		// Token: 0x0600134F RID: 4943 RVA: 0x0002E3F4 File Offset: 0x0002C5F4
		public FunctionAggr(List<IInternalExpression> args)
		{
			this.m_args = args;
			if (this.m_args.Count > 0)
			{
				this._Expr = this.m_args[0];
			}
			if (this.m_args.Count > 1)
			{
				this._Scope = this.m_args[1];
				return;
			}
			this._Scope = null;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0002E456 File Offset: 0x0002C656
		public override TypeCode TypeCode()
		{
			return this._Expr.TypeCode();
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x0002E463 File Offset: 0x0002C663
		public IInternalExpression Expr
		{
			get
			{
				return this._Expr;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x0002E46B File Offset: 0x0002C66B
		// (set) Token: 0x06001353 RID: 4947 RVA: 0x0002E473 File Offset: 0x0002C673
		public object Scope
		{
			get
			{
				return this._Scope;
			}
			set
			{
				this._Scope = value;
			}
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0002E47C File Offset: 0x0002C67C
		internal virtual string DisplayText()
		{
			return "Count";
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0002E484 File Offset: 0x0002C684
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			if (this.m_args != null)
			{
				foreach (IInternalExpression internalExpression in this.m_args)
				{
					internalExpression.Traverse(callback);
				}
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0002E4E0 File Offset: 0x0002C6E0
		internal string GetScopeAsString()
		{
			if (this._Scope is ConstantString)
			{
				return ((ConstantString)this._Scope).EvaluateString();
			}
			return null;
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0002E504 File Offset: 0x0002C704
		protected string GetScopeAsStringForWrite(NameChanges nameChanges)
		{
			string text = this.GetScopeAsString();
			if (text != null && text.ToUpperInvariant() != "NOTHING")
			{
				text = "\"" + nameChanges.GetNewName(NameChanges.EntryType.Scope, text) + "\"";
			}
			return text;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0002E546 File Offset: 0x0002C746
		internal string GetScope()
		{
			return null;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0002E54C File Offset: 0x0002C74C
		public override object Evaluate()
		{
			TypeCode typeCode = this.TypeCode();
			if (typeCode == global::System.TypeCode.Boolean)
			{
				return false;
			}
			if (typeCode != global::System.TypeCode.Int32)
			{
				switch (typeCode)
				{
				case global::System.TypeCode.Double:
					return 1.0;
				case global::System.TypeCode.Decimal:
					return Convert.ToDecimal(1.0);
				case global::System.TypeCode.DateTime:
					return DateTime.Now;
				case global::System.TypeCode.String:
					return "1";
				}
				return "#Error";
			}
			return 1;
		}

		// Token: 0x0400068D RID: 1677
		public IInternalExpression _Expr;

		// Token: 0x0400068E RID: 1678
		public object _Scope;

		// Token: 0x0400068F RID: 1679
		protected List<IInternalExpression> m_args;
	}
}
