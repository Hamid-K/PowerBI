using System;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using Microsoft.AnalysisServices.AdomdClient.Sspi;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000036 RID: 54
	internal class TcpSignedStream : TcpSecureStream
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x00010D17 File Offset: 0x0000EF17
		public TcpSignedStream(TcpStream tcpStream, SecurityContext securityContext)
			: base(tcpStream, securityContext)
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00010D24 File Offset: 0x0000EF24
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				for (int i = Math.Min(65535, size); i > 0; i = Math.Min(65535, size))
				{
					this.securityBuffers[0].Update(SecurityBufferType.Data, offset, size, buffer);
					this.securityBuffers[1].Update(SecurityBufferType.Token, 0, this.maxTokenSize, this.tokenBufferForWrite);
					SecurityContext securityContext = this.securityContext;
					SecurityBuffer[] securityBuffers = this.securityBuffers;
					int num = this.sequenceNumberForWrite + 1;
					this.sequenceNumberForWrite = num;
					securityContext.MakeSignature(securityBuffers, num);
					base.Write();
					offset += i;
					size -= i;
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
				throw new XmlaStreamException(ex3.Message, ex3);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00010E04 File Offset: 0x0000F004
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			int num;
			try
			{
				if (size == 0)
				{
					num = 0;
				}
				else
				{
					while (this.dataSizeForRead <= 0)
					{
						if (!this.Read())
						{
							return 0;
						}
						SecurityContext securityContext = this.securityContext;
						SecurityBuffer[] securityBuffers = this.securityBuffers;
						int num2 = this.sequenceNumberForRead + 1;
						this.sequenceNumberForRead = num2;
						securityContext.VerifySignature(securityBuffers, num2);
						if (this.securityBuffers[0].Offset + this.securityBuffers[0].Size > 65535)
						{
							throw new XmlaStreamException(XmlaSR.InternalError);
						}
						this.dataOffsetForRead = (ushort)this.securityBuffers[0].Offset;
						this.dataSizeForRead = (ushort)this.securityBuffers[0].Size;
					}
					ushort num3 = 0;
					int num4 = (int)this.dataOffsetForRead;
					int num5 = offset;
					while (num4 < (int)(this.dataOffsetForRead + this.dataSizeForRead) && num5 < offset + size)
					{
						buffer[num5] = this.dataBufferForRead[num4];
						num4++;
						num5++;
						num3 += 1;
					}
					this.dataSizeForRead -= num3;
					this.dataOffsetForRead += num3;
					num = (int)num3;
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
				throw new XmlaStreamException(ex3.Message, ex3);
			}
			return num;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00010F8C File Offset: 0x0000F18C
		private bool Read()
		{
			if (!base.ReadHeader())
			{
				return false;
			}
			for (int i = 0; i < (int)this.dataSizeForRead; i += base.Read(this.dataBufferForRead, i, (int)this.dataSizeForRead - i))
			{
			}
			for (int j = 0; j < (int)this.tokenSizeForRead; j += base.Read(this.tokenBufferForRead, j, (int)this.tokenSizeForRead - j))
			{
			}
			this.securityBuffers[0].Update(SecurityBufferType.Data, 0, (int)this.dataSizeForRead, this.dataBufferForRead);
			this.securityBuffers[1].Update(SecurityBufferType.Token, 0, (int)this.tokenSizeForRead, this.tokenBufferForRead);
			return true;
		}
	}
}
