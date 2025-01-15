using System;
using System.Data;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200000D RID: 13
	public interface IRowsetDefinition : IXmlSerializable, IName
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003C RID: 60
		// (set) Token: 0x0600003D RID: 61
		string Name { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003E RID: 62
		// (set) Token: 0x0600003F RID: 63
		string RidColumnName { get; set; }

		// Token: 0x06000040 RID: 64
		DataTable GetSchemaTable(ConnectionManager connectionManager);

		// Token: 0x06000041 RID: 65
		IDataReader CreateDataReader(ConnectionManager connectionManager);
	}
}
