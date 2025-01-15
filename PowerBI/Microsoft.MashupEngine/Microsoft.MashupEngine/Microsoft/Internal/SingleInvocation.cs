using System;
using Microsoft.Mashup.Engine1;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Internal
{
	// Token: 0x020001CA RID: 458
	internal class SingleInvocation<T>
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x000118E4 File Offset: 0x0000FAE4
		public SingleInvocation(Func<T> function)
		{
			this.function = function;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000118F3 File Offset: 0x0000FAF3
		public T Invoke()
		{
			if (!this.invoked)
			{
				this.invoked = true;
				return this.function();
			}
			throw ValueException.NewExpressionError<Message0>(Strings.SingleInvocation_MultipleEvaluations, null, null);
		}

		// Token: 0x04000521 RID: 1313
		private readonly Func<T> function;

		// Token: 0x04000522 RID: 1314
		private bool invoked;
	}
}
