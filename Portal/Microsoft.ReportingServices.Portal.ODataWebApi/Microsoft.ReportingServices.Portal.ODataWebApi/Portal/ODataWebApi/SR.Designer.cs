using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.Portal.ODataWebApi
{
	// Token: 0x02000009 RID: 9
	[CompilerGenerated]
	internal class SR
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000025FC File Offset: 0x000007FC
		protected SR()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002604 File Offset: 0x00000804
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000260B File Offset: 0x0000080B
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

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002613 File Offset: 0x00000813
		public static string Error_SearchTextNullOrEmpty
		{
			get
			{
				return SR.Keys.GetString("Error_SearchTextNullOrEmpty");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000261F File Offset: 0x0000081F
		public static string SystemResourcePackageException
		{
			get
			{
				return SR.Keys.GetString("SystemResourcePackageException");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000262B File Offset: 0x0000082B
		public static string NotExpectedTypeException
		{
			get
			{
				return SR.Keys.GetString("NotExpectedTypeException");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002637 File Offset: 0x00000837
		public static string PropertyNullException
		{
			get
			{
				return SR.Keys.GetString("PropertyNullException");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002643 File Offset: 0x00000843
		public static string ParameterValueNotSupplied
		{
			get
			{
				return SR.Keys.GetString("ParameterValueNotSupplied");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000264F File Offset: 0x0000084F
		public static string DataSetProcessingException
		{
			get
			{
				return SR.Keys.GetString("DataSetProcessingException");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000265B File Offset: 0x0000085B
		public static string DataSetProcessingSoapException
		{
			get
			{
				return SR.Keys.GetString("DataSetProcessingSoapException");
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002667 File Offset: 0x00000867
		public static string SystemResourceProcessingException
		{
			get
			{
				return SR.Keys.GetString("SystemResourceProcessingException");
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002673 File Offset: 0x00000873
		public static string NotSupportedException
		{
			get
			{
				return SR.Keys.GetString("NotSupportedException");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000267F File Offset: 0x0000087F
		public static string InvalidAggregation
		{
			get
			{
				return SR.Keys.GetString("InvalidAggregation");
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000268B File Offset: 0x0000088B
		public static string ErrorPbixUpload
		{
			get
			{
				return SR.Keys.GetString("ErrorPbixUpload");
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002697 File Offset: 0x00000897
		public static string InvalidDataSourceForTestConnectionPbi
		{
			get
			{
				return SR.Keys.GetString("InvalidDataSourceForTestConnectionPbi");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000026A3 File Offset: 0x000008A3
		public static string UploadFileCanceled
		{
			get
			{
				return SR.Keys.GetString("UploadFileCanceled");
			}
		}

		// Token: 0x0200004D RID: 77
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06000347 RID: 839 RVA: 0x000025FC File Offset: 0x000007FC
			private Keys()
			{
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06000348 RID: 840 RVA: 0x0000E5CA File Offset: 0x0000C7CA
			// (set) Token: 0x06000349 RID: 841 RVA: 0x0000E5D1 File Offset: 0x0000C7D1
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

			// Token: 0x0600034A RID: 842 RVA: 0x0000E5D9 File Offset: 0x0000C7D9
			public static string GetString(string key)
			{
				return SR.Keys.resourceManager.GetString(key, SR.Keys._culture);
			}

			// Token: 0x040000E0 RID: 224
			private static ResourceManager resourceManager = new ResourceManager(typeof(SR).FullName, typeof(SR).Module.Assembly);

			// Token: 0x040000E1 RID: 225
			private static CultureInfo _culture = null;

			// Token: 0x040000E2 RID: 226
			public const string Error_SearchTextNullOrEmpty = "Error_SearchTextNullOrEmpty";

			// Token: 0x040000E3 RID: 227
			public const string SystemResourcePackageException = "SystemResourcePackageException";

			// Token: 0x040000E4 RID: 228
			public const string NotExpectedTypeException = "NotExpectedTypeException";

			// Token: 0x040000E5 RID: 229
			public const string PropertyNullException = "PropertyNullException";

			// Token: 0x040000E6 RID: 230
			public const string ParameterValueNotSupplied = "ParameterValueNotSupplied";

			// Token: 0x040000E7 RID: 231
			public const string DataSetProcessingException = "DataSetProcessingException";

			// Token: 0x040000E8 RID: 232
			public const string DataSetProcessingSoapException = "DataSetProcessingSoapException";

			// Token: 0x040000E9 RID: 233
			public const string SystemResourceProcessingException = "SystemResourceProcessingException";

			// Token: 0x040000EA RID: 234
			public const string NotSupportedException = "NotSupportedException";

			// Token: 0x040000EB RID: 235
			public const string InvalidAggregation = "InvalidAggregation";

			// Token: 0x040000EC RID: 236
			public const string ErrorPbixUpload = "ErrorPbixUpload";

			// Token: 0x040000ED RID: 237
			public const string InvalidDataSourceForTestConnectionPbi = "InvalidDataSourceForTestConnectionPbi";

			// Token: 0x040000EE RID: 238
			public const string UploadFileCanceled = "UploadFileCanceled";
		}
	}
}
