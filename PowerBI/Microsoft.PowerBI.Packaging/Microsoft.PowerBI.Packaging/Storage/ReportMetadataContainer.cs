using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000050 RID: 80
	[DataContract]
	public class ReportMetadataContainer : IBinarySerializable
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000766E File Offset: 0x0000586E
		public ReportMetadataContainer()
		{
			this.AutoCreatedRelationships = new List<AutoCreatedRelationship>();
			this.CreatedFromRelease = "Unknown";
			this.Version = ReportMetadataContainer.GetLatestMetadataVersion();
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00007697 File Offset: 0x00005897
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000769F File Offset: 0x0000589F
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 1)]
		public int Version { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000076A8 File Offset: 0x000058A8
		// (set) Token: 0x06000249 RID: 585 RVA: 0x000076B0 File Offset: 0x000058B0
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public List<AutoCreatedRelationship> AutoCreatedRelationships { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600024A RID: 586 RVA: 0x000076B9 File Offset: 0x000058B9
		// (set) Token: 0x0600024B RID: 587 RVA: 0x000076C1 File Offset: 0x000058C1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public string FileDescription { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600024C RID: 588 RVA: 0x000076CA File Offset: 0x000058CA
		// (set) Token: 0x0600024D RID: 589 RVA: 0x000076D2 File Offset: 0x000058D2
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public string CreatedFrom { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000076DB File Offset: 0x000058DB
		// (set) Token: 0x0600024F RID: 591 RVA: 0x000076E3 File Offset: 0x000058E3
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public string CreatedFromRelease { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000250 RID: 592 RVA: 0x000076EC File Offset: 0x000058EC
		// (set) Token: 0x06000251 RID: 593 RVA: 0x000076F4 File Offset: 0x000058F4
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 6)]
		public IReadOnlyDictionary<string, string> CurrentQueryNameToOldQueryName { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000252 RID: 594 RVA: 0x000076FD File Offset: 0x000058FD
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00007705 File Offset: 0x00005905
		[JsonIgnore]
		public IReadOnlyDictionary<string, string> CurrentQueryNameToLineageTag { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000770E File Offset: 0x0000590E
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00007716 File Offset: 0x00005916
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 7)]
		public IReadOnlyDictionary<string, QueryResourceInfoStorage> DirectQueryResources { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000771F File Offset: 0x0000591F
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00007727 File Offset: 0x00005927
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 8)]
		public IReadOnlyDictionary<string, IReadOnlyCollection<string>> QueryDependencyGraph { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00007730 File Offset: 0x00005930
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00007738 File Offset: 0x00005938
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 9)]
		[JsonIgnore]
		public Dictionary<string, string> QueryNameToKeyMapping { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00007741 File Offset: 0x00005941
		// (set) Token: 0x0600025B RID: 603 RVA: 0x00007749 File Offset: 0x00005949
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IReadOnlyDictionary<string, ConceptualSchemaSettingsStorage> PendingConceptualSchemaFilters { get; private set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00007752 File Offset: 0x00005952
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000775A File Offset: 0x0000595A
		[JsonIgnore]
		public IReadOnlyDictionary<string, ConceptualSchemaSettingsStorage> PendingConceptualSchemaSettings { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00007763 File Offset: 0x00005963
		// (set) Token: 0x0600025F RID: 607 RVA: 0x00007778 File Offset: 0x00005978
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		private IEnumerable<KeyValuePair<string, string>> _queryNameToKeyMapping
		{
			get
			{
				Dictionary<string, string> queryNameToKeyMapping = this.QueryNameToKeyMapping;
				if (queryNameToKeyMapping == null)
				{
					return null;
				}
				return queryNameToKeyMapping.ToList<KeyValuePair<string, string>>();
			}
			set
			{
				Dictionary<string, string> dictionary = null;
				if (value != null)
				{
					dictionary = new Dictionary<string, string>();
					foreach (KeyValuePair<string, string> keyValuePair in value)
					{
						dictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				this.QueryNameToKeyMapping = dictionary;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000077E0 File Offset: 0x000059E0
		public void Deserialize(BinarySerializationReader reader)
		{
			int num = reader.ReadInt();
			if (num >= 0)
			{
				this.AutoCreatedRelationships = reader.ReadList<AutoCreatedRelationship>();
			}
			if (num >= 1)
			{
				this.QueryNameToKeyMapping = new Dictionary<string, string>();
				reader.ReadDictionary<string, string>(this.QueryNameToKeyMapping, (BinarySerializationReader r) => r.ReadString(), (BinarySerializationReader r) => r.ReadString());
			}
			if (num >= 2)
			{
				this.FileDescription = reader.ReadNullableString();
			}
			if (num >= 3)
			{
				this.CreatedFrom = reader.ReadNullableString();
			}
			if (num >= 4)
			{
				this.CreatedFromRelease = reader.ReadNullableString();
			}
			this.Version = num;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00007894 File Offset: 0x00005A94
		public static int GetLatestMetadataVersion()
		{
			return 5;
		}

		// Token: 0x0400012E RID: 302
		public const int JsonVersion = 5;
	}
}
