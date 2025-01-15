using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.OData
{
	// Token: 0x02000087 RID: 135
	public class ODataPreferenceHeader
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x0000E8BC File Offset: 0x0000CABC
		internal ODataPreferenceHeader(IODataRequestMessage requestMessage)
		{
			this.message = new ODataRequestMessage(requestMessage, true, false, -1L);
			this.preferenceHeaderName = "Prefer";
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000E8DF File Offset: 0x0000CADF
		internal ODataPreferenceHeader(IODataResponseMessage responseMessage)
		{
			this.message = new ODataResponseMessage(responseMessage, true, false, -1L);
			this.preferenceHeaderName = "Preference-Applied";
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000E904 File Offset: 0x0000CB04
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0000E970 File Offset: 0x0000CB70
		public bool? ReturnContent
		{
			get
			{
				HttpHeaderValueElement httpHeaderValueElement = this.Get("return");
				if (httpHeaderValueElement != null && httpHeaderValueElement.Value != null)
				{
					if (httpHeaderValueElement.Value.ToLowerInvariant().Equals("representation"))
					{
						return new bool?(true);
					}
					if (httpHeaderValueElement.Value.ToLowerInvariant().Equals("minimal"))
					{
						return new bool?(false);
					}
				}
				return default(bool?);
			}
			set
			{
				this.Clear("return");
				if (value == true)
				{
					this.Set(ODataPreferenceHeader.ReturnRepresentationPreference);
				}
				if (value == false)
				{
					this.Set(ODataPreferenceHeader.ReturnMinimalPreference);
				}
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0000EA10 File Offset: 0x0000CC10
		public string AnnotationFilter
		{
			get
			{
				HttpHeaderValueElement httpHeaderValueElement = this.Get("odata.include-annotations");
				if (httpHeaderValueElement != null && httpHeaderValueElement.Value != null)
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

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000EA47 File Offset: 0x0000CC47
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0000EA57 File Offset: 0x0000CC57
		public bool RespondAsync
		{
			get
			{
				return this.Get("respond-async") != null;
			}
			set
			{
				if (value)
				{
					this.Set(ODataPreferenceHeader.RespondAsyncPreference);
					return;
				}
				this.Clear("respond-async");
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000EA74 File Offset: 0x0000CC74
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0000EAE4 File Offset: 0x0000CCE4
		public int? Wait
		{
			get
			{
				HttpHeaderValueElement httpHeaderValueElement = this.Get("wait");
				if (httpHeaderValueElement == null || httpHeaderValueElement.Value == null)
				{
					return default(int?);
				}
				int num;
				if (int.TryParse(httpHeaderValueElement.Value, ref num))
				{
					return new int?(num);
				}
				throw new ODataException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for {1} preference header found. The {1} preference header requires an integer value.", new object[] { httpHeaderValueElement.Value, "wait" }));
			}
			set
			{
				if (value != null)
				{
					this.Set(new HttpHeaderValueElement("wait", string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { value }), ODataPreferenceHeader.EmptyParameters));
					return;
				}
				this.Clear("wait");
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000EB39 File Offset: 0x0000CD39
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0000EB49 File Offset: 0x0000CD49
		public bool ContinueOnError
		{
			get
			{
				return this.Get("odata.continue-on-error") != null;
			}
			set
			{
				if (value)
				{
					this.Set(ODataPreferenceHeader.ContinueOnErrorPreference);
					return;
				}
				this.Clear("odata.continue-on-error");
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000EB68 File Offset: 0x0000CD68
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x0000EBD8 File Offset: 0x0000CDD8
		public int? MaxPageSize
		{
			get
			{
				HttpHeaderValueElement httpHeaderValueElement = this.Get("odata.maxpagesize");
				if (httpHeaderValueElement == null || httpHeaderValueElement.Value == null)
				{
					return default(int?);
				}
				int num;
				if (int.TryParse(httpHeaderValueElement.Value, ref num))
				{
					return new int?(num);
				}
				throw new ODataException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for {1} preference header found. The {1} preference header requires an integer value.", new object[] { httpHeaderValueElement.Value, "odata.maxpagesize" }));
			}
			set
			{
				if (value != null)
				{
					this.Set(new HttpHeaderValueElement("odata.maxpagesize", string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { value.Value }), ODataPreferenceHeader.EmptyParameters));
					return;
				}
				this.Clear("odata.maxpagesize");
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0000EC33 File Offset: 0x0000CE33
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0000EC43 File Offset: 0x0000CE43
		public bool TrackChanges
		{
			get
			{
				return this.Get("odata.track-changes") != null;
			}
			set
			{
				if (value)
				{
					this.Set(ODataPreferenceHeader.TrackChangesPreference);
					return;
				}
				this.Clear("odata.track-changes");
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000EC60 File Offset: 0x0000CE60
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

		// Token: 0x0600054A RID: 1354 RVA: 0x0000EC86 File Offset: 0x0000CE86
		protected void Clear(string preference)
		{
			if (this.Preferences.Remove(preference))
			{
				this.SetPreferencesToMessageHeader();
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		protected void Set(HttpHeaderValueElement preference)
		{
			this.Preferences[preference.Name] = preference;
			this.SetPreferencesToMessageHeader();
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
		protected HttpHeaderValueElement Get(string preferenceName)
		{
			HttpHeaderValueElement httpHeaderValueElement;
			if (!this.Preferences.TryGetValue(preferenceName, ref httpHeaderValueElement))
			{
				return null;
			}
			return httpHeaderValueElement;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		private static string AddQuotes(string text)
		{
			return "\"" + text + "\"";
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		private HttpHeaderValue ParsePreferences()
		{
			string header = this.message.GetHeader(this.preferenceHeaderName);
			HttpHeaderValueLexer httpHeaderValueLexer = HttpHeaderValueLexer.Create(this.preferenceHeaderName, header);
			return httpHeaderValueLexer.ToHttpHeaderValue();
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000ED1E File Offset: 0x0000CF1E
		private void SetPreferencesToMessageHeader()
		{
			this.message.SetHeader(this.preferenceHeaderName, this.Preferences.ToString());
		}

		// Token: 0x0400027A RID: 634
		private const string ReturnPreferenceTokenName = "return";

		// Token: 0x0400027B RID: 635
		private const string ReturnRepresentationPreferenceTokenValue = "representation";

		// Token: 0x0400027C RID: 636
		private const string ReturnMinimalPreferenceTokenValue = "minimal";

		// Token: 0x0400027D RID: 637
		private const string ODataAnnotationPreferenceToken = "odata.include-annotations";

		// Token: 0x0400027E RID: 638
		private const string RespondAsyncPreferenceToken = "respond-async";

		// Token: 0x0400027F RID: 639
		private const string WaitPreferenceTokenName = "wait";

		// Token: 0x04000280 RID: 640
		private const string ODataContinueOnErrorPreferenceToken = "odata.continue-on-error";

		// Token: 0x04000281 RID: 641
		private const string ODataMaxPageSizePreferenceToken = "odata.maxpagesize";

		// Token: 0x04000282 RID: 642
		private const string ODataTrackChangesPreferenceToken = "odata.track-changes";

		// Token: 0x04000283 RID: 643
		private const string PreferHeaderName = "Prefer";

		// Token: 0x04000284 RID: 644
		private const string PreferenceAppliedHeaderName = "Preference-Applied";

		// Token: 0x04000285 RID: 645
		private static readonly KeyValuePair<string, string>[] EmptyParameters = new KeyValuePair<string, string>[0];

		// Token: 0x04000286 RID: 646
		private static readonly HttpHeaderValueElement ContinueOnErrorPreference = new HttpHeaderValueElement("odata.continue-on-error", null, ODataPreferenceHeader.EmptyParameters);

		// Token: 0x04000287 RID: 647
		private static readonly HttpHeaderValueElement ReturnMinimalPreference = new HttpHeaderValueElement("return", "minimal", ODataPreferenceHeader.EmptyParameters);

		// Token: 0x04000288 RID: 648
		private static readonly HttpHeaderValueElement ReturnRepresentationPreference = new HttpHeaderValueElement("return", "representation", ODataPreferenceHeader.EmptyParameters);

		// Token: 0x04000289 RID: 649
		private static readonly HttpHeaderValueElement RespondAsyncPreference = new HttpHeaderValueElement("respond-async", null, ODataPreferenceHeader.EmptyParameters);

		// Token: 0x0400028A RID: 650
		private static readonly HttpHeaderValueElement TrackChangesPreference = new HttpHeaderValueElement("odata.track-changes", null, ODataPreferenceHeader.EmptyParameters);

		// Token: 0x0400028B RID: 651
		private readonly ODataMessage message;

		// Token: 0x0400028C RID: 652
		private readonly string preferenceHeaderName;

		// Token: 0x0400028D RID: 653
		private HttpHeaderValue preferences;
	}
}
