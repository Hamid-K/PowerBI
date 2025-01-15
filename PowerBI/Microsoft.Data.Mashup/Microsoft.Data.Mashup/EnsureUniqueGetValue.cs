using System;
using Microsoft.Data.Mashup.ProviderCommon;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200001E RID: 30
	internal sealed class EnsureUniqueGetValue<T> where T : class
	{
		// Token: 0x06000122 RID: 290 RVA: 0x000067D4 File Offset: 0x000049D4
		public EnsureUniqueGetValue(Func<T> getValue)
		{
			this.getValue = getValue;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000067E4 File Offset: 0x000049E4
		public T GetValue()
		{
			T t = this.getValue();
			if (this.lastValue == t)
			{
				throw new MashupException(ProviderErrorStrings.ParameterEnumeratedMultipleTimes(typeof(T).Name));
			}
			this.lastValue = t;
			return t;
		}

		// Token: 0x040000A6 RID: 166
		private readonly Func<T> getValue;

		// Token: 0x040000A7 RID: 167
		private T lastValue;
	}
}
