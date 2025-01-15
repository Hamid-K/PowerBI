using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002B7 RID: 695
	[Serializable]
	internal sealed class FunctionLateBoundAccessor : BaseInternalExpression
	{
		// Token: 0x06001576 RID: 5494 RVA: 0x00031B8A File Offset: 0x0002FD8A
		internal FunctionLateBoundAccessor(IInternalExpression callTarget, string methodName, List<IInternalExpression> args)
		{
			this.m_callTarget = callTarget;
			this.m_methodName = methodName;
			this.m_args = args;
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x00031BA7 File Offset: 0x0002FDA7
		internal IInternalExpression CallTarget
		{
			get
			{
				return this.m_callTarget;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x00031BAF File Offset: 0x0002FDAF
		internal string MethodName
		{
			get
			{
				return this.m_methodName;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x00031BB7 File Offset: 0x0002FDB7
		internal List<IInternalExpression> Args
		{
			get
			{
				return this.m_args;
			}
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00031BBF File Offset: 0x0002FDBF
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x00031BC4 File Offset: 0x0002FDC4
		public override string WriteSource(NameChanges nameChanges)
		{
			string text = this.m_callTarget.WriteSource(nameChanges);
			text += ".";
			text += this.MethodName;
			if (!this.m_callTarget.Bracketed)
			{
				return text;
			}
			text += "(";
			if (this.Args != null && this.Args.Count != 0)
			{
				text += this.Args[0].WriteSource(nameChanges);
				for (int i = 1; i < this.Args.Count; i++)
				{
					text += ", ";
					text += this.Args[i].WriteSource(nameChanges);
				}
			}
			return text + ")";
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00031C88 File Offset: 0x0002FE88
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

		// Token: 0x040006CA RID: 1738
		private readonly IInternalExpression m_callTarget;

		// Token: 0x040006CB RID: 1739
		private readonly string m_methodName;

		// Token: 0x040006CC RID: 1740
		private readonly List<IInternalExpression> m_args;
	}
}
