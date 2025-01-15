using System;
using System.Data.Common;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000008 RID: 8
	internal interface IComputedColumn
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000088 RID: 136
		int Decimals { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000089 RID: 137
		int Length { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008A RID: 138
		string Name { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008B RID: 139
		RfcDataType RfcDataType { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008C RID: 140
		Type Type { get; }

		// Token: 0x0600008D RID: 141
		object GetValue(DbDataReader reader, int currentIndex);
	}
}
