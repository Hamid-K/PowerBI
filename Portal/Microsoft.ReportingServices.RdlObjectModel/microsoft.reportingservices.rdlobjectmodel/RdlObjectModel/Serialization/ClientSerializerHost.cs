using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002D2 RID: 722
	public class ClientSerializerHost : ISerializerHost
	{
		// Token: 0x06001641 RID: 5697 RVA: 0x00033518 File Offset: 0x00031718
		public Type GetSubstituteType(Type type)
		{
			return type;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0003351B File Offset: 0x0003171B
		public void OnDeserialization(object value)
		{
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00033520 File Offset: 0x00031720
		public IEnumerable<ExtensionNamespace> GetExtensionNamespaces()
		{
			return new ExtensionNamespace[]
			{
				new ExtensionNamespace("rd", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner", false),
				new ExtensionNamespace("cl", "http://schemas.microsoft.com/sqlserver/reporting/2010/01/componentdefinition", false),
				new ExtensionNamespace("wa", "http://schemas.microsoft.com/sqlserver/reporting/webauthoring", false),
				new ExtensionNamespace("df", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition/defaultfontfamily", true),
				new ExtensionNamespace("am", "http://schemas.microsoft.com/sqlserver/reporting/authoringmetadata", false),
				new ExtensionNamespace("ap", "http://schemas.microsoft.com/sqlserver/reporting/accessibilityproperties", false)
			};
		}
	}
}
