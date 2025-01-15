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
		// Token: 0x060002D0 RID: 720 RVA: 0x000107E0 File Offset: 0x0000E9E0
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

		// Token: 0x060002D1 RID: 721 RVA: 0x00010A20 File Offset: 0x0000EC20
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

		// Token: 0x060002D2 RID: 722 RVA: 0x00010A80 File Offset: 0x0000EC80
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

		// Token: 0x060002D3 RID: 723 RVA: 0x00010AC0 File Offset: 0x0000ECC0
		private protected void WriteHeader(ushort dataSize, ushort tokenSize)
		{
			this.outBufferForPackageSize[0] = (byte)(dataSize & 255);
			this.outBufferForPackageSize[1] = (byte)((dataSize >> 8) & 255);
			this.outBufferForPackageSize[2] = (byte)(tokenSize & 255);
			this.outBufferForPackageSize[3] = (byte)((tokenSize >> 8) & 255);
			base.Write(this.outBufferForPackageSize, 0, 4);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00010B20 File Offset: 0x0000ED20
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

		// Token: 0x060002D5 RID: 725 RVA: 0x00010B50 File Offset: 0x0000ED50
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

		// Token: 0x060002D6 RID: 726 RVA: 0x00010BC4 File Offset: 0x0000EDC4
		private void WriteInStreamMode()
		{
			base.Write(this.securityBuffers[0].Buffer, this.securityBuffers[0].Offset, this.securityBuffers[0].Size);
			base.Write(this.securityBuffers[1].Buffer, this.securityBuffers[1].Offset, this.securityBuffers[1].Size);
			base.Write(this.securityBuffers[2].Buffer, this.securityBuffers[2].Offset, this.securityBuffers[2].Size);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00010C58 File Offset: 0x0000EE58
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

		// Token: 0x04000215 RID: 533
		private protected byte[] outBufferForPackageSize = new byte[4];

		// Token: 0x04000216 RID: 534
		private protected byte[] inBufferForPackageSize = new byte[4];

		// Token: 0x04000217 RID: 535
		private protected SecurityContext securityContext;

		// Token: 0x04000218 RID: 536
		private protected SecurityBuffer[] securityBuffers;

		// Token: 0x04000219 RID: 537
		private protected int maxTokenSize;

		// Token: 0x0400021A RID: 538
		private protected int maxEncryptionBufferSize;

		// Token: 0x0400021B RID: 539
		private protected int maxStreamMessageLength;

		// Token: 0x0400021C RID: 540
		private protected ArraySegment<byte> streamHeaderForWrite;

		// Token: 0x0400021D RID: 541
		private protected ArraySegment<byte> streamTrailerForWrite;

		// Token: 0x0400021E RID: 542
		private protected List<ArraySegment<byte>> streamEncryptedDataAccumulatorForRead;

		// Token: 0x0400021F RID: 543
		private protected List<ArraySegment<byte>> streamEncryptedDataAccumulatorForReadFreeBuffers;

		// Token: 0x04000220 RID: 544
		private protected byte[] contiguosEncryptedByteArrayCache;

		// Token: 0x04000221 RID: 545
		private protected List<ArraySegment<byte>> streamDecryptedDataForRead;

		// Token: 0x04000222 RID: 546
		private protected byte[] dataBufferForRead = new byte[65535];

		// Token: 0x04000223 RID: 547
		private protected ushort dataOffsetForRead;

		// Token: 0x04000224 RID: 548
		private protected ushort dataSizeForRead;

		// Token: 0x04000225 RID: 549
		private protected byte[] tokenBufferForRead;

		// Token: 0x04000226 RID: 550
		private protected ushort tokenSizeForRead;

		// Token: 0x04000227 RID: 551
		private protected int sequenceNumberForRead;

		// Token: 0x04000228 RID: 552
		private protected byte[] tokenBufferForWrite;

		// Token: 0x04000229 RID: 553
		private protected int sequenceNumberForWrite;
	}
}
