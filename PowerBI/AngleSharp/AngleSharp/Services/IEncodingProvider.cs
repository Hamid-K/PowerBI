using System;
using System.Text;

namespace AngleSharp.Services
{
	// Token: 0x0200002B RID: 43
	public interface IEncodingProvider
	{
		// Token: 0x0600012E RID: 302
		Encoding Suggest(string locale);
	}
}
