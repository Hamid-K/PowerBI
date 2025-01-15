using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019D7 RID: 6615
	public class PandasTranslationConstraint : PythonTranslationConstraint, IPandasTranslationOptions, IPythonTranslationOptions, ITranslationOptions, IRenderingOptions, IUniqueConstraint<PandasTranslationConstraint>
	{
		// Token: 0x1700240A RID: 9226
		// (get) Token: 0x0600D80E RID: 55310 RVA: 0x002DE29F File Offset: 0x002DC49F
		// (set) Token: 0x0600D80F RID: 55311 RVA: 0x002DE2A7 File Offset: 0x002DC4A7
		public string DataFrameName { get; set; } = "df";

		// Token: 0x1700240B RID: 9227
		// (get) Token: 0x0600D810 RID: 55312 RVA: 0x002DE2B0 File Offset: 0x002DC4B0
		// (set) Token: 0x0600D811 RID: 55313 RVA: 0x002DE2B8 File Offset: 0x002DC4B8
		public int? DerivedColumnIndex { get; set; }

		// Token: 0x1700240C RID: 9228
		// (get) Token: 0x0600D812 RID: 55314 RVA: 0x002DE2C1 File Offset: 0x002DC4C1
		// (set) Token: 0x0600D813 RID: 55315 RVA: 0x002DE2C9 File Offset: 0x002DC4C9
		public string DerivedColumnName { get; set; } = "derived_column";

		// Token: 0x1700240D RID: 9229
		// (get) Token: 0x0600D814 RID: 55316 RVA: 0x002DE2D2 File Offset: 0x002DC4D2
		// (set) Token: 0x0600D815 RID: 55317 RVA: 0x002DE2DA File Offset: 0x002DC4DA
		public bool ImportPandas { get; set; }

		// Token: 0x1700240E RID: 9230
		// (get) Token: 0x0600D816 RID: 55318 RVA: 0x002DE2E3 File Offset: 0x002DC4E3
		// (set) Token: 0x0600D817 RID: 55319 RVA: 0x002DE2EB File Offset: 0x002DC4EB
		public PandasOptimizations PandasOptimizations { get; set; } = PandasOptimizations.All;

		// Token: 0x1700240F RID: 9231
		// (get) Token: 0x0600D818 RID: 55320 RVA: 0x002DE2F4 File Offset: 0x002DC4F4
		// (set) Token: 0x0600D819 RID: 55321 RVA: 0x002DE2FC File Offset: 0x002DC4FC
		public string RegexLibrary { get; set; } = "regex";

		// Token: 0x17002410 RID: 9232
		// (get) Token: 0x0600D81A RID: 55322 RVA: 0x002DE305 File Offset: 0x002DC505
		// (set) Token: 0x0600D81B RID: 55323 RVA: 0x002DE30D File Offset: 0x002DC50D
		public string TransformationFunctionName { get; set; }

		// Token: 0x0600D81C RID: 55324 RVA: 0x002DE316 File Offset: 0x002DC516
		public PandasTranslationConstraint()
		{
		}

		// Token: 0x0600D81D RID: 55325 RVA: 0x002DE34C File Offset: 0x002DC54C
		public PandasTranslationConstraint(PythonTranslationConstraint pythonTranslationConstraint, string dataFrameName, int? derivedColumnIndex, string derivedColumnName, bool importPandas, PandasOptimizations pandasOptimizations, string regexLibrary, string transformationFunctionName)
		{
			base.DefinitionName = pythonTranslationConstraint.DefinitionName;
			this.DataFrameName = dataFrameName;
			this.DerivedColumnIndex = derivedColumnIndex;
			this.DerivedColumnName = derivedColumnName;
			this.ImportPandas = importPandas;
			base.IndentLevel = pythonTranslationConstraint.IndentLevel;
			base.IndentSize = pythonTranslationConstraint.IndentSize;
			base.LocalizedStrings = pythonTranslationConstraint.LocalizedStrings;
			base.MaximumExamplesInComments = pythonTranslationConstraint.MaximumExamplesInComments;
			this.PandasOptimizations = pandasOptimizations;
			base.PythonOptimizations = pythonTranslationConstraint.PythonOptimizations;
			this.RegexLibrary = regexLibrary;
			this.TransformationFunctionName = transformationFunctionName;
		}

		// Token: 0x0600D81E RID: 55326 RVA: 0x002DE40C File Offset: 0x002DC60C
		public IPandasTranslationOptions With(string definitionName = null, string dataFrameName = null, int? derivedColumnIndex = null, string derivedColumnName = null, bool? importPandas = null, uint? indentLevel = null, uint? indentSize = null, ILocalizedStrings localizedStrings = null, int? maximumExamplesInComments = null, PandasOptimizations? pandasOptimizations = null, PythonOptimizations? pythonOptimizations = null, string regexLibrary = null, string transformationFunctionName = null)
		{
			PythonTranslationConstraint pythonTranslationConstraint = base.With(definitionName, indentLevel, indentSize, localizedStrings, maximumExamplesInComments, pythonOptimizations, null);
			string text = dataFrameName ?? this.DataFrameName;
			int? num = derivedColumnIndex;
			return new PandasTranslationConstraint(pythonTranslationConstraint, text, (num != null) ? num : this.DerivedColumnIndex, derivedColumnName ?? this.DerivedColumnName, importPandas ?? this.ImportPandas, pandasOptimizations ?? this.PandasOptimizations, regexLibrary ?? this.RegexLibrary, transformationFunctionName ?? this.TransformationFunctionName);
		}

		// Token: 0x0600D81F RID: 55327 RVA: 0x002DE4B4 File Offset: 0x002DC6B4
		internal override string ToEqualString()
		{
			return string.Concat(new string[]
			{
				base.ToEqualString(),
				" DataFrameName=",
				this.DataFrameName,
				"; DerivedColumnName=",
				this.DerivedColumnName,
				"; TransformationFunctionName=",
				this.TransformationFunctionName,
				";",
				string.Format(" {0}={1};", "DerivedColumnIndex", this.DerivedColumnIndex),
				string.Format(" {0}={1};", "ImportPandas", this.ImportPandas),
				string.Format(" {0}={1};", "PandasOptimizations", this.PandasOptimizations),
				" RegexLibrary=",
				this.RegexLibrary,
				";"
			});
		}
	}
}
