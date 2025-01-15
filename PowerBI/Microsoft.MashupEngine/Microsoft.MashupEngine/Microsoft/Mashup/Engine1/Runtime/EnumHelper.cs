using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012E8 RID: 4840
	internal static class EnumHelper
	{
		// Token: 0x06008036 RID: 32822 RVA: 0x001B542E File Offset: 0x001B362E
		public static bool TryParse<T>(string value, out T t)
		{
			if (Enum.IsDefined(typeof(T), value))
			{
				t = (T)((object)Enum.Parse(typeof(T), value));
				return true;
			}
			t = default(T);
			return false;
		}
	}
}
