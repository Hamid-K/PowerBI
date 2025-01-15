using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000571 RID: 1393
	public class SecurityPolicyDropDownList : StringConverter
	{
		// Token: 0x06002F43 RID: 12099 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000A2390 File Offset: 0x000A0590
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ConfigurationElementCollection essoSecurityPolicies = ((ResolutionEntry)context.Instance).GetEssoSecurityPolicies();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in essoSecurityPolicies)
			{
				EssoSecurityPolicy essoSecurityPolicy = (EssoSecurityPolicy)obj;
				arrayList.Add(essoSecurityPolicy.Name);
			}
			return new TypeConverter.StandardValuesCollection(arrayList);
		}
	}
}
