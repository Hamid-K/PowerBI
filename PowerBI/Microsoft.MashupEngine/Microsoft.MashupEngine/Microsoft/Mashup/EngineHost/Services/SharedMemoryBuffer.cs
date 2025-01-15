using System;
using System.Globalization;
using System.Threading;
using Microsoft.Mashup.Shims.Interprocess;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B39 RID: 6969
	internal class SharedMemoryBuffer : IDisposable
	{
		// Token: 0x0600AE69 RID: 44649 RVA: 0x0023B464 File Offset: 0x00239664
		public SharedMemoryBuffer(Guid identity, bool owner = false)
		{
			string text = SharedMemoryBuffer.CreateSharedMemoryName(identity);
			string text2 = string.Format(CultureInfo.InvariantCulture, "buffer_{0}.mutex", identity);
			this.fileMappingMutex = MutexFactory.Create(false, text2);
			if (owner)
			{
				this.sharedMemory = SharedMemory.Create(4096L, text);
			}
			else
			{
				this.sharedMemory = SharedMemory.Open(text);
				this.sharedMemory.Unlink();
			}
			this.view = this.sharedMemory.MapView(0UL, 4096U);
		}

		// Token: 0x0600AE6A RID: 44650 RVA: 0x0023B4F4 File Offset: 0x002396F4
		public void Dispose()
		{
			if (this.view != null)
			{
				this.view.Dispose();
				this.view = null;
			}
			if (this.sharedMemory != null)
			{
				this.sharedMemory.Unlink();
				this.sharedMemory.Dispose();
				this.sharedMemory = null;
			}
			if (this.fileMappingMutex != null)
			{
				this.fileMappingMutex.Close();
				this.fileMappingMutex = null;
			}
		}

		// Token: 0x0600AE6B RID: 44651 RVA: 0x0023B55C File Offset: 0x0023975C
		public bool TryRead(out byte[] buffer)
		{
			bool flag;
			try
			{
				buffer = this.Read();
				flag = true;
			}
			catch (AbandonedMutexException)
			{
				buffer = null;
				this.fileMappingMutex.ReleaseMutex();
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600AE6C RID: 44652 RVA: 0x0023B59C File Offset: 0x0023979C
		private byte[] Read()
		{
			this.fileMappingMutex.WaitOne();
			byte[] array2;
			try
			{
				this.view.Read(0, this.headerBuffer.Length, this.headerBuffer, 0);
				int num = BitConverter.ToInt32(this.headerBuffer, 0);
				if (num < 0 || (long)num >= 4092L)
				{
					num = 0;
				}
				byte[] array = new byte[num];
				this.view.Read(this.headerBuffer.Length, num, array, 0);
				array2 = array;
			}
			finally
			{
				this.fileMappingMutex.ReleaseMutex();
			}
			return array2;
		}

		// Token: 0x0600AE6D RID: 44653 RVA: 0x0023B62C File Offset: 0x0023982C
		public void Write(byte[] buffer)
		{
			this.fileMappingMutex.WaitOne();
			try
			{
				int num = buffer.Length;
				if ((long)num >= 4092L)
				{
					num = 0;
				}
				this.view.Write(0, this.headerBuffer.Length, BitConverter.GetBytes(num), 0);
				this.view.Write(this.headerBuffer.Length, num, buffer, 0);
			}
			finally
			{
				this.fileMappingMutex.ReleaseMutex();
			}
		}

		// Token: 0x0600AE6E RID: 44654 RVA: 0x0023B6A4 File Offset: 0x002398A4
		private static string CreateSharedMemoryName(Guid identity)
		{
			return string.Format(CultureInfo.InvariantCulture, "buffer_{0}.bin", identity);
		}

		// Token: 0x040059F1 RID: 23025
		private const uint fileMappingMaxSize = 4096U;

		// Token: 0x040059F2 RID: 23026
		private const uint fileMappingHeaderSize = 4U;

		// Token: 0x040059F3 RID: 23027
		private const uint fileMappingMaxContentSize = 4092U;

		// Token: 0x040059F4 RID: 23028
		private readonly byte[] headerBuffer = new byte[4];

		// Token: 0x040059F5 RID: 23029
		private SharedMemory sharedMemory;

		// Token: 0x040059F6 RID: 23030
		private MemoryMappedView view;

		// Token: 0x040059F7 RID: 23031
		private Mutex fileMappingMutex;
	}
}
