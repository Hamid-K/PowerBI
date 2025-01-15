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
	// Token: 0x02000075 RID: 117
	public sealed class Measure : NamedMetadataObject, IMetadataObjectWithLineage
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x00030B5C File Offset: 0x0002ED5C
		public Measure()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00030B6F File Offset: 0x0002ED6F
		internal Measure(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00030B80 File Offset: 0x0002ED80
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new Measure.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this.body.DataType = DataType.Unknown;
			this.body.Expression = string.Empty;
			this.body.FormatString = string.Empty;
			this.body.State = ObjectState.Ready;
			this.body.ErrorMessage = string.Empty;
			this.body.DisplayFolder = string.Empty;
			this.body.DataCategory = string.Empty;
			this.body.LineageTag = string.Empty;
			this.body.SourceLineageTag = string.Empty;
			this._Annotations = new MeasureAnnotationCollection(this, comparer);
			this._ExtendedProperties = new MeasureExtendedPropertyCollection(this, comparer);
			this._ChangedProperties = new MeasureChangedPropertyCollection(this);
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00030C68 File Offset: 0x0002EE68
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Measure;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00030C6B File Offset: 0x0002EE6B
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x00030C7D File Offset: 0x0002EE7D
		public override MetadataObject Parent
		{
			get
			{
				return this.body.TableID.Object;
			}
			internal set
			{
				if (this.body.TableID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<Measure, Table>(this.body.TableID, (Table)value, null, null);
				}
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00030CAA File Offset: 0x0002EEAA
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.TableID.ObjectID;
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00030CBC File Offset: 0x0002EEBC
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Measure, null, "Measure object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("dataType", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<DataType>("dataType", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, null);
				}
				if (writer.ShouldIncludeProperty("formatString", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("formatString", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("isHidden", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("isHidden", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, PropertyHelper.GetObjectStateCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("isSimpleMeasure", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("isSimpleMeasure", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, typeof(string));
				}
				if (writer.ShouldIncludeProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (CompatibilityRestrictions.Measure_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("lineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("lineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (CompatibilityRestrictions.Measure_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (CompatibilityRestrictions.Measure_DataCategory.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("dataCategory", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("dataCategory", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("kpi", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteSingleChild(context, "kpi", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Translatable, ObjectType.KPI);
				}
				if (CompatibilityRestrictions.Measure_DetailRowsDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("detailRowsDefinition", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "detailRowsDefinition", MetadataPropertyNature.ChildProperty, ObjectType.DetailRowsDefinition);
				}
				if (CompatibilityRestrictions.Measure_FormatStringDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("formatStringDefinition", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "formatStringDefinition", MetadataPropertyNature.ChildProperty, ObjectType.FormatStringDefinition);
				}
				if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ExtendedProperty);
				}
				if (CompatibilityRestrictions.ChangedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("changedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "changedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ChangedProperty);
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000310C8 File Offset: 0x0002F2C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.body.DataCategory))
			{
				int num = CompatibilityRestrictions.Measure_DataCategory[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DataCategory");
					requiredLevel = num;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				int num2 = CompatibilityRestrictions.Measure_LineageTag[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num2, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "LineageTag");
					requiredLevel = num2;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				int num3 = CompatibilityRestrictions.Measure_SourceLineageTag[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num3, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceLineageTag");
					requiredLevel = num3;
					int num4 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x000311C3 File Offset: 0x0002F3C3
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x000311CB File Offset: 0x0002F3CB
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Measure.ObjectBody)value;
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000311D9 File Offset: 0x0002F3D9
		internal override ITxObjectBody CreateBody()
		{
			return new Measure.ObjectBody(this);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000311E1 File Offset: 0x0002F3E1
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new Measure();
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000311E8 File Offset: 0x0002F3E8
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Table)parent).Measures;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000311F8 File Offset: 0x0002F3F8
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Table table = MetadataObject.ResolveMetadataObjectParentById<Measure, Table>(this.body.TableID, objectMap, throwIfCantResolve, null, null);
			this.body.KPIID.ResolveById(objectMap, throwIfCantResolve);
			this.body.DetailRowsDefinitionID.ResolveById(objectMap, throwIfCantResolve);
			this.body.FormatStringDefinitionID.ResolveById(objectMap, throwIfCantResolve);
			if (table != null)
			{
				table.Measures.Add(this);
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00031262 File Offset: 0x0002F462
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00031264 File Offset: 0x0002F464
		internal override IEnumerable<MetadataObject> GetDirectChildren(bool isLogicalStructure)
		{
			if (this.body.KPIID.Object != null)
			{
				yield return this.body.KPIID.Object;
			}
			if (this.body.DetailRowsDefinitionID.Object != null)
			{
				yield return this.body.DetailRowsDefinitionID.Object;
			}
			if (this.body.FormatStringDefinitionID.Object != null)
			{
				yield return this.body.FormatStringDefinitionID.Object;
			}
			yield break;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00031274 File Offset: 0x0002F474
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield return this._ChangedProperties;
			yield break;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00031284 File Offset: 0x0002F484
		private protected override void SetDirectChildImpl(MetadataObject child)
		{
			ObjectType objectType = child.ObjectType;
			if (objectType == ObjectType.KPI)
			{
				ObjectChangeTracker.RegisterPropertyChanging(this, "KPI", typeof(KPI), this.body.KPIID.Object, child);
				KPI @object = this.body.KPIID.Object;
				this.body.KPIID.Object = (KPI)child;
				ObjectChangeTracker.RegisterPropertyChanged(this, "KPI", typeof(KPI), @object, child);
				return;
			}
			if (objectType == ObjectType.DetailRowsDefinition)
			{
				base.ValidateCompatibilityRequirement(child, "DetailRowsDefinition", CompatibilityRestrictions.Measure_DetailRowsDefinition);
				ObjectChangeTracker.RegisterPropertyChanging(this, "DetailRowsDefinition", typeof(DetailRowsDefinition), this.body.DetailRowsDefinitionID.Object, child);
				DetailRowsDefinition object2 = this.body.DetailRowsDefinitionID.Object;
				this.body.DetailRowsDefinitionID.Object = (DetailRowsDefinition)child;
				ObjectChangeTracker.RegisterPropertyChanged(this, "DetailRowsDefinition", typeof(DetailRowsDefinition), object2, child);
				return;
			}
			if (objectType != ObjectType.FormatStringDefinition)
			{
				base.SetDirectChildImpl(child);
				return;
			}
			base.ValidateCompatibilityRequirement(child, "FormatStringDefinition", CompatibilityRestrictions.Measure_FormatStringDefinition);
			ObjectChangeTracker.RegisterPropertyChanging(this, "FormatStringDefinition", typeof(FormatStringDefinition), this.body.FormatStringDefinitionID.Object, child);
			FormatStringDefinition object3 = this.body.FormatStringDefinitionID.Object;
			this.body.FormatStringDefinitionID.Object = (FormatStringDefinition)child;
			ObjectChangeTracker.RegisterPropertyChanged(this, "FormatStringDefinition", typeof(FormatStringDefinition), object3, child);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0003140C File Offset: 0x0002F60C
		private protected override void RemoveDirectChildImpl(MetadataObject child)
		{
			ObjectType objectType = child.ObjectType;
			if (objectType != ObjectType.KPI)
			{
				if (objectType != ObjectType.DetailRowsDefinition)
				{
					if (objectType != ObjectType.FormatStringDefinition)
					{
						base.RemoveDirectChildImpl(child);
					}
					else if (this.body.FormatStringDefinitionID.ObjectID == child.Id)
					{
						ObjectChangeTracker.RegisterPropertyChanging(this, "FormatStringDefinition", typeof(FormatStringDefinition), this.body.FormatStringDefinitionID.Object, null);
						base.ResetCompatibilityRequirement();
						FormatStringDefinition @object = this.body.FormatStringDefinitionID.Object;
						this.body.FormatStringDefinitionID.Object = null;
						ObjectChangeTracker.RegisterPropertyChanged(this, "FormatStringDefinition", typeof(FormatStringDefinition), @object, null);
						return;
					}
				}
				else if (this.body.DetailRowsDefinitionID.ObjectID == child.Id)
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DetailRowsDefinition", typeof(DetailRowsDefinition), this.body.DetailRowsDefinitionID.Object, null);
					base.ResetCompatibilityRequirement();
					DetailRowsDefinition object2 = this.body.DetailRowsDefinitionID.Object;
					this.body.DetailRowsDefinitionID.Object = null;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DetailRowsDefinition", typeof(DetailRowsDefinition), object2, null);
					return;
				}
			}
			else if (this.body.KPIID.ObjectID == child.Id)
			{
				ObjectChangeTracker.RegisterPropertyChanging(this, "KPI", typeof(KPI), this.body.KPIID.Object, null);
				KPI object3 = this.body.KPIID.Object;
				this.body.KPIID.Object = null;
				ObjectChangeTracker.RegisterPropertyChanged(this, "KPI", typeof(KPI), object3, null);
				return;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x000315CA File Offset: 0x0002F7CA
		public MeasureAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x000315D2 File Offset: 0x0002F7D2
		[CompatibilityRequirement("1400")]
		public MeasureExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x000315DA File Offset: 0x0002F7DA
		[CompatibilityRequirement("1567")]
		public MeasureChangedPropertyCollection ChangedProperties
		{
			get
			{
				return this._ChangedProperties;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x000315E2 File Offset: 0x0002F7E2
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x000315F0 File Offset: 0x0002F7F0
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Measure, out text))
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

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00031672 File Offset: 0x0002F872
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x00031680 File Offset: 0x0002F880
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

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x000316F0 File Offset: 0x0002F8F0
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x00031700 File Offset: 0x0002F900
		public DataType DataType
		{
			get
			{
				return this.body.DataType;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DataType, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DataType", typeof(DataType), this.body.DataType, value);
					DataType dataType = this.body.DataType;
					this.body.DataType = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DataType", typeof(DataType), dataType, value);
				}
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00031784 File Offset: 0x0002F984
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x00031794 File Offset: 0x0002F994
		public string Expression
		{
			get
			{
				return this.body.Expression;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Expression, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Expression", typeof(string), this.body.Expression, value);
					string expression = this.body.Expression;
					this.body.Expression = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Expression", typeof(string), expression, value);
				}
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00031804 File Offset: 0x0002FA04
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x00031814 File Offset: 0x0002FA14
		public string FormatString
		{
			get
			{
				return this.body.FormatString;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.FormatString, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "FormatString", typeof(string), this.body.FormatString, value);
					string formatString = this.body.FormatString;
					this.body.FormatString = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "FormatString", typeof(string), formatString, value);
				}
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00031884 File Offset: 0x0002FA84
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x00031894 File Offset: 0x0002FA94
		public bool IsHidden
		{
			get
			{
				return this.body.IsHidden;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsHidden, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsHidden", typeof(bool), this.body.IsHidden, value);
					bool isHidden = this.body.IsHidden;
					this.body.IsHidden = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsHidden", typeof(bool), isHidden, value);
				}
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00031918 File Offset: 0x0002FB18
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x00031928 File Offset: 0x0002FB28
		public ObjectState State
		{
			get
			{
				return this.body.State;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.State, value))
				{
					CompatibilityRestrictionSet objectStateCompatibilityRestrictions = PropertyHelper.GetObjectStateCompatibilityRestrictions(value);
					CompatibilityRestrictionSet objectStateCompatibilityRestrictions2 = PropertyHelper.GetObjectStateCompatibilityRestrictions(this.body.State);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = objectStateCompatibilityRestrictions.Compare(objectStateCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != ObjectState.Ready))
					{
						array = base.ValidateCompatibilityRequirement(objectStateCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "State", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "State", typeof(ObjectState), this.body.State, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						break;
					}
					ObjectState state = this.body.State;
					this.body.State = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "State", typeof(ObjectState), state, value);
				}
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00031A4A File Offset: 0x0002FC4A
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x00031A58 File Offset: 0x0002FC58
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

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00031ADC File Offset: 0x0002FCDC
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x00031AEC File Offset: 0x0002FCEC
		public DateTime StructureModifiedTime
		{
			get
			{
				return this.body.StructureModifiedTime;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.StructureModifiedTime, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "StructureModifiedTime", typeof(DateTime), this.body.StructureModifiedTime, value);
					DateTime structureModifiedTime = this.body.StructureModifiedTime;
					this.body.StructureModifiedTime = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "StructureModifiedTime", typeof(DateTime), structureModifiedTime, value);
				}
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00031B70 File Offset: 0x0002FD70
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x00031B80 File Offset: 0x0002FD80
		public bool IsSimpleMeasure
		{
			get
			{
				return this.body.IsSimpleMeasure;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsSimpleMeasure, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsSimpleMeasure", typeof(bool), this.body.IsSimpleMeasure, value);
					bool isSimpleMeasure = this.body.IsSimpleMeasure;
					this.body.IsSimpleMeasure = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsSimpleMeasure", typeof(bool), isSimpleMeasure, value);
				}
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00031C04 File Offset: 0x0002FE04
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x00031C14 File Offset: 0x0002FE14
		public string ErrorMessage
		{
			get
			{
				return this.body.ErrorMessage;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ErrorMessage, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ErrorMessage", typeof(string), this.body.ErrorMessage, value);
					string errorMessage = this.body.ErrorMessage;
					this.body.ErrorMessage = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ErrorMessage", typeof(string), errorMessage, value);
				}
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00031C84 File Offset: 0x0002FE84
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x00031C94 File Offset: 0x0002FE94
		public string DisplayFolder
		{
			get
			{
				return this.body.DisplayFolder;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DisplayFolder, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DisplayFolder", typeof(string), this.body.DisplayFolder, value);
					string displayFolder = this.body.DisplayFolder;
					this.body.DisplayFolder = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DisplayFolder", typeof(string), displayFolder, value);
				}
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00031D04 File Offset: 0x0002FF04
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00031D14 File Offset: 0x0002FF14
		[CompatibilityRequirement("1455")]
		public string DataCategory
		{
			get
			{
				return this.body.DataCategory;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DataCategory, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Measure_DataCategory, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DataCategory"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DataCategory", typeof(string), this.body.DataCategory, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Measure_DataCategory, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string dataCategory = this.body.DataCategory;
					this.body.DataCategory = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DataCategory", typeof(string), dataCategory, value);
				}
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00031DC9 File Offset: 0x0002FFC9
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00031DD8 File Offset: 0x0002FFD8
		[CompatibilityRequirement("1540")]
		public string LineageTag
		{
			get
			{
				return this.body.LineageTag;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.LineageTag, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Measure_LineageTag, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "LineageTag"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "LineageTag", typeof(string), this.body.LineageTag, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Measure_LineageTag, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string lineageTag = this.body.LineageTag;
					this.body.LineageTag = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "LineageTag", typeof(string), lineageTag, value);
				}
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00031E8D File Offset: 0x0003008D
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x00031E9C File Offset: 0x0003009C
		[CompatibilityRequirement("1550")]
		public string SourceLineageTag
		{
			get
			{
				return this.body.SourceLineageTag;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SourceLineageTag, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Measure_SourceLineageTag, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceLineageTag"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "SourceLineageTag", typeof(string), this.body.SourceLineageTag, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Measure_SourceLineageTag, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string sourceLineageTag = this.body.SourceLineageTag;
					this.body.SourceLineageTag = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SourceLineageTag", typeof(string), sourceLineageTag, value);
				}
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00031F51 File Offset: 0x00030151
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00031F64 File Offset: 0x00030164
		public Table Table
		{
			get
			{
				return this.body.TableID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TableID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Table", typeof(Table), this.body.TableID.Object, value);
					Table @object = this.body.TableID.Object;
					this.body.TableID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Table", typeof(Table), @object, value);
				}
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00031FE8 File Offset: 0x000301E8
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x00031FFA File Offset: 0x000301FA
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

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0003200D File Offset: 0x0003020D
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x00032020 File Offset: 0x00030220
		public KPI KPI
		{
			get
			{
				return this.body.KPIID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.KPIID.Object, value))
				{
					if (value != null)
					{
						base.ValidateCompatibilityRequirement(value, "KPI", null);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "KPI", typeof(KPI), this.body.KPIID.Object, value);
					KPI @object = this.body.KPIID.Object;
					this.body.KPIID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "KPI", typeof(KPI), @object, value);
				}
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x000320B5 File Offset: 0x000302B5
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x000320C7 File Offset: 0x000302C7
		internal ObjectId _KPIID
		{
			get
			{
				return this.body.KPIID.ObjectID;
			}
			set
			{
				this.body.KPIID.ObjectID = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x000320DA File Offset: 0x000302DA
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x000320EC File Offset: 0x000302EC
		[CompatibilityRequirement("1400")]
		public DetailRowsDefinition DetailRowsDefinition
		{
			get
			{
				return this.body.DetailRowsDefinitionID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DetailRowsDefinitionID.Object, value))
				{
					if (value != null)
					{
						base.ValidateCompatibilityRequirement(value, "DetailRowsDefinition", CompatibilityRestrictions.Measure_DetailRowsDefinition);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DetailRowsDefinition", typeof(DetailRowsDefinition), this.body.DetailRowsDefinitionID.Object, value);
					DetailRowsDefinition @object = this.body.DetailRowsDefinitionID.Object;
					this.body.DetailRowsDefinitionID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DetailRowsDefinition", typeof(DetailRowsDefinition), @object, value);
				}
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00032185 File Offset: 0x00030385
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x00032197 File Offset: 0x00030397
		internal ObjectId _DetailRowsDefinitionID
		{
			get
			{
				return this.body.DetailRowsDefinitionID.ObjectID;
			}
			set
			{
				this.body.DetailRowsDefinitionID.ObjectID = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x000321AA File Offset: 0x000303AA
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x000321BC File Offset: 0x000303BC
		[CompatibilityRequirement("1601")]
		public FormatStringDefinition FormatStringDefinition
		{
			get
			{
				return this.body.FormatStringDefinitionID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.FormatStringDefinitionID.Object, value))
				{
					if (value != null)
					{
						base.ValidateCompatibilityRequirement(value, "FormatStringDefinition", CompatibilityRestrictions.Measure_FormatStringDefinition);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "FormatStringDefinition", typeof(FormatStringDefinition), this.body.FormatStringDefinitionID.Object, value);
					FormatStringDefinition @object = this.body.FormatStringDefinitionID.Object;
					this.body.FormatStringDefinitionID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "FormatStringDefinition", typeof(FormatStringDefinition), @object, value);
				}
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00032255 File Offset: 0x00030455
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x00032267 File Offset: 0x00030467
		internal ObjectId _FormatStringDefinitionID
		{
			get
			{
				return this.body.FormatStringDefinitionID.ObjectID;
			}
			set
			{
				this.body.FormatStringDefinitionID.ObjectID = value;
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0003227C File Offset: 0x0003047C
		internal void CopyFrom(Measure other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions || !this.body.IsEqualTo(other.body, context))
			{
				ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				this.body.CopyFrom(other.body, context);
			}
			else if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				if (this.body.KPIID.Object != null && other.body.KPIID.Object != null)
				{
					this.body.KPIID.Object.CopyFrom(other.body.KPIID.Object, context);
				}
				if (this.body.DetailRowsDefinitionID.Object != null && other.body.DetailRowsDefinitionID.Object != null)
				{
					this.body.DetailRowsDefinitionID.Object.CopyFrom(other.body.DetailRowsDefinitionID.Object, context);
				}
				if (this.body.FormatStringDefinitionID.Object != null && other.body.FormatStringDefinitionID.Object != null)
				{
					this.body.FormatStringDefinitionID.Object.CopyFrom(other.body.FormatStringDefinitionID.Object, context);
				}
			}
			if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
				this.ChangedProperties.CopyFrom(other.ChangedProperties, context);
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00032406 File Offset: 0x00030606
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Measure)other, context);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00032415 File Offset: 0x00030615
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Measure other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00032431 File Offset: 0x00030631
		public void CopyTo(Measure other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0003244D File Offset: 0x0003064D
		public Measure Clone()
		{
			return base.CloneInternal<Measure>();
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00032458 File Offset: 0x00030658
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.TableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "TableID", this.body.TableID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.Expression))
			{
				writer.WriteProperty<string>(options, "Expression", this.body.Expression);
			}
			if (!string.IsNullOrEmpty(this.body.FormatString))
			{
				writer.WriteProperty<string>(options, "FormatString", this.body.FormatString);
			}
			if (this.body.IsHidden)
			{
				writer.WriteProperty<bool>(options, "IsHidden", this.body.IsHidden);
			}
			if (this.body.IsSimpleMeasure)
			{
				writer.WriteProperty<bool>(options, "IsSimpleMeasure", this.body.IsSimpleMeasure);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder))
			{
				writer.WriteProperty<string>(options, "DisplayFolder", this.body.DisplayFolder);
			}
			if (!string.IsNullOrEmpty(this.body.DataCategory))
			{
				if (!CompatibilityRestrictions.Measure_DataCategory.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataCategory is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "DataCategory", this.body.DataCategory);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Measure_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "LineageTag", this.body.LineageTag);
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Measure_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "SourceLineageTag", this.body.SourceLineageTag);
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000326C8 File Offset: 0x000308C8
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("TableID", out objectId))
			{
				this.body.TableID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("KPIID", out objectId2))
			{
				this.body.KPIID.ObjectID = objectId2;
			}
			ObjectId objectId3;
			if (CompatibilityRestrictions.Measure_DetailRowsDefinition.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("DetailRowsDefinitionID", out objectId3))
			{
				this.body.DetailRowsDefinitionID.ObjectID = objectId3;
			}
			ObjectId objectId4;
			if (CompatibilityRestrictions.Measure_FormatStringDefinition.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("FormatStringDefinitionID", out objectId4))
			{
				this.body.FormatStringDefinitionID.ObjectID = objectId4;
			}
			string text;
			if (reader.TryReadProperty<string>("Name", out text))
			{
				this.body.Name = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("Description", out text2))
			{
				this.body.Description = text2;
			}
			DataType dataType;
			if (reader.TryReadProperty<DataType>("DataType", out dataType))
			{
				this.body.DataType = dataType;
			}
			string text3;
			if (reader.TryReadProperty<string>("Expression", out text3))
			{
				this.body.Expression = text3;
			}
			string text4;
			if (reader.TryReadProperty<string>("FormatString", out text4))
			{
				this.body.FormatString = text4;
			}
			bool flag;
			if (reader.TryReadProperty<bool>("IsHidden", out flag))
			{
				this.body.IsHidden = flag;
			}
			ObjectState objectState;
			if (reader.TryReadProperty<ObjectState>("State", out objectState))
			{
				this.body.State = objectState;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			DateTime dateTime2;
			if (reader.TryReadProperty<DateTime>("StructureModifiedTime", out dateTime2))
			{
				this.body.StructureModifiedTime = dateTime2;
			}
			bool flag2;
			if (reader.TryReadProperty<bool>("IsSimpleMeasure", out flag2))
			{
				this.body.IsSimpleMeasure = flag2;
			}
			string text5;
			if (reader.TryReadProperty<string>("ErrorMessage", out text5))
			{
				this.body.ErrorMessage = text5;
			}
			string text6;
			if (reader.TryReadProperty<string>("DisplayFolder", out text6))
			{
				this.body.DisplayFolder = text6;
			}
			string text7;
			if (CompatibilityRestrictions.Measure_DataCategory.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("DataCategory", out text7))
			{
				this.body.DataCategory = text7;
			}
			string text8;
			if (CompatibilityRestrictions.Measure_LineageTag.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("LineageTag", out text8))
			{
				this.body.LineageTag = text8;
			}
			string text9;
			if (CompatibilityRestrictions.Measure_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("SourceLineageTag", out text9))
			{
				this.body.SourceLineageTag = text9;
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00032948 File Offset: 0x00030B48
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.TableID.Object != null && writer.ShouldIncludeProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.TableID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Expression);
			}
			if (!string.IsNullOrEmpty(this.body.FormatString) && writer.ShouldIncludeProperty("FormatString", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("FormatString", MetadataPropertyNature.RegularProperty, this.body.FormatString);
			}
			if (this.body.IsHidden && writer.ShouldIncludeProperty("IsHidden", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsHidden", MetadataPropertyNature.RegularProperty, this.body.IsHidden);
			}
			if (this.body.IsSimpleMeasure && writer.ShouldIncludeProperty("IsSimpleMeasure", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsSimpleMeasure", MetadataPropertyNature.RegularProperty, this.body.IsSimpleMeasure);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder) && writer.ShouldIncludeProperty("DisplayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("DisplayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.DisplayFolder);
			}
			if (!string.IsNullOrEmpty(this.body.DataCategory))
			{
				if (!CompatibilityRestrictions.Measure_DataCategory.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataCategory is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("DataCategory", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("DataCategory", MetadataPropertyNature.RegularProperty, this.body.DataCategory);
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Measure_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("LineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("LineageTag", MetadataPropertyNature.RegularProperty, this.body.LineageTag);
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Measure_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
				}
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00032CAC File Offset: 0x00030EAC
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Expression);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.DataType != DataType.Unknown && writer.ShouldIncludeProperty("dataType", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteEnumProperty<DataType>("dataType", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.DataType);
			}
			if (!string.IsNullOrEmpty(this.body.FormatString) && writer.ShouldIncludeProperty("formatString", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("formatString", MetadataPropertyNature.RegularProperty, this.body.FormatString);
			}
			if (this.body.IsHidden && writer.ShouldIncludeProperty("isHidden", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isHidden", MetadataPropertyNature.RegularProperty, this.body.IsHidden);
			}
			if (this.body.State != ObjectState.Ready)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.State);
				}
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.StructureModifiedTime);
			}
			if (this.body.IsSimpleMeasure && writer.ShouldIncludeProperty("isSimpleMeasure", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isSimpleMeasure", MetadataPropertyNature.RegularProperty, this.body.IsSimpleMeasure);
			}
			if (!string.IsNullOrEmpty(this.body.ErrorMessage) && writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteStringProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.ErrorMessage);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder) && writer.ShouldIncludeProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.DisplayFolder);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Measure_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("lineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("lineageTag", MetadataPropertyNature.RegularProperty, this.body.LineageTag);
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Measure_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
				}
			}
			if (!string.IsNullOrEmpty(this.body.DataCategory))
			{
				if (!CompatibilityRestrictions.Measure_DataCategory.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataCategory is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("dataCategory", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("dataCategory", MetadataPropertyNature.RegularProperty, this.body.DataCategory);
				}
			}
			if (this.body.KPIID.Object != null && writer.ShouldIncludeProperty("kpi", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteSingleChild(context, "kpi", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Translatable, this.body.KPIID.Object);
			}
			if (this.body.DetailRowsDefinitionID.Object != null)
			{
				if (!CompatibilityRestrictions.Measure_DetailRowsDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DetailRowsDefinitionID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("detailRowsDefinition", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "detailRowsDefinition", MetadataPropertyNature.ChildProperty, this.body.DetailRowsDefinitionID.Object);
				}
			}
			if (this.body.FormatStringDefinitionID.Object != null)
			{
				if (!CompatibilityRestrictions.Measure_FormatStringDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member FormatStringDefinitionID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("formatStringDefinition", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "formatStringDefinition", MetadataPropertyNature.ChildProperty, this.body.FormatStringDefinitionID.Object);
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
			if (this.ChangedProperties.Count > 0)
			{
				if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child ChangedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("changedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "changedProperties", MetadataPropertyNature.ChildCollection, this.ChangedProperties);
				}
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000333B8 File Offset: 0x000315B8
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
				case 3:
					if (propertyName == "kpi")
					{
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							KPI kpi = reader.ReadSingleChildProperty<KPI>(context);
							try
							{
								this.body.KPIID.Object = kpi;
							}
							catch (Exception ex)
							{
								throw reader.CreateInvalidChildException(context, kpi, TomSR.Exception_FailedAddDeserializedObject("KPI", ex.Message), ex);
							}
						}
						return true;
					}
					break;
				case 4:
				{
					char c = propertyName[0];
					if (c != 'N')
					{
						if (c != 'n')
						{
							break;
						}
						if (!(propertyName == "name"))
						{
							break;
						}
					}
					else if (!(propertyName == "Name"))
					{
						break;
					}
					this.body.Name = reader.ReadStringProperty();
					return true;
				}
				case 5:
				{
					char c = propertyName[0];
					if (c != 'K')
					{
						if (c != 'S')
						{
							if (c != 's')
							{
								break;
							}
							if (!(propertyName == "state"))
							{
								break;
							}
						}
						else if (!(propertyName == "State"))
						{
							break;
						}
						ObjectState objectState = reader.ReadEnumProperty<ObjectState>();
						if (!PropertyHelper.IsObjectStateValueCompatible(objectState, context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
							return false;
						}
						this.body.State = objectState;
						return true;
					}
					else if (propertyName == "KPIID")
					{
						this.body.KPIID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
				case 7:
					if (propertyName == "TableID")
					{
						this.body.TableID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 8:
				{
					char c = propertyName[0];
					if (c <= 'I')
					{
						if (c != 'D')
						{
							if (c != 'I')
							{
								break;
							}
							if (!(propertyName == "IsHidden"))
							{
								break;
							}
							goto IL_0678;
						}
						else if (!(propertyName == "DataType"))
						{
							break;
						}
					}
					else if (c != 'd')
					{
						if (c != 'i')
						{
							break;
						}
						if (!(propertyName == "isHidden"))
						{
							break;
						}
						goto IL_0678;
					}
					else if (!(propertyName == "dataType"))
					{
						break;
					}
					this.body.DataType = reader.ReadEnumProperty<DataType>();
					return true;
					IL_0678:
					this.body.IsHidden = reader.ReadBooleanProperty();
					return true;
				}
				case 10:
				{
					char c = propertyName[0];
					if (c <= 'L')
					{
						if (c != 'E')
						{
							if (c != 'L')
							{
								break;
							}
							if (!(propertyName == "LineageTag"))
							{
								break;
							}
							goto IL_0748;
						}
						else if (!(propertyName == "Expression"))
						{
							break;
						}
					}
					else if (c != 'e')
					{
						if (c != 'l')
						{
							break;
						}
						if (!(propertyName == "lineageTag"))
						{
							break;
						}
						goto IL_0748;
					}
					else if (!(propertyName == "expression"))
					{
						break;
					}
					this.body.Expression = reader.ReadStringProperty();
					return true;
					IL_0748:
					if (!CompatibilityRestrictions.Measure_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.LineageTag = reader.ReadStringProperty();
					return true;
				}
				case 11:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'a')
						{
							if (c != 'd')
							{
								break;
							}
							if (!(propertyName == "description"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "annotations"))
							{
								break;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (Annotation annotation in reader.ReadChildCollectionProperty<Annotation>(context))
								{
									try
									{
										this.Annotations.Add(annotation);
									}
									catch (Exception ex2)
									{
										throw reader.CreateInvalidChildException(context, annotation, TomSR.Exception_FailedAddDeserializedNamedObject("Annotation", (annotation != null) ? annotation.Name : null, ex2.Message), ex2);
									}
								}
							}
							return true;
						}
					}
					else if (!(propertyName == "Description"))
					{
						break;
					}
					this.body.Description = reader.ReadStringProperty();
					return true;
				}
				case 12:
				{
					char c = propertyName[0];
					if (c <= 'M')
					{
						switch (c)
						{
						case 'D':
							if (!(propertyName == "DataCategory"))
							{
								goto IL_0AA8;
							}
							goto IL_0718;
						case 'E':
							if (!(propertyName == "ErrorMessage"))
							{
								goto IL_0AA8;
							}
							goto IL_06F2;
						case 'F':
							if (!(propertyName == "FormatString"))
							{
								goto IL_0AA8;
							}
							break;
						default:
							if (c != 'M')
							{
								goto IL_0AA8;
							}
							if (!(propertyName == "ModifiedTime"))
							{
								goto IL_0AA8;
							}
							goto IL_06B9;
						}
					}
					else
					{
						switch (c)
						{
						case 'd':
							if (!(propertyName == "dataCategory"))
							{
								goto IL_0AA8;
							}
							goto IL_0718;
						case 'e':
							if (!(propertyName == "errorMessage"))
							{
								goto IL_0AA8;
							}
							goto IL_06F2;
						case 'f':
							if (!(propertyName == "formatString"))
							{
								goto IL_0AA8;
							}
							break;
						default:
							if (c != 'm')
							{
								goto IL_0AA8;
							}
							if (!(propertyName == "modifiedTime"))
							{
								goto IL_0AA8;
							}
							goto IL_06B9;
						}
					}
					this.body.FormatString = reader.ReadStringProperty();
					return true;
					IL_06B9:
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
					IL_06F2:
					this.body.ErrorMessage = reader.ReadStringProperty();
					return true;
					IL_0718:
					if (!CompatibilityRestrictions.Measure_DataCategory.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.DataCategory = reader.ReadStringProperty();
					return true;
				}
				case 13:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'd')
						{
							break;
						}
						if (!(propertyName == "displayFolder"))
						{
							break;
						}
					}
					else if (!(propertyName == "DisplayFolder"))
					{
						break;
					}
					this.body.DisplayFolder = reader.ReadStringProperty();
					return true;
				}
				case 15:
				{
					char c = propertyName[0];
					if (c != 'I')
					{
						if (c != 'i')
						{
							break;
						}
						if (!(propertyName == "isSimpleMeasure"))
						{
							break;
						}
					}
					else if (!(propertyName == "IsSimpleMeasure"))
					{
						break;
					}
					this.body.IsSimpleMeasure = reader.ReadBooleanProperty();
					return true;
				}
				case 16:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 's')
						{
							break;
						}
						if (!(propertyName == "sourceLineageTag"))
						{
							break;
						}
					}
					else if (!(propertyName == "SourceLineageTag"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Measure_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.SourceLineageTag = reader.ReadStringProperty();
					return true;
				}
				case 17:
					if (propertyName == "changedProperties")
					{
						if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (ChangedProperty changedProperty in reader.ReadChildCollectionProperty<ChangedProperty>(context))
							{
								try
								{
									this.ChangedProperties.Add(changedProperty);
								}
								catch (Exception ex3)
								{
									throw reader.CreateInvalidChildException(context, changedProperty, TomSR.Exception_FailedAddDeserializedObject("ChangedProperty", ex3.Message), ex3);
								}
							}
						}
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
								catch (Exception ex4)
								{
									throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex4.Message), ex4);
								}
							}
						}
						return true;
					}
					break;
				case 20:
					if (propertyName == "detailRowsDefinition")
					{
						if (!CompatibilityRestrictions.Measure_DetailRowsDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							DetailRowsDefinition detailRowsDefinition = reader.ReadSingleChildProperty<DetailRowsDefinition>(context);
							try
							{
								this.body.DetailRowsDefinitionID.Object = detailRowsDefinition;
							}
							catch (Exception ex5)
							{
								throw reader.CreateInvalidChildException(context, detailRowsDefinition, TomSR.Exception_FailedAddDeserializedObject("DetailRowsDefinition", ex5.Message), ex5);
							}
						}
						return true;
					}
					break;
				case 21:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 's')
						{
							break;
						}
						if (!(propertyName == "structureModifiedTime"))
						{
							break;
						}
					}
					else if (!(propertyName == "StructureModifiedTime"))
					{
						break;
					}
					this.body.StructureModifiedTime = reader.ReadDateTimeProperty();
					return true;
				}
				case 22:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c == 'f')
						{
							if (propertyName == "formatStringDefinition")
							{
								if (!CompatibilityRestrictions.Measure_FormatStringDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
								{
									classification = UnexpectedPropertyClassification.IncompatibleProperty;
									return false;
								}
								using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
								{
									FormatStringDefinition formatStringDefinition = reader.ReadSingleChildProperty<FormatStringDefinition>(context);
									try
									{
										this.body.FormatStringDefinitionID.Object = formatStringDefinition;
									}
									catch (Exception ex6)
									{
										throw reader.CreateInvalidChildException(context, formatStringDefinition, TomSR.Exception_FailedAddDeserializedObject("FormatStringDefinition", ex6.Message), ex6);
									}
								}
								return true;
							}
						}
					}
					else if (propertyName == "DetailRowsDefinitionID")
					{
						if (!CompatibilityRestrictions.Measure_DetailRowsDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							return false;
						}
						this.body.DetailRowsDefinitionID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
				case 24:
					if (propertyName == "FormatStringDefinitionID")
					{
						if (!CompatibilityRestrictions.Measure_FormatStringDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							return false;
						}
						this.body.FormatStringDefinitionID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
			}
			IL_0AA8:
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00033F2C File Offset: 0x0003212C
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00033F35 File Offset: 0x00032135
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00033F58 File Offset: 0x00032158
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && this.body.DataType != DataType.Unknown)
			{
				result["dataType", TomPropCategory.Regular, 4, true] = JsonPropertyHelper.ConvertEnumToJsonValue<DataType>(this.body.DataType);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Expression))
			{
				result["expression", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Expression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.FormatString))
			{
				result["formatString", TomPropCategory.Regular, 6, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.FormatString, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsHidden)
			{
				result["isHidden", TomPropCategory.Regular, 7, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsHidden);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && this.body.State != ObjectState.Ready)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["state", TomPropCategory.Regular, 8, true] = JsonPropertyHelper.ConvertEnumToJsonValue<ObjectState>(this.body.State);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 9, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["structureModifiedTime", TomPropCategory.Regular, 10, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.StructureModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsSimpleMeasure)
			{
				result["isSimpleMeasure", TomPropCategory.Regular, 12, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsSimpleMeasure);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && !string.IsNullOrEmpty(this.body.ErrorMessage))
			{
				result["errorMessage", TomPropCategory.Regular, 13, true] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.ErrorMessage, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder))
			{
				result["displayFolder", TomPropCategory.Regular, 14, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.DisplayFolder, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.DataCategory))
			{
				if (!CompatibilityRestrictions.Measure_DataCategory.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataCategory is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["dataCategory", TomPropCategory.Regular, 116, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.DataCategory, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Measure_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["lineageTag", TomPropCategory.Regular, 18, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.LineageTag, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Measure_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["sourceLineageTag", TomPropCategory.Regular, 19, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.SourceLineageTag, SplitMultilineOptions.None);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (this.body.KPIID.Object != null && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.body.KPIID.Object)))
				{
					result["kpi", TomPropCategory.ChildLink, 11, false] = this.body.KPIID.Object.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
				}
				if (this.body.DetailRowsDefinitionID.Object != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.body.DetailRowsDefinitionID.Object)))
				{
					if (!CompatibilityRestrictions.Measure_DetailRowsDefinition.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("Member DetailRowsDefinitionID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					result["detailRowsDefinition", TomPropCategory.ChildLink, 15, false] = this.body.DetailRowsDefinitionID.Object.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
				}
				if (this.body.FormatStringDefinitionID.Object != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.body.FormatStringDefinitionID.Object)))
				{
					if (!CompatibilityRestrictions.Measure_FormatStringDefinition.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("Member FormatStringDefinitionID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					result["formatStringDefinition", TomPropCategory.ChildLink, 17, false] = this.body.FormatStringDefinitionID.Object.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
				}
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ExtendedProperty> enumerable;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ExtendedProperty> extendedProperties = this.ExtendedProperties;
						enumerable = extendedProperties;
					}
					else
					{
						enumerable = this.ExtendedProperties.Where((ExtendedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ExtendedProperty> enumerable2 = enumerable;
					if (enumerable2.Any<ExtendedProperty>())
					{
						if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable2.Select((ExtendedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array2 = array;
						result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array2;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ChangedProperty> enumerable3;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ChangedProperty> changedProperties = this.ChangedProperties;
						enumerable3 = changedProperties;
					}
					else
					{
						enumerable3 = this.ChangedProperties.Where((ChangedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ChangedProperty> enumerable4 = enumerable3;
					if (enumerable4.Any<ChangedProperty>())
					{
						if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ChangedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable4.Select((ChangedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array3 = array;
						result["changedProperties", TomPropCategory.ChildCollection, 52, false] = array3;
					}
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array4;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000348D4 File Offset: 0x00032AD4
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				switch (name.Length)
				{
				case 3:
					if (name == "kpi")
					{
						if (jsonProp.Value.Type != 10)
						{
							KPI kpi = new KPI();
							kpi.DeserializeFromJsonObject(jsonProp.Value, options, mode, dbCompatibilityLevel);
							this.body.KPIID.Object = kpi;
						}
						return true;
					}
					break;
				case 4:
					if (name == "name")
					{
						this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 5:
					if (name == "state")
					{
						ObjectState objectState = JsonPropertyHelper.ConvertJsonValueToEnum<ObjectState>(jsonProp.Value);
						if (jsonProp.Value.Type != 10 && !PropertyHelper.IsObjectStateValueCompatible(objectState, mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.State = objectState;
						return true;
					}
					break;
				case 8:
				{
					char c = name[0];
					if (c != 'd')
					{
						if (c == 'i')
						{
							if (name == "isHidden")
							{
								this.body.IsHidden = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "dataType")
					{
						this.body.DataType = JsonPropertyHelper.ConvertJsonValueToEnum<DataType>(jsonProp.Value);
						return true;
					}
					break;
				}
				case 10:
				{
					char c = name[0];
					if (c != 'e')
					{
						if (c == 'l')
						{
							if (name == "lineageTag")
							{
								if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Measure_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
								{
									return false;
								}
								this.body.LineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "expression")
					{
						this.body.Expression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				case 11:
				{
					char c = name[0];
					if (c != 'a')
					{
						if (c == 'd')
						{
							if (name == "description")
							{
								this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
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
				{
					char c = name[0];
					switch (c)
					{
					case 'd':
						if (name == "dataCategory")
						{
							if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Measure_DataCategory.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							this.body.DataCategory = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
							return true;
						}
						break;
					case 'e':
						if (name == "errorMessage")
						{
							this.body.ErrorMessage = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
							return true;
						}
						break;
					case 'f':
						if (name == "formatString")
						{
							this.body.FormatString = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
							return true;
						}
						break;
					default:
						if (c == 'm')
						{
							if (name == "modifiedTime")
							{
								this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
								return true;
							}
						}
						break;
					}
					break;
				}
				case 13:
					if (name == "displayFolder")
					{
						this.body.DisplayFolder = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 15:
					if (name == "isSimpleMeasure")
					{
						this.body.IsSimpleMeasure = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				case 16:
					if (name == "sourceLineageTag")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Measure_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.SourceLineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 17:
					if (name == "changedProperties")
					{
						if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						JsonPropertyHelper.ReadObjectCollection(this.ChangedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 18:
					if (name == "extendedProperties")
					{
						if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						JsonPropertyHelper.ReadObjectCollection(this.ExtendedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 20:
					if (name == "detailRowsDefinition")
					{
						if (jsonProp.Value.Type != 10)
						{
							if (!CompatibilityRestrictions.Measure_DetailRowsDefinition.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							DetailRowsDefinition detailRowsDefinition = new DetailRowsDefinition();
							detailRowsDefinition.DeserializeFromJsonObject(jsonProp.Value, options, mode, dbCompatibilityLevel);
							this.body.DetailRowsDefinitionID.Object = detailRowsDefinition;
						}
						return true;
					}
					break;
				case 21:
					if (name == "structureModifiedTime")
					{
						this.body.StructureModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
						return true;
					}
					break;
				case 22:
					if (name == "formatStringDefinition")
					{
						if (jsonProp.Value.Type != 10)
						{
							if (!CompatibilityRestrictions.Measure_FormatStringDefinition.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							FormatStringDefinition formatStringDefinition = new FormatStringDefinition();
							formatStringDefinition.DeserializeFromJsonObject(jsonProp.Value, options, mode, dbCompatibilityLevel);
							this.body.FormatStringDefinitionID.Object = formatStringDefinition;
						}
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00034EC4 File Offset: 0x000330C4
		internal override IEnumerable<MetadataObject> GetNameLinkedObjects(string objectName = null)
		{
			if (objectName == null)
			{
				objectName = this.Name;
			}
			if (this.Table == null || base.Model == null)
			{
				yield break;
			}
			foreach (Perspective perspective in base.Model.Perspectives)
			{
				PerspectiveTable perspectiveTable = perspective.PerspectiveTables.Find(this.Table.Name);
				if (perspectiveTable != null)
				{
					PerspectiveMeasure perspectiveMeasure = perspectiveTable.PerspectiveMeasures.Find(objectName);
					if (perspectiveMeasure != null)
					{
						yield return perspectiveMeasure;
					}
				}
			}
			IEnumerator<Perspective> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00034EDB File Offset: 0x000330DB
		internal override string GetFormattedObjectPath()
		{
			if (this.Table != null)
			{
				return TomSR.ObjectPath_Measure_2Args(this.Name, this.Table.Name);
			}
			return TomSR.ObjectPath_Measure_1Arg(this.Name);
		}

		// Token: 0x04000115 RID: 277
		internal Measure.ObjectBody body;

		// Token: 0x04000116 RID: 278
		private MeasureAnnotationCollection _Annotations;

		// Token: 0x04000117 RID: 279
		private MeasureExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x04000118 RID: 280
		private MeasureChangedPropertyCollection _ChangedProperties;

		// Token: 0x0200027F RID: 639
		internal class ObjectBody : NamedMetadataObjectBody<Measure>
		{
			// Token: 0x060020D8 RID: 8408 RVA: 0x000D6968 File Offset: 0x000D4B68
			public ObjectBody(Measure owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.StructureModifiedTime = DateTime.MinValue;
				this.TableID = new ParentLink<Measure, Table>(owner, "Table");
				this.KPIID = new ChildLink<Measure, KPI>(owner, "KPI");
				this.DetailRowsDefinitionID = new ChildLink<Measure, DetailRowsDefinition>(owner, "DetailRowsDefinition");
				this.FormatStringDefinitionID = new ChildLink<Measure, FormatStringDefinition>(owner, "FormatStringDefinition");
			}

			// Token: 0x060020D9 RID: 8409 RVA: 0x000D69D6 File Offset: 0x000D4BD6
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x060020DA RID: 8410 RVA: 0x000D69E0 File Offset: 0x000D4BE0
			internal bool IsEqualTo(Measure.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.DataType, other.DataType)) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.FormatString, other.FormatString) && PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime)) && PropertyHelper.AreValuesIdentical(this.IsSimpleMeasure, other.IsSimpleMeasure) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage)) && PropertyHelper.AreValuesIdentical(this.DisplayFolder, other.DisplayFolder) && PropertyHelper.AreValuesIdentical(this.DataCategory, other.DataCategory) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.TableID.IsEqualTo(other.TableID, context)) && this.KPIID.IsEqualTo(other.KPIID, context) && this.DetailRowsDefinitionID.IsEqualTo(other.DetailRowsDefinitionID, context) && this.FormatStringDefinitionID.IsEqualTo(other.FormatStringDefinitionID, context);
			}

			// Token: 0x060020DB RID: 8411 RVA: 0x000D6BF4 File Offset: 0x000D4DF4
			internal void CopyFromImpl(Measure.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.Description = other.Description;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.DataType = other.DataType;
				}
				this.Expression = other.Expression;
				this.FormatString = other.FormatString;
				this.IsHidden = other.IsHidden;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.State = other.State;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.StructureModifiedTime = other.StructureModifiedTime;
				}
				this.IsSimpleMeasure = other.IsSimpleMeasure;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ErrorMessage = other.ErrorMessage;
				}
				this.DisplayFolder = other.DisplayFolder;
				this.DataCategory = other.DataCategory;
				base.Owner.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.TableID.CopyFrom(other.TableID, context);
				}
				this.KPIID.CopyFrom(other.KPIID, context);
				this.DetailRowsDefinitionID.CopyFrom(other.DetailRowsDefinitionID, context);
				this.FormatStringDefinitionID.CopyFrom(other.FormatStringDefinitionID, context);
			}

			// Token: 0x060020DC RID: 8412 RVA: 0x000D6D8C File Offset: 0x000D4F8C
			internal void CopyFromImpl(Measure.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.DataType = other.DataType;
				this.Expression = other.Expression;
				this.FormatString = other.FormatString;
				this.IsHidden = other.IsHidden;
				this.State = other.State;
				this.ModifiedTime = other.ModifiedTime;
				this.StructureModifiedTime = other.StructureModifiedTime;
				this.IsSimpleMeasure = other.IsSimpleMeasure;
				this.ErrorMessage = other.ErrorMessage;
				this.DisplayFolder = other.DisplayFolder;
				this.DataCategory = other.DataCategory;
				this.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				this.TableID.CopyFrom(other.TableID, ObjectChangeTracker.BodyCloneContext);
				this.KPIID.CopyFrom(other.KPIID, ObjectChangeTracker.BodyCloneContext);
				this.DetailRowsDefinitionID.CopyFrom(other.DetailRowsDefinitionID, ObjectChangeTracker.BodyCloneContext);
				this.FormatStringDefinitionID.CopyFrom(other.FormatStringDefinitionID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x060020DD RID: 8413 RVA: 0x000D6EA5 File Offset: 0x000D50A5
			public override void CopyFrom(MetadataObjectBody<Measure> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Measure.ObjectBody)other, context);
			}

			// Token: 0x060020DE RID: 8414 RVA: 0x000D6EBC File Offset: 0x000D50BC
			internal bool IsEqualTo(Measure.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.DataType, other.DataType) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.FormatString, other.FormatString) && PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden) && PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime) && PropertyHelper.AreValuesIdentical(this.IsSimpleMeasure, other.IsSimpleMeasure) && PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage) && PropertyHelper.AreValuesIdentical(this.DisplayFolder, other.DisplayFolder) && PropertyHelper.AreValuesIdentical(this.DataCategory, other.DataCategory) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && this.TableID.IsEqualTo(other.TableID) && this.KPIID.IsEqualTo(other.KPIID) && this.DetailRowsDefinitionID.IsEqualTo(other.DetailRowsDefinitionID) && this.FormatStringDefinitionID.IsEqualTo(other.FormatStringDefinitionID);
			}

			// Token: 0x060020DF RID: 8415 RVA: 0x000D7059 File Offset: 0x000D5259
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Measure.ObjectBody)other);
			}

			// Token: 0x060020E0 RID: 8416 RVA: 0x000D7074 File Offset: 0x000D5274
			internal void CompareWith(Measure.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DataType, other.DataType))
				{
					context.RegisterPropertyChange(base.Owner, "DataType", typeof(DataType), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.DataType, this.DataType);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Expression, other.Expression))
				{
					context.RegisterPropertyChange(base.Owner, "Expression", typeof(string), PropertyFlags.DdlAndUser, other.Expression, this.Expression);
				}
				if (!PropertyHelper.AreValuesIdentical(this.FormatString, other.FormatString))
				{
					context.RegisterPropertyChange(base.Owner, "FormatString", typeof(string), PropertyFlags.DdlAndUser, other.FormatString, this.FormatString);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden))
				{
					context.RegisterPropertyChange(base.Owner, "IsHidden", typeof(bool), PropertyFlags.DdlAndUser, other.IsHidden, this.IsHidden);
				}
				if (!PropertyHelper.AreValuesIdentical(this.State, other.State))
				{
					context.RegisterPropertyChange(base.Owner, "State", typeof(ObjectState), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.State, this.State);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "StructureModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.StructureModifiedTime, this.StructureModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsSimpleMeasure, other.IsSimpleMeasure))
				{
					context.RegisterPropertyChange(base.Owner, "IsSimpleMeasure", typeof(bool), PropertyFlags.DdlAndUser, other.IsSimpleMeasure, this.IsSimpleMeasure);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage))
				{
					context.RegisterPropertyChange(base.Owner, "ErrorMessage", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ErrorMessage, this.ErrorMessage);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DisplayFolder, other.DisplayFolder))
				{
					context.RegisterPropertyChange(base.Owner, "DisplayFolder", typeof(string), PropertyFlags.DdlAndUser, other.DisplayFolder, this.DisplayFolder);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DataCategory, other.DataCategory))
				{
					context.RegisterPropertyChange(base.Owner, "DataCategory", typeof(string), PropertyFlags.DdlAndUser, other.DataCategory, this.DataCategory);
				}
				if (!PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "LineageTag", typeof(string), PropertyFlags.DdlAndUser, other.LineageTag, this.LineageTag);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "SourceLineageTag", typeof(string), PropertyFlags.DdlAndUser, other.SourceLineageTag, this.SourceLineageTag);
				}
				this.TableID.CompareWith(other.TableID, "TableID", "Table", PropertyFlags.ReadOnly, context);
				this.KPIID.CompareWith(other.KPIID, "KPIID", "KPI", PropertyFlags.None, context);
				this.DetailRowsDefinitionID.CompareWith(other.DetailRowsDefinitionID, "DetailRowsDefinitionID", "DetailRowsDefinition", PropertyFlags.None, context);
				this.FormatStringDefinitionID.CompareWith(other.FormatStringDefinitionID, "FormatStringDefinitionID", "FormatStringDefinition", PropertyFlags.None, context);
			}

			// Token: 0x060020E1 RID: 8417 RVA: 0x000D74B1 File Offset: 0x000D56B1
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Measure.ObjectBody)other, context);
			}

			// Token: 0x040008A3 RID: 2211
			internal string Name;

			// Token: 0x040008A4 RID: 2212
			internal string Description;

			// Token: 0x040008A5 RID: 2213
			internal DataType DataType;

			// Token: 0x040008A6 RID: 2214
			internal string Expression;

			// Token: 0x040008A7 RID: 2215
			internal string FormatString;

			// Token: 0x040008A8 RID: 2216
			internal bool IsHidden;

			// Token: 0x040008A9 RID: 2217
			internal ObjectState State;

			// Token: 0x040008AA RID: 2218
			internal DateTime ModifiedTime;

			// Token: 0x040008AB RID: 2219
			internal DateTime StructureModifiedTime;

			// Token: 0x040008AC RID: 2220
			internal bool IsSimpleMeasure;

			// Token: 0x040008AD RID: 2221
			internal string ErrorMessage;

			// Token: 0x040008AE RID: 2222
			internal string DisplayFolder;

			// Token: 0x040008AF RID: 2223
			internal string DataCategory;

			// Token: 0x040008B0 RID: 2224
			internal string LineageTag;

			// Token: 0x040008B1 RID: 2225
			internal string SourceLineageTag;

			// Token: 0x040008B2 RID: 2226
			internal ParentLink<Measure, Table> TableID;

			// Token: 0x040008B3 RID: 2227
			internal ChildLink<Measure, KPI> KPIID;

			// Token: 0x040008B4 RID: 2228
			internal ChildLink<Measure, DetailRowsDefinition> DetailRowsDefinitionID;

			// Token: 0x040008B5 RID: 2229
			internal ChildLink<Measure, FormatStringDefinition> FormatStringDefinitionID;
		}
	}
}
