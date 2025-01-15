using System;

namespace Microsoft.InfoNav.Utils
{
	// Token: 0x0200002F RID: 47
	public static class WrapperExtensions
	{
		// Token: 0x0600023C RID: 572 RVA: 0x000070E8 File Offset: 0x000052E8
		public static IContainsMarkedInformation WrapAsMarked<T>(this T item, Func<string, string> marker)
		{
			return new WrapperExtensions.MarkedWrapper<T>(item, marker);
		}

		// Token: 0x020000B6 RID: 182
		public class MarkedWrapper<T> : IContainsMarkedInformation
		{
			// Token: 0x060005AE RID: 1454 RVA: 0x0000ED6F File Offset: 0x0000CF6F
			internal MarkedWrapper(T item, Func<string, string> marker)
			{
				this.m_wrapped = item;
				this.m_marker = marker;
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x0000ED85 File Offset: 0x0000CF85
			public string BuildMarkedString()
			{
				return this.m_marker(this.m_wrapped.ToString());
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x0000EDA3 File Offset: 0x0000CFA3
			public override string ToString()
			{
				if (this.m_wrapped == null)
				{
					return string.Empty;
				}
				return this.m_wrapped.ToString();
			}

			// Token: 0x040001D1 RID: 465
			private T m_wrapped;

			// Token: 0x040001D2 RID: 466
			private Func<string, string> m_marker;
		}
	}
}
