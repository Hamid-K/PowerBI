using System;
using System.Globalization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000BF RID: 191
	internal class StringGenerator
	{
		// Token: 0x06000918 RID: 2328 RVA: 0x000296BF File Offset: 0x000278BF
		internal StringGenerator(string prefix, int maxLength)
		{
			if (prefix.Length > maxLength)
			{
				prefix = prefix.Substring(0, maxLength);
			}
			this.prefix = prefix;
			this.counter = 0;
			this.maxLength = maxLength;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x000296F0 File Offset: 0x000278F0
		internal string Next
		{
			get
			{
				this.counter++;
				string text = this.prefix + " " + this.counter.ToString(CultureInfo.InvariantCulture);
				if (text.Length > this.maxLength)
				{
					this.counter = 0;
					text = (this.prefix = Utils.Trim(this.prefix.Substring(0, this.prefix.Length - 1)));
				}
				return text;
			}
		}

		// Token: 0x04000515 RID: 1301
		private string prefix;

		// Token: 0x04000516 RID: 1302
		private int counter;

		// Token: 0x04000517 RID: 1303
		private int maxLength;
	}
}
