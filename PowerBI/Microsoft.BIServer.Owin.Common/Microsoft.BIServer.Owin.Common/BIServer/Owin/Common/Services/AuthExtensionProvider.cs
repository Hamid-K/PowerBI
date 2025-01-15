using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.BIServer.Configuration;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.BIServer.Owin.Common.Services
{
	// Token: 0x0200000F RID: 15
	public class AuthExtensionProvider : IAuthExtensionProvider
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002811 File Offset: 0x00000A11
		public AuthExtensionProvider()
		{
			this._serverConfig = ConfigReader.Current.ServerConfiguration;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002829 File Offset: 0x00000A29
		public AuthExtensionProvider(ServerConfiguration serverConfig, IPropertyProvider propertyProvider, bool resetCacheForTest = false)
		{
			this._serverConfig = serverConfig;
			this._propertyProvider = propertyProvider;
			if (resetCacheForTest)
			{
				AuthExtensionProvider._extensionCompatModes.Clear();
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000284C File Offset: 0x00000A4C
		public bool IsAuthExtensionInBackCompatMode(Microsoft.BIServer.Configuration.AuthenticationType authType)
		{
			return AuthExtensionProvider._extensionCompatModes.GetOrAdd(authType, new Func<Microsoft.BIServer.Configuration.AuthenticationType, bool>(this.InternalIsBackCompatMode));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002868 File Offset: 0x00000A68
		private bool InternalIsBackCompatMode(Microsoft.BIServer.Configuration.AuthenticationType authType)
		{
			IAuthenticationExtension2 authenticationExtension = this.GetAuthenticationExtension(authType);
			return authenticationExtension is AuthenticationExtensionBackCompatProxy || authenticationExtension is WindowsAuthenticationBackCompatProxy;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002890 File Offset: 0x00000A90
		public IAuthenticationExtension2 GetAuthenticationExtension(Microsoft.BIServer.Configuration.AuthenticationType authType)
		{
			return this.CreateAndInitializeAuthenticationExtensionInstance(this._serverConfig.AuthenticationExtensions.Where((Microsoft.BIServer.Configuration.Extension p) => p.Name.Equals(authType.ToString())).FirstOrDefault<Microsoft.BIServer.Configuration.Extension>());
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028D4 File Offset: 0x00000AD4
		private IAuthenticationExtension2 CreateAndInitializeAuthenticationExtensionInstance(Microsoft.BIServer.Configuration.Extension extConfig)
		{
			IAuthenticationExtension2 authenticationExtension = null;
			if (extConfig != null)
			{
				object obj = this.CreateExtensionObject(extConfig, false);
				IWindowsAuthenticationExtension windowsAuthenticationExtension = obj as IWindowsAuthenticationExtension;
				if (windowsAuthenticationExtension != null)
				{
					authenticationExtension = new WindowsAuthenticationBackCompatProxy(windowsAuthenticationExtension);
				}
				else
				{
					IAuthenticationExtension authenticationExtension2 = obj as IAuthenticationExtension;
					if (authenticationExtension2 != null)
					{
						authenticationExtension = new AuthenticationExtensionBackCompatProxy(authenticationExtension2);
					}
					else
					{
						authenticationExtension = obj as IAuthenticationExtension2;
					}
				}
				if (authenticationExtension != null)
				{
					try
					{
						IAuthenticationExtension2 authenticationExtension3 = authenticationExtension;
						if (authenticationExtension3 == null)
						{
							throw new Exception(string.Format(CultureInfo.InvariantCulture, "Report server extension {0} does not implement IExtension interface.", extConfig.Name));
						}
						try
						{
							authenticationExtension3.SetConfiguration(extConfig.Configuration);
						}
						catch
						{
							Logger.Error("Error setting configuration to extension", Array.Empty<object>());
							throw;
						}
						ICatalogConfiguredExtension catalogConfiguredExtension = authenticationExtension as ICatalogConfiguredExtension;
						if (catalogConfiguredExtension != null)
						{
							IEnumerable<string> enumerable = catalogConfiguredExtension.EnumerateRequiredProperties();
							if (enumerable != null)
							{
								catalogConfiguredExtension.SetCatalogConfiguration(this._propertyProvider.GetProperties(enumerable));
							}
						}
					}
					catch (Exception ex)
					{
						Logger.Error(ex, "Error loading extension: \r", Array.Empty<object>());
						authenticationExtension = null;
					}
				}
			}
			return authenticationExtension;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000029CC File Offset: 0x00000BCC
		private object CreateExtensionObject(Microsoft.BIServer.Configuration.Extension extConfig, bool typeOnly)
		{
			object obj = null;
			if (extConfig != null)
			{
				try
				{
					Assembly assembly = this.LoadAssembly(extConfig.Assembly);
					if (typeOnly)
					{
						obj = assembly.GetType(extConfig.Class);
					}
					else
					{
						obj = assembly.CreateInstance(extConfig.Class);
					}
					if (obj == null)
					{
						throw new Exception("Could not create Extension of type: " + extConfig.Type + "name: " + extConfig.Name);
					}
				}
				catch (Exception ex)
				{
					Logger.Error(ex, "Error loading extension: \r", Array.Empty<object>());
					obj = null;
				}
			}
			return obj;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A54 File Offset: 0x00000C54
		private Assembly LoadAssembly(string name)
		{
			Assembly assembly = null;
			try
			{
				assembly = Assembly.Load(name);
			}
			catch (FileNotFoundException)
			{
				assembly = this.LoadAssemblyFromFile(name);
			}
			return assembly;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A88 File Offset: 0x00000C88
		private Assembly LoadAssemblyFromFile(string name)
		{
			string reportServerBinDirectory = this.GetReportServerBinDirectory();
			AssemblyName assemblyName = new AssemblyName(name);
			assemblyName.CodeBase = string.Format("{0}.dll", Path.Combine(reportServerBinDirectory, assemblyName.Name));
			return Assembly.Load(assemblyName);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AC8 File Offset: 0x00000CC8
		private string GetReportServerBinDirectory()
		{
			string fullName = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
			if (Directory.Exists(fullName))
			{
				return Path.Combine(fullName, "ReportServer\\Bin");
			}
			throw new DirectoryNotFoundException("ReportServer\\bin directory not found.");
		}

		// Token: 0x04000039 RID: 57
		private readonly ServerConfiguration _serverConfig;

		// Token: 0x0400003A RID: 58
		private readonly IPropertyProvider _propertyProvider;

		// Token: 0x0400003B RID: 59
		private static readonly ConcurrentDictionary<Microsoft.BIServer.Configuration.AuthenticationType, bool> _extensionCompatModes = new ConcurrentDictionary<Microsoft.BIServer.Configuration.AuthenticationType, bool>();
	}
}
