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
	// Token: 0x0200002D RID: 45
	public sealed class AttributeHierarchy : MetadataObject
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00004CDF File Offset: 0x00002EDF
		public AttributeHierarchy()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004CF2 File Offset: 0x00002EF2
		internal AttributeHierarchy(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004D01 File Offset: 0x00002F01
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new AttributeHierarchy.ObjectBody(this);
			this.body.State = ObjectState.CalculationNeeded;
			this._Annotations = new AttributeHierarchyAnnotationCollection(this, comparer);
			this._ExtendedProperties = new AttributeHierarchyExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00004D35 File Offset: 0x00002F35
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.AttributeHierarchy;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004D38 File Offset: 0x00002F38
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00004D4A File Offset: 0x00002F4A
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
					MetadataObject.UpdateMetadataObjectParent<AttributeHierarchy, Column>(this.body.ColumnID, (Column)value, "AttributeHierarchy", null);
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004D7B File Offset: 0x00002F7B
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ColumnID.ObjectID;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004D90 File Offset: 0x00002F90
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.AttributeHierarchy, null, "AttributeHierarchy object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, PropertyHelper.GetObjectStateCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
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

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004EAC File Offset: 0x000030AC
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00004EB4 File Offset: 0x000030B4
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (AttributeHierarchy.ObjectBody)value;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004EC2 File Offset: 0x000030C2
		internal override ITxObjectBody CreateBody()
		{
			return new AttributeHierarchy.ObjectBody(this);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004ECA File Offset: 0x000030CA
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new AttributeHierarchy();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004ED1 File Offset: 0x000030D1
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return null;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004ED4 File Offset: 0x000030D4
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Column column = MetadataObject.ResolveMetadataObjectParentById<AttributeHierarchy, Column>(this.body.ColumnID, objectMap, throwIfCantResolve, "AttributeHierarchy", null);
			if (column != null && column.Model != null)
			{
				foreach (MetadataObject metadataObject in base.GetChildren(false))
				{
					column.Model.NotifySubtreeAdded(metadataObject);
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004F4C File Offset: 0x0000314C
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004F4E File Offset: 0x0000314E
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004F5E File Offset: 0x0000315E
		public AttributeHierarchyAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004F66 File Offset: 0x00003166
		[CompatibilityRequirement("1400")]
		public AttributeHierarchyExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004F6E File Offset: 0x0000316E
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00004F7C File Offset: 0x0000317C
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
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != ObjectState.CalculationNeeded))
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000509E File Offset: 0x0000329E
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000050AC File Offset: 0x000032AC
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00005130 File Offset: 0x00003330
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00005140 File Offset: 0x00003340
		public DateTime RefreshedTime
		{
			get
			{
				return this.body.RefreshedTime;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RefreshedTime, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "RefreshedTime", typeof(DateTime), this.body.RefreshedTime, value);
					DateTime refreshedTime = this.body.RefreshedTime;
					this.body.RefreshedTime = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RefreshedTime", typeof(DateTime), refreshedTime, value);
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000051C4 File Offset: 0x000033C4
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000051D8 File Offset: 0x000033D8
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000525C File Offset: 0x0000345C
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000526E File Offset: 0x0000346E
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

		// Token: 0x0600009B RID: 155 RVA: 0x00005284 File Offset: 0x00003484
		internal void CopyFrom(AttributeHierarchy other, CopyContext context)
		{
			base.CopyFrom(other, context);
			bool flag;
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions)
			{
				flag = true;
			}
			else if ((context.Flags & CopyFlags.MetadataSync) == CopyFlags.MetadataSync)
			{
				flag = this.body.ModifiedTime.CompareTo(other.body.ModifiedTime) != 0 || this.body.RefreshedTime.CompareTo(other.body.RefreshedTime) != 0;
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
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005366 File Offset: 0x00003566
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((AttributeHierarchy)other, context);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005375 File Offset: 0x00003575
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(AttributeHierarchy other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005391 File Offset: 0x00003591
		public void CopyTo(AttributeHierarchy other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000053AD File Offset: 0x000035AD
		public AttributeHierarchy Clone()
		{
			return base.CloneInternal<AttributeHierarchy>();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000053B8 File Offset: 0x000035B8
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ColumnID", out objectId))
			{
				this.body.ColumnID.ObjectID = objectId;
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
			if (reader.TryReadProperty<DateTime>("RefreshedTime", out dateTime2))
			{
				this.body.RefreshedTime = dateTime2;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005440 File Offset: 0x00003640
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (this.body.State != ObjectState.CalculationNeeded)
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
			if (this.body.RefreshedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.RefreshedTime);
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

		// Token: 0x060000A2 RID: 162 RVA: 0x00005608 File Offset: 0x00003808
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
				case 5:
				{
					char c = propertyName[0];
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
				case 6:
				case 7:
				case 9:
				case 10:
					break;
				case 8:
					if (propertyName == "ColumnID")
					{
						this.body.ColumnID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 11:
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
					break;
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
				{
					char c = propertyName[0];
					if (c != 'R')
					{
						if (c != 'r')
						{
							break;
						}
						if (!(propertyName == "refreshedTime"))
						{
							break;
						}
					}
					else if (!(propertyName == "RefreshedTime"))
					{
						break;
					}
					this.body.RefreshedTime = reader.ReadDateTimeProperty();
					return true;
				}
				default:
					if (length == 18)
					{
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
									catch (Exception ex2)
									{
										throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex2.Message), ex2);
									}
								}
							}
							return true;
						}
					}
					break;
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000595C File Offset: 0x00003B5C
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && this.body.State != ObjectState.CalculationNeeded)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["state", TomPropCategory.Regular, 2, true] = JsonPropertyHelper.ConvertEnumToJsonValue<ObjectState>(this.body.State);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 4, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.RefreshedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["refreshedTime", TomPropCategory.Regular, 5, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.RefreshedTime);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && !options.IncludeTranslatablePropertiesOnly)
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
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array3;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005C1C File Offset: 0x00003E1C
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (!(name == "state"))
			{
				if (name == "modifiedTime")
				{
					this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
					return true;
				}
				if (name == "refreshedTime")
				{
					this.body.RefreshedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
					return true;
				}
				if (!(name == "extendedProperties"))
				{
					if (!(name == "annotations"))
					{
						return false;
					}
					JsonPropertyHelper.ReadObjectCollection(this.Annotations, jsonProp.Value, options, mode, dbCompatibilityLevel);
					return true;
				}
				else
				{
					if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
					{
						return false;
					}
					JsonPropertyHelper.ReadObjectCollection(this.ExtendedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
					return true;
				}
			}
			else
			{
				ObjectState objectState = JsonPropertyHelper.ConvertJsonValueToEnum<ObjectState>(jsonProp.Value);
				if (jsonProp.Value.Type != 10 && !PropertyHelper.IsObjectStateValueCompatible(objectState, mode, dbCompatibilityLevel))
				{
					return false;
				}
				this.body.State = objectState;
				return true;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005D2C File Offset: 0x00003F2C
		internal override string GetFormattedObjectPath()
		{
			if (this.Column != null && this.Column.Table != null)
			{
				return TomSR.ObjectPath_AttributeHierarchy_2Args(this.Column.Name, this.Column.Table.Name);
			}
			if (this.Column != null)
			{
				return TomSR.ObjectPath_Column_1Arg(this.Column.Name);
			}
			return TomSR.ObjectPath_AttributeHierarchy_0Args;
		}

		// Token: 0x040000CA RID: 202
		internal AttributeHierarchy.ObjectBody body;

		// Token: 0x040000CB RID: 203
		private AttributeHierarchyAnnotationCollection _Annotations;

		// Token: 0x040000CC RID: 204
		private AttributeHierarchyExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x02000226 RID: 550
		internal class ObjectBody : MetadataObjectBody<AttributeHierarchy>
		{
			// Token: 0x06001EAA RID: 7850 RVA: 0x000CC89B File Offset: 0x000CAA9B
			public ObjectBody(AttributeHierarchy owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.RefreshedTime = DateTime.MinValue;
				this.ColumnID = new ParentLink<AttributeHierarchy, Column>(owner, "Column");
			}

			// Token: 0x06001EAB RID: 7851 RVA: 0x000CC8CC File Offset: 0x000CAACC
			internal bool IsEqualTo(AttributeHierarchy.ObjectBody other, CopyContext context)
			{
				return ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ColumnID.IsEqualTo(other.ColumnID, context));
			}

			// Token: 0x06001EAC RID: 7852 RVA: 0x000CC97C File Offset: 0x000CAB7C
			internal void CopyFromImpl(AttributeHierarchy.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
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
					this.RefreshedTime = other.RefreshedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ColumnID.CopyFrom(other.ColumnID, context);
				}
			}

			// Token: 0x06001EAD RID: 7853 RVA: 0x000CCA1E File Offset: 0x000CAC1E
			internal void CopyFromImpl(AttributeHierarchy.ObjectBody other)
			{
				this.State = other.State;
				this.ModifiedTime = other.ModifiedTime;
				this.RefreshedTime = other.RefreshedTime;
				this.ColumnID.CopyFrom(other.ColumnID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001EAE RID: 7854 RVA: 0x000CCA5A File Offset: 0x000CAC5A
			public override void CopyFrom(MetadataObjectBody<AttributeHierarchy> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((AttributeHierarchy.ObjectBody)other, context);
			}

			// Token: 0x06001EAF RID: 7855 RVA: 0x000CCA74 File Offset: 0x000CAC74
			internal bool IsEqualTo(AttributeHierarchy.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime) && this.ColumnID.IsEqualTo(other.ColumnID);
			}

			// Token: 0x06001EB0 RID: 7856 RVA: 0x000CCAD6 File Offset: 0x000CACD6
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((AttributeHierarchy.ObjectBody)other);
			}

			// Token: 0x06001EB1 RID: 7857 RVA: 0x000CCAF0 File Offset: 0x000CACF0
			internal void CompareWith(AttributeHierarchy.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.State, other.State))
				{
					context.RegisterPropertyChange(base.Owner, "State", typeof(ObjectState), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.State, this.State);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime))
				{
					context.RegisterPropertyChange(base.Owner, "RefreshedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.RefreshedTime, this.RefreshedTime);
				}
				this.ColumnID.CompareWith(other.ColumnID, "ColumnID", "Column", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06001EB2 RID: 7858 RVA: 0x000CCBE9 File Offset: 0x000CADE9
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((AttributeHierarchy.ObjectBody)other, context);
			}

			// Token: 0x0400071B RID: 1819
			internal ObjectState State;

			// Token: 0x0400071C RID: 1820
			internal DateTime ModifiedTime;

			// Token: 0x0400071D RID: 1821
			internal DateTime RefreshedTime;

			// Token: 0x0400071E RID: 1822
			internal ParentLink<AttributeHierarchy, Column> ColumnID;
		}
	}
}
