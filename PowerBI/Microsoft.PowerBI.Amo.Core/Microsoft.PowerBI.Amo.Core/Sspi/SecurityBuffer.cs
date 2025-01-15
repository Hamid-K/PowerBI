using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Interop;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x0200010A RID: 266
	internal sealed class SecurityBuffer
	{
		// Token: 0x06000FED RID: 4077 RVA: 0x00037878 File Offset: 0x00035A78
		public SecurityBuffer(SecurityBufferType type, int size)
		{
			this.Type = type;
			this.Offset = 0;
			this.Size = size;
			this.Buffer = ((size > 0) ? new byte[size] : null);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x000378A8 File Offset: 0x00035AA8
		public SecurityBuffer(SecurityBufferType type, byte[] buffer)
		{
			this.Type = type;
			this.Offset = 0;
			this.Size = ((buffer == null) ? 0 : buffer.Length);
			this.Buffer = buffer;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x000378D4 File Offset: 0x00035AD4
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

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x00037940 File Offset: 0x00035B40
		// (set) Token: 0x06000FF1 RID: 4081 RVA: 0x00037948 File Offset: 0x00035B48
		public SecurityBufferType Type { get; private set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x00037951 File Offset: 0x00035B51
		// (set) Token: 0x06000FF3 RID: 4083 RVA: 0x00037959 File Offset: 0x00035B59
		public int Offset { get; private set; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x00037962 File Offset: 0x00035B62
		// (set) Token: 0x06000FF5 RID: 4085 RVA: 0x0003796A File Offset: 0x00035B6A
		public int Size { get; private set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00037973 File Offset: 0x00035B73
		// (set) Token: 0x06000FF7 RID: 4087 RVA: 0x0003797B File Offset: 0x00035B7B
		public byte[] Buffer { get; private set; }

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00037984 File Offset: 0x00035B84
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

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00037A10 File Offset: 0x00035C10
		public static void GetBuffers(SecBufferDesc descriptor, SecurityBuffer[] buffers)
		{
			int num = 0;
			foreach (SecBuffer secBuffer in descriptor.GetBuffers())
			{
				buffers[num] = new SecurityBuffer(secBuffer);
				num++;
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00037A68 File Offset: 0x00035C68
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

		// Token: 0x06000FFB RID: 4091 RVA: 0x00037ADC File Offset: 0x00035CDC
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

		// Token: 0x06000FFC RID: 4092 RVA: 0x00037BAC File Offset: 0x00035DAC
		public void Update(SecurityBufferType type)
		{
			this.UpdateImpl(type, 0, 0, null);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00037BB8 File Offset: 0x00035DB8
		public void Update(SecurityBufferType type, int offset, int size, byte[] buffer)
		{
			this.UpdateImpl(type, offset, size, buffer);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00037BC5 File Offset: 0x00035DC5
		public void Reset()
		{
			this.UpdateImpl(SecurityBufferType.Empty, 0, 0, null);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00037BD1 File Offset: 0x00035DD1
		private void UpdateImpl(SecurityBufferType type, int offset, int size, byte[] buffer)
		{
			this.Type = type;
			this.Offset = offset;
			this.Size = size;
			this.Buffer = buffer;
		}
	}
}
