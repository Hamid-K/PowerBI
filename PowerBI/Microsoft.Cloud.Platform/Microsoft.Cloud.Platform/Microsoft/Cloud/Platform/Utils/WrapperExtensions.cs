using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200022B RID: 555
	public static class WrapperExtensions
	{
		// Token: 0x06000E89 RID: 3721 RVA: 0x000330D0 File Offset: 0x000312D0
		public static IContainsMarkedInformation WrapAsMarked<T>(this T item, Func<string, string> marker)
		{
			return new WrapperExtensions.MarkedWrapper<T>(item, marker);
		}

		// Token: 0x020006C8 RID: 1736
		public class MarkedWrapper<T> : IContainsMarkedInformation
		{
			// Token: 0x06002E6F RID: 11887 RVA: 0x000A2218 File Offset: 0x000A0418
			internal MarkedWrapper(T item, Func<string, string> marker)
			{
				this.m_wrapped = item;
				this.m_marker = marker;
			}

			// Token: 0x06002E70 RID: 11888 RVA: 0x000A222E File Offset: 0x000A042E
			public string BuildMarkedString()
			{
				return this.m_marker(this.m_wrapped.ToString());
			}

			// Token: 0x06002E71 RID: 11889 RVA: 0x000A224C File Offset: 0x000A044C
			public override string ToString()
			{
				if (this.m_wrapped == null)
				{
					return string.Empty;
				}
				return this.m_wrapped.ToString();
			}

			// Token: 0x04001348 RID: 4936
			private T m_wrapped;

			// Token: 0x04001349 RID: 4937
			private Func<string, string> m_marker;
		}
	}
}
