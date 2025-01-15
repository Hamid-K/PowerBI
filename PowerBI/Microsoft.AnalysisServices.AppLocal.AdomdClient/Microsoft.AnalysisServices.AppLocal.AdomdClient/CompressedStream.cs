using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000021 RID: 33
	internal sealed class CompressedStream : XmlaStream
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x00009694 File Offset: 0x00007894
		public CompressedStream(XmlaStream xmlaStream, int compressionLevel)
			: base(true)
		{
			try
			{
				this.baseXmlaStream = xmlaStream;
				this.compressionLevel = compressionLevel;
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(ex);
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000096EC File Offset: 0x000078EC
		~CompressedStream()
		{
			this.Dispose(false);
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000971C File Offset: 0x0000791C
		public static bool IsCompressionAvailable
		{
			get
			{
				return CompressedStream.XpressMethodsWrapper.XpressAvailable;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00009723 File Offset: 0x00007923
		public override bool CanTimeout
		{
			get
			{
				return this.baseXmlaStream.CanTimeout;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00009730 File Offset: 0x00007930
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000973D File Offset: 0x0000793D
		public override int ReadTimeout
		{
			get
			{
				return this.baseXmlaStream.ReadTimeout;
			}
			set
			{
				this.baseXmlaStream.ReadTimeout = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000974B File Offset: 0x0000794B
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00009758 File Offset: 0x00007958
		public override string SessionID
		{
			get
			{
				return this.baseXmlaStream.SessionID;
			}
			set
			{
				if (this.baseXmlaStream != null)
				{
					this.baseXmlaStream.SessionID = value;
				}
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000976E File Offset: 0x0000796E
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000977B File Offset: 0x0000797B
		public override bool IsSessionTokenNeeded
		{
			get
			{
				return this.baseXmlaStream.IsSessionTokenNeeded;
			}
			set
			{
				this.baseXmlaStream.IsSessionTokenNeeded = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00009789 File Offset: 0x00007989
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00009796 File Offset: 0x00007996
		public override bool IsCompressionEnabled
		{
			get
			{
				return this.baseXmlaStream.IsCompressionEnabled;
			}
			set
			{
				this.baseXmlaStream.IsCompressionEnabled = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000097A4 File Offset: 0x000079A4
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x000097B1 File Offset: 0x000079B1
		public override Guid ActivityID
		{
			get
			{
				return this.baseXmlaStream.ActivityID;
			}
			set
			{
				this.baseXmlaStream.ActivityID = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000097BF File Offset: 0x000079BF
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x000097CC File Offset: 0x000079CC
		public override Guid RequestID
		{
			get
			{
				return this.baseXmlaStream.RequestID;
			}
			set
			{
				this.baseXmlaStream.RequestID = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000097DA File Offset: 0x000079DA
		internal XmlaStream BaseXmlaStream
		{
			get
			{
				return this.baseXmlaStream;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000097E4 File Offset: 0x000079E4
		public void SetBaseXmlaStream(XmlaStream xmlaStream)
		{
			try
			{
				if (this.baseXmlaStream != xmlaStream)
				{
					this.baseXmlaStream.Dispose();
					this.baseXmlaStream = xmlaStream;
				}
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(ex);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009824 File Offset: 0x00007A24
		public override XmlaDataType GetRequestDataType()
		{
			XmlaDataType xmlaDataType;
			try
			{
				XmlaDataType requestDataType = this.baseXmlaStream.GetRequestDataType();
				if (requestDataType == XmlaDataType.BinaryXml || requestDataType == XmlaDataType.CompressedBinaryXml)
				{
					xmlaDataType = XmlaDataType.BinaryXml;
				}
				else
				{
					xmlaDataType = XmlaDataType.TextXml;
				}
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(ex);
			}
			return xmlaDataType;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009864 File Offset: 0x00007A64
		public override void WriteSoapActionHeader(string action)
		{
			this.baseXmlaStream.WriteSoapActionHeader(action);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009874 File Offset: 0x00007A74
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
			if (!this.CompressedWriteEnabled())
			{
				this.baseXmlaStream.Write(buffer, offset, size);
				return;
			}
			try
			{
				if (this.compressHandle == IntPtr.Zero)
				{
					this.InitCompress();
				}
				int i = size;
				int num = offset;
				while (i > 0)
				{
					if (this.writeCacheOffset >= 65535)
					{
						this.FlushCache();
					}
					ushort num2 = (ushort)Math.Min((int)(ushort.MaxValue - this.writeCacheOffset), i);
					Buffer.BlockCopy(buffer, num, this.decompressedBuffer, (int)this.writeCacheOffset, (int)num2);
					this.writeCacheOffset += num2;
					num += (int)num2;
					i -= (int)num2;
				}
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(ex);
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009978 File Offset: 0x00007B78
		public override void WriteEndOfMessage()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				if (this.CompressedWriteEnabled() && this.writeCacheOffset > 8)
				{
					this.FlushCache();
				}
				this.baseXmlaStream.WriteEndOfMessage();
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(ex);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000099D0 File Offset: 0x00007BD0
		public override XmlaDataType GetResponseDataType()
		{
			XmlaDataType xmlaDataType;
			try
			{
				XmlaDataType responseDataType = this.baseXmlaStream.GetResponseDataType();
				if (responseDataType == XmlaDataType.BinaryXml || responseDataType == XmlaDataType.CompressedBinaryXml)
				{
					xmlaDataType = XmlaDataType.BinaryXml;
				}
				else
				{
					xmlaDataType = XmlaDataType.TextXml;
				}
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(ex);
			}
			return xmlaDataType;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009A10 File Offset: 0x00007C10
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
			if (size == 0)
			{
				return 0;
			}
			int num2;
			try
			{
				int num = 0;
				if (this.decompressedBufferSize > 0)
				{
					num = this.ReadFromCache(buffer, offset, size);
				}
				else
				{
					XmlaDataType responseDataType = this.baseXmlaStream.GetResponseDataType();
					switch (responseDataType)
					{
					case XmlaDataType.Undetermined:
						break;
					case XmlaDataType.TextXml:
					case XmlaDataType.BinaryXml:
						num = this.baseXmlaStream.Read(buffer, offset, size);
						break;
					case XmlaDataType.CompressedXml:
					case XmlaDataType.CompressedBinaryXml:
						if (this.decompressHandle == IntPtr.Zero)
						{
							this.InitDecompress();
						}
						while (this.decompressedBufferSize <= 0 && this.ReadCompressedPacket())
						{
						}
						if (this.decompressedBufferSize > 0)
						{
							num = this.ReadFromCache(buffer, offset, size);
						}
						break;
					default:
						throw new NotImplementedException(XmlaSR.UnsupportedDataFormat(responseDataType.ToString()));
					}
				}
				num2 = num;
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(ex);
			}
			return num2;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009B3C File Offset: 0x00007D3C
		public override void Flush()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				if (this.CompressedWriteEnabled() && this.writeCacheOffset > 8)
				{
					this.FlushCache();
				}
				this.baseXmlaStream.Flush();
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(ex);
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009B94 File Offset: 0x00007D94
		public override void Skip()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			this.baseXmlaStream.Skip();
			this.decompressedBufferOffset = 0;
			this.decompressedBufferSize = 0;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009BBE File Offset: 0x00007DBE
		public override string GetExtendedErrorInfo()
		{
			return this.baseXmlaStream.GetExtendedErrorInfo();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009BCB File Offset: 0x00007DCB
		public override void Close()
		{
			this.baseXmlaStream.Close();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009BD8 File Offset: 0x00007DD8
		public override void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00009BE4 File Offset: 0x00007DE4
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			try
			{
				this.CloseCompressionHandlesAndBuffers();
				if (disposing)
				{
					this.baseXmlaStream.Dispose();
				}
				this.disposed = true;
				if (disposing)
				{
					GC.SuppressFinalize(this);
				}
			}
			catch (IOException)
			{
			}
			catch (Win32Exception)
			{
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009C58 File Offset: 0x00007E58
		private void InitCompress()
		{
			this.compressHandle = CompressedStream.XpressMethodsWrapper.XpressWrapper.CompressInit(65536, this.compressionLevel);
			if (this.compressHandle == IntPtr.Zero)
			{
				throw new XmlaStreamException(XmlaSR.Compression_InitializationFailed);
			}
			this.InitCompressionBuffers();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009C98 File Offset: 0x00007E98
		private void InitDecompress()
		{
			this.decompressHandle = CompressedStream.XpressMethodsWrapper.XpressWrapper.DecompressInit();
			if (this.decompressHandle == IntPtr.Zero)
			{
				throw new XmlaStreamException(XmlaSR.Decompression_InitializationFailed);
			}
			if (this.decompressedBuffer == null)
			{
				this.InitCompressionBuffers();
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009CD8 File Offset: 0x00007ED8
		private bool CompressedWriteEnabled()
		{
			XmlaDataType requestDataType = this.baseXmlaStream.GetRequestDataType();
			return requestDataType == XmlaDataType.CompressedXml || requestDataType == XmlaDataType.CompressedBinaryXml;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009CFC File Offset: 0x00007EFC
		private void FlushCache()
		{
			int num = (int)(this.writeCacheOffset - 8);
			int num2 = CompressedStream.XpressMethodsWrapper.XpressWrapper.Compress(this.compressHandle, this.decompressedBuffer, 8, num, this.compressedBuffer, 8, num);
			byte[] array;
			int num3;
			if (0 < num2 && num2 < num)
			{
				array = this.compressedBuffer;
				num3 = num2 + 8;
			}
			else
			{
				array = this.decompressedBuffer;
				num3 = (int)this.writeCacheOffset;
			}
			array[0] = (byte)(num & 255);
			array[1] = (byte)((num >> 8) & 255);
			array[2] = 0;
			array[3] = 0;
			array[4] = (byte)(num2 & 255);
			array[5] = (byte)((num2 >> 8) & 255);
			array[6] = 0;
			array[7] = 0;
			this.baseXmlaStream.Write(array, 0, num3);
			this.writeCacheOffset = 8;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00009DAC File Offset: 0x00007FAC
		private int ReadFromCache(byte[] buffer, int offset, int size)
		{
			int num = Math.Min((int)this.decompressedBufferSize, size);
			Buffer.BlockCopy(this.decompressedBuffer, (int)this.decompressedBufferOffset, buffer, offset, num);
			this.decompressedBufferSize -= (ushort)num;
			this.decompressedBufferOffset += (ushort)num;
			return num;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00009DFC File Offset: 0x00007FFC
		private bool ReadCompressedPacket()
		{
			int i = 0;
			while (i < 8)
			{
				int num = this.baseXmlaStream.Read(this.compressionHeader, i, 8 - i);
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
			ushort num2 = (ushort)((int)this.compressionHeader[0] + ((int)this.compressionHeader[1] << 8));
			ushort num3 = (ushort)((int)this.compressionHeader[4] + ((int)this.compressionHeader[5] << 8));
			bool flag = 0 < num3 && num3 < num2;
			ushort num4 = (flag ? num3 : num2);
			byte[] array = (flag ? this.compressedBuffer : this.decompressedBuffer);
			int num5;
			for (int j = 0; j < (int)num4; j += num5)
			{
				num5 = this.baseXmlaStream.Read(array, j, (int)num4 - j);
				if (num5 == 0)
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, "Could not read all expected data");
				}
			}
			if (flag)
			{
				int num6 = CompressedStream.XpressMethodsWrapper.XpressWrapper.Decompress(this.decompressHandle, this.compressedBuffer, (int)num3, this.decompressedBuffer, (int)num2, (int)num2);
				if (num6 != (int)num2)
				{
					throw new XmlaStreamException(XmlaSR.Decompression_Failed((int)num3, (int)num2, num6));
				}
			}
			this.decompressedBufferOffset = 0;
			this.decompressedBufferSize = num2;
			return true;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009F20 File Offset: 0x00008120
		private void InitCompressionBuffers()
		{
			this.compressionHeader = new byte[8];
			this.compressedBuffer = new byte[65535];
			this.decompressedBuffer = new byte[65535];
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009F50 File Offset: 0x00008150
		private void CloseCompressionHandlesAndBuffers()
		{
			if (this.compressHandle != IntPtr.Zero)
			{
				CompressedStream.XpressMethodsWrapper.XpressWrapper.CompressClose(this.compressHandle);
				this.compressHandle = IntPtr.Zero;
			}
			if (this.decompressHandle != IntPtr.Zero)
			{
				CompressedStream.XpressMethodsWrapper.XpressWrapper.DecompressClose(this.decompressHandle);
				this.decompressHandle = IntPtr.Zero;
			}
			this.compressionHeader = null;
			this.compressedBuffer = null;
			this.decompressedBuffer = null;
			this.decompressedBufferOffset = 0;
			this.decompressedBufferSize = 0;
		}

		// Token: 0x040001AB RID: 427
		private const ushort WriteBufferFlushThreshold = 65535;

		// Token: 0x040001AC RID: 428
		private XmlaStream baseXmlaStream;

		// Token: 0x040001AD RID: 429
		private IntPtr compressHandle = IntPtr.Zero;

		// Token: 0x040001AE RID: 430
		private IntPtr decompressHandle = IntPtr.Zero;

		// Token: 0x040001AF RID: 431
		private byte[] compressionHeader;

		// Token: 0x040001B0 RID: 432
		private byte[] compressedBuffer;

		// Token: 0x040001B1 RID: 433
		private byte[] decompressedBuffer;

		// Token: 0x040001B2 RID: 434
		private ushort decompressedBufferOffset;

		// Token: 0x040001B3 RID: 435
		private ushort decompressedBufferSize;

		// Token: 0x040001B4 RID: 436
		private ushort writeCacheOffset = 8;

		// Token: 0x040001B5 RID: 437
		private int compressionLevel;

		// Token: 0x0200017B RID: 379
		internal sealed class XpressMethodsWrapper : LibraryHandle
		{
			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x06001193 RID: 4499 RVA: 0x0003CC96 File Offset: 0x0003AE96
			internal static CompressedStream.XpressMethodsWrapper XpressWrapper
			{
				get
				{
					return CompressedStream.XpressMethodsWrapper.wrapper;
				}
			}

			// Token: 0x06001194 RID: 4500 RVA: 0x0003CC9D File Offset: 0x0003AE9D
			private XpressMethodsWrapper()
			{
			}

			// Token: 0x06001195 RID: 4501
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
			private static extern CompressedStream.XpressMethodsWrapper LoadLibrary([MarshalAs(UnmanagedType.LPTStr)] [In] string fileName);

			// Token: 0x06001196 RID: 4502 RVA: 0x0003CCA8 File Offset: 0x0003AEA8
			private void SetDelegates()
			{
				if (this.IsInvalid)
				{
					throw new Win32Exception(Marshal.GetHRForLastWin32Error());
				}
				try
				{
					this.compressInitDelegate = (CompressedStream.XpressMethodsWrapper.CompressInitDelegate)base.GetDelegate("CompressInit", typeof(CompressedStream.XpressMethodsWrapper.CompressInitDelegate));
					this.compressDelegate = (CompressedStream.XpressMethodsWrapper.CompressDelegate)base.GetDelegate("Compress", typeof(CompressedStream.XpressMethodsWrapper.CompressDelegate));
					this.compressCloseDelegate = (CompressedStream.XpressMethodsWrapper.CompressCloseDelegate)base.GetDelegate("CompressClose", typeof(CompressedStream.XpressMethodsWrapper.CompressCloseDelegate));
					this.decompressInitDelegate = (CompressedStream.XpressMethodsWrapper.DecompressInitDelegate)base.GetDelegate("DecompressInit", typeof(CompressedStream.XpressMethodsWrapper.DecompressInitDelegate));
					this.decompressDelegate = (CompressedStream.XpressMethodsWrapper.DecompressDelegate)base.GetDelegate("Decompress", typeof(CompressedStream.XpressMethodsWrapper.DecompressDelegate));
					this.decompressCloseDelegate = (CompressedStream.XpressMethodsWrapper.DecompressCloseDelegate)base.GetDelegate("DecompressClose", typeof(CompressedStream.XpressMethodsWrapper.DecompressCloseDelegate));
				}
				catch
				{
					base.Close();
					base.SetHandleAsInvalid();
					throw;
				}
			}

			// Token: 0x06001197 RID: 4503 RVA: 0x0003CDAC File Offset: 0x0003AFAC
			internal IntPtr CompressInit(int maxInputSize, int compressionLevel)
			{
				return LibraryHandle.CheckEmptyHandle(this.compressInitDelegate(maxInputSize, compressionLevel));
			}

			// Token: 0x06001198 RID: 4504 RVA: 0x0003CDC0 File Offset: 0x0003AFC0
			internal int Compress(IntPtr compressHandle, byte[] input, int inputOffset, int inputSize, byte[] output, int outputOffset, int outputSize)
			{
				return this.compressDelegate(compressHandle, input, inputOffset, inputSize, output, outputOffset, outputSize);
			}

			// Token: 0x06001199 RID: 4505 RVA: 0x0003CDD8 File Offset: 0x0003AFD8
			internal void CompressClose(IntPtr compressHandle)
			{
				this.compressCloseDelegate(compressHandle);
			}

			// Token: 0x0600119A RID: 4506 RVA: 0x0003CDE6 File Offset: 0x0003AFE6
			internal IntPtr DecompressInit()
			{
				return LibraryHandle.CheckEmptyHandle(this.decompressInitDelegate());
			}

			// Token: 0x0600119B RID: 4507 RVA: 0x0003CDF8 File Offset: 0x0003AFF8
			internal int Decompress(IntPtr decompressHandle, byte[] input, int inputSize, byte[] output, int outputSize, int bytesToDecompress)
			{
				return this.decompressDelegate(decompressHandle, input, inputSize, output, outputSize, bytesToDecompress);
			}

			// Token: 0x0600119C RID: 4508 RVA: 0x0003CE0E File Offset: 0x0003B00E
			internal void DecompressClose(IntPtr decompressHandle)
			{
				this.decompressCloseDelegate(decompressHandle);
			}

			// Token: 0x0600119D RID: 4509 RVA: 0x0003CE1C File Offset: 0x0003B01C
			private static bool CheckIfXpressAvailable(out CompressedStream.XpressMethodsWrapper wrapper)
			{
				wrapper = null;
				string text = null;
				string location = Assembly.GetExecutingAssembly().Location;
				if (!string.IsNullOrEmpty(location))
				{
					text = Path.Combine(Path.GetDirectoryName(location), "msasxpress.dll");
				}
				if (string.IsNullOrEmpty(text) || !File.Exists(text))
				{
					text = Path.Combine(string.Format("{0}\\Microsoft SQL Server\\{1}\\shared", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "160"), "msasxpress.dll");
				}
				if (!File.Exists(text))
				{
					return false;
				}
				bool flag;
				try
				{
					wrapper = CompressedStream.XpressMethodsWrapper.LoadLibrary(text);
					wrapper.SetDelegates();
					flag = !wrapper.IsInvalid;
				}
				catch (Exception)
				{
					wrapper = null;
					flag = false;
				}
				return flag;
			}

			// Token: 0x04000BFF RID: 3071
			private const string XpressDllName = "msasxpress.dll";

			// Token: 0x04000C00 RID: 3072
			private const string XpressSqlLocationTemplate = "{0}\\Microsoft SQL Server\\{1}\\shared";

			// Token: 0x04000C01 RID: 3073
			private CompressedStream.XpressMethodsWrapper.CompressInitDelegate compressInitDelegate;

			// Token: 0x04000C02 RID: 3074
			private CompressedStream.XpressMethodsWrapper.CompressDelegate compressDelegate;

			// Token: 0x04000C03 RID: 3075
			private CompressedStream.XpressMethodsWrapper.CompressCloseDelegate compressCloseDelegate;

			// Token: 0x04000C04 RID: 3076
			private CompressedStream.XpressMethodsWrapper.DecompressInitDelegate decompressInitDelegate;

			// Token: 0x04000C05 RID: 3077
			private CompressedStream.XpressMethodsWrapper.DecompressDelegate decompressDelegate;

			// Token: 0x04000C06 RID: 3078
			private CompressedStream.XpressMethodsWrapper.DecompressCloseDelegate decompressCloseDelegate;

			// Token: 0x04000C07 RID: 3079
			private static readonly CompressedStream.XpressMethodsWrapper wrapper;

			// Token: 0x04000C08 RID: 3080
			internal static readonly bool XpressAvailable = CompressedStream.XpressMethodsWrapper.CheckIfXpressAvailable(out CompressedStream.XpressMethodsWrapper.wrapper);

			// Token: 0x04000C09 RID: 3081
			internal const int MaxBlock = 65536;

			// Token: 0x02000219 RID: 537
			// (Invoke) Token: 0x060014EB RID: 5355
			private delegate IntPtr CompressInitDelegate([In] int maxInputSize, [In] int compressionLevel);

			// Token: 0x0200021A RID: 538
			// (Invoke) Token: 0x060014EF RID: 5359
			private delegate int CompressDelegate([In] IntPtr compressHandle, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] input, [In] int inputOffset, [In] int inputSize, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] output, [In] int outputOffset, [In] int outputSize);

			// Token: 0x0200021B RID: 539
			// (Invoke) Token: 0x060014F3 RID: 5363
			private delegate void CompressCloseDelegate([In] IntPtr compressHandle);

			// Token: 0x0200021C RID: 540
			// (Invoke) Token: 0x060014F7 RID: 5367
			private delegate IntPtr DecompressInitDelegate();

			// Token: 0x0200021D RID: 541
			// (Invoke) Token: 0x060014FB RID: 5371
			private delegate int DecompressDelegate([In] IntPtr decompressHandle, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] input, [In] int inputSize, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] output, [In] int outputSize, [In] int bytesToDecompress);

			// Token: 0x0200021E RID: 542
			// (Invoke) Token: 0x060014FF RID: 5375
			private delegate void DecompressCloseDelegate([In] IntPtr decompressHandle);
		}
	}
}
