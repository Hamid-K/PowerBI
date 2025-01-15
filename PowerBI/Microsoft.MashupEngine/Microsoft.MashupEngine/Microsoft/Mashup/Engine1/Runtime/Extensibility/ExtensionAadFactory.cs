using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x020016EF RID: 5871
	internal sealed class ExtensionAadFactory : IOAuthFactory
	{
		// Token: 0x0600953C RID: 38204 RVA: 0x001ED8E1 File Offset: 0x001EBAE1
		public ExtensionAadFactory(object moduleLock, ExtensionModule module, string resourceKind, OAuthClientApplication defaultClientApplication)
		{
			this.moduleLock = moduleLock;
			this.module = module;
			this.resourceKind = resourceKind;
			this.defaultClientApplication = defaultClientApplication;
		}

		// Token: 0x0600953D RID: 38205 RVA: 0x001ED908 File Offset: 0x001EBB08
		public object CreateProvider(IEngineHost engineHost, IEngine engine, object clientApplication, string resourceUrl)
		{
			OAuthServices oauthServices = OAuthFactory.CreateServices(engineHost);
			return new AadOAuthProvider(oauthServices, ((OAuthClientApplication)clientApplication) ?? this.defaultClientApplication, this.CreateResource(engineHost, oauthServices, resourceUrl));
		}

		// Token: 0x0600953E RID: 38206 RVA: 0x001ED93C File Offset: 0x001EBB3C
		public object CreateResource(IEngineHost engineHost, IEngine engine, string resourceUrl)
		{
			return this.CreateResource(engineHost, OAuthFactory.CreateServices(engineHost), resourceUrl);
		}

		// Token: 0x0600953F RID: 38207 RVA: 0x001ED94C File Offset: 0x001EBB4C
		public static void Validate(RecordValue authRecord, bool isUri)
		{
			new ExtensionAadFactory.ResourceConstructor(authRecord, isUri);
		}

		// Token: 0x06009540 RID: 38208 RVA: 0x001ED958 File Offset: 0x001EBB58
		private OAuthResource CreateResource(IEngineHost engineHost, OAuthServices services, string resourceUrl)
		{
			OAuthResource oauthResource;
			try
			{
				RecordValue recordValue = this.module.RelinkAuthRecord(engineHost, this.resourceKind);
				Value value;
				RecordValue recordValue2;
				if (recordValue.TryGetValue("Aad", out value))
				{
					recordValue2 = value.AsRecord;
				}
				else
				{
					recordValue2 = recordValue["AAD"].AsRecord;
				}
				ResourceKindInfo resourceKindInfo = this.module.DataSources.FirstOrDefault((ResourceKindInfo rki) => rki.Kind == this.resourceKind);
				bool flag = resourceKindInfo != null && resourceKindInfo.IsUri;
				object obj = this.moduleLock;
				lock (obj)
				{
					oauthResource = new ExtensionAadFactory.ResourceConstructor(recordValue2, flag).CreateResource(services, resourceUrl);
				}
			}
			catch (ValueException ex)
			{
				throw new OAuthException(ex.MessageString, ex);
			}
			catch (RuntimeException ex2)
			{
				throw new OAuthException(ex2.Message, ex2);
			}
			return oauthResource;
		}

		// Token: 0x04004F53 RID: 20307
		private readonly object moduleLock;

		// Token: 0x04004F54 RID: 20308
		private readonly ExtensionModule module;

		// Token: 0x04004F55 RID: 20309
		private readonly string resourceKind;

		// Token: 0x04004F56 RID: 20310
		private readonly OAuthClientApplication defaultClientApplication;

		// Token: 0x020016F0 RID: 5872
		private class ResourceConstructor
		{
			// Token: 0x06009542 RID: 38210 RVA: 0x001EDA60 File Offset: 0x001EBC60
			public ResourceConstructor(RecordValue authRecord, bool isUri)
			{
				Value authorizationValue;
				if (authRecord.TryGetValue("AuthorizationUri", out authorizationValue))
				{
					if (authorizationValue.IsFunction && authorizationValue.Type.AsFunctionType.ParameterCount == 1)
					{
						this.getUris = (string resource) => ExtensionAadFactory.ResourceConstructor.CreateUris(authorizationValue.AsFunction.Invoke(TextValue.New(resource)));
					}
					else
					{
						ISecureTokenService result2 = ExtensionAadFactory.ResourceConstructor.CreateUris(authorizationValue);
						this.getUris = (string resource) => result2;
					}
				}
				Value resourceValue;
				bool flag = authRecord.TryGetValue("Resource", out resourceValue);
				Value scopeValue;
				if (authRecord.TryGetValue("Scope", out scopeValue))
				{
					if (scopeValue.IsFunction && scopeValue.Type.AsFunctionType.ParameterCount == 1)
					{
						this.getScope = (string resource) => scopeValue.AsFunction.Invoke(TextValue.New(resource)).AsString;
					}
					else
					{
						string result3 = scopeValue.AsString;
						this.getScope = (string resource) => result3;
						KeyValuePair<string, string>[] array;
						if (!flag && OAuthResource.TryExtractResourceFromScopes(result3, out array))
						{
							this.getResourceId = (string resource) => string.Empty;
						}
					}
				}
				else
				{
					this.getScope = (string resource) => ".default";
				}
				if (flag)
				{
					if (resourceValue.IsFunction && resourceValue.Type.AsFunctionType.ParameterCount == 1)
					{
						this.getResourceId = (string resource) => resourceValue.AsFunction.Invoke(TextValue.New(resource)).AsString;
						return;
					}
					string result = resourceValue.AsString;
					this.getResourceId = (string resource) => result;
					return;
				}
				else
				{
					if (!isUri && this.getResourceId == null)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.AadMustHaveResource, authRecord, null);
					}
					return;
				}
			}

			// Token: 0x06009543 RID: 38211 RVA: 0x001EDC5C File Offset: 0x001EBE5C
			public OAuthResource CreateResource(OAuthServices services, string resourceUrl)
			{
				if (this.getUris == null)
				{
					OAuthResource oauthResource;
					if (this.getResourceId != null)
					{
						oauthResource = AadOAuthProvider.CreateResourceForId(services, this.getResourceId(resourceUrl), null);
					}
					else
					{
						oauthResource = AadOAuthProvider.CreateResourceForUrl(services, resourceUrl, null);
					}
					return OAuthResource.CreateResource(oauthResource.TokenService.GetAuthorizeUri(string.Empty), oauthResource.TokenService.GetTokenUri(string.Empty), oauthResource.TokenService.GetLogoutUri(string.Empty), oauthResource.Resource, this.getScope(resourceUrl));
				}
				ISecureTokenService secureTokenService = this.getUris(resourceUrl);
				return OAuthResource.CreateResource(secureTokenService.GetAuthorizeUri(string.Empty), secureTokenService.GetTokenUri(string.Empty), secureTokenService.GetLogoutUri(string.Empty), (this.getResourceId != null) ? this.getResourceId(resourceUrl) : new Uri(resourceUrl).GetLeftPart(UriPartial.Authority), this.getScope(resourceUrl));
			}

			// Token: 0x06009544 RID: 38212 RVA: 0x001EDD44 File Offset: 0x001EBF44
			private static ISecureTokenService CreateUris(Value uri)
			{
				Uri uri2 = UriHelper.CreateAbsoluteUriFromValue(uri.AsText);
				string text = uri2.AbsoluteUri;
				if (!UriHelper.IsHttpsUri(uri2))
				{
					throw UriErrors.InvalidHttpsScheme(uri.AsText);
				}
				if (!text.EndsWith("/authorize", StringComparison.OrdinalIgnoreCase))
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.AadAuthorizationUriMustEndInAuthorize, uri, null);
				}
				text = text.Substring(0, text.Length - "/authorize".Length);
				return new SecureTokenService(uri2.GetLeftPart(UriPartial.Authority), text + "/authorize", text + "/token", text + "/logout");
			}

			// Token: 0x04004F57 RID: 20311
			private readonly Func<string, ISecureTokenService> getUris;

			// Token: 0x04004F58 RID: 20312
			private readonly Func<string, string> getResourceId;

			// Token: 0x04004F59 RID: 20313
			private readonly Func<string, string> getScope;
		}
	}
}
