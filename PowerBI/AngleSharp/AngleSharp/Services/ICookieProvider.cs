using System;

namespace AngleSharp.Services
{
	// Token: 0x02000027 RID: 39
	public interface ICookieProvider
	{
		// Token: 0x0600011F RID: 287
		string GetCookie(string origin);

		// Token: 0x06000120 RID: 288
		void SetCookie(string origin, string value);
	}
}
