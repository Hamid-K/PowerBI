using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200008C RID: 140
	internal sealed class ODataRawInputContext : ODataInputContext
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x0000EE74 File Offset: 0x0000D074
		public ODataRawInputContext(ODataFormat format, ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
			: base(format, messageInfo, messageReaderSettings)
		{
			try
			{
				this.stream = messageInfo.MessageStream;
				this.encoding = messageInfo.Encoding;
				this.readerPayloadKind = messageInfo.PayloadKind;
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					messageInfo.MessageStream.Dispose();
				}
				throw;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		internal override ODataAsynchronousReader CreateAsynchronousReader()
		{
			return this.CreateAsynchronousReaderImplementation();
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		internal override ODataBatchReader CreateBatchReader(string batchBoundary)
		{
			return this.CreateBatchReaderImplementation(batchBoundary, true);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000EEF2 File Offset: 0x0000D0F2
		internal override object ReadValue(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			return this.ReadValueImplementation(expectedPrimitiveTypeReference);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000EEFC File Offset: 0x0000D0FC
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

		// Token: 0x06000567 RID: 1383 RVA: 0x0000EF5C File Offset: 0x0000D15C
		private ODataAsynchronousReader CreateAsynchronousReaderImplementation()
		{
			return new ODataAsynchronousReader(this, this.encoding);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000EF6A File Offset: 0x0000D16A
		private ODataBatchReader CreateBatchReaderImplementation(string batchBoundary, bool synchronous)
		{
			return new ODataBatchReader(this, batchBoundary, this.encoding, synchronous);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000EF7C File Offset: 0x0000D17C
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

		// Token: 0x0600056A RID: 1386 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
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

		// Token: 0x0600056B RID: 1387 RVA: 0x0000F088 File Offset: 0x0000D288
		private object ReadRawValue(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			string text = this.textReader.ReadToEnd();
			object obj;
			if (expectedPrimitiveTypeReference != null && base.MessageReaderSettings.EnablePrimitiveTypeConversion)
			{
				obj = ODataRawValueUtils.ConvertStringToPrimitive(text, expectedPrimitiveTypeReference);
			}
			else
			{
				obj = text;
			}
			return obj;
		}

		// Token: 0x04000298 RID: 664
		private const int BufferSize = 4096;

		// Token: 0x04000299 RID: 665
		private readonly ODataPayloadKind readerPayloadKind;

		// Token: 0x0400029A RID: 666
		private readonly Encoding encoding;

		// Token: 0x0400029B RID: 667
		private Stream stream;

		// Token: 0x0400029C RID: 668
		private TextReader textReader;
	}
}
