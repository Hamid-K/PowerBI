using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DDE RID: 3550
	[DataContract]
	internal class CdpaPropertyFilterAndGroup : IEquatable<CdpaPropertyFilterAndGroup>
	{
		// Token: 0x06005FEE RID: 24558 RVA: 0x00148E71 File Offset: 0x00147071
		public CdpaPropertyFilterAndGroup()
		{
			this.PropertyFilters = EmptyArray<CdpaPropertyFilter>.Instance;
		}

		// Token: 0x17001C5A RID: 7258
		// (get) Token: 0x06005FEF RID: 24559 RVA: 0x00148E84 File Offset: 0x00147084
		// (set) Token: 0x06005FF0 RID: 24560 RVA: 0x00148E8C File Offset: 0x0014708C
		[DataMember(Name = "propertyFilters", IsRequired = true)]
		public IList<CdpaPropertyFilter> PropertyFilters { get; set; }

		// Token: 0x06005FF1 RID: 24561 RVA: 0x00148E95 File Offset: 0x00147095
		public CdpaPropertyFilterAndGroup And(CdpaPropertyFilterAndGroup other)
		{
			return new CdpaPropertyFilterAndGroup
			{
				PropertyFilters = this.PropertyFilters.Union(other.PropertyFilters).ToArray<CdpaPropertyFilter>()
			};
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x00148EB8 File Offset: 0x001470B8
		public CdpaPropertyFilterOrGroup Not()
		{
			CdpaPropertyFilterAndGroup[] array = new CdpaPropertyFilterAndGroup[this.PropertyFilters.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.PropertyFilters[i].Not().ToAndGroup();
			}
			return new CdpaPropertyFilterOrGroup
			{
				PropertyFilterAndGroup = array
			};
		}

		// Token: 0x06005FF3 RID: 24563 RVA: 0x00148F0C File Offset: 0x0014710C
		public CdpaPropertyFilterOrGroup ToOrGroup()
		{
			return new CdpaPropertyFilterOrGroup
			{
				PropertyFilterAndGroup = new CdpaPropertyFilterAndGroup[] { this }
			};
		}

		// Token: 0x06005FF4 RID: 24564 RVA: 0x00148F30 File Offset: 0x00147130
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaPropertyFilterAndGroup);
		}

		// Token: 0x06005FF5 RID: 24565 RVA: 0x00148F3E File Offset: 0x0014713E
		public bool Equals(CdpaPropertyFilterAndGroup other)
		{
			return other != null && this.PropertyFilters.SetEquals(other.PropertyFilters);
		}

		// Token: 0x06005FF6 RID: 24566 RVA: 0x00148F56 File Offset: 0x00147156
		public override int GetHashCode()
		{
			return this.PropertyFilters.SetGetHashCode<CdpaPropertyFilter>();
		}
	}
}
