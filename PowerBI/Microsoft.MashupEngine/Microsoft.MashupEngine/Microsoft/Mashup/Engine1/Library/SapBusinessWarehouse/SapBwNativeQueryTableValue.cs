using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004CE RID: 1230
	internal sealed class SapBwNativeQueryTableValue : TableValue
	{
		// Token: 0x06002843 RID: 10307 RVA: 0x000770F6 File Offset: 0x000752F6
		public SapBwNativeQueryTableValue(IEngineHost host, string query, ISapBwService service)
		{
			this.host = host;
			this.query = query;
			this.service = service;
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x00077113 File Offset: 0x00075313
		public override TypeValue Type
		{
			get
			{
				if (this.type == null)
				{
					this.type = DataReaderSchemaTableTableTypeValue.New(this.GetReaderCore());
				}
				return this.type;
			}
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x00077134 File Offset: 0x00075334
		public override void TestConnection()
		{
			this.service.TestConnection();
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x00077144 File Offset: 0x00075344
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			IEnumerator<IValueReference> enumerator;
			try
			{
				TableTypeValue asTableType = this.Type.AsTableType;
				IDataReader readerCore = this.GetReaderCore();
				this.reader = null;
				enumerator = new DbDataReaderEnumerator(readerCore, true, asTableType.ItemType, "SAP Business Warehouse", null);
			}
			catch
			{
				if (this.reader != null)
				{
					this.reader.Dispose();
				}
				throw;
			}
			return enumerator;
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000771A8 File Offset: 0x000753A8
		private IDataReader GetReaderCore()
		{
			if (this.reader == null)
			{
				this.reader = this.service.ExecuteMdx(this.query, RowRange.All, true, false, null, null);
				this.reader = this.host.RegisterForCleanup(this.reader);
			}
			return this.reader;
		}

		// Token: 0x04001129 RID: 4393
		private readonly IEngineHost host;

		// Token: 0x0400112A RID: 4394
		private readonly string query;

		// Token: 0x0400112B RID: 4395
		private readonly ISapBwService service;

		// Token: 0x0400112C RID: 4396
		private IDataReaderWithTableSchema reader;

		// Token: 0x0400112D RID: 4397
		private TableTypeValue type;
	}
}
