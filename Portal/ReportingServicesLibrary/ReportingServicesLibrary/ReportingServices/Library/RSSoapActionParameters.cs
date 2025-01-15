using System;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000A5 RID: 165
	internal abstract class RSSoapActionParameters
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0001FD23 File Offset: 0x0001DF23
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0001FD2F File Offset: 0x0001DF2F
		internal Regex ForbiddenCharsCombinations
		{
			get
			{
				return new Regex("[/@$&*+=<>:\"?|\\\\]|' *\\)");
			}
			private set
			{
				this.ForbiddenCharsCombinations = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00005C88 File Offset: 0x00003E88
		internal virtual string InputTrace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00005C88 File Offset: 0x00003E88
		internal virtual string OutputTrace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007AB RID: 1963
		internal abstract void Validate();
	}
}
