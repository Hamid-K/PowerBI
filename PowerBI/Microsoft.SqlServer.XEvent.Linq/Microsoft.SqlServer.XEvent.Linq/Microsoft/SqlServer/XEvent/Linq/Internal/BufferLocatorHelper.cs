using System;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x0200002D RID: 45
	internal class BufferLocatorHelper
	{
		// Token: 0x0600017E RID: 382 RVA: 0x0000E248 File Offset: 0x0000E248
		internal static void SetBufferOffset(BufferLocator bufLoc, ulong byteOffset)
		{
			bufLoc.m_bufferNum = byteOffset >> 9;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000E268 File Offset: 0x0000E268
		internal static ulong GetBufferOffset(BufferLocator bufLoc)
		{
			return bufLoc.m_bufferNum << 9;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000E284 File Offset: 0x0000E284
		internal unsafe static void InitBufLocator(BufferLocator bufLoc, EventLocator eventLocation)
		{
			GCHandle gchandle = GCHandle.Alloc(eventLocation, GCHandleType.Pinned);
			XE_EventLocation* ptr = (XE_EventLocation*)gchandle.AddrOfPinnedObject().ToPointer();
			bufLoc.m_fileId = <Module>.XE_EventLocation.GetFileIndex(ptr);
			bufLoc.m_bufferNum = <Module>.XE_EventLocation.GetBufferByteOffset(ptr) >> 9;
			gchandle.Free();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000E2D4 File Offset: 0x0000E2D4
		internal static int GetEventOffset(EventLocator eventLocation)
		{
			GCHandle gchandle = GCHandle.Alloc(eventLocation, GCHandleType.Pinned);
			int num = <Module>.XE_EventLocation.GetEventOffset(gchandle.AddrOfPinnedObject().ToPointer());
			gchandle.Free();
			return num;
		}
	}
}
