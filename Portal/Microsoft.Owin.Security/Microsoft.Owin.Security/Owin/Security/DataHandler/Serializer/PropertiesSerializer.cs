using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Owin.Security.DataHandler.Serializer
{
	// Token: 0x0200002F RID: 47
	public class PropertiesSerializer : IDataSerializer<AuthenticationProperties>
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00003638 File Offset: 0x00001838
		public byte[] Serialize(AuthenticationProperties model)
		{
			byte[] array;
			using (MemoryStream memory = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(memory))
				{
					PropertiesSerializer.Write(writer, model);
					writer.Flush();
					array = memory.ToArray();
				}
			}
			return array;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000369C File Offset: 0x0000189C
		public AuthenticationProperties Deserialize(byte[] data)
		{
			AuthenticationProperties authenticationProperties;
			using (MemoryStream memory = new MemoryStream(data))
			{
				using (BinaryReader reader = new BinaryReader(memory))
				{
					authenticationProperties = PropertiesSerializer.Read(reader);
				}
			}
			return authenticationProperties;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000036F4 File Offset: 0x000018F4
		public static void Write(BinaryWriter writer, AuthenticationProperties properties)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			writer.Write(1);
			writer.Write(properties.Dictionary.Count);
			foreach (KeyValuePair<string, string> kv in properties.Dictionary)
			{
				writer.Write(kv.Key);
				writer.Write(kv.Value);
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003788 File Offset: 0x00001988
		public static AuthenticationProperties Read(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.ReadInt32() != 1)
			{
				return null;
			}
			int count = reader.ReadInt32();
			Dictionary<string, string> extra = new Dictionary<string, string>(count);
			for (int index = 0; index != count; index++)
			{
				string key = reader.ReadString();
				string value = reader.ReadString();
				extra.Add(key, value);
			}
			return new AuthenticationProperties(extra);
		}

		// Token: 0x0400004E RID: 78
		private const int FormatVersion = 1;
	}
}
