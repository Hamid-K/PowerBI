using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200000E RID: 14
	internal static class LocalClientConstants
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000237D File Offset: 0x0000057D
		public static string ClientNotLocalHeaderName
		{
			get
			{
				return LocalClientConstants.m_clientNotLocalHeaderName;
			}
		}

		// Token: 0x04000038 RID: 56
		private static readonly string m_clientNotLocalHeaderName = "RSClientNotLocalHeader";
	}
}
