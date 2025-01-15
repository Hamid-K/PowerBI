using System;

namespace Microsoft.Identity.Json.Linq
{
	// Token: 0x020000C4 RID: 196
	internal class JsonMergeSettings
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002AB31 File Offset: 0x00028D31
		public JsonMergeSettings()
		{
			this._propertyNameComparison = StringComparison.Ordinal;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0002AB40 File Offset: 0x00028D40
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x0002AB48 File Offset: 0x00028D48
		public MergeArrayHandling MergeArrayHandling
		{
			get
			{
				return this._mergeArrayHandling;
			}
			set
			{
				if (value < MergeArrayHandling.Concat || value > MergeArrayHandling.Merge)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._mergeArrayHandling = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0002AB64 File Offset: 0x00028D64
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x0002AB6C File Offset: 0x00028D6C
		public MergeNullValueHandling MergeNullValueHandling
		{
			get
			{
				return this._mergeNullValueHandling;
			}
			set
			{
				if (value < MergeNullValueHandling.Ignore || value > MergeNullValueHandling.Merge)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._mergeNullValueHandling = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002AB88 File Offset: 0x00028D88
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x0002AB90 File Offset: 0x00028D90
		public StringComparison PropertyNameComparison
		{
			get
			{
				return this._propertyNameComparison;
			}
			set
			{
				if (value < StringComparison.CurrentCulture || value > StringComparison.OrdinalIgnoreCase)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._propertyNameComparison = value;
			}
		}

		// Token: 0x04000373 RID: 883
		private MergeArrayHandling _mergeArrayHandling;

		// Token: 0x04000374 RID: 884
		private MergeNullValueHandling _mergeNullValueHandling;

		// Token: 0x04000375 RID: 885
		private StringComparison _propertyNameComparison;
	}
}
