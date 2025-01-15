using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.StrictResources.Nls
{
	// Token: 0x02000612 RID: 1554
	internal class SR
	{
		// Token: 0x0600347E RID: 13438 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x0600347F RID: 13439 RVA: 0x000AEE6E File Offset: 0x000AD06E
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.StrictResources.Nls.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x000AEE9A File Offset: 0x000AD09A
		// (set) Token: 0x06003481 RID: 13441 RVA: 0x000AEEA1 File Offset: 0x000AD0A1
		internal static CultureInfo Culture
		{
			get
			{
				return SR.resourceCulture;
			}
			set
			{
				SR.resourceCulture = value;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000AEEA9 File Offset: 0x000AD0A9
		internal static string BufferTooSmallThanRequiredSize
		{
			get
			{
				return SR.ResourceManager.GetString("BufferTooSmallThanRequiredSize", SR.Culture);
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06003483 RID: 13443 RVA: 0x000AEEBF File Offset: 0x000AD0BF
		internal static string InvalidConfigurationFile
		{
			get
			{
				return SR.ResourceManager.GetString("InvalidConfigurationFile", SR.Culture);
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x000AEED5 File Offset: 0x000AD0D5
		internal static string InvalidHoldBackBuffer
		{
			get
			{
				return SR.ResourceManager.GetString("InvalidHoldBackBuffer", SR.Culture);
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x000AEEEB File Offset: 0x000AD0EB
		internal static string InvalidSISOPair
		{
			get
			{
				return SR.ResourceManager.GetString("InvalidSISOPair", SR.Culture);
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x000AEF01 File Offset: 0x000AD101
		internal static string InvalidSourceCount
		{
			get
			{
				return SR.ResourceManager.GetString("InvalidSourceCount", SR.Culture);
			}
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000AEF17 File Offset: 0x000AD117
		internal static string BufferTooSmall(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("BufferTooSmall", SR.Culture), param0);
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000AEF38 File Offset: 0x000AD138
		internal static string CodePageTableNotFound(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("CodePageTableNotFound", SR.Culture), param0);
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000AEF59 File Offset: 0x000AD159
		internal static string DuplicateCodePageDefination(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DuplicateCodePageDefination", SR.Culture), param0);
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000AEF7A File Offset: 0x000AD17A
		internal static string EBCDICToUnicodeError(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("EBCDICToUnicodeError", SR.Culture), param0);
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000AEF9B File Offset: 0x000AD19B
		internal static string ErrorOpeningTranslationTable(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("ErrorOpeningTranslationTable", SR.Culture), param0);
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000AEFBC File Offset: 0x000AD1BC
		internal static string InvalidCodePage(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidCodePage", SR.Culture), param0);
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000AEFDD File Offset: 0x000AD1DD
		internal static string InvalidCodePageForCustomEncoding(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidCodePageForCustomEncoding", SR.Culture), param0);
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000AEFFE File Offset: 0x000AD1FE
		internal static string InvalidEBCDICMappings(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidEBCDICMappings", SR.Culture), param0, param1);
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000AF020 File Offset: 0x000AD220
		internal static string InvalidInputBufferLength(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidInputBufferLength", SR.Culture), param0);
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000AF041 File Offset: 0x000AD241
		internal static string InvalidOutputBufferLength(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidOutputBufferLength", SR.Culture), param0);
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000AF062 File Offset: 0x000AD262
		internal static string InvalidParameter(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidParameter", SR.Culture), param0);
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000AF083 File Offset: 0x000AD283
		internal static string OutOfMemory(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("OutOfMemory", SR.Culture), param0);
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000AF0A4 File Offset: 0x000AD2A4
		internal static string OutOfMemoryAllocatingTable(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("OutOfMemoryAllocatingTable", SR.Culture), param0);
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000AF0C5 File Offset: 0x000AD2C5
		internal static string UnicodeToEBCDICError(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UnicodeToEBCDICError", SR.Culture), param0);
		}

		// Token: 0x04001D9B RID: 7579
		private static ResourceManager resourceManager;

		// Token: 0x04001D9C RID: 7580
		private static CultureInfo resourceCulture;
	}
}
