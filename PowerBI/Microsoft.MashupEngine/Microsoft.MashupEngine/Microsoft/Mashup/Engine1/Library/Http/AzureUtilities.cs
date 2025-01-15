using System;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A46 RID: 2630
	internal static class AzureUtilities
	{
		// Token: 0x06004932 RID: 18738 RVA: 0x000F50C0 File Offset: 0x000F32C0
		public static void ValidateTableParameters(TextValue account)
		{
			bool flag;
			AzureUtilities.ValidateParameters(account, out flag);
			if (flag)
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.AzureTableInvalidAccountUrl, account, null);
			}
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x000F50E8 File Offset: 0x000F32E8
		public static void ValidateParameters(TextValue account, TextValue containerName = null)
		{
			bool flag;
			AzureUtilities.ValidateParameters(account, containerName, AzureUtilities.Validation.ValidateAccountName, out flag);
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x000F50FF File Offset: 0x000F32FF
		public static void ValidateParameters(TextValue account, out bool hasContainer)
		{
			AzureUtilities.ValidateParameters(account, null, AzureUtilities.Validation.ValidateAccountName, out hasContainer);
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x000F510C File Offset: 0x000F330C
		public static void ValidateParameters(TextValue account, TextValue containerName, AzureUtilities.Validation validation, out bool hasContainer)
		{
			hasContainer = false;
			if (!account.String.Contains("."))
			{
				AzureUtilities.ValidateAccountName(account.String);
			}
			else
			{
				Uri uri;
				if (!Uri.TryCreate(account.String.TrimEnd(new char[] { '/' }), UriKind.Absolute, out uri))
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.AzureInvalidAccountURL, account, null);
				}
				int num = uri.Segments.Length;
				bool flag = (validation & AzureUtilities.Validation.AllowDirectory) == AzureUtilities.Validation.AllowDirectory;
				if (uri.Scheme != Uri.UriSchemeHttps)
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.AzureInvalidAccountScheme, account, null);
				}
				if (!string.IsNullOrEmpty(uri.Query) || (!flag && num > 2))
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.AzureInvalidAccountQueryParameters, account, null);
				}
				int num2 = uri.Host.IndexOf('.');
				if (num2 == -1)
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.AzureInvalidAccountURL, account, null);
				}
				if ((validation & AzureUtilities.Validation.ValidateAccountName) == AzureUtilities.Validation.ValidateAccountName)
				{
					AzureUtilities.ValidateAccountName(uri.Host.Substring(0, num2));
				}
				if (num >= 2)
				{
					hasContainer = true;
					containerName = TextValue.New(uri.Segments[1].TrimEnd(new char[] { '/' }));
				}
			}
			if (containerName != null && (containerName.Length < 3 || containerName.Length > 63 || containerName.String.Contains("--") || !AzureUtilities.IsAlphaNumeric(containerName.String, true) || !AzureUtilities.IsAlphaNumeric(containerName.String[containerName.String.Length - 1]) || !AzureUtilities.IsAlphaNumeric(containerName.String[0])))
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.AzureInvalidContainerName, containerName, null);
			}
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x000F5290 File Offset: 0x000F3490
		private static bool IsAlphaNumeric(string value, bool allowDashes = false)
		{
			if (allowDashes)
			{
				return value.All((char c) => AzureUtilities.IsAlphaNumeric(c) || c == '-');
			}
			return value.All((char c) => AzureUtilities.IsAlphaNumeric(c));
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x000F52EB File Offset: 0x000F34EB
		private static bool IsAlphaNumeric(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9');
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x000F5314 File Offset: 0x000F3514
		private static void ValidateAccountName(string accountName)
		{
			if (accountName.EndsWith("-secondary", StringComparison.OrdinalIgnoreCase))
			{
				accountName = accountName.Substring(0, accountName.Length - "-secondary".Length);
			}
			if (accountName.Length < 3 || accountName.Length > 24 || !AzureUtilities.IsAlphaNumeric(accountName, false))
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.AzureInvalidAccountName, TextValue.New(accountName), null);
			}
		}

		// Token: 0x04002737 RID: 10039
		private const string secondarySuffix = "-secondary";

		// Token: 0x02000A47 RID: 2631
		[Flags]
		public enum Validation
		{
			// Token: 0x04002739 RID: 10041
			AllowDirectory = 1,
			// Token: 0x0400273A RID: 10042
			ValidateAccountName = 2,
			// Token: 0x0400273B RID: 10043
			Default = 2,
			// Token: 0x0400273C RID: 10044
			AdlsGen2 = 1
		}
	}
}
