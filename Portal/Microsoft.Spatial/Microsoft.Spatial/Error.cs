using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000074 RID: 116
	internal static class Error
	{
		// Token: 0x06000337 RID: 823 RVA: 0x00007954 File Offset: 0x00005B54
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000795C File Offset: 0x00005B5C
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00007964 File Offset: 0x00005B64
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000796B File Offset: 0x00005B6B
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
