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
	// Token: 0x02000061 RID: 97
	[CompatibilityRequirement(Pbi = "1400")]
	public sealed class GroupByColumn : MetadataObject, ILinkedMetadataObject
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x00026CA8 File Offset: 0x00024EA8
		public GroupByColumn()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00026CB6 File Offset: 0x00024EB6
		internal GroupByColumn(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00026CC4 File Offset: 0x00024EC4
		private void InitBodyAndCollections()
		{
			this.body = new GroupByColumn.ObjectBody(this);
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00026CD2 File Offset: 0x00024ED2
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.GroupByColumn;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00026CD6 File Offset: 0x00024ED6
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x00026CE8 File Offset: 0x00024EE8
		public override MetadataObject Parent
		{
			get
			{
				return this.body.RelatedColumnDetailsID.Object;
			}
			internal set
			{
				if (this.body.RelatedColumnDetailsID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<GroupByColumn, RelatedColumnDetails>(this.body.RelatedColumnDetailsID, (RelatedColumnDetails)value, null, null);
				}
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00026D15 File Offset: 0x00024F15
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.RelatedColumnDetailsID.ObjectID;
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00026D28 File Offset: 0x00024F28
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.GroupByColumn, null, "GroupByColumn object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("groupingColumn", MetadataPropertyNature.CrossLinkProperty))
				{
					CrossLink<GroupByColumn, Column>.WriteMetadataSchema(ObjectType.Column, ObjectType.Column, true, "groupingColumn", false, writer);
				}
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00026DB0 File Offset: 0x00024FB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.GroupByColumn[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00026DE7 File Offset: 0x00024FE7
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x00026DEF File Offset: 0x00024FEF
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (GroupByColumn.ObjectBody)value;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00026DFD File Offset: 0x00024FFD
		internal override ITxObjectBody CreateBody()
		{
			return new GroupByColumn.ObjectBody(this);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00026E05 File Offset: 0x00025005
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new GroupByColumn();
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00026E0C File Offset: 0x0002500C
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((RelatedColumnDetails)parent).GroupByColumns;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00026E1C File Offset: 0x0002501C
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			RelatedColumnDetails relatedColumnDetails = MetadataObject.ResolveMetadataObjectParentById<GroupByColumn, RelatedColumnDetails>(this.body.RelatedColumnDetailsID, objectMap, throwIfCantResolve, null, null);
			this.body.GroupingColumnID.ResolveById(objectMap, throwIfCantResolve);
			if (relatedColumnDetails != null)
			{
				relatedColumnDetails.GroupByColumns.Add(this);
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00026E60 File Offset: 0x00025060
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.GroupingColumnID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00026E78 File Offset: 0x00025078
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.GroupingColumnID.IsResolved && !this.body.GroupingColumnID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "GroupingColumn"));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00026ECC File Offset: 0x000250CC
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			this.body.GroupingColumnID.TryResolveAfterCopy(copyContext);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00026EE0 File Offset: 0x000250E0
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.GroupingColumnID.Validate(result, throwOnError);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00026EF4 File Offset: 0x000250F4
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.GroupingColumnID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00026F10 File Offset: 0x00025110
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00026F20 File Offset: 0x00025120
		public DateTime ModifiedTime
		{
			get
			{
				return this.body.ModifiedTime;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ModifiedTime, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ModifiedTime", typeof(DateTime), this.body.ModifiedTime, value);
					DateTime modifiedTime = this.body.ModifiedTime;
					this.body.ModifiedTime = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ModifiedTime", typeof(DateTime), modifiedTime, value);
				}
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00026FA4 File Offset: 0x000251A4
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00026FB8 File Offset: 0x000251B8
		public RelatedColumnDetails RelatedColumnDetails
		{
			get
			{
				return this.body.RelatedColumnDetailsID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RelatedColumnDetailsID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "RelatedColumnDetails", typeof(RelatedColumnDetails), this.body.RelatedColumnDetailsID.Object, value);
					RelatedColumnDetails @object = this.body.RelatedColumnDetailsID.Object;
					this.body.RelatedColumnDetailsID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RelatedColumnDetails", typeof(RelatedColumnDetails), @object, value);
				}
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0002703C File Offset: 0x0002523C
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0002704E File Offset: 0x0002524E
		internal ObjectId _RelatedColumnDetailsID
		{
			get
			{
				return this.body.RelatedColumnDetailsID.ObjectID;
			}
			set
			{
				this.body.RelatedColumnDetailsID.ObjectID = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00027061 File Offset: 0x00025261
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00027074 File Offset: 0x00025274
		public Column GroupingColumn
		{
			get
			{
				return this.body.GroupingColumnID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.GroupingColumnID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "GroupingColumn", typeof(Column), this.body.GroupingColumnID.Object, value);
					Column @object = this.body.GroupingColumnID.Object;
					this.body.GroupingColumnID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "GroupingColumn", typeof(Column), @object, value);
				}
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x000270F8 File Offset: 0x000252F8
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0002710A File Offset: 0x0002530A
		internal ObjectId _GroupingColumnID
		{
			get
			{
				return this.body.GroupingColumnID.ObjectID;
			}
			set
			{
				this.body.GroupingColumnID.ObjectID = value;
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00027120 File Offset: 0x00025320
		internal void CopyFrom(GroupByColumn other, CopyContext context)
		{
			base.CopyFrom(other, context);
			bool flag;
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions)
			{
				flag = true;
			}
			else if ((context.Flags & CopyFlags.MetadataSync) == CopyFlags.MetadataSync)
			{
				flag = this.body.ModifiedTime.CompareTo(other.body.ModifiedTime) != 0;
			}
			else
			{
				flag = !this.body.IsEqualTo(other.body, context);
			}
			if (flag)
			{
				ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				this.body.CopyFrom(other.body, context);
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000271B1 File Offset: 0x000253B1
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((GroupByColumn)other, context);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000271C0 File Offset: 0x000253C0
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(GroupByColumn other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000271DC File Offset: 0x000253DC
		public void CopyTo(GroupByColumn other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000271F8 File Offset: 0x000253F8
		public GroupByColumn Clone()
		{
			return base.CloneInternal<GroupByColumn>();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00027200 File Offset: 0x00025400
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.GroupByColumn.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object GroupByColumn is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.RelatedColumnDetailsID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "RelatedColumnDetailsID", this.body.RelatedColumnDetailsID.Object);
			}
			this.body.GroupingColumnID.Validate(null, true);
			if (this.body.GroupingColumnID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "GroupingColumnID", this.body.GroupingColumnID.Object);
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000272BC File Offset: 0x000254BC
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("RelatedColumnDetailsID", out objectId))
			{
				this.body.RelatedColumnDetailsID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("GroupingColumnID", out objectId2))
			{
				this.body.GroupingColumnID.ObjectID = objectId2;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00027330 File Offset: 0x00025530
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.GroupByColumn.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object GroupByColumn is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.RelatedColumnDetailsID.Object != null && writer.ShouldIncludeProperty("RelatedColumnDetailsID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("RelatedColumnDetailsID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.RelatedColumnDetailsID.Object);
			}
			this.body.GroupingColumnID.Validate(null, true);
			if (this.body.GroupingColumnID.Object != null && writer.ShouldIncludeProperty("GroupingColumnID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("GroupingColumnID", MetadataPropertyNature.CrossLinkProperty, this.body.GroupingColumnID.Object);
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0002741C File Offset: 0x0002561C
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.GroupByColumn.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object GroupByColumn is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.body.GroupingColumnID.Object != null && writer.ShouldIncludeProperty("groupingColumn", MetadataPropertyNature.CrossLinkProperty))
			{
				this.body.GroupingColumnID.WriteToMetadataStream(ObjectType.Column, true, "groupingColumn", false, writer);
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000274F4 File Offset: 0x000256F4
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName == "RelatedColumnDetailsID")
			{
				this.body.RelatedColumnDetailsID.ObjectID = reader.ReadObjectIdProperty();
				return true;
			}
			if (propertyName == "GroupingColumnID")
			{
				this.body.GroupingColumnID.ObjectID = reader.ReadObjectIdProperty();
				return true;
			}
			if (propertyName == "ModifiedTime" || propertyName == "modifiedTime")
			{
				this.body.ModifiedTime = reader.ReadDateTimeProperty();
				return true;
			}
			if (!(propertyName == "groupingColumn"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			this.body.GroupingColumnID.Path = reader.ReadCrossLinkProperty((string p) => new ObjectPath(ObjectType.Column, p));
			return true;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000275D8 File Offset: 0x000257D8
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.GroupByColumn.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object GroupByColumn is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 3, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.GroupingColumnID.Object != null)
			{
				this.body.GroupingColumnID.SerializeToJsonObject(true, "groupingColumn", ObjectType.Column, result, 2, false);
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0002769C File Offset: 0x0002589C
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "modifiedTime")
			{
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			if (!(name == "groupingColumn"))
			{
				return false;
			}
			this.body.GroupingColumnID.Path = new ObjectPath(ObjectType.Column, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
			return true;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00027708 File Offset: 0x00025908
		void ILinkedMetadataObject.GetLinkedObjectTarget(out ObjectId objectId, out ObjectPath objectPath, out MetadataObject @object, out string property)
		{
			objectId = this.body.GroupingColumnID.ObjectID;
			objectPath = this.body.GroupingColumnID.Path;
			@object = this.body.GroupingColumnID.Object;
			property = null;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00027748 File Offset: 0x00025948
		internal override string GetFormattedObjectPath()
		{
			if (this.RelatedColumnDetails == null || this.RelatedColumnDetails.Column == null)
			{
				return TomSR.ObjectPath_GroupByColumn_0Args;
			}
			if (this.RelatedColumnDetails.Column.Table != null)
			{
				return TomSR.ObjectPath_GroupByColumn_2Args(this.RelatedColumnDetails.Column.Name, this.RelatedColumnDetails.Column.Table.Name);
			}
			return TomSR.ObjectPath_GroupByColumn_1Args(this.RelatedColumnDetails.Column.Name);
		}

		// Token: 0x04000100 RID: 256
		internal GroupByColumn.ObjectBody body;

		// Token: 0x0200026A RID: 618
		internal class ObjectBody : MetadataObjectBody<GroupByColumn>
		{
			// Token: 0x06002054 RID: 8276 RVA: 0x000D45C4 File Offset: 0x000D27C4
			public ObjectBody(GroupByColumn owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.RelatedColumnDetailsID = new ParentLink<GroupByColumn, RelatedColumnDetails>(owner, "RelatedColumnDetails");
				this.GroupingColumnID = new CrossLink<GroupByColumn, Column>(owner, "GroupingColumn");
			}

			// Token: 0x06002055 RID: 8277 RVA: 0x000D45FC File Offset: 0x000D27FC
			internal bool IsEqualTo(GroupByColumn.ObjectBody other, CopyContext context)
			{
				return ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.RelatedColumnDetailsID.IsEqualTo(other.RelatedColumnDetailsID, context)) && this.GroupingColumnID.IsEqualTo(other.GroupingColumnID, context);
			}

			// Token: 0x06002056 RID: 8278 RVA: 0x000D4674 File Offset: 0x000D2874
			internal void CopyFromImpl(GroupByColumn.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.RelatedColumnDetailsID.CopyFrom(other.RelatedColumnDetailsID, context);
				}
				this.GroupingColumnID.CopyFrom(other.GroupingColumnID, context);
			}

			// Token: 0x06002057 RID: 8279 RVA: 0x000D46EA File Offset: 0x000D28EA
			internal void CopyFromImpl(GroupByColumn.ObjectBody other)
			{
				this.ModifiedTime = other.ModifiedTime;
				this.RelatedColumnDetailsID.CopyFrom(other.RelatedColumnDetailsID, ObjectChangeTracker.BodyCloneContext);
				this.GroupingColumnID.CopyFrom(other.GroupingColumnID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06002058 RID: 8280 RVA: 0x000D4724 File Offset: 0x000D2924
			public override void CopyFrom(MetadataObjectBody<GroupByColumn> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((GroupByColumn.ObjectBody)other, context);
			}

			// Token: 0x06002059 RID: 8281 RVA: 0x000D473C File Offset: 0x000D293C
			internal bool IsEqualTo(GroupByColumn.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.RelatedColumnDetailsID.IsEqualTo(other.RelatedColumnDetailsID) && this.GroupingColumnID.IsEqualTo(other.GroupingColumnID);
			}

			// Token: 0x0600205A RID: 8282 RVA: 0x000D4789 File Offset: 0x000D2989
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((GroupByColumn.ObjectBody)other);
			}

			// Token: 0x0600205B RID: 8283 RVA: 0x000D47A4 File Offset: 0x000D29A4
			internal void CompareWith(GroupByColumn.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.RelatedColumnDetailsID.CompareWith(other.RelatedColumnDetailsID, "RelatedColumnDetailsID", "RelatedColumnDetails", PropertyFlags.ReadOnly, context);
				this.GroupingColumnID.CompareWith(other.GroupingColumnID, "GroupingColumnID", "GroupingColumn", PropertyFlags.None, context);
			}

			// Token: 0x0600205C RID: 8284 RVA: 0x000D4830 File Offset: 0x000D2A30
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((GroupByColumn.ObjectBody)other, context);
			}

			// Token: 0x04000848 RID: 2120
			internal DateTime ModifiedTime;

			// Token: 0x04000849 RID: 2121
			internal ParentLink<GroupByColumn, RelatedColumnDetails> RelatedColumnDetailsID;

			// Token: 0x0400084A RID: 2122
			internal CrossLink<GroupByColumn, Column> GroupingColumnID;
		}
	}
}
