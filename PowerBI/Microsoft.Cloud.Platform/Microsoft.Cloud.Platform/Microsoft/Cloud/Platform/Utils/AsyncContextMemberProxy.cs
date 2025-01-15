using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000180 RID: 384
	public class AsyncContextMemberProxy<T>
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00022695 File Offset: 0x00020895
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0002269D File Offset: 0x0002089D
		private protected T Member { protected get; private set; }

		// Token: 0x060009F2 RID: 2546 RVA: 0x000226A6 File Offset: 0x000208A6
		public AsyncContextMemberProxy(T member, int contextMemberKey)
		{
			this.Member = member;
			this.m_contextMemberKey = contextMemberKey;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000226BC File Offset: 0x000208BC
		private IDisposable PushContextMember()
		{
			return ContextManager.PushContext<T>(this.m_contextMemberKey, this.Member);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000226CF File Offset: 0x000208CF
		private void DisposeBeginAsyncScope()
		{
			AsyncContextMemberProxy<T>.s_currentProxy = this.m_previousProxy;
			this.m_beginAsyncScopeDisposable.Dispose();
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x000226E8 File Offset: 0x000208E8
		private void DisposeEndAsyncScope(bool fireVerboseEvents = true)
		{
			this.OnDisposingEndAsyncScopeStarted(fireVerboseEvents);
			T t = ContextManager.GetContextMember<T>(this.m_contextMemberKey);
			T member = this.Member;
			if (!member.Equals(t))
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Context member mismatch - Expected: {0}; Current: {1}", new object[] { this.Member, t });
				if (t == null)
				{
					this.m_capturedStack.Restore();
				}
				else
				{
					this.PushContextMember();
				}
			}
			t = ContextManager.GetContextMember<T>(this.m_contextMemberKey);
			if (!object.Equals(AsyncContextMemberProxy<T>.s_currentProxy, this.Member))
			{
				this.PopMember();
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002279C File Offset: 0x0002099C
		private void PopMember()
		{
			ContextManager.Pop<T>(this.m_contextMemberKey);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000227AC File Offset: 0x000209AC
		public IDisposable GetBeginAsyncScope(bool fireVerboseEvents = true)
		{
			this.OnGetBeginAsyncScopeStarted();
			this.m_beginAsyncScopeDisposable = this.PushContextMember();
			this.m_capturedStack = UtilsContext.Current.CaptureStack();
			this.m_previousProxy = AsyncContextMemberProxy<T>.s_currentProxy;
			AsyncContextMemberProxy<T>.s_currentProxy = this.Member;
			IDisposable disposable = new DeferredDispose(new Action(this.DisposeBeginAsyncScope));
			this.OnGetBeginAsyncScopeEnded(fireVerboseEvents);
			return disposable;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnGetBeginAsyncScopeStarted()
		{
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnGetBeginAsyncScopeEnded(bool fireVerboseEvents = true)
		{
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00022809 File Offset: 0x00020A09
		public IDisposable GetEndAsyncScope(bool fireVerboseEvents = true)
		{
			return new DeferredDispose(new Action<bool>(this.DisposeEndAsyncScope), fireVerboseEvents);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnDisposingEndAsyncScopeStarted(bool fireVerboseEvents = true)
		{
		}

		// Token: 0x040003E4 RID: 996
		[ThreadStatic]
		private static T s_currentProxy;

		// Token: 0x040003E5 RID: 997
		private int m_contextMemberKey;

		// Token: 0x040003E6 RID: 998
		private T m_previousProxy;

		// Token: 0x040003E7 RID: 999
		private IDisposable m_beginAsyncScopeDisposable;

		// Token: 0x040003E8 RID: 1000
		private IContextStack m_capturedStack;
	}
}
