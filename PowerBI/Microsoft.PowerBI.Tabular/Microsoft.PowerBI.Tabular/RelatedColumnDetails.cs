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
	// Token: 0x020000B0 RID: 176
	[CompatibilityRequirement(Pbi = "1400")]
	public sealed class RelatedColumnDetails : MetadataObject
	{
		// Token: 0x06000ABE RID: 2750 RVA: 0x0005866F File Offset: 0x0005686F
		public RelatedColumnDetails()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0005867D File Offset: 0x0005687D
		internal RelatedColumnDetails(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0005868B File Offset: 0x0005688B
		private void InitBodyAndCollections()
		{
			this.body = new RelatedColumnDetails.ObjectBody(this);
			this._GroupByColumns = new GroupByColumnCollection(this);
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x000586A5 File Offset: 0x000568A5
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.RelatedColumnDetails;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x000586A9 File Offset: 0x000568A9
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x000586BB File Offset: 0x000568BB
		public override MetadataObject Parent
		{
			get
			{
				return this.body.ColumnID.Object;
			}
			internal set
			{
				if (this.body.ColumnID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<RelatedColumnDetails, Column>(this.body.ColumnID, (Column)value, "RelatedColumnDetails", CompatibilityRestrictions.Column_RelatedColumnDetails);
				}
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x000586F0 File Offset: 0x000568F0
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ColumnID.ObjectID;
			}
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00058704 File Offset: 0x00056904
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.RelatedColumnDetails, null, "RelatedColumnDetails object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (CompatibilityRestrictions.GroupByColumn.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("groupByColumns", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "groupByColumns", MetadataPropertyNature.ChildCollection, ObjectType.GroupByColumn);
				}
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x000587A4 File Offset: 0x000569A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.RelatedColumnDetails[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x000587DB File Offset: 0x000569DB
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x000587E3 File Offset: 0x000569E3
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (RelatedColumnDetails.ObjectBody)value;
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000587F1 File Offset: 0x000569F1
		internal override ITxObjectBody CreateBody()
		{
			return new RelatedColumnDetails.ObjectBody(this);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000587F9 File Offset: 0x000569F9
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new RelatedColumnDetails();
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00058800 File Offset: 0x00056A00
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return null;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00058804 File Offset: 0x00056A04
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Column column = MetadataObject.ResolveMetadataObjectParentById<RelatedColumnDetails, Column>(this.body.ColumnID, objectMap, throwIfCantResolve, "RelatedColumnDetails", CompatibilityRestrictions.Column_RelatedColumnDetails);
			if (column != null && column.Model != null)
			{
				foreach (MetadataObject metadataObject in base.GetChildren(false))
				{
					column.Model.NotifySubtreeAdded(metadataObject);
				}
			}
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00058880 File Offset: 0x00056A80
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00058882 File Offset: 0x00056A82
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._GroupByColumns;
			yield break;
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00058892 File Offset: 0x00056A92
		public GroupByColumnCollection GroupByColumns
		{
			get
			{
				return this._GroupByColumns;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0005889A File Offset: 0x00056A9A
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x000588A8 File Offset: 0x00056AA8
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

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0005892C File Offset: 0x00056B2C
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x00058940 File Offset: 0x00056B40
		public Column Column
		{
			get
			{
				return this.body.ColumnID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ColumnID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Column", typeof(Column), this.body.ColumnID.Object, value);
					Column @object = this.body.ColumnID.Object;
					this.body.ColumnID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Column", typeof(Column), @object, value);
				}
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x000589C4 File Offset: 0x00056BC4
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x000589D6 File Offset: 0x00056BD6
		internal ObjectId _ColumnID
		{
			get
			{
				return this.body.ColumnID.ObjectID;
			}
			set
			{
				this.body.ColumnID.ObjectID = value;
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000589EC File Offset: 0x00056BEC
		internal void CopyFrom(RelatedColumnDetails other, CopyContext context)
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
			if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				this.GroupByColumns.CopyFrom(other.GroupByColumns, context);
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00058A9C File Offset: 0x00056C9C
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((RelatedColumnDetails)other, context);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00058AAB File Offset: 0x00056CAB
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(RelatedColumnDetails other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00058AC7 File Offset: 0x00056CC7
		public void CopyTo(RelatedColumnDetails other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00058AE3 File Offset: 0x00056CE3
		public RelatedColumnDetails Clone()
		{
			return base.CloneInternal<RelatedColumnDetails>();
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00058AEC File Offset: 0x00056CEC
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.RelatedColumnDetails.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object RelatedColumnDetails is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.ColumnID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ColumnID", this.body.ColumnID.Object);
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00058B68 File Offset: 0x00056D68
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ColumnID", out objectId))
			{
				this.body.ColumnID.ObjectID = objectId;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00058BBC File Offset: 0x00056DBC
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.RelatedColumnDetails.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object RelatedColumnDetails is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.ColumnID.Object != null && writer.ShouldIncludeProperty("ColumnID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("ColumnID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.ColumnID.Object);
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00058C5C File Offset: 0x00056E5C
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.RelatedColumnDetails.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object RelatedColumnDetails is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.GroupByColumns.Count > 0)
			{
				if (!CompatibilityRestrictions.GroupByColumn.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child GroupByColumn is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("groupByColumns", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "groupByColumns", MetadataPropertyNature.ChildCollection, this.GroupByColumns);
				}
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00058D70 File Offset: 0x00056F70
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName == "ColumnID")
			{
				this.body.ColumnID.ObjectID = reader.ReadObjectIdProperty();
				return true;
			}
			if (propertyName == "ModifiedTime" || propertyName == "modifiedTime")
			{
				this.body.ModifiedTime = reader.ReadDateTimeProperty();
				return true;
			}
			if (!(propertyName == "groupByColumns"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			if (!CompatibilityRestrictions.GroupByColumn.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				classification = UnexpectedPropertyClassification.IncompatibleProperty;
				return false;
			}
			using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
			{
				foreach (GroupByColumn groupByColumn in reader.ReadChildCollectionProperty<GroupByColumn>(context))
				{
					try
					{
						this.GroupByColumns.Add(groupByColumn);
					}
					catch (Exception ex)
					{
						throw reader.CreateInvalidChildException(context, groupByColumn, TomSR.Exception_FailedAddDeserializedObject("GroupByColumn", ex.Message), ex);
					}
				}
			}
			return true;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00058EAC File Offset: 0x000570AC
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.RelatedColumnDetails.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object RelatedColumnDetails is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 2, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && !options.IncludeTranslatablePropertiesOnly)
			{
				IEnumerable<GroupByColumn> enumerable;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<GroupByColumn> groupByColumns = this.GroupByColumns;
					enumerable = groupByColumns;
				}
				else
				{
					enumerable = this.GroupByColumns.Where((GroupByColumn o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<GroupByColumn> enumerable2 = enumerable;
				if (enumerable2.Any<GroupByColumn>())
				{
					if (!CompatibilityRestrictions.GroupByColumn.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("A child GroupByColumn is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					object[] array = enumerable2.Select((GroupByColumn obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array2 = array;
					result["groupByColumns", TomPropCategory.ChildCollection, 44, false] = array2;
				}
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00059064 File Offset: 0x00057264
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "modifiedTime")
			{
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			if (!(name == "groupByColumns"))
			{
				return false;
			}
			if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.GroupByColumn.IsCompatible(mode, dbCompatibilityLevel))
			{
				return false;
			}
			JsonPropertyHelper.ReadObjectCollection(this.GroupByColumns, jsonProp.Value, options, mode, dbCompatibilityLevel);
			return true;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x000590E4 File Offset: 0x000572E4
		internal override string GetFormattedObjectPath()
		{
			if (this.Column == null)
			{
				return TomSR.ObjectPath_RelatedColumnDetails_0Args;
			}
			if (this.Column.Table != null)
			{
				return TomSR.ObjectPath_RelatedColumnDetails_2Args(this.Column.Name, this.Column.Table.Name);
			}
			return TomSR.ObjectPath_RelatedColumnDetails_1Args(this.Column.Name);
		}

		// Token: 0x04000168 RID: 360
		internal RelatedColumnDetails.ObjectBody body;

		// Token: 0x04000169 RID: 361
		private GroupByColumnCollection _GroupByColumns;

		// Token: 0x020002C2 RID: 706
		internal class ObjectBody : MetadataObjectBody<RelatedColumnDetails>
		{
			// Token: 0x060022BD RID: 8893 RVA: 0x000DE813 File Offset: 0x000DCA13
			public ObjectBody(RelatedColumnDetails owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.ColumnID = new ParentLink<RelatedColumnDetails, Column>(owner, "Column");
			}

			// Token: 0x060022BE RID: 8894 RVA: 0x000DE838 File Offset: 0x000DCA38
			internal bool IsEqualTo(RelatedColumnDetails.ObjectBody other, CopyContext context)
			{
				return ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ColumnID.IsEqualTo(other.ColumnID, context));
			}

			// Token: 0x060022BF RID: 8895 RVA: 0x000DE898 File Offset: 0x000DCA98
			internal void CopyFromImpl(RelatedColumnDetails.ObjectBody other, CopyContext context)
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
					this.ColumnID.CopyFrom(other.ColumnID, context);
				}
			}

			// Token: 0x060022C0 RID: 8896 RVA: 0x000DE8FC File Offset: 0x000DCAFC
			internal void CopyFromImpl(RelatedColumnDetails.ObjectBody other)
			{
				this.ModifiedTime = other.ModifiedTime;
				this.ColumnID.CopyFrom(other.ColumnID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x060022C1 RID: 8897 RVA: 0x000DE920 File Offset: 0x000DCB20
			public override void CopyFrom(MetadataObjectBody<RelatedColumnDetails> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((RelatedColumnDetails.ObjectBody)other, context);
			}

			// Token: 0x060022C2 RID: 8898 RVA: 0x000DE937 File Offset: 0x000DCB37
			internal bool IsEqualTo(RelatedColumnDetails.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.ColumnID.IsEqualTo(other.ColumnID);
			}

			// Token: 0x060022C3 RID: 8899 RVA: 0x000DE964 File Offset: 0x000DCB64
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((RelatedColumnDetails.ObjectBody)other);
			}

			// Token: 0x060022C4 RID: 8900 RVA: 0x000DE980 File Offset: 0x000DCB80
			internal void CompareWith(RelatedColumnDetails.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.ColumnID.CompareWith(other.ColumnID, "ColumnID", "Column", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x060022C5 RID: 8901 RVA: 0x000DE9EF File Offset: 0x000DCBEF
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((RelatedColumnDetails.ObjectBody)other, context);
			}

			// Token: 0x04000A02 RID: 2562
			internal DateTime ModifiedTime;

			// Token: 0x04000A03 RID: 2563
			internal ParentLink<RelatedColumnDetails, Column> ColumnID;
		}
	}
}
