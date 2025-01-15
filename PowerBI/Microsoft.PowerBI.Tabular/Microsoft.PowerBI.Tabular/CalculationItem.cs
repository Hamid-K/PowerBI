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
	// Token: 0x0200003B RID: 59
	[CompatibilityRequirement("1470")]
	public sealed class CalculationItem : NamedMetadataObject
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0000B61B File Offset: 0x0000981B
		public CalculationItem()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000B629 File Offset: 0x00009829
		internal CalculationItem(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000B638 File Offset: 0x00009838
		private void InitBodyAndCollections()
		{
			this.body = new CalculationItem.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this.body.State = ObjectState.Ready;
			this.body.ErrorMessage = string.Empty;
			this.body.Expression = string.Empty;
			this.body.Ordinal = -1;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000B6A9 File Offset: 0x000098A9
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.CalculationItem;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000B6AD File Offset: 0x000098AD
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000B6BF File Offset: 0x000098BF
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
					MetadataObject.UpdateMetadataObjectParent<CalculationItem, CalculationGroup>(this.body.CalculationGroupID, (CalculationGroup)value, null, null);
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000B6EC File Offset: 0x000098EC
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.CalculationGroupID.ObjectID;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000B700 File Offset: 0x00009900
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.CalculationItem, null, "CalculationItem object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
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
				if (CompatibilityRestrictions.CalculationItem_Ordinal.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("ordinal", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("ordinal", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (writer.ShouldIncludeProperty("formatStringDefinition", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "formatStringDefinition", MetadataPropertyNature.ChildProperty, ObjectType.FormatStringDefinition);
				}
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.CalculationItem[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.Ordinal != -1)
			{
				int num = CompatibilityRestrictions.CalculationItem_Ordinal[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Ordinal");
					requiredLevel = num;
					int num2 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000B93D File Offset: 0x00009B3D
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000B945 File Offset: 0x00009B45
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (CalculationItem.ObjectBody)value;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000B953 File Offset: 0x00009B53
		internal override ITxObjectBody CreateBody()
		{
			return new CalculationItem.ObjectBody(this);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000B95B File Offset: 0x00009B5B
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new CalculationItem();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000B962 File Offset: 0x00009B62
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((CalculationGroup)parent).CalculationItems;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000B970 File Offset: 0x00009B70
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			CalculationGroup calculationGroup = MetadataObject.ResolveMetadataObjectParentById<CalculationItem, CalculationGroup>(this.body.CalculationGroupID, objectMap, throwIfCantResolve, null, null);
			this.body.FormatStringDefinitionID.ResolveById(objectMap, throwIfCantResolve);
			if (calculationGroup != null)
			{
				calculationGroup.CalculationItems.Add(this);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000B9B6 File Offset: 0x00009BB6
		internal override IEnumerable<MetadataObject> GetDirectChildren(bool isLogicalStructure)
		{
			if (this.body.FormatStringDefinitionID.Object != null)
			{
				yield return this.body.FormatStringDefinitionID.Object;
			}
			yield break;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000B9C8 File Offset: 0x00009BC8
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

		// Token: 0x06000197 RID: 407 RVA: 0x0000BA4C File Offset: 0x00009C4C
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000BAE7 File Offset: 0x00009CE7
		// (set) Token: 0x06000199 RID: 409 RVA: 0x0000BAF4 File Offset: 0x00009CF4
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.CalculationItem, out text))
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000BB77 File Offset: 0x00009D77
		// (set) Token: 0x0600019B RID: 411 RVA: 0x0000BB84 File Offset: 0x00009D84
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

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		// (set) Token: 0x0600019D RID: 413 RVA: 0x0000BC04 File Offset: 0x00009E04
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000BC88 File Offset: 0x00009E88
		// (set) Token: 0x0600019F RID: 415 RVA: 0x0000BC98 File Offset: 0x00009E98
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000BDBA File Offset: 0x00009FBA
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x0000BDC8 File Offset: 0x00009FC8
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

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000BE38 File Offset: 0x0000A038
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000BE48 File Offset: 0x0000A048
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000BEC8 File Offset: 0x0000A0C8
		[CompatibilityRequirement("1500")]
		public int Ordinal
		{
			get
			{
				return this.body.Ordinal;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Ordinal, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != -1)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.CalculationItem_Ordinal, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Ordinal"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Ordinal", typeof(int), this.body.Ordinal, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.CalculationItem_Ordinal, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					int ordinal = this.body.Ordinal;
					this.body.Ordinal = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Ordinal", typeof(int), ordinal, value);
				}
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000BF8D File Offset: 0x0000A18D
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000C024 File Offset: 0x0000A224
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000C036 File Offset: 0x0000A236
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000C049 File Offset: 0x0000A249
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000C05C File Offset: 0x0000A25C
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000C0F1 File Offset: 0x0000A2F1
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000C103 File Offset: 0x0000A303
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

		// Token: 0x060001AE RID: 430 RVA: 0x0000C118 File Offset: 0x0000A318
		internal void CopyFrom(CalculationItem other, CopyContext context)
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

		// Token: 0x060001AF RID: 431 RVA: 0x0000C1C4 File Offset: 0x0000A3C4
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((CalculationItem)other, context);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000C1D3 File Offset: 0x0000A3D3
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(CalculationItem other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000C1EF File Offset: 0x0000A3EF
		public void CopyTo(CalculationItem other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000C20B File Offset: 0x0000A40B
		public CalculationItem Clone()
		{
			return base.CloneInternal<CalculationItem>();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000C214 File Offset: 0x0000A414
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.CalculationItem.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalculationItem is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.CalculationGroupID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "CalculationGroupID", this.body.CalculationGroupID.Object);
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
			if (this.body.Ordinal != -1)
			{
				if (!CompatibilityRestrictions.CalculationItem_Ordinal.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Ordinal is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<int>(options, "Ordinal", this.body.Ordinal);
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000C360 File Offset: 0x0000A560
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
			if (reader.TryReadProperty<string>("Name", out text))
			{
				this.body.Name = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("Description", out text2))
			{
				this.body.Description = text2;
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
			string text4;
			if (reader.TryReadProperty<string>("Expression", out text4))
			{
				this.body.Expression = text4;
			}
			int num;
			if (CompatibilityRestrictions.CalculationItem_Ordinal.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<int>("Ordinal", out num))
			{
				this.body.Ordinal = num;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000C488 File Offset: 0x0000A688
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.CalculationItem.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalculationItem is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.CalculationGroupID.Object != null && writer.ShouldIncludeProperty("CalculationGroupID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("CalculationGroupID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.CalculationGroupID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Expression);
			}
			if (this.body.Ordinal != -1)
			{
				if (!CompatibilityRestrictions.CalculationItem_Ordinal.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Ordinal is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("Ordinal", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteInt32Property("Ordinal", MetadataPropertyNature.RegularProperty, this.body.Ordinal);
				}
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000C65C File Offset: 0x0000A85C
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.CalculationItem.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalculationItem is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
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
			if (this.body.Ordinal != -1)
			{
				if (!CompatibilityRestrictions.CalculationItem_Ordinal.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Ordinal is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("ordinal", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteInt32Property("ordinal", MetadataPropertyNature.RegularProperty, this.body.Ordinal);
				}
			}
			if (this.body.FormatStringDefinitionID.Object != null && writer.ShouldIncludeProperty("formatStringDefinition", MetadataPropertyNature.ChildProperty))
			{
				writer.WriteSingleChild(context, "formatStringDefinition", MetadataPropertyNature.ChildProperty, this.body.FormatStringDefinitionID.Object);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000C934 File Offset: 0x0000AB34
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
					case 8:
					case 9:
						break;
					case 7:
					{
						char c = propertyName[0];
						if (c != 'O')
						{
							if (c != 'o')
							{
								break;
							}
							if (!(propertyName == "ordinal"))
							{
								break;
							}
						}
						else if (!(propertyName == "Ordinal"))
						{
							break;
						}
						if (!CompatibilityRestrictions.CalculationItem_Ordinal.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						this.body.Ordinal = reader.ReadInt32Property();
						return true;
					}
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
								goto IL_0338;
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
							goto IL_0338;
						}
						this.body.ModifiedTime = reader.ReadDateTimeProperty();
						return true;
						IL_0338:
						this.body.ErrorMessage = reader.ReadStringProperty();
						return true;
					}
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

		// Token: 0x060001B8 RID: 440 RVA: 0x0000CD48 File Offset: 0x0000AF48
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000CD51 File Offset: 0x0000AF51
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000CD74 File Offset: 0x0000AF74
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.CalculationItem.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalculationItem is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
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
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Expression))
			{
				result["expression", TomPropCategory.Regular, 8, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Expression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.Ordinal != -1)
			{
				if (!CompatibilityRestrictions.CalculationItem_Ordinal.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Ordinal is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["ordinal", TomPropCategory.Regular, 9, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.body.Ordinal);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && this.body.FormatStringDefinitionID.Object != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.body.FormatStringDefinitionID.Object)))
			{
				result["formatStringDefinition", TomPropCategory.ChildLink, 2, false] = this.body.FormatStringDefinitionID.Object.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000D050 File Offset: 0x0000B250
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				int length = name.Length;
				switch (length)
				{
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
				case 6:
				case 8:
				case 9:
					break;
				case 7:
					if (name == "ordinal")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.CalculationItem_Ordinal.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.Ordinal = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
						return true;
					}
					break;
				case 10:
					if (name == "expression")
					{
						this.body.Expression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 11:
					if (name == "description")
					{
						this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 12:
				{
					char c = name[0];
					if (c != 'e')
					{
						if (c == 'm')
						{
							if (name == "modifiedTime")
							{
								this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "errorMessage")
					{
						this.body.ErrorMessage = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				default:
					if (length == 22)
					{
						if (name == "formatStringDefinition")
						{
							if (jsonProp.Value.Type != 10)
							{
								FormatStringDefinition formatStringDefinition = new FormatStringDefinition();
								formatStringDefinition.DeserializeFromJsonObject(jsonProp.Value, options, mode, dbCompatibilityLevel);
								this.body.FormatStringDefinitionID.Object = formatStringDefinition;
							}
							return true;
						}
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000D28C File Offset: 0x0000B48C
		internal override string GetFormattedObjectPath()
		{
			CalculationGroup calculationGroup = (CalculationGroup)this.Parent;
			if (calculationGroup == null || calculationGroup.Parent == null)
			{
				return TomSR.ObjectPath_CalculationItem_0Args;
			}
			return TomSR.ObjectPath_CalculationItem_1Args(((Table)calculationGroup.Parent).Name);
		}

		// Token: 0x040000D9 RID: 217
		internal CalculationItem.ObjectBody body;

		// Token: 0x02000239 RID: 569
		internal class ObjectBody : NamedMetadataObjectBody<CalculationItem>
		{
			// Token: 0x06001F21 RID: 7969 RVA: 0x000CE015 File Offset: 0x000CC215
			public ObjectBody(CalculationItem owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.CalculationGroupID = new ParentLink<CalculationItem, CalculationGroup>(owner, "CalculationGroup");
				this.FormatStringDefinitionID = new ChildLink<CalculationItem, FormatStringDefinition>(owner, "FormatStringDefinition");
			}

			// Token: 0x06001F22 RID: 7970 RVA: 0x000CE04B File Offset: 0x000CC24B
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x06001F23 RID: 7971 RVA: 0x000CE054 File Offset: 0x000CC254
			internal bool IsEqualTo(CalculationItem.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage)) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.Ordinal, other.Ordinal) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.CalculationGroupID.IsEqualTo(other.CalculationGroupID, context)) && this.FormatStringDefinitionID.IsEqualTo(other.FormatStringDefinitionID, context);
			}

			// Token: 0x06001F24 RID: 7972 RVA: 0x000CE170 File Offset: 0x000CC370
			internal void CopyFromImpl(CalculationItem.ObjectBody other, CopyContext context)
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
				this.Expression = other.Expression;
				this.Ordinal = other.Ordinal;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.CalculationGroupID.CopyFrom(other.CalculationGroupID, context);
				}
				this.FormatStringDefinitionID.CopyFrom(other.FormatStringDefinitionID, context);
			}

			// Token: 0x06001F25 RID: 7973 RVA: 0x000CE25C File Offset: 0x000CC45C
			internal void CopyFromImpl(CalculationItem.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.ModifiedTime = other.ModifiedTime;
				this.State = other.State;
				this.ErrorMessage = other.ErrorMessage;
				this.Expression = other.Expression;
				this.Ordinal = other.Ordinal;
				this.CalculationGroupID.CopyFrom(other.CalculationGroupID, ObjectChangeTracker.BodyCloneContext);
				this.FormatStringDefinitionID.CopyFrom(other.FormatStringDefinitionID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001F26 RID: 7974 RVA: 0x000CE2E9 File Offset: 0x000CC4E9
			public override void CopyFrom(MetadataObjectBody<CalculationItem> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((CalculationItem.ObjectBody)other, context);
			}

			// Token: 0x06001F27 RID: 7975 RVA: 0x000CE300 File Offset: 0x000CC500
			internal bool IsEqualTo(CalculationItem.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.Ordinal, other.Ordinal) && this.CalculationGroupID.IsEqualTo(other.CalculationGroupID) && this.FormatStringDefinitionID.IsEqualTo(other.FormatStringDefinitionID);
			}

			// Token: 0x06001F28 RID: 7976 RVA: 0x000CE3CB File Offset: 0x000CC5CB
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((CalculationItem.ObjectBody)other);
			}

			// Token: 0x06001F29 RID: 7977 RVA: 0x000CE3E4 File Offset: 0x000CC5E4
			internal void CompareWith(CalculationItem.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
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
				if (!PropertyHelper.AreValuesIdentical(this.Expression, other.Expression))
				{
					context.RegisterPropertyChange(base.Owner, "Expression", typeof(string), PropertyFlags.DdlAndUser, other.Expression, this.Expression);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Ordinal, other.Ordinal))
				{
					context.RegisterPropertyChange(base.Owner, "Ordinal", typeof(int), PropertyFlags.DdlAndUser, other.Ordinal, this.Ordinal);
				}
				this.CalculationGroupID.CompareWith(other.CalculationGroupID, "CalculationGroupID", "CalculationGroup", PropertyFlags.ReadOnly, context);
				this.FormatStringDefinitionID.CompareWith(other.FormatStringDefinitionID, "FormatStringDefinitionID", "FormatStringDefinition", PropertyFlags.None, context);
			}

			// Token: 0x06001F2A RID: 7978 RVA: 0x000CE5F1 File Offset: 0x000CC7F1
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((CalculationItem.ObjectBody)other, context);
			}

			// Token: 0x04000761 RID: 1889
			internal string Name;

			// Token: 0x04000762 RID: 1890
			internal string Description;

			// Token: 0x04000763 RID: 1891
			internal DateTime ModifiedTime;

			// Token: 0x04000764 RID: 1892
			internal ObjectState State;

			// Token: 0x04000765 RID: 1893
			internal string ErrorMessage;

			// Token: 0x04000766 RID: 1894
			internal string Expression;

			// Token: 0x04000767 RID: 1895
			internal int Ordinal;

			// Token: 0x04000768 RID: 1896
			internal ParentLink<CalculationItem, CalculationGroup> CalculationGroupID;

			// Token: 0x04000769 RID: 1897
			internal ChildLink<CalculationItem, FormatStringDefinition> FormatStringDefinitionID;
		}
	}
}
