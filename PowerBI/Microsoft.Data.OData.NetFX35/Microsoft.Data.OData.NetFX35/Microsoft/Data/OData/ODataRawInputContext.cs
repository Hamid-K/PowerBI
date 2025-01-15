using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x02000240 RID: 576
	internal sealed class ODataRawInputContext : ODataInputContext
	{
		// Token: 0x06001178 RID: 4472 RVA: 0x00042184 File Offset: 0x00040384
		internal ODataRawInputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver, ODataPayloadKind readerPayloadKind)
			: base(format, messageReaderSettings, version, readingResponse, synchronous, model, urlResolver)
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

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x000421F8 File Offset: 0x000403F8
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00042200 File Offset: 0x00040400
		internal override ODataBatchReader CreateBatchReader(string batchBoundary)
		{
			return this.CreateBatchReaderImplementation(batchBoundary, true);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0004220A File Offset: 0x0004040A
		internal override object ReadValue(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			return this.ReadValueImplementation(expectedPrimitiveTypeReference);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00042214 File Offset: 0x00040414
		protected override void DisposeImplementation()
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

		// Token: 0x0600117D RID: 4477 RVA: 0x0004226C File Offset: 0x0004046C
		private ODataBatchReader CreateBatchReaderImplementation(string batchBoundary, bool synchronous)
		{
			return new ODataBatchReader(this, batchBoundary, this.encoding, synchronous);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0004227C File Offset: 0x0004047C
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

		// Token: 0x0600117F RID: 4479 RVA: 0x000422E4 File Offset: 0x000404E4
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

		// Token: 0x06001180 RID: 4480 RVA: 0x00042388 File Offset: 0x00040588
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

		// Token: 0x040006A4 RID: 1700
		private const int BufferSize = 4096;

		// Token: 0x040006A5 RID: 1701
		private readonly ODataPayloadKind readerPayloadKind;

		// Token: 0x040006A6 RID: 1702
		private readonly Encoding encoding;

		// Token: 0x040006A7 RID: 1703
		private Stream stream;

		// Token: 0x040006A8 RID: 1704
		private TextReader textReader;
	}
}
