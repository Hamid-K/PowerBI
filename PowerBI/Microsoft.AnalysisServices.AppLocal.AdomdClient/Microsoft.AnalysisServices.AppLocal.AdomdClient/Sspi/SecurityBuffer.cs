using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.AdomdClient.Interop;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000115 RID: 277
	internal sealed class SecurityBuffer
	{
		// Token: 0x06000F5F RID: 3935 RVA: 0x00034F74 File Offset: 0x00033174
		public SecurityBuffer(SecurityBufferType type, int size)
		{
			this.Type = type;
			this.Offset = 0;
			this.Size = size;
			this.Buffer = ((size > 0) ? new byte[size] : null);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x00034FA4 File Offset: 0x000331A4
		public SecurityBuffer(SecurityBufferType type, byte[] buffer)
		{
			this.Type = type;
			this.Offset = 0;
			this.Size = ((buffer == null) ? 0 : buffer.Length);
			this.Buffer = buffer;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00034FD0 File Offset: 0x000331D0
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

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0003503C File Offset: 0x0003323C
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x00035044 File Offset: 0x00033244
		public SecurityBufferType Type { get; private set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0003504D File Offset: 0x0003324D
		// (set) Token: 0x06000F65 RID: 3941 RVA: 0x00035055 File Offset: 0x00033255
		public int Offset { get; private set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0003505E File Offset: 0x0003325E
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x00035066 File Offset: 0x00033266
		public int Size { get; private set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0003506F File Offset: 0x0003326F
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x00035077 File Offset: 0x00033277
		public byte[] Buffer { get; private set; }

		// Token: 0x06000F6A RID: 3946 RVA: 0x00035080 File Offset: 0x00033280
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

		// Token: 0x06000F6B RID: 3947 RVA: 0x0003510C File Offset: 0x0003330C
		public static void GetBuffers(SecBufferDesc descriptor, SecurityBuffer[] buffers)
		{
			int num = 0;
			foreach (SecBuffer secBuffer in descriptor.GetBuffers())
			{
				buffers[num] = new SecurityBuffer(secBuffer);
				num++;
			}
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00035164 File Offset: 0x00033364
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

		// Token: 0x06000F6D RID: 3949 RVA: 0x000351D8 File Offset: 0x000333D8
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

		// Token: 0x06000F6E RID: 3950 RVA: 0x000352A8 File Offset: 0x000334A8
		public void Update(SecurityBufferType type)
		{
			this.UpdateImpl(type, 0, 0, null);
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x000352B4 File Offset: 0x000334B4
		public void Update(SecurityBufferType type, int offset, int size, byte[] buffer)
		{
			this.UpdateImpl(type, offset, size, buffer);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x000352C1 File Offset: 0x000334C1
		public void Reset()
		{
			this.UpdateImpl(SecurityBufferType.Empty, 0, 0, null);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x000352CD File Offset: 0x000334CD
		private void UpdateImpl(SecurityBufferType type, int offset, int size, byte[] buffer)
		{
			this.Type = type;
			this.Offset = offset;
			this.Size = size;
			this.Buffer = buffer;
		}
	}
}
