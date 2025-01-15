using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000035 RID: 53
	[DataContract]
	public class CustomConnectorMetadata
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00003285 File Offset: 0x00001485
		// (set) Token: 0x0600012A RID: 298 RVA: 0x0000328D File Offset: 0x0000148D
		[DataMember(Name = "moduleName", Order = 0)]
		public string ModuleName { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00003296 File Offset: 0x00001496
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0000329E File Offset: 0x0000149E
		[DataMember(Name = "dataSourceKind", Order = 10)]
		public string DataSourceKind { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000032A7 File Offset: 0x000014A7
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000032AF File Offset: 0x000014AF
		[DataMember(Name = "version", Order = 20)]
		public string Version { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000032B8 File Offset: 0x000014B8
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000032C0 File Offset: 0x000014C0
		[DataMember(Name = "authenticationInfo", Order = 40)]
		public IList<DataSourceAuthenticationInfo> DataSourceAuthenticationInfo { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000032C9 File Offset: 0x000014C9
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000032D1 File Offset: 0x000014D1
		[DataMember(Name = "functions", Order = 50)]
		public IList<DataSourceFunction> Functions { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000032DA File Offset: 0x000014DA
		// (set) Token: 0x06000134 RID: 308 RVA: 0x000032E2 File Offset: 0x000014E2
		[DataMember(Name = "libraryProvider", Order = 60)]
		public string LibraryProvider { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000032EB File Offset: 0x000014EB
		// (set) Token: 0x06000136 RID: 310 RVA: 0x000032F3 File Offset: 0x000014F3
		[DataMember(Name = "libraryIdentifier", Order = 70)]
		public string LibraryIdentifier { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000032FC File Offset: 0x000014FC
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00003304 File Offset: 0x00001504
		[DataMember(Name = "status", Order = 80, IsRequired = false)]
		public string Status { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000330D File Offset: 0x0000150D
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00003315 File Offset: 0x00001515
		[DataMember(Name = "isEncryptedConnectionSupported", Order = 90)]
		public bool IsEncryptedConnectionSupported { get; set; }

		// Token: 0x0600013B RID: 315 RVA: 0x0000331E File Offset: 0x0000151E
		public MashupConnectorType ConnectorType()
		{
			if (this.LibraryProvider == "Assembly(PowerBIExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35)" || this.LibraryProvider == "Memory(Memory)")
			{
				return MashupConnectorType.BuiltIn;
			}
			if (this.LibraryProvider == "Memory(Certified)")
			{
				return MashupConnectorType.Certified;
			}
			return MashupConnectorType.Custom;
		}
	}
}
