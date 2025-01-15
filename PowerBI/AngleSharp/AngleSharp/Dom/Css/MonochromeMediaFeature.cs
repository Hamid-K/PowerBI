using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000242 RID: 578
	internal sealed class MonochromeMediaFeature : MediaFeature
	{
		// Token: 0x060013C5 RID: 5061 RVA: 0x0004AC6D File Offset: 0x00048E6D
		public MonochromeMediaFeature(string name)
			: base(name)
		{
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0004ACDF File Offset: 0x00048EDF
		internal override IValueConverter Converter
		{
			get
			{
				if (!base.IsMinimum && !base.IsMaximum)
				{
					return Converters.NaturalIntegerConverter.Option(1);
				}
				return Converters.NaturalIntegerConverter;
			}
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0004AF98 File Offset: 0x00049198
		public override bool Validate(RenderDevice device)
		{
			int num = 0;
			int monochromeBits = device.MonochromeBits;
			if (base.IsMaximum)
			{
				return monochromeBits <= num;
			}
			if (base.IsMinimum)
			{
				return monochromeBits >= num;
			}
			return num == monochromeBits;
		}
	}
}
