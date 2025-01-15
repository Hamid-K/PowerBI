using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	internal class RDLSandboxingPropertyBag : Dictionary<string, ConfigurationPropertyInfo>
	{
		// Token: 0x060000FF RID: 255 RVA: 0x000047EC File Offset: 0x000029EC
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

		// Token: 0x06000100 RID: 256 RVA: 0x000038A5 File Offset: 0x00001AA5
		public RDLSandboxingPropertyBag()
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000038AD File Offset: 0x00001AAD
		protected RDLSandboxingPropertyBag(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
