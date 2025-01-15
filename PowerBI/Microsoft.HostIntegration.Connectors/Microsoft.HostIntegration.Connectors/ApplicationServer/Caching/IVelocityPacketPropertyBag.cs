using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000162 RID: 354
	internal interface IVelocityPacketPropertyBag
	{
		// Token: 0x06000AEF RID: 2799
		bool Exists(VelocityPacketProperty key);

		// Token: 0x06000AF0 RID: 2800
		bool TryGetElement(VelocityPacketProperty key, out byte[] value);

		// Token: 0x06000AF1 RID: 2801
		bool SetElement(VelocityPacketProperty key, byte[] value);

		// Token: 0x06000AF2 RID: 2802
		IEnumerable<VelocityPacketProperty> GetRequestedProperties();

		// Token: 0x06000AF3 RID: 2803
		void AddRequestedProperty(VelocityPacketProperty property);
	}
}
