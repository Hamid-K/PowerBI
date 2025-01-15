using System;
using System.Collections.Generic;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000460 RID: 1120
	internal class TransportsContext<T> : OperationContext where T : class, ITransportObject
	{
		// Token: 0x0600271F RID: 10015 RVA: 0x00077594 File Offset: 0x00075794
		public TransportsContext(AsyncCallback callback, object state, TimeSpan timeout)
			: base(callback, state, timeout)
		{
			this.m_openException = null;
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x000775A6 File Offset: 0x000757A6
		public void Open(ICollection<T> transportObjects)
		{
			this.BeginOpen(transportObjects);
			this.EndOpen(this);
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x000775B8 File Offset: 0x000757B8
		public IAsyncResult BeginOpen(ICollection<T> transportObjects)
		{
			MultiOperationContext<T> multiOperationContext = new MultiOperationContext<T>(new AsyncCallback(TransportsContext<T>.StaticOpenCallback), this, TimeSpan.MaxValue, transportObjects.Count);
			return TransportsContext<T>.BeginOpen(transportObjects, multiOperationContext);
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x000775EC File Offset: 0x000757EC
		private static IAsyncResult BeginOpen(ICollection<T> asyncTargets, MultiOperationContext<T> multiOpContext)
		{
			foreach (T t in asyncTargets)
			{
				try
				{
					IAsyncResult asyncResult = t.BeginOpen(TimeSpan.MaxValue, multiOpContext.OperationCompletionCallback, multiOpContext);
					multiOpContext.AsyncOperationBegan(t, asyncResult);
				}
				catch (Exception ex)
				{
					multiOpContext.AsyncException = ex;
					break;
				}
			}
			multiOpContext.AllAsyncOperationsBegan();
			return multiOpContext;
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x00077670 File Offset: 0x00075870
		private static void StaticOpenCallback(IAsyncResult ar)
		{
			TransportsContext<T> transportsContext = (TransportsContext<T>)ar.AsyncState;
			transportsContext.OpenCallback(ar);
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x00077690 File Offset: 0x00075890
		private void OpenCallback(IAsyncResult ar)
		{
			MultiOperationContext<T> multiOperationContext = (MultiOperationContext<T>)ar;
			List<T> asyncTargets = multiOperationContext.AsyncTargets;
			List<IAsyncResult> asyncOps = multiOperationContext.AsyncOps;
			for (int i = 0; i < asyncTargets.Count; i++)
			{
				Exception ex = null;
				try
				{
					T t = asyncTargets[i];
					t.EndOpen(asyncOps[i]);
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				multiOperationContext.AsyncOperationEnded(i, ex);
			}
			try
			{
				multiOperationContext.End();
			}
			catch (Exception ex3)
			{
				multiOperationContext.AsyncException = ex3;
			}
			if (multiOperationContext.AsyncException == null)
			{
				base.OperationCompleted(multiOperationContext.CompletedSynchronously, null);
				return;
			}
			base.CompletedSynchronously = multiOperationContext.CompletedSynchronously;
			this.m_openException = multiOperationContext.AsyncException;
			this.Cleanup(multiOperationContext);
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x0007775C File Offset: 0x0007595C
		public void EndOpen(IAsyncResult ar)
		{
			ReleaseAssert.IsTrue(this == ar);
			base.End();
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x00077770 File Offset: 0x00075970
		private void Cleanup(MultiOperationContext<T> multiOpContext)
		{
			List<T> asyncTargets = multiOpContext.AsyncTargets;
			multiOpContext.Reinitialize(new AsyncCallback(TransportsContext<T>.StaticCloseCallback), this, TimeSpan.MaxValue, asyncTargets.Count);
			TransportsContext<T>.BeginClose(asyncTargets, multiOpContext);
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000777AA File Offset: 0x000759AA
		private void CleanupCallback(MultiOperationContext<T> multiOpContext)
		{
			base.OperationCompleted(multiOpContext.CompletedSynchronously, this.m_openException);
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x000777BE File Offset: 0x000759BE
		public void Close(ICollection<T> transportObjects)
		{
			this.BeginClose(transportObjects);
			this.EndClose(this);
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x000777D0 File Offset: 0x000759D0
		public IAsyncResult BeginClose(ICollection<T> transportObjects)
		{
			MultiOperationContext<T> multiOperationContext = new MultiOperationContext<T>(new AsyncCallback(TransportsContext<T>.StaticCloseCallback), this, TimeSpan.MaxValue, transportObjects.Count);
			return TransportsContext<T>.BeginClose(transportObjects, multiOperationContext);
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x00077804 File Offset: 0x00075A04
		private static IAsyncResult BeginClose(ICollection<T> asyncTargets, MultiOperationContext<T> multiOpContext)
		{
			foreach (T t in asyncTargets)
			{
				if (t != null)
				{
					try
					{
						IAsyncResult asyncResult = t.BeginClose(TimeSpan.MaxValue, multiOpContext.OperationCompletionCallback, multiOpContext);
						multiOpContext.AsyncOperationBegan(t, asyncResult);
					}
					catch (Exception ex)
					{
						multiOpContext.AsyncException = ex;
					}
				}
			}
			multiOpContext.AllAsyncOperationsBegan();
			return multiOpContext;
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x00077890 File Offset: 0x00075A90
		private static void StaticCloseCallback(IAsyncResult ar)
		{
			TransportsContext<T> transportsContext = (TransportsContext<T>)ar.AsyncState;
			transportsContext.CloseCallback(ar);
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000778B0 File Offset: 0x00075AB0
		private void CloseCallback(IAsyncResult ar)
		{
			MultiOperationContext<T> multiOperationContext = (MultiOperationContext<T>)ar;
			List<T> asyncTargets = multiOperationContext.AsyncTargets;
			List<IAsyncResult> asyncOps = multiOperationContext.AsyncOps;
			for (int i = 0; i < asyncTargets.Count; i++)
			{
				Exception ex = null;
				try
				{
					T t = asyncTargets[i];
					t.EndClose(asyncOps[i]);
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				multiOperationContext.AsyncOperationEnded(i, ex);
			}
			if (this.m_openException == null)
			{
				base.OperationCompleted(multiOperationContext.CompletedSynchronously, null);
				return;
			}
			this.CleanupCallback(multiOperationContext);
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x0007775C File Offset: 0x0007595C
		public void EndClose(IAsyncResult ar)
		{
			ReleaseAssert.IsTrue(this == ar);
			base.End();
		}

		// Token: 0x04001728 RID: 5928
		private Exception m_openException;
	}
}
