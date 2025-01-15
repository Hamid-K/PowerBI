using System;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Internal
{
	// Token: 0x02000055 RID: 85
	internal class XEventAutoObject<XEEvent> : IDisposable
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00007884 File Offset: 0x00007884
		private unsafe void ~XEventAutoObject<XEEvent>()
		{
			<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEEvent(this.m_XEObj);
			<Module>.delete((void*)this.m_XEObj, 72UL);
			this.m_XEObj = null;
			GC.KeepAlive(this);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007844 File Offset: 0x00007844
		public unsafe XEEvent* Set(XEEvent* pObj)
		{
			XEEvent* xeobj = this.m_XEObj;
			if (xeobj != pObj)
			{
				<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEEvent(xeobj);
				<Module>.delete((void*)this.m_XEObj, 72UL);
			}
			this.m_XEObj = pObj;
			GC.KeepAlive(this);
			return pObj;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00004984 File Offset: 0x00004984
		public unsafe XEEvent* Get()
		{
			return this.m_XEObj;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00004960 File Offset: 0x00004960
		public unsafe XEEvent* PvReturn()
		{
			XEEvent* xeobj = this.m_XEObj;
			this.m_XEObj = null;
			return xeobj;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000078F4 File Offset: 0x000078F4
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~XEventAutoObject<XEEvent>();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000082A0 File Offset: 0x000082A0
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400014F RID: 335
		private unsafe XEEvent* m_XEObj = null;
	}
}
