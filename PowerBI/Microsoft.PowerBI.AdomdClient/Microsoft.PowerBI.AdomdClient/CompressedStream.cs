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
		// Token: 0x060001D7 RID: 471 RVA: 0x00009394 File Offset: 0x00007594
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

		// Token: 0x060001D8 RID: 472 RVA: 0x000093EC File Offset: 0x000075EC
		~CompressedStream()
		{
			this.Dispose(false);
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000941C File Offset: 0x0000761C
		public static bool IsCompressionAvailable
		{
			get
			{
				return CompressedStream.XpressMethodsWrapper.XpressAvailable;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00009423 File Offset: 0x00007623
		public override bool CanTimeout
		{
			get
			{
				return this.baseXmlaStream.CanTimeout;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00009430 File Offset: 0x00007630
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000943D File Offset: 0x0000763D
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000944B File Offset: 0x0000764B
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00009458 File Offset: 0x00007658
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000946E File Offset: 0x0000766E
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000947B File Offset: 0x0000767B
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00009489 File Offset: 0x00007689
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00009496 File Offset: 0x00007696
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000094A4 File Offset: 0x000076A4
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000094B1 File Offset: 0x000076B1
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

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000094BF File Offset: 0x000076BF
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000094CC File Offset: 0x000076CC
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000094DA File Offset: 0x000076DA
		internal XmlaStream BaseXmlaStream
		{
			get
			{
				return this.baseXmlaStream;
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000094E4 File Offset: 0x000076E4
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

		// Token: 0x060001E9 RID: 489 RVA: 0x00009524 File Offset: 0x00007724
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

		// Token: 0x060001EA RID: 490 RVA: 0x00009564 File Offset: 0x00007764
		public override void WriteSoapActionHeader(string action)
		{
			this.baseXmlaStream.WriteSoapActionHeader(action);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009574 File Offset: 0x00007774
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

		// Token: 0x060001EC RID: 492 RVA: 0x00009678 File Offset: 0x00007878
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

		// Token: 0x060001ED RID: 493 RVA: 0x000096D0 File Offset: 0x000078D0
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

		// Token: 0x060001EE RID: 494 RVA: 0x00009710 File Offset: 0x00007910
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

		// Token: 0x060001EF RID: 495 RVA: 0x0000983C File Offset: 0x00007A3C
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

		// Token: 0x060001F0 RID: 496 RVA: 0x00009894 File Offset: 0x00007A94
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

		// Token: 0x060001F1 RID: 497 RVA: 0x000098BE File Offset: 0x00007ABE
		public override string GetExtendedErrorInfo()
		{
			return this.baseXmlaStream.GetExtendedErrorInfo();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000098CB File Offset: 0x00007ACB
		public override void Close()
		{
			this.baseXmlaStream.Close();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000098D8 File Offset: 0x00007AD8
		public override void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000098E4 File Offset: 0x00007AE4
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

		// Token: 0x060001F5 RID: 501 RVA: 0x00009958 File Offset: 0x00007B58
		private void InitCompress()
		{
			this.compressHandle = CompressedStream.XpressMethodsWrapper.XpressWrapper.CompressInit(65536, this.compressionLevel);
			if (this.compressHandle == IntPtr.Zero)
			{
				throw new XmlaStreamException(XmlaSR.Compression_InitializationFailed);
			}
			this.InitCompressionBuffers();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009998 File Offset: 0x00007B98
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

		// Token: 0x060001F7 RID: 503 RVA: 0x000099D8 File Offset: 0x00007BD8
		private bool CompressedWriteEnabled()
		{
			XmlaDataType requestDataType = this.baseXmlaStream.GetRequestDataType();
			return requestDataType == XmlaDataType.CompressedXml || requestDataType == XmlaDataType.CompressedBinaryXml;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000099FC File Offset: 0x00007BFC
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

		// Token: 0x060001F9 RID: 505 RVA: 0x00009AAC File Offset: 0x00007CAC
		private int ReadFromCache(byte[] buffer, int offset, int size)
		{
			int num = Math.Min((int)this.decompressedBufferSize, size);
			Buffer.BlockCopy(this.decompressedBuffer, (int)this.decompressedBufferOffset, buffer, offset, num);
			this.decompressedBufferSize -= (ushort)num;
			this.decompressedBufferOffset += (ushort)num;
			return num;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009AFC File Offset: 0x00007CFC
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

		// Token: 0x060001FB RID: 507 RVA: 0x00009C20 File Offset: 0x00007E20
		private void InitCompressionBuffers()
		{
			this.compressionHeader = new byte[8];
			this.compressedBuffer = new byte[65535];
			this.decompressedBuffer = new byte[65535];
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009C50 File Offset: 0x00007E50
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

		// Token: 0x0400019E RID: 414
		private const ushort WriteBufferFlushThreshold = 65535;

		// Token: 0x0400019F RID: 415
		private XmlaStream baseXmlaStream;

		// Token: 0x040001A0 RID: 416
		private IntPtr compressHandle = IntPtr.Zero;

		// Token: 0x040001A1 RID: 417
		private IntPtr decompressHandle = IntPtr.Zero;

		// Token: 0x040001A2 RID: 418
		private byte[] compressionHeader;

		// Token: 0x040001A3 RID: 419
		private byte[] compressedBuffer;

		// Token: 0x040001A4 RID: 420
		private byte[] decompressedBuffer;

		// Token: 0x040001A5 RID: 421
		private ushort decompressedBufferOffset;

		// Token: 0x040001A6 RID: 422
		private ushort decompressedBufferSize;

		// Token: 0x040001A7 RID: 423
		private ushort writeCacheOffset = 8;

		// Token: 0x040001A8 RID: 424
		private int compressionLevel;

		// Token: 0x0200017B RID: 379
		internal sealed class XpressMethodsWrapper : LibraryHandle
		{
			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x06001186 RID: 4486 RVA: 0x0003C966 File Offset: 0x0003AB66
			internal static CompressedStream.XpressMethodsWrapper XpressWrapper
			{
				get
				{
					return CompressedStream.XpressMethodsWrapper.wrapper;
				}
			}

			// Token: 0x06001187 RID: 4487 RVA: 0x0003C96D File Offset: 0x0003AB6D
			private XpressMethodsWrapper()
			{
			}

			// Token: 0x06001188 RID: 4488
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
			private static extern CompressedStream.XpressMethodsWrapper LoadLibrary([MarshalAs(UnmanagedType.LPTStr)] [In] string fileName);

			// Token: 0x06001189 RID: 4489 RVA: 0x0003C978 File Offset: 0x0003AB78
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

			// Token: 0x0600118A RID: 4490 RVA: 0x0003CA7C File Offset: 0x0003AC7C
			internal IntPtr CompressInit(int maxInputSize, int compressionLevel)
			{
				return LibraryHandle.CheckEmptyHandle(this.compressInitDelegate(maxInputSize, compressionLevel));
			}

			// Token: 0x0600118B RID: 4491 RVA: 0x0003CA90 File Offset: 0x0003AC90
			internal int Compress(IntPtr compressHandle, byte[] input, int inputOffset, int inputSize, byte[] output, int outputOffset, int outputSize)
			{
				return this.compressDelegate(compressHandle, input, inputOffset, inputSize, output, outputOffset, outputSize);
			}

			// Token: 0x0600118C RID: 4492 RVA: 0x0003CAA8 File Offset: 0x0003ACA8
			internal void CompressClose(IntPtr compressHandle)
			{
				this.compressCloseDelegate(compressHandle);
			}

			// Token: 0x0600118D RID: 4493 RVA: 0x0003CAB6 File Offset: 0x0003ACB6
			internal IntPtr DecompressInit()
			{
				return LibraryHandle.CheckEmptyHandle(this.decompressInitDelegate());
			}

			// Token: 0x0600118E RID: 4494 RVA: 0x0003CAC8 File Offset: 0x0003ACC8
			internal int Decompress(IntPtr decompressHandle, byte[] input, int inputSize, byte[] output, int outputSize, int bytesToDecompress)
			{
				return this.decompressDelegate(decompressHandle, input, inputSize, output, outputSize, bytesToDecompress);
			}

			// Token: 0x0600118F RID: 4495 RVA: 0x0003CADE File Offset: 0x0003ACDE
			internal void DecompressClose(IntPtr decompressHandle)
			{
				this.decompressCloseDelegate(decompressHandle);
			}

			// Token: 0x06001190 RID: 4496 RVA: 0x0003CAEC File Offset: 0x0003ACEC
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

			// Token: 0x04000BEF RID: 3055
			private const string XpressDllName = "msasxpress.dll";

			// Token: 0x04000BF0 RID: 3056
			private const string XpressSqlLocationTemplate = "{0}\\Microsoft SQL Server\\{1}\\shared";

			// Token: 0x04000BF1 RID: 3057
			private CompressedStream.XpressMethodsWrapper.CompressInitDelegate compressInitDelegate;

			// Token: 0x04000BF2 RID: 3058
			private CompressedStream.XpressMethodsWrapper.CompressDelegate compressDelegate;

			// Token: 0x04000BF3 RID: 3059
			private CompressedStream.XpressMethodsWrapper.CompressCloseDelegate compressCloseDelegate;

			// Token: 0x04000BF4 RID: 3060
			private CompressedStream.XpressMethodsWrapper.DecompressInitDelegate decompressInitDelegate;

			// Token: 0x04000BF5 RID: 3061
			private CompressedStream.XpressMethodsWrapper.DecompressDelegate decompressDelegate;

			// Token: 0x04000BF6 RID: 3062
			private CompressedStream.XpressMethodsWrapper.DecompressCloseDelegate decompressCloseDelegate;

			// Token: 0x04000BF7 RID: 3063
			private static readonly CompressedStream.XpressMethodsWrapper wrapper;

			// Token: 0x04000BF8 RID: 3064
			internal static readonly bool XpressAvailable = CompressedStream.XpressMethodsWrapper.CheckIfXpressAvailable(out CompressedStream.XpressMethodsWrapper.wrapper);

			// Token: 0x04000BF9 RID: 3065
			internal const int MaxBlock = 65536;

			// Token: 0x02000219 RID: 537
			// (Invoke) Token: 0x060014DE RID: 5342
			private delegate IntPtr CompressInitDelegate([In] int maxInputSize, [In] int compressionLevel);

			// Token: 0x0200021A RID: 538
			// (Invoke) Token: 0x060014E2 RID: 5346
			private delegate int CompressDelegate([In] IntPtr compressHandle, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] input, [In] int inputOffset, [In] int inputSize, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] output, [In] int outputOffset, [In] int outputSize);

			// Token: 0x0200021B RID: 539
			// (Invoke) Token: 0x060014E6 RID: 5350
			private delegate void CompressCloseDelegate([In] IntPtr compressHandle);

			// Token: 0x0200021C RID: 540
			// (Invoke) Token: 0x060014EA RID: 5354
			private delegate IntPtr DecompressInitDelegate();

			// Token: 0x0200021D RID: 541
			// (Invoke) Token: 0x060014EE RID: 5358
			private delegate int DecompressDelegate([In] IntPtr decompressHandle, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] input, [In] int inputSize, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] output, [In] int outputSize, [In] int bytesToDecompress);

			// Token: 0x0200021E RID: 542
			// (Invoke) Token: 0x060014F2 RID: 5362
			private delegate void DecompressCloseDelegate([In] IntPtr decompressHandle);
		}
	}
}
