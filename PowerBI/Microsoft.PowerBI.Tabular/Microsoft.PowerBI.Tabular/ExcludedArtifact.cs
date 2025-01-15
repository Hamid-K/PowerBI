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
	// Token: 0x02000058 RID: 88
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class ExcludedArtifact : MetadataObject
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x00021279 File Offset: 0x0001F479
		public ExcludedArtifact()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00021287 File Offset: 0x0001F487
		internal ExcludedArtifact(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00021295 File Offset: 0x0001F495
		private void InitBodyAndCollections()
		{
			this.body = new ExcludedArtifact.ObjectBody(this);
			this.body.Reference = string.Empty;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000212B3 File Offset: 0x0001F4B3
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.ExcludedArtifact;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x000212B7 File Offset: 0x0001F4B7
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x000212C9 File Offset: 0x0001F4C9
		public override MetadataObject Parent
		{
			get
			{
				return this.body.ObjectID.Object;
			}
			internal set
			{
				if (this.body.ObjectID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<ExcludedArtifact, MetadataObject>(this.body.ObjectID, value, null, null);
				}
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x000212F1 File Offset: 0x0001F4F1
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ObjectID.ObjectID;
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00021304 File Offset: 0x0001F504
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.ExcludedArtifact, null, "ExcludedArtifact object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("artifactType", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("artifactType", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (writer.ShouldIncludeProperty("reference", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString))
				{
					writer.WriteProperty("reference", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString, typeof(string));
				}
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00021394 File Offset: 0x0001F594
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.ExcludedArtifact[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x000213CB File Offset: 0x0001F5CB
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x000213D3 File Offset: 0x0001F5D3
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (ExcludedArtifact.ObjectBody)value;
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000213E1 File Offset: 0x0001F5E1
		internal override ITxObjectBody CreateBody()
		{
			return new ExcludedArtifact.ObjectBody(this);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000213E9 File Offset: 0x0001F5E9
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new ExcludedArtifact();
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000213F0 File Offset: 0x0001F5F0
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			ObjectType objectType = parent.ObjectType;
			if (objectType <= ObjectType.Table)
			{
				if (objectType == ObjectType.Model)
				{
					return ((Model)parent).ExcludedArtifacts;
				}
				if (objectType == ObjectType.Table)
				{
					return ((Table)parent).ExcludedArtifacts;
				}
			}
			else
			{
				if (objectType == ObjectType.Hierarchy)
				{
					return ((Hierarchy)parent).ExcludedArtifacts;
				}
				if (objectType == ObjectType.Expression)
				{
					return ((NamedExpression)parent).ExcludedArtifacts;
				}
			}
			throw TomInternalException.Create("Got a parent object of unexpected type: {0}", new object[] { parent.GetType().Name });
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0002146C File Offset: 0x0001F66C
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			MetadataObject metadataObject = MetadataObject.ResolveMetadataObjectParentById<ExcludedArtifact, MetadataObject>(this.body.ObjectID, objectMap, throwIfCantResolve, null, null);
			if (metadataObject != null)
			{
				ObjectType objectType = metadataObject.ObjectType;
				if (objectType <= ObjectType.Table)
				{
					if (objectType == ObjectType.Model)
					{
						((Model)metadataObject).ExcludedArtifacts.Add(this);
						return;
					}
					if (objectType == ObjectType.Table)
					{
						((Table)metadataObject).ExcludedArtifacts.Add(this);
						return;
					}
				}
				else
				{
					if (objectType == ObjectType.Hierarchy)
					{
						((Hierarchy)metadataObject).ExcludedArtifacts.Add(this);
						return;
					}
					if (objectType == ObjectType.Expression)
					{
						((NamedExpression)metadataObject).ExcludedArtifacts.Add(this);
						return;
					}
				}
				throw TomInternalException.Create("Got a parent object of unexpected type: {0}", new object[] { metadataObject.GetType().Name });
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0002151C File Offset: 0x0001F71C
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0002151E File Offset: 0x0001F71E
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0002152C File Offset: 0x0001F72C
		public ObjectType ArtifactType
		{
			get
			{
				return (ObjectType)this.body.ArtifactType;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ArtifactType, (int)value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ArtifactType", typeof(int), this.body.ArtifactType, (int)value);
					int artifactType = this.body.ArtifactType;
					this.body.ArtifactType = (int)value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ArtifactType", typeof(int), artifactType, (int)value);
				}
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x000215B0 File Offset: 0x0001F7B0
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x000215C0 File Offset: 0x0001F7C0
		public string Reference
		{
			get
			{
				return this.body.Reference;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Reference, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Reference", typeof(string), this.body.Reference, value);
					string reference = this.body.Reference;
					this.body.Reference = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Reference", typeof(string), reference, value);
				}
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00021630 File Offset: 0x0001F830
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x00021644 File Offset: 0x0001F844
		public MetadataObject Object
		{
			get
			{
				return this.body.ObjectID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ObjectID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Object", typeof(MetadataObject), this.body.ObjectID.Object, value);
					MetadataObject @object = this.body.ObjectID.Object;
					this.body.ObjectID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Object", typeof(MetadataObject), @object, value);
				}
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x000216C8 File Offset: 0x0001F8C8
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x000216DA File Offset: 0x0001F8DA
		internal ObjectId _ObjectID
		{
			get
			{
				return this.body.ObjectID.ObjectID;
			}
			set
			{
				this.body.ObjectID.ObjectID = value;
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000216F0 File Offset: 0x0001F8F0
		internal void CopyFrom(ExcludedArtifact other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions || !this.body.IsEqualTo(other.body, context))
			{
				ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				this.body.CopyFrom(other.body, context);
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00021744 File Offset: 0x0001F944
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((ExcludedArtifact)other, context);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00021753 File Offset: 0x0001F953
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(ExcludedArtifact other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0002176F File Offset: 0x0001F96F
		public void CopyTo(ExcludedArtifact other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0002178B File Offset: 0x0001F98B
		public ExcludedArtifact Clone()
		{
			return base.CloneInternal<ExcludedArtifact>();
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00021794 File Offset: 0x0001F994
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ExcludedArtifact is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.ObjectID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ObjectID", this.body.ObjectID.Object);
				writer.WriteProperty<int>(options, "ObjectType", (int)this.body.ObjectID.Object.ObjectType);
			}
			if (this.body.ArtifactType != 0)
			{
				writer.WriteProperty<int>(options, "ArtifactType", this.body.ArtifactType);
			}
			if (!string.IsNullOrEmpty(this.body.Reference))
			{
				writer.WriteProperty<string>(options, "Reference", this.body.Reference);
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0002187C File Offset: 0x0001FA7C
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ObjectID", out objectId))
			{
				this.body.ObjectID.ObjectID = objectId;
			}
			int num;
			if (reader.TryReadProperty<int>("ArtifactType", out num))
			{
				this.body.ArtifactType = num;
			}
			string text;
			if (reader.TryReadProperty<string>("Reference", out text))
			{
				this.body.Reference = text;
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000218E8 File Offset: 0x0001FAE8
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.ExcludedArtifact.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ExcludedArtifact is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.ObjectID.Object != null && writer.ShouldIncludeProperty("ObjectID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("ObjectID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.ObjectID.Object);
				writer.WriteObjectTypeProperty("ObjectType", MetadataPropertyNature.LinkTypeProperty, this.body.ObjectID.Object.ObjectType);
			}
			if (this.body.ArtifactType != 0 && writer.ShouldIncludeProperty("ArtifactType", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("ArtifactType", MetadataPropertyNature.RegularProperty, this.body.ArtifactType);
			}
			if (!string.IsNullOrEmpty(this.body.Reference) && writer.ShouldIncludeProperty("Reference", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString))
			{
				writer.WriteStringProperty("Reference", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString, this.body.Reference);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00021A18 File Offset: 0x0001FC18
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.ExcludedArtifact.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ExcludedArtifact is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (this.body.ArtifactType != 0 && writer.ShouldIncludeProperty("artifactType", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("artifactType", MetadataPropertyNature.RegularProperty, this.body.ArtifactType);
			}
			if (!string.IsNullOrEmpty(this.body.Reference) && writer.ShouldIncludeProperty("reference", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString))
			{
				writer.WriteStringProperty("reference", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString, this.body.Reference);
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00021AE4 File Offset: 0x0001FCE4
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName == "ObjectID")
			{
				this.body.ObjectID.ObjectID = reader.ReadObjectIdProperty();
				return true;
			}
			if (propertyName == "ArtifactType" || propertyName == "artifactType")
			{
				this.body.ArtifactType = reader.ReadInt32Property();
				return true;
			}
			if (!(propertyName == "Reference") && !(propertyName == "reference"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			this.body.Reference = reader.ReadStringProperty();
			return true;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00021B8C File Offset: 0x0001FD8C
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ExcludedArtifact is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.ArtifactType != 0)
			{
				result["artifactType", TomPropCategory.Regular, 2, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.body.ArtifactType);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Reference))
			{
				result["reference", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonObject(this.body.Reference, "Reference");
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00021C3C File Offset: 0x0001FE3C
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "artifactType")
			{
				this.body.ArtifactType = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
				return true;
			}
			if (!(name == "reference"))
			{
				return false;
			}
			this.body.Reference = JsonPropertyHelper.ConvertJsonContentToString(jsonProp.Value);
			return true;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00021CA0 File Offset: 0x0001FEA0
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_ExcludedArtifact_1Args(base.Id.ToString());
		}

		// Token: 0x040000F5 RID: 245
		internal ExcludedArtifact.ObjectBody body;

		// Token: 0x02000260 RID: 608
		internal class ObjectBody : MetadataObjectBody<ExcludedArtifact>
		{
			// Token: 0x06002014 RID: 8212 RVA: 0x000D31C2 File Offset: 0x000D13C2
			public ObjectBody(ExcludedArtifact owner)
				: base(owner)
			{
				this.ObjectID = new UntypedParentLink<ExcludedArtifact>(owner, "Object");
			}

			// Token: 0x06002015 RID: 8213 RVA: 0x000D31DC File Offset: 0x000D13DC
			internal bool IsEqualTo(ExcludedArtifact.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.ArtifactType, other.ArtifactType) && PropertyHelper.AreValuesIdentical(this.Reference, other.Reference) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ObjectID.IsEqualTo(other.ObjectID, context));
			}

			// Token: 0x06002016 RID: 8214 RVA: 0x000D3240 File Offset: 0x000D1440
			internal void CopyFromImpl(ExcludedArtifact.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.ArtifactType = other.ArtifactType;
				this.Reference = other.Reference;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ObjectID.CopyFrom(other.ObjectID, context);
				}
			}

			// Token: 0x06002017 RID: 8215 RVA: 0x000D329D File Offset: 0x000D149D
			internal void CopyFromImpl(ExcludedArtifact.ObjectBody other)
			{
				this.ArtifactType = other.ArtifactType;
				this.Reference = other.Reference;
				this.ObjectID.CopyFrom(other.ObjectID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06002018 RID: 8216 RVA: 0x000D32CD File Offset: 0x000D14CD
			public override void CopyFrom(MetadataObjectBody<ExcludedArtifact> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((ExcludedArtifact.ObjectBody)other, context);
			}

			// Token: 0x06002019 RID: 8217 RVA: 0x000D32E4 File Offset: 0x000D14E4
			internal bool IsEqualTo(ExcludedArtifact.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.ArtifactType, other.ArtifactType) && PropertyHelper.AreValuesIdentical(this.Reference, other.Reference) && this.ObjectID.IsEqualTo(other.ObjectID);
			}

			// Token: 0x0600201A RID: 8218 RVA: 0x000D3331 File Offset: 0x000D1531
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((ExcludedArtifact.ObjectBody)other);
			}

			// Token: 0x0600201B RID: 8219 RVA: 0x000D334C File Offset: 0x000D154C
			internal void CompareWith(ExcludedArtifact.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.ArtifactType, other.ArtifactType))
				{
					context.RegisterPropertyChange(base.Owner, "ArtifactType", typeof(int), PropertyFlags.DdlAndUser, other.ArtifactType, this.ArtifactType);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Reference, other.Reference))
				{
					context.RegisterPropertyChange(base.Owner, "Reference", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.Json, other.Reference, this.Reference);
				}
				this.ObjectID.CompareWith(other.ObjectID, "ObjectID", "Object", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x0600201C RID: 8220 RVA: 0x000D33F7 File Offset: 0x000D15F7
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((ExcludedArtifact.ObjectBody)other, context);
			}

			// Token: 0x04000822 RID: 2082
			internal int ArtifactType;

			// Token: 0x04000823 RID: 2083
			internal string Reference;

			// Token: 0x04000824 RID: 2084
			internal UntypedParentLink<ExcludedArtifact> ObjectID;
		}
	}
}
