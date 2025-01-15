using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000007 RID: 7
	public static class WrapperExtensions
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002067 File Offset: 0x00000267
		public static IContainsMarkedInformation WrapAsMarked<T>(this T item, Func<string, string> marker)
		{
			return new WrapperExtensions.MarkedWrapper<T>(item, marker);
		}

		// Token: 0x0200003B RID: 59
		public class MarkedWrapper<T> : IContainsMarkedInformation
		{
			// Token: 0x060000E5 RID: 229 RVA: 0x00003BAB File Offset: 0x00001DAB
			internal MarkedWrapper(T item, Func<string, string> marker)
			{
				this.m_wrapped = item;
				this.m_marker = marker;
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x00003BC1 File Offset: 0x00001DC1
			public string BuildMarkedString()
			{
				return this.m_marker(this.m_wrapped.ToString());
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x00003BDF File Offset: 0x00001DDF
			public override string ToString()
			{
				if (this.m_wrapped == null)
				{
					return string.Empty;
				}
				return this.m_wrapped.ToString();
			}

			// Token: 0x0400008B RID: 139
			private T m_wrapped;

			// Token: 0x0400008C RID: 140
			private Func<string, string> m_marker;
		}
	}
}
