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
	// Token: 0x020000AD RID: 173
	[CompatibilityRequirement("1450")]
	public abstract class RefreshPolicy : MetadataObject
	{
		// Token: 0x06000A83 RID: 2691 RVA: 0x00056463 File Offset: 0x00054663
		private protected RefreshPolicy()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00056476 File Offset: 0x00054676
		private protected RefreshPolicy(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00056488 File Offset: 0x00054688
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new RefreshPolicy.ObjectBody(this);
			this.body.PolicyType = (RefreshPolicyType)(-1);
			this.body.RollingWindowGranularity = RefreshGranularityType.Invalid;
			this.body.IncrementalGranularity = RefreshGranularityType.Invalid;
			this.body.PollingExpression = string.Empty;
			this.body.SourceExpression = string.Empty;
			this.body.Mode = RefreshPolicyMode.Import;
			this._Annotations = new RefreshPolicyAnnotationCollection(this, comparer);
			this._ExtendedProperties = new RefreshPolicyExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0005650B File Offset: 0x0005470B
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.RefreshPolicy;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0005650F File Offset: 0x0005470F
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x00056521 File Offset: 0x00054721
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
					MetadataObject.UpdateMetadataObjectParent<RefreshPolicy, Table>(this.body.TableID, (Table)value, "RefreshPolicy", CompatibilityRestrictions.Table_RefreshPolicy);
				}
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00056556 File Offset: 0x00054756
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.TableID.ObjectID;
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00056568 File Offset: 0x00054768
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateChoiceScope())
			{
				RefreshPolicy.WriteMetadataSchemaForBasicRefreshPolicy(context, writer);
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000565A0 File Offset: 0x000547A0
		private static void WriteMetadataSchemaForBasicRefreshPolicy(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.RefreshPolicy, "BasicRefreshPolicy", "BasicRefreshPolicy object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("policyType", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RefreshPolicyType>("policyType", MetadataPropertyNature.RegularProperty, null);
				}
				if (CompatibilityRestrictions.RefreshPolicy_Mode.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("mode", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RefreshPolicyMode>("mode", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("rollingWindowGranularity", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RefreshGranularityType>("rollingWindowGranularity", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("rollingWindowPeriods", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("rollingWindowPeriods", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (writer.ShouldIncludeProperty("incrementalGranularity", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RefreshGranularityType>("incrementalGranularity", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("incrementalPeriods", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("incrementalPeriods", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (writer.ShouldIncludeProperty("incrementalPeriodsOffset", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("incrementalPeriodsOffset", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (writer.ShouldIncludeProperty("pollingExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("pollingExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("sourceExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("sourceExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
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

		// Token: 0x06000A8C RID: 2700 RVA: 0x00056788 File Offset: 0x00054988
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.RefreshPolicy[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.PolicyType != (RefreshPolicyType)(-1))
			{
				int num = PropertyHelper.GetRefreshPolicyTypeCompatibilityRestrictions(this.body.PolicyType)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "PolicyType");
					requiredLevel = num;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.RollingWindowGranularity != RefreshGranularityType.Invalid)
			{
				int num2 = PropertyHelper.GetRefreshGranularityTypeCompatibilityRestrictions(this.body.RollingWindowGranularity)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num2, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "RollingWindowGranularity");
					requiredLevel = num2;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.IncrementalGranularity != RefreshGranularityType.Invalid)
			{
				int num3 = PropertyHelper.GetRefreshGranularityTypeCompatibilityRestrictions(this.body.IncrementalGranularity)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num3, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "IncrementalGranularity");
					requiredLevel = num3;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.Mode != RefreshPolicyMode.Import)
			{
				int num4;
				CompatibilityRestrictionSet.MergeLevelDemand(CompatibilityRestrictions.RefreshPolicy_Mode[mode], PropertyHelper.GetRefreshPolicyModeCompatibilityRestrictions(this.body.Mode)[mode], out num4);
				if (CompatibilityRestrictionSet.CompareLevel(num4, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Mode");
					requiredLevel = num4;
					int num5 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00056920 File Offset: 0x00054B20
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x00056928 File Offset: 0x00054B28
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (RefreshPolicy.ObjectBody)value;
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00056936 File Offset: 0x00054B36
		internal override ITxObjectBody CreateBody()
		{
			return new RefreshPolicy.ObjectBody(this);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0005693E File Offset: 0x00054B3E
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return null;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00056944 File Offset: 0x00054B44
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Table table = MetadataObject.ResolveMetadataObjectParentById<RefreshPolicy, Table>(this.body.TableID, objectMap, throwIfCantResolve, "RefreshPolicy", CompatibilityRestrictions.Table_RefreshPolicy);
			if (table != null && table.Model != null)
			{
				foreach (MetadataObject metadataObject in base.GetChildren(false))
				{
					table.Model.NotifySubtreeAdded(metadataObject);
				}
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000569C0 File Offset: 0x00054BC0
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000569C2 File Offset: 0x00054BC2
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x000569D2 File Offset: 0x00054BD2
		public RefreshPolicyAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x000569DA File Offset: 0x00054BDA
		public RefreshPolicyExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x000569E2 File Offset: 0x00054BE2
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x000569F0 File Offset: 0x00054BF0
		public RefreshPolicyType PolicyType
		{
			get
			{
				return this.body.PolicyType;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.PolicyType, value))
				{
					CompatibilityRestrictionSet refreshPolicyTypeCompatibilityRestrictions = PropertyHelper.GetRefreshPolicyTypeCompatibilityRestrictions(value);
					CompatibilityRestrictionSet refreshPolicyTypeCompatibilityRestrictions2 = PropertyHelper.GetRefreshPolicyTypeCompatibilityRestrictions(this.body.PolicyType);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = refreshPolicyTypeCompatibilityRestrictions.Compare(refreshPolicyTypeCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != (RefreshPolicyType)(-1)))
					{
						array = base.ValidateCompatibilityRequirement(refreshPolicyTypeCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "PolicyType", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "PolicyType", typeof(RefreshPolicyType), this.body.PolicyType, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(refreshPolicyTypeCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(refreshPolicyTypeCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(refreshPolicyTypeCompatibilityRestrictions, array);
						break;
					}
					RefreshPolicyType policyType = this.body.PolicyType;
					this.body.PolicyType = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "PolicyType", typeof(RefreshPolicyType), policyType, value);
				}
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00056B12 File Offset: 0x00054D12
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x00056B20 File Offset: 0x00054D20
		internal RefreshGranularityType RollingWindowGranularity
		{
			get
			{
				return this.body.RollingWindowGranularity;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RollingWindowGranularity, value))
				{
					CompatibilityRestrictionSet refreshGranularityTypeCompatibilityRestrictions = PropertyHelper.GetRefreshGranularityTypeCompatibilityRestrictions(value);
					CompatibilityRestrictionSet refreshGranularityTypeCompatibilityRestrictions2 = PropertyHelper.GetRefreshGranularityTypeCompatibilityRestrictions(this.body.RollingWindowGranularity);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = refreshGranularityTypeCompatibilityRestrictions.Compare(refreshGranularityTypeCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != RefreshGranularityType.Invalid))
					{
						array = base.ValidateCompatibilityRequirement(refreshGranularityTypeCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "RollingWindowGranularity", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "RollingWindowGranularity", typeof(RefreshGranularityType), this.body.RollingWindowGranularity, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(refreshGranularityTypeCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(refreshGranularityTypeCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(refreshGranularityTypeCompatibilityRestrictions, array);
						break;
					}
					RefreshGranularityType rollingWindowGranularity = this.body.RollingWindowGranularity;
					this.body.RollingWindowGranularity = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RollingWindowGranularity", typeof(RefreshGranularityType), rollingWindowGranularity, value);
				}
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00056C42 File Offset: 0x00054E42
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x00056C50 File Offset: 0x00054E50
		internal int RollingWindowPeriods
		{
			get
			{
				return this.body.RollingWindowPeriods;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RollingWindowPeriods, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "RollingWindowPeriods", typeof(int), this.body.RollingWindowPeriods, value);
					int rollingWindowPeriods = this.body.RollingWindowPeriods;
					this.body.RollingWindowPeriods = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RollingWindowPeriods", typeof(int), rollingWindowPeriods, value);
				}
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00056CD4 File Offset: 0x00054ED4
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x00056CE4 File Offset: 0x00054EE4
		internal RefreshGranularityType IncrementalGranularity
		{
			get
			{
				return this.body.IncrementalGranularity;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IncrementalGranularity, value))
				{
					CompatibilityRestrictionSet refreshGranularityTypeCompatibilityRestrictions = PropertyHelper.GetRefreshGranularityTypeCompatibilityRestrictions(value);
					CompatibilityRestrictionSet refreshGranularityTypeCompatibilityRestrictions2 = PropertyHelper.GetRefreshGranularityTypeCompatibilityRestrictions(this.body.IncrementalGranularity);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = refreshGranularityTypeCompatibilityRestrictions.Compare(refreshGranularityTypeCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != RefreshGranularityType.Invalid))
					{
						array = base.ValidateCompatibilityRequirement(refreshGranularityTypeCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "IncrementalGranularity", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "IncrementalGranularity", typeof(RefreshGranularityType), this.body.IncrementalGranularity, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(refreshGranularityTypeCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(refreshGranularityTypeCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(refreshGranularityTypeCompatibilityRestrictions, array);
						break;
					}
					RefreshGranularityType incrementalGranularity = this.body.IncrementalGranularity;
					this.body.IncrementalGranularity = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IncrementalGranularity", typeof(RefreshGranularityType), incrementalGranularity, value);
				}
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00056E06 File Offset: 0x00055006
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x00056E14 File Offset: 0x00055014
		internal int IncrementalPeriods
		{
			get
			{
				return this.body.IncrementalPeriods;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IncrementalPeriods, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IncrementalPeriods", typeof(int), this.body.IncrementalPeriods, value);
					int incrementalPeriods = this.body.IncrementalPeriods;
					this.body.IncrementalPeriods = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IncrementalPeriods", typeof(int), incrementalPeriods, value);
				}
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00056E98 File Offset: 0x00055098
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x00056EA8 File Offset: 0x000550A8
		internal int IncrementalPeriodsOffset
		{
			get
			{
				return this.body.IncrementalPeriodsOffset;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IncrementalPeriodsOffset, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IncrementalPeriodsOffset", typeof(int), this.body.IncrementalPeriodsOffset, value);
					int incrementalPeriodsOffset = this.body.IncrementalPeriodsOffset;
					this.body.IncrementalPeriodsOffset = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IncrementalPeriodsOffset", typeof(int), incrementalPeriodsOffset, value);
				}
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00056F2C File Offset: 0x0005512C
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x00056F3C File Offset: 0x0005513C
		internal string PollingExpression
		{
			get
			{
				return this.body.PollingExpression;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.PollingExpression, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "PollingExpression", typeof(string), this.body.PollingExpression, value);
					string pollingExpression = this.body.PollingExpression;
					this.body.PollingExpression = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "PollingExpression", typeof(string), pollingExpression, value);
				}
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00056FAC File Offset: 0x000551AC
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x00056FBC File Offset: 0x000551BC
		internal string SourceExpression
		{
			get
			{
				return this.body.SourceExpression;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SourceExpression, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "SourceExpression", typeof(string), this.body.SourceExpression, value);
					string sourceExpression = this.body.SourceExpression;
					this.body.SourceExpression = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SourceExpression", typeof(string), sourceExpression, value);
				}
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0005702C File Offset: 0x0005522C
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x0005703C File Offset: 0x0005523C
		[CompatibilityRequirement("1565")]
		public RefreshPolicyMode Mode
		{
			get
			{
				return this.body.Mode;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Mode, value))
				{
					CompatibilityRestrictionSet compatibilityRestrictionSet = CompatibilityRestrictions.RefreshPolicy_Mode.Merge(PropertyHelper.GetRefreshPolicyModeCompatibilityRestrictions(value));
					CompatibilityRestrictionSet compatibilityRestrictionSet2 = CompatibilityRestrictions.RefreshPolicy_Mode.Merge(PropertyHelper.GetRefreshPolicyModeCompatibilityRestrictions(this.body.Mode));
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = compatibilityRestrictionSet.Compare(compatibilityRestrictionSet2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != RefreshPolicyMode.Import))
					{
						array = base.ValidateCompatibilityRequirement(compatibilityRestrictionSet, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "Mode", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Mode", typeof(RefreshPolicyMode), this.body.Mode, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						break;
					}
					RefreshPolicyMode mode = this.body.Mode;
					this.body.Mode = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Mode", typeof(RefreshPolicyMode), mode, value);
				}
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00057171 File Offset: 0x00055371
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x00057184 File Offset: 0x00055384
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

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00057208 File Offset: 0x00055408
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x0005721A File Offset: 0x0005541A
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

		// Token: 0x06000AAC RID: 2732 RVA: 0x00057230 File Offset: 0x00055430
		internal void CopyFrom(RefreshPolicy other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions || !this.body.IsEqualTo(other.body, context))
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

		// Token: 0x06000AAD RID: 2733 RVA: 0x000572B5 File Offset: 0x000554B5
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((RefreshPolicy)other, context);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000572C4 File Offset: 0x000554C4
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(RefreshPolicy other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000572E0 File Offset: 0x000554E0
		public void CopyTo(RefreshPolicy other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000572FC File Offset: 0x000554FC
		public RefreshPolicy Clone()
		{
			return base.CloneInternal<RefreshPolicy>();
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00057304 File Offset: 0x00055504
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.RefreshPolicy.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object RefreshPolicy is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.TableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "TableID", this.body.TableID.Object);
			}
			if (this.body.PolicyType != (RefreshPolicyType)(-1))
			{
				if (!PropertyHelper.IsRefreshPolicyTypeValueCompatible(this.body.PolicyType, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member PolicyType is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<RefreshPolicyType>(options, "PolicyType", this.body.PolicyType);
			}
			if (this.body.RollingWindowGranularity != RefreshGranularityType.Invalid)
			{
				if (!PropertyHelper.IsRefreshGranularityTypeValueCompatible(this.body.RollingWindowGranularity, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RollingWindowGranularity is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<RefreshGranularityType>(options, "RollingWindowGranularity", this.body.RollingWindowGranularity);
			}
			if (this.body.RollingWindowPeriods != 0)
			{
				writer.WriteProperty<int>(options, "RollingWindowPeriods", this.body.RollingWindowPeriods);
			}
			if (this.body.IncrementalGranularity != RefreshGranularityType.Invalid)
			{
				if (!PropertyHelper.IsRefreshGranularityTypeValueCompatible(this.body.IncrementalGranularity, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member IncrementalGranularity is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<RefreshGranularityType>(options, "IncrementalGranularity", this.body.IncrementalGranularity);
			}
			if (this.body.IncrementalPeriods != 0)
			{
				writer.WriteProperty<int>(options, "IncrementalPeriods", this.body.IncrementalPeriods);
			}
			if (this.body.IncrementalPeriodsOffset != 0)
			{
				writer.WriteProperty<int>(options, "IncrementalPeriodsOffset", this.body.IncrementalPeriodsOffset);
			}
			if (!string.IsNullOrEmpty(this.body.PollingExpression))
			{
				writer.WriteProperty<string>(options, "PollingExpression", this.body.PollingExpression);
			}
			if (!string.IsNullOrEmpty(this.body.SourceExpression))
			{
				writer.WriteProperty<string>(options, "SourceExpression", this.body.SourceExpression);
			}
			if (this.body.Mode != RefreshPolicyMode.Import)
			{
				if (!CompatibilityRestrictions.RefreshPolicy_Mode.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsRefreshPolicyModeValueCompatible(this.body.Mode, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Mode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<RefreshPolicyMode>(options, "Mode", this.body.Mode);
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000575C4 File Offset: 0x000557C4
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("TableID", out objectId))
			{
				this.body.TableID.ObjectID = objectId;
			}
			RefreshPolicyType refreshPolicyType;
			if (reader.TryReadProperty<RefreshPolicyType>("PolicyType", out refreshPolicyType))
			{
				this.body.PolicyType = refreshPolicyType;
			}
			RefreshGranularityType refreshGranularityType;
			if (reader.TryReadProperty<RefreshGranularityType>("RollingWindowGranularity", out refreshGranularityType))
			{
				this.body.RollingWindowGranularity = refreshGranularityType;
			}
			int num;
			if (reader.TryReadProperty<int>("RollingWindowPeriods", out num))
			{
				this.body.RollingWindowPeriods = num;
			}
			RefreshGranularityType refreshGranularityType2;
			if (reader.TryReadProperty<RefreshGranularityType>("IncrementalGranularity", out refreshGranularityType2))
			{
				this.body.IncrementalGranularity = refreshGranularityType2;
			}
			int num2;
			if (reader.TryReadProperty<int>("IncrementalPeriods", out num2))
			{
				this.body.IncrementalPeriods = num2;
			}
			int num3;
			if (reader.TryReadProperty<int>("IncrementalPeriodsOffset", out num3))
			{
				this.body.IncrementalPeriodsOffset = num3;
			}
			string text;
			if (reader.TryReadProperty<string>("PollingExpression", out text))
			{
				this.body.PollingExpression = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("SourceExpression", out text2))
			{
				this.body.SourceExpression = text2;
			}
			RefreshPolicyMode refreshPolicyMode;
			if (CompatibilityRestrictions.RefreshPolicy_Mode.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<RefreshPolicyMode>("Mode", out refreshPolicyMode))
			{
				this.body.Mode = refreshPolicyMode;
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00057704 File Offset: 0x00055904
		private protected sealed override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.RefreshPolicy.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object RefreshPolicy is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.TableID.Object != null && writer.ShouldIncludeProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.TableID.Object);
			}
			if (this.body.PolicyType != (RefreshPolicyType)(-1))
			{
				if (!PropertyHelper.IsRefreshPolicyTypeValueCompatible(this.body.PolicyType, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member PolicyType is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("PolicyType", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RefreshPolicyType>("PolicyType", MetadataPropertyNature.RegularProperty, this.body.PolicyType);
				}
			}
			if (this.body.RollingWindowGranularity != RefreshGranularityType.Invalid)
			{
				if (!PropertyHelper.IsRefreshGranularityTypeValueCompatible(this.body.RollingWindowGranularity, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RollingWindowGranularity is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("RollingWindowGranularity", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RefreshGranularityType>("RollingWindowGranularity", MetadataPropertyNature.RegularProperty, this.body.RollingWindowGranularity);
				}
			}
			if (this.body.RollingWindowPeriods != 0 && writer.ShouldIncludeProperty("RollingWindowPeriods", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("RollingWindowPeriods", MetadataPropertyNature.RegularProperty, this.body.RollingWindowPeriods);
			}
			if (this.body.IncrementalGranularity != RefreshGranularityType.Invalid)
			{
				if (!PropertyHelper.IsRefreshGranularityTypeValueCompatible(this.body.IncrementalGranularity, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member IncrementalGranularity is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("IncrementalGranularity", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RefreshGranularityType>("IncrementalGranularity", MetadataPropertyNature.RegularProperty, this.body.IncrementalGranularity);
				}
			}
			if (this.body.IncrementalPeriods != 0 && writer.ShouldIncludeProperty("IncrementalPeriods", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("IncrementalPeriods", MetadataPropertyNature.RegularProperty, this.body.IncrementalPeriods);
			}
			if (this.body.IncrementalPeriodsOffset != 0 && writer.ShouldIncludeProperty("IncrementalPeriodsOffset", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("IncrementalPeriodsOffset", MetadataPropertyNature.RegularProperty, this.body.IncrementalPeriodsOffset);
			}
			if (!string.IsNullOrEmpty(this.body.PollingExpression) && writer.ShouldIncludeProperty("PollingExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("PollingExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.PollingExpression);
			}
			if (!string.IsNullOrEmpty(this.body.SourceExpression) && writer.ShouldIncludeProperty("SourceExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("SourceExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.SourceExpression);
			}
			if (this.body.Mode != RefreshPolicyMode.Import)
			{
				if (!CompatibilityRestrictions.RefreshPolicy_Mode.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsRefreshPolicyModeValueCompatible(this.body.Mode, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Mode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("Mode", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RefreshPolicyMode>("Mode", MetadataPropertyNature.RegularProperty, this.body.Mode);
				}
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00057AC8 File Offset: 0x00055CC8
		private protected virtual void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (this.body.PolicyType != (RefreshPolicyType)(-1))
			{
				if (!PropertyHelper.IsRefreshPolicyTypeValueCompatible(this.body.PolicyType, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member PolicyType is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("policyType", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RefreshPolicyType>("policyType", MetadataPropertyNature.RegularProperty, this.body.PolicyType);
				}
			}
			if (this.body.Mode != RefreshPolicyMode.Import)
			{
				if (!CompatibilityRestrictions.RefreshPolicy_Mode.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsRefreshPolicyModeValueCompatible(this.body.Mode, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Mode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("mode", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RefreshPolicyMode>("mode", MetadataPropertyNature.RegularProperty, this.body.Mode);
				}
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00057BEC File Offset: 0x00055DEC
		private protected sealed override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.RefreshPolicy.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object RefreshPolicy is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			this.WriteRegularPropertiesToMetadataStream(context, writer);
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

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00057CF4 File Offset: 0x00055EF4
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
				if (length != 4)
				{
					switch (length)
					{
					case 7:
						if (propertyName == "TableID")
						{
							this.body.TableID.ObjectID = reader.ReadObjectIdProperty();
							return true;
						}
						break;
					case 8:
					case 9:
						break;
					case 10:
					{
						char c = propertyName[0];
						if (c != 'P')
						{
							if (c != 'p')
							{
								break;
							}
							if (!(propertyName == "policyType"))
							{
								break;
							}
						}
						else if (!(propertyName == "PolicyType"))
						{
							break;
						}
						if (!CompatibilityRestrictions.RefreshPolicyType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						this.body.PolicyType = reader.ReadEnumProperty<RefreshPolicyType>();
						return true;
					}
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
					default:
						switch (length)
						{
						case 16:
							if (propertyName == "SourceExpression")
							{
								this.body.SourceExpression = reader.ReadStringProperty();
								return true;
							}
							break;
						case 17:
							if (propertyName == "PollingExpression")
							{
								this.body.PollingExpression = reader.ReadStringProperty();
								return true;
							}
							break;
						case 18:
						{
							char c = propertyName[0];
							if (c != 'I')
							{
								if (c == 'e')
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
							}
							else if (propertyName == "IncrementalPeriods")
							{
								this.body.IncrementalPeriods = reader.ReadInt32Property();
								return true;
							}
							break;
						}
						case 20:
							if (propertyName == "RollingWindowPeriods")
							{
								this.body.RollingWindowPeriods = reader.ReadInt32Property();
								return true;
							}
							break;
						case 22:
							if (propertyName == "IncrementalGranularity")
							{
								if (!CompatibilityRestrictions.RefreshGranularityType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
								{
									classification = UnexpectedPropertyClassification.IncompatibleProperty;
									return false;
								}
								this.body.IncrementalGranularity = reader.ReadEnumProperty<RefreshGranularityType>();
								return true;
							}
							break;
						case 24:
						{
							char c = propertyName[0];
							if (c != 'I')
							{
								if (c == 'R')
								{
									if (propertyName == "RollingWindowGranularity")
									{
										if (!CompatibilityRestrictions.RefreshGranularityType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
										{
											classification = UnexpectedPropertyClassification.IncompatibleProperty;
											return false;
										}
										this.body.RollingWindowGranularity = reader.ReadEnumProperty<RefreshGranularityType>();
										return true;
									}
								}
							}
							else if (propertyName == "IncrementalPeriodsOffset")
							{
								this.body.IncrementalPeriodsOffset = reader.ReadInt32Property();
								return true;
							}
							break;
						}
						}
						break;
					}
				}
				else
				{
					char c = propertyName[0];
					if (c != 'M')
					{
						if (c != 'm')
						{
							return false;
						}
						if (!(propertyName == "mode"))
						{
							return false;
						}
					}
					else if (!(propertyName == "Mode"))
					{
						return false;
					}
					if (!CompatibilityRestrictions.RefreshPolicy_Mode.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !CompatibilityRestrictions.RefreshPolicyMode.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.Mode = reader.ReadEnumProperty<RefreshPolicyMode>();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000581CC File Offset: 0x000563CC
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.RefreshPolicy.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object RefreshPolicy is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.PolicyType != (RefreshPolicyType)(-1))
			{
				if (!PropertyHelper.IsRefreshPolicyTypeValueCompatible(this.body.PolicyType, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member PolicyType is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["policyType", TomPropCategory.Regular, 2, false] = JsonPropertyHelper.ConvertEnumToJsonValue<RefreshPolicyType>(this.body.PolicyType);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.Mode != RefreshPolicyMode.Import)
			{
				if (!CompatibilityRestrictions.RefreshPolicy_Mode.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsRefreshPolicyModeValueCompatible(this.body.Mode, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Mode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["mode", TomPropCategory.Regular, 10, false] = JsonPropertyHelper.ConvertEnumToJsonValue<RefreshPolicyMode>(this.body.Mode);
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

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000584A8 File Offset: 0x000566A8
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (!(name == "policyType"))
			{
				if (!(name == "mode"))
				{
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
					RefreshPolicyMode refreshPolicyMode = JsonPropertyHelper.ConvertJsonValueToEnum<RefreshPolicyMode>(jsonProp.Value);
					if (jsonProp.Value.Type != 10 && (!CompatibilityRestrictions.RefreshPolicy_Mode.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsRefreshPolicyModeValueCompatible(refreshPolicyMode, mode, dbCompatibilityLevel)))
					{
						return false;
					}
					this.body.Mode = refreshPolicyMode;
					return true;
				}
			}
			else
			{
				RefreshPolicyType refreshPolicyType = JsonPropertyHelper.ConvertJsonValueToEnum<RefreshPolicyType>(jsonProp.Value);
				if (jsonProp.Value.Type != 10 && !PropertyHelper.IsRefreshPolicyTypeValueCompatible(refreshPolicyType, mode, dbCompatibilityLevel))
				{
					return false;
				}
				this.body.PolicyType = refreshPolicyType;
				return true;
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000585C4 File Offset: 0x000567C4
		internal static RefreshPolicy CreateFromMetadataStream(SerializationActivityContext context, IMetadataReader reader)
		{
			RefreshPolicyType refreshPolicyType;
			if (reader.TryMoveToProperty("policyType"))
			{
				refreshPolicyType = reader.ReadEnumProperty<RefreshPolicyType>();
			}
			else
			{
				refreshPolicyType = RefreshPolicyType.Basic;
			}
			reader.Reset();
			if (refreshPolicyType == RefreshPolicyType.Basic)
			{
				return new BasicRefreshPolicy();
			}
			throw reader.CreateInvalidDataException(context, TomSR.Exception_UnrecognizedValueOfType("PolicyType", refreshPolicyType.ToString()), null);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00058617 File Offset: 0x00056817
		internal override string GetFormattedObjectPath()
		{
			if (this.Table != null)
			{
				return TomSR.ObjectPath_RefreshPolicy_1Args_Table(this.Table.Name);
			}
			return TomSR.ObjectPath_RefreshPolicy_0Args;
		}

		// Token: 0x04000165 RID: 357
		internal RefreshPolicy.ObjectBody body;

		// Token: 0x04000166 RID: 358
		private RefreshPolicyAnnotationCollection _Annotations;

		// Token: 0x04000167 RID: 359
		private RefreshPolicyExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x020002BE RID: 702
		internal class ObjectBody : MetadataObjectBody<RefreshPolicy>
		{
			// Token: 0x060022A6 RID: 8870 RVA: 0x000DE0BB File Offset: 0x000DC2BB
			public ObjectBody(RefreshPolicy owner)
				: base(owner)
			{
				this.TableID = new ParentLink<RefreshPolicy, Table>(owner, "Table");
			}

			// Token: 0x060022A7 RID: 8871 RVA: 0x000DE0D8 File Offset: 0x000DC2D8
			internal bool IsEqualTo(RefreshPolicy.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.PolicyType, other.PolicyType) && PropertyHelper.AreValuesIdentical(this.RollingWindowGranularity, other.RollingWindowGranularity) && PropertyHelper.AreValuesIdentical(this.RollingWindowPeriods, other.RollingWindowPeriods) && PropertyHelper.AreValuesIdentical(this.IncrementalGranularity, other.IncrementalGranularity) && PropertyHelper.AreValuesIdentical(this.IncrementalPeriods, other.IncrementalPeriods) && PropertyHelper.AreValuesIdentical(this.IncrementalPeriodsOffset, other.IncrementalPeriodsOffset) && PropertyHelper.AreValuesIdentical(this.PollingExpression, other.PollingExpression) && PropertyHelper.AreValuesIdentical(this.SourceExpression, other.SourceExpression) && PropertyHelper.AreValuesIdentical(this.Mode, other.Mode) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.TableID.IsEqualTo(other.TableID, context));
			}

			// Token: 0x060022A8 RID: 8872 RVA: 0x000DE1CC File Offset: 0x000DC3CC
			internal void CopyFromImpl(RefreshPolicy.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.PolicyType = other.PolicyType;
				this.RollingWindowGranularity = other.RollingWindowGranularity;
				this.RollingWindowPeriods = other.RollingWindowPeriods;
				this.IncrementalGranularity = other.IncrementalGranularity;
				this.IncrementalPeriods = other.IncrementalPeriods;
				this.IncrementalPeriodsOffset = other.IncrementalPeriodsOffset;
				this.PollingExpression = other.PollingExpression;
				this.SourceExpression = other.SourceExpression;
				this.Mode = other.Mode;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.TableID.CopyFrom(other.TableID, context);
				}
			}

			// Token: 0x060022A9 RID: 8873 RVA: 0x000DE280 File Offset: 0x000DC480
			internal void CopyFromImpl(RefreshPolicy.ObjectBody other)
			{
				this.PolicyType = other.PolicyType;
				this.RollingWindowGranularity = other.RollingWindowGranularity;
				this.RollingWindowPeriods = other.RollingWindowPeriods;
				this.IncrementalGranularity = other.IncrementalGranularity;
				this.IncrementalPeriods = other.IncrementalPeriods;
				this.IncrementalPeriodsOffset = other.IncrementalPeriodsOffset;
				this.PollingExpression = other.PollingExpression;
				this.SourceExpression = other.SourceExpression;
				this.Mode = other.Mode;
				this.TableID.CopyFrom(other.TableID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x060022AA RID: 8874 RVA: 0x000DE30F File Offset: 0x000DC50F
			public override void CopyFrom(MetadataObjectBody<RefreshPolicy> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((RefreshPolicy.ObjectBody)other, context);
			}

			// Token: 0x060022AB RID: 8875 RVA: 0x000DE328 File Offset: 0x000DC528
			internal bool IsEqualTo(RefreshPolicy.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.PolicyType, other.PolicyType) && PropertyHelper.AreValuesIdentical(this.RollingWindowGranularity, other.RollingWindowGranularity) && PropertyHelper.AreValuesIdentical(this.RollingWindowPeriods, other.RollingWindowPeriods) && PropertyHelper.AreValuesIdentical(this.IncrementalGranularity, other.IncrementalGranularity) && PropertyHelper.AreValuesIdentical(this.IncrementalPeriods, other.IncrementalPeriods) && PropertyHelper.AreValuesIdentical(this.IncrementalPeriodsOffset, other.IncrementalPeriodsOffset) && PropertyHelper.AreValuesIdentical(this.PollingExpression, other.PollingExpression) && PropertyHelper.AreValuesIdentical(this.SourceExpression, other.SourceExpression) && PropertyHelper.AreValuesIdentical(this.Mode, other.Mode) && this.TableID.IsEqualTo(other.TableID);
			}

			// Token: 0x060022AC RID: 8876 RVA: 0x000DE408 File Offset: 0x000DC608
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((RefreshPolicy.ObjectBody)other);
			}

			// Token: 0x060022AD RID: 8877 RVA: 0x000DE424 File Offset: 0x000DC624
			internal void CompareWith(RefreshPolicy.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.PolicyType, other.PolicyType))
				{
					context.RegisterPropertyChange(base.Owner, "PolicyType", typeof(RefreshPolicyType), PropertyFlags.DdlAndUser, other.PolicyType, this.PolicyType);
				}
				if (!PropertyHelper.AreValuesIdentical(this.RollingWindowGranularity, other.RollingWindowGranularity))
				{
					context.RegisterPropertyChange(base.Owner, "RollingWindowGranularity", typeof(RefreshGranularityType), PropertyFlags.DdlAndUser, other.RollingWindowGranularity, this.RollingWindowGranularity);
				}
				if (!PropertyHelper.AreValuesIdentical(this.RollingWindowPeriods, other.RollingWindowPeriods))
				{
					context.RegisterPropertyChange(base.Owner, "RollingWindowPeriods", typeof(int), PropertyFlags.DdlAndUser, other.RollingWindowPeriods, this.RollingWindowPeriods);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IncrementalGranularity, other.IncrementalGranularity))
				{
					context.RegisterPropertyChange(base.Owner, "IncrementalGranularity", typeof(RefreshGranularityType), PropertyFlags.DdlAndUser, other.IncrementalGranularity, this.IncrementalGranularity);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IncrementalPeriods, other.IncrementalPeriods))
				{
					context.RegisterPropertyChange(base.Owner, "IncrementalPeriods", typeof(int), PropertyFlags.DdlAndUser, other.IncrementalPeriods, this.IncrementalPeriods);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IncrementalPeriodsOffset, other.IncrementalPeriodsOffset))
				{
					context.RegisterPropertyChange(base.Owner, "IncrementalPeriodsOffset", typeof(int), PropertyFlags.DdlAndUser, other.IncrementalPeriodsOffset, this.IncrementalPeriodsOffset);
				}
				if (!PropertyHelper.AreValuesIdentical(this.PollingExpression, other.PollingExpression))
				{
					context.RegisterPropertyChange(base.Owner, "PollingExpression", typeof(string), PropertyFlags.DdlAndUser, other.PollingExpression, this.PollingExpression);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SourceExpression, other.SourceExpression))
				{
					context.RegisterPropertyChange(base.Owner, "SourceExpression", typeof(string), PropertyFlags.DdlAndUser, other.SourceExpression, this.SourceExpression);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Mode, other.Mode))
				{
					context.RegisterPropertyChange(base.Owner, "Mode", typeof(RefreshPolicyMode), PropertyFlags.DdlAndUser, other.Mode, this.Mode);
				}
				this.TableID.CompareWith(other.TableID, "TableID", "Table", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x060022AE RID: 8878 RVA: 0x000DE6A7 File Offset: 0x000DC8A7
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((RefreshPolicy.ObjectBody)other, context);
			}

			// Token: 0x040009EF RID: 2543
			internal RefreshPolicyType PolicyType;

			// Token: 0x040009F0 RID: 2544
			internal RefreshGranularityType RollingWindowGranularity;

			// Token: 0x040009F1 RID: 2545
			internal int RollingWindowPeriods;

			// Token: 0x040009F2 RID: 2546
			internal RefreshGranularityType IncrementalGranularity;

			// Token: 0x040009F3 RID: 2547
			internal int IncrementalPeriods;

			// Token: 0x040009F4 RID: 2548
			internal int IncrementalPeriodsOffset;

			// Token: 0x040009F5 RID: 2549
			internal string PollingExpression;

			// Token: 0x040009F6 RID: 2550
			internal string SourceExpression;

			// Token: 0x040009F7 RID: 2551
			internal RefreshPolicyMode Mode;

			// Token: 0x040009F8 RID: 2552
			internal ParentLink<RefreshPolicy, Table> TableID;
		}
	}
}
