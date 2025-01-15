using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x02000099 RID: 153
	[DebuggerDisplay("InputReferenceExpression -> {Type}")]
	internal sealed class InputReferenceExpression : Expression
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x0001199F File Offset: 0x0000FB9F
		internal InputReferenceExpression(ResourceExpression target)
		{
			this.target = target;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x000119AE File Offset: 0x0000FBAE
		public override Type Type
		{
			get
			{
				return this.target.ResourceType;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000119BB File Offset: 0x0000FBBB
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10008;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x000119C2 File Offset: 0x0000FBC2
		internal ResourceExpression Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000119CA File Offset: 0x0000FBCA
		internal void OverrideTarget(QueryableResourceExpression newTarget)
		{
			this.target = newTarget;
		}

		// Token: 0x0400020B RID: 523
		private ResourceExpression target;
	}
}
