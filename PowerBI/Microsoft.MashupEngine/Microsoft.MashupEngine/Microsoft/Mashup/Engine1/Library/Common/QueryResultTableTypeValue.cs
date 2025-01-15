using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001119 RID: 4377
	internal class QueryResultTableTypeValue : TableTypeValue
	{
		// Token: 0x06007282 RID: 29314 RVA: 0x00189B6D File Offset: 0x00187D6D
		public QueryResultTableTypeValue(DbEnvironment environment, string schemaName, string tableName, bool fromFunction)
		{
			this.environment = environment;
			this.schemaName = schemaName;
			this.tableName = tableName;
			this.fromFunction = fromFunction;
		}

		// Token: 0x1700200F RID: 8207
		// (get) Token: 0x06007283 RID: 29315 RVA: 0x00189B92 File Offset: 0x00187D92
		public override RecordTypeValue ItemType
		{
			get
			{
				if (this.itemType == null)
				{
					this.itemType = this.environment.RetrieveRowTypeForTable(this.schemaName, this.tableName, this.fromFunction);
				}
				return this.itemType;
			}
		}

		// Token: 0x17002010 RID: 8208
		// (get) Token: 0x06007284 RID: 29316 RVA: 0x00189BC5 File Offset: 0x00187DC5
		public override IList<TableKey> TableKeys
		{
			get
			{
				if (this.keys == null)
				{
					this.keys = this.environment.RetrieveKeysForTable(this.schemaName, this.tableName, this.ItemType.Fields.Keys);
				}
				return this.keys;
			}
		}

		// Token: 0x17002011 RID: 8209
		// (get) Token: 0x06007285 RID: 29317 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsNullable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002012 RID: 8210
		// (get) Token: 0x06007286 RID: 29318 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override TypeValue NonNullable
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002013 RID: 8211
		// (get) Token: 0x06007287 RID: 29319 RVA: 0x00189C02 File Offset: 0x00187E02
		public override TypeValue Nullable
		{
			get
			{
				if (this.nullableTypeValue == null)
				{
					this.nullableTypeValue = TableTypeValue.New(this.ItemType).Nullable;
				}
				return this.nullableTypeValue;
			}
		}

		// Token: 0x04003F20 RID: 16160
		private DbEnvironment environment;

		// Token: 0x04003F21 RID: 16161
		private string schemaName;

		// Token: 0x04003F22 RID: 16162
		private string tableName;

		// Token: 0x04003F23 RID: 16163
		private bool fromFunction;

		// Token: 0x04003F24 RID: 16164
		private RecordTypeValue itemType;

		// Token: 0x04003F25 RID: 16165
		private IList<TableKey> keys;

		// Token: 0x04003F26 RID: 16166
		private TypeValue nullableTypeValue;
	}
}
