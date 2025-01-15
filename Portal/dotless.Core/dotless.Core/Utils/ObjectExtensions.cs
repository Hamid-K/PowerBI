using System;

namespace dotless.Core.Utils
{
	// Token: 0x0200000F RID: 15
	public static class ObjectExtensions
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000032FA File Offset: 0x000014FA
		public static T Do<T>(this T obj, Action<T> action)
		{
			action(obj);
			return obj;
		}
	}
}
