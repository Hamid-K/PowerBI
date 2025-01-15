using System;

namespace Microsoft.PowerBI.Telemetry.PIIUtils
{
	// Token: 0x02000034 RID: 52
	public static class WrapperExtensions
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000048A9 File Offset: 0x00002AA9
		public static IContainsMarkedInformation WrapAsMarked<T>(this T item, Func<string, string> marker)
		{
			return new WrapperExtensions.MarkedWrapper<T>(item, marker);
		}

		// Token: 0x02000043 RID: 67
		public class MarkedWrapper<T> : IContainsMarkedInformation
		{
			// Token: 0x060001A2 RID: 418 RVA: 0x00005F9A File Offset: 0x0000419A
			internal MarkedWrapper(T item, Func<string, string> marker)
			{
				this.m_wrapped = item;
				this.m_marker = marker;
			}

			// Token: 0x060001A3 RID: 419 RVA: 0x00005FB0 File Offset: 0x000041B0
			public string BuildMarkedString()
			{
				return this.m_marker(this.m_wrapped.ToString());
			}

			// Token: 0x060001A4 RID: 420 RVA: 0x00005FCE File Offset: 0x000041CE
			public override string ToString()
			{
				if (this.m_wrapped == null)
				{
					return string.Empty;
				}
				return this.m_wrapped.ToString();
			}

			// Token: 0x040000F3 RID: 243
			private T m_wrapped;

			// Token: 0x040000F4 RID: 244
			private Func<string, string> m_marker;
		}
	}
}
