using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.SignificantInputs
{
	// Token: 0x020000FE RID: 254
	public class Distinguisher<TInput> : Tuple<uint, Func<TInput, uint?>, bool>
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x000132AB File Offset: 0x000114AB
		public Distinguisher(uint numChoices, Func<TInput, uint?> choiceFunc, bool significantEvenIfCorrect = false)
			: base(numChoices, choiceFunc, significantEvenIfCorrect)
		{
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x000132B6 File Offset: 0x000114B6
		private uint NumChoices
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x000132BE File Offset: 0x000114BE
		private Func<TInput, uint?> ChoiceFunc
		{
			get
			{
				return base.Item2;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000132C6 File Offset: 0x000114C6
		internal bool SignificantEvenIfCorrect
		{
			get
			{
				return base.Item3;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000132CE File Offset: 0x000114CE
		internal uint? ChoiceFor(TInput input)
		{
			return this.ChoiceFunc(input);
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x000132DC File Offset: 0x000114DC
		internal IEnumerable<int> Choices
		{
			get
			{
				return Enumerable.Range(-1, (int)(this.NumChoices + 1U));
			}
		}
	}
}
