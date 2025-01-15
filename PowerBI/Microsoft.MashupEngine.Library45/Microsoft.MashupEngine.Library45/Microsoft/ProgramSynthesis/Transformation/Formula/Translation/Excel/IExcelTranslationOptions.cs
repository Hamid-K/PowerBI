using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001938 RID: 6456
	public interface IExcelTranslationOptions : ITranslationOptions
	{
		// Token: 0x17002303 RID: 8963
		// (get) Token: 0x0600D33D RID: 54077
		bool EnableFindN { get; }

		// Token: 0x17002304 RID: 8964
		// (get) Token: 0x0600D33E RID: 54078
		bool EnableTextAfter { get; }

		// Token: 0x17002305 RID: 8965
		// (get) Token: 0x0600D33F RID: 54079
		bool EnableTextBefore { get; }

		// Token: 0x17002306 RID: 8966
		// (get) Token: 0x0600D340 RID: 54080
		bool EnableTextSlice { get; }

		// Token: 0x17002307 RID: 8967
		// (get) Token: 0x0600D341 RID: 54081
		ExcelOptimizations Optimizations { get; }

		// Token: 0x17002308 RID: 8968
		// (get) Token: 0x0600D342 RID: 54082
		string ReferenceRowExpression { get; }

		// Token: 0x17002309 RID: 8969
		// (get) Token: 0x0600D343 RID: 54083
		CultureInfo UserInterfaceCulture { get; }
	}
}
