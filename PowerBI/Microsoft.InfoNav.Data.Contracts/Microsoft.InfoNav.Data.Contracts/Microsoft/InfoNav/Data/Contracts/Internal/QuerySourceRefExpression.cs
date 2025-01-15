using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002D1 RID: 721
	[DataContract(Name = "SourceRefExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QuerySourceRefExpression : QueryExpression
	{
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x0002AD20 File Offset: 0x00028F20
		// (set) Token: 0x06001801 RID: 6145 RVA: 0x0002AD28 File Offset: 0x00028F28
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public string Source { get; set; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x0002AD31 File Offset: 0x00028F31
		// (set) Token: 0x06001803 RID: 6147 RVA: 0x0002AD39 File Offset: 0x00028F39
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public string Entity { get; set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x0002AD42 File Offset: 0x00028F42
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x0002AD4A File Offset: 0x00028F4A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public string Schema { get; set; }

		// Token: 0x06001806 RID: 6150 RVA: 0x0002AD53 File Offset: 0x00028F53
		internal override void WriteQueryString(QueryStringWriter w)
		{
			if (this.Source != null)
			{
				w.WriteIdentifierCustomerContent(this.Source);
				return;
			}
			w.WriteIdentifierCustomerContent(this.Entity);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0002AD76 File Offset: 0x00028F76
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0002AD7F File Offset: 0x00028F7F
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0002AD88 File Offset: 0x00028F88
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<string>(this.Source, null), Hashing.GetHashCode<string>(this.Entity, null), Hashing.GetHashCode<string>(this.Schema, null));
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0002ADC0 File Offset: 0x00028FC0
		public override bool Equals(QueryExpression other)
		{
			QuerySourceRefExpression querySourceRefExpression = other as QuerySourceRefExpression;
			bool? flag = Util.AreEqual<QuerySourceRefExpression>(this, querySourceRefExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryNameComparer.Instance.Equals(querySourceRefExpression.Source, this.Source) && QueryNameComparer.Instance.Equals(querySourceRefExpression.Entity, this.Entity) && QueryNameComparer.Instance.Equals(querySourceRefExpression.Schema, this.Schema);
		}
	}
}
