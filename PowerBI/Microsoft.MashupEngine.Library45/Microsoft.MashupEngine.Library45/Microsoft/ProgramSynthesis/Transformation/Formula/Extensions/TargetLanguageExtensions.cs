using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Extensions
{
	// Token: 0x020019BE RID: 6590
	public static class TargetLanguageExtensions
	{
		// Token: 0x0600D736 RID: 55094 RVA: 0x002DB2B8 File Offset: 0x002D94B8
		public static LearnConstraint ToDefaultLearnConstraint(this TargetLanguage? target)
		{
			if (target == null)
			{
				return new LearnConstraint();
			}
			if (target != null)
			{
				LearnConstraint learnConstraint;
				switch (target.GetValueOrDefault())
				{
				case TargetLanguage.Python:
					learnConstraint = new PythonLearnConstraint();
					break;
				case TargetLanguage.Pandas:
					learnConstraint = new PandasLearnConstraint();
					break;
				case TargetLanguage.PySpark:
					learnConstraint = new PySparkLearnConstraint();
					break;
				case TargetLanguage.R:
				case TargetLanguage.Java:
					goto IL_009F;
				case TargetLanguage.PowerQueryM:
					learnConstraint = new PowerQueryLearnConstraint();
					break;
				case TargetLanguage.PowerApps:
					learnConstraint = new PowerFxLearnConstraint();
					break;
				case TargetLanguage.Excel:
					learnConstraint = new ExcelLearnConstraint();
					break;
				case TargetLanguage.Sql:
					learnConstraint = new PySparkLearnConstraint();
					break;
				case TargetLanguage.PowerAutomate:
					learnConstraint = new PowerAutomateLearnConstraint();
					break;
				case TargetLanguage.CSharp:
					learnConstraint = new CSharpLearnConstraint();
					break;
				default:
					goto IL_009F;
				}
				return learnConstraint;
			}
			IL_009F:
			throw new Exception(string.Format("Unable to determine default LearnConstraint: {0}", target));
		}

		// Token: 0x0600D737 RID: 55095 RVA: 0x002DB37C File Offset: 0x002D957C
		public static TranslationConstraint ToDefaultTranslationConstraint(this TargetLanguage target)
		{
			switch (target)
			{
			case TargetLanguage.Python:
				return new PythonTranslationConstraint();
			case TargetLanguage.Pandas:
				return new PandasTranslationConstraint();
			case TargetLanguage.PySpark:
				return new PySparkTranslationConstraint();
			case TargetLanguage.PowerQueryM:
				return new PowerQueryTranslationConstraint();
			case TargetLanguage.PowerApps:
				return new PowerFxTranslationConstraint();
			case TargetLanguage.Excel:
				return new ExcelTranslationConstraint();
			case TargetLanguage.Sql:
				return new PySparkTranslationConstraint();
			case TargetLanguage.PowerAutomate:
				return new PowerAutomateTranslationConstraint();
			case TargetLanguage.CSharp:
				return new CSharpTranslationConstraint();
			}
			throw new Exception(string.Format("Unable to determine default TranslationConstraint: {0}", target));
		}
	}
}
