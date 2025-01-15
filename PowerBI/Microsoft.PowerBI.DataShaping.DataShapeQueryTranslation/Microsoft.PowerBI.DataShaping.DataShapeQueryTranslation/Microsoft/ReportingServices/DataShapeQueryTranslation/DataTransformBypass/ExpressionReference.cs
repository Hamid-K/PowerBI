using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass
{
	// Token: 0x020000C3 RID: 195
	internal sealed class ExpressionReference : IEquatable<ExpressionReference>
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x0001FCFB File Offset: 0x0001DEFB
		internal ExpressionReference(IContextItem dataBoundOwner, ExpressionId expressionId)
		{
			this.m_dataBoundOwner = dataBoundOwner;
			this.m_expressionId = expressionId;
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0001FD11 File Offset: 0x0001DF11
		internal IContextItem Owner
		{
			get
			{
				return this.m_dataBoundOwner;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0001FD19 File Offset: 0x0001DF19
		internal ExpressionId ExpressionId
		{
			get
			{
				return this.m_expressionId;
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001FD21 File Offset: 0x0001DF21
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ExpressionReference);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001FD2F File Offset: 0x0001DF2F
		public bool Equals(ExpressionReference other)
		{
			return other != null && this.Owner == other.Owner && this.ExpressionId == other.ExpressionId;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001FD58 File Offset: 0x0001DF58
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Owner.GetHashCode(), this.ExpressionId.GetHashCode());
		}

		// Token: 0x04000415 RID: 1045
		private readonly IContextItem m_dataBoundOwner;

		// Token: 0x04000416 RID: 1046
		private readonly ExpressionId m_expressionId;
	}
}
