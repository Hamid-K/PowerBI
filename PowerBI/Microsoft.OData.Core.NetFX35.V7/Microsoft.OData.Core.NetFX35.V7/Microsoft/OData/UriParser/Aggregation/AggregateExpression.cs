using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001C5 RID: 453
	public sealed class AggregateExpression
	{
		// Token: 0x060011C9 RID: 4553 RVA: 0x00031D29 File Offset: 0x0002FF29
		public AggregateExpression(SingleValueNode expression, AggregationMethod method, string alias, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<string>(alias, "alias");
			this.expression = expression;
			this.method = method;
			this.alias = alias;
			this.typeReference = typeReference;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00031D66 File Offset: 0x0002FF66
		public AggregateExpression(SingleValueNode expression, AggregationMethodDefinition methodDefinition, string alias, IEdmTypeReference typeReference)
			: this(expression, methodDefinition.MethodKind, alias, typeReference)
		{
			this.methodDefinition = methodDefinition;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00031D7F File Offset: 0x0002FF7F
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x00031D87 File Offset: 0x0002FF87
		public AggregationMethod Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x00031D8F File Offset: 0x0002FF8F
		public AggregationMethodDefinition MethodDefinition
		{
			get
			{
				return this.methodDefinition;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x00031D97 File Offset: 0x0002FF97
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x00031D9F File Offset: 0x0002FF9F
		public IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x0400090B RID: 2315
		private readonly AggregationMethod method;

		// Token: 0x0400090C RID: 2316
		private readonly AggregationMethodDefinition methodDefinition;

		// Token: 0x0400090D RID: 2317
		private readonly SingleValueNode expression;

		// Token: 0x0400090E RID: 2318
		private readonly string alias;

		// Token: 0x0400090F RID: 2319
		private readonly IEdmTypeReference typeReference;
	}
}
