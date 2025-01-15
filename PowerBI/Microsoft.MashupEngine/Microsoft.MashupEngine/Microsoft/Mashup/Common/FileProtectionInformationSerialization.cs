using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BF5 RID: 7157
	public static class FileProtectionInformationSerialization
	{
		// Token: 0x0600B2A4 RID: 45732 RVA: 0x00245D5F File Offset: 0x00243F5F
		public static FileProtectionInformation ReadFileProtectionInformation(BinaryReader reader)
		{
			return new FileProtectionInformation
			{
				Encrypted = reader.ReadBool(),
				Classified = reader.ReadBool(),
				ProtectionInformation = reader.ReadNullable(new Func<BinaryReader, ProtectionInformation>(FileProtectionInformationSerialization.ReadProtectionInformation))
			};
		}

		// Token: 0x0600B2A5 RID: 45733 RVA: 0x00245D96 File Offset: 0x00243F96
		public static void WriteFileProtectionInformation(BinaryWriter writer, FileProtectionInformation fileProtectionInformation)
		{
			writer.WriteBool(fileProtectionInformation.Encrypted);
			writer.WriteBool(fileProtectionInformation.Classified);
			writer.WriteNullable(fileProtectionInformation.ProtectionInformation, new Action<BinaryWriter, ProtectionInformation>(FileProtectionInformationSerialization.WriteProtectionInformation));
		}

		// Token: 0x0600B2A6 RID: 45734 RVA: 0x00245DC8 File Offset: 0x00243FC8
		public static ProtectionInformation ReadProtectionInformation(BinaryReader reader)
		{
			return new ProtectionInformation
			{
				Id = reader.ReadNullableString(),
				Name = reader.ReadNullableString(),
				Description = reader.ReadNullableString(),
				Sensitivity = reader.ReadInt32(),
				IsActive = reader.ReadBool(),
				Parent = reader.ReadNullable(new Func<BinaryReader, ProtectionInformation>(FileProtectionInformationSerialization.ReadProtectionInformation))
			};
		}

		// Token: 0x0600B2A7 RID: 45735 RVA: 0x00245E30 File Offset: 0x00244030
		public static void WriteProtectionInformation(BinaryWriter writer, ProtectionInformation protectionInformation)
		{
			writer.WriteNullableString(protectionInformation.Id);
			writer.WriteNullableString(protectionInformation.Name);
			writer.WriteNullableString(protectionInformation.Description);
			writer.WriteInt32(protectionInformation.Sensitivity);
			writer.WriteBool(protectionInformation.IsActive);
			writer.WriteNullable(protectionInformation.Parent, new Action<BinaryWriter, ProtectionInformation>(FileProtectionInformationSerialization.WriteProtectionInformation));
		}
	}
}
