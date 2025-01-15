using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000332 RID: 818
	public abstract class ImageInstance : ReportItemInstance, IImageInstance
	{
		// Token: 0x06001E83 RID: 7811 RVA: 0x0007658B File Offset: 0x0007478B
		internal ImageInstance(Image reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06001E84 RID: 7812
		// (set) Token: 0x06001E85 RID: 7813
		public abstract byte[] ImageData { get; set; }

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06001E86 RID: 7814
		// (set) Token: 0x06001E87 RID: 7815
		public abstract string StreamName { get; internal set; }

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06001E88 RID: 7816
		// (set) Token: 0x06001E89 RID: 7817
		public abstract string MIMEType { get; set; }

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06001E8A RID: 7818
		public abstract ActionInfoWithDynamicImageMapCollection ActionInfoWithDynamicImageMapAreas { get; }

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x06001E8B RID: 7819
		internal abstract bool IsNullImage { get; }

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x06001E8C RID: 7820
		public abstract TypeCode TagDataType { get; }

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x06001E8D RID: 7821
		public abstract object Tag { get; }

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x06001E8E RID: 7822
		internal abstract string ImageDataId { get; }

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x00076594 File Offset: 0x00074794
		internal Image ImageDef
		{
			get
			{
				return (Image)this.m_reportElementDef;
			}
		}

		// Token: 0x06001E90 RID: 7824
		internal abstract List<string> GetFieldsUsedInValueExpression();

		// Token: 0x06001E91 RID: 7825
		public abstract ActionInfoWithDynamicImageMap CreateActionInfoWithDynamicImageMap();
	}
}
