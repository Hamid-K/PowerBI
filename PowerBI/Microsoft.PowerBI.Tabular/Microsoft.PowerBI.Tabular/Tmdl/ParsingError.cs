using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000134 RID: 308
	internal enum ParsingError
	{
		// Token: 0x0400034D RID: 845
		Unknown,
		// Token: 0x0400034E RID: 846
		Indentation,
		// Token: 0x0400034F RID: 847
		UnsupportedScalarType,
		// Token: 0x04000350 RID: 848
		UnsupportedPropertyType,
		// Token: 0x04000351 RID: 849
		UnsupportedObjectType,
		// Token: 0x04000352 RID: 850
		MismatchObjectType,
		// Token: 0x04000353 RID: 851
		UnknownKeyword,
		// Token: 0x04000354 RID: 852
		MismatchPropertyName,
		// Token: 0x04000355 RID: 853
		MissingName,
		// Token: 0x04000356 RID: 854
		InvalidName,
		// Token: 0x04000357 RID: 855
		InvalidObjectHeader,
		// Token: 0x04000358 RID: 856
		MissingDefaultProperty,
		// Token: 0x04000359 RID: 857
		PropertyValueMismatch,
		// Token: 0x0400035A RID: 858
		InvalidValueFormat,
		// Token: 0x0400035B RID: 859
		InvalidLineType
	}
}
