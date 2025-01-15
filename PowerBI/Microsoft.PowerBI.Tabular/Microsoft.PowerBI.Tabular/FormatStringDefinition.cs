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
	// Token: 0x0200005B RID: 91
	[CompatibilityRequirement("1470")]
	public sealed class FormatStringDefinition : MetadataObject
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x000235FA File Offset: 0x000217FA
		public FormatStringDefinition()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00023608 File Offset: 0x00021808
		internal FormatStringDefinition(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00023616 File Offset: 0x00021816
		private void InitBodyAndCollections()
		{
			this.body = new FormatStringDefinition.ObjectBody(this);
			this.body.Expression = string.Empty;
			this.body.State = ObjectState.Ready;
			this.body.ErrorMessage = string.Empty;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00023650 File Offset: 0x00021850
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.FormatStringDefinition;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00023654 File Offset: 0x00021854
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00023668 File Offset: 0x00021868
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
					MetadataObject.UpdateMetadataObjectParent<FormatStringDefinition, MetadataObject>(this.body.ObjectID, value, null, null);
					return;
				}
				ObjectType objectType = value.ObjectType;
				if (objectType == ObjectType.Measure)
				{
					MetadataObject.UpdateMetadataObjectParent<FormatStringDefinition, MetadataObject>(this.body.ObjectID, (Measure)value, "FormatStringDefinition", CompatibilityRestrictions.Measure_FormatStringDefinition);
					return;
				}
				if (objectType == ObjectType.CalculationItem)
				{
					MetadataObject.UpdateMetadataObjectParent<FormatStringDefinition, MetadataObject>(this.body.ObjectID, (CalculationItem)value, "FormatStringDefinition", null);
					return;
				}
				if (objectType != ObjectType.CalculationExpression)
				{
					throw TomInternalException.Create("Got a parent object of unexpected type: {0}", new object[] { value.ObjectType });
				}
				MetadataObject.UpdateMetadataObjectParent<FormatStringDefinition, MetadataObject>(this.body.ObjectID, (CalculationGroupExpression)value, "FormatStringDefinition", null);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00023733 File Offset: 0x00021933
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ObjectID.ObjectID;
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00023748 File Offset: 0x00021948
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.FormatStringDefinition, null, "FormatStringDefinition object of Tabular Object Model (TOM)", new bool?(false)))
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

		// Token: 0x060004A8 RID: 1192 RVA: 0x00023840 File Offset: 0x00021A40
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.FormatStringDefinition[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00023877 File Offset: 0x00021A77
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x0002387F File Offset: 0x00021A7F
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (FormatStringDefinition.ObjectBody)value;
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0002388D File Offset: 0x00021A8D
		internal override ITxObjectBody CreateBody()
		{
			return new FormatStringDefinition.ObjectBody(this);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00023895 File Offset: 0x00021A95
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new FormatStringDefinition();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0002389C File Offset: 0x00021A9C
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return null;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0002389F File Offset: 0x00021A9F
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			MetadataObject.ResolveMetadataObjectParentById<FormatStringDefinition, MetadataObject>(this.body.ObjectID, objectMap, throwIfCantResolve, delegate(ObjectType type, out string requestingPath, out CompatibilityRestrictionSet restrictions)
			{
				if (type == ObjectType.Measure)
				{
					requestingPath = "FormatStringDefinition";
					restrictions = CompatibilityRestrictions.Measure_FormatStringDefinition;
					return;
				}
				if (type == ObjectType.CalculationItem)
				{
					requestingPath = "FormatStringDefinition";
					restrictions = null;
					return;
				}
				if (type != ObjectType.CalculationExpression)
				{
					throw TomInternalException.Create("Got a parent object of unexpected type: {0}", new object[] { type });
				}
				requestingPath = "FormatStringDefinition";
				restrictions = null;
			});
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000238D3 File Offset: 0x00021AD3
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x000238D5 File Offset: 0x00021AD5
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x000238E4 File Offset: 0x00021AE4
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

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00023954 File Offset: 0x00021B54
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x00023964 File Offset: 0x00021B64
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

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000239E8 File Offset: 0x00021BE8
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x000239F8 File Offset: 0x00021BF8
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

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00023B1A File Offset: 0x00021D1A
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x00023B28 File Offset: 0x00021D28
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

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00023B98 File Offset: 0x00021D98
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x00023BAC File Offset: 0x00021DAC
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00023C30 File Offset: 0x00021E30
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x00023C42 File Offset: 0x00021E42
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

		// Token: 0x060004BC RID: 1212 RVA: 0x00023C58 File Offset: 0x00021E58
		internal void CopyFrom(FormatStringDefinition other, CopyContext context)
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

		// Token: 0x060004BD RID: 1213 RVA: 0x00023CE9 File Offset: 0x00021EE9
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((FormatStringDefinition)other, context);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00023CF8 File Offset: 0x00021EF8
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(FormatStringDefinition other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00023D14 File Offset: 0x00021F14
		public void CopyTo(FormatStringDefinition other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00023D30 File Offset: 0x00021F30
		public FormatStringDefinition Clone()
		{
			return base.CloneInternal<FormatStringDefinition>();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00023D38 File Offset: 0x00021F38
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.FormatStringDefinition.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object FormatStringDefinition is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
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

		// Token: 0x060004C2 RID: 1218 RVA: 0x00023DFC File Offset: 0x00021FFC
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

		// Token: 0x060004C3 RID: 1219 RVA: 0x00023EA0 File Offset: 0x000220A0
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.FormatStringDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object FormatStringDefinition is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
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

		// Token: 0x060004C4 RID: 1220 RVA: 0x00023FA0 File Offset: 0x000221A0
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.FormatStringDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object FormatStringDefinition is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
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

		// Token: 0x060004C5 RID: 1221 RVA: 0x00024144 File Offset: 0x00022344
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

		// Token: 0x060004C6 RID: 1222 RVA: 0x00024330 File Offset: 0x00022530
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.FormatStringDefinition.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object FormatStringDefinition is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
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

		// Token: 0x060004C7 RID: 1223 RVA: 0x000244B8 File Offset: 0x000226B8
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

		// Token: 0x060004C8 RID: 1224 RVA: 0x00024584 File Offset: 0x00022784
		internal override string GetFormattedObjectPath()
		{
			if (this.Object == null)
			{
				return TomSR.ObjectPath_FormatStringDefinition_0Args;
			}
			ObjectType objectType = this.Object.ObjectType;
			if (objectType == ObjectType.Measure)
			{
				return TomSR.ObjectPath_FormatStringDefinition_1Args_Measure(((Measure)this.Object).Name);
			}
			if (objectType == ObjectType.CalculationItem)
			{
				return TomSR.ObjectPath_FormatStringDefinition_1Args_CalculationItem(((CalculationItem)this.Object).Name);
			}
			if (objectType != ObjectType.CalculationExpression)
			{
				return null;
			}
			if (this.Object.Parent == null || this.Object.Parent.Parent == null)
			{
				return TomSR.ObjectPath_FormatStringDefinition_0Args;
			}
			return TomSR.ObjectPath_FormatStringDefinition_1Args_CalculationGroupExpression(((Table)this.Object.Parent.Parent).Name);
		}

		// Token: 0x040000F9 RID: 249
		internal FormatStringDefinition.ObjectBody body;

		// Token: 0x02000263 RID: 611
		internal class ObjectBody : MetadataObjectBody<FormatStringDefinition>
		{
			// Token: 0x0600202A RID: 8234 RVA: 0x000D37D7 File Offset: 0x000D19D7
			public ObjectBody(FormatStringDefinition owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.ObjectID = new UntypedParentLink<FormatStringDefinition>(owner, "Object");
			}

			// Token: 0x0600202B RID: 8235 RVA: 0x000D37FC File Offset: 0x000D19FC
			internal bool IsEqualTo(FormatStringDefinition.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ObjectID.IsEqualTo(other.ObjectID, context));
			}

			// Token: 0x0600202C RID: 8236 RVA: 0x000D38C0 File Offset: 0x000D1AC0
			internal void CopyFromImpl(FormatStringDefinition.ObjectBody other, CopyContext context)
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

			// Token: 0x0600202D RID: 8237 RVA: 0x000D3970 File Offset: 0x000D1B70
			internal void CopyFromImpl(FormatStringDefinition.ObjectBody other)
			{
				this.Expression = other.Expression;
				this.ModifiedTime = other.ModifiedTime;
				this.State = other.State;
				this.ErrorMessage = other.ErrorMessage;
				this.ObjectID.CopyFrom(other.ObjectID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x0600202E RID: 8238 RVA: 0x000D39C3 File Offset: 0x000D1BC3
			public override void CopyFrom(MetadataObjectBody<FormatStringDefinition> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((FormatStringDefinition.ObjectBody)other, context);
			}

			// Token: 0x0600202F RID: 8239 RVA: 0x000D39DC File Offset: 0x000D1BDC
			internal bool IsEqualTo(FormatStringDefinition.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage) && this.ObjectID.IsEqualTo(other.ObjectID);
			}

			// Token: 0x06002030 RID: 8240 RVA: 0x000D3A53 File Offset: 0x000D1C53
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((FormatStringDefinition.ObjectBody)other);
			}

			// Token: 0x06002031 RID: 8241 RVA: 0x000D3A6C File Offset: 0x000D1C6C
			internal void CompareWith(FormatStringDefinition.ObjectBody other, CompareContext context)
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

			// Token: 0x06002032 RID: 8242 RVA: 0x000D3B96 File Offset: 0x000D1D96
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((FormatStringDefinition.ObjectBody)other, context);
			}

			// Token: 0x0400082B RID: 2091
			internal string Expression;

			// Token: 0x0400082C RID: 2092
			internal DateTime ModifiedTime;

			// Token: 0x0400082D RID: 2093
			internal ObjectState State;

			// Token: 0x0400082E RID: 2094
			internal string ErrorMessage;

			// Token: 0x0400082F RID: 2095
			internal UntypedParentLink<FormatStringDefinition> ObjectID;
		}
	}
}
