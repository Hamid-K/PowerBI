using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000FB RID: 251
	[CompatibilityRequirement("1400")]
	public class EntityPartitionSource : PartitionSource
	{
		// Token: 0x06001041 RID: 4161 RVA: 0x00078501 File Offset: 0x00076701
		public EntityPartitionSource()
		{
			this.standaloneDataSourceLink = new StandaloneCrossLink<Partition, DataSource>("DataSource");
			this.standaloneExpressionSourceLink = new StandaloneCrossLink<Partition, NamedExpression>("ExpressionSource");
			this.standaloneEntityName = string.Empty;
			this.standaloneSchemaName = string.Empty;
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0007853F File Offset: 0x0007673F
		internal override PartitionSourceType Type
		{
			get
			{
				return PartitionSourceType.Entity;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x00078542 File Offset: 0x00076742
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x00078564 File Offset: 0x00076764
		public string EntityName
		{
			get
			{
				if (base.Partition != null)
				{
					return base.Partition.body.QueryDefinition;
				}
				return this.standaloneEntityName;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.EntityName, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(base.Partition, "Source.EntityName", typeof(string), this.EntityName, value);
					string entityName = this.EntityName;
					if (base.Partition != null)
					{
						base.Partition.body.QueryDefinition = value;
					}
					else
					{
						this.standaloneEntityName = value;
					}
					ObjectChangeTracker.RegisterPropertyChanged(base.Partition, "Source.EntityName", typeof(string), entityName, value);
				}
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x000785E5 File Offset: 0x000767E5
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x00078608 File Offset: 0x00076808
		[CompatibilityRequirement("1604")]
		public string SchemaName
		{
			get
			{
				if (base.Partition != null)
				{
					return base.Partition.body.SchemaName;
				}
				return this.standaloneSchemaName;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.SchemaName, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(base.Partition, "Source.SchemaName", typeof(string), this.SchemaName, value);
					string schemaName = this.SchemaName;
					if (base.Partition != null)
					{
						base.Partition.body.SchemaName = value;
					}
					else
					{
						this.standaloneSchemaName = value;
					}
					ObjectChangeTracker.RegisterPropertyChanged(base.Partition, "Source.SchemaName", typeof(string), schemaName, value);
				}
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x00078689 File Offset: 0x00076889
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

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x000786AA File Offset: 0x000768AA
		private CrossLink<Partition, NamedExpression> ExpressionSourceLink
		{
			get
			{
				if (base.Partition != null)
				{
					return base.Partition.body.ExpressionSourceID;
				}
				return this.standaloneExpressionSourceLink;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x000786CB File Offset: 0x000768CB
		// (set) Token: 0x0600104A RID: 4170 RVA: 0x000786D8 File Offset: 0x000768D8
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

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x00078743 File Offset: 0x00076943
		// (set) Token: 0x0600104C RID: 4172 RVA: 0x00078750 File Offset: 0x00076950
		public NamedExpression ExpressionSource
		{
			get
			{
				return this.ExpressionSourceLink.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.ExpressionSource, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(base.Partition, "Source.ExpressionSource", typeof(NamedExpression), this.ExpressionSource, value);
					NamedExpression expressionSource = this.ExpressionSource;
					this.ExpressionSourceLink.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(base.Partition, "Source.ExpressionSource", typeof(NamedExpression), expressionSource, value);
				}
			}
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000787BC File Offset: 0x000769BC
		internal override void MoveDataToPartition(Partition partition)
		{
			partition.SetExpressionSource(this.standaloneExpressionSourceLink);
			partition.SetDataSource(this.standaloneDataSourceLink);
			partition.body.QueryDefinition = this.standaloneEntityName;
			partition.body.SchemaName = this.standaloneSchemaName;
			this.standaloneExpressionSourceLink.Object = null;
			this.standaloneDataSourceLink.Object = null;
			this.standaloneEntityName = null;
			this.standaloneSchemaName = null;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0007882C File Offset: 0x00076A2C
		internal override void LoadDataFromPartition(Partition partition, bool canResolveLinks, bool resetPartitionBodyProperties)
		{
			if (canResolveLinks)
			{
				this.standaloneDataSourceLink.Object = partition.DataSource;
				if (this.standaloneDataSourceLink.Object == null && partition.body.DataSourceID.Path != null && !partition.body.DataSourceID.Path.IsEmpty)
				{
					this.standaloneDataSourceLink.Path = partition.body.DataSourceID.Path.Clone();
				}
				this.standaloneExpressionSourceLink.Object = partition.ExpressionSource;
				if (this.standaloneExpressionSourceLink.Object == null && partition.body.ExpressionSourceID.Path != null && !partition.body.ExpressionSourceID.Path.IsEmpty)
				{
					this.standaloneExpressionSourceLink.Path = partition.body.ExpressionSourceID.Path.Clone();
				}
			}
			else
			{
				this.standaloneDataSourceLink.CopyFrom(partition.body.DataSourceID, new CopyContext(CopyFlags.DontResolveCrossLinks, null));
				this.standaloneExpressionSourceLink.CopyFrom(partition.body.ExpressionSourceID, new CopyContext(CopyFlags.DontResolveCrossLinks, null));
			}
			this.standaloneEntityName = partition.body.QueryDefinition;
			this.standaloneSchemaName = partition.body.SchemaName;
			if (resetPartitionBodyProperties)
			{
				partition.ResetExpressionSource();
				partition.ResetDataSource();
				partition.body.QueryDefinition = string.Empty;
				partition.body.SchemaName = string.Empty;
			}
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0007899F File Offset: 0x00076B9F
		internal override IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames()
		{
			yield return new CustomizedPropertyName("DataSourceID", "Source.DataSource");
			yield return new CustomizedPropertyName("QueryDefinition", "Source.EntityName");
			yield return new CustomizedPropertyName("ExpressionSourceID", "Source.ExpressionSource");
			yield return new CustomizedPropertyName("SchemaName", "Source.SchemaName");
			yield break;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000789A8 File Offset: 0x00076BA8
		internal override void SerializeToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			Utils.Verify(CompatibilityRestrictions.PartitionSourceType_Entity.IsCompatible(mode, dbCompatibilityLevel));
			base.SerializeToJsonObject(jsonObj, options, mode, dbCompatibilityLevel);
			if (!string.IsNullOrEmpty(this.EntityName))
			{
				jsonObj["entityName", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.EntityName, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (this.DataSourceLink.Object != null)
			{
				this.DataSourceLink.SerializeToJsonObject(false, "dataSource", ObjectType.DataSource, jsonObj, 0, false);
			}
			if (this.ExpressionSourceLink.Object != null)
			{
				if (!CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("An ExpressionSource is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				this.ExpressionSourceLink.SerializeToJsonObject(false, "expressionSource", ObjectType.Expression, jsonObj, 0, false);
			}
			if (!string.IsNullOrEmpty(this.SchemaName))
			{
				if (!CompatibilityRestrictions.Partition_SchemaName.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("A SchemaName is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				jsonObj["schemaName", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.SchemaName, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00078AE4 File Offset: 0x00076CE4
		private protected override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "type")
			{
				return true;
			}
			if (name == "entityName")
			{
				this.EntityName = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "dataSource")
			{
				if (this.DataSourceLink.Path == null)
				{
					this.DataSourceLink.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
				}
				this.DataSourceLink.Path.Push(ObjectType.DataSource, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
				return true;
			}
			if (!(name == "expressionSource"))
			{
				if (!(name == "schemaName"))
				{
					return false;
				}
				if (!CompatibilityRestrictions.Partition_SchemaName.IsCompatible(mode, dbCompatibilityLevel))
				{
					return false;
				}
				this.SchemaName = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			else
			{
				if (!CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
				{
					return false;
				}
				if (this.ExpressionSourceLink.Path == null)
				{
					this.ExpressionSourceLink.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
				}
				this.ExpressionSourceLink.Path.Push(ObjectType.Expression, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
				return true;
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00078C10 File Offset: 0x00076E10
		private protected override void SaveMetadataProperties(SerializationActivityContext context, IList<MetadataProperty> properties)
		{
			base.SaveMetadataProperties(context, properties);
			if (!string.IsNullOrEmpty(this.EntityName))
			{
				properties.Add(new MetadataProperty("entityName", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string), this.EntityName));
			}
			if (!string.IsNullOrEmpty(this.SchemaName))
			{
				if (!CompatibilityRestrictions.Partition_SchemaName.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A SchemaName is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				properties.Add(new MetadataProperty("schemaName", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string), this.SchemaName));
			}
			if (this.DataSourceLink.Object != null)
			{
				foreach (MetadataProperty metadataProperty in this.DataSourceLink.WriteToMetadataStreamImpl(ObjectType.DataSource, true, "dataSource", false))
				{
					properties.Add(metadataProperty);
				}
			}
			if (this.ExpressionSourceLink.Object != null)
			{
				if (!CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("An ExpressionSource is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				foreach (MetadataProperty metadataProperty2 in this.ExpressionSourceLink.WriteToMetadataStreamImpl(ObjectType.Expression, true, "expressionSource", false))
				{
					properties.Add(metadataProperty2);
				}
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00078DC8 File Offset: 0x00076FC8
		private protected override bool TryReadMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName == "entityName")
			{
				this.EntityName = reader.ReadStringProperty();
				return true;
			}
			if (propertyName == "dataSource")
			{
				this.DataSourceLink.Path = reader.ReadCrossLinkProperty((string name) => new ObjectPath(ObjectType.DataSource, name));
				return true;
			}
			if (!(propertyName == "expressionSource"))
			{
				if (!(propertyName == "schemaName"))
				{
					classification = UnexpectedPropertyClassification.UnknownProperty;
					return false;
				}
				if (!CompatibilityRestrictions.Partition_SchemaName.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					classification |= UnexpectedPropertyClassification.IncompatibleProperty;
					return false;
				}
				this.SchemaName = reader.ReadStringProperty();
				return true;
			}
			else
			{
				if (!CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					classification = UnexpectedPropertyClassification.IncompatibleProperty;
					return false;
				}
				this.ExpressionSourceLink.Path = reader.ReadCrossLinkProperty((string name) => new ObjectPath(ObjectType.Expression, name));
				return true;
			}
		}

		// Token: 0x0400024D RID: 589
		private StandaloneCrossLink<Partition, DataSource> standaloneDataSourceLink;

		// Token: 0x0400024E RID: 590
		private StandaloneCrossLink<Partition, NamedExpression> standaloneExpressionSourceLink;

		// Token: 0x0400024F RID: 591
		private string standaloneEntityName;

		// Token: 0x04000250 RID: 592
		private string standaloneSchemaName;
	}
}
