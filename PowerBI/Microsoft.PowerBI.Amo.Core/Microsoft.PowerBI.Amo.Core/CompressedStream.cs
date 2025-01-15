using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000039 RID: 57
	internal sealed class CompressedStream : XmlaStream
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000C590 File Offset: 0x0000A790
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

		// Token: 0x06000271 RID: 625 RVA: 0x0000C5E8 File Offset: 0x0000A7E8
		~CompressedStream()
		{
			this.Dispose(false);
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000C618 File Offset: 0x0000A818
		public static bool IsCompressionAvailable
		{
			get
			{
				return CompressedStream.XpressMethodsWrapper.XpressAvailable;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000C61F File Offset: 0x0000A81F
		public override bool CanTimeout
		{
			get
			{
				return this.baseXmlaStream.CanTimeout;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000C62C File Offset: 0x0000A82C
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000C639 File Offset: 0x0000A839
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000C647 File Offset: 0x0000A847
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000C654 File Offset: 0x0000A854
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

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000C66A File Offset: 0x0000A86A
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000C677 File Offset: 0x0000A877
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000C685 File Offset: 0x0000A885
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000C692 File Offset: 0x0000A892
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000C6AD File Offset: 0x0000A8AD
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000C6BB File Offset: 0x0000A8BB
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
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

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000C6D6 File Offset: 0x0000A8D6
		internal XmlaStream BaseXmlaStream
		{
			get
			{
				return this.baseXmlaStream;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
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

		// Token: 0x06000282 RID: 642 RVA: 0x0000C720 File Offset: 0x0000A920
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

		// Token: 0x06000283 RID: 643 RVA: 0x0000C760 File Offset: 0x0000A960
		public override void WriteSoapActionHeader(string action)
		{
			this.baseXmlaStream.WriteSoapActionHeader(action);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C770 File Offset: 0x0000A970
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

		// Token: 0x06000285 RID: 645 RVA: 0x0000C874 File Offset: 0x0000AA74
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

		// Token: 0x06000286 RID: 646 RVA: 0x0000C8CC File Offset: 0x0000AACC
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

		// Token: 0x06000287 RID: 647 RVA: 0x0000C90C File Offset: 0x0000AB0C
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

		// Token: 0x06000288 RID: 648 RVA: 0x0000CA38 File Offset: 0x0000AC38
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

		// Token: 0x06000289 RID: 649 RVA: 0x0000CA90 File Offset: 0x0000AC90
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

		// Token: 0x0600028A RID: 650 RVA: 0x0000CABA File Offset: 0x0000ACBA
		public override string GetExtendedErrorInfo()
		{
			return this.baseXmlaStream.GetExtendedErrorInfo();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000CAC7 File Offset: 0x0000ACC7
		public override void Close()
		{
			this.baseXmlaStream.Close();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		public override void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
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

		// Token: 0x0600028E RID: 654 RVA: 0x0000CB54 File Offset: 0x0000AD54
		private void InitCompress()
		{
			this.compressHandle = CompressedStream.XpressMethodsWrapper.XpressWrapper.CompressInit(65536, this.compressionLevel);
			if (this.compressHandle == IntPtr.Zero)
			{
				throw new XmlaStreamException(XmlaSR.Compression_InitializationFailed);
			}
			this.InitCompressionBuffers();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000CB94 File Offset: 0x0000AD94
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

		// Token: 0x06000290 RID: 656 RVA: 0x0000CBD4 File Offset: 0x0000ADD4
		private bool CompressedWriteEnabled()
		{
			XmlaDataType requestDataType = this.baseXmlaStream.GetRequestDataType();
			return requestDataType == XmlaDataType.CompressedXml || requestDataType == XmlaDataType.CompressedBinaryXml;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000CBF8 File Offset: 0x0000ADF8
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

		// Token: 0x06000292 RID: 658 RVA: 0x0000CCA8 File Offset: 0x0000AEA8
		private int ReadFromCache(byte[] buffer, int offset, int size)
		{
			int num = Math.Min((int)this.decompressedBufferSize, size);
			Buffer.BlockCopy(this.decompressedBuffer, (int)this.decompressedBufferOffset, buffer, offset, num);
			this.decompressedBufferSize -= (ushort)num;
			this.decompressedBufferOffset += (ushort)num;
			return num;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
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
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, "Could not read all expected data");
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

		// Token: 0x06000294 RID: 660 RVA: 0x0000CE1C File Offset: 0x0000B01C
		private void InitCompressionBuffers()
		{
			this.compressionHeader = new byte[8];
			this.compressedBuffer = new byte[65535];
			this.decompressedBuffer = new byte[65535];
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000CE4C File Offset: 0x0000B04C
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

		// Token: 0x040001F0 RID: 496
		private const ushort WriteBufferFlushThreshold = 65535;

		// Token: 0x040001F1 RID: 497
		private XmlaStream baseXmlaStream;

		// Token: 0x040001F2 RID: 498
		private IntPtr compressHandle = IntPtr.Zero;

		// Token: 0x040001F3 RID: 499
		private IntPtr decompressHandle = IntPtr.Zero;

		// Token: 0x040001F4 RID: 500
		private byte[] compressionHeader;

		// Token: 0x040001F5 RID: 501
		private byte[] compressedBuffer;

		// Token: 0x040001F6 RID: 502
		private byte[] decompressedBuffer;

		// Token: 0x040001F7 RID: 503
		private ushort decompressedBufferOffset;

		// Token: 0x040001F8 RID: 504
		private ushort decompressedBufferSize;

		// Token: 0x040001F9 RID: 505
		private ushort writeCacheOffset = 8;

		// Token: 0x040001FA RID: 506
		private int compressionLevel;

		// Token: 0x02000177 RID: 375
		internal sealed class XpressMethodsWrapper : LibraryHandle
		{
			// Token: 0x1700060F RID: 1551
			// (get) Token: 0x06001231 RID: 4657 RVA: 0x0003F4F6 File Offset: 0x0003D6F6
			internal static CompressedStream.XpressMethodsWrapper XpressWrapper
			{
				get
				{
					return CompressedStream.XpressMethodsWrapper.wrapper;
				}
			}

			// Token: 0x06001232 RID: 4658 RVA: 0x0003F4FD File Offset: 0x0003D6FD
			private XpressMethodsWrapper()
			{
			}

			// Token: 0x06001233 RID: 4659
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
			private static extern CompressedStream.XpressMethodsWrapper LoadLibrary([MarshalAs(UnmanagedType.LPTStr)] [In] string fileName);

			// Token: 0x06001234 RID: 4660 RVA: 0x0003F508 File Offset: 0x0003D708
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

			// Token: 0x06001235 RID: 4661 RVA: 0x0003F60C File Offset: 0x0003D80C
			internal IntPtr CompressInit(int maxInputSize, int compressionLevel)
			{
				return LibraryHandle.CheckEmptyHandle(this.compressInitDelegate(maxInputSize, compressionLevel));
			}

			// Token: 0x06001236 RID: 4662 RVA: 0x0003F620 File Offset: 0x0003D820
			internal int Compress(IntPtr compressHandle, byte[] input, int inputOffset, int inputSize, byte[] output, int outputOffset, int outputSize)
			{
				return this.compressDelegate(compressHandle, input, inputOffset, inputSize, output, outputOffset, outputSize);
			}

			// Token: 0x06001237 RID: 4663 RVA: 0x0003F638 File Offset: 0x0003D838
			internal void CompressClose(IntPtr compressHandle)
			{
				this.compressCloseDelegate(compressHandle);
			}

			// Token: 0x06001238 RID: 4664 RVA: 0x0003F646 File Offset: 0x0003D846
			internal IntPtr DecompressInit()
			{
				return LibraryHandle.CheckEmptyHandle(this.decompressInitDelegate());
			}

			// Token: 0x06001239 RID: 4665 RVA: 0x0003F658 File Offset: 0x0003D858
			internal int Decompress(IntPtr decompressHandle, byte[] input, int inputSize, byte[] output, int outputSize, int bytesToDecompress)
			{
				return this.decompressDelegate(decompressHandle, input, inputSize, output, outputSize, bytesToDecompress);
			}

			// Token: 0x0600123A RID: 4666 RVA: 0x0003F66E File Offset: 0x0003D86E
			internal void DecompressClose(IntPtr decompressHandle)
			{
				this.decompressCloseDelegate(decompressHandle);
			}

			// Token: 0x0600123B RID: 4667 RVA: 0x0003F67C File Offset: 0x0003D87C
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

			// Token: 0x04000BBA RID: 3002
			private const string XpressDllName = "msasxpress.dll";

			// Token: 0x04000BBB RID: 3003
			private const string XpressSqlLocationTemplate = "{0}\\Microsoft SQL Server\\{1}\\shared";

			// Token: 0x04000BBC RID: 3004
			private CompressedStream.XpressMethodsWrapper.CompressInitDelegate compressInitDelegate;

			// Token: 0x04000BBD RID: 3005
			private CompressedStream.XpressMethodsWrapper.CompressDelegate compressDelegate;

			// Token: 0x04000BBE RID: 3006
			private CompressedStream.XpressMethodsWrapper.CompressCloseDelegate compressCloseDelegate;

			// Token: 0x04000BBF RID: 3007
			private CompressedStream.XpressMethodsWrapper.DecompressInitDelegate decompressInitDelegate;

			// Token: 0x04000BC0 RID: 3008
			private CompressedStream.XpressMethodsWrapper.DecompressDelegate decompressDelegate;

			// Token: 0x04000BC1 RID: 3009
			private CompressedStream.XpressMethodsWrapper.DecompressCloseDelegate decompressCloseDelegate;

			// Token: 0x04000BC2 RID: 3010
			private static readonly CompressedStream.XpressMethodsWrapper wrapper;

			// Token: 0x04000BC3 RID: 3011
			internal static readonly bool XpressAvailable = CompressedStream.XpressMethodsWrapper.CheckIfXpressAvailable(out CompressedStream.XpressMethodsWrapper.wrapper);

			// Token: 0x04000BC4 RID: 3012
			internal const int MaxBlock = 65536;

			// Token: 0x020001F8 RID: 504
			// (Invoke) Token: 0x06001446 RID: 5190
			private delegate IntPtr CompressInitDelegate([In] int maxInputSize, [In] int compressionLevel);

			// Token: 0x020001F9 RID: 505
			// (Invoke) Token: 0x0600144A RID: 5194
			private delegate int CompressDelegate([In] IntPtr compressHandle, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] input, [In] int inputOffset, [In] int inputSize, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] output, [In] int outputOffset, [In] int outputSize);

			// Token: 0x020001FA RID: 506
			// (Invoke) Token: 0x0600144E RID: 5198
			private delegate void CompressCloseDelegate([In] IntPtr compressHandle);

			// Token: 0x020001FB RID: 507
			// (Invoke) Token: 0x06001452 RID: 5202
			private delegate IntPtr DecompressInitDelegate();

			// Token: 0x020001FC RID: 508
			// (Invoke) Token: 0x06001456 RID: 5206
			private delegate int DecompressDelegate([In] IntPtr decompressHandle, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] input, [In] int inputSize, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] output, [In] int outputSize, [In] int bytesToDecompress);

			// Token: 0x020001FD RID: 509
			// (Invoke) Token: 0x0600145A RID: 5210
			private delegate void DecompressCloseDelegate([In] IntPtr decompressHandle);
		}
	}
}
