using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.OData.Core
{
	// Token: 0x02000191 RID: 401
	public sealed class ODataPreferenceHeader
	{
		// Token: 0x06000F10 RID: 3856 RVA: 0x00034C10 File Offset: 0x00032E10
		internal ODataPreferenceHeader(IODataRequestMessage requestMessage)
		{
			this.message = new ODataRequestMessage(requestMessage, true, false, -1L);
			this.preferenceHeaderName = "Prefer";
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00034C33 File Offset: 0x00032E33
		internal ODataPreferenceHeader(IODataResponseMessage responseMessage)
		{
			this.message = new ODataResponseMessage(responseMessage, true, false, -1L);
			this.preferenceHeaderName = "Preference-Applied";
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x00034C58 File Offset: 0x00032E58
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x00034CBC File Offset: 0x00032EBC
		public bool? ReturnContent
		{
			get
			{
				HttpHeaderValueElement httpHeaderValueElement = this.Get("return");
				if (httpHeaderValueElement != null)
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

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00034D18 File Offset: 0x00032F18
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x00034D4E File Offset: 0x00032F4E
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

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00034D85 File Offset: 0x00032F85
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x00034D98 File Offset: 0x00032F98
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

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00034DB4 File Offset: 0x00032FB4
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x00034E20 File Offset: 0x00033020
		public int? Wait
		{
			get
			{
				HttpHeaderValueElement httpHeaderValueElement = this.Get("wait");
				if (httpHeaderValueElement == null)
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

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00034E77 File Offset: 0x00033077
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x00034E8A File Offset: 0x0003308A
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

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00034EA8 File Offset: 0x000330A8
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x00034F14 File Offset: 0x00033114
		public int? MaxPageSize
		{
			get
			{
				HttpHeaderValueElement httpHeaderValueElement = this.Get("odata.maxpagesize");
				if (httpHeaderValueElement == null)
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

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00034F71 File Offset: 0x00033171
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x00034F84 File Offset: 0x00033184
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

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00034FA0 File Offset: 0x000331A0
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

		// Token: 0x06000F21 RID: 3873 RVA: 0x00034FC6 File Offset: 0x000331C6
		private static string AddQuotes(string text)
		{
			return "\"" + text + "\"";
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00034FD8 File Offset: 0x000331D8
		private void Clear(string preference)
		{
			if (this.Preferences.Remove(preference))
			{
				this.SetPreferencesToMessageHeader();
			}
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00034FEE File Offset: 0x000331EE
		private void Set(HttpHeaderValueElement preference)
		{
			this.Preferences[preference.Name] = preference;
			this.SetPreferencesToMessageHeader();
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00035008 File Offset: 0x00033208
		private HttpHeaderValueElement Get(string preferenceName)
		{
			HttpHeaderValueElement httpHeaderValueElement;
			if (!this.Preferences.TryGetValue(preferenceName, ref httpHeaderValueElement))
			{
				return null;
			}
			return httpHeaderValueElement;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00035028 File Offset: 0x00033228
		private HttpHeaderValue ParsePreferences()
		{
			string header = this.message.GetHeader(this.preferenceHeaderName);
			HttpHeaderValueLexer httpHeaderValueLexer = HttpHeaderValueLexer.Create(this.preferenceHeaderName, header);
			return httpHeaderValueLexer.ToHttpHeaderValue();
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0003505A File Offset: 0x0003325A
		private void SetPreferencesToMessageHeader()
		{
			this.message.SetHeader(this.preferenceHeaderName, this.Preferences.ToString());
		}

		// Token: 0x0400068B RID: 1675
		private const string ReturnPreferenceTokenName = "return";

		// Token: 0x0400068C RID: 1676
		private const string ReturnRepresentationPreferenceTokenValue = "representation";

		// Token: 0x0400068D RID: 1677
		private const string ReturnMinimalPreferenceTokenValue = "minimal";

		// Token: 0x0400068E RID: 1678
		private const string ODataAnnotationPreferenceToken = "odata.include-annotations";

		// Token: 0x0400068F RID: 1679
		private const string RespondAsyncPreferenceToken = "respond-async";

		// Token: 0x04000690 RID: 1680
		private const string WaitPreferenceTokenName = "wait";

		// Token: 0x04000691 RID: 1681
		private const string ODataContinueOnErrorPreferenceToken = "odata.continue-on-error";

		// Token: 0x04000692 RID: 1682
		private const string ODataMaxPageSizePreferenceToken = "odata.maxpagesize";

		// Token: 0x04000693 RID: 1683
		private const string ODataTrackChangesPreferenceToken = "odata.track-changes";

		// Token: 0x04000694 RID: 1684
		private const string PreferHeaderName = "Prefer";

		// Token: 0x04000695 RID: 1685
		private const string PreferenceAppliedHeaderName = "Preference-Applied";

		// Token: 0x04000696 RID: 1686
		private static readonly KeyValuePair<string, string>[] EmptyParameters = new KeyValuePair<string, string>[0];

		// Token: 0x04000697 RID: 1687
		private static readonly HttpHeaderValueElement ContinueOnErrorPreference = new HttpHeaderValueElement("odata.continue-on-error", null, ODataPreferenceHeader.EmptyParameters);

		// Token: 0x04000698 RID: 1688
		private static readonly HttpHeaderValueElement ReturnMinimalPreference = new HttpHeaderValueElement("return", "minimal", ODataPreferenceHeader.EmptyParameters);

		// Token: 0x04000699 RID: 1689
		private static readonly HttpHeaderValueElement ReturnRepresentationPreference = new HttpHeaderValueElement("return", "representation", ODataPreferenceHeader.EmptyParameters);

		// Token: 0x0400069A RID: 1690
		private static readonly HttpHeaderValueElement RespondAsyncPreference = new HttpHeaderValueElement("respond-async", null, ODataPreferenceHeader.EmptyParameters);

		// Token: 0x0400069B RID: 1691
		private static readonly HttpHeaderValueElement TrackChangesPreference = new HttpHeaderValueElement("odata.track-changes", null, ODataPreferenceHeader.EmptyParameters);

		// Token: 0x0400069C RID: 1692
		private readonly ODataMessage message;

		// Token: 0x0400069D RID: 1693
		private readonly string preferenceHeaderName;

		// Token: 0x0400069E RID: 1694
		private HttpHeaderValue preferences;
	}
}
