using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Privacy
{
	// Token: 0x020000D1 RID: 209
	[NullableContext(1)]
	[Nullable(0)]
	public static class WrapperExtensions
	{
		// Token: 0x0600107E RID: 4222 RVA: 0x000456A1 File Offset: 0x000438A1
		public static IContainsMarkedInformation WrapAsMarked<[Nullable(2)] T>(this T item, Func<string, string> marker)
		{
			return new WrapperExtensions.MarkedWrapper<T>(item, marker);
		}

		// Token: 0x020000E6 RID: 230
		[Nullable(0)]
		public class MarkedWrapper<[Nullable(2)] T> : IContainsMarkedInformation
		{
			// Token: 0x0600112C RID: 4396 RVA: 0x00046B33 File Offset: 0x00044D33
			internal MarkedWrapper(T item, Func<string, string> marker)
			{
				this.m_wrapped = item;
				this.m_marker = marker;
			}

			// Token: 0x0600112D RID: 4397 RVA: 0x00046B49 File Offset: 0x00044D49
			public string BuildMarkedString()
			{
				return this.m_marker(this.m_wrapped.ToString());
			}

			// Token: 0x0600112E RID: 4398 RVA: 0x00046B67 File Offset: 0x00044D67
			public override string ToString()
			{
				if (this.m_wrapped == null)
				{
					return string.Empty;
				}
				return this.m_wrapped.ToString();
			}

			// Token: 0x04000370 RID: 880
			private T m_wrapped;

			// Token: 0x04000371 RID: 881
			private Func<string, string> m_marker;
		}
	}
}
