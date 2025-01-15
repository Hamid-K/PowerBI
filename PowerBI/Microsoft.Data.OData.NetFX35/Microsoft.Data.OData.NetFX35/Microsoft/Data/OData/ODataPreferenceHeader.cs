using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x02000136 RID: 310
	public sealed class ODataPreferenceHeader
	{
		// Token: 0x060007FD RID: 2045 RVA: 0x0001A2A3 File Offset: 0x000184A3
		internal ODataPreferenceHeader(IODataRequestMessage requestMessage)
		{
			this.message = new ODataRequestMessage(requestMessage, true, false, -1L);
			this.preferenceHeaderName = "Prefer";
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001A2C6 File Offset: 0x000184C6
		internal ODataPreferenceHeader(IODataResponseMessage responseMessage)
		{
			this.message = new ODataResponseMessage(responseMessage, true, false, -1L);
			this.preferenceHeaderName = "Preference-Applied";
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0001A2EC File Offset: 0x000184EC
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x0001A32C File Offset: 0x0001852C
		public bool? ReturnContent
		{
			get
			{
				if (this.PreferenceExists("return-content"))
				{
					return new bool?(true);
				}
				if (this.PreferenceExists("return-no-content"))
				{
					return new bool?(false);
				}
				return default(bool?);
			}
			set
			{
				this.Clear("return-content");
				this.Clear("return-no-content");
				if (value == true)
				{
					this.Set(ODataPreferenceHeader.ReturnContentPreference);
				}
				if (value == false)
				{
					this.Set(ODataPreferenceHeader.ReturnNoContentPreference);
				}
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0001A394 File Offset: 0x00018594
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x0001A3CA File Offset: 0x000185CA
		public string AnnotationFilter
		{
			get
			{
				HttpHeaderValueElement httpHeaderValueElement = this.Get("odata.include-annotations");
				if (httpHeaderValueElement != null)
				{
					return httpHeaderValueElement.Value.Trim(new char[] { '"' });
				}
				return null;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotEmpty(value, "AnnotationFilter");
				if (value == null)
				{
					this.Clear("odata.include-annotations");
					return;
				}
				this.Set(new HttpHeaderValueElement("odata.include-annotations", ODataPreferenceHeader.AddQuotes(value), ODataPreferenceHeader.EmptyParameters));
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0001A404 File Offset: 0x00018604
		private HttpHeaderValue Preferences
		{
			get
			{
				HttpHeaderValue httpHeaderValue;
				if ((httpHeaderValue = this.preferences) == null)
				{
					httpHeaderValue = (this.preferences = this.ParsePreferences());
				}
				return httpHeaderValue;
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001A42A File Offset: 0x0001862A
		private static string AddQuotes(string text)
		{
			return "\"" + text + "\"";
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001A43C File Offset: 0x0001863C
		private bool PreferenceExists(string preference)
		{
			return this.Get(preference) != null;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001A44B File Offset: 0x0001864B
		private void Clear(string preference)
		{
			if (this.Preferences.Remove(preference))
			{
				this.SetPreferencesToMessageHeader();
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001A461 File Offset: 0x00018661
		private void Set(HttpHeaderValueElement preference)
		{
			this.Preferences[preference.Name] = preference;
			this.SetPreferencesToMessageHeader();
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001A47C File Offset: 0x0001867C
		private HttpHeaderValueElement Get(string preferenceName)
		{
			HttpHeaderValueElement httpHeaderValueElement;
			if (!this.Preferences.TryGetValue(preferenceName, ref httpHeaderValueElement))
			{
				return null;
			}
			return httpHeaderValueElement;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001A49C File Offset: 0x0001869C
		private HttpHeaderValue ParsePreferences()
		{
			string header = this.message.GetHeader(this.preferenceHeaderName);
			HttpHeaderValueLexer httpHeaderValueLexer = HttpHeaderValueLexer.Create(this.preferenceHeaderName, header);
			return httpHeaderValueLexer.ToHttpHeaderValue();
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001A4CE File Offset: 0x000186CE
		private void SetPreferencesToMessageHeader()
		{
			this.message.SetHeader(this.preferenceHeaderName, this.Preferences.ToString());
		}

		// Token: 0x04000315 RID: 789
		private const string ReturnNoContentPreferenceToken = "return-no-content";

		// Token: 0x04000316 RID: 790
		private const string ReturnContentPreferenceToken = "return-content";

		// Token: 0x04000317 RID: 791
		private const string ODataAnnotationPreferenceToken = "odata.include-annotations";

		// Token: 0x04000318 RID: 792
		private const string PreferHeaderName = "Prefer";

		// Token: 0x04000319 RID: 793
		private const string PreferenceAppliedHeaderName = "Preference-Applied";

		// Token: 0x0400031A RID: 794
		private static readonly KeyValuePair<string, string>[] EmptyParameters = new KeyValuePair<string, string>[0];

		// Token: 0x0400031B RID: 795
		private static readonly HttpHeaderValueElement ReturnNoContentPreference = new HttpHeaderValueElement("return-no-content", null, ODataPreferenceHeader.EmptyParameters);

		// Token: 0x0400031C RID: 796
		private static readonly HttpHeaderValueElement ReturnContentPreference = new HttpHeaderValueElement("return-content", null, ODataPreferenceHeader.EmptyParameters);

		// Token: 0x0400031D RID: 797
		private readonly ODataMessage message;

		// Token: 0x0400031E RID: 798
		private readonly string preferenceHeaderName;

		// Token: 0x0400031F RID: 799
		private HttpHeaderValue preferences;
	}
}
