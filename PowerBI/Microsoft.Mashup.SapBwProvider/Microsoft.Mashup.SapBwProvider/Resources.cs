using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000004 RID: 4
	internal class Resources
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020AB File Offset: 0x000002AB
		public static ResourceManager ResourceManager
		{
			get
			{
				return Resources.ResourceLoader.Resources;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B2 File Offset: 0x000002B2
		public static Message2 BadIndex(object p0, object p1)
		{
			return Resources.ResourceLoader.GetMessage("BadIndex", p0, p1);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020C0 File Offset: 0x000002C0
		public static Message0 CannotFindDestination
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("CannotFindDestination");
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020CC File Offset: 0x000002CC
		public static Message1 DuplicateParameterName(object p0)
		{
			return Resources.ResourceLoader.GetMessage("DuplicateParameterName", p0);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020D9 File Offset: 0x000002D9
		public static Message0 ExceededBufferCapacity
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("ExceededBufferCapacity");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020E5 File Offset: 0x000002E5
		public static Message0 FailedToBuildSchemaTable
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("FailedToBuildSchemaTable");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020F1 File Offset: 0x000002F1
		public static Message0 FailedToResumeValue
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("FailedToResumeValue");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020FD File Offset: 0x000002FD
		public static Message0 FieldsParameterMissing
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("FieldsParameterMissing");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002109 File Offset: 0x00000309
		public static Message0 FoundExtraRows
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("FoundExtraRows");
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002115 File Offset: 0x00000315
		public static Message1 IndexOutOfRange(object p0)
		{
			return Resources.ResourceLoader.GetMessage("IndexOutOfRange", p0);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002122 File Offset: 0x00000322
		public static Message0 InvalidBxmlEncoding
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("InvalidBxmlEncoding");
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000212E File Offset: 0x0000032E
		public static Message0 InvalidBxmlHeader
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("InvalidBxmlHeader");
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000213A File Offset: 0x0000033A
		public static Message1 InvalidBxmlTokenKind(object p0)
		{
			return Resources.ResourceLoader.GetMessage("InvalidBxmlTokenKind", p0);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002147 File Offset: 0x00000347
		public static Message1 InvalidBxmlTokenKindResume(object p0)
		{
			return Resources.ResourceLoader.GetMessage("InvalidBxmlTokenKindResume", p0);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002154 File Offset: 0x00000354
		public static Message0 InvalidBxmlVersion
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("InvalidBxmlVersion");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002160 File Offset: 0x00000360
		public static Message0 UnknownParameterInTableCall
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("UnknownParameterInTableCall");
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000216C File Offset: 0x0000036C
		public static Message0 InvalidCommandType
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("InvalidCommandType");
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002178 File Offset: 0x00000378
		public static Message2 InvalidDataLength(object p0, object p1)
		{
			return Resources.ResourceLoader.GetMessage("InvalidDataLength", p0, p1);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002186 File Offset: 0x00000386
		public static Message2 InvalidEscapeSequence(object p0, object p1)
		{
			return Resources.ResourceLoader.GetMessage("InvalidEscapeSequence", p0, p1);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002194 File Offset: 0x00000394
		public static Message0 InvalidLength
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("InvalidLength");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021A0 File Offset: 0x000003A0
		public static Message0 InvalidParameterType
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("InvalidParameterType");
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021AC File Offset: 0x000003AC
		public static Message1 InvalidTypeMapping(object p0)
		{
			return Resources.ResourceLoader.GetMessage("InvalidTypeMapping", p0);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000021B9 File Offset: 0x000003B9
		public static Message0 MissingAuthenticationParameters
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("MissingAuthenticationParameters");
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021C5 File Offset: 0x000003C5
		public static Message0 MissingBapiParameter
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("MissingBapiParameter");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000021D1 File Offset: 0x000003D1
		public static Message0 MissingConnectionString
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("MissingConnectionString");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021DD File Offset: 0x000003DD
		public static Message0 MissingParametersOnBapiCall
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("MissingParametersOnBapiCall");
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000021E9 File Offset: 0x000003E9
		public static Message1 MissingRequiredKeyword(object p0)
		{
			return Resources.ResourceLoader.GetMessage("MissingRequiredKeyword", p0);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021F6 File Offset: 0x000003F6
		public static Message0 MultipleReturnTablesFound
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("MultipleReturnTablesFound");
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002202 File Offset: 0x00000402
		public static Message0 NullFieldName
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("NullFieldName");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000220E File Offset: 0x0000040E
		public static Message0 NullParameter
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("NullParameter");
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000221A File Offset: 0x0000041A
		public static Message0 NullParameterName
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("NullParameterName");
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002226 File Offset: 0x00000426
		public static Message2 ParameterNameMismatch(object p0, object p1)
		{
			return Resources.ResourceLoader.GetMessage("ParameterNameMismatch", p0, p1);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002234 File Offset: 0x00000434
		public static Message1 ParameterNotFound(object p0)
		{
			return Resources.ResourceLoader.GetMessage("ParameterNotFound", p0);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002241 File Offset: 0x00000441
		public static Message1 ParameterValueMustBeString(object p0)
		{
			return Resources.ResourceLoader.GetMessage("ParameterValueMustBeString", p0);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000224E File Offset: 0x0000044E
		public static Message0 ReadNotCalled
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("ReadNotCalled");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000225A File Offset: 0x0000045A
		public static Message0 StreamClosed
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("StreamClosed");
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002266 File Offset: 0x00000466
		public static Message3 TableIndexOutOfRange(object p0, object p1, object p2)
		{
			return Resources.ResourceLoader.GetMessage("TableIndexOutOfRange", p0, p1, p2);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002275 File Offset: 0x00000475
		public static Message1 TableNotFound(object p0)
		{
			return Resources.ResourceLoader.GetMessage("TableNotFound", p0);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002282 File Offset: 0x00000482
		public static Message0 TableNotSpecified
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("TableNotSpecified");
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000228E File Offset: 0x0000048E
		public static Message0 TableParameterMissing
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("TableParameterMissing");
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000229A File Offset: 0x0000049A
		public static Message1 UnexpectedConnectionState(object p0)
		{
			return Resources.ResourceLoader.GetMessage("UnexpectedConnectionState", p0);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000022A7 File Offset: 0x000004A7
		public static Message0 UnexpectedEOF
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("UnexpectedEOF");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000022B3 File Offset: 0x000004B3
		public static Message0 UnexpectedStringLength
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("UnexpectedStringLength");
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000022BF File Offset: 0x000004BF
		public static Message1 UnsupportedParameterType(object p0)
		{
			return Resources.ResourceLoader.GetMessage("UnsupportedParameterType", p0);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000022CC File Offset: 0x000004CC
		public static Message1 FailedToParseDate(object p0)
		{
			return Resources.ResourceLoader.GetMessage("FailedToParseDate", p0);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000022D9 File Offset: 0x000004D9
		public static Message1 FailedToParseTime(object p0)
		{
			return Resources.ResourceLoader.GetMessage("FailedToParseTime", p0);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000022E6 File Offset: 0x000004E6
		public static Message2 IncorrectParameterValueType(object p0, object p1)
		{
			return Resources.ResourceLoader.GetMessage("IncorrectParameterValueType", p0, p1);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000022F4 File Offset: 0x000004F4
		public static Message1 MissingParameterValue(object p0)
		{
			return Resources.ResourceLoader.GetMessage("MissingParameterValue", p0);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002301 File Offset: 0x00000501
		public static Message2 InvalidCharacterWhileParsingValue(object p0, object p1)
		{
			return Resources.ResourceLoader.GetMessage("InvalidCharacterWhileParsingValue", p0, p1);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000230F File Offset: 0x0000050F
		public static Message0 NullValueMessage
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("NullValueMessage");
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000231B File Offset: 0x0000051B
		public static Message1 FailedToFindFunctionMetadata(object p0)
		{
			return Resources.ResourceLoader.GetMessage("FailedToFindFunctionMetadata", p0);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002328 File Offset: 0x00000528
		public static Message1 ExtraAxesFound(object p0)
		{
			return Resources.ResourceLoader.GetMessage("ExtraAxesFound", p0);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002335 File Offset: 0x00000535
		public static Message3 IncorrectMeasureCount(object p0, object p1, object p2)
		{
			return Resources.ResourceLoader.GetMessage("IncorrectMeasureCount", p0, p1, p2);
		}

		// Token: 0x0200003A RID: 58
		private class ResourceLoader
		{
			// Token: 0x060002E6 RID: 742 RVA: 0x0000C0EE File Offset: 0x0000A2EE
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.SapBwProvider.Resources", base.GetType().Assembly);
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x0000C114 File Offset: 0x0000A314
			private static Resources.ResourceLoader GetLoader()
			{
				if (Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.instance == null)
				{
					Resources.ResourceLoader resourceLoader = new Resources.ResourceLoader();
					Interlocked.CompareExchange<Resources.ResourceLoader>(ref Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.instance, resourceLoader, null);
				}
				return Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.instance;
			}

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000C140 File Offset: 0x0000A340
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000C143 File Offset: 0x0000A343
			public static ResourceManager Resources
			{
				get
				{
					return Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x060002EA RID: 746 RVA: 0x0000C150 File Offset: 0x0000A350
			public static Message0 GetMessage(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message0(null);
				}
				return new Message0(loader.resources.GetString(name, Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.Culture));
			}

			// Token: 0x060002EB RID: 747 RVA: 0x0000C184 File Offset: 0x0000A384
			public static Message1 GetMessage(string name, object arg0)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message1(null, null);
				}
				return new Message1(loader.resources.GetString(name, Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.Culture), arg0);
			}

			// Token: 0x060002EC RID: 748 RVA: 0x0000C1BC File Offset: 0x0000A3BC
			public static Message2 GetMessage(string name, object arg0, object arg1)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message2(null, null, null);
				}
				return new Message2(loader.resources.GetString(name, Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.Culture), arg0, arg1);
			}

			// Token: 0x060002ED RID: 749 RVA: 0x0000C1F4 File Offset: 0x0000A3F4
			public static Message3 GetMessage(string name, object arg0, object arg1, object arg2)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message3(null, null, null, null);
				}
				return new Message3(loader.resources.GetString(name, Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.Culture), arg0, arg1, arg2);
			}

			// Token: 0x060002EE RID: 750 RVA: 0x0000C230 File Offset: 0x0000A430
			public static Message4 GetMessage(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message4(null, null);
				}
				return new Message4(loader.resources.GetString(name, Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.Culture), args);
			}

			// Token: 0x060002EF RID: 751 RVA: 0x0000C268 File Offset: 0x0000A468
			public static string GetString(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				string @string = loader.resources.GetString(name, Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.Culture);
				if (args != null && args.Length != 0)
				{
					return string.Format(CultureInfo.CurrentCulture, @string, args);
				}
				return @string;
			}

			// Token: 0x060002F0 RID: 752 RVA: 0x0000C2A8 File Offset: 0x0000A4A8
			public static string GetString(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.Culture);
			}

			// Token: 0x060002F1 RID: 753 RVA: 0x0000C2D4 File Offset: 0x0000A4D4
			public static object GetObject(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.Culture);
			}

			// Token: 0x060002F2 RID: 754 RVA: 0x0000C300 File Offset: 0x0000A500
			public static T GetObject<T>(string name) where T : class
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Microsoft.Mashup.SapBwProvider.Resources.ResourceLoader.Culture));
			}

			// Token: 0x040001EA RID: 490
			private static Resources.ResourceLoader instance;

			// Token: 0x040001EB RID: 491
			private ResourceManager resources;
		}
	}
}
