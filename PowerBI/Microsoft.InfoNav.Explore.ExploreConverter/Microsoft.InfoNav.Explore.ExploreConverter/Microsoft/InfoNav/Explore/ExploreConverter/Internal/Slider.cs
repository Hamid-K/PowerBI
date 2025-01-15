using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200009F RID: 159
	internal sealed class Slider
	{
		// Token: 0x06000317 RID: 791 RVA: 0x0000D0C9 File Offset: 0x0000B2C9
		internal Slider(LabelData labelData)
		{
			this._labelData = labelData;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		public LabelData LabelData
		{
			get
			{
				return this._labelData;
			}
		}

		// Token: 0x0400020D RID: 525
		private readonly LabelData _labelData;
	}
}
