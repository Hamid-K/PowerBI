using System;

namespace System.Net.Http
{
	// Token: 0x02000003 RID: 3
	internal static class CloneableExtensions
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000023E4 File Offset: 0x000005E4
		internal static T Clone<T>(this T value) where T : ICloneable
		{
			return (T)((object)value.Clone());
		}
	}
}
