using System;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Internal
{
	// Token: 0x02000057 RID: 87
	internal class XEventAutoObject<XETarget> : IDisposable
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x0000778C File Offset: 0x0000778C
		private unsafe void ~XEventAutoObject<XETarget>()
		{
			<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXETarget(this.m_XEObj);
			<Module>.delete((void*)this.m_XEObj, 56UL);
			this.m_XEObj = null;
			GC.KeepAlive(this);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000774C File Offset: 0x0000774C
		public unsafe XETarget* Set(XETarget* pObj)
		{
			XETarget* xeobj = this.m_XEObj;
			if (xeobj != pObj)
			{
				<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXETarget(xeobj);
				<Module>.delete((void*)this.m_XEObj, 56UL);
			}
			this.m_XEObj = pObj;
			GC.KeepAlive(this);
			return pObj;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000048BC File Offset: 0x000048BC
		public unsafe XETarget* Get()
		{
			return this.m_XEObj;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00004898 File Offset: 0x00004898
		public unsafe XETarget* PvReturn()
		{
			XETarget* xeobj = this.m_XEObj;
			this.m_XEObj = null;
			return xeobj;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000793C File Offset: 0x0000793C
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~XEventAutoObject<XETarget>();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008F0C File Offset: 0x00008F0C
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000151 RID: 337
		private unsafe XETarget* m_XEObj = null;
	}
}
