using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DE1 RID: 3553
	[DataContract]
	internal class CdpaTableConfiguration : CdpaTableOrMetricConfiguration, IEquatable<CdpaTableConfiguration>, IIntersectable<CdpaTableConfiguration>, IUnionable<CdpaTableConfiguration>
	{
		// Token: 0x06006005 RID: 24581 RVA: 0x0014930C File Offset: 0x0014750C
		public CdpaTableConfiguration()
		{
			this.Dimensions = EmptyArray<QualifiedName>.Instance;
			this.Events = EmptyArray<CdpaEvent>.Instance;
		}

		// Token: 0x17001C5C RID: 7260
		// (get) Token: 0x06006006 RID: 24582 RVA: 0x0014932A File Offset: 0x0014752A
		// (set) Token: 0x06006007 RID: 24583 RVA: 0x00149332 File Offset: 0x00147532
		public IList<QualifiedName> Dimensions { get; set; }

		// Token: 0x17001C5D RID: 7261
		// (get) Token: 0x06006008 RID: 24584 RVA: 0x0014933B File Offset: 0x0014753B
		// (set) Token: 0x06006009 RID: 24585 RVA: 0x00149343 File Offset: 0x00147543
		[DataMember(Name = "events", IsRequired = true)]
		public IList<CdpaEvent> Events { get; set; }

		// Token: 0x17001C5E RID: 7262
		// (get) Token: 0x0600600A RID: 24586 RVA: 0x0014934C File Offset: 0x0014754C
		// (set) Token: 0x0600600B RID: 24587 RVA: 0x00149354 File Offset: 0x00147554
		[DataMember(Name = "filters", IsRequired = false)]
		public CdpaPropertyFilterOrGroup Filters { get; set; }

		// Token: 0x0600600C RID: 24588 RVA: 0x0014935D File Offset: 0x0014755D
		public CdpaTableConfiguration ShallowCopy()
		{
			return new CdpaTableConfiguration
			{
				Dimensions = this.Dimensions,
				Events = this.Events,
				Filters = this.Filters
			};
		}

		// Token: 0x0600600D RID: 24589 RVA: 0x00149388 File Offset: 0x00147588
		public CdpaTableConfiguration Filter(CdpaPropertyFilterOrGroup filter)
		{
			CdpaTableConfiguration cdpaTableConfiguration = this.ShallowCopy();
			cdpaTableConfiguration.Filters = ((this.Filters != null) ? this.Filters.And(filter) : filter);
			return cdpaTableConfiguration;
		}

		// Token: 0x0600600E RID: 24590 RVA: 0x001493B0 File Offset: 0x001475B0
		public CdpaTableConfiguration Intersect(CdpaTableConfiguration other)
		{
			return new CdpaTableConfiguration
			{
				Dimensions = this.Dimensions.Intersect(other.Dimensions).ToArray<QualifiedName>(),
				Events = this.Events.Intersect(other.Events).ToArray<CdpaEvent>(),
				Filters = this.Filters.NullableIntersect(other.Filters)
			};
		}

		// Token: 0x0600600F RID: 24591 RVA: 0x00149414 File Offset: 0x00147614
		public CdpaTableConfiguration Union(CdpaTableConfiguration other)
		{
			return new CdpaTableConfiguration
			{
				Dimensions = this.Dimensions.Union(other.Dimensions).ToArray<QualifiedName>(),
				Events = this.Events.Union(other.Events).ToArray<CdpaEvent>(),
				Filters = this.Filters.NullableUnion(other.Filters)
			};
		}

		// Token: 0x06006010 RID: 24592 RVA: 0x00149475 File Offset: 0x00147675
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaTableConfiguration);
		}

		// Token: 0x06006011 RID: 24593 RVA: 0x00149483 File Offset: 0x00147683
		public bool Equals(CdpaTableConfiguration other)
		{
			return other != null && this.Events.SetEquals(other.Events) && this.Filters.NullableEquals(other.Filters);
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x001494AE File Offset: 0x001476AE
		public override int GetHashCode()
		{
			return this.Events.SetGetHashCode<CdpaEvent>() * 5011 + this.Filters.NullableGetHashCode<CdpaPropertyFilterOrGroup>();
		}
	}
}
