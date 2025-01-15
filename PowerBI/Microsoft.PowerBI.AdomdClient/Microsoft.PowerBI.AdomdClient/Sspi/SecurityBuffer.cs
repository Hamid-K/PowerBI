using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.AdomdClient.Interop;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000115 RID: 277
	internal sealed class SecurityBuffer
	{
		// Token: 0x06000F52 RID: 3922 RVA: 0x00034C44 File Offset: 0x00032E44
		public SecurityBuffer(SecurityBufferType type, int size)
		{
			this.Type = type;
			this.Offset = 0;
			this.Size = size;
			this.Buffer = ((size > 0) ? new byte[size] : null);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00034C74 File Offset: 0x00032E74
		public SecurityBuffer(SecurityBufferType type, byte[] buffer)
		{
			this.Type = type;
			this.Offset = 0;
			this.Size = ((buffer == null) ? 0 : buffer.Length);
			this.Buffer = buffer;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00034CA0 File Offset: 0x00032EA0
		private SecurityBuffer(SecBuffer buffer)
		{
			this.Type = (SecurityBufferType)buffer.BufferType;
			this.Offset = 0;
			this.Size = buffer.cbBuffer;
			if (this.Size > 0)
			{
				this.Buffer = new byte[this.Size];
				Marshal.Copy(buffer.pvBuffer, this.Buffer, 0, this.Size);
				return;
			}
			this.Buffer = null;
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x00034D0C File Offset: 0x00032F0C
		// (set) Token: 0x06000F56 RID: 3926 RVA: 0x00034D14 File Offset: 0x00032F14
		public SecurityBufferType Type { get; private set; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00034D1D File Offset: 0x00032F1D
		// (set) Token: 0x06000F58 RID: 3928 RVA: 0x00034D25 File Offset: 0x00032F25
		public int Offset { get; private set; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x00034D2E File Offset: 0x00032F2E
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x00034D36 File Offset: 0x00032F36
		public int Size { get; private set; }

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x00034D3F File Offset: 0x00032F3F
		// (set) Token: 0x06000F5C RID: 3932 RVA: 0x00034D47 File Offset: 0x00032F47
		public byte[] Buffer { get; private set; }

		// Token: 0x06000F5D RID: 3933 RVA: 0x00034D50 File Offset: 0x00032F50
		public static SecBufferDesc CreateDescriptor(SecurityBuffer[] buffers, out GCHandle[] handles)
		{
			if (buffers != null && buffers.Length != 0)
			{
				handles = new GCHandle[buffers.Length];
				for (int i = 0; i < buffers.Length; i++)
				{
					if (buffers[i] != null && buffers[i].Buffer != null)
					{
						handles[i] = GCHandle.Alloc(buffers[i].Buffer, GCHandleType.Pinned);
					}
				}
				return new SecBufferDesc(buffers.Length, buffers.Select((SecurityBuffer buffer) => new SecBuffer(buffer.Size, (int)buffer.Type, (buffer.Buffer == null) ? IntPtr.Zero : Marshal.UnsafeAddrOfPinnedArrayElement<byte>(buffer.Buffer, buffer.Offset))));
			}
			handles = null;
			return new SecBufferDesc(0, null);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00034DDC File Offset: 0x00032FDC
		public static void GetBuffers(SecBufferDesc descriptor, SecurityBuffer[] buffers)
		{
			int num = 0;
			foreach (SecBuffer secBuffer in descriptor.GetBuffers())
			{
				buffers[num] = new SecurityBuffer(secBuffer);
				num++;
			}
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00034E34 File Offset: 0x00033034
		public static void UpdateBuffers(SecBufferDesc descriptor, SecurityBuffer[] buffers)
		{
			int num = 0;
			foreach (SecBuffer secBuffer in descriptor.GetBuffers())
			{
				if ((long)secBuffer.BufferType != 0L)
				{
					buffers[num].Size = secBuffer.cbBuffer;
				}
				else
				{
					buffers[num].UpdateImpl(SecurityBufferType.Empty, 0, 0, null);
				}
				num++;
			}
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x00034EA8 File Offset: 0x000330A8
		public static void UpdateBuffersAfterDecryptInStreamMode(SecBufferDesc descriptor, SecurityBuffer[] buffers)
		{
			int offset = buffers[0].Offset;
			int size = buffers[0].Size;
			byte[] buffer = buffers[0].Buffer;
			IntPtr intPtr = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(buffer, offset);
			int num = 0;
			foreach (SecBuffer secBuffer in descriptor.GetBuffers())
			{
				int num2;
				if ((long)secBuffer.BufferType == 0L)
				{
					buffers[num].UpdateImpl(SecurityBufferType.Empty, 0, 0, null);
				}
				else if (InteropHelper.IsIncludedInBuffer(intPtr, size, secBuffer.pvBuffer, out num2))
				{
					buffers[num].UpdateImpl((SecurityBufferType)secBuffer.BufferType, offset + num2, secBuffer.cbBuffer, buffer);
				}
				else
				{
					buffers[num] = new SecurityBuffer(secBuffer);
				}
				num++;
			}
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00034F78 File Offset: 0x00033178
		public void Update(SecurityBufferType type)
		{
			this.UpdateImpl(type, 0, 0, null);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00034F84 File Offset: 0x00033184
		public void Update(SecurityBufferType type, int offset, int size, byte[] buffer)
		{
			this.UpdateImpl(type, offset, size, buffer);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00034F91 File Offset: 0x00033191
		public void Reset()
		{
			this.UpdateImpl(SecurityBufferType.Empty, 0, 0, null);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00034F9D File Offset: 0x0003319D
		private void UpdateImpl(SecurityBufferType type, int offset, int size, byte[] buffer)
		{
			this.Type = type;
			this.Offset = offset;
			this.Size = size;
			this.Buffer = buffer;
		}
	}
}
