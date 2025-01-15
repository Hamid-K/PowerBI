using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using Microsoft.AnalysisServices.Sspi;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000050 RID: 80
	internal abstract class TcpSecureStream : TcpStream
	{
		// Token: 0x06000378 RID: 888 RVA: 0x00013960 File Offset: 0x00011B60
		private protected TcpSecureStream(TcpStream tcpStream, SecurityContext securityContext)
			: base(tcpStream)
		{
			if (securityContext == null)
			{
				throw new ArgumentNullException("securityContext");
			}
			this.securityContext = securityContext;
			try
			{
				SecurityMode mode = securityContext.Mode;
				if (mode != SecurityMode.Block)
				{
					if (mode != SecurityMode.Stream)
					{
						throw new XmlaStreamException("SecurityContextMode " + securityContext.Mode.ToString() + " not configured!");
					}
					int num;
					int num2;
					int num3;
					int num4;
					securityContext.QueryContextStreamSizes(out num, out num2, out this.maxStreamMessageLength, out num3, out num4);
					if (this.maxStreamMessageLength > 65535)
					{
						throw new XmlaStreamException(XmlaSR.TcpStream_MaxSignatureExceedsProtocolLimit);
					}
					this.streamHeaderForWrite = new ArraySegment<byte>(new byte[num]);
					this.streamTrailerForWrite = new ArraySegment<byte>(new byte[num2]);
					this.streamEncryptedDataAccumulatorForRead = new List<ArraySegment<byte>>();
					this.streamEncryptedDataAccumulatorForReadFreeBuffers = new List<ArraySegment<byte>>();
					this.streamDecryptedDataForRead = new List<ArraySegment<byte>>();
					this.securityBuffers = new SecurityBuffer[]
					{
						new SecurityBuffer(SecurityBufferType.StreamHeader, this.streamHeaderForWrite.Array),
						new SecurityBuffer(SecurityBufferType.Data, null),
						new SecurityBuffer(SecurityBufferType.StreamTrailer, this.streamTrailerForWrite.Array),
						new SecurityBuffer(SecurityBufferType.Empty, null)
					};
				}
				else
				{
					int num3;
					int num5;
					int num6;
					int num7;
					securityContext.QueryContextSizes(out num5, out num6, out num3, out num7);
					this.maxTokenSize = Math.Max(num7, num6);
					this.maxEncryptionBufferSize = Math.Min(num5, 65535);
					if (this.maxTokenSize > 65535)
					{
						throw new XmlaStreamException(XmlaSR.TcpStream_MaxSignatureExceedsProtocolLimit);
					}
					this.tokenBufferForWrite = new byte[this.maxTokenSize];
					this.tokenBufferForRead = new byte[this.maxTokenSize];
					this.securityBuffers = new SecurityBuffer[]
					{
						new SecurityBuffer(SecurityBufferType.Data, null),
						new SecurityBuffer(SecurityBufferType.Token, this.tokenBufferForWrite)
					};
				}
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
			catch (SocketException ex2)
			{
				throw new XmlaStreamException(ex2);
			}
			catch (Win32Exception ex3)
			{
				throw new XmlaStreamException(ex3);
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00013BA0 File Offset: 0x00011DA0
		public override void Skip()
		{
			try
			{
				base.Skip();
				this.dataSizeForRead = 0;
				this.dataOffsetForRead = 0;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
			catch (SocketException ex2)
			{
				throw new XmlaStreamException(ex2);
			}
			catch (Win32Exception ex3)
			{
				throw new XmlaStreamException(ex3);
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00013C00 File Offset: 0x00011E00
		public override void Dispose()
		{
			try
			{
				this.securityContext.Dispose();
			}
			catch (Win32Exception)
			{
			}
			finally
			{
				base.Dispose();
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00013C40 File Offset: 0x00011E40
		private protected void WriteHeader(ushort dataSize, ushort tokenSize)
		{
			this.outBufferForPackageSize[0] = (byte)(dataSize & 255);
			this.outBufferForPackageSize[1] = (byte)((dataSize >> 8) & 255);
			this.outBufferForPackageSize[2] = (byte)(tokenSize & 255);
			this.outBufferForPackageSize[3] = (byte)((tokenSize >> 8) & 255);
			base.Write(this.outBufferForPackageSize, 0, 4);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00013CA0 File Offset: 0x00011EA0
		private protected void Write()
		{
			SecurityMode mode = this.securityContext.Mode;
			if (mode == SecurityMode.Block)
			{
				this.WriteInBlockMode();
				return;
			}
			if (mode != SecurityMode.Stream)
			{
				return;
			}
			this.WriteInStreamMode();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00013CD0 File Offset: 0x00011ED0
		private protected bool ReadHeader()
		{
			int i = 0;
			while (i < 4)
			{
				int num = base.Read(this.inBufferForPackageSize, i, 4 - i);
				if (num == 0)
				{
					if (i == 0)
					{
						return false;
					}
					throw new Exception(XmlaSR.UnknownServerResponseFormat);
				}
				else
				{
					i += num;
				}
			}
			this.dataSizeForRead = (ushort)((int)this.inBufferForPackageSize[0] + ((int)this.inBufferForPackageSize[1] << 8));
			this.tokenSizeForRead = (ushort)((int)this.inBufferForPackageSize[2] + ((int)this.inBufferForPackageSize[3] << 8));
			return true;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00013D44 File Offset: 0x00011F44
		private void WriteInStreamMode()
		{
			base.Write(this.securityBuffers[0].Buffer, this.securityBuffers[0].Offset, this.securityBuffers[0].Size);
			base.Write(this.securityBuffers[1].Buffer, this.securityBuffers[1].Offset, this.securityBuffers[1].Size);
			base.Write(this.securityBuffers[2].Buffer, this.securityBuffers[2].Offset, this.securityBuffers[2].Size);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00013DD8 File Offset: 0x00011FD8
		private void WriteInBlockMode()
		{
			if (this.securityBuffers[0].Type != SecurityBufferType.Data)
			{
				throw new XmlaStreamException(XmlaSR.InternalErrorAndInvalidBufferType);
			}
			if (this.securityBuffers[1].Type != SecurityBufferType.Token)
			{
				throw new XmlaStreamException(XmlaSR.InternalErrorAndInvalidBufferType);
			}
			this.WriteHeader((ushort)this.securityBuffers[0].Size, (ushort)this.securityBuffers[1].Size);
			base.Write(this.securityBuffers[0].Buffer, this.securityBuffers[0].Offset, this.securityBuffers[0].Size);
			base.Write(this.securityBuffers[1].Buffer, this.securityBuffers[1].Offset, this.securityBuffers[1].Size);
		}

		// Token: 0x04000266 RID: 614
		private protected byte[] outBufferForPackageSize = new byte[4];

		// Token: 0x04000267 RID: 615
		private protected byte[] inBufferForPackageSize = new byte[4];

		// Token: 0x04000268 RID: 616
		private protected SecurityContext securityContext;

		// Token: 0x04000269 RID: 617
		private protected SecurityBuffer[] securityBuffers;

		// Token: 0x0400026A RID: 618
		private protected int maxTokenSize;

		// Token: 0x0400026B RID: 619
		private protected int maxEncryptionBufferSize;

		// Token: 0x0400026C RID: 620
		private protected int maxStreamMessageLength;

		// Token: 0x0400026D RID: 621
		private protected ArraySegment<byte> streamHeaderForWrite;

		// Token: 0x0400026E RID: 622
		private protected ArraySegment<byte> streamTrailerForWrite;

		// Token: 0x0400026F RID: 623
		private protected List<ArraySegment<byte>> streamEncryptedDataAccumulatorForRead;

		// Token: 0x04000270 RID: 624
		private protected List<ArraySegment<byte>> streamEncryptedDataAccumulatorForReadFreeBuffers;

		// Token: 0x04000271 RID: 625
		private protected byte[] contiguosEncryptedByteArrayCache;

		// Token: 0x04000272 RID: 626
		private protected List<ArraySegment<byte>> streamDecryptedDataForRead;

		// Token: 0x04000273 RID: 627
		private protected byte[] dataBufferForRead = new byte[65535];

		// Token: 0x04000274 RID: 628
		private protected ushort dataOffsetForRead;

		// Token: 0x04000275 RID: 629
		private protected ushort dataSizeForRead;

		// Token: 0x04000276 RID: 630
		private protected byte[] tokenBufferForRead;

		// Token: 0x04000277 RID: 631
		private protected ushort tokenSizeForRead;

		// Token: 0x04000278 RID: 632
		private protected int sequenceNumberForRead;

		// Token: 0x04000279 RID: 633
		private protected byte[] tokenBufferForWrite;

		// Token: 0x0400027A RID: 634
		private protected int sequenceNumberForWrite;
	}
}
