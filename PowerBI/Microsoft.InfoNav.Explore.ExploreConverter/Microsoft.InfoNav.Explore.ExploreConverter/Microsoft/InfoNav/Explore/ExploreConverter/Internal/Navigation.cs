using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200009E RID: 158
	internal sealed class Navigation
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000D091 File Offset: 0x0000B291
		internal Navigation(NavigationType navigationType, Slider slider)
		{
			this._navigationType = navigationType;
			this._slider = slider;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000D0A7 File Offset: 0x0000B2A7
		public Slider Slider
		{
			get
			{
				return this._slider;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000D0AF File Offset: 0x0000B2AF
		internal NavigationType NavigationType
		{
			get
			{
				return this._navigationType;
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000D0B7 File Offset: 0x0000B2B7
		public string GetDataField()
		{
			return this._slider.LabelData.LabelDataField;
		}

		// Token: 0x0400020B RID: 523
		private readonly NavigationType _navigationType;

		// Token: 0x0400020C RID: 524
		private readonly Slider _slider;
	}
}
