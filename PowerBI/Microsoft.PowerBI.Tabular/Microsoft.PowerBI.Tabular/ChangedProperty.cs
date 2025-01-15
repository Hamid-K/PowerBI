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
	// Token: 0x02000041 RID: 65
	[CompatibilityRequirement("1567")]
	public sealed class ChangedProperty : MetadataObject
	{
		// Token: 0x0600022B RID: 555 RVA: 0x0000F5E4 File Offset: 0x0000D7E4
		public ChangedProperty()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000F5F2 File Offset: 0x0000D7F2
		internal ChangedProperty(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000F600 File Offset: 0x0000D800
		private void InitBodyAndCollections()
		{
			this.body = new ChangedProperty.ObjectBody(this);
			this.body.Property = string.Empty;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000F61E File Offset: 0x0000D81E
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.ChangedProperty;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000F622 File Offset: 0x0000D822
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000F634 File Offset: 0x0000D834
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
					MetadataObject.UpdateMetadataObjectParent<ChangedProperty, MetadataObject>(this.body.ObjectID, value, null, null);
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000F65C File Offset: 0x0000D85C
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ObjectID.ObjectID;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000F670 File Offset: 0x0000D870
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.ChangedProperty, null, "ChangedProperty object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("property", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteProperty("property", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty, typeof(string));
				}
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.ChangedProperty[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000F713 File Offset: 0x0000D913
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000F71B File Offset: 0x0000D91B
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (ChangedProperty.ObjectBody)value;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000F729 File Offset: 0x0000D929
		internal override ITxObjectBody CreateBody()
		{
			return new ChangedProperty.ObjectBody(this);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000F731 File Offset: 0x0000D931
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new ChangedProperty();
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000F738 File Offset: 0x0000D938
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			ObjectType objectType = parent.ObjectType;
			switch (objectType)
			{
			case ObjectType.Table:
				return ((Table)parent).ChangedProperties;
			case ObjectType.Column:
				return ((Column)parent).ChangedProperties;
			case ObjectType.AttributeHierarchy:
			case ObjectType.Partition:
				break;
			case ObjectType.Relationship:
				return ((Relationship)parent).ChangedProperties;
			case ObjectType.Measure:
				return ((Measure)parent).ChangedProperties;
			case ObjectType.Hierarchy:
				return ((Hierarchy)parent).ChangedProperties;
			case ObjectType.Level:
				return ((Level)parent).ChangedProperties;
			default:
				if (objectType == ObjectType.Function)
				{
					return ((Function)parent).ChangedProperties;
				}
				break;
			}
			throw TomInternalException.Create("Got a parent object of unexpected type: {0}", new object[] { parent.GetType().Name });
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			MetadataObject metadataObject = MetadataObject.ResolveMetadataObjectParentById<ChangedProperty, MetadataObject>(this.body.ObjectID, objectMap, throwIfCantResolve, null, null);
			if (metadataObject != null)
			{
				ObjectType objectType = metadataObject.ObjectType;
				switch (objectType)
				{
				case ObjectType.Table:
					((Table)metadataObject).ChangedProperties.Add(this);
					return;
				case ObjectType.Column:
					((Column)metadataObject).ChangedProperties.Add(this);
					return;
				case ObjectType.AttributeHierarchy:
				case ObjectType.Partition:
					break;
				case ObjectType.Relationship:
					((Relationship)metadataObject).ChangedProperties.Add(this);
					return;
				case ObjectType.Measure:
					((Measure)metadataObject).ChangedProperties.Add(this);
					return;
				case ObjectType.Hierarchy:
					((Hierarchy)metadataObject).ChangedProperties.Add(this);
					return;
				case ObjectType.Level:
					((Level)metadataObject).ChangedProperties.Add(this);
					return;
				default:
					if (objectType == ObjectType.Function)
					{
						((Function)metadataObject).ChangedProperties.Add(this);
						return;
					}
					break;
				}
				throw TomInternalException.Create("Got a parent object of unexpected type: {0}", new object[] { metadataObject.GetType().Name });
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000F8EB File Offset: 0x0000DAEB
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000F8ED File Offset: 0x0000DAED
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000F8FC File Offset: 0x0000DAFC
		public string Property
		{
			get
			{
				return this.body.Property;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Property, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Property", typeof(string), this.body.Property, value);
					string property = this.body.Property;
					this.body.Property = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Property", typeof(string), property, value);
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000F96C File Offset: 0x0000DB6C
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000F980 File Offset: 0x0000DB80
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000FA04 File Offset: 0x0000DC04
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000FA16 File Offset: 0x0000DC16
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

		// Token: 0x06000241 RID: 577 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		internal void CopyFrom(ChangedProperty other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions || !this.body.IsEqualTo(other.body, context))
			{
				ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				this.body.CopyFrom(other.body, context);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000FA80 File Offset: 0x0000DC80
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((ChangedProperty)other, context);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000FA8F File Offset: 0x0000DC8F
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(ChangedProperty other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000FAAB File Offset: 0x0000DCAB
		public void CopyTo(ChangedProperty other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000FAC7 File Offset: 0x0000DCC7
		public ChangedProperty Clone()
		{
			return base.CloneInternal<ChangedProperty>();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ChangedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.ObjectID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ObjectID", this.body.ObjectID.Object);
				writer.WriteProperty<int>(options, "ObjectType", (int)this.body.ObjectID.Object.ObjectType);
			}
			if (!string.IsNullOrEmpty(this.body.Property))
			{
				writer.WriteProperty<string>(options, "Property", this.body.Property);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000FB94 File Offset: 0x0000DD94
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ObjectID", out objectId))
			{
				this.body.ObjectID.ObjectID = objectId;
			}
			string text;
			if (reader.TryReadProperty<string>("Property", out text))
			{
				this.body.Property = text;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000FBE8 File Offset: 0x0000DDE8
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ChangedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.ObjectID.Object != null && writer.ShouldIncludeProperty("ObjectID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("ObjectID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.ObjectID.Object);
				writer.WriteObjectTypeProperty("ObjectType", MetadataPropertyNature.LinkTypeProperty, this.body.ObjectID.Object.ObjectType);
			}
			if (!string.IsNullOrEmpty(this.body.Property) && writer.ShouldIncludeProperty("Property", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("Property", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty, this.body.Property);
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ChangedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Property) && writer.ShouldIncludeProperty("property", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("property", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty, this.body.Property);
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000FD84 File Offset: 0x0000DF84
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
			if (!(propertyName == "Property") && !(propertyName == "property"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			this.body.Property = reader.ReadStringProperty();
			return true;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000FE00 File Offset: 0x0000E000
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ChangedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Property))
			{
				result["property", TomPropCategory.Regular, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Property, SplitMultilineOptions.None);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000FE79 File Offset: 0x0000E079
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (jsonProp.Name == "property")
			{
				this.body.Property = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			return false;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_ChangedProperty_1Args(base.Id.ToString());
		}

		// Token: 0x040000DF RID: 223
		internal ChangedProperty.ObjectBody body;

		// Token: 0x02000241 RID: 577
		internal class ObjectBody : MetadataObjectBody<ChangedProperty>
		{
			// Token: 0x06001F58 RID: 8024 RVA: 0x000CF040 File Offset: 0x000CD240
			public ObjectBody(ChangedProperty owner)
				: base(owner)
			{
				this.ObjectID = new UntypedParentLink<ChangedProperty>(owner, "Object");
			}

			// Token: 0x06001F59 RID: 8025 RVA: 0x000CF05C File Offset: 0x000CD25C
			internal bool IsEqualTo(ChangedProperty.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Property, other.Property) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ObjectID.IsEqualTo(other.ObjectID, context));
			}

			// Token: 0x06001F5A RID: 8026 RVA: 0x000CF0A8 File Offset: 0x000CD2A8
			internal void CopyFromImpl(ChangedProperty.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.Property = other.Property;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ObjectID.CopyFrom(other.ObjectID, context);
				}
			}

			// Token: 0x06001F5B RID: 8027 RVA: 0x000CF0F9 File Offset: 0x000CD2F9
			internal void CopyFromImpl(ChangedProperty.ObjectBody other)
			{
				this.Property = other.Property;
				this.ObjectID.CopyFrom(other.ObjectID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001F5C RID: 8028 RVA: 0x000CF11D File Offset: 0x000CD31D
			public override void CopyFrom(MetadataObjectBody<ChangedProperty> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((ChangedProperty.ObjectBody)other, context);
			}

			// Token: 0x06001F5D RID: 8029 RVA: 0x000CF134 File Offset: 0x000CD334
			internal bool IsEqualTo(ChangedProperty.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Property, other.Property) && this.ObjectID.IsEqualTo(other.ObjectID);
			}

			// Token: 0x06001F5E RID: 8030 RVA: 0x000CF161 File Offset: 0x000CD361
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((ChangedProperty.ObjectBody)other);
			}

			// Token: 0x06001F5F RID: 8031 RVA: 0x000CF17C File Offset: 0x000CD37C
			internal void CompareWith(ChangedProperty.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Property, other.Property))
				{
					context.RegisterPropertyChange(base.Owner, "Property", typeof(string), PropertyFlags.DdlAndUser, other.Property, this.Property);
				}
				this.ObjectID.CompareWith(other.ObjectID, "ObjectID", "Object", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06001F60 RID: 8032 RVA: 0x000CF1E1 File Offset: 0x000CD3E1
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((ChangedProperty.ObjectBody)other, context);
			}

			// Token: 0x04000783 RID: 1923
			internal string Property;

			// Token: 0x04000784 RID: 1924
			internal UntypedParentLink<ChangedProperty> ObjectID;
		}
	}
}
