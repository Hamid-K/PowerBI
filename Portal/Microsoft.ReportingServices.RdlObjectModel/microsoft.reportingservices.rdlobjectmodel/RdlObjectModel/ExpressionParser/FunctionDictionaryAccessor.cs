using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002B6 RID: 694
	[Serializable]
	internal sealed class FunctionDictionaryAccessor : BaseInternalExpression
	{
		// Token: 0x06001570 RID: 5488 RVA: 0x00031B40 File Offset: 0x0002FD40
		internal FunctionDictionaryAccessor(IInternalExpression callTarget, string dictionaryArg)
		{
			this.m_callTarget = callTarget;
			this.m_dictionaryArg = dictionaryArg;
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x00031B56 File Offset: 0x0002FD56
		internal IInternalExpression CallTarget
		{
			get
			{
				return this.m_callTarget;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x00031B5E File Offset: 0x0002FD5E
		internal string DictionaryArg
		{
			get
			{
				return this.m_dictionaryArg;
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00031B66 File Offset: 0x0002FD66
		public override TypeCode TypeCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00031B6D File Offset: 0x0002FD6D
		public override string WriteSource(NameChanges nameChanges)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00031B74 File Offset: 0x0002FD74
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			if (this.m_callTarget != null)
			{
				this.m_callTarget.Traverse(callback);
			}
		}

		// Token: 0x040006C8 RID: 1736
		private readonly IInternalExpression m_callTarget;

		// Token: 0x040006C9 RID: 1737
		private readonly string m_dictionaryArg;
	}
}
