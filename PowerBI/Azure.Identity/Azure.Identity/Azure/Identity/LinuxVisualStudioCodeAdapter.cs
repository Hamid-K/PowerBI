using System;
using System.IO;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x0200006C RID: 108
	internal sealed class LinuxVisualStudioCodeAdapter : IVisualStudioCodeAdapter
	{
		// Token: 0x060003AC RID: 940 RVA: 0x0000AEFC File Offset: 0x000090FC
		public string GetUserSettingsPath()
		{
			return LinuxVisualStudioCodeAdapter.s_userSettingsJsonPath;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000AF04 File Offset: 0x00009104
		public string GetCredentials(string serviceName, string accountName)
		{
			Argument.AssertNotNullOrEmpty(serviceName, "serviceName");
			Argument.AssertNotNullOrEmpty(accountName, "accountName");
			IntPtr libsecretSchema = LinuxVisualStudioCodeAdapter.GetLibsecretSchema();
			string text;
			try
			{
				text = LinuxVisualStudioCodeAdapter.LookupPassword(in libsecretSchema, serviceName, accountName);
			}
			finally
			{
				LinuxNativeMethods.secret_schema_unref(libsecretSchema);
			}
			return text;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000AF54 File Offset: 0x00009154
		private static string LookupPassword(in IntPtr schemaPtr, string serviceName, string accountName)
		{
			return LinuxNativeMethods.secret_password_lookup_sync(schemaPtr, IntPtr.Zero, "service", serviceName, "account", accountName);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000AF6E File Offset: 0x0000916E
		private static IntPtr GetLibsecretSchema()
		{
			return LinuxNativeMethods.secret_schema_new("org.freedesktop.Secret.Generic", LinuxNativeMethods.SecretSchemaFlags.SECRET_SCHEMA_DONT_MATCH_NAME, "service", LinuxNativeMethods.SecretSchemaAttributeType.SECRET_SCHEMA_ATTRIBUTE_STRING, "account", LinuxNativeMethods.SecretSchemaAttributeType.SECRET_SCHEMA_ATTRIBUTE_STRING);
		}

		// Token: 0x04000221 RID: 545
		private static readonly string s_userSettingsJsonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Code", "User", "settings.json");
	}
}
