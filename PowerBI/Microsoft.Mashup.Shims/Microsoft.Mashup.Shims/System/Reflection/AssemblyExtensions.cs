using System;

namespace System.Reflection
{
	// Token: 0x02000007 RID: 7
	public static class AssemblyExtensions
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000212D File Offset: 0x0000032D
		public static string GetLocation(this Assembly assembly)
		{
			return assembly.CodeBase;
		}
	}
}
