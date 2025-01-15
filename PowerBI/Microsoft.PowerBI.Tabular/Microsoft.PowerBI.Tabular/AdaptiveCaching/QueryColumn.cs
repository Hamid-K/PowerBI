using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Tabular.AdaptiveCaching
{
	// Token: 0x02000127 RID: 295
	[DataContract]
	[Serializable]
	internal struct QueryColumn : IEquatable<QueryColumn>
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x0008B008 File Offset: 0x00089208
		public override bool Equals(object obj)
		{
			if (obj is QueryColumn)
			{
				QueryColumn queryColumn = (QueryColumn)obj;
				return this.Equals(queryColumn);
			}
			return false;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0008B030 File Offset: 0x00089230
		public bool Equals(QueryColumn other)
		{
			AggregationType? aggregation = this.Aggregation;
			AggregationType? aggregation2 = other.Aggregation;
			return ((aggregation.GetValueOrDefault() == aggregation2.GetValueOrDefault()) & (aggregation != null == (aggregation2 != null))) && this.Table == other.Table && this.Column == other.Column;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0008B094 File Offset: 0x00089294
		public override int GetHashCode()
		{
			return ((1857747731 * -1521134295 + EqualityComparer<AggregationType?>.Default.GetHashCode(this.Aggregation)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Table)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Column);
		}

		// Token: 0x04000328 RID: 808
		[DataMember(Name = "aggregation")]
		public AggregationType? Aggregation;

		// Token: 0x04000329 RID: 809
		[DataMember(Name = "table")]
		public string Table;

		// Token: 0x0400032A RID: 810
		[DataMember(Name = "column")]
		public string Column;
	}
}
