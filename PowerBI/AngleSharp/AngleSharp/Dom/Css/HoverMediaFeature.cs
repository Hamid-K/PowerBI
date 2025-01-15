using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000241 RID: 577
	internal sealed class HoverMediaFeature : MediaFeature
	{
		// Token: 0x060013C1 RID: 5057 RVA: 0x0004AF6B File Offset: 0x0004916B
		public HoverMediaFeature()
			: base(FeatureNames.Hover)
		{
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0004AF78 File Offset: 0x00049178
		internal override IValueConverter Converter
		{
			get
			{
				return HoverMediaFeature.TheConverter;
			}
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0004AF7F File Offset: 0x0004917F
		public override bool Validate(RenderDevice device)
		{
			return 2 == 0;
		}

		// Token: 0x04000BD4 RID: 3028
		private static readonly IValueConverter TheConverter = Map.HoverAbilities.ToConverter<HoverAbility>();
	}
}
