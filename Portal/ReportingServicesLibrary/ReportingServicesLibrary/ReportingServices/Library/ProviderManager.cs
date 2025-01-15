using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000082 RID: 130
	internal class ProviderManager
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x00016404 File Offset: 0x00014604
		public static void Configure()
		{
			object staticLock = ProviderManager.m_staticLock;
			lock (staticLock)
			{
				if (!ProviderManager.m_configured)
				{
					ProviderDBInterface providerDBInterface = new ProviderDBInterface();
					providerDBInterface.ConnectionManager = new ConnectionManager();
					providerDBInterface.ConnectionManager.WillDisconnectStorage();
					try
					{
						providerDBInterface.InvalidateDeletedProviders();
						providerDBInterface.Commit();
					}
					catch
					{
						providerDBInterface.ConnectionManager.AbortTransaction();
						throw;
					}
					finally
					{
						providerDBInterface.DisconnectStorage();
					}
					ProviderManager.m_configured = true;
				}
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000164A4 File Offset: 0x000146A4
		public static Microsoft.ReportingServices.Library.Soap2005.Extension[] ListProviders(ExtensionTypeEnum type)
		{
			ArrayList arrayList = new ArrayList();
			switch (type)
			{
			case ExtensionTypeEnum.Delivery:
				ProviderManager.AddProvidersFromArray(Globals.Configuration.Extensions.Delivery, ExtensionTypeEnum.Delivery, arrayList);
				break;
			case ExtensionTypeEnum.Render:
				ProviderManager.AddProvidersFromArray(Globals.Configuration.Extensions.Renderer, ExtensionTypeEnum.Render, arrayList);
				break;
			case ExtensionTypeEnum.Data:
				ProviderManager.AddProvidersFromArray(Globals.Configuration.Extensions.Data, ExtensionTypeEnum.Data, arrayList);
				break;
			case ExtensionTypeEnum.All:
				ProviderManager.AddProvidersFromArray(Globals.Configuration.Extensions.Delivery, ExtensionTypeEnum.Delivery, arrayList);
				ProviderManager.AddProvidersFromArray(Globals.Configuration.Extensions.Renderer, ExtensionTypeEnum.Render, arrayList);
				ProviderManager.AddProvidersFromArray(Globals.Configuration.Extensions.Data, ExtensionTypeEnum.Data, arrayList);
				break;
			default:
				throw new InvalidParameterException("type");
			}
			return arrayList.ToArray(typeof(Microsoft.ReportingServices.Library.Soap2005.Extension)) as Microsoft.ReportingServices.Library.Soap2005.Extension[];
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00016580 File Offset: 0x00014780
		private static void AddProvidersFromArray(ExtensionsConfiguration.ExtensionArray array, ExtensionTypeEnum type, ArrayList list)
		{
			object syncRoot = array.SyncRoot;
			lock (syncRoot)
			{
				for (int i = array.Count - 1; i >= 0; i--)
				{
					Microsoft.ReportingServices.Diagnostics.Extension extension = (Microsoft.ReportingServices.Diagnostics.Extension)array[i];
					if (type != ExtensionTypeEnum.Render || !extension.Name.Equals("SHAREDDATASETJSON", StringComparison.OrdinalIgnoreCase))
					{
						Microsoft.ReportingServices.Library.Soap2005.Extension extension2 = new Microsoft.ReportingServices.Library.Soap2005.Extension();
						extension2.ExtensionType = type;
						extension2.Name = extension.Name;
						extension2.Visible = extension.Visible;
						if (type == ExtensionTypeEnum.Data)
						{
							extension2.IsModelGenerationSupported = Globals.Configuration.Extensions.ModelGeneration[extension2.Name] != null;
						}
						IExtension newInstanceExtensionClass = ExtensionClassFactory.GetNewInstanceExtensionClass(extension.Name, extension.Type);
						if (newInstanceExtensionClass != null)
						{
							extension2.LocalizedName = newInstanceExtensionClass.LocalizedName;
							if (type == ExtensionTypeEnum.Render)
							{
								string overrideName = (extension as RenderingExtension).GetOverrideName();
								if (overrideName != null)
								{
									extension2.LocalizedName = overrideName;
								}
							}
							if (extension2.LocalizedName == null)
							{
								extension2.LocalizedName = extension.Name;
							}
							list.Add(extension2);
						}
					}
				}
				list.Reverse();
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000166C0 File Offset: 0x000148C0
		public static DeliveryReportServerInfo GetExtensionServerSettings(IDeliveryExtension extension, RSService rsService, ExternalItemPath itemPath)
		{
			if (rsService == null)
			{
				return DeliveryReportServerInfo.InfoObject;
			}
			List<Setting> list = new List<Setting>();
			Type type = extension.GetType();
			if (KnownExtensions.IsKnownExtension("Delivery", type) && string.Compare(type.Name, "PowerBIDeliveryProvider", StringComparison.OrdinalIgnoreCase) == 0)
			{
				list.AddRange(ProviderManager.GetPowerBIDeliveryServerSettings(rsService));
			}
			return new DeliveryReportServerInfo(list.ToArray());
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001671C File Offset: 0x0001491C
		public static List<Setting> GetPowerBIDeliveryServerSettings(RSService rsService)
		{
			Security security = new Security(rsService.UserContext, true);
			security.ConnectionManager = new ConnectionManager();
			security.ConnectionManager.WillDisconnectStorage();
			string text = null;
			List<Setting> list = new List<Setting>();
			try
			{
				text = security.GetLatestAADToken();
			}
			catch
			{
				throw new AccessDeniedException(rsService.UserContext.UserName, ErrorCode.rsAccessDenied);
			}
			finally
			{
				security.ConnectionManager.DisconnectStorage();
			}
			list.Add(new Setting
			{
				Name = "AccessToken",
				Value = text
			});
			list.Add(new Setting
			{
				Name = "ContextUserName",
				Value = rsService.UserContext.UserName
			});
			return list;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000167E4 File Offset: 0x000149E4
		public static Setting[] InitDeliveryExtension(IDeliveryExtension extension, bool isPrivileged, RSService rsService, ExternalItemPath itemPath)
		{
			extension.IsPrivilegedUser = isPrivileged;
			extension.ReportServerInformation = ProviderManager.GetExtensionServerSettings(extension, rsService, itemPath);
			Setting[] extensionSettings = extension.ExtensionSettings;
			if (extensionSettings == null)
			{
				return null;
			}
			foreach (Setting setting in extensionSettings)
			{
				if (setting == null)
				{
					throw new ServerConfigurationErrorException(string.Format(CultureInfo.InvariantCulture, "Delivery extension '{0}' specified an uninitialized (null) setting.", extension.LocalizedName));
				}
				if (setting.IsPassword && setting.ValidValues != null && setting.ValidValues.Length != 0)
				{
					throw new ServerConfigurationErrorException(string.Format(CultureInfo.InvariantCulture, "Delivery extension '{0}' specified a valid values list for the field '{1}' marked as a password.", extension.LocalizedName, setting.Name));
				}
			}
			return extensionSettings;
		}

		// Token: 0x040002E2 RID: 738
		private static RSTrace m_tracer = RSTrace.ProviderTracer;

		// Token: 0x040002E3 RID: 739
		private static bool m_configured = false;

		// Token: 0x040002E4 RID: 740
		private static object m_staticLock = new object();

		// Token: 0x040002E5 RID: 741
		internal const string _Extensions = "Extensions";

		// Token: 0x040002E6 RID: 742
		internal const string _All = "All";

		// Token: 0x040002E7 RID: 743
		internal const string _Extension = "Extension";

		// Token: 0x040002E8 RID: 744
		internal const string _ExtensionType = "ExtensionType";

		// Token: 0x040002E9 RID: 745
		internal const string _Visible = "Visible";

		// Token: 0x040002EA RID: 746
		internal const string _LocalizedName = "LocalizedName";

		// Token: 0x040002EB RID: 747
		internal const string _Name = "Name";

		// Token: 0x040002EC RID: 748
		internal const string _PowerBIDefaultWorkspaceID = "myworkspace";
	}
}
