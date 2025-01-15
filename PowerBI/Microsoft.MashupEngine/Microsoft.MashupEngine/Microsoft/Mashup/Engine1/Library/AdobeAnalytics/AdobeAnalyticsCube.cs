using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F53 RID: 3923
	internal sealed class AdobeAnalyticsCube
	{
		// Token: 0x0600679C RID: 26524 RVA: 0x00164C87 File Offset: 0x00162E87
		public AdobeAnalyticsCube(AdobeAnalyticsService service, string id, string name, string company)
		{
			this.service = service;
			this.Id = id;
			this.Name = name;
			this.Company = company;
			this.isInitialized = false;
		}

		// Token: 0x17001DF6 RID: 7670
		// (get) Token: 0x0600679D RID: 26525 RVA: 0x00164CB3 File Offset: 0x00162EB3
		// (set) Token: 0x0600679E RID: 26526 RVA: 0x00164CBB File Offset: 0x00162EBB
		public string Id { get; private set; }

		// Token: 0x17001DF7 RID: 7671
		// (get) Token: 0x0600679F RID: 26527 RVA: 0x00164CC4 File Offset: 0x00162EC4
		// (set) Token: 0x060067A0 RID: 26528 RVA: 0x00164CCC File Offset: 0x00162ECC
		public string Name { get; private set; }

		// Token: 0x17001DF8 RID: 7672
		// (get) Token: 0x060067A1 RID: 26529 RVA: 0x00164CD5 File Offset: 0x00162ED5
		// (set) Token: 0x060067A2 RID: 26530 RVA: 0x00164CDD File Offset: 0x00162EDD
		public string Company { get; private set; }

		// Token: 0x17001DF9 RID: 7673
		// (get) Token: 0x060067A3 RID: 26531 RVA: 0x00164CE6 File Offset: 0x00162EE6
		public IList<AdobeAnalyticsMeasure> Measures
		{
			get
			{
				this.EnsureInitialized();
				return this.measures;
			}
		}

		// Token: 0x17001DFA RID: 7674
		// (get) Token: 0x060067A4 RID: 26532 RVA: 0x00164CF4 File Offset: 0x00162EF4
		public IList<AdobeAnalyticsDimension> Dimensions
		{
			get
			{
				this.EnsureInitialized();
				return this.dimensions;
			}
		}

		// Token: 0x17001DFB RID: 7675
		// (get) Token: 0x060067A5 RID: 26533 RVA: 0x00164D02 File Offset: 0x00162F02
		public IList<AdobeAnalyticsSegment> Segments
		{
			get
			{
				if (this.segments == null)
				{
					this.segments = this.service.GetSegments(this);
				}
				return this.segments;
			}
		}

		// Token: 0x060067A6 RID: 26534 RVA: 0x00164D24 File Offset: 0x00162F24
		public AdobeAnalyticsCubeObject GetCubeObject(string id)
		{
			this.EnsureInitialized();
			return this.cubeObjects[id];
		}

		// Token: 0x060067A7 RID: 26535 RVA: 0x00164D38 File Offset: 0x00162F38
		private void EnsureInitialized()
		{
			if (!this.isInitialized)
			{
				this.cubeObjects = new Dictionary<string, AdobeAnalyticsCubeObject>();
				this.measures = this.service.GetMeasures(this);
				foreach (AdobeAnalyticsCubeObject adobeAnalyticsCubeObject in this.measures)
				{
					this.cubeObjects[adobeAnalyticsCubeObject.Id] = adobeAnalyticsCubeObject;
				}
				this.dimensions = this.service.GetDimensions(this);
				foreach (AdobeAnalyticsCubeObject adobeAnalyticsCubeObject2 in this.dimensions)
				{
					this.cubeObjects[adobeAnalyticsCubeObject2.Id] = adobeAnalyticsCubeObject2;
				}
				this.isInitialized = true;
			}
		}

		// Token: 0x0400390B RID: 14603
		private readonly AdobeAnalyticsService service;

		// Token: 0x0400390C RID: 14604
		private IList<AdobeAnalyticsMeasure> measures;

		// Token: 0x0400390D RID: 14605
		private IList<AdobeAnalyticsDimension> dimensions;

		// Token: 0x0400390E RID: 14606
		private IList<AdobeAnalyticsSegment> segments;

		// Token: 0x0400390F RID: 14607
		private IDictionary<string, AdobeAnalyticsCubeObject> cubeObjects;

		// Token: 0x04003910 RID: 14608
		private bool isInitialized;
	}
}
