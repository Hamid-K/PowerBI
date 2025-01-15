using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200010B RID: 267
	public abstract class PartitionSource
	{
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001176 RID: 4470
		internal abstract PartitionSourceType Type { get; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0007CE81 File Offset: 0x0007B081
		// (set) Token: 0x06001178 RID: 4472 RVA: 0x0007CE89 File Offset: 0x0007B089
		internal bool IsRemoved { get; private set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0007CE92 File Offset: 0x0007B092
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x0007CE9A File Offset: 0x0007B09A
		public Partition Partition
		{
			get
			{
				return this.partition;
			}
			internal set
			{
				this.partition = value;
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0007CEA4 File Offset: 0x0007B0A4
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			writer.WriteProperty("source", MetadataPropertyNature.ChildProperty, null);
			using (writer.CreateChoiceScope())
			{
				PartitionSource.WriteMetadataSchemaForQueryPartitionSource(context, writer);
				PartitionSource.WriteMetadataSchemaForCalculatedPartitionSource(context, writer);
				PartitionSource.WriteMetadataSchemaForEmptyPartitionSource(context, writer);
				if (CompatibilityRestrictions.PartitionSourceType_M.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					PartitionSource.WriteMetadataSchemaForMPartitionSource(context, writer);
				}
				if (CompatibilityRestrictions.PartitionSourceType_Entity.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					PartitionSource.WriteMetadataSchemaForEntityPartitionSource(context, writer);
				}
				if (CompatibilityRestrictions.PartitionSourceType_PolicyRange.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					PartitionSource.WriteMetadataSchemaForPolicyRangePartitionSource(context, writer);
				}
				if (CompatibilityRestrictions.PartitionSourceType_CalculationGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					PartitionSource.WriteMetadataSchemaForCalculationGroupSource(context, writer);
				}
				if (CompatibilityRestrictions.PartitionSourceType_Inferred.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					PartitionSource.WriteMetadataSchemaForInferredPartitionSource(context, writer);
				}
				if (CompatibilityRestrictions.PartitionSourceType_Parquet.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					PartitionSource.WriteMetadataSchemaForParquetPartitionSource(context, writer);
				}
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0007CFB0 File Offset: 0x0007B1B0
		internal static PartitionSource Create(PartitionSourceType type)
		{
			switch (type)
			{
			case PartitionSourceType.Query:
				return new QueryPartitionSource();
			case PartitionSourceType.Calculated:
				return new CalculatedPartitionSource();
			case PartitionSourceType.None:
				return null;
			case PartitionSourceType.M:
				return new MPartitionSource();
			case PartitionSourceType.Entity:
				return new EntityPartitionSource();
			case PartitionSourceType.PolicyRange:
				return new PolicyRangePartitionSource();
			case PartitionSourceType.CalculationGroup:
				return new CalculationGroupSource();
			case PartitionSourceType.Inferred:
				return new InferredPartitionSource();
			case PartitionSourceType.Parquet:
				return new ParquetPartitionSource();
			default:
				throw new ArgumentException(TomSR.Exception_UnrecognizedValueOfType("PartitionSourceType", type.ToString()), "type");
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0007D040 File Offset: 0x0007B240
		internal static PartitionSource CreateFromMetadataStream(SerializationActivityContext context, IMetadataReader reader)
		{
			Utils.Verify(reader.CanReset);
			PartitionSourceType? partitionSourceType = null;
			if (reader.TryMoveToProperty("type"))
			{
				partitionSourceType = new PartitionSourceType?(reader.ReadEnumProperty<PartitionSourceType>());
			}
			if (partitionSourceType == null)
			{
				partitionSourceType = new PartitionSourceType?((context.SerializationMode == MetadataSerializationMode.Json) ? PartitionSourceType.Query : PartitionSourceType.None);
			}
			PartitionSource partitionSource = PartitionSource.Create(partitionSourceType.Value);
			if (partitionSource != null)
			{
				reader.Reset();
				while (reader.IsOnProperty())
				{
					UnexpectedPropertyClassification unexpectedPropertyClassification;
					if (!partitionSource.TryReadMetadataProperty(context, reader, out unexpectedPropertyClassification))
					{
						if (unexpectedPropertyClassification == UnexpectedPropertyClassification.Unclassified)
						{
							unexpectedPropertyClassification = UnexpectedPropertyClassification.UnknownProperty;
						}
						throw reader.CreateUnexpectedPropertyException(context, unexpectedPropertyClassification);
					}
				}
			}
			return partitionSource;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0007D0D2 File Offset: 0x0007B2D2
		internal static IEnumerable<MetadataProperty> GetJsonMetadataPropertiesForEmptySource()
		{
			yield return new MetadataProperty("type", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.DefaultProperty, typeof(PartitionSourceType), PartitionSourceType.None);
			yield break;
		}

		// Token: 0x0600117F RID: 4479
		internal abstract void LoadDataFromPartition(Partition partition, bool canResolveLinks, bool resetPartitionBodyProperties);

		// Token: 0x06001180 RID: 4480
		internal abstract void MoveDataToPartition(Partition partition);

		// Token: 0x06001181 RID: 4481 RVA: 0x0007D0DB File Offset: 0x0007B2DB
		internal void AttachToPartition(Partition partition, bool moveDataToPartition)
		{
			this.partition = partition;
			if (moveDataToPartition)
			{
				this.MoveDataToPartition(partition);
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0007D0EE File Offset: 0x0007B2EE
		internal void DetachFromPartition(bool canResolveLinks, bool resetPartitionBodyProperties)
		{
			this.LoadDataFromPartition(this.partition, canResolveLinks, resetPartitionBodyProperties);
			this.partition = null;
			this.MarkAsRemoved();
		}

		// Token: 0x06001183 RID: 4483
		internal abstract IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames();

		// Token: 0x06001184 RID: 4484 RVA: 0x0007D10B File Offset: 0x0007B30B
		internal void MarkAsRemoved()
		{
			this.IsRemoved = true;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0007D114 File Offset: 0x0007B314
		internal static void SerializePartitionSourceTypeToJsonObject(JsonObject jsonObj, PartitionSourceType type)
		{
			jsonObj["type", TomPropCategory.Type, 0, false] = JsonPropertyHelper.ConvertEnumToJsonValue<PartitionSourceType>(type);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0007D12A File Offset: 0x0007B32A
		internal virtual void SerializeToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			PartitionSource.SerializePartitionSourceTypeToJsonObject(jsonObj, this.Type);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0007D138 File Offset: 0x0007B338
		internal void DeserializeFromJsonObject(JObject jsonObj, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			foreach (JProperty jproperty in jsonObj.Properties())
			{
				if (!this.ReadPropertyFromJson(jproperty, options, mode, dbCompatibilityLevel))
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(jproperty.Name), jproperty, null);
				}
			}
		}

		// Token: 0x06001188 RID: 4488
		private protected abstract bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel);

		// Token: 0x06001189 RID: 4489 RVA: 0x0007D1A0 File Offset: 0x0007B3A0
		internal IEnumerable<MetadataProperty> GetMetadataProperties(SerializationActivityContext context)
		{
			IList<MetadataProperty> list = new List<MetadataProperty>();
			this.SaveMetadataProperties(context, list);
			return list;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0007D1BC File Offset: 0x0007B3BC
		private protected virtual void SaveMetadataProperties(SerializationActivityContext context, IList<MetadataProperty> properties)
		{
			properties.Add(new MetadataProperty("type", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.DefaultProperty, typeof(PartitionSourceType), this.Type));
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0007D1E8 File Offset: 0x0007B3E8
		private protected virtual bool TryReadMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			classification = UnexpectedPropertyClassification.Unclassified;
			if (string.Compare(reader.PropertyName, "type", StringComparison.InvariantCulture) == 0)
			{
				reader.Skip();
				return true;
			}
			return false;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0007D20C File Offset: 0x0007B40C
		private static void WriteMetadataSchemaForQueryPartitionSource(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (PartitionSource.WriteMetadataSchemaOfPartitionSourceCommonElements(context, writer, "QueryPartitionSource"))
			{
				writer.WriteProperty("query", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				CrossLink<Partition, DataSource>.WriteMetadataSchema(ObjectType.DataSource, ObjectType.DataSource, context.SerializationMode != MetadataSerializationMode.Json, "dataSource", false, writer);
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0007D278 File Offset: 0x0007B478
		private static void WriteMetadataSchemaForCalculatedPartitionSource(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (PartitionSource.WriteMetadataSchemaOfPartitionSourceCommonElements(context, writer, "CalculatedPartitionSource"))
			{
				writer.WriteProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				if (CompatibilityRestrictions.Partition_RetainDataTillForceCalculate.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					writer.WriteProperty("retainDataTillForceCalculate", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0007D2F8 File Offset: 0x0007B4F8
		private static void WriteMetadataSchemaForEmptyPartitionSource(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (PartitionSource.WriteMetadataSchemaOfPartitionSourceCommonElements(context, writer, "Empty PartitionSource"))
			{
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0007D330 File Offset: 0x0007B530
		private static void WriteMetadataSchemaForMPartitionSource(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (PartitionSource.WriteMetadataSchemaOfPartitionSourceCommonElements(context, writer, "MPartitionSource"))
			{
				writer.WriteProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				if (CompatibilityRestrictions.Partition_MAttributes.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					writer.WriteProperty("attributes", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
			}
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0007D3B4 File Offset: 0x0007B5B4
		private static void WriteMetadataSchemaForEntityPartitionSource(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (PartitionSource.WriteMetadataSchemaOfPartitionSourceCommonElements(context, writer, "EntityPartitionSource"))
			{
				writer.WriteProperty("entityName", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				if (CompatibilityRestrictions.Partition_SchemaName.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					writer.WriteProperty("schemaName", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				CrossLink<Partition, DataSource>.WriteMetadataSchema(ObjectType.DataSource, ObjectType.DataSource, context.SerializationMode != MetadataSerializationMode.Json, "dataSource", false, writer);
				if (CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					CrossLink<Partition, NamedExpression>.WriteMetadataSchema(ObjectType.Expression, ObjectType.Expression, true, "expressionSource", false, writer);
				}
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0007D47C File Offset: 0x0007B67C
		private static void WriteMetadataSchemaForPolicyRangePartitionSource(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (PartitionSource.WriteMetadataSchemaOfPartitionSourceCommonElements(context, writer, "PolicyRangePartitionSource"))
			{
				writer.WriteProperty("start", MetadataPropertyNature.RegularProperty, typeof(DateTime));
				writer.WriteProperty("end", MetadataPropertyNature.RegularProperty, typeof(DateTime));
				writer.WriteEnumProperty<RefreshGranularityType>("granularity", MetadataPropertyNature.RegularProperty, null);
				writer.WriteProperty("refreshBookmark", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
			}
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0007D508 File Offset: 0x0007B708
		private static void WriteMetadataSchemaForCalculationGroupSource(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (PartitionSource.WriteMetadataSchemaOfPartitionSourceCommonElements(context, writer, "CalculationGroupSource"))
			{
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0007D540 File Offset: 0x0007B740
		private static void WriteMetadataSchemaForInferredPartitionSource(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (PartitionSource.WriteMetadataSchemaOfPartitionSourceCommonElements(context, writer, "InferredPartitionSource"))
			{
			}
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0007D578 File Offset: 0x0007B778
		private static void WriteMetadataSchemaForParquetPartitionSource(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (PartitionSource.WriteMetadataSchemaOfPartitionSourceCommonElements(context, writer, "ParquetPartitionSource"))
			{
				writer.WriteProperty("location", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0007D5C8 File Offset: 0x0007B7C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IDisposable WriteMetadataSchemaOfPartitionSourceCommonElements(SerializationActivityContext context, IMetadataSchemaWriter writer, string sourceType)
		{
			IDisposable disposable = writer.CreateComplexPropertyScope("source", MetadataPropertyNature.ChildProperty, sourceType, string.Format("{0} object of Tabular Object Model (TOM)", sourceType), new bool?(false));
			writer.WriteEnumProperty<PartitionSourceType>("type", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.DefaultProperty, PropertyHelper.GetPartitionSourceTypeCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
			return disposable;
		}

		// Token: 0x040002B5 RID: 693
		private Partition partition;
	}
}
