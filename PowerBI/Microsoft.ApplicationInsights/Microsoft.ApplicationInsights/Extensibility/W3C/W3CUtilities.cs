using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.ApplicationInsights.Extensibility.W3C
{
	// Token: 0x02000064 RID: 100
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class W3CUtilities
	{
		// Token: 0x06000306 RID: 774 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static string GenerateTraceId()
		{
			return W3CUtilities.GenerateId(Guid.NewGuid().ToByteArray(), 0, 16);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000E618 File Offset: 0x0000C818
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal static string GenerateSpanId()
		{
			return W3CUtilities.GenerateId(Guid.NewGuid().ToByteArray(), 0, 8);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000E63C File Offset: 0x0000C83C
		private static string GenerateId(byte[] bytes, int start, int length)
		{
			char[] array = new char[length * 2];
			for (int i = start; i < start + length; i++)
			{
				uint num = W3CUtilities.Lookup32[(int)bytes[i]];
				array[2 * i] = (char)num;
				array[2 * i + 1] = (char)(num >> 16);
			}
			return new string(array);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000E684 File Offset: 0x0000C884
		private static uint[] CreateLookup32()
		{
			uint[] array = new uint[256];
			for (int i = 0; i < 256; i++)
			{
				string text = i.ToString("x2", CultureInfo.InvariantCulture);
				array[i] = (uint)(text[0] + ((uint)text[1] << 16));
			}
			return array;
		}

		// Token: 0x04000150 RID: 336
		private static readonly uint[] Lookup32 = W3CUtilities.CreateLookup32();
	}
}
