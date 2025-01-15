using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x0200056F RID: 1391
	public class ObjectInterfaceDropDownList : StringConverter
	{
		// Token: 0x06002F3C RID: 12092 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x000A210C File Offset: 0x000A030C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ConfigurationElementCollection hipObjects = ((ResolutionEntry)context.Instance).GetHipObjects();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in hipObjects)
			{
				HipObject hipObject = (HipObject)obj;
				arrayList.Add(hipObject.MetaDataInterface);
			}
			return new TypeConverter.StandardValuesCollection(arrayList);
		}
	}
}
