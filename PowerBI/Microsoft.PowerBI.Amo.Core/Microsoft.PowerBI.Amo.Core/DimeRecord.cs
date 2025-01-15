using System;
using System.IO;
using System.Text;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200003C RID: 60
	internal class DimeRecord
	{
		// Token: 0x0600029D RID: 669 RVA: 0x0000D104 File Offset: 0x0000B304
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

		// Token: 0x0600029E RID: 670 RVA: 0x0000D164 File Offset: 0x0000B364
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

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000D218 File Offset: 0x0000B418
		public bool CanRead
		{
			get
			{
				return this.m_ioMode == DimeRecord.IOModeEnum.ReadOnly && !this.m_closed;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000D230 File Offset: 0x0000B430
		public bool CanWrite
		{
			get
			{
				return this.m_ioMode == DimeRecord.IOModeEnum.WriteOnly && !this.m_closed;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000D248 File Offset: 0x0000B448
		public int ChunkSize
		{
			get
			{
				return this.m_chunkSize;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000D250 File Offset: 0x0000B450
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

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000D26B File Offset: 0x0000B46B
		public string Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000D273 File Offset: 0x0000B473
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000D27B File Offset: 0x0000B47B
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000D284 File Offset: 0x0000B484
		public TypeFormatEnum TypeFormat
		{
			get
			{
				return this.m_typeFormat;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000D28C File Offset: 0x0000B48C
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

		// Token: 0x060002A8 RID: 680 RVA: 0x0000D368 File Offset: 0x0000B568
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

		// Token: 0x060002A9 RID: 681 RVA: 0x0000D495 File Offset: 0x0000B695
		public void Close()
		{
			this.Close(false);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000D4A0 File Offset: 0x0000B6A0
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

		// Token: 0x060002AB RID: 683 RVA: 0x0000D520 File Offset: 0x0000B720
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

		// Token: 0x060002AC RID: 684 RVA: 0x0000D554 File Offset: 0x0000B754
		private static void WritePadding(Stream stream, int bytesWritten)
		{
			int num = DimeRecord.PaddedCount(bytesWritten) - bytesWritten;
			for (int i = 0; i < num; i++)
			{
				stream.WriteByte(0);
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000D580 File Offset: 0x0000B780
		private static void ReadPadding(Stream stream, int bytesRead)
		{
			int num = DimeRecord.PaddedCount(bytesRead) - bytesRead;
			for (int i = 0; i < num; i++)
			{
				stream.ReadByte();
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000D5A9 File Offset: 0x0000B7A9
		private static int RoundUp(int length)
		{
			return (length + 3) & -4;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000D5B1 File Offset: 0x0000B7B1
		private static int PaddedCount(int byteCount)
		{
			return byteCount + ((byteCount % 4 > 0) ? (4 - byteCount % 4) : 0);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
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

		// Token: 0x060002B1 RID: 689 RVA: 0x0000D644 File Offset: 0x0000B844
		private void ReadHeader()
		{
			byte[] array = new byte[12];
			DimeRecord.ForceRead(this.m_stream, array, 12);
			this.m_version = (byte)((array[0] & 248) >> 3);
			if (this.m_version != 1)
			{
				throw new ResponseFormatException(XmlaSR.DimeRecord_VersionNotSupported((int)this.m_version), "");
			}
			DimeRecord.HeaderFlagsEnum headerFlagsEnum = (DimeRecord.HeaderFlagsEnum)(array[0] & 7);
			this.m_chunked = (headerFlagsEnum & DimeRecord.HeaderFlagsEnum.ChunkedRecord) > (DimeRecord.HeaderFlagsEnum)0;
			this.m_beginOfMessage = (headerFlagsEnum & DimeRecord.HeaderFlagsEnum.BeginOfMessage) > (DimeRecord.HeaderFlagsEnum)0;
			this.m_endOfMessage = (headerFlagsEnum & DimeRecord.HeaderFlagsEnum.EndOfMessage) > (DimeRecord.HeaderFlagsEnum)0;
			if (this.m_chunked && this.m_endOfMessage)
			{
				throw new ResponseFormatException(XmlaSR.DimeRecord_InvalidHeaderFlags((this.m_beginOfMessage > false) ? 1 : 0, 1, 1), "");
			}
			if ((!this.m_chunked && !this.m_endOfMessage) || (!this.m_firstChunk && this.m_beginOfMessage))
			{
				throw new ResponseFormatException(XmlaSR.DimeRecord_OnlySingleRecordMessagesAreSupported, "");
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
					throw new ResponseFormatException(XmlaSR.DimeRecord_TypeFormatShouldBeMedia(this.m_typeFormat.ToString()), "");
				}
				if (num3 <= 0)
				{
					throw new ResponseFormatException(XmlaSR.DimeRecord_DataTypeShouldBeSpecifiedOnTheFirstChunk, "");
				}
			}
			else
			{
				if (this.m_typeFormat != TypeFormatEnum.Unchanged)
				{
					throw new ResponseFormatException(XmlaSR.DimeRecord_TypeFormatShouldBeUnchanged(this.m_typeFormat.ToString()), "");
				}
				if (num3 != 0)
				{
					throw new ResponseFormatException(XmlaSR.DimeRecord_DataTypeIsOnlyForTheFirstChunk, "");
				}
				if (num2 != 0)
				{
					throw new ResponseFormatException(XmlaSR.DimeRecord_IDIsOnlyForFirstChunk, "");
				}
				if (num != 0)
				{
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, "Unexpected non-zero options length");
				}
			}
			if (this.m_reserved != 0)
			{
				throw new ResponseFormatException(XmlaSR.DimeRecord_ReservedFlagShouldBeZero(this.m_reserved), "");
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
					throw new ResponseFormatException(XmlaSR.DimeRecord_DataTypeNotSupported(this.m_type), "");
				}
			}
			this.m_firstChunk = false;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000D8EC File Offset: 0x0000BAEC
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

		// Token: 0x060002B3 RID: 691 RVA: 0x0000DB14 File Offset: 0x0000BD14
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

		// Token: 0x060002B4 RID: 692 RVA: 0x0000DB62 File Offset: 0x0000BD62
		private void WriteChunkedPayload(bool endOfRecord, bool endOfMessage)
		{
			this.WriteChunkedPayload(endOfRecord, endOfMessage, null, 0, 0);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000DB70 File Offset: 0x0000BD70
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

		// Token: 0x04000203 RID: 515
		private const int PaddingMultiple = 4;

		// Token: 0x04000204 RID: 516
		private const int FixedHeaderSize = 12;

		// Token: 0x04000205 RID: 517
		private const byte HeaderFlagMask = 7;

		// Token: 0x04000206 RID: 518
		private const byte VersionFlagMask = 248;

		// Token: 0x04000207 RID: 519
		private const byte TypeFlagMask = 240;

		// Token: 0x04000208 RID: 520
		private const byte ReservedFlagMask = 15;

		// Token: 0x04000209 RID: 521
		private const int MaxMetadataLength = 65535;

		// Token: 0x0400020A RID: 522
		private DimeRecord.IOModeEnum m_ioMode;

		// Token: 0x0400020B RID: 523
		private Stream m_stream;

		// Token: 0x0400020C RID: 524
		private MemoryStream m_bodyStreamBuffer;

		// Token: 0x0400020D RID: 525
		private bool m_chunked;

		// Token: 0x0400020E RID: 526
		private bool m_firstChunk;

		// Token: 0x0400020F RID: 527
		private bool m_headerWritten;

		// Token: 0x04000210 RID: 528
		private bool m_beginOfMessage;

		// Token: 0x04000211 RID: 529
		private bool m_endOfMessage;

		// Token: 0x04000212 RID: 530
		private bool m_closed;

		// Token: 0x04000213 RID: 531
		private int m_chunkSize;

		// Token: 0x04000214 RID: 532
		private Uri m_id;

		// Token: 0x04000215 RID: 533
		private string m_type;

		// Token: 0x04000216 RID: 534
		private TypeFormatEnum m_typeFormat;

		// Token: 0x04000217 RID: 535
		private byte m_reserved;

		// Token: 0x04000218 RID: 536
		private byte m_version;

		// Token: 0x04000219 RID: 537
		private TransportCapabilities m_Options = new TransportCapabilities();

		// Token: 0x0400021A RID: 538
		private int m_contentLength;

		// Token: 0x0400021B RID: 539
		private int m_bytesReadWritten;

		// Token: 0x02000178 RID: 376
		private enum IOModeEnum
		{
			// Token: 0x04000BC6 RID: 3014
			ReadOnly,
			// Token: 0x04000BC7 RID: 3015
			WriteOnly
		}

		// Token: 0x02000179 RID: 377
		[Flags]
		private enum HeaderFlagsEnum : byte
		{
			// Token: 0x04000BC9 RID: 3017
			BeginOfMessage = 4,
			// Token: 0x04000BCA RID: 3018
			EndOfMessage = 2,
			// Token: 0x04000BCB RID: 3019
			ChunkedRecord = 1
		}
	}
}
