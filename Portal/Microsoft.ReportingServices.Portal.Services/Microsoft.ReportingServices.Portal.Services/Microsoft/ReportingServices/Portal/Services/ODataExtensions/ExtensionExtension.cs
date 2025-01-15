using System;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x02000048 RID: 72
	internal static class ExtensionExtension
	{
		// Token: 0x06000285 RID: 645 RVA: 0x00011130 File Offset: 0x0000F330
		public static KnownDeliveryExtensions GetKnownExtensionType(string extensionName)
		{
			if (string.Equals(extensionName, "Report Server PowerBI", StringComparison.OrdinalIgnoreCase))
			{
				return KnownDeliveryExtensions.ReportServerPowerBI;
			}
			if (string.Equals(extensionName, "Report Server Email", StringComparison.OrdinalIgnoreCase))
			{
				return KnownDeliveryExtensions.ReportServerEmail;
			}
			if (string.Equals(extensionName, "Report Server FileShare", StringComparison.OrdinalIgnoreCase))
			{
				return KnownDeliveryExtensions.ReportServerFileShare;
			}
			return KnownDeliveryExtensions.Unknown;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00011163 File Offset: 0x0000F363
		public static bool IsImmutableKnownDeliveryExtension(string extensionName)
		{
			return ExtensionExtension.GetKnownExtensionType(extensionName) == KnownDeliveryExtensions.ReportServerPowerBI;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00011174 File Offset: 0x0000F374
		public static global::Model.Extension ToWebApiModel(this Microsoft.SqlServer.ReportingServices2010.Extension soapExtension)
		{
			if (soapExtension == null)
			{
				throw new ArgumentNullException("soapExtension");
			}
			ExtensionType extensionType = ExtensionType.All;
			string extensionTypeName = soapExtension.ExtensionTypeName;
			if (!(extensionTypeName == "Data"))
			{
				if (!(extensionTypeName == "Delivery"))
				{
					if (extensionTypeName == "Render")
					{
						extensionType = ExtensionType.Render;
					}
				}
				else
				{
					extensionType = ExtensionType.Delivery;
				}
			}
			else
			{
				extensionType = ExtensionType.Data;
			}
			global::Model.Extension extension;
			if (extensionType == ExtensionType.Delivery)
			{
				extension = new DeliveryExtension
				{
					IsImmutable = ExtensionExtension.IsImmutableKnownDeliveryExtension(soapExtension.Name)
				};
			}
			else
			{
				extension = new global::Model.Extension();
			}
			extension.ExtensionType = extensionType;
			extension.Name = soapExtension.Name;
			extension.LocalizedName = soapExtension.LocalizedName;
			extension.Visible = soapExtension.Visible;
			return extension;
		}

		// Token: 0x040000B9 RID: 185
		private const string PowerBIDeliveryExtensionName = "Report Server PowerBI";

		// Token: 0x040000BA RID: 186
		private const string EmailDeliveryExtensionName = "Report Server Email";

		// Token: 0x040000BB RID: 187
		private const string FileShareDeliveryExtensionName = "Report Server FileShare";
	}
}
