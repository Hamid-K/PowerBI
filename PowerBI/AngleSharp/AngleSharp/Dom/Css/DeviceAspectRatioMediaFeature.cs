using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200023B RID: 571
	internal sealed class DeviceAspectRatioMediaFeature : MediaFeature
	{
		// Token: 0x060013AF RID: 5039 RVA: 0x0004AC6D File Offset: 0x00048E6D
		public DeviceAspectRatioMediaFeature(string name)
			: base(name)
		{
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x0004AC76 File Offset: 0x00048E76
		internal override IValueConverter Converter
		{
			get
			{
				return Converters.RatioConverter;
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0004ADB0 File Offset: 0x00048FB0
		public override bool Validate(RenderDevice device)
		{
			Tuple<float, float> tuple = Tuple.Create<float, float>(1f, 1f);
			float num = tuple.Item1 / tuple.Item2;
			float num2 = (float)device.DeviceWidth / (float)device.DeviceHeight;
			if (base.IsMaximum)
			{
				return num2 <= num;
			}
			if (base.IsMinimum)
			{
				return num2 >= num;
			}
			return num == num2;
		}
	}
}
