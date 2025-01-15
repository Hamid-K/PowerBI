using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200024F RID: 591
	internal sealed class CssFeatureProperty : CssProperty
	{
		// Token: 0x060013F5 RID: 5109 RVA: 0x0004B3AA File Offset: 0x000495AA
		internal CssFeatureProperty(MediaFeature feature)
			: base(feature.Name, PropertyFlags.None)
		{
			this._feature = feature;
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0004B3C0 File Offset: 0x000495C0
		internal override IValueConverter Converter
		{
			get
			{
				return this._feature.Converter;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x0004B3CD File Offset: 0x000495CD
		internal MediaFeature Feature
		{
			get
			{
				return this._feature;
			}
		}

		// Token: 0x04000BE0 RID: 3040
		private readonly MediaFeature _feature;
	}
}
