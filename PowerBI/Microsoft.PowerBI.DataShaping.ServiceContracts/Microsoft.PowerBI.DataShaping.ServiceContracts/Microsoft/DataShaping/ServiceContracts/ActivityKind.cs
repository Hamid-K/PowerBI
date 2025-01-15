using System;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x02000005 RID: 5
	public enum ActivityKind
	{
		// Token: 0x0400002A RID: 42
		CancelCommand,
		// Token: 0x0400002B RID: 43
		CheckConnectionIsAlive,
		// Token: 0x0400002C RID: 44
		CloseDataExtension,
		// Token: 0x0400002D RID: 45
		DataShapeProcessing,
		// Token: 0x0400002E RID: 46
		DataShapeResultGeneration,
		// Token: 0x0400002F RID: 47
		DataTransformExecution,
		// Token: 0x04000030 RID: 48
		ExecuteReader,
		// Token: 0x04000031 RID: 49
		NextResultSet,
		// Token: 0x04000032 RID: 50
		OpenConnection,
		// Token: 0x04000033 RID: 51
		QueryExecution,
		// Token: 0x04000034 RID: 52
		RawDataGeneration,
		// Token: 0x04000035 RID: 53
		DaxQueryExecution,
		// Token: 0x04000036 RID: 54
		DaxQueryResultGeneration,
		// Token: 0x04000037 RID: 55
		TransformSchema,
		// Token: 0x04000038 RID: 56
		TransformData,
		// Token: 0x04000039 RID: 57
		DataShapeQueryGeneration,
		// Token: 0x0400003A RID: 58
		DataShapeQueryTranslation,
		// Token: 0x0400003B RID: 59
		ExecuteSemanticQuery,
		// Token: 0x0400003C RID: 60
		ExecuteSemanticQueryRawData,
		// Token: 0x0400003D RID: 61
		ExecuteDaxQuery,
		// Token: 0x0400003E RID: 62
		TranslateSemanticQuery,
		// Token: 0x0400003F RID: 63
		TranslateClustering,
		// Token: 0x04000040 RID: 64
		TranslateClusteringColumn,
		// Token: 0x04000041 RID: 65
		TranslateGrouping,
		// Token: 0x04000042 RID: 66
		TranslateDataView,
		// Token: 0x04000043 RID: 67
		SemanticTranslation
	}
}
