using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.PowerBI.Packaging.Project;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000039 RID: 57
	[DataContract]
	public class QueriesStorage
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000062B6 File Offset: 0x000044B6
		public static Version[] SupportedVersions
		{
			get
			{
				return new Version[]
				{
					new Version(1, 0)
				};
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000062C8 File Offset: 0x000044C8
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000062D0 File Offset: 0x000044D0
		[DisplayName("Version")]
		[Description("Version of the unapplied changes file format.")]
		[DataMember(Name = "version", EmitDefaultValue = true, IsRequired = true)]
		[EnumDataType(typeof(QueriesStorage.ArtifactVersions))]
		public string Version { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000062D9 File Offset: 0x000044D9
		// (set) Token: 0x06000168 RID: 360 RVA: 0x000062E1 File Offset: 0x000044E1
		[DisplayName("ConceptualSchemaSettings")]
		[Description("Unapplied settings for DirectQuery connections to Power BI datasets and Analysis Services.")]
		[DataMember(Name = "conceptualSchemaSettings", IsRequired = false, EmitDefaultValue = false)]
		public IReadOnlyDictionary<string, ConceptualSchemaSettingsStorage> ConceptualSchemaSettings { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000062EA File Offset: 0x000044EA
		// (set) Token: 0x0600016A RID: 362 RVA: 0x000062F2 File Offset: 0x000044F2
		[DisplayName("Queries")]
		[Description("A list of the queries that have not yet been applied.")]
		[DataMember(Name = "queries", IsRequired = false, EmitDefaultValue = false)]
		public NonNulls<QueryStorage> Queries { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000062FB File Offset: 0x000044FB
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00006303 File Offset: 0x00004503
		[DisplayName("QueryGroups")]
		[Description("The groups for queries that have not yet been applied.")]
		[DataMember(Name = "queryGroups", IsRequired = false, EmitDefaultValue = false)]
		public NonNulls<QueryGroupStorage> QueryGroups { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000630C File Offset: 0x0000450C
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00006314 File Offset: 0x00004514
		[DisplayName("Culture")]
		[Description("Specifies the culture to be used when parsing date/time strings. As described in [RFC1766], culture names adhere to the format \"<languagecode2>-<country/regioncode2>.\"")]
		[DataMember(Name = "culture", IsRequired = false, EmitDefaultValue = false)]
		public string Culture { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000631D File Offset: 0x0000451D
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00006325 File Offset: 0x00004525
		[DisplayName("FirewallEnabled")]
		[Description("Specifies whether Privacy Level settings are used when combining data. See the Microsoft Support article \"Privacy levels (Power Query)\" for more information.")]
		[DataMember(Name = "firewallEnabled", IsRequired = false, EmitDefaultValue = false)]
		public bool FirewallEnabled { get; set; }

		// Token: 0x020000CB RID: 203
		private enum ArtifactVersions
		{
			// Token: 0x04000329 RID: 809
			[EnumMember(Value = "1.0")]
			Version1_0
		}
	}
}
