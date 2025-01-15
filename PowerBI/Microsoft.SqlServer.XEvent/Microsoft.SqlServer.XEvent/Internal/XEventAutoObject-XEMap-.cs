using System;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Internal
{
	// Token: 0x02000056 RID: 86
	internal class XEventAutoObject<XEMap> : IDisposable
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00007808 File Offset: 0x00007808
		private unsafe void ~XEventAutoObject<XEMap>()
		{
			<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEMap(this.m_XEObj);
			<Module>.delete((void*)this.m_XEObj, 48UL);
			this.m_XEObj = null;
			GC.KeepAlive(this);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000077C8 File Offset: 0x000077C8
		public unsafe XEMap* Set(XEMap* pObj)
		{
			XEMap* xeobj = this.m_XEObj;
			if (xeobj != pObj)
			{
				<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEMap(xeobj);
				<Module>.delete((void*)this.m_XEObj, 48UL);
			}
			this.m_XEObj = pObj;
			GC.KeepAlive(this);
			return pObj;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00004920 File Offset: 0x00004920
		public unsafe XEMap* Get()
		{
			return this.m_XEObj;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000048FC File Offset: 0x000048FC
		public unsafe XEMap* PvReturn()
		{
			XEMap* xeobj = this.m_XEObj;
			this.m_XEObj = null;
			return xeobj;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007918 File Offset: 0x00007918
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~XEventAutoObject<XEMap>();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000082C0 File Offset: 0x000082C0
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000150 RID: 336
		private unsafe XEMap* m_XEObj = null;
	}
}
