using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using Microsoft.AnalysisServices.AdomdClient.Sspi;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000035 RID: 53
	internal abstract class TcpSecureStream : TcpStream
	{
		// Token: 0x060002DD RID: 733 RVA: 0x00010B10 File Offset: 0x0000ED10
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

		// Token: 0x060002DE RID: 734 RVA: 0x00010D50 File Offset: 0x0000EF50
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

		// Token: 0x060002DF RID: 735 RVA: 0x00010DB0 File Offset: 0x0000EFB0
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

		// Token: 0x060002E0 RID: 736 RVA: 0x00010DF0 File Offset: 0x0000EFF0
		private protected void WriteHeader(ushort dataSize, ushort tokenSize)
		{
			this.outBufferForPackageSize[0] = (byte)(dataSize & 255);
			this.outBufferForPackageSize[1] = (byte)((dataSize >> 8) & 255);
			this.outBufferForPackageSize[2] = (byte)(tokenSize & 255);
			this.outBufferForPackageSize[3] = (byte)((tokenSize >> 8) & 255);
			base.Write(this.outBufferForPackageSize, 0, 4);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00010E50 File Offset: 0x0000F050
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

		// Token: 0x060002E2 RID: 738 RVA: 0x00010E80 File Offset: 0x0000F080
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

		// Token: 0x060002E3 RID: 739 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		private void WriteInStreamMode()
		{
			base.Write(this.securityBuffers[0].Buffer, this.securityBuffers[0].Offset, this.securityBuffers[0].Size);
			base.Write(this.securityBuffers[1].Buffer, this.securityBuffers[1].Offset, this.securityBuffers[1].Size);
			base.Write(this.securityBuffers[2].Buffer, this.securityBuffers[2].Offset, this.securityBuffers[2].Size);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00010F88 File Offset: 0x0000F188
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

		// Token: 0x04000222 RID: 546
		private protected byte[] outBufferForPackageSize = new byte[4];

		// Token: 0x04000223 RID: 547
		private protected byte[] inBufferForPackageSize = new byte[4];

		// Token: 0x04000224 RID: 548
		private protected SecurityContext securityContext;

		// Token: 0x04000225 RID: 549
		private protected SecurityBuffer[] securityBuffers;

		// Token: 0x04000226 RID: 550
		private protected int maxTokenSize;

		// Token: 0x04000227 RID: 551
		private protected int maxEncryptionBufferSize;

		// Token: 0x04000228 RID: 552
		private protected int maxStreamMessageLength;

		// Token: 0x04000229 RID: 553
		private protected ArraySegment<byte> streamHeaderForWrite;

		// Token: 0x0400022A RID: 554
		private protected ArraySegment<byte> streamTrailerForWrite;

		// Token: 0x0400022B RID: 555
		private protected List<ArraySegment<byte>> streamEncryptedDataAccumulatorForRead;

		// Token: 0x0400022C RID: 556
		private protected List<ArraySegment<byte>> streamEncryptedDataAccumulatorForReadFreeBuffers;

		// Token: 0x0400022D RID: 557
		private protected byte[] contiguosEncryptedByteArrayCache;

		// Token: 0x0400022E RID: 558
		private protected List<ArraySegment<byte>> streamDecryptedDataForRead;

		// Token: 0x0400022F RID: 559
		private protected byte[] dataBufferForRead = new byte[65535];

		// Token: 0x04000230 RID: 560
		private protected ushort dataOffsetForRead;

		// Token: 0x04000231 RID: 561
		private protected ushort dataSizeForRead;

		// Token: 0x04000232 RID: 562
		private protected byte[] tokenBufferForRead;

		// Token: 0x04000233 RID: 563
		private protected ushort tokenSizeForRead;

		// Token: 0x04000234 RID: 564
		private protected int sequenceNumberForRead;

		// Token: 0x04000235 RID: 565
		private protected byte[] tokenBufferForWrite;

		// Token: 0x04000236 RID: 566
		private protected int sequenceNumberForWrite;
	}
}
