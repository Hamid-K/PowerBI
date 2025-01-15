using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000A5 RID: 165
	public sealed class PerspectiveTable : NamedMetadataObject, ILinkedMetadataObject
	{
		// Token: 0x06000A08 RID: 2568 RVA: 0x000536C3 File Offset: 0x000518C3
		public PerspectiveTable()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000536D6 File Offset: 0x000518D6
		internal PerspectiveTable(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000536E8 File Offset: 0x000518E8
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new PerspectiveTable.ObjectBody(this);
			this._PerspectiveColumns = new PerspectiveColumnCollection(this, comparer);
			this._PerspectiveMeasures = new PerspectiveMeasureCollection(this, comparer);
			this._PerspectiveHierarchies = new PerspectiveHierarchyCollection(this, comparer);
			this._PerspectiveSets = new PerspectiveSetCollection(this, comparer);
			this._Annotations = new PerspectiveTableAnnotationCollection(this, comparer);
			this._ExtendedProperties = new PerspectiveTableExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0005374F File Offset: 0x0005194F
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.PerspectiveTable;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00053753 File Offset: 0x00051953
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00053765 File Offset: 0x00051965
		public override MetadataObject Parent
		{
			get
			{
				return this.body.PerspectiveID.Object;
			}
			internal set
			{
				if (this.body.PerspectiveID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<PerspectiveTable, Perspective>(this.body.PerspectiveID, (Perspective)value, null, null);
				}
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00053792 File Offset: 0x00051992
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.PerspectiveID.ObjectID;
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000537A4 File Offset: 0x000519A4
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.PerspectiveTable, null, "PerspectiveTable object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("includeAll", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("includeAll", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("columns", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "columns", MetadataPropertyNature.ChildCollection, ObjectType.PerspectiveColumn);
				}
				if (writer.ShouldIncludeProperty("hierarchies", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "hierarchies", MetadataPropertyNature.ChildCollection, ObjectType.PerspectiveHierarchy);
				}
				if (writer.ShouldIncludeProperty("measures", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "measures", MetadataPropertyNature.ChildCollection, ObjectType.PerspectiveMeasure);
				}
				if (CompatibilityRestrictions.PerspectiveSet.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("sets", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "sets", MetadataPropertyNature.ChildCollection, ObjectType.PerspectiveSet);
				}
				if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ExtendedProperty);
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00053940 File Offset: 0x00051B40
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x00053948 File Offset: 0x00051B48
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (PerspectiveTable.ObjectBody)value;
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00053956 File Offset: 0x00051B56
		internal override ITxObjectBody CreateBody()
		{
			return new PerspectiveTable.ObjectBody(this);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0005395E File Offset: 0x00051B5E
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new PerspectiveTable();
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00053965 File Offset: 0x00051B65
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Perspective)parent).PerspectiveTables;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00053974 File Offset: 0x00051B74
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Perspective perspective = MetadataObject.ResolveMetadataObjectParentById<PerspectiveTable, Perspective>(this.body.PerspectiveID, objectMap, throwIfCantResolve, null, null);
			this.body.TableID.ResolveById(objectMap, throwIfCantResolve);
			if (perspective != null)
			{
				perspective.PerspectiveTables.Add(this);
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000539B8 File Offset: 0x00051BB8
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.TableID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x000539D0 File Offset: 0x00051BD0
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.TableID.IsResolved && !this.body.TableID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Table"));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00053A24 File Offset: 0x00051C24
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			if (!this.body.TableID.TryResolveAfterCopy(copyContext) && this.body.TableID.Path != null && !this.body.TableID.Path.IsEmpty)
			{
				this.body._name = this.body.TableID.Path[this.body.TableID.Path.Count - 1].Value;
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00053AAC File Offset: 0x00051CAC
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.TableID.Validate(result, throwOnError);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00053AC0 File Offset: 0x00051CC0
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.TableID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00053ADC File Offset: 0x00051CDC
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._PerspectiveColumns;
			yield return this._PerspectiveMeasures;
			yield return this._PerspectiveHierarchies;
			yield return this._PerspectiveSets;
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x00053AEC File Offset: 0x00051CEC
		public PerspectiveColumnCollection PerspectiveColumns
		{
			get
			{
				return this._PerspectiveColumns;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00053AF4 File Offset: 0x00051CF4
		public PerspectiveMeasureCollection PerspectiveMeasures
		{
			get
			{
				return this._PerspectiveMeasures;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00053AFC File Offset: 0x00051CFC
		public PerspectiveHierarchyCollection PerspectiveHierarchies
		{
			get
			{
				return this._PerspectiveHierarchies;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00053B04 File Offset: 0x00051D04
		[CompatibilityRequirement(Pbi = "1400")]
		public PerspectiveSetCollection PerspectiveSets
		{
			get
			{
				return this._PerspectiveSets;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00053B0C File Offset: 0x00051D0C
		public PerspectiveTableAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00053B14 File Offset: 0x00051D14
		[CompatibilityRequirement("1400")]
		public PerspectiveTableExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00053B1C File Offset: 0x00051D1C
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x00053B3D File Offset: 0x00051D3D
		public override string Name
		{
			get
			{
				if (this.Table != null)
				{
					return this.Table.Name;
				}
				return this.body._name;
			}
			set
			{
				if (this.Table != null)
				{
					throw new InvalidOperationException(TomSR.Exception_NameCannotBeSetForReferencedObjects);
				}
				this.body._name = value;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00053B5E File Offset: 0x00051D5E
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x00053B6C File Offset: 0x00051D6C
		public bool IncludeAll
		{
			get
			{
				return this.body.IncludeAll;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IncludeAll, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IncludeAll", typeof(bool), this.body.IncludeAll, value);
					bool includeAll = this.body.IncludeAll;
					this.body.IncludeAll = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IncludeAll", typeof(bool), includeAll, value);
				}
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00053BF0 File Offset: 0x00051DF0
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x00053C00 File Offset: 0x00051E00
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

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00053C84 File Offset: 0x00051E84
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x00053C98 File Offset: 0x00051E98
		public Perspective Perspective
		{
			get
			{
				return this.body.PerspectiveID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.PerspectiveID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Perspective", typeof(Perspective), this.body.PerspectiveID.Object, value);
					Perspective @object = this.body.PerspectiveID.Object;
					this.body.PerspectiveID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Perspective", typeof(Perspective), @object, value);
				}
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00053D1C File Offset: 0x00051F1C
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x00053D2E File Offset: 0x00051F2E
		internal ObjectId _PerspectiveID
		{
			get
			{
				return this.body.PerspectiveID.ObjectID;
			}
			set
			{
				this.body.PerspectiveID.ObjectID = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00053D41 File Offset: 0x00051F41
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x00053D54 File Offset: 0x00051F54
		public Table Table
		{
			get
			{
				return this.body.TableID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TableID.Object, value))
				{
					if (this.body.TableID.Object != null)
					{
						throw new InvalidOperationException(TomSR.Exception_CantChangeImmutableProperty("Table", "PerspectiveTable"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Table", typeof(Table), this.body.TableID.Object, value);
					Table @object = this.body.TableID.Object;
					this.body.TableID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Table", typeof(Table), @object, value);
				}
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00053E02 File Offset: 0x00052002
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x00053E14 File Offset: 0x00052014
		internal ObjectId _TableID
		{
			get
			{
				return this.body.TableID.ObjectID;
			}
			set
			{
				this.body.TableID.ObjectID = value;
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00053E28 File Offset: 0x00052028
		internal void CopyFrom(PerspectiveTable other, CopyContext context)
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
				this.PerspectiveColumns.CopyFrom(other.PerspectiveColumns, context);
				this.PerspectiveMeasures.CopyFrom(other.PerspectiveMeasures, context);
				this.PerspectiveHierarchies.CopyFrom(other.PerspectiveHierarchies, context);
				this.PerspectiveSets.CopyFrom(other.PerspectiveSets, context);
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00053F32 File Offset: 0x00052132
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((PerspectiveTable)other, context);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00053F41 File Offset: 0x00052141
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(PerspectiveTable other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00053F5D File Offset: 0x0005215D
		public void CopyTo(PerspectiveTable other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00053F79 File Offset: 0x00052179
		public PerspectiveTable Clone()
		{
			return base.CloneInternal<PerspectiveTable>();
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00053F84 File Offset: 0x00052184
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.PerspectiveID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "PerspectiveID", this.body.PerspectiveID.Object);
			}
			this.body.TableID.Validate(null, true);
			if (this.body.TableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "TableID", this.body.TableID.Object);
			}
			if (this.body.IncludeAll)
			{
				writer.WriteProperty<bool>(options, "IncludeAll", this.body.IncludeAll);
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00054030 File Offset: 0x00052230
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("PerspectiveID", out objectId))
			{
				this.body.PerspectiveID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("TableID", out objectId2))
			{
				this.body.TableID.ObjectID = objectId2;
			}
			bool flag;
			if (reader.TryReadProperty<bool>("IncludeAll", out flag))
			{
				this.body.IncludeAll = flag;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000540BC File Offset: 0x000522BC
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.PerspectiveID.Object != null && writer.ShouldIncludeProperty("PerspectiveID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("PerspectiveID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.PerspectiveID.Object);
			}
			this.body.TableID.Validate(null, true);
			if (this.body.TableID.Object != null && writer.ShouldIncludeProperty("TableID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("TableID", MetadataPropertyNature.CrossLinkProperty, this.body.TableID.Object);
			}
			if (this.body.IncludeAll && writer.ShouldIncludeProperty("IncludeAll", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IncludeAll", MetadataPropertyNature.RegularProperty, this.body.IncludeAll);
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00054198 File Offset: 0x00052398
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty, this.Name);
			}
			if (this.body.IncludeAll && writer.ShouldIncludeProperty("includeAll", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("includeAll", MetadataPropertyNature.RegularProperty, this.body.IncludeAll);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.PerspectiveColumns.Count > 0 && writer.ShouldIncludeProperty("columns", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "columns", MetadataPropertyNature.ChildCollection, this.PerspectiveColumns);
			}
			if (this.PerspectiveHierarchies.Count > 0 && writer.ShouldIncludeProperty("hierarchies", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "hierarchies", MetadataPropertyNature.ChildCollection, this.PerspectiveHierarchies);
			}
			if (this.PerspectiveMeasures.Count > 0 && writer.ShouldIncludeProperty("measures", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "measures", MetadataPropertyNature.ChildCollection, this.PerspectiveMeasures);
			}
			if (this.PerspectiveSets.Count > 0)
			{
				if (!CompatibilityRestrictions.PerspectiveSet.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child PerspectiveSet is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("sets", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "sets", MetadataPropertyNature.ChildCollection, this.PerspectiveSets);
				}
			}
			if (this.ExtendedProperties.Count > 0)
			{
				if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, this.ExtendedProperties);
				}
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000543F4 File Offset: 0x000525F4
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
				case 4:
				{
					char c = propertyName[0];
					if (c != 'n')
					{
						if (c == 's')
						{
							if (propertyName == "sets")
							{
								if (!CompatibilityRestrictions.PerspectiveSet.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
								{
									classification = UnexpectedPropertyClassification.IncompatibleProperty;
									return false;
								}
								using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
								{
									foreach (PerspectiveSet perspectiveSet in reader.ReadChildCollectionProperty<PerspectiveSet>(context))
									{
										try
										{
											this.PerspectiveSets.Add(perspectiveSet);
										}
										catch (Exception ex)
										{
											throw reader.CreateInvalidChildException(context, perspectiveSet, TomSR.Exception_FailedAddDeserializedNamedObject("PerspectiveSet", (perspectiveSet != null) ? perspectiveSet.Name : null, ex.Message), ex);
										}
									}
								}
								return true;
							}
						}
					}
					else if (propertyName == "name")
					{
						this.Name = reader.ReadStringProperty();
						return true;
					}
					break;
				}
				case 7:
				{
					char c = propertyName[0];
					if (c != 'T')
					{
						if (c == 'c')
						{
							if (propertyName == "columns")
							{
								using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
								{
									foreach (PerspectiveColumn perspectiveColumn in reader.ReadChildCollectionProperty<PerspectiveColumn>(context))
									{
										try
										{
											this.PerspectiveColumns.Add(perspectiveColumn);
										}
										catch (Exception ex2)
										{
											throw reader.CreateInvalidChildException(context, perspectiveColumn, TomSR.Exception_FailedAddDeserializedNamedObject("PerspectiveColumn", (perspectiveColumn != null) ? perspectiveColumn.Name : null, ex2.Message), ex2);
										}
									}
								}
								return true;
							}
						}
					}
					else if (propertyName == "TableID")
					{
						this.body.TableID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
				case 8:
					if (propertyName == "measures")
					{
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (PerspectiveMeasure perspectiveMeasure in reader.ReadChildCollectionProperty<PerspectiveMeasure>(context))
							{
								try
								{
									this.PerspectiveMeasures.Add(perspectiveMeasure);
								}
								catch (Exception ex3)
								{
									throw reader.CreateInvalidChildException(context, perspectiveMeasure, TomSR.Exception_FailedAddDeserializedNamedObject("PerspectiveMeasure", (perspectiveMeasure != null) ? perspectiveMeasure.Name : null, ex3.Message), ex3);
								}
							}
						}
						return true;
					}
					break;
				case 10:
				{
					char c = propertyName[0];
					if (c != 'I')
					{
						if (c != 'i')
						{
							break;
						}
						if (!(propertyName == "includeAll"))
						{
							break;
						}
					}
					else if (!(propertyName == "IncludeAll"))
					{
						break;
					}
					this.body.IncludeAll = reader.ReadBooleanProperty();
					return true;
				}
				case 11:
				{
					char c = propertyName[0];
					if (c != 'a')
					{
						if (c == 'h')
						{
							if (propertyName == "hierarchies")
							{
								using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
								{
									foreach (PerspectiveHierarchy perspectiveHierarchy in reader.ReadChildCollectionProperty<PerspectiveHierarchy>(context))
									{
										try
										{
											this.PerspectiveHierarchies.Add(perspectiveHierarchy);
										}
										catch (Exception ex4)
										{
											throw reader.CreateInvalidChildException(context, perspectiveHierarchy, TomSR.Exception_FailedAddDeserializedNamedObject("PerspectiveHierarchy", (perspectiveHierarchy != null) ? perspectiveHierarchy.Name : null, ex4.Message), ex4);
										}
									}
								}
								return true;
							}
						}
					}
					else if (propertyName == "annotations")
					{
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (Annotation annotation in reader.ReadChildCollectionProperty<Annotation>(context))
							{
								try
								{
									this.Annotations.Add(annotation);
								}
								catch (Exception ex5)
								{
									throw reader.CreateInvalidChildException(context, annotation, TomSR.Exception_FailedAddDeserializedNamedObject("Annotation", (annotation != null) ? annotation.Name : null, ex5.Message), ex5);
								}
							}
						}
						return true;
					}
					break;
				}
				case 12:
				{
					char c = propertyName[0];
					if (c != 'M')
					{
						if (c != 'm')
						{
							break;
						}
						if (!(propertyName == "modifiedTime"))
						{
							break;
						}
					}
					else if (!(propertyName == "ModifiedTime"))
					{
						break;
					}
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
				}
				case 13:
					if (propertyName == "PerspectiveID")
					{
						this.body.PerspectiveID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 18:
					if (propertyName == "extendedProperties")
					{
						if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (ExtendedProperty extendedProperty in reader.ReadChildCollectionProperty<ExtendedProperty>(context))
							{
								try
								{
									this.ExtendedProperties.Add(extendedProperty);
								}
								catch (Exception ex6)
								{
									throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex6.Message), ex6);
								}
							}
						}
						return true;
					}
					break;
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00054AA4 File Offset: 0x00052CA4
		private protected override void OnDeserializeEnd(SerializationActivityContext context)
		{
			base.OnDeserializeEnd(context);
			if (context.SerializationMode != MetadataSerializationMode.Xmla)
			{
				context.RegistrerObjectForMasterReferenceCrossLinkReconstruction(this);
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00054AC0 File Offset: 0x00052CC0
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				result["name", TomPropCategory.Name, 0, true] = JsonPropertyHelper.ConvertStringToJsonValue(this.Name, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IncludeAll)
			{
				result["includeAll", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IncludeAll);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 5, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<PerspectiveColumn> enumerable;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<PerspectiveColumn> perspectiveColumns = this.PerspectiveColumns;
						enumerable = perspectiveColumns;
					}
					else
					{
						enumerable = this.PerspectiveColumns.Where((PerspectiveColumn o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<PerspectiveColumn> enumerable2 = enumerable;
					if (enumerable2.Any<PerspectiveColumn>())
					{
						object[] array = enumerable2.Select((PerspectiveColumn obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array2 = array;
						result["columns", TomPropCategory.ChildCollection, 30, false] = array2;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<PerspectiveMeasure> enumerable3;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<PerspectiveMeasure> perspectiveMeasures = this.PerspectiveMeasures;
						enumerable3 = perspectiveMeasures;
					}
					else
					{
						enumerable3 = this.PerspectiveMeasures.Where((PerspectiveMeasure o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<PerspectiveMeasure> enumerable4 = enumerable3;
					if (enumerable4.Any<PerspectiveMeasure>())
					{
						object[] array = enumerable4.Select((PerspectiveMeasure obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array3 = array;
						result["measures", TomPropCategory.ChildCollection, 32, false] = array3;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<PerspectiveHierarchy> enumerable5;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<PerspectiveHierarchy> perspectiveHierarchies = this.PerspectiveHierarchies;
						enumerable5 = perspectiveHierarchies;
					}
					else
					{
						enumerable5 = this.PerspectiveHierarchies.Where((PerspectiveHierarchy o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<PerspectiveHierarchy> enumerable6 = enumerable5;
					if (enumerable6.Any<PerspectiveHierarchy>())
					{
						object[] array = enumerable6.Select((PerspectiveHierarchy obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array4 = array;
						result["hierarchies", TomPropCategory.ChildCollection, 31, false] = array4;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<PerspectiveSet> enumerable7;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<PerspectiveSet> perspectiveSets = this.PerspectiveSets;
						enumerable7 = perspectiveSets;
					}
					else
					{
						enumerable7 = this.PerspectiveSets.Where((PerspectiveSet o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<PerspectiveSet> enumerable8 = enumerable7;
					if (enumerable8.Any<PerspectiveSet>())
					{
						if (!CompatibilityRestrictions.PerspectiveSet.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child PerspectiveSet is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable8.Select((PerspectiveSet obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array5 = array;
						result["sets", TomPropCategory.ChildCollection, 38, false] = array5;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ExtendedProperty> enumerable9;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ExtendedProperty> extendedProperties = this.ExtendedProperties;
						enumerable9 = extendedProperties;
					}
					else
					{
						enumerable9 = this.ExtendedProperties.Where((ExtendedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ExtendedProperty> enumerable10 = enumerable9;
					if (enumerable10.Any<ExtendedProperty>())
					{
						if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable10.Select((ExtendedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array6 = array;
						result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array6;
					}
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array7 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array7;
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00054F5C File Offset: 0x0005315C
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				int length = name.Length;
				switch (length)
				{
				case 4:
				{
					char c = name[0];
					if (c != 'n')
					{
						if (c == 's')
						{
							if (name == "sets")
							{
								if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.PerspectiveSet.IsCompatible(mode, dbCompatibilityLevel))
								{
									return false;
								}
								JsonPropertyHelper.ReadObjectCollection(this.PerspectiveSets, jsonProp.Value, options, mode, dbCompatibilityLevel);
								return true;
							}
						}
					}
					else if (name == "name")
					{
						this.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				case 5:
				case 6:
				case 9:
					break;
				case 7:
					if (name == "columns")
					{
						JsonPropertyHelper.ReadObjectCollection(this.PerspectiveColumns, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 8:
					if (name == "measures")
					{
						JsonPropertyHelper.ReadObjectCollection(this.PerspectiveMeasures, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 10:
					if (name == "includeAll")
					{
						this.body.IncludeAll = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				case 11:
				{
					char c = name[0];
					if (c != 'a')
					{
						if (c == 'h')
						{
							if (name == "hierarchies")
							{
								JsonPropertyHelper.ReadObjectCollection(this.PerspectiveHierarchies, jsonProp.Value, options, mode, dbCompatibilityLevel);
								return true;
							}
						}
					}
					else if (name == "annotations")
					{
						JsonPropertyHelper.ReadObjectCollection(this.Annotations, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				}
				case 12:
					if (name == "modifiedTime")
					{
						this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
						return true;
					}
					break;
				default:
					if (length == 18)
					{
						if (name == "extendedProperties")
						{
							if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							JsonPropertyHelper.ReadObjectCollection(this.ExtendedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
							return true;
						}
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000551B4 File Offset: 0x000533B4
		internal override bool BuildIndirectNameCrossLinkPathIfNeeded()
		{
			if (this.body.TableID.Object != null)
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.body._name))
			{
				return false;
			}
			if (this.body.TableID.Path == null || this.body.TableID.Path.IsEmpty)
			{
				this.body.TableID.Path = new ObjectPath(ObjectType.Table, this.body._name);
			}
			return true;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00055234 File Offset: 0x00053434
		void ILinkedMetadataObject.GetLinkedObjectTarget(out ObjectId objectId, out ObjectPath objectPath, out MetadataObject @object, out string property)
		{
			objectId = this.body.TableID.ObjectID;
			objectPath = this.body.TableID.Path;
			@object = this.body.TableID.Object;
			property = null;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00055274 File Offset: 0x00053474
		internal override string GetFormattedObjectPath()
		{
			if (this.Perspective != null && this.Table != null)
			{
				return TomSR.ObjectPath_PerspectiveTable_2Args(this.Table.Name, this.Perspective.Name);
			}
			if (this.Table != null)
			{
				return TomSR.ObjectPath_PerspectiveTable_1Args(this.Table.Name);
			}
			return TomSR.ObjectPath_PerspectiveTable_0Args;
		}

		// Token: 0x0400015C RID: 348
		internal PerspectiveTable.ObjectBody body;

		// Token: 0x0400015D RID: 349
		private PerspectiveColumnCollection _PerspectiveColumns;

		// Token: 0x0400015E RID: 350
		private PerspectiveMeasureCollection _PerspectiveMeasures;

		// Token: 0x0400015F RID: 351
		private PerspectiveHierarchyCollection _PerspectiveHierarchies;

		// Token: 0x04000160 RID: 352
		private PerspectiveSetCollection _PerspectiveSets;

		// Token: 0x04000161 RID: 353
		private PerspectiveTableAnnotationCollection _Annotations;

		// Token: 0x04000162 RID: 354
		private PerspectiveTableExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x020002B7 RID: 695
		internal class ObjectBody : NamedMetadataObjectBody<PerspectiveTable>
		{
			// Token: 0x06002272 RID: 8818 RVA: 0x000DD7AF File Offset: 0x000DB9AF
			public ObjectBody(PerspectiveTable owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.PerspectiveID = new ParentLink<PerspectiveTable, Perspective>(owner, "Perspective");
				this.TableID = new CrossLink<PerspectiveTable, Table>(owner, "Table");
			}

			// Token: 0x06002273 RID: 8819 RVA: 0x000DD7E5 File Offset: 0x000DB9E5
			public override string GetObjectName()
			{
				if (this.TableID.Object == null)
				{
					return this._name;
				}
				return this.TableID.Object.Name;
			}

			// Token: 0x06002274 RID: 8820 RVA: 0x000DD80C File Offset: 0x000DBA0C
			internal bool IsEqualTo(PerspectiveTable.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.IncludeAll, other.IncludeAll) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.PerspectiveID.IsEqualTo(other.PerspectiveID, context)) && this.TableID.IsEqualTo(other.TableID, context);
			}

			// Token: 0x06002275 RID: 8821 RVA: 0x000DD898 File Offset: 0x000DBA98
			internal void CopyFromImpl(PerspectiveTable.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.IncludeAll = other.IncludeAll;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.PerspectiveID.CopyFrom(other.PerspectiveID, context);
				}
				this.TableID.CopyFrom(other.TableID, context);
				this._name = other._name;
			}

			// Token: 0x06002276 RID: 8822 RVA: 0x000DD928 File Offset: 0x000DBB28
			internal void CopyFromImpl(PerspectiveTable.ObjectBody other)
			{
				this.IncludeAll = other.IncludeAll;
				this.ModifiedTime = other.ModifiedTime;
				this.PerspectiveID.CopyFrom(other.PerspectiveID, ObjectChangeTracker.BodyCloneContext);
				this.TableID.CopyFrom(other.TableID, ObjectChangeTracker.BodyCloneContext);
				this._name = other._name;
			}

			// Token: 0x06002277 RID: 8823 RVA: 0x000DD985 File Offset: 0x000DBB85
			public override void CopyFrom(MetadataObjectBody<PerspectiveTable> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((PerspectiveTable.ObjectBody)other, context);
			}

			// Token: 0x06002278 RID: 8824 RVA: 0x000DD99C File Offset: 0x000DBB9C
			internal bool IsEqualTo(PerspectiveTable.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.IncludeAll, other.IncludeAll) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.PerspectiveID.IsEqualTo(other.PerspectiveID) && this.TableID.IsEqualTo(other.TableID);
			}

			// Token: 0x06002279 RID: 8825 RVA: 0x000DD9FE File Offset: 0x000DBBFE
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((PerspectiveTable.ObjectBody)other);
			}

			// Token: 0x0600227A RID: 8826 RVA: 0x000DDA18 File Offset: 0x000DBC18
			internal void CompareWith(PerspectiveTable.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.IncludeAll, other.IncludeAll))
				{
					context.RegisterPropertyChange(base.Owner, "IncludeAll", typeof(bool), PropertyFlags.DdlAndUser, other.IncludeAll, this.IncludeAll);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.PerspectiveID.CompareWith(other.PerspectiveID, "PerspectiveID", "Perspective", PropertyFlags.ReadOnly, context);
				this.TableID.CompareWith(other.TableID, "TableID", "Table", PropertyFlags.None, context);
			}

			// Token: 0x0600227B RID: 8827 RVA: 0x000DDAE9 File Offset: 0x000DBCE9
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((PerspectiveTable.ObjectBody)other, context);
			}

			// Token: 0x040009D3 RID: 2515
			internal bool IncludeAll;

			// Token: 0x040009D4 RID: 2516
			internal DateTime ModifiedTime;

			// Token: 0x040009D5 RID: 2517
			internal ParentLink<PerspectiveTable, Perspective> PerspectiveID;

			// Token: 0x040009D6 RID: 2518
			internal CrossLink<PerspectiveTable, Table> TableID;

			// Token: 0x040009D7 RID: 2519
			internal string _name;
		}
	}
}
