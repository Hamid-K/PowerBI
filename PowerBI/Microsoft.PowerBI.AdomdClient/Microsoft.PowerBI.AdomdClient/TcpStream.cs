using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000037 RID: 55
	internal class TcpStream : TransportCapabilitiesAwareXmlaStream
	{
		// Token: 0x060002DC RID: 732 RVA: 0x00011024 File Offset: 0x0000F224
		public TcpStream(BufferedStream bufferedStream, int packetSize, XmlaDataType desiredRequestType, XmlaDataType desiredResponseType)
			: base(false, desiredRequestType, desiredResponseType)
		{
			this.bufferedStream = bufferedStream;
			this.dimeChunkSize = packetSize;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00011045 File Offset: 0x0000F245
		private protected TcpStream(TcpStream originalTcpStream)
			: base(false, originalTcpStream)
		{
			this.bufferedStream = originalTcpStream.bufferedStream;
			this.dimeChunkSize = originalTcpStream.dimeChunkSize;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00011070 File Offset: 0x0000F270
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size + offset > buffer.Length)
			{
				throw new ArgumentException(XmlaSR.InvalidArgument, "buffer");
			}
			try
			{
				if (size > 0)
				{
					if (this.dimeWriter == null)
					{
						this.dimeWriter = new DimeWriter(this.bufferedStream);
						this.dimeWriter.DefaultChunkSize = this.dimeChunkSize;
						this.dimeWriter.Options = base.GetTransportCapabilities();
						this.dimeRecordForWrite = this.dimeWriter.CreateRecord(null, DataTypes.GetDataTypeFromEnum(this.GetRequestDataType()), TypeFormatEnum.MediaType, -1);
						if (TcpStream.TRACESWITCH.TraceVerbose)
						{
							StackTrace stackTrace = new StackTrace();
							MethodBase method = stackTrace.GetFrame(1).GetMethod();
							Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpStream}::Write]: ")
								.Append('[')
								.Append('(')
								.Append(stackTrace.FrameCount)
								.Append(')')
								.Append(' ')
								.Append(base.GetType().Equals(method.DeclaringType) ? "." : method.DeclaringType.Name)
								.Append("::")
								.Append(method.Name)
								.Append("] ")
								.Append("DimeWriter initialized")
								.Append("..  ")
								.Append("; ")
								.ToString());
						}
					}
					this.dimeRecordForWrite.WriteBody(buffer, offset, size);
				}
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
			catch (SocketException ex2)
			{
				throw new XmlaStreamException(ex2);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00011284 File Offset: 0x0000F484
		public override void WriteEndOfMessage()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (TcpStream.TRACESWITCH.TraceVerbose)
			{
				StackTrace stackTrace = new StackTrace();
				MethodBase method = stackTrace.GetFrame(1).GetMethod();
				Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpStream}::WriteEndOfMessage]: ")
					.Append('[')
					.Append('(')
					.Append(stackTrace.FrameCount)
					.Append(')')
					.Append(' ')
					.Append(base.GetType().Equals(method.DeclaringType) ? "." : method.DeclaringType.Name)
					.Append("::")
					.Append(method.Name)
					.Append("] ")
					.Append("")
					.Append("..  ")
					.Append("; ")
					.ToString());
			}
			try
			{
				if (this.dimeWriter == null)
				{
					throw new InvalidOperationException();
				}
				this.dimeWriter.Close();
				this.dimeWriter = null;
				this.endOfStream = false;
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
			catch (SocketException ex2)
			{
				throw new XmlaStreamException(ex2);
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000113E8 File Offset: 0x0000F5E8
		public override XmlaDataType GetResponseDataType()
		{
			XmlaDataType xmlaDataType;
			try
			{
				if (this.endOfStream)
				{
					xmlaDataType = XmlaDataType.Undetermined;
				}
				else
				{
					if (this.dimeReader == null)
					{
						this.dimeReader = new DimeReader(this.bufferedStream);
						this.dimeRecordForRead = this.dimeReader.ReadRecord();
						if (TcpStream.TRACESWITCH.TraceVerbose)
						{
							StackTrace stackTrace = new StackTrace();
							MethodBase method = stackTrace.GetFrame(1).GetMethod();
							Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpStream}::GetResponseDataType]: ")
								.Append('[')
								.Append('(')
								.Append(stackTrace.FrameCount)
								.Append(')')
								.Append(' ')
								.Append(base.GetType().Equals(method.DeclaringType) ? "." : method.DeclaringType.Name)
								.Append("::")
								.Append(method.Name)
								.Append("] ")
								.Append("DimeReader initialized and new DimeRecord read")
								.Append("..  ")
								.Append("; ")
								.ToString());
						}
						this.DetermineNegotiatedOptions();
					}
					if (this.dimeRecordForRead == null)
					{
						if (TcpStream.TRACESWITCH.TraceVerbose)
						{
							StackTrace stackTrace2 = new StackTrace();
							MethodBase method2 = stackTrace2.GetFrame(1).GetMethod();
							Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpStream}::GetResponseDataType]: ")
								.Append('[')
								.Append('(')
								.Append(stackTrace2.FrameCount)
								.Append(')')
								.Append(' ')
								.Append(base.GetType().Equals(method2.DeclaringType) ? "." : method2.DeclaringType.Name)
								.Append("::")
								.Append(method2.Name)
								.Append("] ")
								.Append("DimeRecord for read is null. Closing underlying stream")
								.Append("..  ")
								.Append("; ")
								.ToString());
						}
						this.dimeReader.Close();
						this.dimeReader = null;
						this.endOfStream = true;
						xmlaDataType = XmlaDataType.Undetermined;
					}
					else
					{
						XmlaDataType dataTypeFromString = DataTypes.GetDataTypeFromString(this.dimeRecordForRead.Type);
						if (TcpStream.TRACESWITCH.TraceVerbose)
						{
							StackTrace stackTrace3 = new StackTrace();
							MethodBase method3 = stackTrace3.GetFrame(1).GetMethod();
							Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpStream}::GetResponseDataType]: ")
								.Append('[')
								.Append('(')
								.Append(stackTrace3.FrameCount)
								.Append(')')
								.Append(' ')
								.Append(base.GetType().Equals(method3.DeclaringType) ? "." : method3.DeclaringType.Name)
								.Append("::")
								.Append(method3.Name)
								.Append("] ")
								.Append("DataTypes.GetDataTypeFromString")
								.Append("..  ")
								.Append("Result = ")
								.Append(dataTypeFromString)
								.Append("; ")
								.ToString());
						}
						if (dataTypeFromString == XmlaDataType.Unknown)
						{
							throw new AdomdUnknownResponseException(XmlaSR.Dime_DataTypeNotSupported(this.dimeRecordForRead.Type), "");
						}
						xmlaDataType = dataTypeFromString;
					}
				}
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
			catch (SocketException ex2)
			{
				throw new XmlaStreamException(ex2);
			}
			return xmlaDataType;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000117C0 File Offset: 0x0000F9C0
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size + offset > buffer.Length)
			{
				throw new ArgumentException(XmlaSR.InvalidArgument, "buffer");
			}
			int num;
			try
			{
				if (size == 0 || this.endOfStream)
				{
					num = 0;
				}
				else
				{
					if (this.dimeReader == null)
					{
						this.dimeReader = new DimeReader(this.bufferedStream);
						this.dimeRecordForRead = this.dimeReader.ReadRecord();
						if (TcpStream.TRACESWITCH.TraceVerbose)
						{
							StackTrace stackTrace = new StackTrace();
							MethodBase method = stackTrace.GetFrame(1).GetMethod();
							Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpStream}::Read]: ")
								.Append('[')
								.Append('(')
								.Append(stackTrace.FrameCount)
								.Append(')')
								.Append(' ')
								.Append(base.GetType().Equals(method.DeclaringType) ? "." : method.DeclaringType.Name)
								.Append("::")
								.Append(method.Name)
								.Append("] ")
								.Append("DimeReader initialized and new DimeRecord read")
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
								.ToString());
						}
					}
					if (this.dimeRecordForRead == null)
					{
						if (TcpStream.TRACESWITCH.TraceVerbose)
						{
							StackTrace stackTrace2 = new StackTrace();
							MethodBase method2 = stackTrace2.GetFrame(1).GetMethod();
							Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpStream}::Read]: ")
								.Append('[')
								.Append('(')
								.Append(stackTrace2.FrameCount)
								.Append(')')
								.Append(' ')
								.Append(base.GetType().Equals(method2.DeclaringType) ? "." : method2.DeclaringType.Name)
								.Append("::")
								.Append(method2.Name)
								.Append("] ")
								.Append("DimeRecord for read is null. Closing underlying stream")
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
								.ToString());
						}
						this.dimeReader.Close();
						this.dimeReader = null;
						this.endOfStream = true;
						num = 0;
					}
					else
					{
						int num2 = this.dimeRecordForRead.ReadBody(buffer, offset, size);
						if (TcpStream.TRACESWITCH.TraceVerbose)
						{
							StackTrace stackTrace3 = new StackTrace();
							MethodBase method3 = stackTrace3.GetFrame(1).GetMethod();
							Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{TcpStream}::Read]: ")
								.Append('[')
								.Append('(')
								.Append(stackTrace3.FrameCount)
								.Append(')')
								.Append(' ')
								.Append(base.GetType().Equals(method3.DeclaringType) ? "." : method3.DeclaringType.Name)
								.Append("::")
								.Append(method3.Name)
								.Append("] ")
								.Append("DimeRecord ReadBody")
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
								.Append(num2)
								.Append(" bytes read")
								.Append("; ")
								.Append("EndOfRecord: ")
								.Append(num2 == 0)
								.Append("; ")
								.ToString());
						}
						if (num2 == 0)
						{
							this.dimeRecordForRead.Close();
							this.dimeReader.Close();
							this.dimeRecordForRead = null;
							this.dimeReader = null;
							this.endOfStream = true;
						}
						num = num2;
					}
				}
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
			catch (SocketException ex2)
			{
				throw new XmlaStreamException(ex2);
			}
			return num;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00011CE8 File Offset: 0x0000FEE8
		public override void Flush()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				this.bufferedStream.Flush();
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
			catch (SocketException ex2)
			{
				throw new XmlaStreamException(ex2);
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00011D4C File Offset: 0x0000FF4C
		public override void Skip()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (this.dimeReader != null)
			{
				while (0 < this.Read(TcpStream.BufferForSkip, 0, 8192))
				{
				}
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00011D79 File Offset: 0x0000FF79
		public override void Close()
		{
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00011D7C File Offset: 0x0000FF7C
		public override void Dispose()
		{
			try
			{
				this.disposed = true;
			}
			finally
			{
				base.Dispose(true);
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00011DAC File Offset: 0x0000FFAC
		private void DetermineNegotiatedOptions()
		{
			if (!base.NegotiatedOptions)
			{
				TransportCapabilities transportCapabilities = this.dimeReader.Options.Clone();
				transportCapabilities.ContentTypeNegotiated = true;
				base.SetTransportCapabilities(transportCapabilities);
			}
		}

		// Token: 0x0400022A RID: 554
		protected internal static readonly TraceSwitch TRACESWITCH = new TraceSwitch(typeof(TcpStream).FullName, typeof(TcpStream).FullName, TraceLevel.Off.ToString());

		// Token: 0x0400022B RID: 555
		private const int BufferSizeForSkip = 8192;

		// Token: 0x0400022C RID: 556
		private static byte[] BufferForSkip = new byte[8192];

		// Token: 0x0400022D RID: 557
		private DimeWriter dimeWriter;

		// Token: 0x0400022E RID: 558
		private DimeReader dimeReader;

		// Token: 0x0400022F RID: 559
		private DimeRecord dimeRecordForWrite;

		// Token: 0x04000230 RID: 560
		private DimeRecord dimeRecordForRead;

		// Token: 0x04000231 RID: 561
		private bool endOfStream = true;

		// Token: 0x04000232 RID: 562
		private int dimeChunkSize;

		// Token: 0x04000233 RID: 563
		private BufferedStream bufferedStream;
	}
}
