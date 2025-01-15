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
	// Token: 0x02000028 RID: 40
	[CompatibilityRequirement("1460")]
	public sealed class AlternateOf : MetadataObject
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AlternateOf()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002063 File Offset: 0x00000263
		internal AlternateOf(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002072 File Offset: 0x00000272
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new AlternateOf.ObjectBody(this);
			this.body.Summarization = SummarizationType.GroupBy;
			this._Annotations = new AlternateOfAnnotationCollection(this, comparer);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002099 File Offset: 0x00000299
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.AlternateOf;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000209D File Offset: 0x0000029D
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020AF File Offset: 0x000002AF
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
					MetadataObject.UpdateMetadataObjectParent<AlternateOf, Column>(this.body.ColumnID, (Column)value, "AlternateOf", CompatibilityRestrictions.Column_AlternateOf);
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020E4 File Offset: 0x000002E4
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ColumnID.ObjectID;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020F8 File Offset: 0x000002F8
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.AlternateOf, null, "AlternateOf object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("summarization", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<SummarizationType>("summarization", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("baseColumn", MetadataPropertyNature.CrossLinkProperty))
				{
					CrossLink<AlternateOf, Column>.WriteMetadataSchema(ObjectType.Column, ObjectType.Table, true, "baseColumn", false, writer);
				}
				if (writer.ShouldIncludeProperty("baseTable", MetadataPropertyNature.CrossLinkProperty))
				{
					CrossLink<AlternateOf, Table>.WriteMetadataSchema(ObjectType.Table, ObjectType.Table, true, "baseTable", false, writer);
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A8 File Offset: 0x000003A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.AlternateOf[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.Summarization != SummarizationType.GroupBy)
			{
				int num = PropertyHelper.GetSummarizationTypeCompatibilityRestrictions(this.body.Summarization)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Summarization");
					requiredLevel = num;
					int num2 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000223B File Offset: 0x0000043B
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002243 File Offset: 0x00000443
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (AlternateOf.ObjectBody)value;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002251 File Offset: 0x00000451
		internal override ITxObjectBody CreateBody()
		{
			return new AlternateOf.ObjectBody(this);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002259 File Offset: 0x00000459
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new AlternateOf();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002260 File Offset: 0x00000460
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return null;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002264 File Offset: 0x00000464
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Column column = MetadataObject.ResolveMetadataObjectParentById<AlternateOf, Column>(this.body.ColumnID, objectMap, throwIfCantResolve, "AlternateOf", CompatibilityRestrictions.Column_AlternateOf);
			this.body.BaseColumnID.ResolveById(objectMap, throwIfCantResolve);
			this.body.BaseTableID.ResolveById(objectMap, throwIfCantResolve);
			if (column != null && column.Model != null)
			{
				foreach (MetadataObject metadataObject in base.GetChildren(false))
				{
					column.Model.NotifySubtreeAdded(metadataObject);
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002308 File Offset: 0x00000508
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.BaseColumnID.ResolveById(objectMap, throwIfCantResolve);
			this.body.BaseTableID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002330 File Offset: 0x00000530
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.BaseColumnID.IsResolved && !this.body.BaseColumnID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "BaseColumn"));
				}
				flag = false;
			}
			if (!this.body.BaseTableID.IsResolved && !this.body.BaseTableID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "BaseTable"));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023C8 File Offset: 0x000005C8
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			this.body.BaseColumnID.TryResolveAfterCopy(copyContext);
			this.body.BaseTableID.TryResolveAfterCopy(copyContext);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023EE File Offset: 0x000005EE
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.BaseColumnID.Validate(result, throwOnError);
			this.body.BaseTableID.Validate(result, throwOnError);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002414 File Offset: 0x00000614
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.BaseColumnID.IsResolved || !this.body.BaseTableID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002444 File Offset: 0x00000644
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield break;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002454 File Offset: 0x00000654
		public AlternateOfAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000245C File Offset: 0x0000065C
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000246C File Offset: 0x0000066C
		public SummarizationType Summarization
		{
			get
			{
				return this.body.Summarization;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Summarization, value))
				{
					CompatibilityRestrictionSet summarizationTypeCompatibilityRestrictions = PropertyHelper.GetSummarizationTypeCompatibilityRestrictions(value);
					CompatibilityRestrictionSet summarizationTypeCompatibilityRestrictions2 = PropertyHelper.GetSummarizationTypeCompatibilityRestrictions(this.body.Summarization);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = summarizationTypeCompatibilityRestrictions.Compare(summarizationTypeCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != SummarizationType.GroupBy))
					{
						array = base.ValidateCompatibilityRequirement(summarizationTypeCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "Summarization", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Summarization", typeof(SummarizationType), this.body.Summarization, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(summarizationTypeCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(summarizationTypeCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(summarizationTypeCompatibilityRestrictions, array);
						break;
					}
					SummarizationType summarization = this.body.Summarization;
					this.body.Summarization = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Summarization", typeof(SummarizationType), summarization, value);
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000258D File Offset: 0x0000078D
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000025A0 File Offset: 0x000007A0
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002624 File Offset: 0x00000824
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002636 File Offset: 0x00000836
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002649 File Offset: 0x00000849
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000265C File Offset: 0x0000085C
		public Column BaseColumn
		{
			get
			{
				return this.body.BaseColumnID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.BaseColumnID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "BaseColumn", typeof(Column), this.body.BaseColumnID.Object, value);
					Column @object = this.body.BaseColumnID.Object;
					this.body.BaseColumnID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "BaseColumn", typeof(Column), @object, value);
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000026E0 File Offset: 0x000008E0
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000026F2 File Offset: 0x000008F2
		internal ObjectId _BaseColumnID
		{
			get
			{
				return this.body.BaseColumnID.ObjectID;
			}
			set
			{
				this.body.BaseColumnID.ObjectID = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002705 File Offset: 0x00000905
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002718 File Offset: 0x00000918
		public Table BaseTable
		{
			get
			{
				return this.body.BaseTableID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.BaseTableID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "BaseTable", typeof(Table), this.body.BaseTableID.Object, value);
					Table @object = this.body.BaseTableID.Object;
					this.body.BaseTableID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "BaseTable", typeof(Table), @object, value);
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000279C File Offset: 0x0000099C
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000027AE File Offset: 0x000009AE
		internal ObjectId _BaseTableID
		{
			get
			{
				return this.body.BaseTableID.ObjectID;
			}
			set
			{
				this.body.BaseTableID.ObjectID = value;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027C4 File Offset: 0x000009C4
		internal void CopyFrom(AlternateOf other, CopyContext context)
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

		// Token: 0x06000026 RID: 38 RVA: 0x00002837 File Offset: 0x00000A37
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((AlternateOf)other, context);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002846 File Offset: 0x00000A46
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(AlternateOf other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002862 File Offset: 0x00000A62
		public void CopyTo(AlternateOf other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000287E File Offset: 0x00000A7E
		public AlternateOf Clone()
		{
			return base.CloneInternal<AlternateOf>();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002888 File Offset: 0x00000A88
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.AlternateOf.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object AlternateOf is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.ColumnID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ColumnID", this.body.ColumnID.Object);
			}
			this.body.BaseColumnID.Validate(null, true);
			if (this.body.BaseColumnID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "BaseColumnID", this.body.BaseColumnID.Object);
			}
			this.body.BaseTableID.Validate(null, true);
			if (this.body.BaseTableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "BaseTableID", this.body.BaseTableID.Object);
			}
			if (this.body.Summarization != SummarizationType.GroupBy)
			{
				if (!PropertyHelper.IsSummarizationTypeValueCompatible(this.body.Summarization, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Summarization is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<SummarizationType>(options, "Summarization", this.body.Summarization);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000029E0 File Offset: 0x00000BE0
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ColumnID", out objectId))
			{
				this.body.ColumnID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("BaseColumnID", out objectId2))
			{
				this.body.BaseColumnID.ObjectID = objectId2;
			}
			ObjectId objectId3;
			if (reader.TryReadProperty<ObjectId>("BaseTableID", out objectId3))
			{
				this.body.BaseTableID.ObjectID = objectId3;
			}
			SummarizationType summarizationType;
			if (reader.TryReadProperty<SummarizationType>("Summarization", out summarizationType))
			{
				this.body.Summarization = summarizationType;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A74 File Offset: 0x00000C74
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.AlternateOf.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object AlternateOf is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.ColumnID.Object != null && writer.ShouldIncludeProperty("ColumnID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("ColumnID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.ColumnID.Object);
			}
			this.body.BaseColumnID.Validate(null, true);
			if (this.body.BaseColumnID.Object != null && writer.ShouldIncludeProperty("BaseColumnID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("BaseColumnID", MetadataPropertyNature.CrossLinkProperty, this.body.BaseColumnID.Object);
			}
			this.body.BaseTableID.Validate(null, true);
			if (this.body.BaseTableID.Object != null && writer.ShouldIncludeProperty("BaseTableID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("BaseTableID", MetadataPropertyNature.CrossLinkProperty, this.body.BaseTableID.Object);
			}
			if (this.body.Summarization != SummarizationType.GroupBy)
			{
				if (!PropertyHelper.IsSummarizationTypeValueCompatible(this.body.Summarization, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Summarization is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("Summarization", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<SummarizationType>("Summarization", MetadataPropertyNature.RegularProperty, this.body.Summarization);
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C2C File Offset: 0x00000E2C
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.AlternateOf.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object AlternateOf is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (this.body.Summarization != SummarizationType.GroupBy)
			{
				if (!PropertyHelper.IsSummarizationTypeValueCompatible(this.body.Summarization, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Summarization is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("summarization", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<SummarizationType>("summarization", MetadataPropertyNature.RegularProperty, this.body.Summarization);
				}
			}
			if (this.body.BaseColumnID.Object != null && writer.ShouldIncludeProperty("baseColumn", MetadataPropertyNature.CrossLinkProperty))
			{
				this.body.BaseColumnID.WriteToMetadataStream(ObjectType.Table, true, "baseColumn", false, writer);
			}
			if (this.body.BaseTableID.Object != null && writer.ShouldIncludeProperty("baseTable", MetadataPropertyNature.CrossLinkProperty))
			{
				this.body.BaseTableID.WriteToMetadataStream(ObjectType.Table, true, "baseTable", false, writer);
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002DA4 File Offset: 0x00000FA4
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName != null)
			{
				switch (propertyName.Length)
				{
				case 8:
					if (propertyName == "ColumnID")
					{
						this.body.ColumnID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 9:
					if (propertyName == "baseTable")
					{
						this.body.BaseTableID.Path = reader.ReadCrossLinkProperty((string p) => new ObjectPath(ObjectType.Table, p));
						return true;
					}
					break;
				case 10:
					if (propertyName == "baseColumn")
					{
						this.body.BaseColumnID.Path = reader.ReadCrossLinkProperty();
						return true;
					}
					break;
				case 11:
				{
					char c = propertyName[0];
					if (c != 'B')
					{
						if (c == 'a')
						{
							if (propertyName == "annotations")
							{
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
						}
					}
					else if (propertyName == "BaseTableID")
					{
						this.body.BaseTableID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
				case 12:
					if (propertyName == "BaseColumnID")
					{
						this.body.BaseColumnID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 13:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 's')
						{
							break;
						}
						if (!(propertyName == "summarization"))
						{
							break;
						}
					}
					else if (!(propertyName == "Summarization"))
					{
						break;
					}
					if (!CompatibilityRestrictions.SummarizationType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.Summarization = reader.ReadEnumProperty<SummarizationType>();
					return true;
				}
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003044 File Offset: 0x00001244
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.AlternateOf.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object AlternateOf is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.Summarization != SummarizationType.GroupBy)
			{
				if (!PropertyHelper.IsSummarizationTypeValueCompatible(this.body.Summarization, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Summarization is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["summarization", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertEnumToJsonValue<SummarizationType>(this.body.Summarization);
			}
			if (!options.IncludeTranslatablePropertiesOnly)
			{
				if (this.body.BaseColumnID.Object != null)
				{
					this.body.BaseColumnID.SerializeToJsonObject(true, "baseColumn", ObjectType.Table, result, 2, false);
				}
				if (this.body.BaseTableID.Object != null)
				{
					this.body.BaseTableID.SerializeToJsonObject(true, "baseTable", ObjectType.Table, result, 3, false);
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000031F4 File Offset: 0x000013F4
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (!(name == "summarization"))
			{
				if (name == "baseColumn")
				{
					this.body.BaseColumnID.Path = ObjectPath.Parse((JObject)jsonProp.Value);
					return true;
				}
				if (name == "baseTable")
				{
					this.body.BaseTableID.Path = new ObjectPath(ObjectType.Table, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
					return true;
				}
				if (!(name == "annotations"))
				{
					return false;
				}
				JsonPropertyHelper.ReadObjectCollection(this.Annotations, jsonProp.Value, options, mode, dbCompatibilityLevel);
				return true;
			}
			else
			{
				SummarizationType summarizationType = JsonPropertyHelper.ConvertJsonValueToEnum<SummarizationType>(jsonProp.Value);
				if (jsonProp.Value.Type != 10 && !PropertyHelper.IsSummarizationTypeValueCompatible(summarizationType, mode, dbCompatibilityLevel))
				{
					return false;
				}
				this.body.Summarization = summarizationType;
				return true;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000032D8 File Offset: 0x000014D8
		internal override string GetFormattedObjectPath()
		{
			if (this.BaseColumn != null && this.BaseColumn.Table != null)
			{
				return TomSR.ObjectPath_AlternateOf_2Args_Column(this.BaseColumn.Name, this.BaseColumn.Table.Name);
			}
			if (this.BaseTable != null)
			{
				return TomSR.ObjectPath_AlternateOf_1Arg_Table(this.BaseTable.Name);
			}
			return TomSR.ObjectPath_AlternateOf_0Args;
		}

		// Token: 0x040000C6 RID: 198
		internal AlternateOf.ObjectBody body;

		// Token: 0x040000C7 RID: 199
		private AlternateOfAnnotationCollection _Annotations;

		// Token: 0x02000220 RID: 544
		internal class ObjectBody : MetadataObjectBody<AlternateOf>
		{
			// Token: 0x06001E80 RID: 7808 RVA: 0x000CBF4A File Offset: 0x000CA14A
			public ObjectBody(AlternateOf owner)
				: base(owner)
			{
				this.ColumnID = new ParentLink<AlternateOf, Column>(owner, "Column");
				this.BaseColumnID = new CrossLink<AlternateOf, Column>(owner, "BaseColumn");
				this.BaseTableID = new CrossLink<AlternateOf, Table>(owner, "BaseTable");
			}

			// Token: 0x06001E81 RID: 7809 RVA: 0x000CBF88 File Offset: 0x000CA188
			internal bool IsEqualTo(AlternateOf.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Summarization, other.Summarization) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ColumnID.IsEqualTo(other.ColumnID, context)) && this.BaseColumnID.IsEqualTo(other.BaseColumnID, context) && this.BaseTableID.IsEqualTo(other.BaseTableID, context);
			}

			// Token: 0x06001E82 RID: 7810 RVA: 0x000CC000 File Offset: 0x000CA200
			internal void CopyFromImpl(AlternateOf.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.Summarization = other.Summarization;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ColumnID.CopyFrom(other.ColumnID, context);
				}
				this.BaseColumnID.CopyFrom(other.BaseColumnID, context);
				this.BaseTableID.CopyFrom(other.BaseTableID, context);
			}

			// Token: 0x06001E83 RID: 7811 RVA: 0x000CC078 File Offset: 0x000CA278
			internal void CopyFromImpl(AlternateOf.ObjectBody other)
			{
				this.Summarization = other.Summarization;
				this.ColumnID.CopyFrom(other.ColumnID, ObjectChangeTracker.BodyCloneContext);
				this.BaseColumnID.CopyFrom(other.BaseColumnID, ObjectChangeTracker.BodyCloneContext);
				this.BaseTableID.CopyFrom(other.BaseTableID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001E84 RID: 7812 RVA: 0x000CC0D3 File Offset: 0x000CA2D3
			public override void CopyFrom(MetadataObjectBody<AlternateOf> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((AlternateOf.ObjectBody)other, context);
			}

			// Token: 0x06001E85 RID: 7813 RVA: 0x000CC0EC File Offset: 0x000CA2EC
			internal bool IsEqualTo(AlternateOf.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Summarization, other.Summarization) && this.ColumnID.IsEqualTo(other.ColumnID) && this.BaseColumnID.IsEqualTo(other.BaseColumnID) && this.BaseTableID.IsEqualTo(other.BaseTableID);
			}

			// Token: 0x06001E86 RID: 7814 RVA: 0x000CC14E File Offset: 0x000CA34E
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((AlternateOf.ObjectBody)other);
			}

			// Token: 0x06001E87 RID: 7815 RVA: 0x000CC168 File Offset: 0x000CA368
			internal void CompareWith(AlternateOf.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Summarization, other.Summarization))
				{
					context.RegisterPropertyChange(base.Owner, "Summarization", typeof(SummarizationType), PropertyFlags.DdlAndUser, other.Summarization, this.Summarization);
				}
				this.ColumnID.CompareWith(other.ColumnID, "ColumnID", "Column", PropertyFlags.ReadOnly, context);
				this.BaseColumnID.CompareWith(other.BaseColumnID, "BaseColumnID", "BaseColumn", PropertyFlags.None, context);
				this.BaseTableID.CompareWith(other.BaseTableID, "BaseTableID", "BaseTable", PropertyFlags.None, context);
			}

			// Token: 0x06001E88 RID: 7816 RVA: 0x000CC211 File Offset: 0x000CA411
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((AlternateOf.ObjectBody)other, context);
			}

			// Token: 0x04000707 RID: 1799
			internal SummarizationType Summarization;

			// Token: 0x04000708 RID: 1800
			internal ParentLink<AlternateOf, Column> ColumnID;

			// Token: 0x04000709 RID: 1801
			internal CrossLink<AlternateOf, Column> BaseColumnID;

			// Token: 0x0400070A RID: 1802
			internal CrossLink<AlternateOf, Table> BaseTableID;
		}
	}
}
