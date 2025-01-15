using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000244 RID: 580
	internal sealed class PointerMediaFeature : MediaFeature
	{
		// Token: 0x060013CC RID: 5068 RVA: 0x0004B021 File Offset: 0x00049221
		public PointerMediaFeature()
			: base(FeatureNames.Pointer)
		{
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0004B02E File Offset: 0x0004922E
		internal override IValueConverter Converter
		{
			get
			{
				return PointerMediaFeature.TheConverter;
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0004AF7F File Offset: 0x0004917F
		public override bool Validate(RenderDevice device)
		{
			return 2 == 0;
		}

		// Token: 0x04000BD6 RID: 3030
		private static readonly IValueConverter TheConverter = Map.PointerAccuracies.ToConverter<PointerAccuracy>();
	}
}
