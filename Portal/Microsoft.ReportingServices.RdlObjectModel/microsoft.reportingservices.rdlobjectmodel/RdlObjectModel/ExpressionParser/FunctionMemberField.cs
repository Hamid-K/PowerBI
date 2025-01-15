using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002B8 RID: 696
	[Serializable]
	internal sealed class FunctionMemberField : BaseInternalExpression
	{
		// Token: 0x0600157D RID: 5501 RVA: 0x00031CF8 File Offset: 0x0002FEF8
		internal FunctionMemberField(IInternalExpression callTarget, string memberName)
		{
			this.m_callTarget = callTarget;
			this.m_memberName = memberName;
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00031D0E File Offset: 0x0002FF0E
		internal IInternalExpression CallTarget
		{
			get
			{
				return this.m_callTarget;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x00031D16 File Offset: 0x0002FF16
		internal string MemberName
		{
			get
			{
				return this.m_memberName;
			}
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00031D1E File Offset: 0x0002FF1E
		public override TypeCode TypeCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00031D25 File Offset: 0x0002FF25
		public override string WriteSource(NameChanges nameChanges)
		{
			if (this.m_callTarget != null)
			{
				return this.m_callTarget.WriteSource(nameChanges) + "." + this.m_memberName;
			}
			return this.m_memberName;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00031D52 File Offset: 0x0002FF52
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			if (this.m_callTarget != null)
			{
				this.m_callTarget.Traverse(callback);
			}
		}

		// Token: 0x040006CD RID: 1741
		private readonly IInternalExpression m_callTarget;

		// Token: 0x040006CE RID: 1742
		private readonly string m_memberName;
	}
}
