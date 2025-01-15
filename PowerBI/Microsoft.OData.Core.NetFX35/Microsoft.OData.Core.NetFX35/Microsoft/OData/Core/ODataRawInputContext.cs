using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x02000196 RID: 406
	internal sealed class ODataRawInputContext : ODataInputContext
	{
		// Token: 0x06000F38 RID: 3896 RVA: 0x000351D0 File Offset: 0x000333D0
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
		internal ODataRawInputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver, ODataPayloadKind readerPayloadKind)
			: base(format, messageReaderSettings, readingResponse, synchronous, model, urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			try
			{
				this.stream = messageStream;
				this.encoding = encoding;
				this.readerPayloadKind = readerPayloadKind;
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex) && messageStream != null)
				{
					messageStream.Dispose();
				}
				throw;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00035244 File Offset: 0x00033444
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0003524C File Offset: 0x0003344C
		internal override ODataAsynchronousReader CreateAsynchronousReader()
		{
			return this.CreateAsynchronousReaderImplementation();
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00035254 File Offset: 0x00033454
		internal override ODataBatchReader CreateBatchReader(string batchBoundary)
		{
			return this.CreateBatchReaderImplementation(batchBoundary, true);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003525E File Offset: 0x0003345E
		internal override object ReadValue(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			return this.ReadValueImplementation(expectedPrimitiveTypeReference);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00035268 File Offset: 0x00033468
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					if (this.textReader != null)
					{
						this.textReader.Dispose();
					}
					else if (this.stream != null)
					{
						this.stream.Dispose();
					}
				}
				finally
				{
					this.textReader = null;
					this.stream = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000352C8 File Offset: 0x000334C8
		private ODataAsynchronousReader CreateAsynchronousReaderImplementation()
		{
			return new ODataAsynchronousReader(this, this.encoding);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000352D6 File Offset: 0x000334D6
		private ODataBatchReader CreateBatchReaderImplementation(string batchBoundary, bool synchronous)
		{
			return new ODataBatchReader(this, batchBoundary, this.encoding, synchronous);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000352E8 File Offset: 0x000334E8
		private object ReadValueImplementation(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			bool flag;
			if (expectedPrimitiveTypeReference == null)
			{
				flag = this.readerPayloadKind == ODataPayloadKind.BinaryValue;
			}
			else
			{
				flag = expectedPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Binary;
			}
			if (flag)
			{
				return this.ReadBinaryValue();
			}
			this.textReader = ((this.encoding == null) ? new StreamReader(this.stream) : new StreamReader(this.stream, this.encoding));
			return this.ReadRawValue(expectedPrimitiveTypeReference);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00035350 File Offset: 0x00033550
		private byte[] ReadBinaryValue()
		{
			long num = 0L;
			List<byte[]> list = new List<byte[]>();
			byte[] array;
			int num2;
			do
			{
				array = new byte[4096];
				num2 = this.stream.Read(array, 0, array.Length);
				num += (long)num2;
				list.Add(array);
			}
			while (num2 == array.Length);
			array = new byte[num];
			for (int i = 0; i < list.Count - 1; i++)
			{
				Buffer.BlockCopy(list[i], 0, array, i * 4096, 4096);
			}
			Buffer.BlockCopy(list[list.Count - 1], 0, array, (list.Count - 1) * 4096, num2);
			return array;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000353F4 File Offset: 0x000335F4
		private object ReadRawValue(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			string text = this.textReader.ReadToEnd();
			object obj;
			if (expectedPrimitiveTypeReference != null && !base.MessageReaderSettings.DisablePrimitiveTypeConversion)
			{
				obj = AtomValueUtils.ConvertStringToPrimitive(text, expectedPrimitiveTypeReference);
			}
			else
			{
				obj = text;
			}
			return obj;
		}

		// Token: 0x040006A9 RID: 1705
		private const int BufferSize = 4096;

		// Token: 0x040006AA RID: 1706
		private readonly ODataPayloadKind readerPayloadKind;

		// Token: 0x040006AB RID: 1707
		private readonly Encoding encoding;

		// Token: 0x040006AC RID: 1708
		private Stream stream;

		// Token: 0x040006AD RID: 1709
		private TextReader textReader;
	}
}
