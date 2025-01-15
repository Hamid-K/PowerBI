using System;
using System.Data;
using System.IO;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001140 RID: 4416
	internal sealed class StreamWriterDataReader : DelegatingDataReaderWithTableSchema
	{
		// Token: 0x060073B9 RID: 29625 RVA: 0x0018E10C File Offset: 0x0018C30C
		public StreamWriterDataReader(IDataReaderWithTableSchema reader, Stream stream, long count)
			: base(reader)
		{
			this.count = count;
			this.currentRowValues = new object[reader.FieldCount];
			this.reader = reader;
			this.stream = stream;
			this.writer = new DbDataWriter(stream);
			this.writer.WriteStartTable(reader);
		}

		// Token: 0x060073BA RID: 29626 RVA: 0x0018E15E File Offset: 0x0018C35E
		public override void Close()
		{
			if (this.writer != null)
			{
				this.writer.Dispose();
				this.writer = null;
			}
		}

		// Token: 0x060073BB RID: 29627 RVA: 0x0018E17C File Offset: 0x0018C37C
		public override bool Read()
		{
			bool flag = false;
			if (this.writer != null)
			{
				flag = this.currentCount < this.count && this.reader.Read();
				if (flag)
				{
					for (int i = 0; i < this.reader.FieldCount; i++)
					{
						try
						{
							this.currentRowValues[i] = DbData.GetValue(this.reader, i, null);
						}
						catch (ValueException ex)
						{
							this.currentRowValues[i] = ex;
						}
						this.writer.WriteValue(this.currentRowValues[i]);
					}
					this.currentCount += 1L;
				}
			}
			return flag;
		}

		// Token: 0x060073BC RID: 29628 RVA: 0x0018E220 File Offset: 0x0018C420
		public override void Dispose()
		{
			this.Close();
		}

		// Token: 0x04003FB5 RID: 16309
		private readonly long count;

		// Token: 0x04003FB6 RID: 16310
		private readonly object[] currentRowValues;

		// Token: 0x04003FB7 RID: 16311
		private long currentCount;

		// Token: 0x04003FB8 RID: 16312
		private IDataReader reader;

		// Token: 0x04003FB9 RID: 16313
		private Stream stream;

		// Token: 0x04003FBA RID: 16314
		private DbDataWriter writer;
	}
}
