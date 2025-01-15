using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x02000044 RID: 68
	internal static class ExtensionSettingsExtensions
	{
		// Token: 0x0600026D RID: 621 RVA: 0x000108E0 File Offset: 0x0000EAE0
		public static Microsoft.SqlServer.ReportingServices2010.ExtensionSettings ToSoapClass(this global::Model.ExtensionSettings extensionSettings)
		{
			if (extensionSettings == null)
			{
				throw new ArgumentNullException("extensionSettings");
			}
			Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings2 = new Microsoft.SqlServer.ReportingServices2010.ExtensionSettings();
			extensionSettings2.Extension = extensionSettings.Extension;
			Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings3 = extensionSettings2;
			Microsoft.SqlServer.ReportingServices2010.ParameterValueOrFieldReference[] array;
			if (extensionSettings.ParameterValues == null)
			{
				array = new Microsoft.SqlServer.ReportingServices2010.ParameterValueOrFieldReference[0];
			}
			else
			{
				array = extensionSettings.ParameterValues.Select((global::Model.ParameterValue parameterValue) => parameterValue.ToSoapParameterValueOrFieldReference()).ToArray<Microsoft.SqlServer.ReportingServices2010.ParameterValueOrFieldReference>();
			}
			extensionSettings3.ParameterValues = array;
			return extensionSettings2;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00010954 File Offset: 0x0000EB54
		public static global::Model.ExtensionSettings ToWebApiModel(this Microsoft.SqlServer.ReportingServices2010.ExtensionSettings soapExtensionSettings)
		{
			if (soapExtensionSettings == null)
			{
				return null;
			}
			global::Model.ExtensionSettings extensionSettings = new global::Model.ExtensionSettings();
			extensionSettings.Extension = soapExtensionSettings.Extension;
			IEnumerable<global::Model.ParameterValue> enumerable2;
			if (soapExtensionSettings.ParameterValues == null)
			{
				IEnumerable<global::Model.ParameterValue> enumerable = new List<global::Model.ParameterValue>();
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = soapExtensionSettings.ParameterValues.Select((Microsoft.SqlServer.ReportingServices2010.ParameterValueOrFieldReference parameterValue) => parameterValue.ToWebAPIParameterValue());
			}
			extensionSettings.ParameterValues = enumerable2;
			return extensionSettings;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000109B8 File Offset: 0x0000EBB8
		public static global::Model.ExtensionSettings ToWebApiModel(this Microsoft.ReportingServices.Library.Soap.ExtensionSettings soapExtensionSettings)
		{
			if (soapExtensionSettings == null)
			{
				return null;
			}
			global::Model.ExtensionSettings extensionSettings = new global::Model.ExtensionSettings();
			extensionSettings.Extension = soapExtensionSettings.Extension;
			IEnumerable<global::Model.ParameterValue> enumerable2;
			if (soapExtensionSettings.ParameterValues == null)
			{
				IEnumerable<global::Model.ParameterValue> enumerable = new List<global::Model.ParameterValue>();
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = soapExtensionSettings.ParameterValues.Select((Microsoft.ReportingServices.Library.Soap.ParameterValueOrFieldReference parameterValue) => parameterValue.ToWebAPIParameterValue());
			}
			extensionSettings.ParameterValues = enumerable2;
			return extensionSettings;
		}
	}
}
