using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002BB RID: 699
	[Serializable]
	internal sealed class FunctionNewObject : BaseInternalExpression
	{
		// Token: 0x06001590 RID: 5520 RVA: 0x00031F92 File Offset: 0x00030192
		internal FunctionNewObject(FunctionType typeExpr, List<IInternalExpression> args)
		{
			this.m_typeExpr = typeExpr;
			this.m_args = args;
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00031FA8 File Offset: 0x000301A8
		internal FunctionType TypeExpr
		{
			get
			{
				return this.m_typeExpr;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x00031FB0 File Offset: 0x000301B0
		internal List<IInternalExpression> Args
		{
			get
			{
				return this.m_args;
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00031FB8 File Offset: 0x000301B8
		public override TypeCode TypeCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00031FC0 File Offset: 0x000301C0
		public override string WriteSource(NameChanges nameChanges)
		{
			StringBuilder stringBuilder = new StringBuilder("New");
			stringBuilder.Append(this.m_typeExpr.WriteSource(nameChanges));
			stringBuilder.Append("(");
			if (this.m_args != null)
			{
				for (int i = 0; i < this.m_args.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(this.m_args[i].WriteSource(nameChanges));
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00032050 File Offset: 0x00030250
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			this.m_typeExpr.Traverse(callback);
			if (this.m_args != null)
			{
				foreach (IInternalExpression internalExpression in this.m_args)
				{
					internalExpression.Traverse(callback);
				}
			}
		}

		// Token: 0x040006D4 RID: 1748
		private readonly FunctionType m_typeExpr;

		// Token: 0x040006D5 RID: 1749
		private readonly List<IInternalExpression> m_args;
	}
}
