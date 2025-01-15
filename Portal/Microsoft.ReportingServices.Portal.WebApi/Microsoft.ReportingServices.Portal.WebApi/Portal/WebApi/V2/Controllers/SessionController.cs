using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.BIServer.Configuration;

namespace Microsoft.ReportingServices.Portal.WebApi.V2.Controllers
{
	// Token: 0x02000009 RID: 9
	public sealed class SessionController : ApiController
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000217B File Offset: 0x0000037B
		public SessionController(ServerConfiguration serverConfiguration)
		{
			this._serverConfiguration = serverConfiguration;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000218A File Offset: 0x0000038A
		[Route("~/api/v2.0/session")]
		[HttpPost]
		public IHttpActionResult Logon()
		{
			return this.BadRequest("Logon can only be called in a server configured for Custom Authentication");
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002198 File Offset: 0x00000398
		[Route("~/api/v2.0/session")]
		[HttpDelete]
		public IHttpActionResult Logoff()
		{
			if (!string.IsNullOrEmpty(this._serverConfiguration.FormsCookieName))
			{
				HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
				CookieHeaderValue cookieHeaderValue = new CookieHeaderValue(this._serverConfiguration.FormsCookieName, string.Empty);
				cookieHeaderValue.Path = this._serverConfiguration.FormsCookiePath;
				cookieHeaderValue.Expires = new DateTimeOffset?(DateTimeOffset.Now.AddMinutes(1.0));
				cookieHeaderValue.HttpOnly = true;
				List<CookieHeaderValue> list = new List<CookieHeaderValue>();
				list.Add(cookieHeaderValue);
				httpResponseMessage.Headers.AddCookies(list);
				return this.ResponseMessage(httpResponseMessage);
			}
			return this.BadRequest();
		}

		// Token: 0x04000039 RID: 57
		private readonly ServerConfiguration _serverConfiguration;
	}
}
