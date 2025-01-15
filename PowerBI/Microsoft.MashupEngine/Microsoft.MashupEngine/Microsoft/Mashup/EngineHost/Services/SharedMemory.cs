using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B38 RID: 6968
	internal class SharedMemory : MemoryMappedFile
	{
		// Token: 0x0600AE63 RID: 44643 RVA: 0x0023B420 File Offset: 0x00239620
		private SharedMemory(SafeHandle sharedMemoryHandle)
			: base(null, sharedMemoryHandle, null)
		{
		}

		// Token: 0x0600AE64 RID: 44644 RVA: 0x0023B42B File Offset: 0x0023962B
		public static SharedMemory Create(long length, string name)
		{
			return new SharedMemory(SharedMemory.CreateSharedMemory(length, name));
		}

		// Token: 0x0600AE65 RID: 44645 RVA: 0x0023B439 File Offset: 0x00239639
		public static SharedMemory Open(string name)
		{
			return new SharedMemory(SharedMemory.OpenSharedMemory(name));
		}

		// Token: 0x0600AE66 RID: 44646 RVA: 0x0000336E File Offset: 0x0000156E
		public void Unlink()
		{
		}

		// Token: 0x0600AE67 RID: 44647 RVA: 0x0023B446 File Offset: 0x00239646
		private static SafeHandle CreateSharedMemory(long length, string name)
		{
			return MemoryMappedFile.CreateFileMapping(new SafeFileHandle(MemoryMappedFile.Handle.INVALID_HANDLE_VALUE, true), checked((ulong)length), name);
		}

		// Token: 0x0600AE68 RID: 44648 RVA: 0x0023B45B File Offset: 0x0023965B
		private static SafeHandle OpenSharedMemory(string name)
		{
			return MemoryMappedFile.OpenFileMapping(name);
		}
	}
}
