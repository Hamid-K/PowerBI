using System;

namespace Microsoft.Lucia.Yaml
{
	// Token: 0x02000019 RID: 25
	public static class SerializationOptionsExtensions
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00002B1E File Offset: 0x00000D1E
		public static bool IsCompact(this YamlSerializationOptions value)
		{
			return SerializationOptionsExtensions.HasFlag(value, YamlSerializationOptions.Compact);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002B27 File Offset: 0x00000D27
		public static bool IsLineBreakAfter(this YamlSerializationOptions value)
		{
			return SerializationOptionsExtensions.HasFlag(value, YamlSerializationOptions.LineBreakAfter);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002B30 File Offset: 0x00000D30
		private static bool HasFlag(YamlSerializationOptions value, YamlSerializationOptions flag)
		{
			return (value & flag) == flag;
		}
	}
}
