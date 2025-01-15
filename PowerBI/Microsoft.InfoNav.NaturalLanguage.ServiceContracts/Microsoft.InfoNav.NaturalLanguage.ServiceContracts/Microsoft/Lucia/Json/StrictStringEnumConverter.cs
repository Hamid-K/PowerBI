using System;
using Newtonsoft.Json.Converters;

namespace Microsoft.Lucia.Json
{
	// Token: 0x02000028 RID: 40
	public class StrictStringEnumConverter : StringEnumConverter
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00003412 File Offset: 0x00001612
		public StrictStringEnumConverter()
		{
			base.AllowIntegerValues = false;
		}
	}
}
