using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200007C RID: 124
	internal sealed class ClrTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x0000E986 File Offset: 0x0000CB86
		internal override object Parse(string text)
		{
			return PlatformHelper.GetTypeOrThrow(text);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000E98E File Offset: 0x0000CB8E
		internal override string ToString(object instance)
		{
			return ((Type)instance).AssemblyQualifiedName;
		}
	}
}
