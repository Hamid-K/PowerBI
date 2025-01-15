using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DFC RID: 3580
	[DataContract]
	internal class AllWithLimitsCdpaMetricSplit : CdpaMetricSplit
	{
		// Token: 0x17001C78 RID: 7288
		// (get) Token: 0x06006076 RID: 24694 RVA: 0x00149B2B File Offset: 0x00147D2B
		[DataMember(Name = "type", IsRequired = true)]
		public override string Type
		{
			get
			{
				return "allWithLimits";
			}
		}

		// Token: 0x17001C79 RID: 7289
		// (get) Token: 0x06006077 RID: 24695 RVA: 0x00149B32 File Offset: 0x00147D32
		// (set) Token: 0x06006078 RID: 24696 RVA: 0x00149B3A File Offset: 0x00147D3A
		[DataMember(Name = "includeValues", IsRequired = false)]
		public IList<string> IncludeValues { get; set; }

		// Token: 0x17001C7A RID: 7290
		// (get) Token: 0x06006079 RID: 24697 RVA: 0x00149B43 File Offset: 0x00147D43
		// (set) Token: 0x0600607A RID: 24698 RVA: 0x00149B4B File Offset: 0x00147D4B
		[DataMember(Name = "excludeValues", IsRequired = false)]
		public IList<string> ExcludeValues { get; set; }

		// Token: 0x17001C7B RID: 7291
		// (get) Token: 0x0600607B RID: 24699 RVA: 0x00149B54 File Offset: 0x00147D54
		// (set) Token: 0x0600607C RID: 24700 RVA: 0x00149B5C File Offset: 0x00147D5C
		[DataMember(Name = "limits", IsRequired = false)]
		public CdpaLimits Limits { get; set; }

		// Token: 0x17001C7C RID: 7292
		// (get) Token: 0x0600607D RID: 24701 RVA: 0x00149B65 File Offset: 0x00147D65
		public override bool IsRestricted
		{
			get
			{
				return (this.IncludeValues != null && this.IncludeValues.Count > 0) || (this.ExcludeValues != null && this.ExcludeValues.Count > 0) || this.Limits != null;
			}
		}

		// Token: 0x0600607E RID: 24702 RVA: 0x00149BA0 File Offset: 0x00147DA0
		public override CdpaMetricSplit Intersect(CdpaMetricSplit other)
		{
			AllWithLimitsCdpaMetricSplit allWithLimitsCdpaMetricSplit = other as AllWithLimitsCdpaMetricSplit;
			if (allWithLimitsCdpaMetricSplit != null && base.PropertyName == allWithLimitsCdpaMetricSplit.PropertyName)
			{
				AllWithLimitsCdpaMetricSplit allWithLimitsCdpaMetricSplit2 = new AllWithLimitsCdpaMetricSplit();
				allWithLimitsCdpaMetricSplit2.PropertyName = base.PropertyName;
				IEnumerable<string> enumerable = this.IncludeValues.NullableSetIntersect(allWithLimitsCdpaMetricSplit.IncludeValues);
				allWithLimitsCdpaMetricSplit2.IncludeValues = ((enumerable != null) ? enumerable.ToArray<string>() : null);
				IEnumerable<string> enumerable2 = this.ExcludeValues.NullableSetUnion(allWithLimitsCdpaMetricSplit.ExcludeValues);
				allWithLimitsCdpaMetricSplit2.ExcludeValues = ((enumerable2 != null) ? enumerable2.ToArray<string>() : null);
				allWithLimitsCdpaMetricSplit2.Limits = this.Limits.NullableIntersect(allWithLimitsCdpaMetricSplit.Limits);
				return allWithLimitsCdpaMetricSplit2;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x00149C44 File Offset: 0x00147E44
		public override CdpaMetricSplit Union(CdpaMetricSplit other)
		{
			AllWithLimitsCdpaMetricSplit allWithLimitsCdpaMetricSplit = other as AllWithLimitsCdpaMetricSplit;
			if (allWithLimitsCdpaMetricSplit != null && base.PropertyName == allWithLimitsCdpaMetricSplit.PropertyName)
			{
				AllWithLimitsCdpaMetricSplit allWithLimitsCdpaMetricSplit2 = new AllWithLimitsCdpaMetricSplit();
				allWithLimitsCdpaMetricSplit2.PropertyName = base.PropertyName;
				IEnumerable<string> enumerable = this.IncludeValues.NullableSetUnion(allWithLimitsCdpaMetricSplit.IncludeValues);
				allWithLimitsCdpaMetricSplit2.IncludeValues = ((enumerable != null) ? enumerable.ToArray<string>() : null);
				IEnumerable<string> enumerable2 = this.ExcludeValues.NullableSetIntersect(allWithLimitsCdpaMetricSplit.ExcludeValues);
				allWithLimitsCdpaMetricSplit2.ExcludeValues = ((enumerable2 != null) ? enumerable2.ToArray<string>() : null);
				allWithLimitsCdpaMetricSplit2.Limits = this.Limits.NullableUnion(allWithLimitsCdpaMetricSplit.Limits);
				return allWithLimitsCdpaMetricSplit2;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006080 RID: 24704 RVA: 0x00149CE5 File Offset: 0x00147EE5
		public AllWithLimitsCdpaMetricSplit ShallowCopy()
		{
			return new AllWithLimitsCdpaMetricSplit
			{
				PropertyName = base.PropertyName,
				IncludeValues = this.IncludeValues,
				ExcludeValues = this.ExcludeValues,
				Limits = this.Limits
			};
		}

		// Token: 0x06006081 RID: 24705 RVA: 0x00149D1C File Offset: 0x00147F1C
		public override bool Equals(CdpaMetricSplit other)
		{
			return this.Equals(other as AllWithLimitsCdpaMetricSplit);
		}

		// Token: 0x06006082 RID: 24706 RVA: 0x00149D2C File Offset: 0x00147F2C
		public bool Equals(AllWithLimitsCdpaMetricSplit other)
		{
			return other != null && base.PropertyName == other.PropertyName && this.IncludeValues.NullableSetEquals(other.IncludeValues) && this.ExcludeValues.NullableSetEquals(other.ExcludeValues) && this.Limits.NullableEquals(other.Limits);
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x00149D88 File Offset: 0x00147F88
		public override int GetHashCode()
		{
			int num = this.Type.GetHashCode();
			num += base.PropertyName.GetHashCode();
			num += this.IncludeValues.NullableSetGetHashCode<string>();
			num += this.ExcludeValues.NullableSetGetHashCode<string>();
			if (this.Limits != null)
			{
				num += this.Limits.NullableGetHashCode<CdpaLimits>();
			}
			return num;
		}
	}
}
