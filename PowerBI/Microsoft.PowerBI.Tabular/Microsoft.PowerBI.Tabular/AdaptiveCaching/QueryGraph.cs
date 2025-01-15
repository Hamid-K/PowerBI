using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Tabular.AdaptiveCaching
{
	// Token: 0x0200012B RID: 299
	[DataContract]
	internal struct QueryGraph
	{
		// Token: 0x04000332 RID: 818
		[DataMember(Name = "rank")]
		public int Rank;

		// Token: 0x04000333 RID: 819
		[DataMember(Name = "estimatedCardinality(Krows)")]
		public double RowCount;

		// Token: 0x04000334 RID: 820
		[DataMember(Name = "activeFrequency")]
		public long CoveredFrequency;

		// Token: 0x04000335 RID: 821
		[DataMember(Name = "queryShape")]
		public QueryShape QueryShape;

		// Token: 0x04000336 RID: 822
		[DataMember(Name = "joinGraph")]
		public List<JoinEdge> JoinGraph;

		// Token: 0x04000337 RID: 823
		[DataMember(Name = "datasetId")]
		public string DatasetId;

		// Token: 0x04000338 RID: 824
		[DataMember(Name = "relativeSize(%)")]
		public double RelativeSize;

		// Token: 0x04000339 RID: 825
		[DataMember(Name = "relativeLift(%)")]
		public double RelativeLift;

		// Token: 0x0400033A RID: 826
		[DataMember(Name = "relativeGain(%)")]
		public double RelativeGain;

		// Token: 0x0400033B RID: 827
		[DataMember(Name = "activeQueries")]
		public int CoveredPatterns;

		// Token: 0x0400033C RID: 828
		[DataMember(Name = "avoidQueryHints")]
		public bool AvoidQueryHints;
	}
}
