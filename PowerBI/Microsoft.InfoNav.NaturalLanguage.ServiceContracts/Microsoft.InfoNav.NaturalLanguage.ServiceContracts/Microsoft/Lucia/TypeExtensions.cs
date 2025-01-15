using System;
using System.IO;

namespace Microsoft.Lucia
{
	// Token: 0x02000015 RID: 21
	public static class TypeExtensions
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002A4E File Offset: 0x00000C4E
		public static Stream GetManifestResourceStream(this Type type, string name)
		{
			return type.Assembly.GetManifestResourceStream(type, name);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A5D File Offset: 0x00000C5D
		public static StreamReader GetManifestResourceStreamReader(this Type type, string name)
		{
			return new StreamReader(type.GetManifestResourceStream(name));
		}
	}
}
