using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	internal sealed class OAuthConnectionConfigurationPropertyBag : Dictionary<string, ConfigurationPropertyInfo>
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public void Add(string propertyId, string value)
		{
			if (base.ContainsKey(propertyId))
			{
				XmlParseExceptions.ThrowInvalidFormat(propertyId);
			}
			base.Add(propertyId, new ConfigurationPropertyInfo
			{
				SpecifiedValue = value,
				Value = value
			});
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000038A5 File Offset: 0x00001AA5
		public OAuthConnectionConfigurationPropertyBag()
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000038AD File Offset: 0x00001AAD
		private OAuthConnectionConfigurationPropertyBag(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
