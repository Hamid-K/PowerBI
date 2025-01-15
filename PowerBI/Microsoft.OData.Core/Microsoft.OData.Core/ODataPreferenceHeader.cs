using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.OData
{
	// Token: 0x020000AC RID: 172
	public class ODataPreferenceHeader
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x00011FE0 File Offset: 0x000101E0
		internal ODataPreferenceHeader(IODataRequestMessage requestMessage)
		{
			this.message = new ODataRequestMessage(requestMessage, true, false, -1L);
			this.preferenceHeaderName = "Prefer";
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00012003 File Offset: 0x00010203
		internal ODataPreferenceHeader(IODataResponseMessage responseMessage)
		{
			this.message = new ODataResponseMessage(responseMessage, true, false, -1L);
			this.preferenceHeaderName = "Preference-Applied";
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00012028 File Offset: 0x00010228
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x00012094 File Offset: 0x00010294
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
				return null;
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

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x000120F8 File Offset: 0x000102F8
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x00012134 File Offset: 0x00010334
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

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001216B File Offset: 0x0001036B
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x0001217B File Offset: 0x0001037B
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

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00012198 File Offset: 0x00010398
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x00012208 File Offset: 0x00010408
		public int? Wait
		{
			get
			{
				HttpHeaderValueElement httpHeaderValueElement = this.Get("wait");
				if (httpHeaderValueElement == null || httpHeaderValueElement.Value == null)
				{
					return null;
				}
				int num;
				if (int.TryParse(httpHeaderValueElement.Value, out num))
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

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0001225D File Offset: 0x0001045D
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x0001226D File Offset: 0x0001046D
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

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0001228C File Offset: 0x0001048C
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x000122FC File Offset: 0x000104FC
		public int? MaxPageSize
		{
			get
			{
				HttpHeaderValueElement httpHeaderValueElement = this.Get("odata.maxpagesize");
				if (httpHeaderValueElement == null || httpHeaderValueElement.Value == null)
				{
					return null;
				}
				int num;
				if (int.TryParse(httpHeaderValueElement.Value, out num))
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

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00012357 File Offset: 0x00010557
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x00012367 File Offset: 0x00010567
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

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00012384 File Offset: 0x00010584
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

		// Token: 0x0600078A RID: 1930 RVA: 0x000123AA File Offset: 0x000105AA
		protected void Clear(string preference)
		{
			if (this.Preferences.Remove(preference))
			{
				this.SetPreferencesToMessageHeader();
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000123C0 File Offset: 0x000105C0
		protected void Set(HttpHeaderValueElement preference)
		{
			this.Preferences[preference.Name] = preference;
			this.SetPreferencesToMessageHeader();
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000123DC File Offset: 0x000105DC
		protected HttpHeaderValueElement Get(string preferenceName)
		{
			HttpHeaderValueElement httpHeaderValueElement;
			if (!this.Preferences.TryGetValue(preferenceName, out httpHeaderValueElement))
			{
				return null;
			}
			return httpHeaderValueElement;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000123FC File Offset: 0x000105FC
		private static string AddQuotes(string text)
		{
			return "\"" + text + "\"";
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00012410 File Offset: 0x00010610
		private HttpHeaderValue ParsePreferences()
		{
			string header = this.message.GetHeader(this.preferenceHeaderName);
			HttpHeaderValueLexer httpHeaderValueLexer = HttpHeaderValueLexer.Create(this.preferenceHeaderName, header);
			return httpHeaderValueLexer.ToHttpHeaderValue();
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00012442 File Offset: 0x00010642
		private void SetPreferencesToMessageHeader()
		{
			this.message.SetHeader(this.preferenceHeaderName, this.Preferences.ToString());
		}

		// Token: 0x040002E0 RID: 736
		private const string ReturnPreferenceTokenName = "return";

		// Token: 0x040002E1 RID: 737
		private const string ReturnRepresentationPreferenceTokenValue = "representation";

		// Token: 0x040002E2 RID: 738
		private const string ReturnMinimalPreferenceTokenValue = "minimal";

		// Token: 0x040002E3 RID: 739
		private const string ODataAnnotationPreferenceToken = "odata.include-annotations";

		// Token: 0x040002E4 RID: 740
		private const string RespondAsyncPreferenceToken = "respond-async";

		// Token: 0x040002E5 RID: 741
		private const string WaitPreferenceTokenName = "wait";

		// Token: 0x040002E6 RID: 742
		private const string ODataContinueOnErrorPreferenceToken = "odata.continue-on-error";

		// Token: 0x040002E7 RID: 743
		private const string ODataMaxPageSizePreferenceToken = "odata.maxpagesize";

		// Token: 0x040002E8 RID: 744
		private const string ODataTrackChangesPreferenceToken = "odata.track-changes";

		// Token: 0x040002E9 RID: 745
		private const string PreferHeaderName = "Prefer";

		// Token: 0x040002EA RID: 746
		private const string PreferenceAppliedHeaderName = "Preference-Applied";

		// Token: 0x040002EB RID: 747
		private static readonly KeyValuePair<string, string>[] EmptyParameters = new KeyValuePair<string, string>[0];

		// Token: 0x040002EC RID: 748
		private static readonly HttpHeaderValueElement ContinueOnErrorPreference = new HttpHeaderValueElement("odata.continue-on-error", null, ODataPreferenceHeader.EmptyParameters);

		// Token: 0x040002ED RID: 749
		private static readonly HttpHeaderValueElement ReturnMinimalPreference = new HttpHeaderValueElement("return", "minimal", ODataPreferenceHeader.EmptyParameters);

		// Token: 0x040002EE RID: 750
		private static readonly HttpHeaderValueElement ReturnRepresentationPreference = new HttpHeaderValueElement("return", "representation", ODataPreferenceHeader.EmptyParameters);

		// Token: 0x040002EF RID: 751
		private static readonly HttpHeaderValueElement RespondAsyncPreference = new HttpHeaderValueElement("respond-async", null, ODataPreferenceHeader.EmptyParameters);

		// Token: 0x040002F0 RID: 752
		private static readonly HttpHeaderValueElement TrackChangesPreference = new HttpHeaderValueElement("odata.track-changes", null, ODataPreferenceHeader.EmptyParameters);

		// Token: 0x040002F1 RID: 753
		private readonly ODataMessage message;

		// Token: 0x040002F2 RID: 754
		private readonly string preferenceHeaderName;

		// Token: 0x040002F3 RID: 755
		private HttpHeaderValue preferences;
	}
}
