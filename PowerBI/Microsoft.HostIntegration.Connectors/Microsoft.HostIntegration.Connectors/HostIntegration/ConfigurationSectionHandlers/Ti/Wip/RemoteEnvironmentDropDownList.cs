using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x02000590 RID: 1424
	public class RemoteEnvironmentDropDownList : StringConverter
	{
		// Token: 0x0600313A RID: 12602 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000A4ADC File Offset: 0x000A2CDC
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ConfigurationElementCollection remoteEnvironments = ((WipObject)context.Instance).GetRemoteEnvironments();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in remoteEnvironments)
			{
				RemoteEnvironment remoteEnvironment = (RemoteEnvironment)obj;
				if (((WipObject)context.Instance).RemoteEnvironmentType == remoteEnvironment.RemoteEnvironmentType || ((WipObject)context.Instance).RemoteEnvironmentType == RemoteEnvironmentType.Unknown)
				{
					arrayList.Add(remoteEnvironment.Name);
				}
			}
			arrayList.Add("");
			return new TypeConverter.StandardValuesCollection(arrayList);
		}
	}
}
