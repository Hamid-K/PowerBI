using System;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x02000199 RID: 409
	internal static class LsdlAsserts
	{
		// Token: 0x06000846 RID: 2118 RVA: 0x00010C4B File Offset: 0x0000EE4B
		internal static DateTime DateTimeShouldBeUtc(DateTime value)
		{
			return value;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00010C4E File Offset: 0x0000EE4E
		internal static DateTime? DateTimeShouldBeUtc(DateTime? value)
		{
			if (value != null)
			{
				LsdlAsserts.DateTimeShouldBeUtc(value.Value);
			}
			return value;
		}
	}
}
