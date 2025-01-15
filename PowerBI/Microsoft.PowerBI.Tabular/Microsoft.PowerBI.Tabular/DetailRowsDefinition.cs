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
	// Token: 0x02000057 RID: 87
	[CompatibilityRequirement("1400")]
	public sealed class DetailRowsDefinition : MetadataObject
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x00020292 File Offset: 0x0001E492
		public DetailRowsDefinition()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000202A0 File Offset: 0x0001E4A0
		internal DetailRowsDefinition(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000202AE File Offset: 0x0001E4AE
		private void InitBodyAndCollections()
		{
			this.body = new DetailRowsDefinition.ObjectBody(this);
			this.body.Expression = string.Empty;
			this.body.State = ObjectState.Ready;
			this.body.ErrorMessage = string.Empty;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x000202E8 File Offset: 0x0001E4E8
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.DetailRowsDefinition;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000202EC File Offset: 0x0001E4EC
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00020300 File Offset: 0x0001E500
		public override MetadataObject Parent
		{
			get
			{
				return this.body.ObjectID.Object;
			}
			internal set
			{
				if (this.body.ObjectID.Object == value)
				{
					return;
				}
				if (value == null)
				{
					MetadataObject.UpdateMetadataObjectParent<DetailRowsDefinition, MetadataObject>(this.body.ObjectID, value, null, null);
					return;
				}
				ObjectType objectType = value.ObjectType;
				if (objectType == ObjectType.Table)
				{
					MetadataObject.UpdateMetadataObjectParent<DetailRowsDefinition, MetadataObject>(this.body.ObjectID, (Table)value, "DefaultDetailRowsDefinition", CompatibilityRestrictions.Table_DefaultDetailRowsDefinition);
					return;
				}
				if (objectType != ObjectType.Measure)
				{
					throw TomInternalException.Create("Got a parent object of unexpected type: {0}", new object[] { value.ObjectType });
				}
				MetadataObject.UpdateMetadataObjectParent<DetailRowsDefinition, MetadataObject>(this.body.ObjectID, (Measure)value, "DetailRowsDefinition", CompatibilityRestrictions.Measure_DetailRowsDefinition);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x000203AC File Offset: 0x0001E5AC
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ObjectID.ObjectID;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000203C0 File Offset: 0x0001E5C0
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.DetailRowsDefinition, null, "DetailRowsDefinition object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, PropertyHelper.GetObjectStateCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, typeof(string));
				}
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000204B8 File Offset: 0x0001E6B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.DetailRowsDefinition[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x000204EF File Offset: 0x0001E6EF
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x000204F7 File Offset: 0x0001E6F7
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (DetailRowsDefinition.ObjectBody)value;
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00020505 File Offset: 0x0001E705
		internal override ITxObjectBody CreateBody()
		{
			return new DetailRowsDefinition.ObjectBody(this);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0002050D File Offset: 0x0001E70D
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new DetailRowsDefinition();
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00020514 File Offset: 0x0001E714
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return null;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00020517 File Offset: 0x0001E717
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			MetadataObject.ResolveMetadataObjectParentById<DetailRowsDefinition, MetadataObject>(this.body.ObjectID, objectMap, throwIfCantResolve, delegate(ObjectType type, out string requestingPath, out CompatibilityRestrictionSet restrictions)
			{
				if (type == ObjectType.Table)
				{
					requestingPath = "DefaultDetailRowsDefinition";
					restrictions = CompatibilityRestrictions.Table_DefaultDetailRowsDefinition;
					return;
				}
				if (type != ObjectType.Measure)
				{
					throw TomInternalException.Create("Got a parent object of unexpected type: {0}", new object[] { type });
				}
				requestingPath = "DetailRowsDefinition";
				restrictions = CompatibilityRestrictions.Measure_DetailRowsDefinition;
			});
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0002054B File Offset: 0x0001E74B
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0002054D File Offset: 0x0001E74D
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x0002055C File Offset: 0x0001E75C
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

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000205CC File Offset: 0x0001E7CC
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x000205DC File Offset: 0x0001E7DC
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

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00020660 File Offset: 0x0001E860
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x00020670 File Offset: 0x0001E870
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

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00020792 File Offset: 0x0001E992
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x000207A0 File Offset: 0x0001E9A0
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

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00020810 File Offset: 0x0001EA10
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x00020824 File Offset: 0x0001EA24
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

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x000208A8 File Offset: 0x0001EAA8
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x000208BA File Offset: 0x0001EABA
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

		// Token: 0x06000431 RID: 1073 RVA: 0x000208D0 File Offset: 0x0001EAD0
		internal void CopyFrom(DetailRowsDefinition other, CopyContext context)
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

		// Token: 0x06000432 RID: 1074 RVA: 0x00020961 File Offset: 0x0001EB61
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((DetailRowsDefinition)other, context);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00020970 File Offset: 0x0001EB70
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(DetailRowsDefinition other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0002098C File Offset: 0x0001EB8C
		public void CopyTo(DetailRowsDefinition other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000209A8 File Offset: 0x0001EBA8
		public DetailRowsDefinition Clone()
		{
			return base.CloneInternal<DetailRowsDefinition>();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000209B0 File Offset: 0x0001EBB0
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.DetailRowsDefinition.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object DetailRowsDefinition is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.ObjectID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ObjectID", this.body.ObjectID.Object);
				writer.WriteProperty<int>(options, "ObjectType", (int)this.body.ObjectID.Object.ObjectType);
			}
			if (!string.IsNullOrEmpty(this.body.Expression))
			{
				writer.WriteProperty<string>(options, "Expression", this.body.Expression);
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00020A74 File Offset: 0x0001EC74
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ObjectID", out objectId))
			{
				this.body.ObjectID.ObjectID = objectId;
			}
			string text;
			if (reader.TryReadProperty<string>("Expression", out text))
			{
				this.body.Expression = text;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			ObjectState objectState;
			if (reader.TryReadProperty<ObjectState>("State", out objectState))
			{
				this.body.State = objectState;
			}
			string text2;
			if (reader.TryReadProperty<string>("ErrorMessage", out text2))
			{
				this.body.ErrorMessage = text2;
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00020B18 File Offset: 0x0001ED18
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.DetailRowsDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object DetailRowsDefinition is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.ObjectID.Object != null && writer.ShouldIncludeProperty("ObjectID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("ObjectID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.ObjectID.Object);
				writer.WriteObjectTypeProperty("ObjectType", MetadataPropertyNature.LinkTypeProperty, this.body.ObjectID.Object.ObjectType);
			}
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Expression);
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00020C18 File Offset: 0x0001EE18
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.DetailRowsDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object DetailRowsDefinition is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Expression);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
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
			if (!string.IsNullOrEmpty(this.body.ErrorMessage) && writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteStringProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.ErrorMessage);
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00020DBC File Offset: 0x0001EFBC
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
				if (length != 5)
				{
					switch (length)
					{
					case 8:
						if (propertyName == "ObjectID")
						{
							this.body.ObjectID.ObjectID = reader.ReadObjectIdProperty();
							return true;
						}
						break;
					case 10:
					{
						char c = propertyName[0];
						if (c != 'E')
						{
							if (c != 'e')
							{
								break;
							}
							if (!(propertyName == "expression"))
							{
								break;
							}
						}
						else if (!(propertyName == "Expression"))
						{
							break;
						}
						this.body.Expression = reader.ReadStringProperty();
						return true;
					}
					case 12:
					{
						char c = propertyName[0];
						if (c <= 'M')
						{
							if (c != 'E')
							{
								if (c != 'M')
								{
									break;
								}
								if (!(propertyName == "ModifiedTime"))
								{
									break;
								}
							}
							else
							{
								if (!(propertyName == "ErrorMessage"))
								{
									break;
								}
								goto IL_01C8;
							}
						}
						else if (c != 'e')
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
						else
						{
							if (!(propertyName == "errorMessage"))
							{
								break;
							}
							goto IL_01C8;
						}
						this.body.ModifiedTime = reader.ReadDateTimeProperty();
						return true;
						IL_01C8:
						this.body.ErrorMessage = reader.ReadStringProperty();
						return true;
					}
					}
				}
				else
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 's')
						{
							goto IL_01DB;
						}
						if (!(propertyName == "state"))
						{
							goto IL_01DB;
						}
					}
					else if (!(propertyName == "State"))
					{
						goto IL_01DB;
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
			}
			IL_01DB:
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00020FA8 File Offset: 0x0001F1A8
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.DetailRowsDefinition.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object DetailRowsDefinition is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Expression))
			{
				result["expression", TomPropCategory.Regular, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Expression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 3, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && this.body.State != ObjectState.Ready)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["state", TomPropCategory.Regular, 4, true] = JsonPropertyHelper.ConvertEnumToJsonValue<ObjectState>(this.body.State);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && !string.IsNullOrEmpty(this.body.ErrorMessage))
			{
				result["errorMessage", TomPropCategory.Regular, 5, true] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.ErrorMessage, SplitMultilineOptions.None);
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00021130 File Offset: 0x0001F330
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "expression")
			{
				this.body.Expression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "modifiedTime")
			{
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			if (!(name == "state"))
			{
				if (!(name == "errorMessage"))
				{
					return false;
				}
				this.body.ErrorMessage = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
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

		// Token: 0x0600043D RID: 1085 RVA: 0x000211FC File Offset: 0x0001F3FC
		internal override string GetFormattedObjectPath()
		{
			if (this.Object == null)
			{
				return TomSR.ObjectPath_DetailRowsDefinition_0Args;
			}
			ObjectType objectType = this.Object.ObjectType;
			if (objectType == ObjectType.Table)
			{
				return TomSR.ObjectPath_DetailRowsDefinition_1Arg_Table(((Table)this.Object).Name);
			}
			if (objectType != ObjectType.Measure)
			{
				return null;
			}
			Measure measure = (Measure)this.Object;
			if (measure.Table != null)
			{
				return TomSR.ObjectPath_DetailRowsDefinition_2Args(measure.Name, measure.Table.Name);
			}
			return TomSR.ObjectPath_DetailRowsDefinition_1Arg_Measure(measure.Name);
		}

		// Token: 0x040000F4 RID: 244
		internal DetailRowsDefinition.ObjectBody body;

		// Token: 0x0200025E RID: 606
		internal class ObjectBody : MetadataObjectBody<DetailRowsDefinition>
		{
			// Token: 0x06002008 RID: 8200 RVA: 0x000D2D87 File Offset: 0x000D0F87
			public ObjectBody(DetailRowsDefinition owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.ObjectID = new UntypedParentLink<DetailRowsDefinition>(owner, "Object");
			}

			// Token: 0x06002009 RID: 8201 RVA: 0x000D2DAC File Offset: 0x000D0FAC
			internal bool IsEqualTo(DetailRowsDefinition.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ObjectID.IsEqualTo(other.ObjectID, context));
			}

			// Token: 0x0600200A RID: 8202 RVA: 0x000D2E70 File Offset: 0x000D1070
			internal void CopyFromImpl(DetailRowsDefinition.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.Expression = other.Expression;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.State = other.State;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ErrorMessage = other.ErrorMessage;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ObjectID.CopyFrom(other.ObjectID, context);
				}
			}

			// Token: 0x0600200B RID: 8203 RVA: 0x000D2F20 File Offset: 0x000D1120
			internal void CopyFromImpl(DetailRowsDefinition.ObjectBody other)
			{
				this.Expression = other.Expression;
				this.ModifiedTime = other.ModifiedTime;
				this.State = other.State;
				this.ErrorMessage = other.ErrorMessage;
				this.ObjectID.CopyFrom(other.ObjectID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x0600200C RID: 8204 RVA: 0x000D2F73 File Offset: 0x000D1173
			public override void CopyFrom(MetadataObjectBody<DetailRowsDefinition> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((DetailRowsDefinition.ObjectBody)other, context);
			}

			// Token: 0x0600200D RID: 8205 RVA: 0x000D2F8C File Offset: 0x000D118C
			internal bool IsEqualTo(DetailRowsDefinition.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage) && this.ObjectID.IsEqualTo(other.ObjectID);
			}

			// Token: 0x0600200E RID: 8206 RVA: 0x000D3003 File Offset: 0x000D1203
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((DetailRowsDefinition.ObjectBody)other);
			}

			// Token: 0x0600200F RID: 8207 RVA: 0x000D301C File Offset: 0x000D121C
			internal void CompareWith(DetailRowsDefinition.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Expression, other.Expression))
				{
					context.RegisterPropertyChange(base.Owner, "Expression", typeof(string), PropertyFlags.DdlAndUser, other.Expression, this.Expression);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.State, other.State))
				{
					context.RegisterPropertyChange(base.Owner, "State", typeof(ObjectState), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.State, this.State);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage))
				{
					context.RegisterPropertyChange(base.Owner, "ErrorMessage", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ErrorMessage, this.ErrorMessage);
				}
				this.ObjectID.CompareWith(other.ObjectID, "ObjectID", "Object", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06002010 RID: 8208 RVA: 0x000D3146 File Offset: 0x000D1346
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((DetailRowsDefinition.ObjectBody)other, context);
			}

			// Token: 0x0400081B RID: 2075
			internal string Expression;

			// Token: 0x0400081C RID: 2076
			internal DateTime ModifiedTime;

			// Token: 0x0400081D RID: 2077
			internal ObjectState State;

			// Token: 0x0400081E RID: 2078
			internal string ErrorMessage;

			// Token: 0x0400081F RID: 2079
			internal UntypedParentLink<DetailRowsDefinition> ObjectID;
		}
	}
}
