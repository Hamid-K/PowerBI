using System;
using System.Threading;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies
{
	// Token: 0x02000719 RID: 1817
	public abstract class SynthesisStrategy<TSpec> : ISynthesisStrategy where TSpec : Spec
	{
		// Token: 0x0600275E RID: 10078 RVA: 0x0006FA2F File Offset: 0x0006DC2F
		protected SynthesisStrategy(params StrategyAttribute[] attributes)
		{
			this.Attributes = attributes;
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x0006FA3E File Offset: 0x0006DC3E
		public Type SpecType
		{
			get
			{
				return typeof(TSpec);
			}
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public virtual void Initialize(SynthesisEngine engine)
		{
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002761 RID: 10081 RVA: 0x0006FA4A File Offset: 0x0006DC4A
		public StrategyAttribute[] Attributes { get; }

		// Token: 0x06002762 RID: 10082 RVA: 0x0006FA52 File Offset: 0x0006DC52
		public Optional<ProgramSet> Learn(SynthesisEngine engine, LearningTask task, CancellationToken cancel)
		{
			if (!((ISynthesisStrategy)this).CanCall(task.Spec))
			{
				throw new ArgumentException("The spec should be derived from the supported base spec type.");
			}
			return this.Learn(engine, task.Cast<TSpec>(), cancel);
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x0006FA7B File Offset: 0x0006DC7B
		bool ISynthesisStrategy.CanCall(Spec spec)
		{
			return this.SpecType.IsInstanceOfType(spec) && this.CanCall((TSpec)((object)spec));
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x0000A5FD File Offset: 0x000087FD
		public virtual bool CanCall(TSpec spec)
		{
			return true;
		}

		// Token: 0x06002765 RID: 10085
		public abstract Optional<ProgramSet> Learn(SynthesisEngine engine, LearningTask<TSpec> task, CancellationToken cancel);
	}
}
