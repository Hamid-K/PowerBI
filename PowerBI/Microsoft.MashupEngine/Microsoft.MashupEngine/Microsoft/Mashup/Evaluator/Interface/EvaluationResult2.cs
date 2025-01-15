using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E03 RID: 7683
	public struct EvaluationResult2<T>
	{
		// Token: 0x0600BDBB RID: 48571 RVA: 0x00266E7D File Offset: 0x0026507D
		public EvaluationResult2(T result)
		{
			this.result = result;
			this.exception = null;
		}

		// Token: 0x0600BDBC RID: 48572 RVA: 0x00266E8D File Offset: 0x0026508D
		public EvaluationResult2(Exception exception)
		{
			this.result = default(T);
			this.exception = exception;
		}

		// Token: 0x17002EAD RID: 11949
		// (get) Token: 0x0600BDBD RID: 48573 RVA: 0x00266EA2 File Offset: 0x002650A2
		public T Result
		{
			get
			{
				if (this.exception != null)
				{
					throw this.exception;
				}
				return this.result;
			}
		}

		// Token: 0x17002EAE RID: 11950
		// (get) Token: 0x0600BDBE RID: 48574 RVA: 0x00266EB9 File Offset: 0x002650B9
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040060CF RID: 24783
		private readonly T result;

		// Token: 0x040060D0 RID: 24784
		private readonly Exception exception;
	}
}
