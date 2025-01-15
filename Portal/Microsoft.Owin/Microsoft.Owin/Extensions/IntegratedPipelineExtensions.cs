using System;
using Owin;

namespace Microsoft.Owin.Extensions
{
	// Token: 0x02000040 RID: 64
	public static class IntegratedPipelineExtensions
	{
		// Token: 0x0600024B RID: 587 RVA: 0x00006680 File Offset: 0x00004880
		public static IAppBuilder UseStageMarker(this IAppBuilder app, string stageName)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			object obj;
			if (app.Properties.TryGetValue("integratedpipeline.StageMarker", out obj))
			{
				Action<IAppBuilder, string> addMarker = (Action<IAppBuilder, string>)obj;
				addMarker(app, stageName);
			}
			return app;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000066BF File Offset: 0x000048BF
		public static IAppBuilder UseStageMarker(this IAppBuilder app, PipelineStage stage)
		{
			return app.UseStageMarker(stage.ToString());
		}

		// Token: 0x04000072 RID: 114
		private const string IntegratedPipelineStageMarker = "integratedpipeline.StageMarker";
	}
}
