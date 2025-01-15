using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012B2 RID: 4786
	public static class CredentialExtensions
	{
		// Token: 0x06007DAC RID: 32172 RVA: 0x001AF71C File Offset: 0x001AD91C
		public static ICredentials GetNetworkCredential(this WindowsCredential credential, IEngineHost host, IResource resource)
		{
			if (!credential.OverrideCurrentUser)
			{
				return CredentialCache.DefaultCredentials;
			}
			string username = credential.Username;
			if (credential.Password == null)
			{
				return new ImpersonationCredential(CredentialExtensions.GetImpersonationWrapper(credential.Username, null, host, resource));
			}
			string domain = CredentialExtensions.GetDomain(ref username);
			return new NetworkCredential(username, credential.Password, domain);
		}

		// Token: 0x06007DAD RID: 32173 RVA: 0x001AF770 File Offset: 0x001AD970
		private static void SwallowExceptions(Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06007DAE RID: 32174 RVA: 0x000020FA File Offset: 0x000002FA
		public static IDisposable NullImpersonationWrapper()
		{
			return null;
		}

		// Token: 0x06007DAF RID: 32175 RVA: 0x001AF7A0 File Offset: 0x001AD9A0
		private static string GetDomain(ref string username)
		{
			int num = username.IndexOf('\\');
			string text;
			if (num > 0)
			{
				text = username.Substring(0, num);
				username = username.Substring(num + 1);
			}
			else if (username.IndexOf('@') > 0)
			{
				text = null;
			}
			else
			{
				text = Environment.MachineName;
			}
			return text;
		}

		// Token: 0x06007DB0 RID: 32176 RVA: 0x001AF7EC File Offset: 0x001AD9EC
		public static Func<IDisposable> GetImpersonationWrapper(string username, string password, IEngineHost host, IResource resource)
		{
			Func<IDisposable> func;
			using (IHostTrace trace = TracingService.CreateTrace(host, "CredentialExtensions/GetImpersonationWrapper", TraceEventType.Information, resource))
			{
				if (password == null)
				{
					WindowsIdentity identity = null;
					string text = username;
					try
					{
						string[] array = username.Split(new char[] { '\\' });
						if (array.Length == 2)
						{
							if (IPGlobalProperties.GetIPGlobalProperties().DomainName == null)
							{
								throw new ArgumentException(Strings.NonDomainUserCannotUseSAMName, "username");
							}
							string text2 = array[0];
							string text3 = array[1];
							using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, text2))
							{
								using (UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, IdentityType.SamAccountName, text3))
								{
									if (userPrincipal != null)
									{
										text = userPrincipal.UserPrincipalName;
									}
								}
							}
						}
						try
						{
							identity = new WindowsIdentity(text);
						}
						catch (SecurityException ex)
						{
							throw new Exception(ex.Message, ex);
						}
					}
					catch (Exception ex2)
					{
						trace.Add(ex2, true);
						trace.Add("Username", username, true);
						trace.Add("ConvertedUsername", text, true);
						if (!SafeExceptions.IsSafeException(ex2))
						{
							throw;
						}
						throw DataSourceException.NewAccessAuthorizationError(host, resource, ex2.Message, null, ex2);
					}
					func = delegate
					{
						IDisposable disposable;
						try
						{
							disposable = WindowsIdentity.Impersonate(identity.Token);
						}
						catch (Exception ex4)
						{
							trace.Add("Exception impersonating identity. Username: ", username, true);
							trace.Add("Identity", identity.Name, true);
							trace.Add("IsAuthenticated", identity.IsAuthenticated, false);
							trace.Add("ImpersonationLevel", identity.ImpersonationLevel, false);
							trace.Add(ex4, true);
							if (!SafeExceptions.IsSafeException(ex4))
							{
								throw;
							}
							throw DataSourceException.NewAccessAuthorizationError(host, resource, ex4.Message, null, ex4);
						}
						return disposable;
					};
				}
				else
				{
					try
					{
						SafeHandle token = ProcessHelpers.LogonUser(username, password);
						func = delegate
						{
							IDisposable disposable2 = WindowsIdentity.Impersonate(token.DangerousGetHandle());
							GC.KeepAlive(token);
							return disposable2;
						};
					}
					catch (Win32Exception ex3)
					{
						trace.Add(ex3, true);
						throw DataSourceException.NewAccessAuthorizationError(host, resource, ex3.Message, null, ex3);
					}
				}
			}
			return func;
		}

		// Token: 0x06007DB1 RID: 32177 RVA: 0x001AFA58 File Offset: 0x001ADC58
		public static Func<IDisposable> GetImpersonationWrapper(this IResourceCredential credential, IEngineHost host, IResource resource)
		{
			WindowsCredential windowsCredential = credential as WindowsCredential;
			if (windowsCredential == null || !windowsCredential.OverrideCurrentUser)
			{
				return new Func<IDisposable>(CredentialExtensions.NullImpersonationWrapper);
			}
			if (windowsCredential.Token != null)
			{
				SafeHandle token = windowsCredential.Token;
				return delegate
				{
					IDisposable disposable = WindowsIdentity.Impersonate(token.DangerousGetHandle());
					GC.KeepAlive(token);
					return disposable;
				};
			}
			if (!CredentialExtensions.assemblyLoadInitialized)
			{
				AssemblyName name = new AssemblyName(typeof(object).Assembly.FullName);
				CredentialExtensions.SwallowExceptions(delegate
				{
					ConfigurationManager.GetSection("system.xml/xmlReader");
				});
				string[] array = new string[] { "System.Numerics", "System.Transactions", "System.Xml", "System.DirectoryServices" };
				for (int i = 0; i < array.Length; i++)
				{
					string assembly = array[i];
					CredentialExtensions.SwallowExceptions(delegate
					{
						name.Name = assembly;
						Assembly.Load(name);
					});
				}
				CredentialExtensions.assemblyLoadInitialized = true;
			}
			Func<IDisposable> func2;
			using (IHostTrace hostTrace = TracingService.CreateTrace(host, "CredentialExtensions/GetImpersonationWrapper", TraceEventType.Information, resource))
			{
				Func<IDisposable> func;
				bool flag = CredentialExtensions.ImpersonationManager.TryGetImpersonator(windowsCredential, out func);
				hostTrace.Add("cached", flag, false);
				if (!flag)
				{
					func = CredentialExtensions.GetImpersonationWrapper(windowsCredential.Username, windowsCredential.Password, host, resource);
					if (string.Equals(windowsCredential.Username, WindowsIdentity.GetCurrent().Name, StringComparison.OrdinalIgnoreCase))
					{
						func = new Func<IDisposable>(CredentialExtensions.NullImpersonationWrapper);
					}
					CredentialExtensions.ImpersonationManager.SetImpersonator(host, windowsCredential, func);
				}
				func2 = func;
			}
			return func2;
		}

		// Token: 0x0400452B RID: 17707
		private static bool assemblyLoadInitialized;

		// Token: 0x020012B3 RID: 4787
		private sealed class ImpersonationManager : IDisposable
		{
			// Token: 0x06007DB2 RID: 32178 RVA: 0x001AFBF8 File Offset: 0x001ADDF8
			private ImpersonationManager()
			{
			}

			// Token: 0x06007DB3 RID: 32179 RVA: 0x001AFC0C File Offset: 0x001ADE0C
			public static bool TryGetImpersonator(WindowsCredential credential, out Func<IDisposable> impersonator)
			{
				object obj = CredentialExtensions.ImpersonationManager.syncRoot;
				lock (obj)
				{
					if (CredentialExtensions.ImpersonationManager.manager != null)
					{
						string text = CredentialExtensions.ImpersonationManager.MakeCacheKey(credential);
						if (text != null)
						{
							return CredentialExtensions.ImpersonationManager.manager.cache.TryGetValue(text, out impersonator);
						}
					}
				}
				impersonator = null;
				return false;
			}

			// Token: 0x06007DB4 RID: 32180 RVA: 0x001AFC70 File Offset: 0x001ADE70
			public static void SetImpersonator(IEngineHost engineHost, WindowsCredential credential, Func<IDisposable> impersonator)
			{
				ILifetimeService lifetimeService = ((engineHost != null) ? engineHost.QueryService<ILifetimeService>() : null);
				string text = CredentialExtensions.ImpersonationManager.MakeCacheKey(credential);
				if (lifetimeService != null && text != null)
				{
					object obj = CredentialExtensions.ImpersonationManager.syncRoot;
					lock (obj)
					{
						if (CredentialExtensions.ImpersonationManager.manager == null)
						{
							CredentialExtensions.ImpersonationManager.manager = new CredentialExtensions.ImpersonationManager();
							lifetimeService.Register(CredentialExtensions.ImpersonationManager.manager);
						}
						CredentialExtensions.ImpersonationManager.manager.cache[text] = impersonator;
					}
				}
			}

			// Token: 0x06007DB5 RID: 32181 RVA: 0x001AFCF0 File Offset: 0x001ADEF0
			void IDisposable.Dispose()
			{
				object obj = CredentialExtensions.ImpersonationManager.syncRoot;
				lock (obj)
				{
					this.cache.Clear();
					CredentialExtensions.ImpersonationManager.manager = null;
				}
			}

			// Token: 0x06007DB6 RID: 32182 RVA: 0x001AFD3C File Offset: 0x001ADF3C
			private static string MakeCacheKey(WindowsCredential credential)
			{
				if (credential.Username == null || credential.Password == null)
				{
					return null;
				}
				return credential.Username.Replace("?", "??") + "?" + credential.Password;
			}

			// Token: 0x0400452C RID: 17708
			private static readonly object syncRoot = new object();

			// Token: 0x0400452D RID: 17709
			private static CredentialExtensions.ImpersonationManager manager = null;

			// Token: 0x0400452E RID: 17710
			private readonly Dictionary<string, Func<IDisposable>> cache = new Dictionary<string, Func<IDisposable>>();
		}
	}
}
