using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DF7 RID: 3575
	[DataContract]
	internal class SpecificCdpaMetricSplit : CdpaMetricSplit
	{
		// Token: 0x06006054 RID: 24660 RVA: 0x0014979F File Offset: 0x0014799F
		public SpecificCdpaMetricSplit()
		{
			this.IncludeValues = EmptyArray<string>.Instance;
		}

		// Token: 0x17001C72 RID: 7282
		// (get) Token: 0x06006055 RID: 24661 RVA: 0x001497B2 File Offset: 0x001479B2
		[DataMember(Name = "type", IsRequired = true)]
		public override string Type
		{
			get
			{
				return "specific";
			}
		}

		// Token: 0x17001C73 RID: 7283
		// (get) Token: 0x06006056 RID: 24662 RVA: 0x001497B9 File Offset: 0x001479B9
		// (set) Token: 0x06006057 RID: 24663 RVA: 0x001497C1 File Offset: 0x001479C1
		[DataMember(Name = "includeValues", IsRequired = true)]
		public IList<string> IncludeValues { get; set; }

		// Token: 0x17001C74 RID: 7284
		// (get) Token: 0x06006058 RID: 24664 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsRestricted
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006059 RID: 24665 RVA: 0x001497CC File Offset: 0x001479CC
		public override CdpaMetricSplit Intersect(CdpaMetricSplit other)
		{
			SpecificCdpaMetricSplit specificCdpaMetricSplit = other as SpecificCdpaMetricSplit;
			if (specificCdpaMetricSplit != null && base.PropertyName == specificCdpaMetricSplit.PropertyName)
			{
				return new SpecificCdpaMetricSplit
				{
					PropertyName = base.PropertyName,
					IncludeValues = this.IncludeValues.NullableSetIntersect(specificCdpaMetricSplit.IncludeValues).ToArray<string>()
				};
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600605A RID: 24666 RVA: 0x0014982C File Offset: 0x00147A2C
		public override CdpaMetricSplit Union(CdpaMetricSplit other)
		{
			SpecificCdpaMetricSplit specificCdpaMetricSplit = other as SpecificCdpaMetricSplit;
			if (specificCdpaMetricSplit != null && base.PropertyName == specificCdpaMetricSplit.PropertyName)
			{
				return new SpecificCdpaMetricSplit
				{
					PropertyName = base.PropertyName,
					IncludeValues = this.IncludeValues.NullableSetUnion(specificCdpaMetricSplit.IncludeValues).ToArray<string>()
				};
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600605B RID: 24667 RVA: 0x00149889 File Offset: 0x00147A89
		public override bool Equals(CdpaMetricSplit other)
		{
			return this.Equals(other as SpecificCdpaMetricSplit);
		}

		// Token: 0x0600605C RID: 24668 RVA: 0x00149897 File Offset: 0x00147A97
		public bool Equals(SpecificCdpaMetricSplit other)
		{
			return other != null && base.PropertyName == other.PropertyName && this.IncludeValues.NullableSetEquals(other.IncludeValues);
		}

		// Token: 0x0600605D RID: 24669 RVA: 0x001498C2 File Offset: 0x00147AC2
		public override int GetHashCode()
		{
			return this.Type.GetHashCode() + base.PropertyName.GetHashCode() + this.IncludeValues.NullableSetGetHashCode<string>();
		}
	}
}
