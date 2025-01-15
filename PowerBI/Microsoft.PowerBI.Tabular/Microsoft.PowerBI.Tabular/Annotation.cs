using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200002C RID: 44
	public sealed class Annotation : NamedMetadataObject
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003B9A File Offset: 0x00001D9A
		public Annotation()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003BA8 File Offset: 0x00001DA8
		internal Annotation(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003BB6 File Offset: 0x00001DB6
		private void InitBodyAndCollections()
		{
			this.body = new Annotation.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Value = string.Empty;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Annotation;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003BE8 File Offset: 0x00001DE8
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003BFA File Offset: 0x00001DFA
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
					MetadataObject.UpdateMetadataObjectParent<Annotation, MetadataObject>(this.body.ObjectID, value, null, null);
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003C22 File Offset: 0x00001E22
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ObjectID.ObjectID;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003C34 File Offset: 0x00001E34
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Annotation, null, "Annotation object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteProperty("value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003CF8 File Offset: 0x00001EF8
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003D00 File Offset: 0x00001F00
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Annotation.ObjectBody)value;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003D0E File Offset: 0x00001F0E
		internal override ITxObjectBody CreateBody()
		{
			return new Annotation.ObjectBody(this);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003D16 File Offset: 0x00001F16
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new Annotation();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003D20 File Offset: 0x00001F20
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			switch (parent.ObjectType)
			{
			case ObjectType.Model:
				return ((Model)parent).Annotations;
			case ObjectType.DataSource:
				return ((DataSource)parent).Annotations;
			case ObjectType.Table:
				return ((Table)parent).Annotations;
			case ObjectType.Column:
				return ((Column)parent).Annotations;
			case ObjectType.AttributeHierarchy:
				return ((AttributeHierarchy)parent).Annotations;
			case ObjectType.Partition:
				return ((Partition)parent).Annotations;
			case ObjectType.Relationship:
				return ((Relationship)parent).Annotations;
			case ObjectType.Measure:
				return ((Measure)parent).Annotations;
			case ObjectType.Hierarchy:
				return ((Hierarchy)parent).Annotations;
			case ObjectType.Level:
				return ((Level)parent).Annotations;
			case ObjectType.KPI:
				return ((KPI)parent).Annotations;
			case ObjectType.Culture:
				return ((Culture)parent).Annotations;
			case ObjectType.LinguisticMetadata:
				return ((LinguisticMetadata)parent).Annotations;
			case ObjectType.Perspective:
				return ((Perspective)parent).Annotations;
			case ObjectType.PerspectiveTable:
				return ((PerspectiveTable)parent).Annotations;
			case ObjectType.PerspectiveColumn:
				return ((PerspectiveColumn)parent).Annotations;
			case ObjectType.PerspectiveHierarchy:
				return ((PerspectiveHierarchy)parent).Annotations;
			case ObjectType.PerspectiveMeasure:
				return ((PerspectiveMeasure)parent).Annotations;
			case ObjectType.Role:
				return ((ModelRole)parent).Annotations;
			case ObjectType.RoleMembership:
				return ((ModelRoleMember)parent).Annotations;
			case ObjectType.TablePermission:
				return ((TablePermission)parent).Annotations;
			case ObjectType.Variation:
				return ((Variation)parent).Annotations;
			case ObjectType.Set:
				return ((Set)parent).Annotations;
			case ObjectType.PerspectiveSet:
				return ((PerspectiveSet)parent).Annotations;
			case ObjectType.Expression:
				return ((NamedExpression)parent).Annotations;
			case ObjectType.ColumnPermission:
				return ((ColumnPermission)parent).Annotations;
			case ObjectType.CalculationGroup:
				return ((CalculationGroup)parent).Annotations;
			case ObjectType.AlternateOf:
				return ((AlternateOf)parent).Annotations;
			case ObjectType.RefreshPolicy:
				return ((RefreshPolicy)parent).Annotations;
			case ObjectType.QueryGroup:
				return ((QueryGroup)parent).Annotations;
			case ObjectType.DataCoverageDefinition:
				return ((DataCoverageDefinition)parent).Annotations;
			case ObjectType.Function:
				return ((Function)parent).Annotations;
			case ObjectType.BindingInfo:
				return ((BindingInfo)parent).Annotations;
			}
			throw TomInternalException.Create("Got a parent object of unexpected type: {0}", new object[] { parent.GetType().Name });
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003FEC File Offset: 0x000021EC
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			MetadataObject metadataObject = MetadataObject.ResolveMetadataObjectParentById<Annotation, MetadataObject>(this.body.ObjectID, objectMap, throwIfCantResolve, null, null);
			if (metadataObject != null)
			{
				switch (metadataObject.ObjectType)
				{
				case ObjectType.Model:
					((Model)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.DataSource:
					((DataSource)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Table:
					((Table)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Column:
					((Column)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.AttributeHierarchy:
					((AttributeHierarchy)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Partition:
					((Partition)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Relationship:
					((Relationship)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Measure:
					((Measure)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Hierarchy:
					((Hierarchy)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Level:
					((Level)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.KPI:
					((KPI)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Culture:
					((Culture)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.LinguisticMetadata:
					((LinguisticMetadata)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Perspective:
					((Perspective)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.PerspectiveTable:
					((PerspectiveTable)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.PerspectiveColumn:
					((PerspectiveColumn)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.PerspectiveHierarchy:
					((PerspectiveHierarchy)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.PerspectiveMeasure:
					((PerspectiveMeasure)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Role:
					((ModelRole)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.RoleMembership:
					((ModelRoleMember)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.TablePermission:
					((TablePermission)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Variation:
					((Variation)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Set:
					((Set)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.PerspectiveSet:
					((PerspectiveSet)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Expression:
					((NamedExpression)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.ColumnPermission:
					((ColumnPermission)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.CalculationGroup:
					((CalculationGroup)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.AlternateOf:
					((AlternateOf)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.RefreshPolicy:
					((RefreshPolicy)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.QueryGroup:
					((QueryGroup)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.DataCoverageDefinition:
					((DataCoverageDefinition)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.Function:
					((Function)metadataObject).Annotations.Add(this);
					return;
				case ObjectType.BindingInfo:
					((BindingInfo)metadataObject).Annotations.Add(this);
					return;
				}
				throw TomInternalException.Create("Got a parent object of unexpected type: {0}", new object[] { metadataObject.GetType().Name });
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004399 File Offset: 0x00002599
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000439B File Offset: 0x0000259B
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000043A8 File Offset: 0x000025A8
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Annotation, out text))
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000442B File Offset: 0x0000262B
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00004438 File Offset: 0x00002638
		public string Value
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000044A8 File Offset: 0x000026A8
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000044B8 File Offset: 0x000026B8
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000453C File Offset: 0x0000273C
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00004550 File Offset: 0x00002750
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000045D4 File Offset: 0x000027D4
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000045E6 File Offset: 0x000027E6
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

		// Token: 0x06000070 RID: 112 RVA: 0x000045FC File Offset: 0x000027FC
		internal void CopyFrom(Annotation other, CopyContext context)
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

		// Token: 0x06000071 RID: 113 RVA: 0x0000468D File Offset: 0x0000288D
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Annotation)other, context);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000469C File Offset: 0x0000289C
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Annotation other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000046B8 File Offset: 0x000028B8
		public void CopyTo(Annotation other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000046D4 File Offset: 0x000028D4
		public Annotation Clone()
		{
			return base.CloneInternal<Annotation>();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000046DC File Offset: 0x000028DC
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
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
			if (!string.IsNullOrEmpty(this.body.Value))
			{
				writer.WriteProperty<string>(options, "Value", this.body.Value);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004798 File Offset: 0x00002998
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

		// Token: 0x06000077 RID: 119 RVA: 0x00004820 File Offset: 0x00002A20
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
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
			if (!string.IsNullOrEmpty(this.body.Value) && writer.ShouldIncludeProperty("Value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("Value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Value);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004918 File Offset: 0x00002B18
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Value) && writer.ShouldIncludeProperty("value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Value);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000049F0 File Offset: 0x00002BF0
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
					if (c != 'V')
					{
						if (c != 'v')
						{
							break;
						}
						if (!(propertyName == "value"))
						{
							break;
						}
					}
					else if (!(propertyName == "Value"))
					{
						break;
					}
					this.body.Value = reader.ReadStringProperty();
					return true;
				}
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
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004B50 File Offset: 0x00002D50
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004B59 File Offset: 0x00002D59
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004B7C File Offset: 0x00002D7C
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Value))
			{
				result["value", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Value, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 4, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004C4C File Offset: 0x00002E4C
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "name")
			{
				this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "value")
			{
				this.body.Value = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (!(name == "modifiedTime"))
			{
				return false;
			}
			this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
			return true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004CD2 File Offset: 0x00002ED2
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_Annotation_1Arg(this.Name);
		}

		// Token: 0x040000C9 RID: 201
		internal Annotation.ObjectBody body;

		// Token: 0x02000225 RID: 549
		internal class ObjectBody : NamedMetadataObjectBody<Annotation>
		{
			// Token: 0x06001EA0 RID: 7840 RVA: 0x000CC588 File Offset: 0x000CA788
			public ObjectBody(Annotation owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.ObjectID = new UntypedParentLink<Annotation>(owner, "Object");
			}

			// Token: 0x06001EA1 RID: 7841 RVA: 0x000CC5AD File Offset: 0x000CA7AD
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x06001EA2 RID: 7842 RVA: 0x000CC5B8 File Offset: 0x000CA7B8
			internal bool IsEqualTo(Annotation.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Value, other.Value) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ObjectID.IsEqualTo(other.ObjectID, context));
			}

			// Token: 0x06001EA3 RID: 7843 RVA: 0x000CC644 File Offset: 0x000CA844
			internal void CopyFromImpl(Annotation.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
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

			// Token: 0x06001EA4 RID: 7844 RVA: 0x000CC6C5 File Offset: 0x000CA8C5
			internal void CopyFromImpl(Annotation.ObjectBody other)
			{
				this.Name = other.Name;
				this.Value = other.Value;
				this.ModifiedTime = other.ModifiedTime;
				this.ObjectID.CopyFrom(other.ObjectID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001EA5 RID: 7845 RVA: 0x000CC701 File Offset: 0x000CA901
			public override void CopyFrom(MetadataObjectBody<Annotation> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Annotation.ObjectBody)other, context);
			}

			// Token: 0x06001EA6 RID: 7846 RVA: 0x000CC718 File Offset: 0x000CA918
			internal bool IsEqualTo(Annotation.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Value, other.Value) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.ObjectID.IsEqualTo(other.ObjectID);
			}

			// Token: 0x06001EA7 RID: 7847 RVA: 0x000CC77A File Offset: 0x000CA97A
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Annotation.ObjectBody)other);
			}

			// Token: 0x06001EA8 RID: 7848 RVA: 0x000CC794 File Offset: 0x000CA994
			internal void CompareWith(Annotation.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Value, other.Value))
				{
					context.RegisterPropertyChange(base.Owner, "Value", typeof(string), PropertyFlags.DdlAndUser, other.Value, this.Value);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.ObjectID.CompareWith(other.ObjectID, "ObjectID", "Object", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06001EA9 RID: 7849 RVA: 0x000CC884 File Offset: 0x000CAA84
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Annotation.ObjectBody)other, context);
			}

			// Token: 0x04000717 RID: 1815
			internal string Name;

			// Token: 0x04000718 RID: 1816
			internal string Value;

			// Token: 0x04000719 RID: 1817
			internal DateTime ModifiedTime;

			// Token: 0x0400071A RID: 1818
			internal UntypedParentLink<Annotation> ObjectID;
		}
	}
}
