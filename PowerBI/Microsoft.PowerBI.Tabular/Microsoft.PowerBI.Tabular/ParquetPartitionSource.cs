using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200010A RID: 266
	[CompatibilityRequirement("1566")]
	public class ParquetPartitionSource : PartitionSource
	{
		// Token: 0x0600116B RID: 4459 RVA: 0x0007CC74 File Offset: 0x0007AE74
		public ParquetPartitionSource()
		{
			this.standaloneLocation = string.Empty;
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0007CC87 File Offset: 0x0007AE87
		internal override PartitionSourceType Type
		{
			get
			{
				return PartitionSourceType.Parquet;
			}
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0007CC8B File Offset: 0x0007AE8B
		internal override void MoveDataToPartition(Partition partition)
		{
			partition.QueryDefinition = this.standaloneLocation;
			this.standaloneLocation = null;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0007CCA0 File Offset: 0x0007AEA0
		internal override void LoadDataFromPartition(Partition partition, bool canResolveLinks, bool resetPartitionBodyProperties)
		{
			this.standaloneLocation = partition.QueryDefinition;
			if (resetPartitionBodyProperties)
			{
				partition.body.QueryDefinition = string.Empty;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x0007CCC1 File Offset: 0x0007AEC1
		// (set) Token: 0x06001170 RID: 4464 RVA: 0x0007CCE4 File Offset: 0x0007AEE4
		public string Location
		{
			get
			{
				if (base.Partition != null)
				{
					return base.Partition.body.QueryDefinition;
				}
				return this.standaloneLocation;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.Location, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(base.Partition, "Source.Location", typeof(string), this.Location, value);
					string location = this.Location;
					if (base.Partition != null)
					{
						base.Partition.body.QueryDefinition = value;
					}
					else
					{
						this.standaloneLocation = value;
					}
					ObjectChangeTracker.RegisterPropertyChanged(base.Partition, "Source.Location", typeof(string), location, value);
				}
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0007CD65 File Offset: 0x0007AF65
		internal override IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames()
		{
			yield return new CustomizedPropertyName("QueryDefinition", "Source.Location");
			yield break;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0007CD70 File Offset: 0x0007AF70
		internal override void SerializeToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			Utils.Verify(CompatibilityRestrictions.PartitionSourceType_Parquet.IsCompatible(mode, dbCompatibilityLevel));
			base.SerializeToJsonObject(jsonObj, options, mode, dbCompatibilityLevel);
			if (!string.IsNullOrEmpty(this.Location))
			{
				jsonObj["location", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.Location, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0007CDCC File Offset: 0x0007AFCC
		private protected override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "type")
			{
				return true;
			}
			if (!(name == "location"))
			{
				return false;
			}
			this.Location = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
			return true;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0007CE12 File Offset: 0x0007B012
		private protected override void SaveMetadataProperties(SerializationActivityContext context, IList<MetadataProperty> properties)
		{
			base.SaveMetadataProperties(context, properties);
			if (!string.IsNullOrEmpty(this.Location))
			{
				properties.Add(new MetadataProperty("location", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string), this.Location));
			}
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0007CE4E File Offset: 0x0007B04E
		private protected override bool TryReadMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			if (reader.PropertyName == "location")
			{
				this.Location = reader.ReadStringProperty();
				return true;
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x040002B4 RID: 692
		private string standaloneLocation;
	}
}
