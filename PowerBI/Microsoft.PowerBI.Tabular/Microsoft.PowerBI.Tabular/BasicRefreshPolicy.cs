using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000030 RID: 48
	[CompatibilityRequirement("1450")]
	public class BasicRefreshPolicy : RefreshPolicy
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00005DC5 File Offset: 0x00003FC5
		public BasicRefreshPolicy()
		{
			this.OnAfterConstructor();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005DD3 File Offset: 0x00003FD3
		internal BasicRefreshPolicy(IEqualityComparer<string> comparer)
			: base(comparer)
		{
			this.OnAfterConstructor();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005DE2 File Offset: 0x00003FE2
		private void OnAfterConstructor()
		{
			this.body.PolicyType = RefreshPolicyType.Basic;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005DF0 File Offset: 0x00003FF0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.RollingWindowGranularity != RefreshGranularityType.Invalid)
			{
				int num = PropertyHelper.GetRefreshGranularityTypeCompatibilityRestrictions(this.body.RollingWindowGranularity)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "RollingWindowGranularity");
					requiredLevel = num;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.IncrementalGranularity != RefreshGranularityType.Invalid)
			{
				int num2 = PropertyHelper.GetRefreshGranularityTypeCompatibilityRestrictions(this.body.IncrementalGranularity)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num2, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "IncrementalGranularity");
					requiredLevel = num2;
					int num3 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005EA5 File Offset: 0x000040A5
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new BasicRefreshPolicy();
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00005EAC File Offset: 0x000040AC
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00005EB4 File Offset: 0x000040B4
		public new RefreshGranularityType RollingWindowGranularity
		{
			get
			{
				return base.RollingWindowGranularity;
			}
			set
			{
				base.RollingWindowGranularity = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00005EBD File Offset: 0x000040BD
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00005EC5 File Offset: 0x000040C5
		public new int RollingWindowPeriods
		{
			get
			{
				return base.RollingWindowPeriods;
			}
			set
			{
				base.RollingWindowPeriods = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00005ECE File Offset: 0x000040CE
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00005ED6 File Offset: 0x000040D6
		public new RefreshGranularityType IncrementalGranularity
		{
			get
			{
				return base.IncrementalGranularity;
			}
			set
			{
				base.IncrementalGranularity = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00005EDF File Offset: 0x000040DF
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00005EE7 File Offset: 0x000040E7
		public new int IncrementalPeriods
		{
			get
			{
				return base.IncrementalPeriods;
			}
			set
			{
				base.IncrementalPeriods = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00005EF0 File Offset: 0x000040F0
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00005EF8 File Offset: 0x000040F8
		public new int IncrementalPeriodsOffset
		{
			get
			{
				return base.IncrementalPeriodsOffset;
			}
			set
			{
				base.IncrementalPeriodsOffset = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00005F01 File Offset: 0x00004101
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00005F09 File Offset: 0x00004109
		public new string PollingExpression
		{
			get
			{
				return base.PollingExpression;
			}
			set
			{
				base.PollingExpression = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005F12 File Offset: 0x00004112
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00005F1A File Offset: 0x0000411A
		public new string SourceExpression
		{
			get
			{
				return base.SourceExpression;
			}
			set
			{
				base.SourceExpression = value;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005F24 File Offset: 0x00004124
		private protected override void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteRegularPropertiesToMetadataStream(context, writer);
			if (this.body.RollingWindowGranularity != RefreshGranularityType.Invalid)
			{
				if (!PropertyHelper.IsRefreshGranularityTypeValueCompatible(this.body.RollingWindowGranularity, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property RollingWindowGranularity is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("rollingWindowGranularity", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RefreshGranularityType>("rollingWindowGranularity", MetadataPropertyNature.RegularProperty, this.body.RollingWindowGranularity);
				}
			}
			if (this.body.RollingWindowPeriods != 0 && writer.ShouldIncludeProperty("rollingWindowPeriods", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("rollingWindowPeriods", MetadataPropertyNature.RegularProperty, this.body.RollingWindowPeriods);
			}
			if (this.body.IncrementalGranularity != RefreshGranularityType.Invalid)
			{
				if (!PropertyHelper.IsRefreshGranularityTypeValueCompatible(this.body.IncrementalGranularity, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property IncrementalGranularity is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("incrementalGranularity", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RefreshGranularityType>("incrementalGranularity", MetadataPropertyNature.RegularProperty, this.body.IncrementalGranularity);
				}
			}
			if (this.body.IncrementalPeriods != 0 && writer.ShouldIncludeProperty("incrementalPeriods", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("incrementalPeriods", MetadataPropertyNature.RegularProperty, this.body.IncrementalPeriods);
			}
			if (this.body.IncrementalPeriodsOffset != 0 && writer.ShouldIncludeProperty("incrementalPeriodsOffset", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("incrementalPeriodsOffset", MetadataPropertyNature.RegularProperty, this.body.IncrementalPeriodsOffset);
			}
			if (!string.IsNullOrEmpty(this.body.PollingExpression) && writer.ShouldIncludeProperty("pollingExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("pollingExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.PollingExpression);
			}
			if (!string.IsNullOrEmpty(this.body.SourceExpression) && writer.ShouldIncludeProperty("sourceExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("sourceExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.SourceExpression);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000614C File Offset: 0x0000434C
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			if (classification != UnexpectedPropertyClassification.Unclassified)
			{
				return false;
			}
			string propertyName = reader.PropertyName;
			if (propertyName != null)
			{
				switch (propertyName.Length)
				{
				case 16:
					if (propertyName == "sourceExpression")
					{
						this.body.SourceExpression = reader.ReadStringProperty();
						return true;
					}
					break;
				case 17:
					if (propertyName == "pollingExpression")
					{
						this.body.PollingExpression = reader.ReadStringProperty();
						return true;
					}
					break;
				case 18:
					if (propertyName == "incrementalPeriods")
					{
						this.body.IncrementalPeriods = reader.ReadInt32Property();
						return true;
					}
					break;
				case 20:
					if (propertyName == "rollingWindowPeriods")
					{
						this.body.RollingWindowPeriods = reader.ReadInt32Property();
						return true;
					}
					break;
				case 22:
					if (propertyName == "incrementalGranularity")
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
					if (c != 'i')
					{
						if (c == 'r')
						{
							if (propertyName == "rollingWindowGranularity")
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
					else if (propertyName == "incrementalPeriodsOffset")
					{
						this.body.IncrementalPeriodsOffset = reader.ReadInt32Property();
						return true;
					}
					break;
				}
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006320 File Offset: 0x00004520
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.SerializeToJsonObject(result, options, mode, dbCompatibilityLevel);
			if (options.IncludeTranslatablePropertiesOnly)
			{
				return;
			}
			if (this.body.RollingWindowGranularity != RefreshGranularityType.Invalid)
			{
				if (!PropertyHelper.IsRefreshGranularityTypeValueCompatible(this.body.RollingWindowGranularity, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property RollingWindowGranularity is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["rollingWindowGranularity", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertEnumToJsonValue<RefreshGranularityType>(this.RollingWindowGranularity);
			}
			if (this.body.RollingWindowPeriods != 0)
			{
				result["rollingWindowPeriods", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.RollingWindowPeriods);
			}
			if (this.body.IncrementalGranularity != RefreshGranularityType.Invalid)
			{
				if (!PropertyHelper.IsRefreshGranularityTypeValueCompatible(this.body.IncrementalGranularity, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property IncrementalGranularity is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["incrementalGranularity", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertEnumToJsonValue<RefreshGranularityType>(this.IncrementalGranularity);
			}
			if (this.body.IncrementalPeriods != 0)
			{
				result["incrementalPeriods", TomPropCategory.Regular, 6, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.IncrementalPeriods);
			}
			if (this.body.IncrementalPeriodsOffset != 0)
			{
				result["incrementalPeriodsOffset", TomPropCategory.Regular, 7, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.IncrementalPeriodsOffset);
			}
			if (!string.IsNullOrEmpty(this.body.PollingExpression))
			{
				result["pollingExpression", TomPropCategory.Regular, 8, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.PollingExpression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.SourceExpression))
			{
				result["sourceExpression", TomPropCategory.Regular, 9, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.SourceExpression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000064EC File Offset: 0x000046EC
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (base.ReadPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel))
			{
				return true;
			}
			string name = jsonProp.Name;
			if (name != null)
			{
				switch (name.Length)
				{
				case 16:
					if (name == "sourceExpression")
					{
						this.SourceExpression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 17:
					if (name == "pollingExpression")
					{
						this.PollingExpression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 18:
					if (name == "incrementalPeriods")
					{
						this.IncrementalPeriods = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
						return true;
					}
					break;
				case 20:
					if (name == "rollingWindowPeriods")
					{
						this.RollingWindowPeriods = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
						return true;
					}
					break;
				case 22:
					if (name == "incrementalGranularity")
					{
						RefreshGranularityType refreshGranularityType = JsonPropertyHelper.ConvertJsonValueToEnum<RefreshGranularityType>(jsonProp.Value);
						if (jsonProp.Value.Type != 10 && !PropertyHelper.IsRefreshGranularityTypeValueCompatible(refreshGranularityType, mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.IncrementalGranularity = refreshGranularityType;
						return true;
					}
					break;
				case 24:
				{
					char c = name[0];
					if (c != 'i')
					{
						if (c == 'r')
						{
							if (name == "rollingWindowGranularity")
							{
								RefreshGranularityType refreshGranularityType2 = JsonPropertyHelper.ConvertJsonValueToEnum<RefreshGranularityType>(jsonProp.Value);
								if (jsonProp.Value.Type != 10 && !PropertyHelper.IsRefreshGranularityTypeValueCompatible(refreshGranularityType2, mode, dbCompatibilityLevel))
								{
									return false;
								}
								this.RollingWindowGranularity = refreshGranularityType2;
								return true;
							}
						}
					}
					else if (name == "incrementalPeriodsOffset")
					{
						this.IncrementalPeriodsOffset = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
						return true;
					}
					break;
				}
				}
			}
			return false;
		}
	}
}
