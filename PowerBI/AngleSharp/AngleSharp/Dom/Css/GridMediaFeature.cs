using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200023F RID: 575
	internal sealed class GridMediaFeature : MediaFeature
	{
		// Token: 0x060013BB RID: 5051 RVA: 0x0004AEF7 File Offset: 0x000490F7
		public GridMediaFeature()
			: base(FeatureNames.Grid)
		{
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0004AF04 File Offset: 0x00049104
		internal override IValueConverter Converter
		{
			get
			{
				return Converters.BinaryConverter;
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0004AF0C File Offset: 0x0004910C
		public override bool Validate(RenderDevice device)
		{
			bool flag = false;
			bool isGrid = device.IsGrid;
			return flag == isGrid;
		}
	}
}
