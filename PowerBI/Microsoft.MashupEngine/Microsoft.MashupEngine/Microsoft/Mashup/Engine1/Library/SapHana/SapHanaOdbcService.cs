using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Odbc;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000468 RID: 1128
	internal class SapHanaOdbcService : OdbcDelegatingService
	{
		// Token: 0x060025A6 RID: 9638 RVA: 0x0006CC18 File Offset: 0x0006AE18
		public SapHanaOdbcService(IOdbcService service)
			: base(service)
		{
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x0006CC21 File Offset: 0x0006AE21
		public override IOdbcConnection CreateConnection(OdbcConnectionProperties args)
		{
			return new SapHanaOdbcService.SapHanaOdbcConnection(base.CreateConnection(args));
		}

		// Token: 0x02000469 RID: 1129
		private class SapHanaOdbcConnection : OdbcDelegatingConnection
		{
			// Token: 0x060025A8 RID: 9640 RVA: 0x0006CC2F File Offset: 0x0006AE2F
			public SapHanaOdbcConnection(IOdbcConnection innerConnection)
				: base(innerConnection)
			{
			}

			// Token: 0x060025A9 RID: 9641 RVA: 0x0006CC38 File Offset: 0x0006AE38
			public override IDataReaderWithTableSchema GetColumns(string catalogName, string schemaName, string tableName)
			{
				return new SapHanaOdbcService.SapHanaColumnReader(base.GetColumns(catalogName, schemaName, tableName));
			}
		}

		// Token: 0x0200046A RID: 1130
		private class SapHanaColumnReader : DelegatingDataReaderWithTableSchema
		{
			// Token: 0x060025AA RID: 9642 RVA: 0x0006CC48 File Offset: 0x0006AE48
			public SapHanaColumnReader(IDataReaderWithTableSchema reader)
				: base(reader)
			{
			}

			// Token: 0x060025AB RID: 9643 RVA: 0x0006CC54 File Offset: 0x0006AE54
			public override int GetValues(object[] values)
			{
				int values2 = base.Reader.GetValues(values);
				for (int i = 0; i < values2; i++)
				{
					values[i] = this.GetValue(i);
				}
				return values2;
			}

			// Token: 0x060025AC RID: 9644 RVA: 0x0006CC88 File Offset: 0x0006AE88
			public override object GetValue(int i)
			{
				if (i == 4)
				{
					short num = (short)base.Reader[i];
					return (num == 16) ? -7 : num;
				}
				return base.Reader[i];
			}

			// Token: 0x17000F23 RID: 3875
			public override object this[int i]
			{
				get
				{
					return this.GetValue(i);
				}
			}

			// Token: 0x04000F95 RID: 3989
			private const int DataTypeOrdinal = 4;

			// Token: 0x04000F96 RID: 3990
			private const short HanaBooleanType = 16;

			// Token: 0x04000F97 RID: 3991
			private const short SqlBitType = -7;
		}
	}
}
