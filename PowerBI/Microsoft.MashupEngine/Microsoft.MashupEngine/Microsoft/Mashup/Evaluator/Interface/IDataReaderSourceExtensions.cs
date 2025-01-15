using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E3A RID: 7738
	public static class IDataReaderSourceExtensions
	{
		// Token: 0x0600BE5F RID: 48735 RVA: 0x002681B8 File Offset: 0x002663B8
		public static IDataReaderSource OnDispose(this IDataReaderSource source, Action action)
		{
			return new NotifyingDataReaderSource(source, action);
		}

		// Token: 0x0600BE60 RID: 48736 RVA: 0x002681C4 File Offset: 0x002663C4
		public static IDataReaderSource AfterDispose(this IDataReaderSource source, Action action)
		{
			return new NotifyingDataReaderSource(source, delegate
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
