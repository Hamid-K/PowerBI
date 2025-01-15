using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000BB RID: 187
	public class SingleColumnRelationship : Relationship
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x0005E779 File Offset: 0x0005C979
		public SingleColumnRelationship()
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0005E787 File Offset: 0x0005C987
		internal SingleColumnRelationship(IEqualityComparer<string> comparer)
			: base(comparer)
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0005E796 File Offset: 0x0005C996
		private void OnAfterConstructor()
		{
			this.body.FromCardinality = RelationshipEndCardinality.Many;
			this.body.ToCardinality = RelationshipEndCardinality.One;
			base.Type = RelationshipType.SingleColumn;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0005E7B7 File Offset: 0x0005C9B7
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new SingleColumnRelationship();
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0005E7BE File Offset: 0x0005C9BE
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x0005E7C6 File Offset: 0x0005C9C6
		public new RelationshipEndCardinality FromCardinality
		{
			get
			{
				return base.FromCardinality;
			}
			set
			{
				base.FromCardinality = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0005E7CF File Offset: 0x0005C9CF
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x0005E7D7 File Offset: 0x0005C9D7
		public new RelationshipEndCardinality ToCardinality
		{
			get
			{
				return base.ToCardinality;
			}
			set
			{
				base.ToCardinality = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0005E7E0 File Offset: 0x0005C9E0
		// (set) Token: 0x06000B8B RID: 2955 RVA: 0x0005E7E8 File Offset: 0x0005C9E8
		public new Column FromColumn
		{
			get
			{
				return base.FromColumn;
			}
			set
			{
				base.FromColumn = value;
				base.FromTable = ((value != null) ? value.Table : null);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0005E803 File Offset: 0x0005CA03
		// (set) Token: 0x06000B8D RID: 2957 RVA: 0x0005E80B File Offset: 0x0005CA0B
		public new Column ToColumn
		{
			get
			{
				return base.ToColumn;
			}
			set
			{
				base.ToColumn = value;
				base.ToTable = ((value != null) ? value.Table : null);
			}
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0005E828 File Offset: 0x0005CA28
		internal override void SerializeToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.SerializeToJsonObject(jsonObj, options, mode, dbCompatibilityLevel);
			if (this.FromCardinality != RelationshipEndCardinality.Many)
			{
				jsonObj["fromCardinality", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertEnumToJsonValue<RelationshipEndCardinality>(this.FromCardinality);
			}
			if (this.ToCardinality != RelationshipEndCardinality.One)
			{
				jsonObj["toCardinality", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertEnumToJsonValue<RelationshipEndCardinality>(this.ToCardinality);
			}
			if (this.body.FromColumnID.Object != null)
			{
				this.body.FromColumnID.SerializeToJsonObject(false, "from", ObjectType.Table, jsonObj, 0, false);
			}
			if (this.body.ToColumnID.Object != null)
			{
				this.body.ToColumnID.SerializeToJsonObject(false, "to", ObjectType.Table, jsonObj, 0, false);
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0005E8DC File Offset: 0x0005CADC
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "fromCardinality")
			{
				this.FromCardinality = JsonPropertyHelper.ConvertJsonValueToEnum<RelationshipEndCardinality>(jsonProp.Value);
				return true;
			}
			if (name == "toCardinality")
			{
				this.ToCardinality = JsonPropertyHelper.ConvertJsonValueToEnum<RelationshipEndCardinality>(jsonProp.Value);
				return true;
			}
			if (name == "fromColumn")
			{
				if (this.body.FromColumnID.Path == null)
				{
					this.body.FromColumnID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
				}
				this.body.FromColumnID.Path.Push(ObjectType.Column, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
				return true;
			}
			if (name == "fromTable")
			{
				if (this.body.FromColumnID.Path == null)
				{
					this.body.FromColumnID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
				}
				this.body.FromColumnID.Path.Push(ObjectType.Table, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
				if (this.body.FromTableID.Path == null)
				{
					this.body.FromTableID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
				}
				this.body.FromTableID.Path.Push(ObjectType.Table, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
				return true;
			}
			if (name == "toColumn")
			{
				if (this.body.ToColumnID.Path == null)
				{
					this.body.ToColumnID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
				}
				this.body.ToColumnID.Path.Push(ObjectType.Column, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
				return true;
			}
			if (!(name == "toTable"))
			{
				return base.ReadPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel);
			}
			if (this.body.ToColumnID.Path == null)
			{
				this.body.ToColumnID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
			}
			this.body.ToColumnID.Path.Push(ObjectType.Table, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
			if (this.body.ToTableID.Path == null)
			{
				this.body.ToTableID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
			}
			this.body.ToTableID.Path.Push(ObjectType.Table, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
			return true;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0005EB54 File Offset: 0x0005CD54
		private protected override void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteRegularPropertiesToMetadataStream(context, writer);
			if (this.FromCardinality != RelationshipEndCardinality.Many && writer.ShouldIncludeProperty("fromCardinality", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<RelationshipEndCardinality>("fromCardinality", MetadataPropertyNature.RegularProperty, this.FromCardinality);
			}
			if (this.ToCardinality != RelationshipEndCardinality.One && writer.ShouldIncludeProperty("toCardinality", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<RelationshipEndCardinality>("toCardinality", MetadataPropertyNature.RegularProperty, this.ToCardinality);
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0005EBBC File Offset: 0x0005CDBC
		private protected override void WriteCrossLinksToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteCrossLinksToMetadataStream(context, writer);
			if (this.body.FromColumnID.Object != null && writer.ShouldIncludeProperty("fromColumn", MetadataPropertyNature.CrossLinkProperty))
			{
				if (context.SerializationMode == MetadataSerializationMode.Json)
				{
					this.body.FromColumnID.WriteToMetadataStream(ObjectType.Table, false, "from", false, writer);
				}
				else
				{
					this.body.FromColumnID.WriteToMetadataStream(ObjectType.Table, true, "fromColumn", false, writer);
				}
			}
			if (this.body.ToColumnID.Object != null && writer.ShouldIncludeProperty("toColumn", MetadataPropertyNature.CrossLinkProperty))
			{
				if (context.SerializationMode == MetadataSerializationMode.Json)
				{
					this.body.ToColumnID.WriteToMetadataStream(ObjectType.Table, false, "to", false, writer);
					return;
				}
				this.body.ToColumnID.WriteToMetadataStream(ObjectType.Table, true, "toColumn", false, writer);
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0005EC8C File Offset: 0x0005CE8C
		private protected override void ReadMetadataProperties(SerializationActivityContext context, IMetadataReader reader)
		{
			if (context.SerializationMode != MetadataSerializationMode.Xmla)
			{
				context.ActivityInfo.Remove("SerializationActivity::SingleColumnRelationshipFromTable");
				context.ActivityInfo.Remove("SerializationActivity::SingleColumnRelationshipFromColumn");
				context.ActivityInfo.Remove("SerializationActivity::SingleColumnRelationshipToTable");
				context.ActivityInfo.Remove("SerializationActivity::SingleColumnRelationshipToColumn");
			}
			base.ReadMetadataProperties(context, reader);
			if (context.SerializationMode != MetadataSerializationMode.Xmla)
			{
				string text;
				if (context.TryExtractActivityInfo<string>("SerializationActivity::SingleColumnRelationshipFromTable", out text))
				{
					string text2;
					if (!context.TryExtractActivityInfo<string>("SerializationActivity::SingleColumnRelationshipFromColumn", out text2))
					{
						throw reader.CreateInvalidDataException(context, TomSR.Exception_MissingJsonProperty("fromColumn"), null);
					}
					this.body.FromTableID.Path = new ObjectPath(ObjectType.Table, text);
					this.body.FromColumnID.Path = new ObjectPath(new KeyValuePair<ObjectType, string>[]
					{
						new KeyValuePair<ObjectType, string>(ObjectType.Table, text),
						new KeyValuePair<ObjectType, string>(ObjectType.Column, text2)
					});
				}
				string text3;
				if (context.TryExtractActivityInfo<string>("SerializationActivity::SingleColumnRelationshipToTable", out text3))
				{
					string text4;
					if (!context.TryExtractActivityInfo<string>("SerializationActivity::SingleColumnRelationshipToColumn", out text4))
					{
						throw reader.CreateInvalidDataException(context, TomSR.Exception_MissingJsonProperty("toColumn"), null);
					}
					this.body.ToTableID.Path = new ObjectPath(ObjectType.Table, text3);
					this.body.ToColumnID.Path = new ObjectPath(new KeyValuePair<ObjectType, string>[]
					{
						new KeyValuePair<ObjectType, string>(ObjectType.Table, text3),
						new KeyValuePair<ObjectType, string>(ObjectType.Column, text4)
					});
				}
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0005EDF8 File Offset: 0x0005CFF8
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			if (classification != UnexpectedPropertyClassification.Unclassified)
			{
				return false;
			}
			string propertyName = reader.PropertyName;
			if (propertyName == "fromCardinality")
			{
				this.FromCardinality = reader.ReadEnumProperty<RelationshipEndCardinality>();
				return true;
			}
			if (propertyName == "toCardinality")
			{
				this.ToCardinality = reader.ReadEnumProperty<RelationshipEndCardinality>();
				return true;
			}
			if (propertyName == "fromTable")
			{
				context.ActivityInfo["SerializationActivity::SingleColumnRelationshipFromTable"] = reader.ReadStringProperty();
				return true;
			}
			if (propertyName == "fromColumn")
			{
				if (context.SerializationMode == MetadataSerializationMode.Json)
				{
					context.ActivityInfo["SerializationActivity::SingleColumnRelationshipFromColumn"] = reader.ReadStringProperty();
				}
				else
				{
					ObjectPath objectPath = reader.ReadCrossLinkProperty();
					context.ActivityInfo["SerializationActivity::SingleColumnRelationshipFromTable"] = objectPath[0].Value;
					context.ActivityInfo["SerializationActivity::SingleColumnRelationshipFromColumn"] = objectPath[1].Value;
				}
				return true;
			}
			if (propertyName == "toTable")
			{
				context.ActivityInfo["SerializationActivity::SingleColumnRelationshipToTable"] = reader.ReadStringProperty();
				return true;
			}
			if (!(propertyName == "toColumn"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			if (context.SerializationMode == MetadataSerializationMode.Json)
			{
				context.ActivityInfo["SerializationActivity::SingleColumnRelationshipToColumn"] = reader.ReadStringProperty();
			}
			else
			{
				ObjectPath objectPath2 = reader.ReadCrossLinkProperty();
				context.ActivityInfo["SerializationActivity::SingleColumnRelationshipToTable"] = objectPath2[0].Value;
				context.ActivityInfo["SerializationActivity::SingleColumnRelationshipToColumn"] = objectPath2[1].Value;
			}
			return true;
		}

		// Token: 0x04000172 RID: 370
		private const RelationshipEndCardinality DefaultFromCardinality = RelationshipEndCardinality.Many;

		// Token: 0x04000173 RID: 371
		private const RelationshipEndCardinality DefaultToCardinality = RelationshipEndCardinality.One;

		// Token: 0x020002CF RID: 719
		internal static class SerializationActivityInfoKey
		{
			// Token: 0x04000A43 RID: 2627
			public const string FromTable = "SerializationActivity::SingleColumnRelationshipFromTable";

			// Token: 0x04000A44 RID: 2628
			public const string FromColumn = "SerializationActivity::SingleColumnRelationshipFromColumn";

			// Token: 0x04000A45 RID: 2629
			public const string ToTable = "SerializationActivity::SingleColumnRelationshipToTable";

			// Token: 0x04000A46 RID: 2630
			public const string ToColumn = "SerializationActivity::SingleColumnRelationshipToColumn";
		}
	}
}
