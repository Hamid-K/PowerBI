using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000B0 RID: 176
	internal class ODataRawInputContext : ODataInputContext
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x00012530 File Offset: 0x00010730
		public ODataRawInputContext(ODataFormat format, ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
			: base(format, messageInfo, messageReaderSettings)
		{
			try
			{
				this.stream = messageInfo.MessageStream;
				this.Encoding = messageInfo.Encoding;
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

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00012594 File Offset: 0x00010794
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001259C File Offset: 0x0001079C
		internal override ODataAsynchronousReader CreateAsynchronousReader()
		{
			return this.CreateAsynchronousReaderImplementation();
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000125A4 File Offset: 0x000107A4
		internal override Task<ODataAsynchronousReader> CreateAsynchronousReaderAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataAsynchronousReader>(() => this.CreateAsynchronousReaderImplementation());
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x000125B7 File Offset: 0x000107B7
		internal override object ReadValue(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			return this.ReadValueImplementation(expectedPrimitiveTypeReference);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000125C0 File Offset: 0x000107C0
		internal override Task<object> ReadValueAsync(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			return TaskUtils.GetTaskForSynchronousOperation<object>(() => this.ReadValueImplementation(expectedPrimitiveTypeReference));
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000125F4 File Offset: 0x000107F4
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

		// Token: 0x060007A0 RID: 1952 RVA: 0x00012654 File Offset: 0x00010854
		private ODataAsynchronousReader CreateAsynchronousReaderImplementation()
		{
			return new ODataAsynchronousReader(this, this.Encoding);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00012664 File Offset: 0x00010864
		private object ReadValueImplementation(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			bool flag;
			if (expectedPrimitiveTypeReference == null)
			{
				flag = this.readerPayloadKind == ODataPayloadKind.BinaryValue;
			}
			else
			{
				flag = expectedPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Binary || expectedPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Stream;
			}
			if (flag)
			{
				return this.ReadBinaryValue();
			}
			this.textReader = ((this.Encoding == null) ? new StreamReader(this.stream) : new StreamReader(this.stream, this.Encoding));
			return this.ReadRawValue(expectedPrimitiveTypeReference);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000126D8 File Offset: 0x000108D8
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

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001277C File Offset: 0x0001097C
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

		// Token: 0x040002FB RID: 763
		protected readonly Encoding Encoding;

		// Token: 0x040002FC RID: 764
		private const int BufferSize = 4096;

		// Token: 0x040002FD RID: 765
		private readonly ODataPayloadKind readerPayloadKind;

		// Token: 0x040002FE RID: 766
		private Stream stream;

		// Token: 0x040002FF RID: 767
		private TextReader textReader;
	}
}
