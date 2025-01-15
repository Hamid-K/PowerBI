using System;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000007 RID: 7
	internal interface IDbSpatialValue
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000032 RID: 50
		bool IsGeography { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000033 RID: 51
		object ProviderValue { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000034 RID: 52
		int? CoordinateSystemId { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000035 RID: 53
		string WellKnownText { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000036 RID: 54
		byte[] WellKnownBinary { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000037 RID: 55
		string GmlString { get; }

		// Token: 0x06000038 RID: 56
		Exception NotSqlCompatible();
	}
}
