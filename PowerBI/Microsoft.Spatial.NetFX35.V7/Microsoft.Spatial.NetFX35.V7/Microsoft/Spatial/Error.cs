using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000071 RID: 113
	internal static class Error
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x00006D41 File Offset: 0x00004F41
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00006D49 File Offset: 0x00004F49
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00006D51 File Offset: 0x00004F51
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00006D58 File Offset: 0x00004F58
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
