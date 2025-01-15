using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003D6 RID: 982
	internal class FailPointCriteriaExpression : IFailPointCriteria
	{
		// Token: 0x06002287 RID: 8839 RVA: 0x0006AA88 File Offset: 0x00068C88
		public FailPointCriteriaExpression(string exp)
		{
			if (exp == null)
			{
				throw new ArgumentNullException("exp");
			}
			this.m_exp = exp;
			try
			{
				this.m_predicate = PropertyExpression.Build(exp);
			}
			catch (Exception ex)
			{
				throw new FailPointException("Invalid fail point criteria expression", ex);
			}
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x0006AADC File Offset: 0x00068CDC
		public bool Match(FailPointContext context)
		{
			object obj = this.m_predicate.Eval(context);
			return (bool)obj;
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0006AAFC File Offset: 0x00068CFC
		public override bool Equals(object obj)
		{
			FailPointCriteriaExpression failPointCriteriaExpression = obj as FailPointCriteriaExpression;
			return failPointCriteriaExpression != null && this.m_exp.Equals(failPointCriteriaExpression.m_exp);
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0006AB26 File Offset: 0x00068D26
		public override int GetHashCode()
		{
			return this.m_exp.GetHashCode();
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x0006AB33 File Offset: 0x00068D33
		public override string ToString()
		{
			return this.m_predicate.ToString();
		}

		// Token: 0x040015B7 RID: 5559
		private string m_exp;

		// Token: 0x040015B8 RID: 5560
		private PropertyExpression m_predicate;
	}
}
