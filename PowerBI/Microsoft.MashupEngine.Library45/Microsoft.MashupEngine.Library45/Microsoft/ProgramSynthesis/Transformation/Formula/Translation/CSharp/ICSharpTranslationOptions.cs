using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001981 RID: 6529
	public interface ICSharpTranslationOptions : ITranslationOptions
	{
		// Token: 0x17002364 RID: 9060
		// (get) Token: 0x0600D5B7 RID: 54711
		string ClassName { get; }

		// Token: 0x17002365 RID: 9061
		// (get) Token: 0x0600D5B8 RID: 54712
		string MethodName { get; }

		// Token: 0x17002366 RID: 9062
		// (get) Token: 0x0600D5B9 RID: 54713
		string NamespaceName { get; }

		// Token: 0x17002367 RID: 9063
		// (get) Token: 0x0600D5BA RID: 54714
		CSharpOptimizations Optimizations { get; }

		// Token: 0x17002368 RID: 9064
		// (get) Token: 0x0600D5BB RID: 54715
		CSharpCodeStyle Style { get; }
	}
}
