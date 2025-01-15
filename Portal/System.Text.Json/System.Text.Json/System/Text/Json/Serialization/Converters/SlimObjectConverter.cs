using System;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000E7 RID: 231
	internal sealed class SlimObjectConverter : ObjectConverter
	{
		// Token: 0x06000C53 RID: 3155 RVA: 0x000305A9 File Offset: 0x0002E7A9
		public SlimObjectConverter(IJsonTypeInfoResolver originatingResolver)
		{
			this._originatingResolver = originatingResolver;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x000305B8 File Offset: 0x0002E7B8
		public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			ThrowHelper.ThrowNotSupportedException_NoMetadataForType(typeToConvert, this._originatingResolver);
			return null;
		}

		// Token: 0x04000408 RID: 1032
		private readonly IJsonTypeInfoResolver _originatingResolver;
	}
}
