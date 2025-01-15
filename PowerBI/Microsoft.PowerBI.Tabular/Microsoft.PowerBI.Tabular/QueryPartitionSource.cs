using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200010F RID: 271
	public class QueryPartitionSource : PartitionSource
	{
		// Token: 0x060011BF RID: 4543 RVA: 0x0007DFAB File Offset: 0x0007C1AB
		public QueryPartitionSource()
		{
			this.standaloneDataSourceLink = new StandaloneCrossLink<Partition, DataSource>("DataSource");
			this.standaloneQuery = string.Empty;
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0007DFCE File Offset: 0x0007C1CE
		internal override PartitionSourceType Type
		{
			get
			{
				return PartitionSourceType.Query;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0007DFD1 File Offset: 0x0007C1D1
		private CrossLink<Partition, DataSource> DataSourceLink
		{
			get
			{
				if (base.Partition != null)
				{
					return base.Partition.body.DataSourceID;
				}
				return this.standaloneDataSourceLink;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0007DFF2 File Offset: 0x0007C1F2
		// (set) Token: 0x060011C3 RID: 4547 RVA: 0x0007E000 File Offset: 0x0007C200
		public DataSource DataSource
		{
			get
			{
				return this.DataSourceLink.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.DataSource, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(base.Partition, "Source.DataSource", typeof(DataSource), this.DataSource, value);
					DataSource dataSource = this.DataSource;
					this.DataSourceLink.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(base.Partition, "Source.DataSource", typeof(DataSource), dataSource, value);
				}
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0007E06B File Offset: 0x0007C26B
		// (set) Token: 0x060011C5 RID: 4549 RVA: 0x0007E08C File Offset: 0x0007C28C
		public string Query
		{
			get
			{
				if (base.Partition != null)
				{
					return base.Partition.body.QueryDefinition;
				}
				return this.standaloneQuery;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.Query, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(base.Partition, "Source.Query", typeof(string), this.Query, value);
					string query = this.Query;
					if (base.Partition != null)
					{
						base.Partition.body.QueryDefinition = value;
					}
					else
					{
						this.standaloneQuery = value;
					}
					ObjectChangeTracker.RegisterPropertyChanged(base.Partition, "Source.Query", typeof(string), query, value);
				}
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0007E10D File Offset: 0x0007C30D
		internal override void MoveDataToPartition(Partition partition)
		{
			partition.SetDataSource(this.standaloneDataSourceLink);
			partition.QueryDefinition = this.standaloneQuery;
			this.standaloneDataSourceLink.Object = null;
			this.standaloneQuery = null;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0007E13C File Offset: 0x0007C33C
		internal override void LoadDataFromPartition(Partition partition, bool canResolveLinks, bool resetPartitionBodyProperties)
		{
			if (canResolveLinks)
			{
				this.standaloneDataSourceLink.Object = partition.DataSource;
				if (this.standaloneDataSourceLink.Object == null && partition.body.DataSourceID.Path != null && !partition.body.DataSourceID.Path.IsEmpty)
				{
					this.standaloneDataSourceLink.Path = partition.body.DataSourceID.Path.Clone();
				}
			}
			else
			{
				this.standaloneDataSourceLink.CopyFrom(partition.body.DataSourceID, new CopyContext(CopyFlags.DontResolveCrossLinks, null));
			}
			this.standaloneQuery = partition.QueryDefinition;
			if (resetPartitionBodyProperties)
			{
				partition.ResetDataSource();
				partition.body.QueryDefinition = string.Empty;
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0007E1F8 File Offset: 0x0007C3F8
		internal override IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames()
		{
			yield return new CustomizedPropertyName("DataSourceID", "Source.DataSource");
			yield return new CustomizedPropertyName("QueryDefinition", "Source.Query");
			yield break;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0007E204 File Offset: 0x0007C404
		internal override void SerializeToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.SerializeToJsonObject(jsonObj, options, mode, dbCompatibilityLevel);
			if (!string.IsNullOrEmpty(this.Query))
			{
				jsonObj["query", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.Query, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (this.DataSourceLink.Object != null)
			{
				this.DataSourceLink.SerializeToJsonObject(false, "dataSource", ObjectType.DataSource, jsonObj, 0, false);
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0007E270 File Offset: 0x0007C470
		private protected override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "type")
			{
				return true;
			}
			if (name == "query")
			{
				this.Query = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (!(name == "dataSource"))
			{
				return false;
			}
			if (this.DataSourceLink.Path == null)
			{
				this.DataSourceLink.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
			}
			this.DataSourceLink.Path.Push(ObjectType.DataSource, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
			return true;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0007E304 File Offset: 0x0007C504
		private protected override void SaveMetadataProperties(SerializationActivityContext context, IList<MetadataProperty> properties)
		{
			base.SaveMetadataProperties(context, properties);
			if (!string.IsNullOrEmpty(this.Query))
			{
				properties.Add(new MetadataProperty("query", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string), this.Query));
			}
			if (this.DataSourceLink.Object != null)
			{
				foreach (MetadataProperty metadataProperty in this.DataSourceLink.WriteToMetadataStreamImpl(ObjectType.DataSource, context.SerializationMode != MetadataSerializationMode.Json, "dataSource", false))
				{
					properties.Add(metadataProperty);
				}
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0007E3B0 File Offset: 0x0007C5B0
		private protected override bool TryReadMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName == "query")
			{
				this.Query = reader.ReadStringProperty();
				return true;
			}
			if (!(propertyName == "dataSource"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			this.DataSourceLink.Path = reader.ReadCrossLinkProperty((string name) => new ObjectPath(ObjectType.DataSource, name));
			return true;
		}

		// Token: 0x040002C7 RID: 711
		private StandaloneCrossLink<Partition, DataSource> standaloneDataSourceLink;

		// Token: 0x040002C8 RID: 712
		private string standaloneQuery;
	}
}
