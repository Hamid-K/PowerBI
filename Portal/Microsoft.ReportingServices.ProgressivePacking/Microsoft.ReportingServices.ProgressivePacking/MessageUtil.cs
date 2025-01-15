using System;
using System.IO;
using System.Text;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x02000013 RID: 19
	internal static class MessageUtil
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002EAC File Offset: 0x000010AC
		internal static string[] ReadStringArray(BinaryReader reader)
		{
			string[] array = new string[reader.ReadInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = reader.ReadString();
			}
			return array;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002EE0 File Offset: 0x000010E0
		internal static void WriteStringArray(BinaryWriter writer, string[] value)
		{
			writer.Write(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				writer.Write(value[i]);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002F0D File Offset: 0x0000110D
		internal static void WriteByteArray(BinaryWriter writer, byte[] value, int offset, int length)
		{
			writer.Write(length);
			writer.Write(value, offset, length);
		}

		// Token: 0x0400002D RID: 45
		internal static readonly Encoding StringEncoding = Encoding.UTF8;
	}
}
