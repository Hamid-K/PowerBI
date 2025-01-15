using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Tabular.AdaptiveCaching
{
	// Token: 0x02000129 RID: 297
	[DataContract]
	internal struct QueryBatch : IEquatable<QueryBatch>
	{
		// Token: 0x0600147E RID: 5246 RVA: 0x0008B218 File Offset: 0x00089418
		public override bool Equals(object obj)
		{
			if (obj is QueryBatch)
			{
				QueryBatch queryBatch = (QueryBatch)obj;
				return this.Equals(queryBatch);
			}
			return false;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0008B240 File Offset: 0x00089440
		public bool Equals(QueryBatch other)
		{
			if (this.Queries.Count != other.Queries.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Queries.Count; i++)
			{
				if (!this.Queries[i].Equals(other.Queries[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0008B2A4 File Offset: 0x000894A4
		public override int GetHashCode()
		{
			int num = 361752385;
			if (this.Queries != null)
			{
				for (int i = 0; i < this.Queries.Count; i++)
				{
					num = num * -1521134295 + this.Queries[i].GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x0400032D RID: 813
		[DataMember(Name = "queries")]
		public List<QueryShape> Queries;
	}
}
