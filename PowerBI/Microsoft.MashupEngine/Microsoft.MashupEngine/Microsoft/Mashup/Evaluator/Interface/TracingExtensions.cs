using System;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E4A RID: 7754
	public static class TracingExtensions
	{
		// Token: 0x0600BE8E RID: 48782 RVA: 0x0026886E File Offset: 0x00266A6E
		public static IPreviewValueSource TraceTo(this IPreviewValueSource previewValueSource, IHostTrace trace)
		{
			return new TracingPreviewValueSource(previewValueSource, trace);
		}
	}
}
