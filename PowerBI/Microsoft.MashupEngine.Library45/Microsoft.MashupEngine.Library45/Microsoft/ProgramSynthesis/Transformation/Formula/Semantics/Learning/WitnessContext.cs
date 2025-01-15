using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x020016B4 RID: 5812
	public class WitnessContext<TOperatorOutput>
	{
		// Token: 0x170020E5 RID: 8421
		// (get) Token: 0x0600C1F1 RID: 49649 RVA: 0x0029CB2F File Offset: 0x0029AD2F
		// (set) Token: 0x0600C1F2 RID: 49650 RVA: 0x0029CB37 File Offset: 0x0029AD37
		public object DependentArg1 { get; set; }

		// Token: 0x170020E6 RID: 8422
		// (get) Token: 0x0600C1F3 RID: 49651 RVA: 0x0029CB40 File Offset: 0x0029AD40
		// (set) Token: 0x0600C1F4 RID: 49652 RVA: 0x0029CB48 File Offset: 0x0029AD48
		public object DependentArg2 { get; set; }

		// Token: 0x170020E7 RID: 8423
		// (get) Token: 0x0600C1F5 RID: 49653 RVA: 0x0029CB51 File Offset: 0x0029AD51
		// (set) Token: 0x0600C1F6 RID: 49654 RVA: 0x0029CB59 File Offset: 0x0029AD59
		public object DependentArg3 { get; set; }

		// Token: 0x170020E8 RID: 8424
		// (get) Token: 0x0600C1F7 RID: 49655 RVA: 0x0029CB62 File Offset: 0x0029AD62
		// (set) Token: 0x0600C1F8 RID: 49656 RVA: 0x0029CB6A File Offset: 0x0029AD6A
		public IReadOnlyList<WitnessDisjunctiveContext<TOperatorOutput>> DisjunctiveContexts { get; set; }

		// Token: 0x170020E9 RID: 8425
		// (get) Token: 0x0600C1F9 RID: 49657 RVA: 0x0029CB74 File Offset: 0x0029AD74
		public IReadOnlyList<WitnessDisjunctiveContext<TOperatorOutput>> OtherDisjunctiveContexts
		{
			get
			{
				List<WitnessDisjunctiveContext<TOperatorOutput>> list;
				if ((list = this._otherDisjunctiveContexts) == null)
				{
					list = (this._otherDisjunctiveContexts = this.DisjunctiveContexts.Where((WitnessDisjunctiveContext<TOperatorOutput> c) => !c.InputRow.Equals(this.InputRow)).ToList<WitnessDisjunctiveContext<TOperatorOutput>>());
				}
				return list;
			}
		}

		// Token: 0x170020EA RID: 8426
		// (get) Token: 0x0600C1FA RID: 49658 RVA: 0x0029CBB0 File Offset: 0x0029ADB0
		// (set) Token: 0x0600C1FB RID: 49659 RVA: 0x0029CBB8 File Offset: 0x0029ADB8
		public Example<IRow, object> Example { get; set; }

		// Token: 0x170020EB RID: 8427
		// (get) Token: 0x0600C1FC RID: 49660 RVA: 0x0029CBC1 File Offset: 0x0029ADC1
		public IRow InputRow
		{
			get
			{
				Example<IRow, object> example = this.Example;
				if (example == null)
				{
					return null;
				}
				return example.Input;
			}
		}

		// Token: 0x170020EC RID: 8428
		// (get) Token: 0x0600C1FD RID: 49661 RVA: 0x0029CBD4 File Offset: 0x0029ADD4
		// (set) Token: 0x0600C1FE RID: 49662 RVA: 0x0029CBDC File Offset: 0x0029ADDC
		public TOperatorOutput OperatorOutput { get; set; }

		// Token: 0x04004B21 RID: 19233
		private List<WitnessDisjunctiveContext<TOperatorOutput>> _otherDisjunctiveContexts;
	}
}
