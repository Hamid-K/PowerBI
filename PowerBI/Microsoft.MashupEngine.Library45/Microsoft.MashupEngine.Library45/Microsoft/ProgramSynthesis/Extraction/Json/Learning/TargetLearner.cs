using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning.PowerQueryM;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning.Python;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Wrangling.Json;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Learning
{
	// Token: 0x02000B83 RID: 2947
	public abstract class TargetLearner
	{
		// Token: 0x06004AE1 RID: 19169
		protected abstract TargetCode LearnImpl(IReadOnlyList<ParsedJson> jsons, SynthesisOptions options);

		// Token: 0x06004AE2 RID: 19170 RVA: 0x000EB4B8 File Offset: 0x000E96B8
		public static MultiTargetCode Learn(IReadOnlyList<ParsedJson> jsons, SynthesisOptions options)
		{
			TargetCode targetCode = null;
			TargetCode targetCode2 = null;
			TargetCode targetCode3 = null;
			if (options.TranslationTargets.Contains(TargetLanguage.PowerQueryM))
			{
				targetCode = new PowerQueryMLearner().LearnImpl(jsons, options);
			}
			if (options.PythonTargets.Contains(PythonTarget.Pandas))
			{
				targetCode2 = new PandasLearner().LearnImpl(jsons, options);
			}
			if (options.PythonTargets.Contains(PythonTarget.PySpark))
			{
				targetCode3 = new PySparkLearner().LearnImpl(jsons, options);
			}
			return new MultiTargetCode(targetCode, targetCode2, targetCode3);
		}

		// Token: 0x040021A8 RID: 8616
		public static string FileObjectId = "%f%";

		// Token: 0x040021A9 RID: 8617
		protected JsonErrors Errors;

		// Token: 0x040021AA RID: 8618
		protected bool IsLineJson;

		// Token: 0x040021AB RID: 8619
		protected int PrefixLength;

		// Token: 0x040021AC RID: 8620
		protected int SuffixLength;
	}
}
