using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200024C RID: 588
	internal class WorkspaceManager<T> where T : IDisposable
	{
		// Token: 0x0600158B RID: 5515 RVA: 0x00055160 File Offset: 0x00053360
		public void Push(T item)
		{
			Stack<T> items = this.m_items;
			lock (items)
			{
				this.m_items.Push(item);
			}
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x000551A8 File Offset: 0x000533A8
		public void Pop()
		{
			Stack<T> items = this.m_items;
			lock (items)
			{
				if (this.IsFrozen(this.CurrentLevel))
				{
					this.m_suspend.Count++;
				}
				else
				{
					T t = this.m_items.Pop();
					t.Dispose();
				}
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00055220 File Offset: 0x00053420
		public T Peek()
		{
			Stack<T> items = this.m_items;
			T t;
			lock (items)
			{
				t = this.m_items.Peek();
			}
			return t;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00055268 File Offset: 0x00053468
		public void SuspendCleanup()
		{
			Stack<T> items = this.m_items;
			lock (items)
			{
				if (this.m_suspend != null)
				{
					throw new InternalCatalogException("Multiple Suspend() levels is currently not supported");
				}
				this.m_suspend = new WorkspaceManager<T>.SuspendState(this.CurrentLevel);
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x000552C8 File Offset: 0x000534C8
		public void ResumeCleanup()
		{
			Stack<T> items = this.m_items;
			lock (items)
			{
				RSTrace.CatalogTrace.Assert(this.m_suspend != null, "m_suspend");
				RSTrace.CatalogTrace.Assert(this.m_suspend.Level == this.CurrentLevel, "m_suspend.Level == CurrentLevel");
				for (int i = 0; i < this.m_suspend.Count; i++)
				{
					T t = this.m_items.Pop();
					t.Dispose();
				}
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0005536C File Offset: 0x0005356C
		public int Count
		{
			get
			{
				Stack<T> items = this.m_items;
				int count;
				lock (items)
				{
					count = this.m_items.Count;
				}
				return count;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x000553B4 File Offset: 0x000535B4
		private int CurrentLevel
		{
			get
			{
				return this.m_items.Count;
			}
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x000553C1 File Offset: 0x000535C1
		private bool IsFrozen(int level)
		{
			return this.m_suspend != null && this.m_suspend.Level == level;
		}

		// Token: 0x040007D5 RID: 2005
		private readonly Stack<T> m_items = new Stack<T>();

		// Token: 0x040007D6 RID: 2006
		private WorkspaceManager<T>.SuspendState m_suspend;

		// Token: 0x020004B5 RID: 1205
		private sealed class SuspendState
		{
			// Token: 0x06002414 RID: 9236 RVA: 0x000859DC File Offset: 0x00083BDC
			public SuspendState(int level)
			{
				this.Level = level;
				this.Count = 0;
			}

			// Token: 0x040010CF RID: 4303
			public readonly int Level;

			// Token: 0x040010D0 RID: 4304
			public int Count;
		}
	}
}
