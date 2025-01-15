using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x0200001C RID: 28
	internal static class Libsecret
	{
		// Token: 0x06000071 RID: 113
		[DllImport("libsecret-1.so.0", CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr secret_schema_new(string name, int flags, string attribute1, int attribute1Type, string attribute2, int attribute2Type, IntPtr end);

		// Token: 0x06000072 RID: 114
		[DllImport("libsecret-1.so.0", CallingConvention = CallingConvention.StdCall)]
		public static extern int secret_password_store_sync(IntPtr schema, string collection, string label, string password, IntPtr cancellable, out IntPtr error, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value, IntPtr end);

		// Token: 0x06000073 RID: 115
		[DllImport("libsecret-1.so.0", CallingConvention = CallingConvention.StdCall)]
		public static extern string secret_password_lookup_sync(IntPtr schema, IntPtr cancellable, out IntPtr error, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value, IntPtr end);

		// Token: 0x06000074 RID: 116
		[DllImport("libsecret-1.so.0", CallingConvention = CallingConvention.StdCall)]
		public static extern int secret_password_clear_sync(IntPtr schema, IntPtr cancellable, out IntPtr error, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value, IntPtr end);

		// Token: 0x0200002E RID: 46
		public enum SecretSchemaAttributeType
		{
			// Token: 0x040000BD RID: 189
			SECRET_SCHEMA_ATTRIBUTE_STRING,
			// Token: 0x040000BE RID: 190
			SECRET_SCHEMA_ATTRIBUTE_INTEGER,
			// Token: 0x040000BF RID: 191
			SECRET_SCHEMA_ATTRIBUTE_BOOLEAN
		}

		// Token: 0x0200002F RID: 47
		public enum SecretSchemaFlags
		{
			// Token: 0x040000C1 RID: 193
			SECRET_SCHEMA_NONE,
			// Token: 0x040000C2 RID: 194
			SECRET_SCHEMA_DONT_MATCH_NAME = 2
		}
	}
}
