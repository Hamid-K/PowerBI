using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200310E RID: 12558
	internal static class ValueExtensionMethods
	{
		// Token: 0x0601B3CA RID: 111562 RVA: 0x003734DD File Offset: 0x003716DD
		public static byte[] Bytes(this int value)
		{
			return BitConverter.GetBytes(value);
		}

		// Token: 0x0601B3CB RID: 111563 RVA: 0x003734E5 File Offset: 0x003716E5
		public static byte[] Bytes(this ushort value)
		{
			return BitConverter.GetBytes(value);
		}

		// Token: 0x0601B3CC RID: 111564 RVA: 0x003734F0 File Offset: 0x003716F0
		public static byte[] Bytes(this ParticleType value)
		{
			return new byte[] { (byte)value };
		}

		// Token: 0x0601B3CD RID: 111565 RVA: 0x0037350C File Offset: 0x0037170C
		public static byte[] Bytes(this XsdAttributeUse value)
		{
			return new byte[] { (byte)value };
		}

		// Token: 0x0601B3CE RID: 111566 RVA: 0x00373528 File Offset: 0x00371728
		public static byte[] Bytes(this byte value)
		{
			return new byte[] { value };
		}
	}
}
