using System;
using System.IO;
using System.Text;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000024 RID: 36
	internal class DimeRecord
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000A208 File Offset: 0x00008408
		public DimeRecord(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(XmlaSR.DimeRecord_StreamShouldBeReadable, "stream");
			}
			this.m_ioMode = DimeRecord.IOModeEnum.ReadOnly;
			this.m_stream = stream;
			this.m_firstChunk = true;
			this.ReadHeader();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A268 File Offset: 0x00008468
		public DimeRecord(Stream stream, Uri id, string type, TypeFormatEnum typeFormat, bool beginOfMessage, int contentLength, int chunkSize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(XmlaSR.DimeRecord_StreamShouldBeWriteable, "stream");
			}
			if (contentLength < -1)
			{
				throw new ArgumentException(XmlaSR.DimeRecord_InvalidContentLength, "contentLength");
			}
			this.SetType(type, typeFormat);
			this.m_id = id;
			this.m_contentLength = contentLength;
			this.m_chunked = contentLength == -1;
			this.m_firstChunk = this.m_chunked;
			this.m_beginOfMessage = beginOfMessage;
			this.m_stream = stream;
			this.m_ioMode = DimeRecord.IOModeEnum.WriteOnly;
			this.m_chunkSize = chunkSize;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000A31C File Offset: 0x0000851C
		public bool CanRead
		{
			get
			{
				return this.m_ioMode == DimeRecord.IOModeEnum.ReadOnly && !this.m_closed;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000A334 File Offset: 0x00008534
		public bool CanWrite
		{
			get
			{
				return this.m_ioMode == DimeRecord.IOModeEnum.WriteOnly && !this.m_closed;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000A34C File Offset: 0x0000854C
		public int ChunkSize
		{
			get
			{
				return this.m_chunkSize;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000A354 File Offset: 0x00008554
		public bool EndOfMessage
		{
			get
			{
				if (this.CanWrite)
				{
					throw new InvalidOperationException(XmlaSR.DimeRecord_PropertyOnlyAvailableForReadRecords);
				}
				return this.m_endOfMessage;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000A36F File Offset: 0x0000856F
		public string Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000A377 File Offset: 0x00008577
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000A37F File Offset: 0x0000857F
		public TransportCapabilities Options
		{
			get
			{
				return this.m_Options;
			}
			set
			{
				this.m_Options = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000A388 File Offset: 0x00008588
		public TypeFormatEnum TypeFormat
		{
			get
			{
				return this.m_typeFormat;
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A390 File Offset: 0x00008590
		public int ReadBody(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (this.m_closed)
			{
				throw new Exception(XmlaSR.DimeRecord_StreamIsClosed);
			}
			if (this.m_ioMode != DimeRecord.IOModeEnum.ReadOnly)
			{
				throw new InvalidOperationException(XmlaSR.DimeRecord_ReadNotAllowed);
			}
			int num = 0;
			int num2 = this.m_contentLength - this.m_bytesReadWritten;
			if (this.m_chunked && num2 == 0)
			{
				do
				{
					this.ReadHeader();
				}
				while (this.m_contentLength == 0 && this.m_chunked);
				num2 = this.m_contentLength;
				this.m_bytesReadWritten = 0;
			}
			if (num2 > 0)
			{
				num = this.m_stream.Read(buffer, offset, (num2 < count) ? num2 : count);
				this.m_bytesReadWritten += num;
				if (this.m_bytesReadWritten == this.m_contentLength)
				{
					DimeRecord.ReadPadding(this.m_stream, this.m_bytesReadWritten);
				}
			}
			return num;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000A46C File Offset: 0x0000866C
		internal void WriteBody(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0 || offset < 0)
			{
				throw new ArgumentOutOfRangeException(XmlaSR.DimeRecord_OffsetAndCountShouldBePositive);
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException(XmlaSR.DimeRecord_StreamIsClosed);
			}
			if (this.m_ioMode != DimeRecord.IOModeEnum.WriteOnly)
			{
				throw new InvalidOperationException(XmlaSR.DimeRecord_WriteNotAllowed);
			}
			if (this.m_chunked)
			{
				if (this.m_bytesReadWritten + count >= this.ChunkSize)
				{
					this.WriteChunkedPayload(false, false, buffer, offset, count);
					return;
				}
				if (this.m_bodyStreamBuffer == null)
				{
					this.m_bodyStreamBuffer = new MemoryStream((count < 512) ? 512 : count);
				}
				this.m_bodyStreamBuffer.Write(buffer, offset, count);
				this.m_bytesReadWritten += count;
				return;
			}
			else
			{
				if (this.m_bytesReadWritten + count > this.m_contentLength)
				{
					throw new Exception(XmlaSR.DimeRecord_ContentLengthExceeded);
				}
				if (!this.m_headerWritten)
				{
					this.WriteHeader(false, false, (long)this.m_contentLength);
					this.m_headerWritten = true;
				}
				this.m_stream.Write(buffer, offset, count);
				this.m_bytesReadWritten += count;
				if (this.m_bytesReadWritten == this.m_contentLength)
				{
					DimeRecord.WritePadding(this.m_stream, this.m_bytesReadWritten);
				}
				return;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000A599 File Offset: 0x00008799
		public void Close()
		{
			this.Close(false);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A5A4 File Offset: 0x000087A4
		internal void Close(bool endOfMessage)
		{
			if (!this.m_closed)
			{
				this.m_closed = true;
				if (this.m_ioMode == DimeRecord.IOModeEnum.ReadOnly)
				{
					if (this.m_bytesReadWritten == this.m_contentLength)
					{
						return;
					}
					byte[] array = new byte[this.m_contentLength - this.m_bytesReadWritten];
					while (this.m_stream.Read(array, 0, array.Length) > 0)
					{
					}
					return;
				}
				else if (this.m_ioMode == DimeRecord.IOModeEnum.WriteOnly)
				{
					if (this.m_chunked)
					{
						this.WriteChunkedPayload(true, endOfMessage);
						return;
					}
					if (endOfMessage)
					{
						this.WriteMessageEndRecord();
					}
				}
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A624 File Offset: 0x00008824
		private static void ForceRead(Stream stream, byte[] buffer, int length)
		{
			int num;
			for (int i = 0; i < length; i += num)
			{
				num = stream.Read(buffer, i, length - i);
				if (num == 0)
				{
					throw new IOException(XmlaSR.DimeRecord_UnableToReadFromStream);
				}
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A658 File Offset: 0x00008858
		private static void WritePadding(Stream stream, int bytesWritten)
		{
			int num = DimeRecord.PaddedCount(bytesWritten) - bytesWritten;
			for (int i = 0; i < num; i++)
			{
				stream.WriteByte(0);
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A684 File Offset: 0x00008884
		private static void ReadPadding(Stream stream, int bytesRead)
		{
			int num = DimeRecord.PaddedCount(bytesRead) - bytesRead;
			for (int i = 0; i < num; i++)
			{
				stream.ReadByte();
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A6AD File Offset: 0x000088AD
		private static int RoundUp(int length)
		{
			return (length + 3) & -4;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A6B5 File Offset: 0x000088B5
		private static int PaddedCount(int byteCount)
		{
			return byteCount + ((byteCount % 4 > 0) ? (4 - byteCount % 4) : 0);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A6C8 File Offset: 0x000088C8
		private void SetType(string type, TypeFormatEnum typeFormat)
		{
			switch (typeFormat)
			{
			case TypeFormatEnum.Unchanged:
				throw new ArgumentException(XmlaSR.DimeRecord_TypeFormatEnumUnchangedNotAllowed, "typeFormat");
			case TypeFormatEnum.MediaType:
				if (type == null || type.Length == 0)
				{
					throw new ArgumentException(XmlaSR.DimeRecord_MediaTypeNotDefined, "type");
				}
				break;
			case TypeFormatEnum.None:
				if (type != null || type.Length != 0)
				{
					throw new ArgumentException(XmlaSR.DimeRecord_NameMustNotBeDefinedForFormatNone, "type");
				}
				break;
			}
			this.m_typeFormat = typeFormat;
			this.m_type = type;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A748 File Offset: 0x00008948
		private void ReadHeader()
		{
			byte[] array = new byte[12];
			DimeRecord.ForceRead(this.m_stream, array, 12);
			this.m_version = (byte)((array[0] & 248) >> 3);
			if (this.m_version != 1)
			{
				throw new AdomdUnknownResponseException(XmlaSR.DimeRecord_VersionNotSupported((int)this.m_version), "");
			}
			DimeRecord.HeaderFlagsEnum headerFlagsEnum = (DimeRecord.HeaderFlagsEnum)(array[0] & 7);
			this.m_chunked = (headerFlagsEnum & DimeRecord.HeaderFlagsEnum.ChunkedRecord) > (DimeRecord.HeaderFlagsEnum)0;
			this.m_beginOfMessage = (headerFlagsEnum & DimeRecord.HeaderFlagsEnum.BeginOfMessage) > (DimeRecord.HeaderFlagsEnum)0;
			this.m_endOfMessage = (headerFlagsEnum & DimeRecord.HeaderFlagsEnum.EndOfMessage) > (DimeRecord.HeaderFlagsEnum)0;
			if (this.m_chunked && this.m_endOfMessage)
			{
				throw new AdomdUnknownResponseException(XmlaSR.DimeRecord_InvalidHeaderFlags((this.m_beginOfMessage > false) ? 1 : 0, 1, 1), "");
			}
			if ((!this.m_chunked && !this.m_endOfMessage) || (!this.m_firstChunk && this.m_beginOfMessage))
			{
				throw new AdomdUnknownResponseException(XmlaSR.DimeRecord_OnlySingleRecordMessagesAreSupported, "");
			}
			this.m_typeFormat = (TypeFormatEnum)((array[1] & 240) >> 4);
			this.m_reserved = array[1] & 15;
			int num = ((int)array[2] << 8) + (int)array[3];
			int num2 = ((int)array[4] << 8) + (int)array[5];
			int num3 = ((int)array[6] << 8) + (int)array[7];
			this.m_contentLength = ((int)array[8] << 24) + ((int)array[9] << 16) + ((int)array[10] << 8) + (int)array[11];
			if (this.m_firstChunk)
			{
				if (this.m_typeFormat != TypeFormatEnum.MediaType)
				{
					throw new AdomdUnknownResponseException(XmlaSR.DimeRecord_TypeFormatShouldBeMedia(this.m_typeFormat.ToString()), "");
				}
				if (num3 <= 0)
				{
					throw new AdomdUnknownResponseException(XmlaSR.DimeRecord_DataTypeShouldBeSpecifiedOnTheFirstChunk, "");
				}
			}
			else
			{
				if (this.m_typeFormat != TypeFormatEnum.Unchanged)
				{
					throw new AdomdUnknownResponseException(XmlaSR.DimeRecord_TypeFormatShouldBeUnchanged(this.m_typeFormat.ToString()), "");
				}
				if (num3 != 0)
				{
					throw new AdomdUnknownResponseException(XmlaSR.DimeRecord_DataTypeIsOnlyForTheFirstChunk, "");
				}
				if (num2 != 0)
				{
					throw new AdomdUnknownResponseException(XmlaSR.DimeRecord_IDIsOnlyForFirstChunk, "");
				}
				if (num != 0)
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, "Unexpected non-zero options length");
				}
			}
			if (this.m_reserved != 0)
			{
				throw new AdomdUnknownResponseException(XmlaSR.DimeRecord_ReservedFlagShouldBeZero(this.m_reserved), "");
			}
			if (num > 0)
			{
				array = new byte[DimeRecord.RoundUp(num)];
				DimeRecord.ForceRead(this.m_stream, array, array.Length);
				this.m_Options.FromBytes(array);
			}
			if (num2 > 0)
			{
				array = new byte[DimeRecord.RoundUp(num2)];
				DimeRecord.ForceRead(this.m_stream, array, array.Length);
			}
			if (num3 > 0)
			{
				array = new byte[DimeRecord.RoundUp(num3)];
				DimeRecord.ForceRead(this.m_stream, array, array.Length);
				this.m_type = Encoding.ASCII.GetString(array, 0, num3);
				if (!DataTypes.IsSupportedDataType(this.m_type))
				{
					throw new AdomdUnknownResponseException(XmlaSR.DimeRecord_DataTypeNotSupported(this.m_type), "");
				}
			}
			this.m_firstChunk = false;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		private void WriteHeader(bool endOfRecord, bool endOfMessage, long contentLength)
		{
			byte[] array = new byte[12];
			byte[] array2 = null;
			byte[] array3 = null;
			DimeRecord.HeaderFlagsEnum headerFlagsEnum = (DimeRecord.HeaderFlagsEnum)0;
			if (this.m_chunked && !endOfRecord)
			{
				headerFlagsEnum = DimeRecord.HeaderFlagsEnum.ChunkedRecord;
			}
			if (this.m_beginOfMessage)
			{
				headerFlagsEnum |= DimeRecord.HeaderFlagsEnum.BeginOfMessage;
				this.m_beginOfMessage = false;
			}
			if (endOfMessage)
			{
				headerFlagsEnum |= DimeRecord.HeaderFlagsEnum.EndOfMessage;
			}
			TypeFormatEnum typeFormatEnum;
			Uri uri;
			string text;
			if (!this.m_chunked || this.m_firstChunk)
			{
				typeFormatEnum = this.m_typeFormat;
				uri = this.m_id;
				text = this.m_type;
			}
			else
			{
				typeFormatEnum = TypeFormatEnum.Unchanged;
				uri = null;
				text = null;
			}
			array[0] = (byte)(headerFlagsEnum | (DimeRecord.HeaderFlagsEnum)8);
			array[1] = (byte)((byte)typeFormatEnum << 4);
			if (!this.m_chunked || this.m_firstChunk)
			{
				array[2] = 0;
				array[3] = 4;
			}
			int length;
			if (uri != null && (length = uri.AbsoluteUri.Length) > 0)
			{
				int num = Encoding.ASCII.GetByteCount(uri.AbsoluteUri);
				if (num > 65535)
				{
					throw new Exception(XmlaSR.DimeRecord_EncodedTypeLengthExceeds8191);
				}
				array3 = new byte[DimeRecord.PaddedCount(num)];
				Encoding.ASCII.GetBytes(this.m_id.AbsoluteUri, 0, length, array3, 0);
				array[4] = (byte)(num >> 8);
				array[5] = (byte)num;
			}
			if (text != null && text.Length > 0)
			{
				int num = Encoding.ASCII.GetByteCount(text);
				if (num > 65535)
				{
					throw new Exception(XmlaSR.DimeRecord_EncodedTypeLengthExceeds8191);
				}
				array2 = new byte[DimeRecord.PaddedCount(num)];
				Encoding.ASCII.GetBytes(text, 0, text.Length, array2, 0);
				array[6] = (byte)(num >> 8);
				array[7] = (byte)num;
			}
			if (contentLength > 0L)
			{
				array[8] = (byte)((contentLength >> 24) & 255L);
				array[9] = (byte)((contentLength >> 16) & 255L);
				array[10] = (byte)((contentLength >> 8) & 255L);
				array[11] = (byte)(contentLength & 255L);
			}
			this.m_stream.Write(array, 0, 12);
			if (array3 != null && array3.Length != 0)
			{
				this.m_stream.Write(array3, 0, array3.Length);
			}
			if (!this.m_chunked || this.m_firstChunk)
			{
				this.m_stream.Write(this.m_Options.GetBytes(), 0, 4);
			}
			if (array2 != null && array2.Length != 0)
			{
				this.m_stream.Write(array2, 0, array2.Length);
			}
			this.m_firstChunk = false;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000AC18 File Offset: 0x00008E18
		private void WriteMessageEndRecord()
		{
			this.m_stream.WriteByte(2);
			this.m_stream.WriteByte(0);
			this.m_stream.WriteByte(128);
			for (int i = 0; i < 9; i++)
			{
				this.m_stream.WriteByte(0);
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000AC66 File Offset: 0x00008E66
		private void WriteChunkedPayload(bool endOfRecord, bool endOfMessage)
		{
			this.WriteChunkedPayload(endOfRecord, endOfMessage, null, 0, 0);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000AC74 File Offset: 0x00008E74
		private void WriteChunkedPayload(bool endOfRecord, bool endOfMessage, byte[] bytes, int offset, int count)
		{
			byte[] array = null;
			int num = 0;
			if (this.m_bodyStreamBuffer != null && this.m_bodyStreamBuffer.Length > 0L)
			{
				array = this.m_bodyStreamBuffer.GetBuffer();
				this.m_bodyStreamBuffer = null;
				num = this.m_bytesReadWritten;
				this.m_bytesReadWritten = 0;
			}
			this.WriteHeader(endOfRecord, endOfMessage, (long)(count + num));
			if (array != null)
			{
				this.m_stream.Write(array, 0, num);
			}
			if (bytes != null)
			{
				this.m_stream.Write(bytes, offset, count);
			}
			DimeRecord.WritePadding(this.m_stream, count + num);
		}

		// Token: 0x040001BE RID: 446
		private const int PaddingMultiple = 4;

		// Token: 0x040001BF RID: 447
		private const int FixedHeaderSize = 12;

		// Token: 0x040001C0 RID: 448
		private const byte HeaderFlagMask = 7;

		// Token: 0x040001C1 RID: 449
		private const byte VersionFlagMask = 248;

		// Token: 0x040001C2 RID: 450
		private const byte TypeFlagMask = 240;

		// Token: 0x040001C3 RID: 451
		private const byte ReservedFlagMask = 15;

		// Token: 0x040001C4 RID: 452
		private const int MaxMetadataLength = 65535;

		// Token: 0x040001C5 RID: 453
		private DimeRecord.IOModeEnum m_ioMode;

		// Token: 0x040001C6 RID: 454
		private Stream m_stream;

		// Token: 0x040001C7 RID: 455
		private MemoryStream m_bodyStreamBuffer;

		// Token: 0x040001C8 RID: 456
		private bool m_chunked;

		// Token: 0x040001C9 RID: 457
		private bool m_firstChunk;

		// Token: 0x040001CA RID: 458
		private bool m_headerWritten;

		// Token: 0x040001CB RID: 459
		private bool m_beginOfMessage;

		// Token: 0x040001CC RID: 460
		private bool m_endOfMessage;

		// Token: 0x040001CD RID: 461
		private bool m_closed;

		// Token: 0x040001CE RID: 462
		private int m_chunkSize;

		// Token: 0x040001CF RID: 463
		private Uri m_id;

		// Token: 0x040001D0 RID: 464
		private string m_type;

		// Token: 0x040001D1 RID: 465
		private TypeFormatEnum m_typeFormat;

		// Token: 0x040001D2 RID: 466
		private byte m_reserved;

		// Token: 0x040001D3 RID: 467
		private byte m_version;

		// Token: 0x040001D4 RID: 468
		private TransportCapabilities m_Options = new TransportCapabilities();

		// Token: 0x040001D5 RID: 469
		private int m_contentLength;

		// Token: 0x040001D6 RID: 470
		private int m_bytesReadWritten;

		// Token: 0x0200017C RID: 380
		private enum IOModeEnum
		{
			// Token: 0x04000C0B RID: 3083
			ReadOnly,
			// Token: 0x04000C0C RID: 3084
			WriteOnly
		}

		// Token: 0x0200017D RID: 381
		[Flags]
		private enum HeaderFlagsEnum : byte
		{
			// Token: 0x04000C0E RID: 3086
			BeginOfMessage = 4,
			// Token: 0x04000C0F RID: 3087
			EndOfMessage = 2,
			// Token: 0x04000C10 RID: 3088
			ChunkedRecord = 1
		}
	}
}
