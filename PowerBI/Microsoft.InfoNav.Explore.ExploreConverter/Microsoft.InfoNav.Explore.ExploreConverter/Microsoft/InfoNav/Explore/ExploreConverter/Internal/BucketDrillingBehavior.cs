using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200002B RID: 43
	internal sealed class BucketDrillingBehavior
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00006A08 File Offset: 0x00004C08
		internal BucketDrillingBehavior()
		{
			this._isDrillable = false;
			this._canExplicitlyEnableDrilling = false;
			this._oppositeBucketName = null;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006A25 File Offset: 0x00004C25
		internal BucketDrillingBehavior(bool canExplicitlyEnableDrilling, string oppositeBucketName = null)
		{
			this._isDrillable = true;
			this._canExplicitlyEnableDrilling = canExplicitlyEnableDrilling;
			this._oppositeBucketName = oppositeBucketName;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006A42 File Offset: 0x00004C42
		public bool IsDrillable
		{
			get
			{
				return this._isDrillable;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006A4A File Offset: 0x00004C4A
		public bool CanExplicitlyEnableDrilling
		{
			get
			{
				return this._canExplicitlyEnableDrilling;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006A52 File Offset: 0x00004C52
		public string OppositeBucketName
		{
			get
			{
				return this._oppositeBucketName;
			}
		}

		// Token: 0x040000BA RID: 186
		private readonly bool _isDrillable;

		// Token: 0x040000BB RID: 187
		private readonly bool _canExplicitlyEnableDrilling;

		// Token: 0x040000BC RID: 188
		private readonly string _oppositeBucketName;
	}
}
