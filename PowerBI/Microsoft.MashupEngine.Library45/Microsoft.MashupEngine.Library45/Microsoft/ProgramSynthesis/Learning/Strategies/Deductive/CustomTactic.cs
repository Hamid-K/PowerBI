using System;
using System.Reflection;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive
{
	// Token: 0x0200071F RID: 1823
	internal class CustomTactic : ITactic
	{
		// Token: 0x06002772 RID: 10098 RVA: 0x0006FBBC File Offset: 0x0006DDBC
		public CustomTactic(MethodInfo method, DomainLearningLogic domainLearningLogic)
		{
			this._delegate = MethodReference.CreateWithParams<CustomTactic.Delegate>(method, !method.IsStatic);
			this._domainLogic = domainLearningLogic;
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x0006FBE0 File Offset: 0x0006DDE0
		public Optional<ProgramSet> LearnAlternative(IAlternatingLanguage language, Func<ILanguage, ProgramSet> learner)
		{
			return this._delegate.Invoke(this._domainLogic, language, new Func<ILanguage, ProgramSet>[] { learner });
		}

		// Token: 0x0400133C RID: 4924
		private readonly MethodReference<CustomTactic.Delegate> _delegate;

		// Token: 0x0400133D RID: 4925
		private readonly DomainLearningLogic _domainLogic;

		// Token: 0x02000720 RID: 1824
		// (Invoke) Token: 0x06002775 RID: 10101
		public delegate Optional<ProgramSet> Delegate(DomainLearningLogic domainLearning, IAlternatingLanguage language, Func<ILanguage, ProgramSet>[] learner);
	}
}
