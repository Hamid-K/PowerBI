using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Protocols
{
	// Token: 0x02000002 RID: 2
	public abstract class AuthenticationProtocolMessage
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020A8 File Offset: 0x000002A8
		public virtual string BuildFormPost()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<html><head><title>");
			stringBuilder.Append(WebUtility.HtmlEncode(this.PostTitle));
			stringBuilder.Append("</title></head><body><form method=\"POST\" name=\"hiddenform\" action=\"");
			stringBuilder.Append(WebUtility.HtmlEncode(this.IssuerAddress));
			stringBuilder.Append("\">");
			foreach (KeyValuePair<string, string> keyValuePair in this._parameters)
			{
				stringBuilder.Append("<input type=\"hidden\" name=\"");
				stringBuilder.Append(WebUtility.HtmlEncode(keyValuePair.Key));
				stringBuilder.Append("\" value=\"");
				stringBuilder.Append(WebUtility.HtmlEncode(keyValuePair.Value));
				stringBuilder.Append("\" />");
			}
			stringBuilder.Append("<noscript><p>");
			stringBuilder.Append(WebUtility.HtmlEncode(this.ScriptDisabledText));
			stringBuilder.Append("</p><input type=\"submit\" value=\"");
			stringBuilder.Append(WebUtility.HtmlEncode(this.ScriptButtonText));
			stringBuilder.Append("\" /></noscript>");
			stringBuilder.Append("</form>");
			stringBuilder.Append(this.Script);
			stringBuilder.Append("</body></html>");
			return stringBuilder.ToString();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002200 File Offset: 0x00000400
		public virtual string BuildRedirectUrl()
		{
			StringBuilder stringBuilder = new StringBuilder(this._issuerAddress);
			bool flag = this._issuerAddress.Contains("?");
			foreach (KeyValuePair<string, string> keyValuePair in this._parameters)
			{
				if (keyValuePair.Value != null)
				{
					if (!flag)
					{
						stringBuilder.Append('?');
						flag = true;
					}
					else
					{
						stringBuilder.Append('&');
					}
					stringBuilder.Append(Uri.EscapeDataString(keyValuePair.Key));
					stringBuilder.Append('=');
					stringBuilder.Append(Uri.EscapeDataString(keyValuePair.Value));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000022C0 File Offset: 0x000004C0
		public virtual string GetParameter(string parameter)
		{
			if (string.IsNullOrEmpty(parameter))
			{
				throw LogHelper.LogArgumentNullException("parameter");
			}
			string text = null;
			this._parameters.TryGetValue(parameter, out text);
			return text;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000022F2 File Offset: 0x000004F2
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000022FA File Offset: 0x000004FA
		public string IssuerAddress
		{
			get
			{
				return this._issuerAddress;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("IssuerAddress");
				}
				this._issuerAddress = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002311 File Offset: 0x00000511
		public IDictionary<string, string> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002319 File Offset: 0x00000519
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002321 File Offset: 0x00000521
		public string PostTitle
		{
			get
			{
				return this._postTitle;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("PostTitle");
				}
				this._postTitle = value;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002338 File Offset: 0x00000538
		public virtual void RemoveParameter(string parameter)
		{
			if (string.IsNullOrEmpty(parameter))
			{
				throw LogHelper.LogArgumentNullException("parameter");
			}
			if (this._parameters.ContainsKey(parameter))
			{
				this._parameters.Remove(parameter);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002368 File Offset: 0x00000568
		public void SetParameter(string parameter, string value)
		{
			if (string.IsNullOrEmpty(parameter))
			{
				throw LogHelper.LogArgumentNullException("parameter");
			}
			if (value == null)
			{
				this.RemoveParameter(parameter);
				return;
			}
			this._parameters[parameter] = value;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002398 File Offset: 0x00000598
		public virtual void SetParameters(NameValueCollection nameValueCollection)
		{
			if (nameValueCollection == null)
			{
				return;
			}
			foreach (string text in nameValueCollection.AllKeys)
			{
				this.SetParameter(text, nameValueCollection[text]);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000023D0 File Offset: 0x000005D0
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000023D8 File Offset: 0x000005D8
		public string Script
		{
			get
			{
				return this._script;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("Script");
				}
				this._script = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000023EF File Offset: 0x000005EF
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000023F7 File Offset: 0x000005F7
		public string ScriptButtonText
		{
			get
			{
				return this._scriptButtonText;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("ScriptButtonText");
				}
				this._scriptButtonText = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000240E File Offset: 0x0000060E
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002416 File Offset: 0x00000616
		public string ScriptDisabledText
		{
			get
			{
				return this._scriptDisabledText;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("ScriptDisabledText");
				}
				this._scriptDisabledText = value;
			}
		}

		// Token: 0x04000001 RID: 1
		private string _postTitle = "Working...";

		// Token: 0x04000002 RID: 2
		private string _script = "<script language=\"javascript\">window.setTimeout(function() {document.forms[0].submit();}, 0);</script>";

		// Token: 0x04000003 RID: 3
		private string _scriptButtonText = "Submit";

		// Token: 0x04000004 RID: 4
		private string _scriptDisabledText = "Script is disabled. Click Submit to continue.";

		// Token: 0x04000005 RID: 5
		private Dictionary<string, string> _parameters = new Dictionary<string, string>();

		// Token: 0x04000006 RID: 6
		private string _issuerAddress = string.Empty;
	}
}
