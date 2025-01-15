using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	internal sealed class MapTileServerConfigurationPropertyBag : Dictionary<string, ConfigurationPropertyInfo>
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00003874 File Offset: 0x00001A74
		public void Add(string propertyId, string value)
		{
			if (base.ContainsKey(propertyId))
			{
				XmlParseExceptions.ThrowInvalidFormat(propertyId);
			}
			base.Add(propertyId, new ConfigurationPropertyInfo
			{
				SpecifiedValue = value
			});
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000038A5 File Offset: 0x00001AA5
		public MapTileServerConfigurationPropertyBag()
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000038AD File Offset: 0x00001AAD
		private MapTileServerConfigurationPropertyBag(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
