using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.OAuthLibrary;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x02001708 RID: 5896
	internal sealed class ExtensionOAuthFactory : IOAuthFactory
	{
		// Token: 0x060095DA RID: 38362 RVA: 0x001F06F7 File Offset: 0x001EE8F7
		public ExtensionOAuthFactory(object moduleLock, ExtensionModule module, string resourceKind)
		{
			this.moduleLock = moduleLock;
			this.module = module;
			this.resourceKind = resourceKind;
		}

		// Token: 0x1700272E RID: 10030
		// (get) Token: 0x060095DB RID: 38363 RVA: 0x001F0714 File Offset: 0x001EE914
		public ExtensionModule Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x060095DC RID: 38364 RVA: 0x001F071C File Offset: 0x001EE91C
		public object CreateProvider(IEngineHost engineHost, IEngine engine, object clientApplication, string resourceUrl)
		{
			return new ExtensionOAuthFactory.Provider(this, engineHost, engine, (OAuthClientApplication)clientApplication, resourceUrl);
		}

		// Token: 0x060095DD RID: 38365 RVA: 0x000020FA File Offset: 0x000002FA
		public object CreateResource(IEngineHost engineHost, IEngine engine, string resourceUrl)
		{
			return null;
		}

		// Token: 0x04004FAA RID: 20394
		private readonly object moduleLock;

		// Token: 0x04004FAB RID: 20395
		private readonly ExtensionModule module;

		// Token: 0x04004FAC RID: 20396
		private readonly string resourceKind;

		// Token: 0x02001709 RID: 5897
		private sealed class Provider : IOAuthProvider
		{
			// Token: 0x060095DE RID: 38366 RVA: 0x001F0730 File Offset: 0x001EE930
			public Provider(ExtensionOAuthFactory factory, IEngineHost engineHost, IEngine engine, OAuthClientApplication clientApplication, string resourceUrl)
			{
				this.engine = engine;
				this.engineHost = engineHost;
				this.moduleLock = factory.moduleLock;
				this.resourceUrl = resourceUrl;
				this.additionalModules = Extension.Modules;
				object obj = this.moduleLock;
				lock (obj)
				{
					RecordValue recordValue = factory.Module.RelinkAuthRecord(engineHost, factory.resourceKind);
					Value value;
					RecordValue recordValue2;
					if (recordValue.TryGetValue("OAuth", out value))
					{
						recordValue2 = value.AsRecord;
					}
					else
					{
						recordValue2 = recordValue["OAuth2"].AsRecord;
					}
					this.startLogin = ExtensionOAuthFactory.Provider.GetFunction(recordValue2, "StartLogin");
					this.finishLogin = ExtensionOAuthFactory.Provider.GetFunction(recordValue2, "FinishLogin");
					this.logout = ExtensionOAuthFactory.Provider.GetFunction(recordValue2, "Logout");
					this.refresh = ExtensionOAuthFactory.Provider.GetFunction(recordValue2, "Refresh");
				}
				this.clientApplication = OAuthModule.ClientApplicationAsRecord(clientApplication);
			}

			// Token: 0x060095DF RID: 38367 RVA: 0x001F0830 File Offset: 0x001EEA30
			private static FunctionValue GetFunction(RecordValue oauth, string member)
			{
				Value value;
				if (!oauth.TryGetValue(member, out value))
				{
					return null;
				}
				return value.AsFunction;
			}

			// Token: 0x060095E0 RID: 38368 RVA: 0x001F0850 File Offset: 0x001EEA50
			OAuthBrowserNavigation IOAuthProvider.StartLogin(string state, string display)
			{
				OAuthBrowserNavigation oauthBrowserNavigation;
				try
				{
					object obj = this.moduleLock;
					lock (obj)
					{
						Value value;
						if (this.startLogin.Type.AsFunctionType.Min == 4)
						{
							value = this.startLogin.Invoke(this.clientApplication, TextValue.New(this.resourceUrl), TextValue.New(state), TextValue.New(display));
						}
						else
						{
							value = this.startLogin.Invoke(TextValue.New(this.resourceUrl), TextValue.New(state), TextValue.New(display));
						}
						oauthBrowserNavigation = this.CreateNavigation(value);
					}
				}
				catch (Exception ex)
				{
					this.TraceException("OAuth/MProvider/StartLogin", ex);
					throw this.TransformToOAuthExceptionIfPossible(ex);
				}
				return oauthBrowserNavigation;
			}

			// Token: 0x060095E1 RID: 38369 RVA: 0x001F0920 File Offset: 0x001EEB20
			private Exception TransformToOAuthExceptionIfPossible(Exception e)
			{
				if (e is ValueException || e is ResourceSecurityException)
				{
					return new OAuthException(e.Message, e);
				}
				return e;
			}

			// Token: 0x060095E2 RID: 38370 RVA: 0x001F0940 File Offset: 0x001EEB40
			TokenCredential IOAuthProvider.FinishLogin(byte[] serializedContext, Uri callbackUri, string state)
			{
				TokenCredential tokenCredential;
				try
				{
					Value value = ((serializedContext == null) ? Value.Null : ((Value)this.engine.DeserializeValue(this.engineHost, serializedContext, this.additionalModules)));
					object obj = this.moduleLock;
					lock (obj)
					{
						Value value2;
						if (this.finishLogin.Type.AsFunctionType.Min == 5)
						{
							value2 = this.finishLogin.Invoke(this.clientApplication, TextValue.New(this.resourceUrl), value, TextValue.New(callbackUri.AbsoluteUri), TextValue.New(state));
						}
						else
						{
							value2 = this.finishLogin.Invoke(value, TextValue.New(callbackUri.AbsoluteUri), TextValue.New(state));
						}
						tokenCredential = OAuthModule.CredentialFromRecord(value2);
					}
				}
				catch (Exception ex)
				{
					this.TraceException("OAuth/MProvider/FinishLogin", ex);
					throw this.TransformToOAuthExceptionIfPossible(ex);
				}
				return tokenCredential;
			}

			// Token: 0x060095E3 RID: 38371 RVA: 0x001F0A38 File Offset: 0x001EEC38
			TokenCredential IOAuthProvider.Refresh(TokenCredential credential)
			{
				if (this.refresh == null)
				{
					return credential;
				}
				TokenCredential tokenCredential2;
				try
				{
					object obj = this.moduleLock;
					Value value;
					lock (obj)
					{
						if (this.refresh.Type.AsFunctionType.Min == 3)
						{
							value = this.refresh.Invoke(this.clientApplication, TextValue.New(this.resourceUrl), OAuthModule.CredentialAsRecord(credential));
						}
						else
						{
							value = this.refresh.Invoke(TextValue.New(this.resourceUrl), TextValue.NewOrNull(credential.RefreshToken));
						}
					}
					TokenCredential tokenCredential = OAuthModule.CredentialFromRecord(value);
					foreach (KeyValuePair<string, string> keyValuePair in credential.Properties)
					{
						if (!tokenCredential.Properties.ContainsKey(keyValuePair.Key))
						{
							tokenCredential.Properties.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					tokenCredential2 = new TokenCredential(tokenCredential.AccessToken, tokenCredential.Expires, tokenCredential.RefreshToken ?? credential.RefreshToken, tokenCredential.Properties);
				}
				catch (Exception ex)
				{
					this.TraceException("OAuth/MProvider/Refresh", ex);
					throw this.TransformToOAuthExceptionIfPossible(ex);
				}
				return tokenCredential2;
			}

			// Token: 0x060095E4 RID: 38372 RVA: 0x001F0BC4 File Offset: 0x001EEDC4
			Uri IOAuthProvider.Logout(string accessToken)
			{
				if (this.logout == null)
				{
					return null;
				}
				Uri uri;
				try
				{
					Value value = ((accessToken == null) ? Value.Null : TextValue.New(accessToken));
					object obj = this.moduleLock;
					lock (obj)
					{
						Value value2;
						if (this.logout.Type.AsFunctionType.Min == 3)
						{
							value2 = this.logout.Invoke(this.clientApplication, TextValue.New(this.resourceUrl), value);
						}
						else
						{
							value2 = this.logout.Invoke(value);
						}
						uri = (value2.IsText ? new Uri(value2.AsString) : null);
					}
				}
				catch (Exception ex)
				{
					this.TraceException("OAuth/MProvider/Logout", ex);
					throw this.TransformToOAuthExceptionIfPossible(ex);
				}
				return uri;
			}

			// Token: 0x060095E5 RID: 38373 RVA: 0x001F0CA0 File Offset: 0x001EEEA0
			private OAuthBrowserNavigation CreateNavigation(Value value)
			{
				RecordValue asRecord = value.AsRecord;
				byte[] array = null;
				Value value2;
				if (asRecord.TryGetValue("Context", out value2) && !value2.IsNull)
				{
					array = this.engine.SerializeValue(this.engineHost, value2, null);
				}
				return new OAuthBrowserNavigation(new Uri(asRecord["LoginUri"].AsString), new Uri(asRecord["CallbackUri"].AsString), asRecord["WindowHeight"].AsNumber.AsInteger32, asRecord["WindowWidth"].AsNumber.AsInteger32, array);
			}

			// Token: 0x060095E6 RID: 38374 RVA: 0x001F0D3C File Offset: 0x001EEF3C
			private void TraceException(string traceName, Exception exception)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(this.engineHost, traceName, TraceEventType.Error, null))
				{
					hostTrace.Add(exception, true);
				}
			}

			// Token: 0x04004FAD RID: 20397
			private readonly IEngine engine;

			// Token: 0x04004FAE RID: 20398
			private readonly IEngineHost engineHost;

			// Token: 0x04004FAF RID: 20399
			private readonly object moduleLock;

			// Token: 0x04004FB0 RID: 20400
			private readonly string resourceUrl;

			// Token: 0x04004FB1 RID: 20401
			private readonly string[] additionalModules;

			// Token: 0x04004FB2 RID: 20402
			private readonly FunctionValue startLogin;

			// Token: 0x04004FB3 RID: 20403
			private readonly FunctionValue finishLogin;

			// Token: 0x04004FB4 RID: 20404
			private readonly FunctionValue refresh;

			// Token: 0x04004FB5 RID: 20405
			private readonly FunctionValue logout;

			// Token: 0x04004FB6 RID: 20406
			private readonly Value clientApplication;
		}
	}
}
