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
	// Token: 0x02000059 RID: 89
	[CompatibilityRequirement("1400")]
	public abstract class ExtendedProperty : NamedMetadataObject
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x00021CC6 File Offset: 0x0001FEC6
		private protected ExtendedProperty()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00021CD4 File Offset: 0x0001FED4
		private protected ExtendedProperty(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00021CE2 File Offset: 0x0001FEE2
		private void InitBodyAndCollections()
		{
			this.body = new ExtendedProperty.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Type = ExtendedPropertyType.String;
			this.body.Value = string.Empty;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00021D1C File Offset: 0x0001FF1C
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.ExtendedProperty;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00021D20 File Offset: 0x0001FF20
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00021D32 File Offset: 0x0001FF32
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
					MetadataObject.UpdateMetadataObjectParent<ExtendedProperty, MetadataObject>(this.body.ObjectID, value, null, null);
				}
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00021D5A File Offset: 0x0001FF5A
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ObjectID.ObjectID;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00021D6C File Offset: 0x0001FF6C
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateChoiceScope())
			{
				ExtendedProperty.WriteMetadataSchemaForStringExtendedProperty(context, writer);
				ExtendedProperty.WriteMetadataSchemaForJsonExtendedProperty(context, writer);
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00021DAC File Offset: 0x0001FFAC
		private static void WriteMetadataSchemaForStringExtendedProperty(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.ExtendedProperty, "StringExtendedProperty", "StringExtendedProperty object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty))
				{
					writer.WriteEnumProperty<ExtendedPropertyType>("type", MetadataPropertyNature.TypeProperty, null);
				}
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteProperty("value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, typeof(string));
				}
				ExtendedProperty.WriteMetadataSchemaForCommonExtendedPropertyRegularProperties(context, writer);
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00021E68 File Offset: 0x00020068
		private static void WriteMetadataSchemaForJsonExtendedProperty(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.ExtendedProperty, "JsonExtendedProperty", "JsonExtendedProperty object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty))
				{
					writer.WriteEnumProperty<ExtendedPropertyType>("type", MetadataPropertyNature.TypeProperty, null);
				}
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteProperty("value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString | MetadataPropertyNature.DefaultProperty, typeof(string));
				}
				ExtendedProperty.WriteMetadataSchemaForCommonExtendedPropertyRegularProperties(context, writer);
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00021F24 File Offset: 0x00020124
		private static void WriteMetadataSchemaForCommonExtendedPropertyRegularProperties(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00021F54 File Offset: 0x00020154
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.ExtendedProperty[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.Type != ExtendedPropertyType.String)
			{
				int num = PropertyHelper.GetExtendedPropertyTypeCompatibilityRestrictions(this.body.Type)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Type");
					requiredLevel = num;
					int num2 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00021FE7 File Offset: 0x000201E7
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00021FEF File Offset: 0x000201EF
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (ExtendedProperty.ObjectBody)value;
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00021FFD File Offset: 0x000201FD
		internal override ITxObjectBody CreateBody()
		{
			return new ExtendedProperty.ObjectBody(this);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00022008 File Offset: 0x00020208
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			ObjectType objectType = parent.ObjectType;
			switch (objectType)
			{
			case ObjectType.Model:
				return ((Model)parent).ExtendedProperties;
			case ObjectType.DataSource:
				return ((DataSource)parent).ExtendedProperties;
			case ObjectType.Table:
				return ((Table)parent).ExtendedProperties;
			case ObjectType.Column:
				return ((Column)parent).ExtendedProperties;
			case ObjectType.AttributeHierarchy:
				return ((AttributeHierarchy)parent).ExtendedProperties;
			case ObjectType.Partition:
				return ((Partition)parent).ExtendedProperties;
			case ObjectType.Relationship:
				return ((Relationship)parent).ExtendedProperties;
			case ObjectType.Measure:
				return ((Measure)parent).ExtendedProperties;
			case ObjectType.Hierarchy:
				return ((Hierarchy)parent).ExtendedProperties;
			case ObjectType.Level:
				return ((Level)parent).ExtendedProperties;
			case ObjectType.Annotation:
			case ObjectType.ObjectTranslation:
			case (ObjectType)16:
			case (ObjectType)17:
			case (ObjectType)18:
			case (ObjectType)19:
			case (ObjectType)20:
			case (ObjectType)21:
			case (ObjectType)22:
			case (ObjectType)23:
			case (ObjectType)24:
			case (ObjectType)25:
			case (ObjectType)26:
			case (ObjectType)27:
			case (ObjectType)28:
			case ObjectType.ExtendedProperty:
			case ObjectType.DetailRowsDefinition:
			case ObjectType.RelatedColumnDetails:
			case ObjectType.GroupByColumn:
			case ObjectType.CalculationGroup:
			case ObjectType.CalculationItem:
			case ObjectType.AlternateOf:
				break;
			case ObjectType.KPI:
				return ((KPI)parent).ExtendedProperties;
			case ObjectType.Culture:
				return ((Culture)parent).ExtendedProperties;
			case ObjectType.LinguisticMetadata:
				return ((LinguisticMetadata)parent).ExtendedProperties;
			case ObjectType.Perspective:
				return ((Perspective)parent).ExtendedProperties;
			case ObjectType.PerspectiveTable:
				return ((PerspectiveTable)parent).ExtendedProperties;
			case ObjectType.PerspectiveColumn:
				return ((PerspectiveColumn)parent).ExtendedProperties;
			case ObjectType.PerspectiveHierarchy:
				return ((PerspectiveHierarchy)parent).ExtendedProperties;
			case ObjectType.PerspectiveMeasure:
				return ((PerspectiveMeasure)parent).ExtendedProperties;
			case ObjectType.Role:
				return ((ModelRole)parent).ExtendedProperties;
			case ObjectType.RoleMembership:
				return ((ModelRoleMember)parent).ExtendedProperties;
			case ObjectType.TablePermission:
				return ((TablePermission)parent).ExtendedProperties;
			case ObjectType.Variation:
				return ((Variation)parent).ExtendedProperties;
			case ObjectType.Set:
				return ((Set)parent).ExtendedProperties;
			case ObjectType.PerspectiveSet:
				return ((PerspectiveSet)parent).ExtendedProperties;
			case ObjectType.Expression:
				return ((NamedExpression)parent).ExtendedProperties;
			case ObjectType.ColumnPermission:
				return ((ColumnPermission)parent).ExtendedProperties;
			case ObjectType.RefreshPolicy:
				return ((RefreshPolicy)parent).ExtendedProperties;
			default:
				if (objectType == ObjectType.Function)
				{
					return ((Function)parent).ExtendedProperties;
				}
				if (objectType == ObjectType.BindingInfo)
				{
					return ((BindingInfo)parent).ExtendedProperties;
				}
				break;
			}
			throw TomInternalException.Create("Got a parent object of unexpected type: {0}", new object[] { parent.GetType().Name });
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00022274 File Offset: 0x00020474
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			MetadataObject metadataObject = MetadataObject.ResolveMetadataObjectParentById<ExtendedProperty, MetadataObject>(this.body.ObjectID, objectMap, throwIfCantResolve, null, null);
			if (metadataObject != null)
			{
				ObjectType objectType = metadataObject.ObjectType;
				switch (objectType)
				{
				case ObjectType.Model:
					((Model)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.DataSource:
					((DataSource)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Table:
					((Table)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Column:
					((Column)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.AttributeHierarchy:
					((AttributeHierarchy)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Partition:
					((Partition)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Relationship:
					((Relationship)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Measure:
					((Measure)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Hierarchy:
					((Hierarchy)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Level:
					((Level)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Annotation:
				case ObjectType.ObjectTranslation:
				case (ObjectType)16:
				case (ObjectType)17:
				case (ObjectType)18:
				case (ObjectType)19:
				case (ObjectType)20:
				case (ObjectType)21:
				case (ObjectType)22:
				case (ObjectType)23:
				case (ObjectType)24:
				case (ObjectType)25:
				case (ObjectType)26:
				case (ObjectType)27:
				case (ObjectType)28:
				case ObjectType.ExtendedProperty:
				case ObjectType.DetailRowsDefinition:
				case ObjectType.RelatedColumnDetails:
				case ObjectType.GroupByColumn:
				case ObjectType.CalculationGroup:
				case ObjectType.CalculationItem:
				case ObjectType.AlternateOf:
					break;
				case ObjectType.KPI:
					((KPI)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Culture:
					((Culture)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.LinguisticMetadata:
					((LinguisticMetadata)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Perspective:
					((Perspective)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.PerspectiveTable:
					((PerspectiveTable)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.PerspectiveColumn:
					((PerspectiveColumn)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.PerspectiveHierarchy:
					((PerspectiveHierarchy)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.PerspectiveMeasure:
					((PerspectiveMeasure)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Role:
					((ModelRole)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.RoleMembership:
					((ModelRoleMember)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.TablePermission:
					((TablePermission)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Variation:
					((Variation)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Set:
					((Set)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.PerspectiveSet:
					((PerspectiveSet)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.Expression:
					((NamedExpression)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.ColumnPermission:
					((ColumnPermission)metadataObject).ExtendedProperties.Add(this);
					return;
				case ObjectType.RefreshPolicy:
					((RefreshPolicy)metadataObject).ExtendedProperties.Add(this);
					return;
				default:
					if (objectType == ObjectType.Function)
					{
						((Function)metadataObject).ExtendedProperties.Add(this);
						return;
					}
					if (objectType == ObjectType.BindingInfo)
					{
						((BindingInfo)metadataObject).ExtendedProperties.Add(this);
						return;
					}
					break;
				}
				throw TomInternalException.Create("Got a parent object of unexpected type: {0}", new object[] { metadataObject.GetType().Name });
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000225AD File Offset: 0x000207AD
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000225AF File Offset: 0x000207AF
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x000225BC File Offset: 0x000207BC
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.ExtendedProperty, out text))
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

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0002263F File Offset: 0x0002083F
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0002264C File Offset: 0x0002084C
		public ExtendedPropertyType Type
		{
			get
			{
				return this.body.Type;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Type, value))
				{
					CompatibilityRestrictionSet extendedPropertyTypeCompatibilityRestrictions = PropertyHelper.GetExtendedPropertyTypeCompatibilityRestrictions(value);
					CompatibilityRestrictionSet extendedPropertyTypeCompatibilityRestrictions2 = PropertyHelper.GetExtendedPropertyTypeCompatibilityRestrictions(this.body.Type);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = extendedPropertyTypeCompatibilityRestrictions.Compare(extendedPropertyTypeCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != ExtendedPropertyType.String))
					{
						array = base.ValidateCompatibilityRequirement(extendedPropertyTypeCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "Type", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Type", typeof(ExtendedPropertyType), this.body.Type, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(extendedPropertyTypeCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(extendedPropertyTypeCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(extendedPropertyTypeCompatibilityRestrictions, array);
						break;
					}
					ExtendedPropertyType type = this.body.Type;
					this.body.Type = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Type", typeof(ExtendedPropertyType), type, value);
				}
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0002276D File Offset: 0x0002096D
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0002277C File Offset: 0x0002097C
		internal string Value
		{
			get
			{
				return this.body.Value;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Value, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Value", typeof(string), this.body.Value, value);
					string value2 = this.body.Value;
					this.body.Value = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Value", typeof(string), value2, value);
				}
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x000227EC File Offset: 0x000209EC
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x000227FC File Offset: 0x000209FC
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

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00022880 File Offset: 0x00020A80
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00022894 File Offset: 0x00020A94
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

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00022918 File Offset: 0x00020B18
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0002292A File Offset: 0x00020B2A
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

		// Token: 0x06000481 RID: 1153 RVA: 0x00022940 File Offset: 0x00020B40
		internal void CopyFrom(ExtendedProperty other, CopyContext context)
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

		// Token: 0x06000482 RID: 1154 RVA: 0x000229D1 File Offset: 0x00020BD1
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((ExtendedProperty)other, context);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000229E0 File Offset: 0x00020BE0
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(ExtendedProperty other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000229FC File Offset: 0x00020BFC
		public void CopyTo(ExtendedProperty other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00022A18 File Offset: 0x00020C18
		public ExtendedProperty Clone()
		{
			return base.CloneInternal<ExtendedProperty>();
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00022A20 File Offset: 0x00020C20
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.ObjectID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ObjectID", this.body.ObjectID.Object);
				writer.WriteProperty<int>(options, "ObjectType", (int)this.body.ObjectID.Object.ObjectType);
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
			if (this.body.Type != ExtendedPropertyType.String)
			{
				if (!PropertyHelper.IsExtendedPropertyTypeValueCompatible(this.body.Type, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<ExtendedPropertyType>(options, "Type", this.body.Type);
			}
			if (!string.IsNullOrEmpty(this.body.Value))
			{
				writer.WriteProperty<string>(options, "Value", this.body.Value);
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00022B6C File Offset: 0x00020D6C
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ObjectID", out objectId))
			{
				this.body.ObjectID.ObjectID = objectId;
			}
			string text;
			if (reader.TryReadProperty<string>("Name", out text))
			{
				this.body.Name = text;
			}
			ExtendedPropertyType extendedPropertyType;
			if (reader.TryReadProperty<ExtendedPropertyType>("Type", out extendedPropertyType))
			{
				this.body.Type = extendedPropertyType;
			}
			string text2;
			if (reader.TryReadProperty<string>("Value", out text2))
			{
				this.body.Value = text2;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00022C10 File Offset: 0x00020E10
		private protected sealed override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.ObjectID.Object != null && writer.ShouldIncludeProperty("ObjectID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("ObjectID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.ObjectID.Object);
				writer.WriteObjectTypeProperty("ObjectType", MetadataPropertyNature.LinkTypeProperty, this.body.ObjectID.Object.ObjectType);
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (this.body.Type != ExtendedPropertyType.String)
			{
				if (!PropertyHelper.IsExtendedPropertyTypeValueCompatible(this.body.Type, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("Type", MetadataPropertyNature.TypeProperty))
				{
					writer.WriteEnumProperty<ExtendedPropertyType>("Type", MetadataPropertyNature.TypeProperty, this.body.Type);
				}
			}
			if (!string.IsNullOrEmpty(this.body.Value) && writer.ShouldIncludeProperty("Value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("Value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty, this.body.Value);
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00022DCC File Offset: 0x00020FCC
		private protected virtual void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00022E20 File Offset: 0x00021020
		private protected sealed override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (this.body.Type != ExtendedPropertyType.String)
			{
				if (!PropertyHelper.IsExtendedPropertyTypeValueCompatible(this.body.Type, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty))
				{
					writer.WriteEnumProperty<ExtendedPropertyType>("type", MetadataPropertyNature.TypeProperty, this.body.Type);
				}
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			this.WriteRegularPropertiesToMetadataStream(context, writer);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00022F40 File Offset: 0x00021140
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName != null)
			{
				int length = propertyName.Length;
				switch (length)
				{
				case 4:
				{
					char c = propertyName[0];
					if (c <= 'T')
					{
						if (c != 'N')
						{
							if (c != 'T')
							{
								break;
							}
							if (!(propertyName == "Type"))
							{
								break;
							}
							goto IL_0156;
						}
						else if (!(propertyName == "Name"))
						{
							break;
						}
					}
					else if (c != 'n')
					{
						if (c != 't')
						{
							break;
						}
						if (!(propertyName == "type"))
						{
							break;
						}
						goto IL_0156;
					}
					else if (!(propertyName == "name"))
					{
						break;
					}
					this.body.Name = reader.ReadStringProperty();
					return true;
					IL_0156:
					if (!CompatibilityRestrictions.ExtendedPropertyType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.Type = reader.ReadEnumProperty<ExtendedPropertyType>();
					return true;
				}
				case 5:
					if (propertyName == "Value")
					{
						this.body.Value = reader.ReadStringProperty();
						return true;
					}
					break;
				case 6:
				case 7:
					break;
				case 8:
					if (propertyName == "ObjectID")
					{
						this.body.ObjectID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				default:
					if (length == 12)
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
					break;
				}
			}
			return false;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000230FA File Offset: 0x000212FA
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00023103 File Offset: 0x00021303
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00023124 File Offset: 0x00021324
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.Type != ExtendedPropertyType.String)
			{
				if (!PropertyHelper.IsExtendedPropertyTypeValueCompatible(this.body.Type, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["type", TomPropCategory.Type, 3, false] = JsonPropertyHelper.ConvertEnumToJsonValue<ExtendedPropertyType>(this.body.Type);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 5, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00023250 File Offset: 0x00021450
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "name")
			{
				this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (!(name == "type"))
			{
				if (!(name == "modifiedTime"))
				{
					return false;
				}
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			else
			{
				ExtendedPropertyType extendedPropertyType = JsonPropertyHelper.ConvertJsonValueToEnum<ExtendedPropertyType>(jsonProp.Value);
				if (jsonProp.Value.Type != 10 && !PropertyHelper.IsExtendedPropertyTypeValueCompatible(extendedPropertyType, mode, dbCompatibilityLevel))
				{
					return false;
				}
				this.body.Type = extendedPropertyType;
				return true;
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000232F4 File Offset: 0x000214F4
		internal static ExtendedProperty CreateFromMetadataStream(SerializationActivityContext context, IMetadataReader reader)
		{
			ExtendedPropertyType extendedPropertyType;
			if (reader.TryMoveToProperty("type"))
			{
				extendedPropertyType = reader.ReadEnumProperty<ExtendedPropertyType>();
			}
			else
			{
				extendedPropertyType = ExtendedPropertyType.String;
			}
			reader.Reset();
			if (extendedPropertyType == ExtendedPropertyType.String)
			{
				return new StringExtendedProperty();
			}
			if (extendedPropertyType != ExtendedPropertyType.Json)
			{
				throw reader.CreateInvalidDataException(context, TomSR.Exception_UnrecognizedValueOfType("ExtendedPropertyType", extendedPropertyType.ToString()), null);
			}
			return new JsonExtendedProperty();
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00023353 File Offset: 0x00021553
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_ExtendedProperty_1Arg(this.Name);
		}

		// Token: 0x040000F6 RID: 246
		internal ExtendedProperty.ObjectBody body;

		// Token: 0x040000F7 RID: 247
		internal static Func<ExtendedProperty, ExtendedProperty, bool> CompareExtendedPropertyType = (ExtendedProperty property1, ExtendedProperty property2) => property1.Type == property2.Type;

		// Token: 0x02000261 RID: 609
		internal class ObjectBody : NamedMetadataObjectBody<ExtendedProperty>
		{
			// Token: 0x0600201D RID: 8221 RVA: 0x000D340E File Offset: 0x000D160E
			public ObjectBody(ExtendedProperty owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.ObjectID = new UntypedParentLink<ExtendedProperty>(owner, "Object");
			}

			// Token: 0x0600201E RID: 8222 RVA: 0x000D3433 File Offset: 0x000D1633
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x0600201F RID: 8223 RVA: 0x000D343C File Offset: 0x000D163C
			internal bool IsEqualTo(ExtendedProperty.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.Type, other.Type)) && PropertyHelper.AreValuesIdentical(this.Value, other.Value) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ObjectID.IsEqualTo(other.ObjectID, context));
			}

			// Token: 0x06002020 RID: 8224 RVA: 0x000D34F0 File Offset: 0x000D16F0
			internal void CopyFromImpl(ExtendedProperty.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.Type = other.Type;
				}
				this.Value = other.Value;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ObjectID.CopyFrom(other.ObjectID, context);
				}
			}

			// Token: 0x06002021 RID: 8225 RVA: 0x000D3590 File Offset: 0x000D1790
			internal void CopyFromImpl(ExtendedProperty.ObjectBody other)
			{
				this.Name = other.Name;
				this.Type = other.Type;
				this.Value = other.Value;
				this.ModifiedTime = other.ModifiedTime;
				this.ObjectID.CopyFrom(other.ObjectID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06002022 RID: 8226 RVA: 0x000D35E3 File Offset: 0x000D17E3
			public override void CopyFrom(MetadataObjectBody<ExtendedProperty> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((ExtendedProperty.ObjectBody)other, context);
			}

			// Token: 0x06002023 RID: 8227 RVA: 0x000D35FC File Offset: 0x000D17FC
			internal bool IsEqualTo(ExtendedProperty.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Value, other.Value) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.ObjectID.IsEqualTo(other.ObjectID);
			}

			// Token: 0x06002024 RID: 8228 RVA: 0x000D365E File Offset: 0x000D185E
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((ExtendedProperty.ObjectBody)other);
			}

			// Token: 0x06002025 RID: 8229 RVA: 0x000D3678 File Offset: 0x000D1878
			internal void CompareWith(ExtendedProperty.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Value, other.Value))
				{
					if (this.Type == ExtendedPropertyType.Json)
					{
						context.RegisterPropertyChange(base.Owner, "Value", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.Json, other.Value, this.Value);
					}
					else
					{
						context.RegisterPropertyChange(base.Owner, "Value", typeof(string), PropertyFlags.DdlAndUser, other.Value, this.Value);
					}
				}
				this.ObjectID.CompareWith(other.ObjectID, "ObjectID", "Object", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06002026 RID: 8230 RVA: 0x000D379C File Offset: 0x000D199C
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((ExtendedProperty.ObjectBody)other, context);
			}

			// Token: 0x04000825 RID: 2085
			internal string Name;

			// Token: 0x04000826 RID: 2086
			internal ExtendedPropertyType Type;

			// Token: 0x04000827 RID: 2087
			internal string Value;

			// Token: 0x04000828 RID: 2088
			internal DateTime ModifiedTime;

			// Token: 0x04000829 RID: 2089
			internal UntypedParentLink<ExtendedProperty> ObjectID;
		}
	}
}
