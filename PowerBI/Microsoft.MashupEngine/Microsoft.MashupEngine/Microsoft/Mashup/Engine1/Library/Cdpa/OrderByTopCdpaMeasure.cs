using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D94 RID: 3476
	internal class OrderByTopCdpaMeasure : CdpaMeasure
	{
		// Token: 0x06005E9E RID: 24222 RVA: 0x00147309 File Offset: 0x00145509
		public OrderByTopCdpaMeasure(CdpaMeasure measure, long takeCount, bool ascending)
		{
			this.measure = measure;
			this.takeCount = takeCount;
			this.ascending = ascending;
		}

		// Token: 0x17001BD1 RID: 7121
		// (get) Token: 0x06005E9F RID: 24223 RVA: 0x00147326 File Offset: 0x00145526
		public override CdpaCube Cube
		{
			get
			{
				return this.measure.Cube;
			}
		}

		// Token: 0x17001BD2 RID: 7122
		// (get) Token: 0x06005EA0 RID: 24224 RVA: 0x00147333 File Offset: 0x00145533
		public override QualifiedName QualifiedName
		{
			get
			{
				return this.measure.QualifiedName;
			}
		}

		// Token: 0x17001BD3 RID: 7123
		// (get) Token: 0x06005EA1 RID: 24225 RVA: 0x00147340 File Offset: 0x00145540
		public override string Caption
		{
			get
			{
				return this.measure.Caption;
			}
		}

		// Token: 0x17001BD4 RID: 7124
		// (get) Token: 0x06005EA2 RID: 24226 RVA: 0x0014734D File Offset: 0x0014554D
		public override TypeValue Type
		{
			get
			{
				return this.measure.Type;
			}
		}

		// Token: 0x17001BD5 RID: 7125
		// (get) Token: 0x06005EA3 RID: 24227 RVA: 0x0014735A File Offset: 0x0014555A
		public CdpaMeasure Measure
		{
			get
			{
				return this.measure;
			}
		}

		// Token: 0x17001BD6 RID: 7126
		// (get) Token: 0x06005EA4 RID: 24228 RVA: 0x00147362 File Offset: 0x00145562
		public long TakeCount
		{
			get
			{
				return this.takeCount;
			}
		}

		// Token: 0x17001BD7 RID: 7127
		// (get) Token: 0x06005EA5 RID: 24229 RVA: 0x0014736A File Offset: 0x0014556A
		public bool Ascending
		{
			get
			{
				return this.ascending;
			}
		}

		// Token: 0x040033FA RID: 13306
		private readonly CdpaMeasure measure;

		// Token: 0x040033FB RID: 13307
		private readonly long takeCount;

		// Token: 0x040033FC RID: 13308
		private readonly bool ascending;
	}
}
