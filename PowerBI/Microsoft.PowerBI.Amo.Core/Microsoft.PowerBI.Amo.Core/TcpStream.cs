using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000052 RID: 82
	internal class TcpStream : TransportCapabilitiesAwareXmlaStream
	{
		// Token: 0x06000384 RID: 900 RVA: 0x000141A4 File Offset: 0x000123A4
		public TcpStream(BufferedStream bufferedStream, int packetSize, XmlaDataType desiredRequestType, XmlaDataType desiredResponseType)
			: base(false, desiredRequestType, desiredResponseType)
		{
			this.bufferedStream = bufferedStream;
			this.dimeChunkSize = packetSize;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000141C5 File Offset: 0x000123C5
		private protected TcpStream(TcpStream originalTcpStream)
			: base(false, originalTcpStream)
		{
			this.bufferedStream = originalTcpStream.bufferedStream;
			this.dimeChunkSize = originalTcpStream.dimeChunkSize;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000141F0 File Offset: 0x000123F0
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

		// Token: 0x06000387 RID: 903 RVA: 0x00014404 File Offset: 0x00012604
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

		// Token: 0x06000388 RID: 904 RVA: 0x00014568 File Offset: 0x00012768
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
							throw new ResponseFormatException(XmlaSR.Dime_DataTypeNotSupported(this.dimeRecordForRead.Type), "");
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

		// Token: 0x06000389 RID: 905 RVA: 0x00014940 File Offset: 0x00012B40
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

		// Token: 0x0600038A RID: 906 RVA: 0x00014E68 File Offset: 0x00013068
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

		// Token: 0x0600038B RID: 907 RVA: 0x00014ECC File Offset: 0x000130CC
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

		// Token: 0x0600038C RID: 908 RVA: 0x00014EF9 File Offset: 0x000130F9
		public override void Close()
		{
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00014EFC File Offset: 0x000130FC
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

		// Token: 0x0600038E RID: 910 RVA: 0x00014F2C File Offset: 0x0001312C
		private void DetermineNegotiatedOptions()
		{
			if (!base.NegotiatedOptions)
			{
				TransportCapabilities transportCapabilities = this.dimeReader.Options.Clone();
				transportCapabilities.ContentTypeNegotiated = true;
				base.SetTransportCapabilities(transportCapabilities);
			}
		}

		// Token: 0x0400027B RID: 635
		protected internal static readonly TraceSwitch TRACESWITCH = new TraceSwitch(typeof(TcpStream).FullName, typeof(TcpStream).FullName, TraceLevel.Off.ToString());

		// Token: 0x0400027C RID: 636
		private const int BufferSizeForSkip = 8192;

		// Token: 0x0400027D RID: 637
		private static byte[] BufferForSkip = new byte[8192];

		// Token: 0x0400027E RID: 638
		private DimeWriter dimeWriter;

		// Token: 0x0400027F RID: 639
		private DimeReader dimeReader;

		// Token: 0x04000280 RID: 640
		private DimeRecord dimeRecordForWrite;

		// Token: 0x04000281 RID: 641
		private DimeRecord dimeRecordForRead;

		// Token: 0x04000282 RID: 642
		private bool endOfStream = true;

		// Token: 0x04000283 RID: 643
		private int dimeChunkSize;

		// Token: 0x04000284 RID: 644
		private BufferedStream bufferedStream;
	}
}
