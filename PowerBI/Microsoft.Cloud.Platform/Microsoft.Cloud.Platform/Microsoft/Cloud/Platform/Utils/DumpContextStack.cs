using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001E4 RID: 484
	internal sealed class DumpContextStack
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0002C074 File Offset: 0x0002A274
		public static DumpContextStack Current
		{
			get
			{
				return DumpContextStack.s_instance;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0002C07B File Offset: 0x0002A27B
		public int Count
		{
			get
			{
				return this.m_stack.Count;
			}
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0002C088 File Offset: 0x0002A288
		public void PushDumpContext(Type exceptionType, string stack, string hash, bool fatal, bool duplicate)
		{
			object sync = this.m_sync;
			lock (sync)
			{
				this.m_stack.Push(new DumpContext(exceptionType, stack, hash, fatal, duplicate));
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0002C0DC File Offset: 0x0002A2DC
		public DumpContext GetDumpContext()
		{
			object sync = this.m_sync;
			lock (sync)
			{
				if (this.m_stack.Count > 0)
				{
					return this.m_stack.Peek();
				}
			}
			return null;
		}

		// Token: 0x040004D6 RID: 1238
		private static DumpContextStack s_instance = new DumpContextStack();

		// Token: 0x040004D7 RID: 1239
		private Stack<DumpContext> m_stack = new Stack<DumpContext>();

		// Token: 0x040004D8 RID: 1240
		private object m_sync = new object();
	}
}
