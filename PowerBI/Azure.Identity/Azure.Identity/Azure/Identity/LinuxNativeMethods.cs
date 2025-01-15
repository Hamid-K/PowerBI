using System;
using System.Runtime.InteropServices;

namespace Azure.Identity
{
	// Token: 0x0200006B RID: 107
	internal static class LinuxNativeMethods
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x0000AD8E File Offset: 0x00008F8E
		public static IntPtr secret_schema_new(string name, LinuxNativeMethods.SecretSchemaFlags flags, string attribute1, LinuxNativeMethods.SecretSchemaAttributeType attribute1Type, string attribute2, LinuxNativeMethods.SecretSchemaAttributeType attribute2Type)
		{
			return LinuxNativeMethods.Imports.secret_schema_new(name, (int)flags, attribute1, (int)attribute1Type, attribute2, (int)attribute2Type, IntPtr.Zero);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000ADA4 File Offset: 0x00008FA4
		public static string secret_password_lookup_sync(IntPtr schemaPtr, IntPtr cancellable, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value)
		{
			IntPtr intPtr2;
			IntPtr intPtr = LinuxNativeMethods.Imports.secret_password_lookup_sync(schemaPtr, cancellable, out intPtr2, attribute1Type, attribute1Value, attribute2Type, attribute2Value, IntPtr.Zero);
			LinuxNativeMethods.HandleError(intPtr2, "An error was encountered while reading secret from keyring");
			if (!(intPtr != IntPtr.Zero))
			{
				return null;
			}
			return Marshal.PtrToStringAnsi(intPtr);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000ADE8 File Offset: 0x00008FE8
		public static void secret_password_store_sync(IntPtr schemaPtr, string collection, string label, string password, IntPtr cancellable, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value)
		{
			IntPtr intPtr;
			LinuxNativeMethods.Imports.secret_password_store_sync(schemaPtr, collection, label, password, cancellable, out intPtr, attribute1Type, attribute1Value, attribute2Type, attribute2Value, IntPtr.Zero);
			LinuxNativeMethods.HandleError(intPtr, "An error was encountered while writing secret to keyring");
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000AE1C File Offset: 0x0000901C
		public static void secret_password_clear_sync(IntPtr schemaPtr, IntPtr cancellable, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value)
		{
			IntPtr intPtr;
			LinuxNativeMethods.Imports.secret_password_clear_sync(schemaPtr, cancellable, out intPtr, attribute1Type, attribute1Value, attribute2Type, attribute2Value, IntPtr.Zero);
			LinuxNativeMethods.HandleError(intPtr, "An error was encountered while clearing secret from keyring ");
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000AE49 File Offset: 0x00009049
		public static void secret_password_free(IntPtr passwordPtr)
		{
			if (passwordPtr != IntPtr.Zero)
			{
				LinuxNativeMethods.Imports.secret_password_free(passwordPtr);
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000AE5E File Offset: 0x0000905E
		public static void secret_schema_unref(IntPtr schemaPtr)
		{
			if (schemaPtr != IntPtr.Zero)
			{
				LinuxNativeMethods.Imports.secret_schema_unref(schemaPtr);
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000AE74 File Offset: 0x00009074
		private static void HandleError(IntPtr errorPtr, string errorMessage)
		{
			if (errorPtr == IntPtr.Zero)
			{
				return;
			}
			LinuxNativeMethods.GError gerror;
			try
			{
				gerror = Marshal.PtrToStructure<LinuxNativeMethods.GError>(errorPtr);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(string.Format("An exception was encountered while processing libsecret error: {0}", ex), ex);
			}
			throw new InvalidOperationException(string.Format("{0}, domain:'{1}', code:'{2}', message:'{3}'", new object[] { errorMessage, gerror.Domain, gerror.Code, gerror.Message }));
		}

		// Token: 0x04000220 RID: 544
		public const string SECRET_COLLECTION_SESSION = "session";

		// Token: 0x020000F1 RID: 241
		public enum SecretSchemaAttributeType
		{
			// Token: 0x040004D1 RID: 1233
			SECRET_SCHEMA_ATTRIBUTE_STRING,
			// Token: 0x040004D2 RID: 1234
			SECRET_SCHEMA_ATTRIBUTE_INTEGER,
			// Token: 0x040004D3 RID: 1235
			SECRET_SCHEMA_ATTRIBUTE_BOOLEAN
		}

		// Token: 0x020000F2 RID: 242
		public enum SecretSchemaFlags
		{
			// Token: 0x040004D5 RID: 1237
			SECRET_SCHEMA_NONE,
			// Token: 0x040004D6 RID: 1238
			SECRET_SCHEMA_DONT_MATCH_NAME = 2
		}

		// Token: 0x020000F3 RID: 243
		internal struct GError
		{
			// Token: 0x040004D7 RID: 1239
			public uint Domain;

			// Token: 0x040004D8 RID: 1240
			public int Code;

			// Token: 0x040004D9 RID: 1241
			public string Message;
		}

		// Token: 0x020000F4 RID: 244
		private static class Imports
		{
			// Token: 0x06000597 RID: 1431
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			[DllImport("libsecret-1.so.0", BestFitMapping = false, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
			public static extern IntPtr secret_schema_new(string name, int flags, string attribute1, int attribute1Type, string attribute2, int attribute2Type, IntPtr end);

			// Token: 0x06000598 RID: 1432
			[DllImport("libsecret-1.so.0", CallingConvention = CallingConvention.StdCall)]
			public static extern void secret_schema_unref(IntPtr schema);

			// Token: 0x06000599 RID: 1433
			[DllImport("libsecret-1.so.0", BestFitMapping = false, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
			public static extern IntPtr secret_password_lookup_sync(IntPtr schema, IntPtr cancellable, out IntPtr error, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value, IntPtr end);

			// Token: 0x0600059A RID: 1434
			[DllImport("libsecret-1.so.0", BestFitMapping = false, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
			public static extern int secret_password_store_sync(IntPtr schema, string collection, string label, string password, IntPtr cancellable, out IntPtr error, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value, IntPtr end);

			// Token: 0x0600059B RID: 1435
			[DllImport("libsecret-1.so.0", BestFitMapping = false, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
			public static extern int secret_password_clear_sync(IntPtr schema, IntPtr cancellable, out IntPtr error, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value, IntPtr end);

			// Token: 0x0600059C RID: 1436
			[DllImport("libsecret-1.so.0", CallingConvention = CallingConvention.StdCall)]
			public static extern void secret_password_free(IntPtr password);
		}
	}
}
