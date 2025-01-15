using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000AA RID: 170
	[CompatibilityRequirement("1480")]
	public sealed class QueryGroup : NamedMetadataObject
	{
		// Token: 0x06000A5A RID: 2650 RVA: 0x000559FB File Offset: 0x00053BFB
		public QueryGroup()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00055A0E File Offset: 0x00053C0E
		internal QueryGroup(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00055A1D File Offset: 0x00053C1D
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new QueryGroup.ObjectBody(this);
			this.body.Folder = string.Empty;
			this.body.Description = string.Empty;
			this._Annotations = new QueryGroupAnnotationCollection(this, comparer);
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00055A58 File Offset: 0x00053C58
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.QueryGroup;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00055A5C File Offset: 0x00053C5C
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x00055A6E File Offset: 0x00053C6E
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
					MetadataObject.UpdateMetadataObjectParent<QueryGroup, Model>(this.body.ModelID, (Model)value, null, null);
				}
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00055A9B File Offset: 0x00053C9B
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00055AB0 File Offset: 0x00053CB0
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.QueryGroup, null, "QueryGroup object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("folder", MetadataPropertyNature.NameProperty))
				{
					writer.WriteProperty("folder", MetadataPropertyNature.NameProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00055B5C File Offset: 0x00053D5C
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.QueryGroup[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00055B93 File Offset: 0x00053D93
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x00055B9B File Offset: 0x00053D9B
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (QueryGroup.ObjectBody)value;
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00055BA9 File Offset: 0x00053DA9
		internal override ITxObjectBody CreateBody()
		{
			return new QueryGroup.ObjectBody(this);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00055BB1 File Offset: 0x00053DB1
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new QueryGroup();
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00055BB8 File Offset: 0x00053DB8
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Model)parent).QueryGroups;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00055BC8 File Offset: 0x00053DC8
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Model model = MetadataObject.ResolveMetadataObjectParentById<QueryGroup, Model>(this.body.ModelID, objectMap, throwIfCantResolve, null, null);
			if (model != null)
			{
				model.QueryGroups.Add(this);
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00055BF9 File Offset: 0x00053DF9
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00055BFB File Offset: 0x00053DFB
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield break;
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00055C0B File Offset: 0x00053E0B
		public QueryGroupAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00055C13 File Offset: 0x00053E13
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x00055C20 File Offset: 0x00053E20
		public override string Name
		{
			get
			{
				return this.body.Folder;
			}
			set
			{
				throw new InvalidOperationException(TomSR.Exception_NameCannotBeSetForReadOnlyNamedObjects);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00055C2C File Offset: 0x00053E2C
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x00055C3C File Offset: 0x00053E3C
		public string Folder
		{
			get
			{
				return this.body.Folder;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Folder, value))
				{
					string text;
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.QueryGroup, out text))
					{
						throw new ArgumentException(text);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Folder", typeof(string), this.body.Folder, value);
					string folder = this.body.Folder;
					this.body.Folder = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Folder", typeof(string), folder, value);
				}
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00055CBF File Offset: 0x00053EBF
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x00055CCC File Offset: 0x00053ECC
		public string Description
		{
			get
			{
				return this.body.Description;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Description, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Description", typeof(string), this.body.Description, value);
					string description = this.body.Description;
					this.body.Description = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Description", typeof(string), description, value);
				}
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00055D3C File Offset: 0x00053F3C
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x00055D4E File Offset: 0x00053F4E
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

		// Token: 0x06000A74 RID: 2676 RVA: 0x00055D64 File Offset: 0x00053F64
		internal void CopyFrom(QueryGroup other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions || !this.body.IsEqualTo(other.body, context))
			{
				ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				this.body.CopyFrom(other.body, context);
			}
			if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				this.Annotations.CopyFrom(other.Annotations, context);
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00055DD7 File Offset: 0x00053FD7
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((QueryGroup)other, context);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00055DE6 File Offset: 0x00053FE6
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(QueryGroup other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00055E02 File Offset: 0x00054002
		public void CopyTo(QueryGroup other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00055E1E File Offset: 0x0005401E
		public QueryGroup Clone()
		{
			return base.CloneInternal<QueryGroup>();
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00055E28 File Offset: 0x00054028
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object QueryGroup is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (!string.IsNullOrEmpty(this.body.Folder))
			{
				writer.WriteProperty<string>(options, "Folder", this.body.Folder);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00055EC8 File Offset: 0x000540C8
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ModelID", out objectId))
			{
				this.body.ModelID.ObjectID = objectId;
			}
			string text;
			if (reader.TryReadProperty<string>("Folder", out text))
			{
				this.body.Folder = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("Description", out text2))
			{
				this.body.Description = text2;
			}
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00055F34 File Offset: 0x00054134
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object QueryGroup is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (!string.IsNullOrEmpty(this.body.Folder) && writer.ShouldIncludeProperty("Folder", MetadataPropertyNature.NameProperty))
			{
				writer.WriteStringProperty("Folder", MetadataPropertyNature.NameProperty, this.body.Folder);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00056004 File Offset: 0x00054204
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object QueryGroup is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Folder) && writer.ShouldIncludeProperty("folder", MetadataPropertyNature.NameProperty))
			{
				writer.WriteStringProperty("folder", MetadataPropertyNature.NameProperty, this.body.Folder);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00056104 File Offset: 0x00054304
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
			if (propertyName == "Folder" || propertyName == "folder")
			{
				this.body.Folder = reader.ReadStringProperty();
				return true;
			}
			if (propertyName == "Description" || propertyName == "description")
			{
				this.body.Description = reader.ReadStringProperty();
				return true;
			}
			if (!(propertyName == "annotations"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
			{
				foreach (Annotation annotation in reader.ReadChildCollectionProperty<Annotation>(context))
				{
					try
					{
						this.Annotations.Add(annotation);
					}
					catch (Exception ex)
					{
						throw reader.CreateInvalidChildException(context, annotation, TomSR.Exception_FailedAddDeserializedNamedObject("Annotation", (annotation != null) ? annotation.Name : null, ex.Message), ex);
					}
				}
			}
			return true;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0005625C File Offset: 0x0005445C
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object QueryGroup is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Folder))
			{
				result["folder", TomPropCategory.Regular, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Folder, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x000563A4 File Offset: 0x000545A4
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "folder")
			{
				this.body.Folder = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "description")
			{
				this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (!(name == "annotations"))
			{
				return false;
			}
			JsonPropertyHelper.ReadObjectCollection(this.Annotations, jsonProp.Value, options, mode, dbCompatibilityLevel);
			return true;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00056429 File Offset: 0x00054629
		internal override string GetFormattedObjectPath()
		{
			if (string.IsNullOrEmpty(this.Folder))
			{
				return TomSR.ObjectPath_QueryGroup_0Args;
			}
			return TomSR.ObjectPath_QueryGroup_1Args_Folder(this.Folder);
		}

		// Token: 0x04000163 RID: 355
		internal QueryGroup.ObjectBody body;

		// Token: 0x04000164 RID: 356
		private QueryGroupAnnotationCollection _Annotations;

		// Token: 0x020002BB RID: 699
		internal class ObjectBody : NamedMetadataObjectBody<QueryGroup>
		{
			// Token: 0x06002292 RID: 8850 RVA: 0x000DDD7F File Offset: 0x000DBF7F
			public ObjectBody(QueryGroup owner)
				: base(owner)
			{
				this.ModelID = new ParentLink<QueryGroup, Model>(owner, "Model");
			}

			// Token: 0x06002293 RID: 8851 RVA: 0x000DDD99 File Offset: 0x000DBF99
			public override string GetObjectName()
			{
				return this.Folder;
			}

			// Token: 0x06002294 RID: 8852 RVA: 0x000DDDA4 File Offset: 0x000DBFA4
			internal bool IsEqualTo(QueryGroup.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Folder, other.Folder) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ModelID.IsEqualTo(other.ModelID, context));
			}

			// Token: 0x06002295 RID: 8853 RVA: 0x000DDE08 File Offset: 0x000DC008
			internal void CopyFromImpl(QueryGroup.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.Folder = other.Folder;
				this.Description = other.Description;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModelID.CopyFrom(other.ModelID, context);
				}
			}

			// Token: 0x06002296 RID: 8854 RVA: 0x000DDE65 File Offset: 0x000DC065
			internal void CopyFromImpl(QueryGroup.ObjectBody other)
			{
				this.Folder = other.Folder;
				this.Description = other.Description;
				this.ModelID.CopyFrom(other.ModelID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06002297 RID: 8855 RVA: 0x000DDE95 File Offset: 0x000DC095
			public override void CopyFrom(MetadataObjectBody<QueryGroup> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((QueryGroup.ObjectBody)other, context);
			}

			// Token: 0x06002298 RID: 8856 RVA: 0x000DDEAC File Offset: 0x000DC0AC
			internal bool IsEqualTo(QueryGroup.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Folder, other.Folder) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && this.ModelID.IsEqualTo(other.ModelID);
			}

			// Token: 0x06002299 RID: 8857 RVA: 0x000DDEF9 File Offset: 0x000DC0F9
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((QueryGroup.ObjectBody)other);
			}

			// Token: 0x0600229A RID: 8858 RVA: 0x000DDF14 File Offset: 0x000DC114
			internal void CompareWith(QueryGroup.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Folder, other.Folder))
				{
					context.RegisterPropertyChange(base.Owner, "Folder", typeof(string), PropertyFlags.DdlAndUser, other.Folder, this.Folder);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				this.ModelID.CompareWith(other.ModelID, "ModelID", "Model", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x0600229B RID: 8859 RVA: 0x000DDFB4 File Offset: 0x000DC1B4
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((QueryGroup.ObjectBody)other, context);
			}

			// Token: 0x040009E5 RID: 2533
			internal string Folder;

			// Token: 0x040009E6 RID: 2534
			internal string Description;

			// Token: 0x040009E7 RID: 2535
			internal ParentLink<QueryGroup, Model> ModelID;
		}
	}
}
