using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.SemanticQueryEngine
{
	// Token: 0x02000014 RID: 20
	[CompilerGenerated]
	internal class SR
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00005ACC File Offset: 0x00003CCC
		protected SR()
		{
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005AD4 File Offset: 0x00003CD4
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00005ADB File Offset: 0x00003CDB
		public static CultureInfo Culture
		{
			get
			{
				return SR.Keys.Culture;
			}
			set
			{
				SR.Keys.Culture = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005AE3 File Offset: 0x00003CE3
		public static string SemanticQueryEngineLocalizedName
		{
			get
			{
				return SR.Keys.GetString("SemanticQueryEngineLocalizedName");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005AEF File Offset: 0x00003CEF
		public static string SqlModelGeneratorLocalizedName
		{
			get
			{
				return SR.Keys.GetString("SqlModelGeneratorLocalizedName");
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005AFB File Offset: 0x00003CFB
		public static string InvalidCommandType
		{
			get
			{
				return SR.Keys.GetString("InvalidCommandType");
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005B07 File Offset: 0x00003D07
		public static string ConnectionNotOpen
		{
			get
			{
				return SR.Keys.GetString("ConnectionNotOpen");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005B13 File Offset: 0x00003D13
		public static string SemanticQueryEngineNotInitialized
		{
			get
			{
				return SR.Keys.GetString("SemanticQueryEngineNotInitialized");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005B1F File Offset: 0x00003D1F
		public static string QueryNotSet
		{
			get
			{
				return SR.Keys.GetString("QueryNotSet");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005B2B File Offset: 0x00003D2B
		public static string AggregateOfBinaryExpression
		{
			get
			{
				return SR.Keys.GetString("AggregateOfBinaryExpression");
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005B37 File Offset: 0x00003D37
		public static string InvalidConfiguration(string extensionName, string details)
		{
			return SR.Keys.GetString("InvalidConfiguration", extensionName, details);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005B45 File Offset: 0x00003D45
		public static string DuplicateParameterName(string name)
		{
			return SR.Keys.GetString("DuplicateParameterName", name);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005B52 File Offset: 0x00003D52
		public static string SemanticQueryCompilationError(string message)
		{
			return SR.Keys.GetString("SemanticQueryCompilationError", message);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005B5F File Offset: 0x00003D5F
		public static string ErrorExecutingSemanticQuery(string message)
		{
			return SR.Keys.GetString("ErrorExecutingSemanticQuery", message);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005B6C File Offset: 0x00003D6C
		public static string GroupingByBlob(string expressionName)
		{
			return SR.Keys.GetString("GroupingByBlob", expressionName);
		}

		// Token: 0x020000B2 RID: 178
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x0600069F RID: 1695 RVA: 0x00005ACC File Offset: 0x00003CCC
			private Keys()
			{
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001AADB File Offset: 0x00018CDB
			// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0001AAE2 File Offset: 0x00018CE2
			public static CultureInfo Culture
			{
				get
				{
					return SR.Keys._culture;
				}
				set
				{
					SR.Keys._culture = value;
				}
			}

			// Token: 0x060006A2 RID: 1698 RVA: 0x0001AAEA File Offset: 0x00018CEA
			public static string GetString(string key)
			{
				return SR.Keys.resourceManager.GetString(key, SR.Keys._culture);
			}

			// Token: 0x060006A3 RID: 1699 RVA: 0x0001AAFC File Offset: 0x00018CFC
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, SR.Keys.resourceManager.GetString(key, SR.Keys._culture), arg0);
			}

			// Token: 0x060006A4 RID: 1700 RVA: 0x0001AB19 File Offset: 0x00018D19
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, SR.Keys.resourceManager.GetString(key, SR.Keys._culture), arg0, arg1);
			}

			// Token: 0x0400032F RID: 815
			private static ResourceManager resourceManager = new ResourceManager(typeof(SR).FullName, typeof(SR).Module.Assembly);

			// Token: 0x04000330 RID: 816
			private static CultureInfo _culture = null;

			// Token: 0x04000331 RID: 817
			public const string SemanticQueryEngineLocalizedName = "SemanticQueryEngineLocalizedName";

			// Token: 0x04000332 RID: 818
			public const string SqlModelGeneratorLocalizedName = "SqlModelGeneratorLocalizedName";

			// Token: 0x04000333 RID: 819
			public const string InvalidCommandType = "InvalidCommandType";

			// Token: 0x04000334 RID: 820
			public const string InvalidConfiguration = "InvalidConfiguration";

			// Token: 0x04000335 RID: 821
			public const string DuplicateParameterName = "DuplicateParameterName";

			// Token: 0x04000336 RID: 822
			public const string SemanticQueryCompilationError = "SemanticQueryCompilationError";

			// Token: 0x04000337 RID: 823
			public const string ConnectionNotOpen = "ConnectionNotOpen";

			// Token: 0x04000338 RID: 824
			public const string ErrorExecutingSemanticQuery = "ErrorExecutingSemanticQuery";

			// Token: 0x04000339 RID: 825
			public const string SemanticQueryEngineNotInitialized = "SemanticQueryEngineNotInitialized";

			// Token: 0x0400033A RID: 826
			public const string QueryNotSet = "QueryNotSet";

			// Token: 0x0400033B RID: 827
			public const string AggregateOfBinaryExpression = "AggregateOfBinaryExpression";

			// Token: 0x0400033C RID: 828
			public const string GroupingByBlob = "GroupingByBlob";
		}
	}
}
