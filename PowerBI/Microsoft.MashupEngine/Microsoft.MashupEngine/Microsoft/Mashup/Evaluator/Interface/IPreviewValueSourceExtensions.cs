using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E3D RID: 7741
	public static class IPreviewValueSourceExtensions
	{
		// Token: 0x0600BE67 RID: 48743 RVA: 0x0026829A File Offset: 0x0026649A
		public static IPreviewValueSource OnDispose(this IPreviewValueSource source, Action action)
		{
			return new NotifyingPreviewValueSource(source, action);
		}

		// Token: 0x0600BE68 RID: 48744 RVA: 0x002682A4 File Offset: 0x002664A4
		public static IPreviewValueSource AfterDispose(this IPreviewValueSource source, Action action)
		{
			return new NotifyingPreviewValueSource(source, delegate
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
