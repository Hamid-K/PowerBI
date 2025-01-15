using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Privacy
{
	// Token: 0x020000D6 RID: 214
	[NullableContext(1)]
	[Nullable(0)]
	public static class PrivateInformationPipelineExtensions
	{
		// Token: 0x060010B4 RID: 4276 RVA: 0x00046188 File Offset: 0x00044388
		public static string MarkAsException(this string input)
		{
			return input.MarkAsCustomerContent();
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00046190 File Offset: 0x00044390
		public static string MarkAsQuery(this string input)
		{
			return input.MarkAsCustomerContent();
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00046198 File Offset: 0x00044398
		public static string MarkAsUserObjectId(this string input)
		{
			return input.MarkAsEUPI();
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000461A0 File Offset: 0x000443A0
		public static string MarkAsPUID(this string input)
		{
			return input.MarkAsEUPI();
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000461A8 File Offset: 0x000443A8
		public static string MarkAsTenantObjectId(this string input)
		{
			return input;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000461AB File Offset: 0x000443AB
		public static string MarkAsUsername(this string input)
		{
			return input.MarkAsEUII();
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x000461B3 File Offset: 0x000443B3
		public static string MarkAsDomainName(this string input)
		{
			return input;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x000461B6 File Offset: 0x000443B6
		public static string MarkAsPath(this string input)
		{
			return input.MarkAsCustomerContent();
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x000461BE File Offset: 0x000443BE
		public static string MarkAsConnectionString(this string input)
		{
			return input.MarkAsCustomerContent();
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000461C6 File Offset: 0x000443C6
		public static string MarkAsDataSourceDetails(this DataSourceGatewayDetails input)
		{
			if (input == null)
			{
				return string.Empty;
			}
			return input.ToString().MarkAsCustomerContent();
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000461DC File Offset: 0x000443DC
		public static string MarkAsEffectiveUserName(this string input)
		{
			Guid guid;
			if (input != null && Guid.TryParse(input, out guid))
			{
				return input.MarkAsEUPI();
			}
			return input.MarkAsEUII();
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00046203 File Offset: 0x00044403
		public static string MarkAsCustomerCertificate(this string input)
		{
			return input.MarkAsCustomerContent();
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0004620C File Offset: 0x0004440C
		public static string MarkAsCustomerGatewayServiceBus(this string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return string.Empty;
			}
			Uri uri;
			if (Uri.TryCreate(input, UriKind.Absolute, out uri) && uri.Host.IsDefaultRelay())
			{
				return input;
			}
			if (input.IsDefaultRelay())
			{
				return input;
			}
			return input.MarkAsCustomerContent();
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00046254 File Offset: 0x00044454
		private static bool IsDefaultRelay(this string input)
		{
			return (input.StartsWith("wabi", StringComparison.OrdinalIgnoreCase) || input.StartsWith("crelay", StringComparison.OrdinalIgnoreCase)) && (input.EndsWith("usgovcloudapi.net", StringComparison.OrdinalIgnoreCase) || input.EndsWith("cloudapi.de", StringComparison.OrdinalIgnoreCase) || input.EndsWith("chinacloudapi.cn", StringComparison.OrdinalIgnoreCase) || input.EndsWith("cloudapi.eaglex.ic.gov", StringComparison.OrdinalIgnoreCase) || input.EndsWith(".windows.net", StringComparison.OrdinalIgnoreCase));
		}
	}
}
