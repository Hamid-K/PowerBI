using System;
using System.Collections;
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
	// Token: 0x020000C8 RID: 200
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class TimeUnitColumnAssociation : MetadataObject, IKeyedMetadataObject
	{
		// Token: 0x06000C7F RID: 3199 RVA: 0x000692C1 File Offset: 0x000674C1
		public TimeUnitColumnAssociation()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x000692D4 File Offset: 0x000674D4
		internal TimeUnitColumnAssociation(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x000692E3 File Offset: 0x000674E3
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new TimeUnitColumnAssociation.ObjectBody(this);
			this.body.TimeUnit = TimeUnit.Unknown;
			this._CalendarColumnReferences = new CalendarColumnReferenceCollection(this, comparer);
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0006930A File Offset: 0x0006750A
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.TimeUnitColumnAssociation;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0006930E File Offset: 0x0006750E
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x00069320 File Offset: 0x00067520
		public override MetadataObject Parent
		{
			get
			{
				return this.body.CalendarID.Object;
			}
			internal set
			{
				if (this.body.CalendarID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<TimeUnitColumnAssociation, Calendar>(this.body.CalendarID, (Calendar)value, null, null);
				}
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0006934D File Offset: 0x0006754D
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.CalendarID.ObjectID;
			}
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00069360 File Offset: 0x00067560
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.TimeUnitColumnAssociation, null, "TimeUnitColumnAssociation object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("timeUnit", MetadataPropertyNature.NameProperty))
				{
					writer.WriteEnumProperty<TimeUnit>("timeUnit", MetadataPropertyNature.NameProperty, null);
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				TimeUnitColumnAssociation.WriteColumnReferencesMetadataSchema(context, writer);
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x000693EC File Offset: 0x000675EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.TimeUnitColumnAssociation[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.TimeUnit != TimeUnit.Unknown)
			{
				int num = PropertyHelper.GetTimeUnitCompatibilityRestrictions(this.body.TimeUnit)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "TimeUnit");
					requiredLevel = num;
					int num2 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0006947F File Offset: 0x0006767F
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x00069487 File Offset: 0x00067687
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (TimeUnitColumnAssociation.ObjectBody)value;
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00069495 File Offset: 0x00067695
		internal override ITxObjectBody CreateBody()
		{
			return new TimeUnitColumnAssociation.ObjectBody(this);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0006949D File Offset: 0x0006769D
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new TimeUnitColumnAssociation();
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x000694A4 File Offset: 0x000676A4
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Calendar)parent).TimeUnitColumnAssociations;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x000694B4 File Offset: 0x000676B4
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Calendar calendar = MetadataObject.ResolveMetadataObjectParentById<TimeUnitColumnAssociation, Calendar>(this.body.CalendarID, objectMap, throwIfCantResolve, null, null);
			if (calendar != null)
			{
				calendar.TimeUnitColumnAssociations.Add(this);
			}
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000694E5 File Offset: 0x000676E5
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x000694E7 File Offset: 0x000676E7
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			if (!isLogicalStructure)
			{
				yield return this._CalendarColumnReferences;
			}
			yield break;
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x000694FE File Offset: 0x000676FE
		internal CalendarColumnReferenceCollection CalendarColumnReferences
		{
			get
			{
				return this._CalendarColumnReferences;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x00069506 File Offset: 0x00067706
		// (set) Token: 0x06000C92 RID: 3218 RVA: 0x00069514 File Offset: 0x00067714
		public TimeUnit TimeUnit
		{
			get
			{
				return this.body.TimeUnit;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TimeUnit, value))
				{
					CompatibilityRestrictionSet timeUnitCompatibilityRestrictions = PropertyHelper.GetTimeUnitCompatibilityRestrictions(value);
					CompatibilityRestrictionSet timeUnitCompatibilityRestrictions2 = PropertyHelper.GetTimeUnitCompatibilityRestrictions(this.body.TimeUnit);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = timeUnitCompatibilityRestrictions.Compare(timeUnitCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != TimeUnit.Unknown))
					{
						array = base.ValidateCompatibilityRequirement(timeUnitCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "TimeUnit", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "TimeUnit", typeof(TimeUnit), this.body.TimeUnit, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(timeUnitCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(timeUnitCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(timeUnitCompatibilityRestrictions, array);
						break;
					}
					TimeUnit timeUnit = this.body.TimeUnit;
					this.body.TimeUnit = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "TimeUnit", typeof(TimeUnit), timeUnit, value);
				}
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00069635 File Offset: 0x00067835
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x00069644 File Offset: 0x00067844
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

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x000696C8 File Offset: 0x000678C8
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x000696DC File Offset: 0x000678DC
		public Calendar Calendar
		{
			get
			{
				return this.body.CalendarID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.CalendarID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Calendar", typeof(Calendar), this.body.CalendarID.Object, value);
					Calendar @object = this.body.CalendarID.Object;
					this.body.CalendarID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Calendar", typeof(Calendar), @object, value);
				}
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00069760 File Offset: 0x00067960
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x00069772 File Offset: 0x00067972
		internal ObjectId _CalendarID
		{
			get
			{
				return this.body.CalendarID.ObjectID;
			}
			set
			{
				this.body.CalendarID.ObjectID = value;
			}
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00069788 File Offset: 0x00067988
		internal void CopyFrom(TimeUnitColumnAssociation other, CopyContext context)
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
				this.CalendarColumnReferences.CopyFrom(other.CalendarColumnReferences, context);
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00069838 File Offset: 0x00067A38
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((TimeUnitColumnAssociation)other, context);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00069847 File Offset: 0x00067A47
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(TimeUnitColumnAssociation other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00069863 File Offset: 0x00067A63
		public void CopyTo(TimeUnitColumnAssociation other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0006987F File Offset: 0x00067A7F
		public TimeUnitColumnAssociation Clone()
		{
			return base.CloneInternal<TimeUnitColumnAssociation>();
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00069888 File Offset: 0x00067A88
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.TimeUnitColumnAssociation.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object TimeUnitColumnAssociation is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.CalendarID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "CalendarID", this.body.CalendarID.Object);
			}
			if (this.body.TimeUnit != TimeUnit.Unknown)
			{
				if (!PropertyHelper.IsTimeUnitValueCompatible(this.body.TimeUnit, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member TimeUnit is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<TimeUnit>(options, "TimeUnit", this.body.TimeUnit);
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00069960 File Offset: 0x00067B60
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("CalendarID", out objectId))
			{
				this.body.CalendarID.ObjectID = objectId;
			}
			TimeUnit timeUnit;
			if (reader.TryReadProperty<TimeUnit>("TimeUnit", out timeUnit))
			{
				this.body.TimeUnit = timeUnit;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000699CC File Offset: 0x00067BCC
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.TimeUnitColumnAssociation.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object TimeUnitColumnAssociation is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.CalendarID.Object != null && writer.ShouldIncludeProperty("CalendarID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("CalendarID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.CalendarID.Object);
			}
			if (this.body.TimeUnit != TimeUnit.Unknown)
			{
				if (!PropertyHelper.IsTimeUnitValueCompatible(this.body.TimeUnit, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member TimeUnit is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("TimeUnit", MetadataPropertyNature.NameProperty))
				{
					writer.WriteEnumProperty<TimeUnit>("TimeUnit", MetadataPropertyNature.NameProperty, this.body.TimeUnit);
				}
			}
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00069AE8 File Offset: 0x00067CE8
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.TimeUnitColumnAssociation.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object TimeUnitColumnAssociation is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (this.body.TimeUnit != TimeUnit.Unknown)
			{
				if (!PropertyHelper.IsTimeUnitValueCompatible(this.body.TimeUnit, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member TimeUnit is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("timeUnit", MetadataPropertyNature.NameProperty))
				{
					writer.WriteEnumProperty<TimeUnit>("timeUnit", MetadataPropertyNature.NameProperty, this.body.TimeUnit);
				}
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			this.WriteColumnReferencesToMetadataStream(context, writer);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00069C0C File Offset: 0x00067E0C
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName == "CalendarID")
			{
				this.body.CalendarID.ObjectID = reader.ReadObjectIdProperty();
				return true;
			}
			if (!(propertyName == "TimeUnit") && !(propertyName == "timeUnit"))
			{
				if (propertyName == "ModifiedTime" || propertyName == "modifiedTime")
				{
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
				}
				if (context.SerializationMode != MetadataSerializationMode.Xmla && this.TryReadColumnReferencesFromMetadataStream(context, reader))
				{
					return true;
				}
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			else
			{
				if (!CompatibilityRestrictions.TimeUnit.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					classification = UnexpectedPropertyClassification.IncompatibleProperty;
					return false;
				}
				this.body.TimeUnit = reader.ReadEnumProperty<TimeUnit>();
				return true;
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00069CE4 File Offset: 0x00067EE4
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.TimeUnitColumnAssociation.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object TimeUnitColumnAssociation is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.TimeUnit != TimeUnit.Unknown)
			{
				if (!PropertyHelper.IsTimeUnitValueCompatible(this.body.TimeUnit, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member TimeUnit is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["timeUnit", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertEnumToJsonValue<TimeUnit>(this.body.TimeUnit);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 3, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			this.SerializeAdditionalDataToJsonObject(result, options, mode, dbCompatibilityLevel);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00069DE8 File Offset: 0x00067FE8
		private void SerializeAdditionalDataToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (this.PrimaryColumn != null)
				{
					jsonObj["primaryColumn", TomPropCategory.Regular, 10, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.primaryColumn.Name, SplitMultilineOptions.None);
				}
				if (this.AssociatedColumns.Count > 0)
				{
					List<string> list = new List<string>(this.associatedColumns.Select((Column c) => c.Name));
					jsonObj["associatedColumns", TomPropCategory.Regular, 11, false] = list.ToArray();
				}
			}
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00069E84 File Offset: 0x00068084
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (!(name == "timeUnit"))
			{
				if (!(name == "modifiedTime"))
				{
					bool flag = false;
					this.ReadAdditionalPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel, ref flag);
					return flag;
				}
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			else
			{
				TimeUnit timeUnit = JsonPropertyHelper.ConvertJsonValueToEnum<TimeUnit>(jsonProp.Value);
				if (jsonProp.Value.Type != 10 && !PropertyHelper.IsTimeUnitValueCompatible(timeUnit, mode, dbCompatibilityLevel))
				{
					return false;
				}
				this.body.TimeUnit = timeUnit;
				return true;
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00069F14 File Offset: 0x00068114
		private void ReadAdditionalPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel, ref bool wasRead)
		{
			string name = jsonProp.Name;
			if (name == "primaryColumn")
			{
				this._CalendarColumnReferences.Add(new CalendarColumnReference
				{
					Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value),
					IsPrimaryColumn = true
				});
				wasRead = true;
				return;
			}
			if (!(name == "associatedColumns"))
			{
				wasRead = false;
				return;
			}
			foreach (string text in TimeUnitColumnAssociation.ReadAssociatedColumns(jsonProp.Value))
			{
				this._CalendarColumnReferences.Add(new CalendarColumnReference
				{
					Name = text,
					IsPrimaryColumn = false
				});
			}
			wasRead = true;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00069FD8 File Offset: 0x000681D8
		object IKeyedMetadataObject.Key
		{
			get
			{
				return this.body.TimeUnit;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00069FEA File Offset: 0x000681EA
		string IKeyedMetadataObject.LogicalPathElement
		{
			get
			{
				return this.body.TimeUnit.ToString("G");
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0006A008 File Offset: 0x00068208
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x0006A064 File Offset: 0x00068264
		public Column PrimaryColumn
		{
			get
			{
				if (this.primaryColumn == null)
				{
					CalendarColumnReference calendarColumnReference = this._CalendarColumnReferences.Where((CalendarColumnReference ccr) => ccr.IsPrimaryColumn).FirstOrDefault<CalendarColumnReference>();
					if (calendarColumnReference != null)
					{
						this.primaryColumn = calendarColumnReference.Column;
					}
				}
				return this.primaryColumn;
			}
			set
			{
				if (this.primaryColumn != value || this.primaryColumn == null)
				{
					List<CalendarColumnReference> list = new List<CalendarColumnReference>(this._CalendarColumnReferences.Where((CalendarColumnReference ccr) => ccr.IsPrimaryColumn));
					for (int i = 0; i < list.Count; i++)
					{
						this._CalendarColumnReferences.Remove(list[i]);
					}
					if (value != null)
					{
						CalendarColumnReference calendarColumnReference = new CalendarColumnReference
						{
							Column = value,
							IsPrimaryColumn = true
						};
						this._CalendarColumnReferences.Add(calendarColumnReference);
					}
					this.primaryColumn = value;
				}
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0006A100 File Offset: 0x00068300
		public ICollection<Column> AssociatedColumns
		{
			get
			{
				if (this.associatedColumns == null)
				{
					this.associatedColumns = new TimeUnitColumnAssociation.AssociatedColumnCollection(this);
				}
				return this.associatedColumns;
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0006A11C File Offset: 0x0006831C
		internal override void OnBeforeDeserialize(DeserializeOptions options)
		{
			base.OnBeforeDeserialize(options);
			this.primaryColumn = null;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0006A12C File Offset: 0x0006832C
		private static IEnumerable<string> ReadAssociatedColumns(JToken jsonValue)
		{
			if (jsonValue.Type == 8)
			{
				yield return JsonPropertyHelper.ConvertJsonStringToString(jsonValue);
			}
			else
			{
				if (jsonValue.Type != 2)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotConvertToType(jsonValue.ToString(), typeof(IEnumerable<string>).Name), jsonValue, null);
				}
				JArray jarray = jsonValue as JArray;
				Utils.Verify(jarray != null);
				List<JToken> allJsonLines = jarray.Children().ToList<JToken>();
				int num;
				for (int i = 0; i < allJsonLines.Count; i = num + 1)
				{
					yield return JsonPropertyHelper.ConvertJsonStringToString(allJsonLines[i]).Trim();
					num = i;
				}
				allJsonLines = null;
			}
			yield break;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0006A13C File Offset: 0x0006833C
		private protected override void OnDeserializeEnd(SerializationActivityContext context)
		{
			base.OnDeserializeEnd(context);
			foreach (CalendarColumnReference calendarColumnReference in this._CalendarColumnReferences)
			{
				context.RegistrerObjectForMasterReferenceCrossLinkReconstruction(calendarColumnReference);
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0006A190 File Offset: 0x00068390
		internal override string GetFormattedObjectPath()
		{
			if (this.Calendar == null)
			{
				return TomSR.ObjectPath_TimeUnitColumnAssociation_1Arg(this.TimeUnit.ToString());
			}
			if (this.Calendar.Table == null)
			{
				return TomSR.ObjectPath_TimeUnitColumnAssociation_2Args(this.TimeUnit.ToString(), this.Calendar.Name);
			}
			return TomSR.ObjectPath_TimeUnitColumnAssociation_3Args(this.TimeUnit.ToString(), this.Calendar.Name, this.Calendar.Table.Name);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0006A228 File Offset: 0x00068428
		private static void WriteColumnReferencesMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (writer.ShouldIncludeProperty("primaryColumn", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("primaryColumn", MetadataPropertyNature.RegularProperty, typeof(string));
			}
			if (writer.ShouldIncludeProperty("associatedColumns", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("associatedColumns", MetadataPropertyNature.RegularProperty, null);
				using (writer.CreateCollectionScope(null, MetadataPropertyNature.None))
				{
					writer.WriteProperty("associatedColumn", MetadataPropertyNature.RegularProperty, typeof(string));
				}
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0006A2B0 File Offset: 0x000684B0
		private void WriteColumnReferencesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (this.PrimaryColumn != null && writer.ShouldIncludeProperty("primaryColumn", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("primaryColumn", MetadataPropertyNature.RegularProperty, this.primaryColumn.Name);
			}
			if (this.AssociatedColumns.Count > 0 && writer.ShouldIncludeProperty("associatedColumns", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty<IEnumerable<string>>("associatedColumns", MetadataPropertyNature.RegularProperty, this.associatedColumns.Select((Column c) => c.Name));
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0006A33C File Offset: 0x0006853C
		private bool TryReadColumnReferencesFromMetadataStream(SerializationActivityContext context, IMetadataReader reader)
		{
			string propertyName = reader.PropertyName;
			if (propertyName == "primaryColumn")
			{
				this._CalendarColumnReferences.Add(new CalendarColumnReference
				{
					Name = reader.ReadStringProperty(),
					IsPrimaryColumn = true
				});
				return true;
			}
			if (!(propertyName == "associatedColumns"))
			{
				return false;
			}
			foreach (string text in reader.ReadProperty<IEnumerable<string>>())
			{
				this._CalendarColumnReferences.Add(new CalendarColumnReference
				{
					Name = text,
					IsPrimaryColumn = false
				});
			}
			return true;
		}

		// Token: 0x04000188 RID: 392
		internal TimeUnitColumnAssociation.ObjectBody body;

		// Token: 0x04000189 RID: 393
		private CalendarColumnReferenceCollection _CalendarColumnReferences;

		// Token: 0x0400018A RID: 394
		private Column primaryColumn;

		// Token: 0x0400018B RID: 395
		private TimeUnitColumnAssociation.AssociatedColumnCollection associatedColumns;

		// Token: 0x020002DD RID: 733
		internal class ObjectBody : MetadataObjectBody<TimeUnitColumnAssociation>
		{
			// Token: 0x06002369 RID: 9065 RVA: 0x000E1B33 File Offset: 0x000DFD33
			public ObjectBody(TimeUnitColumnAssociation owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.CalendarID = new ParentLink<TimeUnitColumnAssociation, Calendar>(owner, "Calendar");
			}

			// Token: 0x0600236A RID: 9066 RVA: 0x000E1B58 File Offset: 0x000DFD58
			internal bool IsEqualTo(TimeUnitColumnAssociation.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.TimeUnit, other.TimeUnit) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.CalendarID.IsEqualTo(other.CalendarID, context));
			}

			// Token: 0x0600236B RID: 9067 RVA: 0x000E1BCC File Offset: 0x000DFDCC
			internal void CopyFromImpl(TimeUnitColumnAssociation.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.TimeUnit = other.TimeUnit;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.CalendarID.CopyFrom(other.CalendarID, context);
				}
			}

			// Token: 0x0600236C RID: 9068 RVA: 0x000E1C3C File Offset: 0x000DFE3C
			internal void CopyFromImpl(TimeUnitColumnAssociation.ObjectBody other)
			{
				this.TimeUnit = other.TimeUnit;
				this.ModifiedTime = other.ModifiedTime;
				this.CalendarID.CopyFrom(other.CalendarID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x0600236D RID: 9069 RVA: 0x000E1C6C File Offset: 0x000DFE6C
			public override void CopyFrom(MetadataObjectBody<TimeUnitColumnAssociation> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((TimeUnitColumnAssociation.ObjectBody)other, context);
			}

			// Token: 0x0600236E RID: 9070 RVA: 0x000E1C84 File Offset: 0x000DFE84
			internal bool IsEqualTo(TimeUnitColumnAssociation.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.TimeUnit, other.TimeUnit) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.CalendarID.IsEqualTo(other.CalendarID);
			}

			// Token: 0x0600236F RID: 9071 RVA: 0x000E1CD1 File Offset: 0x000DFED1
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((TimeUnitColumnAssociation.ObjectBody)other);
			}

			// Token: 0x06002370 RID: 9072 RVA: 0x000E1CEC File Offset: 0x000DFEEC
			internal void CompareWith(TimeUnitColumnAssociation.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.TimeUnit, other.TimeUnit))
				{
					context.RegisterPropertyChange(base.Owner, "TimeUnit", typeof(TimeUnit), PropertyFlags.DdlAndUser, other.TimeUnit, this.TimeUnit);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.CalendarID.CompareWith(other.CalendarID, "CalendarID", "Calendar", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06002371 RID: 9073 RVA: 0x000E1DA0 File Offset: 0x000DFFA0
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((TimeUnitColumnAssociation.ObjectBody)other, context);
			}

			// Token: 0x04000A8E RID: 2702
			internal TimeUnit TimeUnit;

			// Token: 0x04000A8F RID: 2703
			internal DateTime ModifiedTime;

			// Token: 0x04000A90 RID: 2704
			internal ParentLink<TimeUnitColumnAssociation, Calendar> CalendarID;
		}

		// Token: 0x020002DE RID: 734
		private sealed class AssociatedColumnCollection : ICollection<Column>, IEnumerable<Column>, IEnumerable
		{
			// Token: 0x06002372 RID: 9074 RVA: 0x000E1DB7 File Offset: 0x000DFFB7
			public AssociatedColumnCollection(TimeUnitColumnAssociation owner)
			{
				this.owner = owner;
			}

			// Token: 0x17000748 RID: 1864
			// (get) Token: 0x06002373 RID: 9075 RVA: 0x000E1DC8 File Offset: 0x000DFFC8
			public int Count
			{
				get
				{
					int num = this.owner._CalendarColumnReferences.Count((CalendarColumnReference ccr) => ccr.IsPrimaryColumn);
					return this.owner._CalendarColumnReferences.Count - num;
				}
			}

			// Token: 0x17000749 RID: 1865
			// (get) Token: 0x06002374 RID: 9076 RVA: 0x000E1E17 File Offset: 0x000E0017
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06002375 RID: 9077 RVA: 0x000E1E1C File Offset: 0x000E001C
			public void Add(Column item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				CalendarColumnReference calendarColumnReference = new CalendarColumnReference
				{
					Column = item,
					IsPrimaryColumn = false
				};
				this.owner._CalendarColumnReferences.Add(calendarColumnReference);
			}

			// Token: 0x06002376 RID: 9078 RVA: 0x000E1E5C File Offset: 0x000E005C
			public void Clear()
			{
				List<CalendarColumnReference> list = new List<CalendarColumnReference>(this.owner._CalendarColumnReferences.Where((CalendarColumnReference ccr) => !ccr.IsPrimaryColumn));
				for (int i = 0; i < list.Count; i++)
				{
					this.owner._CalendarColumnReferences.Remove(list[i]);
				}
			}

			// Token: 0x06002377 RID: 9079 RVA: 0x000E1EC8 File Offset: 0x000E00C8
			public bool Contains(Column item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				return this.owner._CalendarColumnReferences.Any((CalendarColumnReference ccr) => !ccr.IsPrimaryColumn && ccr.Column == item);
			}

			// Token: 0x06002378 RID: 9080 RVA: 0x000E1F14 File Offset: 0x000E0114
			public void CopyTo(Column[] array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0 || arrayIndex + this.Count >= array.Length)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", TomSR.Exception_InvalidArrayIndex);
				}
				foreach (CalendarColumnReference calendarColumnReference in this.owner._CalendarColumnReferences.Where((CalendarColumnReference ccr) => !ccr.IsPrimaryColumn))
				{
					array[arrayIndex++] = calendarColumnReference.Column;
				}
			}

			// Token: 0x06002379 RID: 9081 RVA: 0x000E1FC0 File Offset: 0x000E01C0
			public bool Remove(Column item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				CalendarColumnReference calendarColumnReference = this.owner._CalendarColumnReferences.Where((CalendarColumnReference ccr) => !ccr.IsPrimaryColumn && ccr.Column == item).FirstOrDefault<CalendarColumnReference>();
				return calendarColumnReference != null && this.owner._CalendarColumnReferences.Remove(calendarColumnReference);
			}

			// Token: 0x0600237A RID: 9082 RVA: 0x000E2025 File Offset: 0x000E0225
			public IEnumerator<Column> GetEnumerator()
			{
				return new TimeUnitColumnAssociation.AssociatedColumnCollection.AssociatedColumnEnumerator(this.owner._CalendarColumnReferences);
			}

			// Token: 0x0600237B RID: 9083 RVA: 0x000E203C File Offset: 0x000E023C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new TimeUnitColumnAssociation.AssociatedColumnCollection.AssociatedColumnEnumerator(this.owner._CalendarColumnReferences);
			}

			// Token: 0x04000A91 RID: 2705
			private TimeUnitColumnAssociation owner;

			// Token: 0x02000457 RID: 1111
			private struct AssociatedColumnEnumerator : IEnumerator<Column>, IDisposable, IEnumerator
			{
				// Token: 0x06002942 RID: 10562 RVA: 0x000F139E File Offset: 0x000EF59E
				public AssociatedColumnEnumerator(CalendarColumnReferenceCollection references)
				{
					this.referenceEnumerator = references.GetEnumerator();
				}

				// Token: 0x17000802 RID: 2050
				// (get) Token: 0x06002943 RID: 10563 RVA: 0x000F13AC File Offset: 0x000EF5AC
				public Column Current
				{
					get
					{
						CalendarColumnReference calendarColumnReference = this.referenceEnumerator.Current;
						if (calendarColumnReference == null)
						{
							return null;
						}
						return calendarColumnReference.Column;
					}
				}

				// Token: 0x17000803 RID: 2051
				// (get) Token: 0x06002944 RID: 10564 RVA: 0x000F13C4 File Offset: 0x000EF5C4
				object IEnumerator.Current
				{
					get
					{
						CalendarColumnReference calendarColumnReference = this.referenceEnumerator.Current;
						if (calendarColumnReference == null)
						{
							return null;
						}
						return calendarColumnReference.Column;
					}
				}

				// Token: 0x06002945 RID: 10565 RVA: 0x000F13DC File Offset: 0x000EF5DC
				public bool MoveNext()
				{
					while (this.referenceEnumerator.MoveNext())
					{
						if (!this.referenceEnumerator.Current.IsPrimaryColumn)
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x06002946 RID: 10566 RVA: 0x000F1400 File Offset: 0x000EF600
				public void Reset()
				{
					this.referenceEnumerator.Reset();
				}

				// Token: 0x06002947 RID: 10567 RVA: 0x000F140D File Offset: 0x000EF60D
				public void Dispose()
				{
					this.referenceEnumerator.Dispose();
				}

				// Token: 0x04001462 RID: 5218
				private IEnumerator<CalendarColumnReference> referenceEnumerator;
			}
		}
	}
}
