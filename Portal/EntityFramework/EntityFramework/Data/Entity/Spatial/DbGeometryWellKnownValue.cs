using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Spatial
{
	// Token: 0x02000094 RID: 148
	[DataContract]
	public sealed class DbGeometryWellKnownValue
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x000124C4 File Offset: 0x000106C4
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x000124CC File Offset: 0x000106CC
		[DataMember(Order = 1, IsRequired = false, EmitDefaultValue = false)]
		public int CoordinateSystemId { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000124D5 File Offset: 0x000106D5
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x000124DD File Offset: 0x000106DD
		[DataMember(Order = 2, IsRequired = false, EmitDefaultValue = false)]
		public string WellKnownText { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x000124E6 File Offset: 0x000106E6
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x000124EE File Offset: 0x000106EE
		[DataMember(Order = 3, IsRequired = false, EmitDefaultValue = false)]
		public byte[] WellKnownBinary { get; set; }
	}
}
