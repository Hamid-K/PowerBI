using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.EngineHost.Services.OAuth
{
	// Token: 0x02001B5D RID: 7005
	public static class OAuthProviderFactory
	{
		// Token: 0x0600AF60 RID: 44896 RVA: 0x0023E870 File Offset: 0x0023CA70
		public static IOAuthProvider GetOAuthProvider(IResource resource, OAuthClientApplication clientApplication, Guid? activityId = null, string correlationId = null)
		{
			IEngine version = MashupEngines.Version1;
			IOAuthFactory ioauthFactory;
			bool flag;
			if (!OAuthProviderFactory.TryGetIOAuthFactory(resource, out ioauthFactory, out flag))
			{
				return null;
			}
			if (clientApplication == null && flag)
			{
				throw new InvalidOperationException(Strings.ClientApplicationRequired);
			}
			IEngineHost engineHost = OAuthEngineHost.Create(version, activityId, correlationId);
			return (IOAuthProvider)ioauthFactory.CreateProvider(engineHost, version, clientApplication, resource.NonNormalizedPath);
		}

		// Token: 0x0600AF61 RID: 44897 RVA: 0x0023E8C4 File Offset: 0x0023CAC4
		public static bool TryGetOAuthResource(IResource resource, Guid activityId, string correlationId, out OAuthResource oAuthResource)
		{
			IEngine version = MashupEngines.Version1;
			IOAuthFactory ioauthFactory;
			bool flag;
			if (OAuthProviderFactory.TryGetIOAuthFactory(resource, out ioauthFactory, out flag))
			{
				IEngineHost engineHost = OAuthEngineHost.Create(version, new Guid?(activityId), correlationId);
				try
				{
					oAuthResource = (OAuthResource)ioauthFactory.CreateResource(engineHost, version, resource.NonNormalizedPath);
					return oAuthResource != null;
				}
				catch (OAuthException ex)
				{
					using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("OAuthProviderFactory/TryGetOAuthResource", engineHost, TraceEventType.Information, null))
					{
						hostTrace.Add(ex, true);
					}
				}
			}
			oAuthResource = null;
			return false;
		}

		// Token: 0x0600AF62 RID: 44898 RVA: 0x0023E960 File Offset: 0x0023CB60
		private static bool TryGetIOAuthFactory(IResource resource, out IOAuthFactory factory, out bool requiresClientApplication)
		{
			ResourceKindInfo resourceKindInfo;
			if (MashupEngines.Version1.TryLookupResourceKind(resource.Kind, out resourceKindInfo))
			{
				OAuth2AuthenticationInfo oauth2AuthenticationInfo = resourceKindInfo.AuthenticationInfo.Where((AuthenticationInfo a) => a.AuthenticationKind == AuthenticationKind.OAuth2).Cast<OAuth2AuthenticationInfo>().FirstOrDefault<OAuth2AuthenticationInfo>();
				if (oauth2AuthenticationInfo != null && oauth2AuthenticationInfo.ProviderFactory != null)
				{
					factory = oauth2AuthenticationInfo.ProviderFactory;
					requiresClientApplication = oauth2AuthenticationInfo.ClientApplicationType == OAuthClientApplicationType.Required;
					return true;
				}
			}
			factory = null;
			requiresClientApplication = false;
			return false;
		}
	}
}
