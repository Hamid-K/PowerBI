using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001F0 RID: 496
	public sealed class AggregateExpression : AggregateExpressionBase
	{
		// Token: 0x0600164E RID: 5710 RVA: 0x0003E479 File Offset: 0x0003C679
		public AggregateExpression(SingleValueNode expression, AggregationMethod method, string alias, IEdmTypeReference typeReference)
			: base(AggregateExpressionKind.PropertyAggregate, alias)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<string>(alias, "alias");
			this.expression = expression;
			this.method = method;
			this.typeReference = typeReference;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0003E4B1 File Offset: 0x0003C6B1
		public AggregateExpression(SingleValueNode expression, AggregationMethodDefinition methodDefinition, string alias, IEdmTypeReference typeReference)
			: this(expression, methodDefinition.MethodKind, alias, typeReference)
		{
			this.methodDefinition = methodDefinition;
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0003E4CA File Offset: 0x0003C6CA
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x0003E4D2 File Offset: 0x0003C6D2
		public AggregationMethod Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0003E4DA File Offset: 0x0003C6DA
		public AggregationMethodDefinition MethodDefinition
		{
			get
			{
				return this.methodDefinition;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0003E4E2 File Offset: 0x0003C6E2
		public IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x04000A0D RID: 2573
		private readonly AggregationMethod method;

		// Token: 0x04000A0E RID: 2574
		private readonly AggregationMethodDefinition methodDefinition;

		// Token: 0x04000A0F RID: 2575
		private readonly SingleValueNode expression;

		// Token: 0x04000A10 RID: 2576
		private readonly IEdmTypeReference typeReference;
	}
}
