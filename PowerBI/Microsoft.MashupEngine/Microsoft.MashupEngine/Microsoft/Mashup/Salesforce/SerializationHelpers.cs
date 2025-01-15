using System;
using System.IO;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001DA RID: 474
	internal static class SerializationHelpers
	{
		// Token: 0x06000964 RID: 2404 RVA: 0x00012EF7 File Offset: 0x000110F7
		public static void WriteNullable(this BinaryWriter writer, string s)
		{
			if (s == null)
			{
				writer.Write(false);
				return;
			}
			writer.Write(true);
			writer.Write(s);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00012F12 File Offset: 0x00011112
		public static string ReadNullableString(this BinaryReader reader)
		{
			if (!reader.ReadBoolean())
			{
				return null;
			}
			return reader.ReadString();
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00012F24 File Offset: 0x00011124
		public static string AsNullableString(this Value value)
		{
			if (value.IsNull)
			{
				return null;
			}
			return value.AsText.String;
		}
	}
}
