using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DC8 RID: 7624
	public class CompositeEvaluation : IEvaluation
	{
		// Token: 0x0600BCE4 RID: 48356 RVA: 0x002656B4 File Offset: 0x002638B4
		public void Add(IEvaluation evaluation)
		{
			List<IEvaluation> list = this.evaluations;
			bool flag2;
			lock (list)
			{
				flag2 = this.cancelled;
				this.evaluations.Add(evaluation);
			}
			if (flag2)
			{
				this.Cancel();
			}
		}

		// Token: 0x0600BCE5 RID: 48357 RVA: 0x0026570C File Offset: 0x0026390C
		public void Cancel()
		{
			List<IEvaluation> list = this.evaluations;
			IEvaluation[] array;
			lock (list)
			{
				this.cancelled = true;
				array = this.evaluations.ToArray();
				this.evaluations.Clear();
			}
			IEvaluation[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Cancel();
			}
		}

		// Token: 0x04006063 RID: 24675
		private readonly List<IEvaluation> evaluations = new List<IEvaluation>();

		// Token: 0x04006064 RID: 24676
		private bool cancelled;
	}
}
