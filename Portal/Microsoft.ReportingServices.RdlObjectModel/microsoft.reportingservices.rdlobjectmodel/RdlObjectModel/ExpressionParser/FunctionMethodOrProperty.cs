using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002B9 RID: 697
	[Serializable]
	internal sealed class FunctionMethodOrProperty : BaseInternalExpression
	{
		// Token: 0x06001583 RID: 5507 RVA: 0x00031D68 File Offset: 0x0002FF68
		internal FunctionMethodOrProperty(IInternalExpression callTarget, string methodName, List<IInternalExpression> args)
		{
			this.m_callTarget = callTarget;
			this.m_methodName = methodName;
			this.m_args = args;
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x00031D85 File Offset: 0x0002FF85
		internal IInternalExpression CallTarget
		{
			get
			{
				return this.m_callTarget;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x00031D8D File Offset: 0x0002FF8D
		internal string MethodName
		{
			get
			{
				return this.m_methodName;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00031D95 File Offset: 0x0002FF95
		internal List<IInternalExpression> Args
		{
			get
			{
				return this.m_args;
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00031D9D File Offset: 0x0002FF9D
		public override TypeCode TypeCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00031DA4 File Offset: 0x0002FFA4
		public override string WriteSource(NameChanges nameChanges)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.m_callTarget != null)
			{
				bool flag = true;
				FunctionType functionType = this.m_callTarget as FunctionType;
				if (functionType != null)
				{
					if (functionType.TypeContext != null)
					{
						flag = !functionType.TypeContext.IsStandardModule();
					}
					else
					{
						flag = this.MethodName.ToUpperInvariant() != "CODE";
					}
				}
				if (flag)
				{
					stringBuilder.AppendFormat("{0}{1}", this.m_callTarget.WriteSource(nameChanges), ".");
				}
			}
			stringBuilder.Append(this.MethodName);
			if (this.Args != null && this.Args.Count != 0)
			{
				stringBuilder.Append("(");
				stringBuilder.Append(this.m_args[0].WriteSource(nameChanges));
				for (int i = 1; i < this.Args.Count; i++)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(this.Args[i].WriteSource(nameChanges));
				}
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00031EB4 File Offset: 0x000300B4
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			if (this.m_callTarget != null)
			{
				this.m_callTarget.Traverse(callback);
			}
			if (this.m_args != null)
			{
				foreach (IInternalExpression internalExpression in this.m_args)
				{
					internalExpression.Traverse(callback);
				}
			}
		}

		// Token: 0x040006CF RID: 1743
		private readonly IInternalExpression m_callTarget;

		// Token: 0x040006D0 RID: 1744
		private readonly string m_methodName;

		// Token: 0x040006D1 RID: 1745
		private readonly List<IInternalExpression> m_args;
	}
}
