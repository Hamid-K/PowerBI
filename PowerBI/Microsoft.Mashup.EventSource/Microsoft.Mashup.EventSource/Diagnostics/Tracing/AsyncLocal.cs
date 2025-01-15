using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200000F RID: 15
	internal sealed class AsyncLocal<T>
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000027C6 File Offset: 0x000009C6
		public AsyncLocal()
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027CE File Offset: 0x000009CE
		public AsyncLocal(Action<AsyncLocalValueChangedArgs<T>> valueChangedHandler)
		{
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000027D8 File Offset: 0x000009D8
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000027FA File Offset: 0x000009FA
		public T Value
		{
			get
			{
				object obj = null;
				if (obj != null)
				{
					return (T)((object)obj);
				}
				return default(T);
			}
			set
			{
			}
		}
	}
}
