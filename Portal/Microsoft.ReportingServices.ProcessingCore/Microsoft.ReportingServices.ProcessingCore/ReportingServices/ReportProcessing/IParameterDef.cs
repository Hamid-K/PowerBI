using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200061A RID: 1562
	internal interface IParameterDef
	{
		// Token: 0x060055BB RID: 21947
		bool HasDefaultValuesExpressions();

		// Token: 0x060055BC RID: 21948
		bool HasDefaultValuesDataSource();

		// Token: 0x17001F56 RID: 8022
		// (get) Token: 0x060055BD RID: 21949
		int DefaultValuesExpressionCount { get; }

		// Token: 0x060055BE RID: 21950
		bool HasValidValuesValueExpressions();

		// Token: 0x060055BF RID: 21951
		bool HasValidValuesLabelExpressions();

		// Token: 0x060055C0 RID: 21952
		bool HasValidValuesDataSource();

		// Token: 0x17001F57 RID: 8023
		// (get) Token: 0x060055C1 RID: 21953
		int ValidValuesValueExpressionCount { get; }

		// Token: 0x17001F58 RID: 8024
		// (get) Token: 0x060055C2 RID: 21954
		int ValidValuesLabelExpressionCount { get; }

		// Token: 0x17001F59 RID: 8025
		// (get) Token: 0x060055C3 RID: 21955
		string Name { get; }

		// Token: 0x17001F5A RID: 8026
		// (get) Token: 0x060055C4 RID: 21956
		ObjectType ParameterObjectType { get; }

		// Token: 0x17001F5B RID: 8027
		// (get) Token: 0x060055C5 RID: 21957
		DataType DataType { get; }

		// Token: 0x17001F5C RID: 8028
		// (get) Token: 0x060055C6 RID: 21958
		bool MultiValue { get; }

		// Token: 0x17001F5D RID: 8029
		// (get) Token: 0x060055C7 RID: 21959
		IParameterDataSource DefaultDataSource { get; }

		// Token: 0x17001F5E RID: 8030
		// (get) Token: 0x060055C8 RID: 21960
		IParameterDataSource ValidValuesDataSource { get; }

		// Token: 0x17001F5F RID: 8031
		// (get) Token: 0x060055C9 RID: 21961
		bool UseAllValidValues { get; }

		// Token: 0x060055CA RID: 21962
		bool ValidateValueForNull(object newValue, ErrorContext errorContext, string parameterValueProperty);

		// Token: 0x060055CB RID: 21963
		bool ValidateValueForBlank(object newValue, ErrorContext errorContext, string parameterValueProperty);
	}
}
