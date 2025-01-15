using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000247 RID: 583
	internal sealed class ScriptingMediaFeature : MediaFeature
	{
		// Token: 0x060013D7 RID: 5079 RVA: 0x0004B0E2 File Offset: 0x000492E2
		public ScriptingMediaFeature()
			: base(FeatureNames.Scripting)
		{
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0004B0EF File Offset: 0x000492EF
		internal override IValueConverter Converter
		{
			get
			{
				return ScriptingMediaFeature.TheConverter;
			}
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0004B0F8 File Offset: 0x000492F8
		public override bool Validate(RenderDevice device)
		{
			ScriptingState scriptingState = ScriptingState.None;
			IConfiguration options = device.Options;
			ScriptingState scriptingState2 = ScriptingState.None;
			if (options != null && options.IsScripting())
			{
				scriptingState2 = ((device.DeviceType == RenderDevice.Kind.Screen) ? ScriptingState.Enabled : ScriptingState.InitialOnly);
			}
			return scriptingState == scriptingState2;
		}

		// Token: 0x04000BD8 RID: 3032
		private static readonly IValueConverter TheConverter = Map.ScriptingStates.ToConverter<ScriptingState>();
	}
}
