using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001CA RID: 458
	public abstract class Context
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x00028B07 File Offset: 0x00026D07
		protected IDisposable PushContextMember<T>(int key, T contextMember)
		{
			if (contextMember == null)
			{
				throw new InvalidOperationException("Context member cannot be null");
			}
			return ContextManager.PushContext<T>(key, contextMember);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00028B23 File Offset: 0x00026D23
		protected T GetContextMember<T>(int key)
		{
			return ContextManager.GetContextMember<T>(key);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00028B2B File Offset: 0x00026D2B
		protected T PopContextMember<T>(int key)
		{
			return ContextManager.Pop<T>(key);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00028B33 File Offset: 0x00026D33
		protected IEnumerable<T> GetContextMemberStack<T>(int key)
		{
			return ContextManager.GetContextMemberStack<T>(key);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00028B3B File Offset: 0x00026D3B
		protected IContextStack CaptureStack()
		{
			return ContextManager.CaptureStack();
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00028B42 File Offset: 0x00026D42
		public void ClearStack()
		{
			ContextManager.ClearStack();
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00028B49 File Offset: 0x00026D49
		public bool IsCurrentSameAs(IContextStack contextStack)
		{
			return this.CaptureStack().Equals(contextStack);
		}
	}
}
