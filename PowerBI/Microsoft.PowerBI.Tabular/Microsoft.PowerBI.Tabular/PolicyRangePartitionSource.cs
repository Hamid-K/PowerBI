using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200010C RID: 268
	[CompatibilityRequirement("1450")]
	public class PolicyRangePartitionSource : PartitionSource
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x0007D61C File Offset: 0x0007B81C
		public PolicyRangePartitionSource()
		{
			this.start = new DateTime(0L);
			this.end = new DateTime(0L);
			this.granularity = RefreshGranularityType.Invalid;
			this.refreshBookmark = string.Empty;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0007D650 File Offset: 0x0007B850
		internal override PartitionSourceType Type
		{
			get
			{
				return PartitionSourceType.PolicyRange;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0007D653 File Offset: 0x0007B853
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x0007D674 File Offset: 0x0007B874
		public DateTime Start
		{
			get
			{
				if (base.Partition != null)
				{
					return base.Partition.body.RangeStart;
				}
				return this.start;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.Start, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(base.Partition, "Source.Start", typeof(DateTime), this.Start, value);
					DateTime dateTime = this.Start;
					if (base.Partition != null)
					{
						base.Partition.body.RangeStart = value;
					}
					else
					{
						this.start = value;
					}
					ObjectChangeTracker.RegisterPropertyChanged(base.Partition, "Source.Start", typeof(DateTime), dateTime, value);
				}
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0007D709 File Offset: 0x0007B909
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x0007D72C File Offset: 0x0007B92C
		public DateTime End
		{
			get
			{
				if (base.Partition != null)
				{
					return base.Partition.body.RangeEnd;
				}
				return this.end;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.End, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(base.Partition, "Source.End", typeof(DateTime), this.End, value);
					DateTime dateTime = this.End;
					if (base.Partition != null)
					{
						base.Partition.body.RangeEnd = value;
					}
					else
					{
						this.end = value;
					}
					ObjectChangeTracker.RegisterPropertyChanged(base.Partition, "Source.End", typeof(DateTime), dateTime, value);
				}
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0007D7C1 File Offset: 0x0007B9C1
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x0007D7E4 File Offset: 0x0007B9E4
		public RefreshGranularityType Granularity
		{
			get
			{
				if (base.Partition != null)
				{
					return base.Partition.body.RangeGranularity;
				}
				return this.granularity;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.Granularity, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(base.Partition, "Source.Granularity", typeof(RefreshGranularityType), this.Granularity, value);
					RefreshGranularityType refreshGranularityType = this.Granularity;
					if (base.Partition != null)
					{
						base.Partition.body.RangeGranularity = value;
					}
					else
					{
						this.granularity = value;
					}
					ObjectChangeTracker.RegisterPropertyChanged(base.Partition, "Source.Granularity", typeof(RefreshGranularityType), refreshGranularityType, value);
				}
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0007D879 File Offset: 0x0007BA79
		// (set) Token: 0x060011A0 RID: 4512 RVA: 0x0007D89C File Offset: 0x0007BA9C
		public string RefreshBookmark
		{
			get
			{
				if (base.Partition != null)
				{
					return base.Partition.body.RefreshBookmark;
				}
				return this.refreshBookmark;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.RefreshBookmark, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(base.Partition, "Source.RefreshBookmark", typeof(string), this.RefreshBookmark, value);
					string text = this.RefreshBookmark;
					if (base.Partition != null)
					{
						base.Partition.body.RefreshBookmark = value;
					}
					else
					{
						this.refreshBookmark = value;
					}
					ObjectChangeTracker.RegisterPropertyChanged(base.Partition, "Source.RefreshBookmark", typeof(string), text, value);
				}
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0007D920 File Offset: 0x0007BB20
		internal override void MoveDataToPartition(Partition partition)
		{
			partition.body.RangeStart = this.start;
			partition.body.RangeEnd = this.end;
			partition.body.RangeGranularity = this.granularity;
			partition.body.RefreshBookmark = this.refreshBookmark;
			this.start = new DateTime(0L);
			this.end = new DateTime(0L);
			this.granularity = RefreshGranularityType.Invalid;
			this.refreshBookmark = null;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0007D99C File Offset: 0x0007BB9C
		internal override void LoadDataFromPartition(Partition partition, bool canResolveLinks, bool resetPartitionBodyProperties)
		{
			this.start = partition.body.RangeStart;
			this.end = partition.body.RangeEnd;
			this.granularity = partition.body.RangeGranularity;
			this.refreshBookmark = partition.body.RefreshBookmark;
			if (resetPartitionBodyProperties)
			{
				partition.body.RangeStart = new DateTime(0L);
				partition.body.RangeEnd = new DateTime(0L);
				partition.body.RangeGranularity = RefreshGranularityType.Invalid;
				partition.body.RefreshBookmark = string.Empty;
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0007DA30 File Offset: 0x0007BC30
		internal override IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames()
		{
			yield return new CustomizedPropertyName("RangeStart", "Source.Start");
			yield return new CustomizedPropertyName("RangeEnd", "Source.End");
			yield return new CustomizedPropertyName("RangeGranularity", "Source.Granularity");
			yield return new CustomizedPropertyName("RefreshBookmark", "Source.RefreshBookmark");
			yield break;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0007DA3C File Offset: 0x0007BC3C
		internal string GeneratePartitionName(bool isDQ = false)
		{
			DateTime dateTime = this.Start;
			switch (this.Granularity)
			{
			case RefreshGranularityType.Day:
				return string.Format(CultureInfo.InvariantCulture, isDQ ? "{0:D4}Q{1:D1}{2:D2}{3:D2}-onward" : "{0:D4}Q{1:D1}{2:D2}{3:D2}", new object[]
				{
					dateTime.Year,
					(dateTime.Month - 1) / 3 + 1,
					dateTime.Month,
					dateTime.Day
				});
			case RefreshGranularityType.Month:
				return string.Format(CultureInfo.InvariantCulture, isDQ ? "{0:D4}Q{1:D1}{2:D2}-onward" : "{0:D4}Q{1:D1}{2:D2}", dateTime.Year, (dateTime.Month - 1) / 3 + 1, dateTime.Month);
			case RefreshGranularityType.Quarter:
				return string.Format(CultureInfo.InvariantCulture, isDQ ? "{0:D4}Q{1:D1}-onward" : "{0:D4}Q{1:D1}", dateTime.Year, (dateTime.Month - 1) / 3 + 1);
			case RefreshGranularityType.Year:
				return string.Format(CultureInfo.InvariantCulture, isDQ ? "{0:D4}-onward" : "{0:D4}", dateTime.Year);
			default:
				throw TomInternalException.Create("Invalid RefreshGranularityType {0} provided.", new object[] { this.Granularity });
			}
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0007DB94 File Offset: 0x0007BD94
		internal override void SerializeToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			Utils.Verify(CompatibilityRestrictions.PartitionSourceType_PolicyRange.IsCompatible(mode, dbCompatibilityLevel));
			base.SerializeToJsonObject(jsonObj, options, mode, dbCompatibilityLevel);
			if (this.Start.CompareTo(DateTime.MinValue) != 0)
			{
				jsonObj["start", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.Start);
			}
			if (this.End.CompareTo(DateTime.MinValue) != 0)
			{
				jsonObj["end", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.End);
			}
			jsonObj["granularity", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertEnumToJsonValue<RefreshGranularityType>(this.Granularity);
			if (!string.IsNullOrEmpty(this.RefreshBookmark))
			{
				jsonObj["refreshBookmark", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.RefreshBookmark, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0007DC68 File Offset: 0x0007BE68
		private protected override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "type")
			{
				return true;
			}
			if (name == "start")
			{
				this.Start = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			if (name == "end")
			{
				this.End = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			if (name == "granularity")
			{
				this.Granularity = JsonPropertyHelper.ConvertJsonValueToEnum<RefreshGranularityType>(jsonProp.Value);
				return true;
			}
			if (!(name == "refreshBookmark"))
			{
				return false;
			}
			this.RefreshBookmark = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
			return true;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0007DD10 File Offset: 0x0007BF10
		private protected override void SaveMetadataProperties(SerializationActivityContext context, IList<MetadataProperty> properties)
		{
			base.SaveMetadataProperties(context, properties);
			if (this.Start.CompareTo(DateTime.MinValue) != 0)
			{
				properties.Add(new MetadataProperty("start", MetadataPropertyNature.RegularProperty, typeof(DateTime), this.Start));
			}
			if (this.End.CompareTo(DateTime.MinValue) != 0)
			{
				properties.Add(new MetadataProperty("end", MetadataPropertyNature.RegularProperty, typeof(DateTime), this.End));
			}
			properties.Add(new MetadataProperty("granularity", MetadataPropertyNature.RegularProperty, typeof(RefreshGranularityType), this.Granularity));
			if (!string.IsNullOrEmpty(this.RefreshBookmark))
			{
				properties.Add(new MetadataProperty("refreshBookmark", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string), this.RefreshBookmark));
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0007DDF4 File Offset: 0x0007BFF4
		private protected override bool TryReadMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName == "start")
			{
				this.Start = reader.ReadDateTimeProperty();
				return true;
			}
			if (propertyName == "end")
			{
				this.End = reader.ReadDateTimeProperty();
				return true;
			}
			if (propertyName == "granularity")
			{
				this.Granularity = reader.ReadEnumProperty<RefreshGranularityType>();
				return true;
			}
			if (!(propertyName == "refreshBookmark"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			this.RefreshBookmark = reader.ReadStringProperty();
			return true;
		}

		// Token: 0x040002B7 RID: 695
		private DateTime start;

		// Token: 0x040002B8 RID: 696
		private DateTime end;

		// Token: 0x040002B9 RID: 697
		private RefreshGranularityType granularity;

		// Token: 0x040002BA RID: 698
		private string refreshBookmark;
	}
}
