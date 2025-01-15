using System;
using Microsoft.ProgramSynthesis.Rules.Concepts;

namespace Microsoft.ProgramSynthesis.Rules
{
	// Token: 0x0200038D RID: 909
	public abstract class GrammarRuleVisitor<TResult, TArgs>
	{
		// Token: 0x0600146B RID: 5227
		public abstract TResult VisitTerminalRule(TerminalRule rule, TArgs args);

		// Token: 0x0600146C RID: 5228
		public abstract TResult VisitLetRule(LetRule rule, TArgs args);

		// Token: 0x0600146D RID: 5229
		public abstract TResult VisitLambdaRule(LambdaRule rule, TArgs args);

		// Token: 0x0600146E RID: 5230
		public abstract TResult VisitConversionRule(ConversionRule rule, TArgs args);

		// Token: 0x0600146F RID: 5231
		public abstract TResult VisitBlackboxRule(BlackBoxRule rule, TArgs args);

		// Token: 0x06001470 RID: 5232
		public abstract TResult VisitConceptRule(ConceptRule rule, TArgs args);
	}
}
