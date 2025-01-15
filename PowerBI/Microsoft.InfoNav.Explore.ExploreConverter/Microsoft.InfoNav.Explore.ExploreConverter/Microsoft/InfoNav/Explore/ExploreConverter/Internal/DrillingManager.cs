using System;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000032 RID: 50
	internal sealed class DrillingManager
	{
		// Token: 0x0600016D RID: 365 RVA: 0x000073B0 File Offset: 0x000055B0
		private DrillingManager()
		{
			this._bucketDrillingBehavior = new BucketDrillingBehavior();
			this._drillingManagerHelper = new DrillingManagerHelper();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000073CE File Offset: 0x000055CE
		private DrillingManager(BucketDrillingBehavior bucketDrillingBehavior)
		{
			this._bucketDrillingBehavior = bucketDrillingBehavior;
			this._drillingManagerHelper = new DrillingManagerHelper();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000073E8 File Offset: 0x000055E8
		public static DrillingManager CreateDrillingManager(string visual, string bucket)
		{
			if (bucket == "Category" && (visual == "CategoryChart" || visual == "SmallMultiple" || visual == "Line" || visual == "Map" || visual == "ChoroplethMap" || visual == "Pie" || visual == "Scatter"))
			{
				return new DrillingManager(new BucketDrillingBehavior(false, "Series"));
			}
			if (visual == "Matrix" && bucket == "RowHierarchy")
			{
				return new DrillingManager(new BucketDrillingBehavior(true, "ColumnHierarchy"));
			}
			if (visual == "Matrix" && bucket == "ColumnHierarchy")
			{
				return new DrillingManager(new BucketDrillingBehavior(true, "RowHierarchy"));
			}
			return new DrillingManager();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000074C9 File Offset: 0x000056C9
		public bool IsInDrillingMode(Bucket bucket)
		{
			Contract.CheckValue<Bucket>(bucket, "bucket");
			return this._drillingManagerHelper.InDrillingMode(bucket, this._bucketDrillingBehavior);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000074E8 File Offset: 0x000056E8
		public void InitializeForDrilling(Bucket bucket, PVVisual visualDefinition, BucketItem bucketItem)
		{
			Contract.CheckValue<Bucket>(bucket, "bucket");
			Contract.CheckValue<PVVisual>(visualDefinition, "visualDefinition");
			this._drillingManagerHelper.InitializeDrillingStates(bucket, bucketItem, this.HasOppositeBucketInDrillingMode(visualDefinition));
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007514 File Offset: 0x00005714
		public bool HasOppositeBucketInDrillingMode(PVVisual visualDefinition)
		{
			Bucket oppositeBucketOrNull = this.GetOppositeBucketOrNull(visualDefinition);
			return oppositeBucketOrNull != null && this._drillingManagerHelper.InDrillingMode(oppositeBucketOrNull, this._bucketDrillingBehavior);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007540 File Offset: 0x00005740
		public Bucket GetOppositeBucketOrNull(PVVisual visualDefinition)
		{
			if (this._bucketDrillingBehavior.OppositeBucketName != null)
			{
				return visualDefinition.DataContext.Buckets.FirstOrDefault((Bucket bucket) => bucket.Name == this._bucketDrillingBehavior.OppositeBucketName);
			}
			return null;
		}

		// Token: 0x040000BE RID: 190
		private BucketDrillingBehavior _bucketDrillingBehavior;

		// Token: 0x040000BF RID: 191
		private DrillingManagerHelper _drillingManagerHelper;
	}
}
