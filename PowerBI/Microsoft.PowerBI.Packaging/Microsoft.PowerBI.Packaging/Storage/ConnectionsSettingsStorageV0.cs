using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000043 RID: 67
	public sealed class ConnectionsSettingsStorageV0 : IBinarySerializable
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00006BA1 File Offset: 0x00004DA1
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00006BA9 File Offset: 0x00004DA9
		public Dictionary<string, ConnectionPropertiesStorageV0> Connections
		{
			get
			{
				return this.connections;
			}
			set
			{
				this.connections = value;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006BB4 File Offset: 0x00004DB4
		public void Deserialize(BinarySerializationReader reader)
		{
			if (reader.ReadInt() == 0)
			{
				this.connections = new Dictionary<string, ConnectionPropertiesStorageV0>();
				reader.ReadDictionary<string, ConnectionPropertiesStorageV0>(this.connections, (BinarySerializationReader r) => r.ReadString(), (BinarySerializationReader r) => r.Read<ConnectionPropertiesStorageV0>());
			}
		}

		// Token: 0x04000111 RID: 273
		private Dictionary<string, ConnectionPropertiesStorageV0> connections;
	}
}
