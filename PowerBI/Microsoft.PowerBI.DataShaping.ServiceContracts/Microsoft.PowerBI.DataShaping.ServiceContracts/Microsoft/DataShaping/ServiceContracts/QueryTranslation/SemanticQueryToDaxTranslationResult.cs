using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.ServiceContracts.QueryTranslation
{
	// Token: 0x0200001E RID: 30
	[DataContract]
	public sealed class SemanticQueryToDaxTranslationResult
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00002AE3 File Offset: 0x00000CE3
		public SemanticQueryToDaxTranslationResult(string daxExpression, ClusteringTranslationResult clusteringResult, string clusteringColumnResult, DataShapeEngineErrorInfo errorInfo)
		{
			this.DaxExpression = daxExpression;
			this.ClusteringTranslationResult = clusteringResult;
			this.ClusteringColumnTranslationResult = clusteringColumnResult;
			this.ErrorInfo = errorInfo;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002B08 File Offset: 0x00000D08
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00002B10 File Offset: 0x00000D10
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string DaxExpression { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002B19 File Offset: 0x00000D19
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00002B21 File Offset: 0x00000D21
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public ClusteringTranslationResult ClusteringTranslationResult { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002B2A File Offset: 0x00000D2A
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00002B32 File Offset: 0x00000D32
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string ClusteringColumnTranslationResult { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002B3B File Offset: 0x00000D3B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		internal DataShapeEngineErrorInfo ErrorInfo { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002B43 File Offset: 0x00000D43
		internal bool Succeeded
		{
			get
			{
				return this.ErrorInfo == null;
			}
		}
	}
}
