using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E40 RID: 7744
	public static class IStreamSourceExtensions
	{
		// Token: 0x0600BE71 RID: 48753 RVA: 0x00268394 File Offset: 0x00266594
		public static IStreamSource OnDispose(this IStreamSource source, Action action)
		{
			return new NotifyingStreamSource(source, action);
		}

		// Token: 0x0600BE72 RID: 48754 RVA: 0x002683A0 File Offset: 0x002665A0
		public static IStreamSource AfterDispose(this IStreamSource source, Action action)
		{
			return new NotifyingStreamSource(source, delegate
			{
				try
				{
					source.Dispose();
				}
				finally
				{
					action();
				}
			});
		}
	}
}
