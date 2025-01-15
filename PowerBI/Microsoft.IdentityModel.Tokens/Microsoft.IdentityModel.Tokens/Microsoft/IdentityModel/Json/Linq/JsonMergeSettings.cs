using System;

namespace Microsoft.IdentityModel.Json.Linq
{
	// Token: 0x020000C5 RID: 197
	internal class JsonMergeSettings
	{
		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002B221 File Offset: 0x00029421
		public JsonMergeSettings()
		{
			this._propertyNameComparison = StringComparison.Ordinal;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0002B230 File Offset: 0x00029430
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x0002B238 File Offset: 0x00029438
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
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0002B254 File Offset: 0x00029454
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x0002B25C File Offset: 0x0002945C
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
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002B278 File Offset: 0x00029478
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0002B280 File Offset: 0x00029480
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

		// Token: 0x0400038E RID: 910
		private MergeArrayHandling _mergeArrayHandling;

		// Token: 0x0400038F RID: 911
		private MergeNullValueHandling _mergeNullValueHandling;

		// Token: 0x04000390 RID: 912
		private StringComparison _propertyNameComparison;
	}
}
