using System;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000040 RID: 64
	public sealed class ConnectionPropertiesStorageV0 : IBinarySerializable
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00006A5E File Offset: 0x00004C5E
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00006A66 File Offset: 0x00004C66
		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				this.connectionString = value;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006A6F File Offset: 0x00004C6F
		public void Deserialize(BinarySerializationReader reader)
		{
			this.ConnectionString = reader.ReadString();
		}

		// Token: 0x04000109 RID: 265
		private string connectionString;
	}
}
