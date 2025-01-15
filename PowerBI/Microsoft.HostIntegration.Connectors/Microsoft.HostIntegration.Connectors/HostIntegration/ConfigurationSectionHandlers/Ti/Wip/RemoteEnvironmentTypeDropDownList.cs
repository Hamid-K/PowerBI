using System;
using System.ComponentModel;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x02000591 RID: 1425
	public class RemoteEnvironmentTypeDropDownList : StringConverter
	{
		// Token: 0x0600313E RID: 12606 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x000A4B88 File Offset: 0x000A2D88
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			WipObject wipObject = context.Instance as WipObject;
			if (wipObject.RemoteEnvironmentType == RemoteEnvironmentType.Unknown)
			{
				return new TypeConverter.StandardValuesCollection(new string[]
				{
					"ElmLink", "ElmUserData", "TrmLink", "TrmUserData", "HttpLink", "HttpUserData", "DistributedProgramCall", "ImsConnect", "SnaLink", "SnaUserData",
					"ImsLu62", "SystemzSocketsLink", "SystemzSocketsUserData", "SystemiSocketsUserData"
				});
			}
			return new TypeConverter.StandardValuesCollection(new string[] { wipObject.RemoteEnvironmentTypeId });
		}
	}
}
