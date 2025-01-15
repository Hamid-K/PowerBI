using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x02000105 RID: 261
	internal sealed class ODataEntriesEntityMaterializer : ODataEntityMaterializer
	{
		// Token: 0x06000AFD RID: 2813 RVA: 0x00029855 File Offset: 0x00027A55
		public ODataEntriesEntityMaterializer(IEnumerable<ODataResource> entries, IODataMaterializerContext materializerContext, EntityTrackingAdapter entityTrackingAdapter, QueryComponents queryComponents, Type expectedType, ProjectionPlan materializeEntryPlan, ODataFormat format)
			: base(materializerContext, entityTrackingAdapter, queryComponents, expectedType, materializeEntryPlan)
		{
			this.format = format;
			this.feedEntries = entries.GetEnumerator();
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00003487 File Offset: 0x00001687
		internal override ODataResourceSet CurrentFeed
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00029879 File Offset: 0x00027A79
		internal override ODataResource CurrentEntry
		{
			get
			{
				base.VerifyNotDisposed();
				return this.feedEntries.Current;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0002988C File Offset: 0x00027A8C
		internal override long CountValue
		{
			get
			{
				throw new InvalidOperationException(Strings.MaterializeFromAtom_CountNotPresent);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00015066 File Offset: 0x00013266
		internal override bool IsCountable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00029898 File Offset: 0x00027A98
		internal override bool IsEndOfStream
		{
			get
			{
				return this.isFinished;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x000298A0 File Offset: 0x00027AA0
		protected override bool IsDisposed
		{
			get
			{
				return this.feedEntries == null;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x000298AB File Offset: 0x00027AAB
		protected override ODataFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000298B3 File Offset: 0x00027AB3
		protected override bool ReadNextFeedOrEntry()
		{
			if (!this.isFinished && !this.feedEntries.MoveNext())
			{
				this.isFinished = true;
			}
			return !this.isFinished;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x000298DA File Offset: 0x00027ADA
		protected override void OnDispose()
		{
			if (this.feedEntries != null)
			{
				this.feedEntries.Dispose();
				this.feedEntries = null;
			}
		}

		// Token: 0x04000629 RID: 1577
		private readonly ODataFormat format;

		// Token: 0x0400062A RID: 1578
		private IEnumerator<ODataResource> feedEntries;

		// Token: 0x0400062B RID: 1579
		private bool isFinished;
	}
}
