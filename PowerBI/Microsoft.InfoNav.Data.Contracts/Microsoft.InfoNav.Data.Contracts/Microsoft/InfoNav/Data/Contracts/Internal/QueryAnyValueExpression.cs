using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200027E RID: 638
	[DataContract(Name = "AnyValueExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryAnyValueExpression : QueryExpression
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00022AEC File Offset: 0x00020CEC
		// (set) Token: 0x06001348 RID: 4936 RVA: 0x00022AF4 File Offset: 0x00020CF4
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		internal bool DefaultValueOverridesAncestors { get; set; }

		// Token: 0x06001349 RID: 4937 RVA: 0x00022AFD File Offset: 0x00020CFD
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("anyValue");
			if (this.DefaultValueOverridesAncestors)
			{
				w.Write(" with ");
				w.Write("defaultValueOverridesAncestors");
			}
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00022B28 File Offset: 0x00020D28
		public override bool Equals(QueryExpression other)
		{
			QueryAnyValueExpression queryAnyValueExpression = other as QueryAnyValueExpression;
			bool? flag = Util.AreEqual<QueryAnyValueExpression>(this, queryAnyValueExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryAnyValueExpression.DefaultValueOverridesAncestors == this.DefaultValueOverridesAncestors;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00022B63 File Offset: 0x00020D63
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00022B70 File Offset: 0x00020D70
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00022B79 File Offset: 0x00020D79
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007F2 RID: 2034
		private const string AnyValue = "anyValue";

		// Token: 0x040007F3 RID: 2035
		private const string DefaultValueOverridesAncestorsString = "defaultValueOverridesAncestors";
	}
}
