using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Microsoft.AnalysisServices.AdomdClient.Sspi;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000034 RID: 52
	internal class TcpEncryptedStream : TcpSecureStream
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x0000F053 File Offset: 0x0000D253
		public TcpEncryptedStream(TcpStream tcpStream, SecurityContext securityContext)
			: base(tcpStream, securityContext)
		{
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000F060 File Offset: 0x0000D260
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				SecurityMode mode = this.securityContext.Mode;
				if (mode != SecurityMode.Block)
				{
					if (mode == SecurityMode.Stream)
					{
						this.WriteInStreamMode(buffer, offset, size);
					}
				}
				else
				{
					this.WriteInBlockMode(buffer, offset, size);
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

		// Token: 0x060002D3 RID: 723 RVA: 0x0000F0E4 File Offset: 0x0000D2E4
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (size == 0)
			{
				return 0;
			}
			int num;
			try
			{
				SecurityMode mode = this.securityContext.Mode;
				if (mode != SecurityMode.Block)
				{
					if (mode != SecurityMode.Stream)
					{
						throw new XmlaStreamException("SecurityContextMode " + this.securityContext.Mode.ToString() + " not configured!");
					}
					num = this.ReadInStreamMode(buffer, offset, size);
				}
				else
				{
					num = this.ReadInBlockMode(buffer, offset, size);
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
			return num;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		private static void CleanSecurityBuffers(SecurityBuffer[] buffers)
		{
			for (int i = 0; i < buffers.Length; i++)
			{
				buffers[i].Reset();
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000F1C4 File Offset: 0x0000D3C4
		private void WriteInStreamMode(byte[] buffer, int offset, int size)
		{
			if (TcpStream.TRACESWITCH.TraceVerbose)
			{
				StackTrace stackTrace = new StackTrace();
				MethodBase method = stackTrace.GetFrame(1).GetMethod();
				Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpEncryptedStream}::WriteInStreamMode]: ")
					.Append('[')
					.Append('(')
					.Append(stackTrace.FrameCount)
					.Append(')')
					.Append(' ')
					.Append(base.GetType().Equals(method.DeclaringType) ? "." : method.DeclaringType.Name)
					.Append("::")
					.Append(method.Name)
					.Append("] ")
					.Append("Begin")
					.Append("..  ")
					.Append("buffer sz: ")
					.Append(buffer.Length)
					.Append(", ")
					.Append("offset: ")
					.Append(offset)
					.Append(", ")
					.Append("size: ")
					.Append(size)
					.Append("; ")
					.Append("cbMaxMessage: ")
					.Append(this.maxStreamMessageLength)
					.Append("; ")
					.ToString());
			}
			for (int i = Math.Min(this.maxStreamMessageLength, size); i > 0; i = Math.Min(this.maxStreamMessageLength, size))
			{
				this.securityBuffers[0].Update(SecurityBufferType.StreamHeader, this.streamHeaderForWrite.Offset, this.streamHeaderForWrite.Count, this.streamHeaderForWrite.Array);
				this.securityBuffers[1].Update(SecurityBufferType.Data, offset, i, buffer);
				this.securityBuffers[2].Update(SecurityBufferType.StreamTrailer, this.streamTrailerForWrite.Offset, this.streamTrailerForWrite.Count, this.streamTrailerForWrite.Array);
				this.securityBuffers[3].Update(SecurityBufferType.Empty);
				try
				{
					SecurityContext securityContext = this.securityContext;
					SecurityBuffer[] securityBuffers = this.securityBuffers;
					int num = this.sequenceNumberForWrite + 1;
					this.sequenceNumberForWrite = num;
					securityContext.EncryptMessage(securityBuffers, num);
				}
				catch (Win32Exception ex)
				{
					throw new XmlaStreamException(ex.Message, ex);
				}
				base.Write();
				offset += i;
				size -= i;
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000F414 File Offset: 0x0000D614
		private void WriteInBlockMode(byte[] buffer, int offset, int size)
		{
			for (int i = Math.Min(this.maxEncryptionBufferSize, size); i > 0; i = Math.Min(this.maxEncryptionBufferSize, size))
			{
				this.securityBuffers[0].Update(SecurityBufferType.Data, offset, i, buffer);
				this.securityBuffers[1].Update(SecurityBufferType.Token, 0, this.maxTokenSize, this.tokenBufferForWrite);
				try
				{
					SecurityContext securityContext = this.securityContext;
					SecurityBuffer[] securityBuffers = this.securityBuffers;
					int num = this.sequenceNumberForWrite + 1;
					this.sequenceNumberForWrite = num;
					securityContext.EncryptMessage(securityBuffers, num);
				}
				catch (Win32Exception ex)
				{
					throw new XmlaStreamException(ex.Message, ex);
				}
				base.Write();
				offset += i;
				size -= i;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		private int ReadInStreamMode(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer can't be null!");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset can't be negative!");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size can't be negative!");
			}
			if (size + offset > buffer.Length)
			{
				throw new ArgumentException(XmlaSR.InvalidArgument, "buffer is smaller than offset + size!");
			}
			if (this.streamDecryptedDataForRead.Count > 0)
			{
				return this.ReturnAvailableDecryptedData(buffer, offset, size);
			}
			this.ReleaseEncryptedDataAccumulatorForReadFreeBuffers();
			int i = 0;
			for (int j = 0; j < this.streamEncryptedDataAccumulatorForRead.Count; j++)
			{
				i += this.streamEncryptedDataAccumulatorForRead[j].Count;
			}
			bool flag;
			if (i < this.maxStreamMessageLength)
			{
				ArraySegment<byte> orCreateFreeEncryptedBuffer = this.GetOrCreateFreeEncryptedBuffer(this.maxStreamMessageLength);
				int num = base.Read(orCreateFreeEncryptedBuffer.Array, orCreateFreeEncryptedBuffer.Offset, orCreateFreeEncryptedBuffer.Count);
				if (num > 0)
				{
					orCreateFreeEncryptedBuffer = new ArraySegment<byte>(orCreateFreeEncryptedBuffer.Array, orCreateFreeEncryptedBuffer.Offset, num);
					this.streamEncryptedDataAccumulatorForRead.Add(orCreateFreeEncryptedBuffer);
					i += num;
				}
				else
				{
					this.streamEncryptedDataAccumulatorForReadFreeBuffers.Add(orCreateFreeEncryptedBuffer);
				}
				flag = true;
				if (TcpStream.TRACESWITCH.TraceVerbose)
				{
					StackTrace stackTrace = new StackTrace();
					MethodBase method = stackTrace.GetFrame(1).GetMethod();
					Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpEncryptedStream}::ReadInStreamMode]: ")
						.Append('[')
						.Append('(')
						.Append(stackTrace.FrameCount)
						.Append(')')
						.Append(' ')
						.Append(base.GetType().Equals(method.DeclaringType) ? "." : method.DeclaringType.Name)
						.Append("::")
						.Append(method.Name)
						.Append("] ")
						.Append("Read more data from TCP")
						.Append("..  ")
						.Append("buffer sz: ")
						.Append(buffer.Length)
						.Append(", ")
						.Append("offset: ")
						.Append(offset)
						.Append(", ")
						.Append("size: ")
						.Append(size)
						.Append("; ")
						.Append("encryptedDataSzRead: ")
						.Append(num)
						.Append(", ")
						.Append("streamEncryptedDataAccumulatorForRead Sz: ")
						.Append(i)
						.Append(", ")
						.Append("cbMaxMessage: ")
						.Append(this.maxStreamMessageLength)
						.Append("; ")
						.ToString());
				}
			}
			else
			{
				flag = false;
				if (TcpStream.TRACESWITCH.TraceVerbose)
				{
					StackTrace stackTrace2 = new StackTrace();
					MethodBase method2 = stackTrace2.GetFrame(1).GetMethod();
					Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpEncryptedStream}::ReadInStreamMode]: ")
						.Append('[')
						.Append('(')
						.Append(stackTrace2.FrameCount)
						.Append(')')
						.Append(' ')
						.Append(base.GetType().Equals(method2.DeclaringType) ? "." : method2.DeclaringType.Name)
						.Append("::")
						.Append(method2.Name)
						.Append("] ")
						.Append("Decrypting accumulated data")
						.Append("..  ")
						.Append("buffer sz: ")
						.Append(buffer.Length)
						.Append(", ")
						.Append("offset: ")
						.Append(offset)
						.Append(", ")
						.Append("size: ")
						.Append(size)
						.Append("; ")
						.Append("streamEncryptedDataAccumulatorForRead Sz: ")
						.Append(i)
						.Append(", ")
						.Append("cbMaxMessage: ")
						.Append(this.maxStreamMessageLength)
						.Append("; ")
						.ToString());
				}
			}
			try
			{
				while (i > 0)
				{
					TcpEncryptedStream.CleanSecurityBuffers(this.securityBuffers);
					byte[] array;
					if (this.contiguosEncryptedByteArrayCache != null && this.contiguosEncryptedByteArrayCache.Length >= i)
					{
						array = this.contiguosEncryptedByteArrayCache;
					}
					else
					{
						array = (this.contiguosEncryptedByteArrayCache = new byte[i]);
					}
					int k = 0;
					int num2 = 0;
					while (k < this.streamEncryptedDataAccumulatorForRead.Count)
					{
						ArraySegment<byte> arraySegment = this.streamEncryptedDataAccumulatorForRead[k];
						Array.Copy(arraySegment.Array, arraySegment.Offset, array, num2, arraySegment.Count);
						num2 += arraySegment.Count;
						this.streamEncryptedDataAccumulatorForReadFreeBuffers.Add(new ArraySegment<byte>(arraySegment.Array));
						k++;
					}
					this.streamEncryptedDataAccumulatorForRead.Clear();
					this.securityBuffers[0].Update(SecurityBufferType.Data, 0, i, array);
					SecurityContext securityContext = this.securityContext;
					SecurityBuffer[] securityBuffers = this.securityBuffers;
					int num3 = this.sequenceNumberForRead + 1;
					this.sequenceNumberForRead = num3;
					bool flag2 = !securityContext.DecryptMessage(securityBuffers, num3, true);
					if (flag2)
					{
						if (TcpStream.TRACESWITCH.TraceVerbose)
						{
							StackTrace stackTrace3 = new StackTrace();
							MethodBase method3 = stackTrace3.GetFrame(1).GetMethod();
							Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpEncryptedStream}::ReadInStreamMode]: ")
								.Append('[')
								.Append('(')
								.Append(stackTrace3.FrameCount)
								.Append(')')
								.Append(' ')
								.Append(base.GetType().Equals(method3.DeclaringType) ? "." : method3.DeclaringType.Name)
								.Append("::")
								.Append(method3.Name)
								.Append("] ")
								.Append("Incomplete message after decryption")
								.Append("..  ")
								.Append("buffer sz: ")
								.Append(buffer.Length)
								.Append(", ")
								.Append("offset: ")
								.Append(offset)
								.Append(", ")
								.Append("size: ")
								.Append(size)
								.Append("; ")
								.Append("encryptedDataSzRead: ")
								.Append(array.Length)
								.Append(", ")
								.Append("streamEncryptedDataAccumulatorForRead Sz: ")
								.Append(i)
								.Append(", ")
								.Append("cbMaxMessage: ")
								.Append(this.maxStreamMessageLength)
								.Append(", ")
								.Append("; ")
								.ToString());
						}
						this.streamEncryptedDataAccumulatorForRead.Add(new ArraySegment<byte>(array, 0, i));
						byte[] array2 = (this.contiguosEncryptedByteArrayCache = null);
					}
					else
					{
						int num4 = this.ExtractDecryptedAndExtraData(buffer, offset, size);
						if (num4 > 0)
						{
							return num4;
						}
					}
					ArraySegment<byte> orCreateFreeEncryptedBuffer2 = this.GetOrCreateFreeEncryptedBuffer(this.maxStreamMessageLength);
					int num5 = base.Read(orCreateFreeEncryptedBuffer2.Array, orCreateFreeEncryptedBuffer2.Offset, orCreateFreeEncryptedBuffer2.Count);
					if (num5 > 0)
					{
						orCreateFreeEncryptedBuffer2 = new ArraySegment<byte>(orCreateFreeEncryptedBuffer2.Array, orCreateFreeEncryptedBuffer2.Offset, num5);
						this.streamEncryptedDataAccumulatorForRead.Add(orCreateFreeEncryptedBuffer2);
					}
					else
					{
						this.streamEncryptedDataAccumulatorForReadFreeBuffers.Add(orCreateFreeEncryptedBuffer2);
						if (flag2)
						{
							throw new XmlaStreamException(string.Format("Not complete Encrypted stream received from underlying layer of type {0}! DecryptMessage returned SEC_E_INCOMPLETE_MESSAGE while underlying stream reported all data was read.", base.GetType().Name));
						}
					}
					i = 0;
					for (int l = 0; l < this.streamEncryptedDataAccumulatorForRead.Count; l++)
					{
						i += this.streamEncryptedDataAccumulatorForRead[l].Count;
					}
					if (TcpStream.TRACESWITCH.TraceVerbose)
					{
						StackTrace stackTrace4 = new StackTrace();
						MethodBase method4 = stackTrace4.GetFrame(1).GetMethod();
						Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpEncryptedStream}::ReadInStreamMode]: ")
							.Append('[')
							.Append('(')
							.Append(stackTrace4.FrameCount)
							.Append(')')
							.Append(' ')
							.Append(base.GetType().Equals(method4.DeclaringType) ? "." : method4.DeclaringType.Name)
							.Append("::")
							.Append(method4.Name)
							.Append("] ")
							.Append("No more data left in TCP. Decrypt whatever left.")
							.Append("..  ")
							.Append("buffer sz: ")
							.Append(buffer.Length)
							.Append(", ")
							.Append("offset: ")
							.Append(offset)
							.Append(", ")
							.Append("size: ")
							.Append(size)
							.Append("; ")
							.Append("streamEncryptedDataAccumulatorForRead Sz: ")
							.Append(i)
							.Append(", ")
							.Append("cbMaxMessage: ")
							.Append(this.maxStreamMessageLength)
							.Append(", ")
							.Append("; ")
							.ToString());
					}
				}
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(string.Format("readFromTcp={0}; encryptedDataSz={1}", flag, i), ex);
			}
			return 0;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000FE00 File Offset: 0x0000E000
		private int ReadInBlockMode(byte[] buffer, int offset, int size)
		{
			try
			{
				while (this.dataSizeForRead <= 0)
				{
					if (!base.ReadHeader())
					{
						return 0;
					}
					for (int i = 0; i < (int)this.dataSizeForRead; i += base.Read(this.dataBufferForRead, i, (int)this.dataSizeForRead - i))
					{
					}
					for (int j = 0; j < (int)this.tokenSizeForRead; j += base.Read(this.tokenBufferForRead, j, (int)this.tokenSizeForRead - j))
					{
					}
					if (this.dataSizeForRead > 0)
					{
						this.securityBuffers[0].Update(SecurityBufferType.Data, 0, (int)this.dataSizeForRead, this.dataBufferForRead);
					}
					else
					{
						this.securityBuffers[0].Update(SecurityBufferType.Data);
					}
					this.securityBuffers[1].Update(SecurityBufferType.Token, 0, (int)this.tokenSizeForRead, this.tokenBufferForRead);
					SecurityContext securityContext = this.securityContext;
					SecurityBuffer[] securityBuffers = this.securityBuffers;
					int num = this.sequenceNumberForRead + 1;
					this.sequenceNumberForRead = num;
					securityContext.DecryptMessage(securityBuffers, num, false);
					if (this.securityBuffers[0].Offset + this.securityBuffers[0].Size > 65535)
					{
						throw new XmlaStreamException(XmlaSR.InternalError);
					}
					this.dataOffsetForRead = (ushort)this.securityBuffers[0].Offset;
					this.dataSizeForRead = (ushort)this.securityBuffers[0].Size;
				}
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(ex.Message, ex);
			}
			ushort num2 = 0;
			int num3 = (int)this.dataOffsetForRead;
			int num4 = offset;
			while (num3 < (int)(this.dataOffsetForRead + this.dataSizeForRead) && num4 < offset + size)
			{
				buffer[num4] = this.dataBufferForRead[num3];
				num3++;
				num4++;
				num2 += 1;
			}
			this.dataSizeForRead -= num2;
			this.dataOffsetForRead += num2;
			return (int)num2;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
		private void ReleaseEncryptedDataAccumulatorForReadFreeBuffers()
		{
			if (this.streamEncryptedDataAccumulatorForReadFreeBuffers.Count > 8)
			{
				if (TcpStream.TRACESWITCH.TraceVerbose)
				{
					StackTrace stackTrace = new StackTrace();
					MethodBase method = stackTrace.GetFrame(1).GetMethod();
					Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpEncryptedStream}::ReleaseEncryptedDataAccumulatorForReadFreeBuffers]: ")
						.Append('[')
						.Append('(')
						.Append(stackTrace.FrameCount)
						.Append(')')
						.Append(' ')
						.Append(base.GetType().Equals(method.DeclaringType) ? "." : method.DeclaringType.Name)
						.Append("::")
						.Append(method.Name)
						.Append("] ")
						.Append("Release ")
						.Append(this.streamEncryptedDataAccumulatorForReadFreeBuffers.Count - 8)
						.Append(" encrypted accumulated buffers")
						.Append("..  ")
						.Append("; ")
						.ToString());
				}
				this.streamEncryptedDataAccumulatorForReadFreeBuffers.Sort((ArraySegment<byte> v1, ArraySegment<byte> v2) => v2.Count - v1.Count);
				for (int i = this.streamEncryptedDataAccumulatorForReadFreeBuffers.Count - 1; i >= 8; i--)
				{
					this.streamEncryptedDataAccumulatorForReadFreeBuffers.RemoveAt(i);
				}
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00010144 File Offset: 0x0000E344
		private ArraySegment<byte> GetOrCreateFreeEncryptedBuffer(int requiredSz)
		{
			if (this.streamEncryptedDataAccumulatorForReadFreeBuffers.Count > 0)
			{
				this.streamEncryptedDataAccumulatorForReadFreeBuffers.Sort((ArraySegment<byte> v1, ArraySegment<byte> v2) => v2.Count - v1.Count);
				for (int i = this.streamEncryptedDataAccumulatorForReadFreeBuffers.Count - 1; i >= 0; i--)
				{
					if (this.streamEncryptedDataAccumulatorForReadFreeBuffers[i].Count >= requiredSz)
					{
						ArraySegment<byte> arraySegment = this.streamEncryptedDataAccumulatorForReadFreeBuffers[i];
						this.streamEncryptedDataAccumulatorForReadFreeBuffers.RemoveAt(i);
						return arraySegment;
					}
				}
			}
			return new ArraySegment<byte>(new byte[Math.Max(requiredSz, this.maxStreamMessageLength)]);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000101E8 File Offset: 0x0000E3E8
		private int ReturnAvailableDecryptedData(byte[] buffer, int offset, int size)
		{
			if (TcpStream.TRACESWITCH.TraceVerbose)
			{
				StackTrace stackTrace = new StackTrace();
				MethodBase method = stackTrace.GetFrame(1).GetMethod();
				int num = 0;
				foreach (ArraySegment<byte> arraySegment in this.streamDecryptedDataForRead)
				{
					num += arraySegment.Count;
				}
				Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpEncryptedStream}::ReturnAvailableDecryptedData]: ")
					.Append('[')
					.Append('(')
					.Append(stackTrace.FrameCount)
					.Append(')')
					.Append(' ')
					.Append(base.GetType().Equals(method.DeclaringType) ? "." : method.DeclaringType.Name)
					.Append("::")
					.Append(method.Name)
					.Append("] ")
					.Append("Return already decrypted data")
					.Append("..  ")
					.Append("buffer sz: ")
					.Append(buffer.Length)
					.Append(", ")
					.Append("offset: ")
					.Append(offset)
					.Append(", ")
					.Append("size: ")
					.Append(size)
					.Append("; ")
					.Append("decryptedDataSz: ")
					.Append(num)
					.Append("; ")
					.Append("streamDecryptedDataForReadCount: ")
					.Append(this.streamDecryptedDataForRead.Count)
					.Append("; ")
					.ToString());
			}
			int num2 = 0;
			int num3 = size;
			int num4 = offset;
			int num5 = 0;
			while (num3 > 0 && num5 < this.streamDecryptedDataForRead.Count)
			{
				ArraySegment<byte> arraySegment2 = this.streamDecryptedDataForRead[num5];
				int num6 = Math.Min(arraySegment2.Count, num3);
				Array.Copy(arraySegment2.Array, arraySegment2.Offset, buffer, num4, num6);
				this.streamDecryptedDataForRead[num5] = new ArraySegment<byte>(arraySegment2.Array, arraySegment2.Offset + num6, arraySegment2.Count - num6);
				num4 += num6;
				num3 -= num6;
				num2 += num6;
				num5++;
			}
			int i = 0;
			while (i < this.streamDecryptedDataForRead.Count)
			{
				if (this.streamDecryptedDataForRead[i].Count > 0)
				{
					i++;
				}
				else
				{
					this.streamDecryptedDataForRead.RemoveAt(i);
				}
			}
			if (TcpStream.TRACESWITCH.TraceVerbose)
			{
				StackTrace stackTrace2 = new StackTrace();
				MethodBase method2 = stackTrace2.GetFrame(1).GetMethod();
				int num7 = 0;
				foreach (ArraySegment<byte> arraySegment3 in this.streamDecryptedDataForRead)
				{
					num7 += arraySegment3.Count;
				}
				Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpEncryptedStream}::ReturnAvailableDecryptedData]: ")
					.Append('[')
					.Append('(')
					.Append(stackTrace2.FrameCount)
					.Append(')')
					.Append(' ')
					.Append(base.GetType().Equals(method2.DeclaringType) ? "." : method2.DeclaringType.Name)
					.Append("::")
					.Append(method2.Name)
					.Append("] ")
					.Append("Return already decrypted data")
					.Append("..  ")
					.Append("buffer sz: ")
					.Append(buffer.Length)
					.Append(", ")
					.Append("offset: ")
					.Append(offset)
					.Append(", ")
					.Append("size: ")
					.Append(size)
					.Append("; ")
					.Append("idxStreamDecryptedDataForRead: ")
					.Append(num5)
					.Append("; ")
					.Append("retVal: ")
					.Append(num2)
					.Append("; ")
					.Append("streamDecryptedDataForReadCount: ")
					.Append(this.streamDecryptedDataForRead.Count)
					.Append("; ")
					.ToString());
			}
			return num2;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00010660 File Offset: 0x0000E860
		private int ExtractDecryptedAndExtraData(byte[] readBuffer, int readOffset, int readSize)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (SecurityBuffer securityBuffer in this.securityBuffers)
			{
				if (securityBuffer.Type == SecurityBufferType.Data)
				{
					num2++;
					if (TcpStream.TRACESWITCH.TraceVerbose)
					{
						StackTrace stackTrace = new StackTrace();
						MethodBase method = stackTrace.GetFrame(1).GetMethod();
						Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpEncryptedStream}::ExtractDecryptedAndExtraData]: ")
							.Append('[')
							.Append('(')
							.Append(stackTrace.FrameCount)
							.Append(')')
							.Append(' ')
							.Append(base.GetType().Equals(method.DeclaringType) ? "." : method.DeclaringType.Name)
							.Append("::")
							.Append(method.Name)
							.Append("] ")
							.Append("Data Buffer processing.")
							.Append("..  ")
							.Append("; ")
							.Append("dataBufCount: ")
							.Append(num2)
							.Append("; ")
							.Append("input buffer sz: ")
							.Append(readBuffer.Length)
							.Append(", ")
							.Append("offset: ")
							.Append(readOffset)
							.Append(", ")
							.Append("size: ")
							.Append(readSize)
							.Append("; ")
							.Append("sec.buffer sz: ")
							.Append((securityBuffer.Buffer == null) ? "NULL" : securityBuffer.Buffer.Length.ToString())
							.Append(", ")
							.Append("offset: ")
							.Append(securityBuffer.Offset)
							.Append(", ")
							.Append("size: ")
							.Append(securityBuffer.Size)
							.Append("; ")
							.ToString());
					}
					if (securityBuffer.Buffer != null)
					{
						if (securityBuffer.Size <= readSize)
						{
							Array.Copy(securityBuffer.Buffer, securityBuffer.Offset, readBuffer, readOffset, securityBuffer.Size);
							num = securityBuffer.Size;
						}
						else
						{
							Array.Copy(securityBuffer.Buffer, securityBuffer.Offset, readBuffer, readOffset, readSize);
							num = readSize;
							this.streamDecryptedDataForRead.Add(new ArraySegment<byte>(securityBuffer.Buffer, securityBuffer.Offset + readSize, securityBuffer.Size - readSize));
						}
					}
					else
					{
						num = 0;
					}
				}
				else if (securityBuffer.Type == SecurityBufferType.Extra)
				{
					num3++;
					if (TcpStream.TRACESWITCH.TraceVerbose)
					{
						StackTrace stackTrace2 = new StackTrace();
						MethodBase method2 = stackTrace2.GetFrame(1).GetMethod();
						Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpEncryptedStream}::ExtractDecryptedAndExtraData]: ")
							.Append('[')
							.Append('(')
							.Append(stackTrace2.FrameCount)
							.Append(')')
							.Append(' ')
							.Append(base.GetType().Equals(method2.DeclaringType) ? "." : method2.DeclaringType.Name)
							.Append("::")
							.Append(method2.Name)
							.Append("] ")
							.Append("Extra Buffer processing.")
							.Append("..  ")
							.Append("; ")
							.Append("extraBufCount: ")
							.Append(num3)
							.Append("; ")
							.Append("input buffer sz: ")
							.Append(readBuffer.Length)
							.Append(", ")
							.Append("offset: ")
							.Append(readOffset)
							.Append(", ")
							.Append("size: ")
							.Append(readSize)
							.Append("; ")
							.Append("sec.buffer sz: ")
							.Append((securityBuffer.Buffer == null) ? "NULL" : securityBuffer.Buffer.Length.ToString())
							.Append(", ")
							.Append("offset: ")
							.Append(securityBuffer.Offset)
							.Append(", ")
							.Append("size: ")
							.Append(securityBuffer.Size)
							.Append("; ")
							.ToString());
					}
					if (securityBuffer.Buffer != null)
					{
						this.streamEncryptedDataAccumulatorForRead.Add(new ArraySegment<byte>(securityBuffer.Buffer, securityBuffer.Offset, securityBuffer.Size));
					}
				}
			}
			return num;
		}
	}
}
