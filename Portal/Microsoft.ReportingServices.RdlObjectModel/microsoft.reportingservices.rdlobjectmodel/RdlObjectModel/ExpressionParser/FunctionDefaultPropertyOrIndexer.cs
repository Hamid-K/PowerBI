using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002B5 RID: 693
	[Serializable]
	internal sealed class FunctionDefaultPropertyOrIndexer : BaseInternalExpression
	{
		// Token: 0x0600156A RID: 5482 RVA: 0x000319F6 File Offset: 0x0002FBF6
		internal FunctionDefaultPropertyOrIndexer(IInternalExpression callTarget, List<IInternalExpression> args)
		{
			this.m_callTarget = callTarget;
			this.m_args = args;
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x00031A0C File Offset: 0x0002FC0C
		internal IInternalExpression CallTarget
		{
			get
			{
				return this.m_callTarget;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x00031A14 File Offset: 0x0002FC14
		internal List<IInternalExpression> Args
		{
			get
			{
				return this.m_args;
			}
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00031A1C File Offset: 0x0002FC1C
		public override TypeCode TypeCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00031A24 File Offset: 0x0002FC24
		public override string WriteSource(NameChanges nameChanges)
		{
			if (this.m_callTarget == null)
			{
				return "";
			}
			string text = this.m_callTarget.WriteSource(nameChanges);
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

		// Token: 0x0600156F RID: 5487 RVA: 0x00031AD0 File Offset: 0x0002FCD0
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

		// Token: 0x040006C6 RID: 1734
		private readonly IInternalExpression m_callTarget;

		// Token: 0x040006C7 RID: 1735
		private readonly List<IInternalExpression> m_args;
	}
}
