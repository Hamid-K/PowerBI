using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C5 RID: 197
	public class JsonMergeSettings
	{
		// Token: 0x06000ACA RID: 2762 RVA: 0x0002B339 File Offset: 0x00029539
		public JsonMergeSettings()
		{
			this._propertyNameComparison = StringComparison.Ordinal;
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0002B348 File Offset: 0x00029548
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0002B350 File Offset: 0x00029550
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

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0002B36C File Offset: 0x0002956C
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0002B374 File Offset: 0x00029574
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

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0002B390 File Offset: 0x00029590
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x0002B398 File Offset: 0x00029598
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

		// Token: 0x0400038F RID: 911
		private MergeArrayHandling _mergeArrayHandling;

		// Token: 0x04000390 RID: 912
		private MergeNullValueHandling _mergeNullValueHandling;

		// Token: 0x04000391 RID: 913
		private StringComparison _propertyNameComparison;
	}
}
