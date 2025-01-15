using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001831 RID: 6193
	public interface IPandasTranslationOptions : IPythonTranslationOptions, ITranslationOptions, IRenderingOptions
	{
		// Token: 0x17002238 RID: 8760
		// (get) Token: 0x0600CB21 RID: 52001
		string DataFrameName { get; }

		// Token: 0x17002239 RID: 8761
		// (get) Token: 0x0600CB22 RID: 52002
		int? DerivedColumnIndex { get; }

		// Token: 0x1700223A RID: 8762
		// (get) Token: 0x0600CB23 RID: 52003
		string DerivedColumnName { get; }

		// Token: 0x1700223B RID: 8763
		// (get) Token: 0x0600CB24 RID: 52004
		bool ImportPandas { get; }

		// Token: 0x1700223C RID: 8764
		// (get) Token: 0x0600CB25 RID: 52005
		PandasOptimizations PandasOptimizations { get; }

		// Token: 0x1700223D RID: 8765
		// (get) Token: 0x0600CB26 RID: 52006
		string RegexLibrary { get; }

		// Token: 0x1700223E RID: 8766
		// (get) Token: 0x0600CB27 RID: 52007
		string TransformationFunctionName { get; }

		// Token: 0x0600CB28 RID: 52008
		IPandasTranslationOptions With(string definitionName = null, string dataFrameName = null, int? derivedColumnIndex = null, string derivedColumnName = null, bool? importPandas = null, uint? indentLevel = null, uint? indentSize = null, ILocalizedStrings localizedStrings = null, int? maximumExamplesInComments = null, PandasOptimizations? pandasOptimizations = null, PythonOptimizations? pythonOptimizations = null, string regexLibrary = null, string transformationFunctionName = null);
	}
}
