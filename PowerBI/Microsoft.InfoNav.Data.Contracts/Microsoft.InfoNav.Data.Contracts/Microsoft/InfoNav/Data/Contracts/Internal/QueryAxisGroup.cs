using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000283 RID: 643
	[DataContract(Name = "Group", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryAxisGroup : IEquatable<QueryAxisGroup>
	{
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x00022E71 File Offset: 0x00021071
		// (set) Token: 0x06001366 RID: 4966 RVA: 0x00022E79 File Offset: 0x00021079
		[DataMember(IsRequired = true, Order = 0)]
		public List<QueryExpressionContainer> Keys { get; set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x00022E82 File Offset: 0x00021082
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x00022E8A File Offset: 0x0002108A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public bool Subtotal { get; set; }

		// Token: 0x06001369 RID: 4969 RVA: 0x00022E94 File Offset: 0x00021094
		public bool Equals(QueryAxisGroup other)
		{
			bool? flag = Util.AreEqual<QueryAxisGroup>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Subtotal == other.Subtotal && this.Keys.SequenceEqual(other.Keys);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00022EDB File Offset: 0x000210DB
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryAxisGroup);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00022EEC File Offset: 0x000210EC
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Subtotal.GetHashCode(), Hashing.CombineHash<QueryExpressionContainer>(this.Keys, null));
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00022F18 File Offset: 0x00021118
		public static bool operator ==(QueryAxisGroup left, QueryAxisGroup right)
		{
			bool? flag = Util.AreEqual<QueryAxisGroup>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00022F45 File Offset: 0x00021145
		public static bool operator !=(QueryAxisGroup left, QueryAxisGroup right)
		{
			return !(left == right);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00022F54 File Offset: 0x00021154
		internal void WriteQueryString(QueryStringWriter w)
		{
			QueryStringWriterUtils.WriteFunction<QueryExpressionContainer>("Group", this.Keys, QueryStringWriter.Separator.Comma, delegate(QueryExpressionContainer key, QueryStringWriter writer)
			{
				if (!(key == null))
				{
					key.WriteQueryString(w);
				}
			}, w);
			if (this.Subtotal)
			{
				w.Write(" with total");
			}
		}
	}
}
