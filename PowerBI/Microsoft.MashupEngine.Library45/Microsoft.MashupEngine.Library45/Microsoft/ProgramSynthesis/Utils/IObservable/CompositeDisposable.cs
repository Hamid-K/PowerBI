using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x02000647 RID: 1607
	public class CompositeDisposable : ICancelable, IDisposable
	{
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x00062AA4 File Offset: 0x00060CA4
		// (set) Token: 0x060022F5 RID: 8949 RVA: 0x00062AAC File Offset: 0x00060CAC
		public bool IsDisposed { get; private set; }

		// Token: 0x060022F6 RID: 8950 RVA: 0x00062AB5 File Offset: 0x00060CB5
		public CompositeDisposable()
		{
			this._disposables = new List<IDisposable>();
			this.IsDisposed = false;
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x00062AD0 File Offset: 0x00060CD0
		public void Add(IDisposable item)
		{
			List<IDisposable> disposables = this._disposables;
			lock (disposables)
			{
				if (!this.IsDisposed)
				{
					this._disposables.Add(item);
					return;
				}
			}
			item.Dispose();
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x00062B28 File Offset: 0x00060D28
		public void Remove(IDisposable item)
		{
			List<IDisposable> disposables = this._disposables;
			lock (disposables)
			{
				if (!this.IsDisposed)
				{
					this._disposables.Remove(item);
				}
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x00062B78 File Offset: 0x00060D78
		public int Count
		{
			get
			{
				List<IDisposable> disposables = this._disposables;
				int count;
				lock (disposables)
				{
					count = this._disposables.Count;
				}
				return count;
			}
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x00062BC0 File Offset: 0x00060DC0
		public void Dispose()
		{
			List<IDisposable> disposables = this._disposables;
			lock (disposables)
			{
				if (!this.IsDisposed)
				{
					foreach (IDisposable disposable in this._disposables)
					{
						disposable.Dispose();
					}
					this.IsDisposed = true;
				}
			}
		}

		// Token: 0x0400108B RID: 4235
		private readonly List<IDisposable> _disposables;
	}
}
