using System;
using System.Linq;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x0200003E RID: 62
	internal static class ExtensionParameterExtensions
	{
		// Token: 0x06000264 RID: 612 RVA: 0x000105B8 File Offset: 0x0000E7B8
		public static global::Model.ExtensionParameter ToWebAPI(this Microsoft.SqlServer.ReportingServices2010.ExtensionParameter extensionParameter)
		{
			if (extensionParameter == null)
			{
				throw new ArgumentNullException("extensionParameter");
			}
			global::Model.ExtensionParameter extensionParameter2 = new global::Model.ExtensionParameter();
			extensionParameter2.Name = extensionParameter.Name;
			extensionParameter2.DisplayName = extensionParameter.DisplayName;
			extensionParameter2.Encrypted = extensionParameter.Encrypted;
			extensionParameter2.IsPassword = extensionParameter.IsPassword;
			extensionParameter2.ReadOnly = extensionParameter.ReadOnly;
			extensionParameter2.Required = extensionParameter.Required;
			extensionParameter2.Value = extensionParameter.Value;
			global::Model.ExtensionParameter extensionParameter3 = extensionParameter2;
			global::Model.ValidValue[] array;
			if (extensionParameter.ValidValues == null)
			{
				array = new global::Model.ValidValue[0];
			}
			else
			{
				array = extensionParameter.ValidValues.Select((Microsoft.SqlServer.ReportingServices2010.ValidValue validValue) => validValue.ToWebAPI()).ToArray<global::Model.ValidValue>();
			}
			extensionParameter3.ValidValues = array;
			extensionParameter2.ValidValuesIsNull = extensionParameter.ValidValues == null;
			extensionParameter2.Error = extensionParameter.Error;
			return extensionParameter2;
		}
	}
}
