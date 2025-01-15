using System;
using System.Threading;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000163 RID: 355
	internal sealed class DisposableObjectPool<T> where T : class, IDisposable
	{
		// Token: 0x06001062 RID: 4194 RVA: 0x0003FD33 File Offset: 0x0003DF33
		internal DisposableObjectPool(Func<T> factory)
			: this(factory, Environment.ProcessorCount * 2)
		{
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0003FD43 File Offset: 0x0003DF43
		internal DisposableObjectPool(Func<T> factory, int size)
		{
			this._factory = factory;
			this.Items = new DisposableObjectPool<T>.Element[size];
			this.Size = size;
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0003FD65 File Offset: 0x0003DF65
		internal DisposableObjectPool<T>.Element[] Items { get; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0003FD6D File Offset: 0x0003DF6D
		internal int Size { get; }

		// Token: 0x06001066 RID: 4198 RVA: 0x0003FD75 File Offset: 0x0003DF75
		private T CreateInstance()
		{
			return this._factory();
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0003FD84 File Offset: 0x0003DF84
		internal T Allocate()
		{
			DisposableObjectPool<T>.Element[] items = this.Items;
			for (int i = 0; i < items.Length; i++)
			{
				T value = items[i].Value;
				if (value != null && value == Interlocked.CompareExchange<T>(ref items[i].Value, default(T), value))
				{
					return value;
				}
			}
			return this.CreateInstance();
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0003FDEC File Offset: 0x0003DFEC
		internal void Free(T obj)
		{
			DisposableObjectPool<T>.Element[] items = this.Items;
			bool flag = false;
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i].Value == null && Interlocked.CompareExchange<T>(ref items[i].Value, obj, default(T)) == null)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				obj.Dispose();
			}
		}

		// Token: 0x0400060C RID: 1548
		private readonly Func<T> _factory;

		// Token: 0x02000278 RID: 632
		internal struct Element
		{
			// Token: 0x04000B42 RID: 2882
			internal T Value;
		}
	}
}
