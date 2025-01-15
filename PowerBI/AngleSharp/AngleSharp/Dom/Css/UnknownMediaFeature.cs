using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000248 RID: 584
	internal sealed class UnknownMediaFeature : MediaFeature
	{
		// Token: 0x060013DB RID: 5083 RVA: 0x0004AC6D File Offset: 0x00048E6D
		public UnknownMediaFeature(string name)
			: base(name)
		{
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0004B13B File Offset: 0x0004933B
		internal override IValueConverter Converter
		{
			get
			{
				return Converters.Any;
			}
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0002F0AA File Offset: 0x0002D2AA
		public override bool Validate(RenderDevice device)
		{
			return true;
		}
	}
}
