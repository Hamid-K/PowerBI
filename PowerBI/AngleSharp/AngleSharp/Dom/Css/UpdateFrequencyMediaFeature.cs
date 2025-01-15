using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000249 RID: 585
	internal sealed class UpdateFrequencyMediaFeature : MediaFeature
	{
		// Token: 0x060013DE RID: 5086 RVA: 0x0004B142 File Offset: 0x00049342
		public UpdateFrequencyMediaFeature()
			: base(FeatureNames.UpdateFrequency)
		{
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0004B14F File Offset: 0x0004934F
		internal override IValueConverter Converter
		{
			get
			{
				return UpdateFrequencyMediaFeature.TheConverter;
			}
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0004B158 File Offset: 0x00049358
		public override bool Validate(RenderDevice device)
		{
			UpdateFrequency updateFrequency = UpdateFrequency.Normal;
			int frequency = device.Frequency;
			if (frequency >= 30)
			{
				return updateFrequency == UpdateFrequency.Normal;
			}
			if (frequency > 0)
			{
				return updateFrequency == UpdateFrequency.Slow;
			}
			return updateFrequency == UpdateFrequency.None;
		}

		// Token: 0x04000BD9 RID: 3033
		private static readonly IValueConverter TheConverter = Map.UpdateFrequencies.ToConverter<UpdateFrequency>();
	}
}
