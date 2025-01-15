using System;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000033 RID: 51
	internal sealed class DrillingManagerHelper
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00007585 File Offset: 0x00005785
		public bool CanDrill(Bucket bucket, BucketDrillingBehavior bucketDrillingBehavior)
		{
			Contract.CheckValue<Bucket>(bucket, "bucket");
			return bucketDrillingBehavior.IsDrillable && bucket.BucketItems.Count > 1;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000075AC File Offset: 0x000057AC
		public bool InDrillingMode(Bucket bucket, BucketDrillingBehavior bucketDrillingBehavior)
		{
			Contract.CheckValue<Bucket>(bucket, "bucket");
			if (bucketDrillingBehavior.CanExplicitlyEnableDrilling)
			{
				bool flag = false;
				if (bucket != null)
				{
					BucketProperty bucketProperty = bucket.Properties.FirstOrDefault((BucketProperty prop) => prop.Name == "EnableDrilling");
					if (bucketProperty != null && bucketProperty.Value)
					{
						flag = true;
					}
				}
				return flag && this.CanDrill(bucket, bucketDrillingBehavior);
			}
			return this.CanDrill(bucket, bucketDrillingBehavior);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000761F File Offset: 0x0000581F
		public void InitializeDrillingStates(Bucket bucket, BucketItem bucketItem, bool oppositeBucketInDrillingMode)
		{
			Contract.CheckValue<Bucket>(bucket, "bucket");
			if (bucketItem == null)
			{
				return;
			}
			if (bucket.BucketItems.Count > 0)
			{
				this.SetDrillingStates(bucket, bucketItem, oppositeBucketInDrillingMode);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007648 File Offset: 0x00005848
		public void SetDrillingStates(Bucket bucket, BucketItem bucketItem, bool oppositeBucketInDrillingMode)
		{
			Contract.CheckValue<Bucket>(bucket, "bucket");
			Contract.CheckValue<BucketItem>(bucketItem, "bucketItem");
			foreach (BucketItem bucketItem2 in bucket.BucketItems)
			{
				if (bucketItem2 == bucketItem)
				{
					bucketItem2.IsDrilledItem = new bool?(true);
				}
				else
				{
					bucketItem2.IsDrilledItem = new bool?(false);
				}
			}
		}
	}
}
