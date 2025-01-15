using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019E1 RID: 6625
	public class PythonTranslationConstraint : TranslationConstraint, IPythonTranslationOptions, ITranslationOptions, IRenderingOptions, IUniqueConstraint<PythonTranslationConstraint>
	{
		// Token: 0x1700241B RID: 9243
		// (get) Token: 0x0600D841 RID: 55361 RVA: 0x002DE866 File Offset: 0x002DCA66
		// (set) Token: 0x0600D842 RID: 55362 RVA: 0x002DE86E File Offset: 0x002DCA6E
		public string DefinitionName { get; set; } = "formula";

		// Token: 0x1700241C RID: 9244
		// (get) Token: 0x0600D843 RID: 55363 RVA: 0x002DE877 File Offset: 0x002DCA77
		// (set) Token: 0x0600D844 RID: 55364 RVA: 0x002DE87F File Offset: 0x002DCA7F
		public uint IndentLevel { get; set; }

		// Token: 0x1700241D RID: 9245
		// (get) Token: 0x0600D845 RID: 55365 RVA: 0x002DE888 File Offset: 0x002DCA88
		// (set) Token: 0x0600D846 RID: 55366 RVA: 0x002DE890 File Offset: 0x002DCA90
		public uint IndentSize { get; set; } = 4U;

		// Token: 0x1700241E RID: 9246
		// (get) Token: 0x0600D847 RID: 55367 RVA: 0x002DE899 File Offset: 0x002DCA99
		// (set) Token: 0x0600D848 RID: 55368 RVA: 0x002DE8A1 File Offset: 0x002DCAA1
		public int MaximumExamplesInComments { get; set; } = 10;

		// Token: 0x1700241F RID: 9247
		// (get) Token: 0x0600D849 RID: 55369 RVA: 0x002DE8AA File Offset: 0x002DCAAA
		// (set) Token: 0x0600D84A RID: 55370 RVA: 0x002DE8B2 File Offset: 0x002DCAB2
		public ILocalizedStrings LocalizedStrings { get; set; } = new DefaultLocalizedStrings();

		// Token: 0x17002420 RID: 9248
		// (get) Token: 0x0600D84B RID: 55371 RVA: 0x002DE8BB File Offset: 0x002DCABB
		// (set) Token: 0x0600D84C RID: 55372 RVA: 0x002DE8C3 File Offset: 0x002DCAC3
		public bool UseNumpy { get; set; } = true;

		// Token: 0x17002421 RID: 9249
		// (get) Token: 0x0600D84D RID: 55373 RVA: 0x002DE8CC File Offset: 0x002DCACC
		// (set) Token: 0x0600D84E RID: 55374 RVA: 0x002DE8D4 File Offset: 0x002DCAD4
		public PythonOptimizations PythonOptimizations { get; set; } = PythonOptimizations.Default;

		// Token: 0x0600D84F RID: 55375 RVA: 0x002DE8E0 File Offset: 0x002DCAE0
		public PythonTranslationConstraint With(string definitionName = null, uint? indentLevel = null, uint? indentSize = null, ILocalizedStrings localizedStrings = null, int? maximumExamplesInComments = null, PythonOptimizations? pythonOptimizations = null, bool? useNumpy = null)
		{
			return new PythonTranslationConstraint
			{
				IndentLevel = (indentLevel ?? this.IndentLevel),
				IndentSize = (indentSize ?? this.IndentSize),
				DefinitionName = (definitionName ?? this.DefinitionName),
				MaximumExamplesInComments = (maximumExamplesInComments ?? this.MaximumExamplesInComments),
				LocalizedStrings = (localizedStrings ?? this.LocalizedStrings),
				UseNumpy = (useNumpy ?? this.UseNumpy),
				PythonOptimizations = (pythonOptimizations ?? this.PythonOptimizations)
			};
		}

		// Token: 0x0600D850 RID: 55376 RVA: 0x002DE9B8 File Offset: 0x002DCBB8
		internal override string ToEqualString()
		{
			return string.Concat(new string[]
			{
				base.ToEqualString(),
				" DefinitionName=",
				this.DefinitionName,
				";",
				string.Format(" {0}={1};", "PythonOptimizations", this.PythonOptimizations),
				string.Format(" {0}={1};", "IndentLevel", this.IndentLevel),
				string.Format(" {0}={1};", "IndentSize", this.IndentSize),
				string.Format(" {0}={1};", "MaximumExamplesInComments", this.MaximumExamplesInComments),
				string.Format(" {0}={1};", "LocalizedStrings", this.LocalizedStrings)
			});
		}
	}
}
