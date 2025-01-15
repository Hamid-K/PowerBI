using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000E3 RID: 227
	internal class UriResolver
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x00020336 File Offset: 0x0001E536
		private UriResolver(Uri baseUri, Func<string, Uri> resolveEntitySet)
		{
			this.baseUri = baseUri;
			this.resolveEntitySet = resolveEntitySet;
			if (this.baseUri != null)
			{
				this.baseUriWithSlash = UriResolver.ForceSlashTerminatedUri(this.baseUri);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0002036B File Offset: 0x0001E56B
		internal Func<string, Uri> ResolveEntitySet
		{
			get
			{
				return this.resolveEntitySet;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00020373 File Offset: 0x0001E573
		internal Uri RawBaseUriValue
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0002037B File Offset: 0x0001E57B
		internal Uri BaseUriOrNull
		{
			get
			{
				return this.baseUriWithSlash;
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00020383 File Offset: 0x0001E583
		internal static UriResolver CreateFromBaseUri(Uri baseUri, string parameterName)
		{
			UriResolver.ConvertToAbsoluteAndValidateBaseUri(ref baseUri, parameterName);
			return new UriResolver(baseUri, null);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00020394 File Offset: 0x0001E594
		internal UriResolver CloneWithOverrideValue(Uri overrideBaseUriValue, string parameterName)
		{
			UriResolver.ConvertToAbsoluteAndValidateBaseUri(ref overrideBaseUriValue, parameterName);
			return new UriResolver(overrideBaseUriValue, this.resolveEntitySet);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000203AA File Offset: 0x0001E5AA
		internal UriResolver CloneWithOverrideValue(Func<string, Uri> overrideResolveEntitySetValue)
		{
			return new UriResolver(this.baseUri, overrideResolveEntitySetValue);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000203B8 File Offset: 0x0001E5B8
		internal Uri GetEntitySetUri(string entitySetName)
		{
			Uri entitySetUriFromResolver = this.GetEntitySetUriFromResolver(entitySetName);
			if (entitySetUriFromResolver != null)
			{
				return UriResolver.ForceNonSlashTerminatedUri(entitySetUriFromResolver);
			}
			if (this.baseUriWithSlash != null)
			{
				return UriUtil.CreateUri(this.baseUriWithSlash, UriUtil.CreateUri(entitySetName, UriKind.Relative));
			}
			throw Error.InvalidOperation(Strings.Context_ResolveEntitySetOrBaseUriRequired(entitySetName));
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00020409 File Offset: 0x0001E609
		internal Uri GetBaseUriWithSlash()
		{
			return this.GetBaseUriWithSlash(() => Strings.Context_BaseUriRequired);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00020430 File Offset: 0x0001E630
		internal Uri GetOrCreateAbsoluteUri(Uri requestUri)
		{
			Util.CheckArgumentNull<Uri>(requestUri, "requestUri");
			if (!requestUri.IsAbsoluteUri)
			{
				return UriUtil.CreateUri(this.GetBaseUriWithSlash(() => Strings.Context_RequestUriIsRelativeBaseUriRequired), requestUri);
			}
			return requestUri;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0002047E File Offset: 0x0001E67E
		private static void ConvertToAbsoluteAndValidateBaseUri(ref Uri baseUri, string parameterName)
		{
			baseUri = UriResolver.ConvertToAbsoluteUri(baseUri);
			if (UriResolver.IsValidBaseUri(baseUri))
			{
				return;
			}
			if (parameterName != null)
			{
				throw Error.Argument(Strings.Context_BaseUri, parameterName);
			}
			throw Error.InvalidOperation(Strings.Context_BaseUri);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000204AC File Offset: 0x0001E6AC
		private static bool IsValidBaseUri(Uri baseUri)
		{
			return baseUri == null || (baseUri.IsAbsoluteUri && Uri.IsWellFormedUriString(UriUtil.UriToString(baseUri), UriKind.Absolute) && string.IsNullOrEmpty(baseUri.Query) && string.IsNullOrEmpty(baseUri.Fragment));
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000204EC File Offset: 0x0001E6EC
		private static Uri ConvertToAbsoluteUri(Uri baseUri)
		{
			if (baseUri == null)
			{
				return null;
			}
			return baseUri;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x000204FC File Offset: 0x0001E6FC
		private static Uri ForceNonSlashTerminatedUri(Uri uri)
		{
			string text = UriUtil.UriToString(uri);
			if (text[text.Length - 1] == '/')
			{
				return UriUtil.CreateUri(text.Substring(0, text.Length - 1), UriKind.Absolute);
			}
			return uri;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0002053C File Offset: 0x0001E73C
		private static Uri ForceSlashTerminatedUri(Uri uri)
		{
			string text = UriUtil.UriToString(uri);
			if (text[text.Length - 1] != '/')
			{
				return UriUtil.CreateUri(text + "/", UriKind.Absolute);
			}
			return uri;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00020575 File Offset: 0x0001E775
		private Uri GetBaseUriWithSlash(Func<string> getErrorMessage)
		{
			if (this.baseUriWithSlash == null)
			{
				throw Error.InvalidOperation(getErrorMessage());
			}
			return this.baseUriWithSlash;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00020598 File Offset: 0x0001E798
		private Uri GetEntitySetUriFromResolver(string entitySetName)
		{
			if (this.resolveEntitySet != null)
			{
				Uri uri = this.resolveEntitySet(entitySetName);
				if (uri != null)
				{
					if (UriResolver.IsValidBaseUri(uri))
					{
						return uri;
					}
					throw Error.InvalidOperation(Strings.Context_ResolveReturnedInvalidUri);
				}
			}
			return null;
		}

		// Token: 0x04000368 RID: 872
		private readonly Uri baseUri;

		// Token: 0x04000369 RID: 873
		private readonly Func<string, Uri> resolveEntitySet;

		// Token: 0x0400036A RID: 874
		private readonly Uri baseUriWithSlash;
	}
}
