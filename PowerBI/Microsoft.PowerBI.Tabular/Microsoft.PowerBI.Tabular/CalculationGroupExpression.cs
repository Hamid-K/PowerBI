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
	// Token: 0x02000039 RID: 57
	[CompatibilityRequirement("1605")]
	public sealed class CalculationGroupExpression : MetadataObject, IKeyedMetadataObject
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00009D44 File Offset: 0x00007F44
		public CalculationGroupExpression()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009D52 File Offset: 0x00007F52
		internal CalculationGroupExpression(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00009D60 File Offset: 0x00007F60
		private void InitBodyAndCollections()
		{
			this.body = new CalculationGroupExpression.ObjectBody(this);
			this.body.Description = string.Empty;
			this.body.Expression = string.Empty;
			this.body.State = ObjectState.Ready;
			this.body.ErrorMessage = string.Empty;
			this.body.SelectionMode = CalculationGroupSelectionMode.Unknown;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00009DC1 File Offset: 0x00007FC1
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.CalculationExpression;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00009DC5 File Offset: 0x00007FC5
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00009DD7 File Offset: 0x00007FD7
		public override MetadataObject Parent
		{
			get
			{
				return this.body.CalculationGroupID.Object;
			}
			internal set
			{
				if (this.body.CalculationGroupID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<CalculationGroupExpression, CalculationGroup>(this.body.CalculationGroupID, (CalculationGroup)value, null, null);
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00009E04 File Offset: 0x00008004
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.CalculationGroupID.ObjectID;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009E18 File Offset: 0x00008018
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.CalculationExpression, null, "CalculationGroupExpression object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
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
				if (writer.ShouldIncludeProperty("formatStringDefinition", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "formatStringDefinition", MetadataPropertyNature.ChildProperty, ObjectType.FormatStringDefinition);
				}
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00009F64 File Offset: 0x00008164
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.CalculationGroupExpression[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.SelectionMode != CalculationGroupSelectionMode.Unknown)
			{
				int num = PropertyHelper.GetCalculationGroupSelectionModeCompatibilityRestrictions(this.body.SelectionMode)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SelectionMode");
					requiredLevel = num;
					int num2 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00009FF7 File Offset: 0x000081F7
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00009FFF File Offset: 0x000081FF
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (CalculationGroupExpression.ObjectBody)value;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A00D File Offset: 0x0000820D
		internal override ITxObjectBody CreateBody()
		{
			return new CalculationGroupExpression.ObjectBody(this);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A015 File Offset: 0x00008215
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new CalculationGroupExpression();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A01C File Offset: 0x0000821C
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((CalculationGroup)parent).CalculationExpressions;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A02C File Offset: 0x0000822C
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			CalculationGroup calculationGroup = MetadataObject.ResolveMetadataObjectParentById<CalculationGroupExpression, CalculationGroup>(this.body.CalculationGroupID, objectMap, throwIfCantResolve, null, null);
			this.body.FormatStringDefinitionID.ResolveById(objectMap, throwIfCantResolve);
			if (calculationGroup != null)
			{
				calculationGroup.CalculationExpressions.Add(this);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000A070 File Offset: 0x00008270
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A072 File Offset: 0x00008272
		internal override IEnumerable<MetadataObject> GetDirectChildren(bool isLogicalStructure)
		{
			if (this.body.FormatStringDefinitionID.Object != null)
			{
				yield return this.body.FormatStringDefinitionID.Object;
			}
			yield break;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A084 File Offset: 0x00008284
		private protected override void SetDirectChildImpl(MetadataObject child)
		{
			if (child.ObjectType == ObjectType.FormatStringDefinition)
			{
				ObjectChangeTracker.RegisterPropertyChanging(this, "FormatStringDefinition", typeof(FormatStringDefinition), this.body.FormatStringDefinitionID.Object, child);
				FormatStringDefinition @object = this.body.FormatStringDefinitionID.Object;
				this.body.FormatStringDefinitionID.Object = (FormatStringDefinition)child;
				ObjectChangeTracker.RegisterPropertyChanged(this, "FormatStringDefinition", typeof(FormatStringDefinition), @object, child);
				return;
			}
			base.SetDirectChildImpl(child);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A108 File Offset: 0x00008308
		private protected override void RemoveDirectChildImpl(MetadataObject child)
		{
			if (child.ObjectType == ObjectType.FormatStringDefinition)
			{
				if (this.body.FormatStringDefinitionID.ObjectID == child.Id)
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "FormatStringDefinition", typeof(FormatStringDefinition), this.body.FormatStringDefinitionID.Object, null);
					FormatStringDefinition @object = this.body.FormatStringDefinitionID.Object;
					this.body.FormatStringDefinitionID.Object = null;
					ObjectChangeTracker.RegisterPropertyChanged(this, "FormatStringDefinition", typeof(FormatStringDefinition), @object, null);
					return;
				}
			}
			else
			{
				base.RemoveDirectChildImpl(child);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000A1A3 File Offset: 0x000083A3
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000A1B0 File Offset: 0x000083B0
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000A220 File Offset: 0x00008420
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000A230 File Offset: 0x00008430
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000A2A0 File Offset: 0x000084A0
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000A2B0 File Offset: 0x000084B0
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000A334 File Offset: 0x00008534
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000A344 File Offset: 0x00008544
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000A466 File Offset: 0x00008666
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000A474 File Offset: 0x00008674
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

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000A4E4 File Offset: 0x000086E4
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000A4F4 File Offset: 0x000086F4
		internal CalculationGroupSelectionMode SelectionMode
		{
			get
			{
				return this.body.SelectionMode;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SelectionMode, value))
				{
					CompatibilityRestrictionSet calculationGroupSelectionModeCompatibilityRestrictions = PropertyHelper.GetCalculationGroupSelectionModeCompatibilityRestrictions(value);
					CompatibilityRestrictionSet calculationGroupSelectionModeCompatibilityRestrictions2 = PropertyHelper.GetCalculationGroupSelectionModeCompatibilityRestrictions(this.body.SelectionMode);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = calculationGroupSelectionModeCompatibilityRestrictions.Compare(calculationGroupSelectionModeCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != CalculationGroupSelectionMode.Unknown))
					{
						array = base.ValidateCompatibilityRequirement(calculationGroupSelectionModeCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "SelectionMode", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "SelectionMode", typeof(CalculationGroupSelectionMode), this.body.SelectionMode, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(calculationGroupSelectionModeCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(calculationGroupSelectionModeCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(calculationGroupSelectionModeCompatibilityRestrictions, array);
						break;
					}
					CalculationGroupSelectionMode selectionMode = this.body.SelectionMode;
					this.body.SelectionMode = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SelectionMode", typeof(CalculationGroupSelectionMode), selectionMode, value);
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000A615 File Offset: 0x00008815
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000A628 File Offset: 0x00008828
		public CalculationGroup CalculationGroup
		{
			get
			{
				return this.body.CalculationGroupID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.CalculationGroupID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "CalculationGroup", typeof(CalculationGroup), this.body.CalculationGroupID.Object, value);
					CalculationGroup @object = this.body.CalculationGroupID.Object;
					this.body.CalculationGroupID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "CalculationGroup", typeof(CalculationGroup), @object, value);
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000A6AC File Offset: 0x000088AC
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000A6BE File Offset: 0x000088BE
		internal ObjectId _CalculationGroupID
		{
			get
			{
				return this.body.CalculationGroupID.ObjectID;
			}
			set
			{
				this.body.CalculationGroupID.ObjectID = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000A6D1 File Offset: 0x000088D1
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000A6E4 File Offset: 0x000088E4
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
						base.ValidateCompatibilityRequirement(value, "FormatStringDefinition", null);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "FormatStringDefinition", typeof(FormatStringDefinition), this.body.FormatStringDefinitionID.Object, value);
					FormatStringDefinition @object = this.body.FormatStringDefinitionID.Object;
					this.body.FormatStringDefinitionID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "FormatStringDefinition", typeof(FormatStringDefinition), @object, value);
				}
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000A779 File Offset: 0x00008979
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000A78B File Offset: 0x0000898B
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

		// Token: 0x06000173 RID: 371 RVA: 0x0000A7A0 File Offset: 0x000089A0
		internal void CopyFrom(CalculationGroupExpression other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions || !this.body.IsEqualTo(other.body, context))
			{
				ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				this.body.CopyFrom(other.body, context);
				return;
			}
			if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy && this.body.FormatStringDefinitionID.Object != null && other.body.FormatStringDefinitionID.Object != null)
			{
				this.body.FormatStringDefinitionID.Object.CopyFrom(other.body.FormatStringDefinitionID.Object, context);
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000A84C File Offset: 0x00008A4C
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((CalculationGroupExpression)other, context);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000A85B File Offset: 0x00008A5B
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(CalculationGroupExpression other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000A877 File Offset: 0x00008A77
		public void CopyTo(CalculationGroupExpression other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000A893 File Offset: 0x00008A93
		public CalculationGroupExpression Clone()
		{
			return base.CloneInternal<CalculationGroupExpression>();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000A89C File Offset: 0x00008A9C
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalculationExpression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.CalculationGroupID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "CalculationGroupID", this.body.CalculationGroupID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.Expression))
			{
				writer.WriteProperty<string>(options, "Expression", this.body.Expression);
			}
			if (this.body.SelectionMode != CalculationGroupSelectionMode.Unknown)
			{
				if (!PropertyHelper.IsCalculationGroupSelectionModeValueCompatible(this.body.SelectionMode, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SelectionMode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<CalculationGroupSelectionMode>(options, "SelectionMode", this.body.SelectionMode);
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000A9C4 File Offset: 0x00008BC4
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("CalculationGroupID", out objectId))
			{
				this.body.CalculationGroupID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("FormatStringDefinitionID", out objectId2))
			{
				this.body.FormatStringDefinitionID.ObjectID = objectId2;
			}
			string text;
			if (reader.TryReadProperty<string>("Description", out text))
			{
				this.body.Description = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("Expression", out text2))
			{
				this.body.Expression = text2;
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
			string text3;
			if (reader.TryReadProperty<string>("ErrorMessage", out text3))
			{
				this.body.ErrorMessage = text3;
			}
			CalculationGroupSelectionMode calculationGroupSelectionMode;
			if (reader.TryReadProperty<CalculationGroupSelectionMode>("SelectionMode", out calculationGroupSelectionMode))
			{
				this.body.SelectionMode = calculationGroupSelectionMode;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalculationExpression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.CalculationGroupID.Object != null && writer.ShouldIncludeProperty("CalculationGroupID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("CalculationGroupID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.CalculationGroupID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Expression);
			}
			if (this.body.SelectionMode != CalculationGroupSelectionMode.Unknown)
			{
				if (!PropertyHelper.IsCalculationGroupSelectionModeValueCompatible(this.body.SelectionMode, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SelectionMode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("SelectionMode", MetadataPropertyNature.NameProperty))
				{
					writer.WriteEnumProperty<CalculationGroupSelectionMode>("SelectionMode", MetadataPropertyNature.NameProperty, this.body.SelectionMode);
				}
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000AC5C File Offset: 0x00008E5C
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalculationExpression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Expression);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Description);
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
			if (this.body.FormatStringDefinitionID.Object != null && writer.ShouldIncludeProperty("formatStringDefinition", MetadataPropertyNature.ChildProperty))
			{
				writer.WriteSingleChild(context, "formatStringDefinition", MetadataPropertyNature.ChildProperty, this.body.FormatStringDefinitionID.Object);
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000AE7C File Offset: 0x0000907C
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
				if (length <= 18)
				{
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
					case 8:
					case 9:
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
					case 11:
					{
						char c = propertyName[0];
						if (c != 'D')
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
								goto IL_02BF;
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
							goto IL_02BF;
						}
						this.body.ModifiedTime = reader.ReadDateTimeProperty();
						return true;
						IL_02BF:
						this.body.ErrorMessage = reader.ReadStringProperty();
						return true;
					}
					case 13:
						if (propertyName == "SelectionMode")
						{
							if (!CompatibilityRestrictions.CalculationGroupSelectionMode.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							this.body.SelectionMode = reader.ReadEnumProperty<CalculationGroupSelectionMode>();
							return true;
						}
						break;
					default:
						if (length == 18)
						{
							if (propertyName == "CalculationGroupID")
							{
								this.body.CalculationGroupID.ObjectID = reader.ReadObjectIdProperty();
								return true;
							}
						}
						break;
					}
				}
				else if (length != 22)
				{
					if (length == 24)
					{
						if (propertyName == "FormatStringDefinitionID")
						{
							this.body.FormatStringDefinitionID.ObjectID = reader.ReadObjectIdProperty();
							return true;
						}
					}
				}
				else if (propertyName == "formatStringDefinition")
				{
					using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
					{
						FormatStringDefinition formatStringDefinition = reader.ReadSingleChildProperty<FormatStringDefinition>(context);
						try
						{
							this.body.FormatStringDefinitionID.Object = formatStringDefinition;
						}
						catch (Exception ex)
						{
							throw reader.CreateInvalidChildException(context, formatStringDefinition, TomSR.Exception_FailedAddDeserializedObject("FormatStringDefinition", ex.Message), ex);
						}
					}
					return true;
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000B204 File Offset: 0x00009404
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalculationExpression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Expression))
			{
				result["expression", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Expression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 5, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && this.body.State != ObjectState.Ready)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["state", TomPropCategory.Regular, 6, true] = JsonPropertyHelper.ConvertEnumToJsonValue<ObjectState>(this.body.State);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && !string.IsNullOrEmpty(this.body.ErrorMessage))
			{
				result["errorMessage", TomPropCategory.Regular, 7, true] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.ErrorMessage, SplitMultilineOptions.None);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && this.body.FormatStringDefinitionID.Object != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.body.FormatStringDefinitionID.Object)))
			{
				result["formatStringDefinition", TomPropCategory.ChildLink, 2, false] = this.body.FormatStringDefinitionID.Object.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000B444 File Offset: 0x00009644
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "description")
			{
				this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
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
				if (name == "errorMessage")
				{
					this.body.ErrorMessage = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
					return true;
				}
				if (!(name == "formatStringDefinition"))
				{
					return false;
				}
				if (jsonProp.Value.Type != 10)
				{
					FormatStringDefinition formatStringDefinition = new FormatStringDefinition();
					formatStringDefinition.DeserializeFromJsonObject(jsonProp.Value, options, mode, dbCompatibilityLevel);
					this.body.FormatStringDefinitionID.Object = formatStringDefinition;
				}
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000B580 File Offset: 0x00009780
		object IKeyedMetadataObject.Key
		{
			get
			{
				return this.body.SelectionMode;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000B592 File Offset: 0x00009792
		string IKeyedMetadataObject.LogicalPathElement
		{
			get
			{
				return this.body.SelectionMode.ToString("G");
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000B5AE File Offset: 0x000097AE
		internal override string GetFormattedObjectPath()
		{
			if (this.Parent == null || this.Parent.Parent == null)
			{
				return TomSR.ObjectPath_CalculationGroupExpression_0Args;
			}
			return TomSR.ObjectPath_CalculationGroupExpression_1Args_CalculationGroup(((Table)this.Parent.Parent).Name);
		}

		// Token: 0x040000D7 RID: 215
		internal CalculationGroupExpression.ObjectBody body;

		// Token: 0x02000236 RID: 566
		internal class ObjectBody : MetadataObjectBody<CalculationGroupExpression>
		{
			// Token: 0x06001F0D RID: 7949 RVA: 0x000CD9A7 File Offset: 0x000CBBA7
			public ObjectBody(CalculationGroupExpression owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.CalculationGroupID = new ParentLink<CalculationGroupExpression, CalculationGroup>(owner, "CalculationGroup");
				this.FormatStringDefinitionID = new ChildLink<CalculationGroupExpression, FormatStringDefinition>(owner, "FormatStringDefinition");
			}

			// Token: 0x06001F0E RID: 7950 RVA: 0x000CD9E0 File Offset: 0x000CBBE0
			internal bool IsEqualTo(CalculationGroupExpression.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage)) && PropertyHelper.AreValuesIdentical(this.SelectionMode, other.SelectionMode) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.CalculationGroupID.IsEqualTo(other.CalculationGroupID, context)) && this.FormatStringDefinitionID.IsEqualTo(other.FormatStringDefinitionID, context);
			}

			// Token: 0x06001F0F RID: 7951 RVA: 0x000CDAE4 File Offset: 0x000CBCE4
			internal void CopyFromImpl(CalculationGroupExpression.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.Description = other.Description;
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
				this.SelectionMode = other.SelectionMode;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.CalculationGroupID.CopyFrom(other.CalculationGroupID, context);
				}
				this.FormatStringDefinitionID.CopyFrom(other.FormatStringDefinitionID, context);
			}

			// Token: 0x06001F10 RID: 7952 RVA: 0x000CDBBC File Offset: 0x000CBDBC
			internal void CopyFromImpl(CalculationGroupExpression.ObjectBody other)
			{
				this.Description = other.Description;
				this.Expression = other.Expression;
				this.ModifiedTime = other.ModifiedTime;
				this.State = other.State;
				this.ErrorMessage = other.ErrorMessage;
				this.SelectionMode = other.SelectionMode;
				this.CalculationGroupID.CopyFrom(other.CalculationGroupID, ObjectChangeTracker.BodyCloneContext);
				this.FormatStringDefinitionID.CopyFrom(other.FormatStringDefinitionID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001F11 RID: 7953 RVA: 0x000CDC3D File Offset: 0x000CBE3D
			public override void CopyFrom(MetadataObjectBody<CalculationGroupExpression> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((CalculationGroupExpression.ObjectBody)other, context);
			}

			// Token: 0x06001F12 RID: 7954 RVA: 0x000CDC54 File Offset: 0x000CBE54
			internal bool IsEqualTo(CalculationGroupExpression.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage) && PropertyHelper.AreValuesIdentical(this.SelectionMode, other.SelectionMode) && this.CalculationGroupID.IsEqualTo(other.CalculationGroupID) && this.FormatStringDefinitionID.IsEqualTo(other.FormatStringDefinitionID);
			}

			// Token: 0x06001F13 RID: 7955 RVA: 0x000CDD0A File Offset: 0x000CBF0A
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((CalculationGroupExpression.ObjectBody)other);
			}

			// Token: 0x06001F14 RID: 7956 RVA: 0x000CDD24 File Offset: 0x000CBF24
			internal void CompareWith(CalculationGroupExpression.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
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
				if (!PropertyHelper.AreValuesIdentical(this.SelectionMode, other.SelectionMode))
				{
					context.RegisterPropertyChange(base.Owner, "SelectionMode", typeof(CalculationGroupSelectionMode), PropertyFlags.DdlAndUser, other.SelectionMode, this.SelectionMode);
				}
				this.CalculationGroupID.CompareWith(other.CalculationGroupID, "CalculationGroupID", "CalculationGroup", PropertyFlags.ReadOnly, context);
				this.FormatStringDefinitionID.CompareWith(other.FormatStringDefinitionID, "FormatStringDefinitionID", "FormatStringDefinition", PropertyFlags.None, context);
			}

			// Token: 0x06001F15 RID: 7957 RVA: 0x000CDEEB File Offset: 0x000CC0EB
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((CalculationGroupExpression.ObjectBody)other, context);
			}

			// Token: 0x04000754 RID: 1876
			internal string Description;

			// Token: 0x04000755 RID: 1877
			internal string Expression;

			// Token: 0x04000756 RID: 1878
			internal DateTime ModifiedTime;

			// Token: 0x04000757 RID: 1879
			internal ObjectState State;

			// Token: 0x04000758 RID: 1880
			internal string ErrorMessage;

			// Token: 0x04000759 RID: 1881
			internal CalculationGroupSelectionMode SelectionMode;

			// Token: 0x0400075A RID: 1882
			internal ParentLink<CalculationGroupExpression, CalculationGroup> CalculationGroupID;

			// Token: 0x0400075B RID: 1883
			internal ChildLink<CalculationGroupExpression, FormatStringDefinition> FormatStringDefinitionID;
		}
	}
}
