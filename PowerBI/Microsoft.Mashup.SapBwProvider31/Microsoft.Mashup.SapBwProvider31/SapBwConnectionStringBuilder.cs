using System;
using System.Data.Common;
using System.Globalization;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200002C RID: 44
	public sealed class SapBwConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x0600023F RID: 575 RVA: 0x0000A484 File Offset: 0x00008684
		public bool TryGetString(string keyword, out string value)
		{
			object obj;
			if (this.TryGetValue(keyword, out obj) && obj != null)
			{
				value = obj as string;
				return !string.IsNullOrEmpty(value);
			}
			value = null;
			return false;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000A4B8 File Offset: 0x000086B8
		public bool TryGetBool(string keyword, out bool value)
		{
			string text;
			if (this.TryGetString(keyword, out text))
			{
				return bool.TryParse(text, out value);
			}
			value = false;
			return false;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A4DC File Offset: 0x000086DC
		public bool TryGetInt(string keyword, out int value)
		{
			string text;
			if (this.TryGetString(keyword, out text))
			{
				return int.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
			}
			value = 0;
			return false;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000A508 File Offset: 0x00008708
		public bool TryGetSncLibrary(out string sncLibrary)
		{
			if (this.TryGetString("SNCLibrary", out sncLibrary))
			{
				return true;
			}
			string text;
			if (this.TryGetString("SSOType", out text))
			{
				if (text == "NTLM")
				{
					sncLibrary = ((IntPtr.Size == 8) ? "GX64NTLM.DLL" : "GSSNTLM.DLL");
					return true;
				}
				if (text == "KERBEROS5")
				{
					sncLibrary = ((IntPtr.Size == 8) ? "GX64KRB5.DLL" : "GSSKRB5.DLL");
					return true;
				}
			}
			sncLibrary = null;
			return false;
		}

		// Token: 0x04000107 RID: 263
		public const string DebugDirectory = "DEBUGDIRECTORY";

		// Token: 0x04000108 RID: 264
		public const string Language = "LANG";

		// Token: 0x04000109 RID: 265
		public const string Client = "CLIENT";

		// Token: 0x0400010A RID: 266
		public const string SystemNumber = "SYSNR";

		// Token: 0x0400010B RID: 267
		public const string AppServerHost = "ASHOST";

		// Token: 0x0400010C RID: 268
		public const string MessageServerHost = "MessageServer";

		// Token: 0x0400010D RID: 269
		public const string SystemId = "SystemID";

		// Token: 0x0400010E RID: 270
		public const string LogonGroup = "LogonGroup";

		// Token: 0x0400010F RID: 271
		public const string User = "USER";

		// Token: 0x04000110 RID: 272
		public const string Password = "PASSWD";

		// Token: 0x04000111 RID: 273
		public const string SncPartnerName = "SNCPartnerName";

		// Token: 0x04000112 RID: 274
		public const string SncLibrary = "SNCLibrary";

		// Token: 0x04000113 RID: 275
		public const string TraceEnabled = "TraceEnabled";

		// Token: 0x04000114 RID: 276
		public const string VerboseEnabled = "VerboseEnabled";

		// Token: 0x04000115 RID: 277
		public const string BatchSize = "BatchSize";

		// Token: 0x04000116 RID: 278
		public const string ExecutionMode = "ExecutionMode";

		// Token: 0x04000117 RID: 279
		public const string SsoType = "SSOType";

		// Token: 0x04000118 RID: 280
		public const string SsoTypeNtlm = "NTLM";

		// Token: 0x04000119 RID: 281
		public const string SsoTypeKerberos = "KERBEROS5";
	}
}
