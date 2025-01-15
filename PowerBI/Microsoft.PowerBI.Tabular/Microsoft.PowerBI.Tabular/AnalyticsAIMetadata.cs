using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200002A RID: 42
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class AnalyticsAIMetadata : NamedMetadataObject
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00003346 File Offset: 0x00001546
		public AnalyticsAIMetadata()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003354 File Offset: 0x00001554
		internal AnalyticsAIMetadata(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003362 File Offset: 0x00001562
		private void InitBodyAndCollections()
		{
			this.body = new AnalyticsAIMetadata.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.MeasureAnalysisDefinition = string.Empty;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003390 File Offset: 0x00001590
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.AnalyticsAIMetadata;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003394 File Offset: 0x00001594
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000033A6 File Offset: 0x000015A6
		public override MetadataObject Parent
		{
			get
			{
				return this.body.ModelID.Object;
			}
			internal set
			{
				if (this.body.ModelID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<AnalyticsAIMetadata, Model>(this.body.ModelID, (Model)value, null, null);
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000033D3 File Offset: 0x000015D3
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000033E8 File Offset: 0x000015E8
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.AnalyticsAIMetadata, null, "AnalyticsAIMetadata object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("measureAnalysisDefinition", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString))
				{
					writer.WriteProperty("measureAnalysisDefinition", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString, typeof(string));
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003480 File Offset: 0x00001680
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.AnalyticsAIMetadata[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000034B7 File Offset: 0x000016B7
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000034BF File Offset: 0x000016BF
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (AnalyticsAIMetadata.ObjectBody)value;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000034CD File Offset: 0x000016CD
		internal override ITxObjectBody CreateBody()
		{
			return new AnalyticsAIMetadata.ObjectBody(this);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000034D5 File Offset: 0x000016D5
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new AnalyticsAIMetadata();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000034DC File Offset: 0x000016DC
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Model)parent).AnalyticsAIMetadata;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000034EC File Offset: 0x000016EC
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Model model = MetadataObject.ResolveMetadataObjectParentById<AnalyticsAIMetadata, Model>(this.body.ModelID, objectMap, throwIfCantResolve, null, null);
			if (model != null)
			{
				model.AnalyticsAIMetadata.Add(this);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000351D File Offset: 0x0000171D
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000351F File Offset: 0x0000171F
		// (set) Token: 0x06000044 RID: 68 RVA: 0x0000352C File Offset: 0x0000172C
		public override string Name
		{
			get
			{
				return this.body.Name;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Name, value))
				{
					string text;
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.AnalyticsAIMetadata, out text))
					{
						throw new ArgumentException(text);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Name", typeof(string), this.body.Name, value);
					string name = this.body.Name;
					this.body.Name = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Name", typeof(string), name, value);
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000035AF File Offset: 0x000017AF
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000035BC File Offset: 0x000017BC
		public string MeasureAnalysisDefinition
		{
			get
			{
				return this.body.MeasureAnalysisDefinition;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.MeasureAnalysisDefinition, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "MeasureAnalysisDefinition", typeof(string), this.body.MeasureAnalysisDefinition, value);
					string measureAnalysisDefinition = this.body.MeasureAnalysisDefinition;
					this.body.MeasureAnalysisDefinition = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "MeasureAnalysisDefinition", typeof(string), measureAnalysisDefinition, value);
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000362C File Offset: 0x0000182C
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000363E File Offset: 0x0000183E
		internal ObjectId _ModelID
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
			set
			{
				this.body.ModelID.ObjectID = value;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003654 File Offset: 0x00001854
		internal void CopyFrom(AnalyticsAIMetadata other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions || !this.body.IsEqualTo(other.body, context))
			{
				ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				this.body.CopyFrom(other.body, context);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000036A8 File Offset: 0x000018A8
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((AnalyticsAIMetadata)other, context);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000036B7 File Offset: 0x000018B7
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(AnalyticsAIMetadata other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000036D3 File Offset: 0x000018D3
		public void CopyTo(AnalyticsAIMetadata other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000036EF File Offset: 0x000018EF
		public AnalyticsAIMetadata Clone()
		{
			return base.CloneInternal<AnalyticsAIMetadata>();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000036F8 File Offset: 0x000018F8
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.AnalyticsAIMetadata.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object AnalyticsAIMetadata is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.MeasureAnalysisDefinition))
			{
				writer.WriteProperty<string>(options, "MeasureAnalysisDefinition", this.body.MeasureAnalysisDefinition);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003798 File Offset: 0x00001998
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ModelID", out objectId))
			{
				this.body.ModelID.ObjectID = objectId;
			}
			string text;
			if (reader.TryReadProperty<string>("Name", out text))
			{
				this.body.Name = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("MeasureAnalysisDefinition", out text2))
			{
				this.body.MeasureAnalysisDefinition = text2;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003804 File Offset: 0x00001A04
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.AnalyticsAIMetadata.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object AnalyticsAIMetadata is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.MeasureAnalysisDefinition) && writer.ShouldIncludeProperty("MeasureAnalysisDefinition", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString))
			{
				writer.WriteStringProperty("MeasureAnalysisDefinition", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString, this.body.MeasureAnalysisDefinition);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000038DC File Offset: 0x00001ADC
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.AnalyticsAIMetadata.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object AnalyticsAIMetadata is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.MeasureAnalysisDefinition) && writer.ShouldIncludeProperty("measureAnalysisDefinition", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString))
			{
				writer.WriteStringProperty("measureAnalysisDefinition", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString, this.body.MeasureAnalysisDefinition);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000039B4 File Offset: 0x00001BB4
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName == "ModelID")
			{
				this.body.ModelID.ObjectID = reader.ReadObjectIdProperty();
				return true;
			}
			if (propertyName == "Name" || propertyName == "name")
			{
				this.body.Name = reader.ReadStringProperty();
				return true;
			}
			if (!(propertyName == "MeasureAnalysisDefinition") && !(propertyName == "measureAnalysisDefinition"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			this.body.MeasureAnalysisDefinition = reader.ReadStringProperty();
			return true;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003A5C File Offset: 0x00001C5C
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.AnalyticsAIMetadata.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object AnalyticsAIMetadata is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.MeasureAnalysisDefinition))
			{
				result["measureAnalysisDefinition", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonObject(this.body.MeasureAnalysisDefinition, "MeasureAnalysisDefinition");
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003B0C File Offset: 0x00001D0C
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "name")
			{
				this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (!(name == "measureAnalysisDefinition"))
			{
				return false;
			}
			this.body.MeasureAnalysisDefinition = JsonPropertyHelper.ConvertJsonContentToString(jsonProp.Value);
			return true;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003B6D File Offset: 0x00001D6D
		internal override string GetFormattedObjectPath()
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				return TomSR.ObjectPath_AnalyticsAIMetadata_0Args;
			}
			return TomSR.ObjectPath_AnalyticsAIMetadata_1Args(this.Name);
		}

		// Token: 0x040000C8 RID: 200
		internal AnalyticsAIMetadata.ObjectBody body;

		// Token: 0x02000224 RID: 548
		internal class ObjectBody : NamedMetadataObjectBody<AnalyticsAIMetadata>
		{
			// Token: 0x06001E96 RID: 7830 RVA: 0x000CC333 File Offset: 0x000CA533
			public ObjectBody(AnalyticsAIMetadata owner)
				: base(owner)
			{
				this.ModelID = new ParentLink<AnalyticsAIMetadata, Model>(owner, "Model");
			}

			// Token: 0x06001E97 RID: 7831 RVA: 0x000CC34D File Offset: 0x000CA54D
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x06001E98 RID: 7832 RVA: 0x000CC358 File Offset: 0x000CA558
			internal bool IsEqualTo(AnalyticsAIMetadata.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.MeasureAnalysisDefinition, other.MeasureAnalysisDefinition) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ModelID.IsEqualTo(other.ModelID, context));
			}

			// Token: 0x06001E99 RID: 7833 RVA: 0x000CC3BC File Offset: 0x000CA5BC
			internal void CopyFromImpl(AnalyticsAIMetadata.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.MeasureAnalysisDefinition = other.MeasureAnalysisDefinition;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModelID.CopyFrom(other.ModelID, context);
				}
			}

			// Token: 0x06001E9A RID: 7834 RVA: 0x000CC41E File Offset: 0x000CA61E
			internal void CopyFromImpl(AnalyticsAIMetadata.ObjectBody other)
			{
				this.Name = other.Name;
				this.MeasureAnalysisDefinition = other.MeasureAnalysisDefinition;
				this.ModelID.CopyFrom(other.ModelID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001E9B RID: 7835 RVA: 0x000CC44E File Offset: 0x000CA64E
			public override void CopyFrom(MetadataObjectBody<AnalyticsAIMetadata> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((AnalyticsAIMetadata.ObjectBody)other, context);
			}

			// Token: 0x06001E9C RID: 7836 RVA: 0x000CC468 File Offset: 0x000CA668
			internal bool IsEqualTo(AnalyticsAIMetadata.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.MeasureAnalysisDefinition, other.MeasureAnalysisDefinition) && this.ModelID.IsEqualTo(other.ModelID);
			}

			// Token: 0x06001E9D RID: 7837 RVA: 0x000CC4B5 File Offset: 0x000CA6B5
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((AnalyticsAIMetadata.ObjectBody)other);
			}

			// Token: 0x06001E9E RID: 7838 RVA: 0x000CC4D0 File Offset: 0x000CA6D0
			internal void CompareWith(AnalyticsAIMetadata.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.MeasureAnalysisDefinition, other.MeasureAnalysisDefinition))
				{
					context.RegisterPropertyChange(base.Owner, "MeasureAnalysisDefinition", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.Json, other.MeasureAnalysisDefinition, this.MeasureAnalysisDefinition);
				}
				this.ModelID.CompareWith(other.ModelID, "ModelID", "Model", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06001E9F RID: 7839 RVA: 0x000CC571 File Offset: 0x000CA771
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((AnalyticsAIMetadata.ObjectBody)other, context);
			}

			// Token: 0x04000714 RID: 1812
			internal string Name;

			// Token: 0x04000715 RID: 1813
			internal string MeasureAnalysisDefinition;

			// Token: 0x04000716 RID: 1814
			internal ParentLink<AnalyticsAIMetadata, Model> ModelID;
		}
	}
}
