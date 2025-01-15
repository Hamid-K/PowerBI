using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Spatial
{
	// Token: 0x02000092 RID: 146
	[DataContract]
	public sealed class DbGeographyWellKnownValue
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00011E5E File Offset: 0x0001005E
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x00011E66 File Offset: 0x00010066
		[DataMember(Order = 1, IsRequired = false, EmitDefaultValue = false)]
		public int CoordinateSystemId { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00011E6F File Offset: 0x0001006F
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00011E77 File Offset: 0x00010077
		[DataMember(Order = 2, IsRequired = false, EmitDefaultValue = false)]
		public string WellKnownText { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00011E80 File Offset: 0x00010080
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00011E88 File Offset: 0x00010088
		[DataMember(Order = 3, IsRequired = false, EmitDefaultValue = false)]
		public byte[] WellKnownBinary { get; set; }
	}
}
