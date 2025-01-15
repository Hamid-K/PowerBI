using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Win32;

namespace Microsoft.Mashup.Engine1.Library.Content
{
	// Token: 0x02000D87 RID: 3463
	internal static class ContentHelper
	{
		// Token: 0x17001BB7 RID: 7095
		// (get) Token: 0x06005E2E RID: 24110 RVA: 0x00145188 File Offset: 0x00143388
		private static IDictionary<string, string> ContentTypes
		{
			get
			{
				if (ContentHelper.contentTypes == null)
				{
					ContentHelper.contentTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
					{
						{ ".ACCDB", "application/msaccess" },
						{ ".ACCDE", "application/msaccess" },
						{ ".ACCDR", "application/msaccess" },
						{ ".MDB", "application/msaccess" },
						{ ".MDE", "application/msaccess" },
						{ ".CSV", "text/csv" },
						{ ".HTML", "text/html" },
						{ ".TXT", "text/plain" },
						{ ".ASPX", "text/html" },
						{ ".XML", "text/xml" },
						{ ".XLA", "application/vnd.ms-excel" },
						{ ".XLS", "application/vnd.ms-excel" },
						{ ".XLSB", "application/vnd.ms-excel.sheet.binary.macroEnabled.12" },
						{ ".XLSM", "application/vnd.ms-excel.sheet.macroEnabled.12" },
						{ ".XLSX", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
						{ ".XLT", "application/vnd.ms-excel" },
						{ ".JSON", "application/json" },
						{ ".PARQ", "application/x-parquet" },
						{ ".PARQUET", "application/x-parquet" }
					};
				}
				return ContentHelper.contentTypes;
			}
		}

		// Token: 0x17001BB8 RID: 7096
		// (get) Token: 0x06005E2F RID: 24111 RVA: 0x001452E3 File Offset: 0x001434E3
		private static Keys ContentTypeKeys
		{
			get
			{
				if (ContentHelper.contentTypeKeys == null)
				{
					ContentHelper.contentTypeKeys = Keys.New("Content.Type");
				}
				return ContentHelper.contentTypeKeys;
			}
		}

		// Token: 0x06005E30 RID: 24112 RVA: 0x00145300 File Offset: 0x00143500
		public static void AddContentType(string fileExtension, string contentType)
		{
			string text;
			if (ContentHelper.ContentTypes.TryGetValue(fileExtension, out text) && string.Equals(text, contentType))
			{
				return;
			}
			ContentHelper.ContentTypes.Add(fileExtension, contentType);
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x00145332 File Offset: 0x00143532
		public static void RemoveContentType(string fileExtension)
		{
			ContentHelper.ContentTypes.Remove(fileExtension);
		}

		// Token: 0x06005E32 RID: 24114 RVA: 0x00145340 File Offset: 0x00143540
		public static RecordValue CreateContentTypeMetadata(string contentType)
		{
			return RecordValue.New(ContentHelper.ContentTypeKeys, new Value[] { string.IsNullOrEmpty(contentType) ? Value.Null : TextValue.New(contentType) });
		}

		// Token: 0x06005E33 RID: 24115 RVA: 0x0014536C File Offset: 0x0014356C
		public static string GetBestContentType(string proposedContentType, string determinedContentType)
		{
			ContentHelper.ContentTypeAmbiguity contentTypeAmbiguity = ContentHelper.GetContentTypeAmbiguity(proposedContentType);
			ContentHelper.ContentTypeAmbiguity contentTypeAmbiguity2 = ContentHelper.GetContentTypeAmbiguity(determinedContentType);
			if (contentTypeAmbiguity == ContentHelper.ContentTypeAmbiguity.HtmlXmlAmbiguity && contentTypeAmbiguity2 == ContentHelper.ContentTypeAmbiguity.HtmlXmlAmbiguity && !string.Equals(proposedContentType, determinedContentType, StringComparison.OrdinalIgnoreCase))
			{
				return "application/xhtml+xml";
			}
			if (contentTypeAmbiguity >= contentTypeAmbiguity2)
			{
				return determinedContentType;
			}
			return proposedContentType;
		}

		// Token: 0x06005E34 RID: 24116 RVA: 0x001453A8 File Offset: 0x001435A8
		private static ContentHelper.ContentTypeAmbiguity GetContentTypeAmbiguity(string contentType)
		{
			if (string.IsNullOrEmpty(contentType))
			{
				return ContentHelper.ContentTypeAmbiguity.UnknownAmbiguity;
			}
			if (string.Equals(contentType, "application/octet-stream", StringComparison.OrdinalIgnoreCase))
			{
				return ContentHelper.ContentTypeAmbiguity.PlainBinaryAmbiguity;
			}
			if (string.Equals(contentType, "text/plain", StringComparison.OrdinalIgnoreCase))
			{
				return ContentHelper.ContentTypeAmbiguity.PlainTextAmbiguity;
			}
			if (string.Equals(contentType, "text/html", StringComparison.OrdinalIgnoreCase) || string.Equals(contentType, "text/xml", StringComparison.OrdinalIgnoreCase))
			{
				return ContentHelper.ContentTypeAmbiguity.HtmlXmlAmbiguity;
			}
			return ContentHelper.ContentTypeAmbiguity.Unambiguous;
		}

		// Token: 0x06005E35 RID: 24117 RVA: 0x001453FE File Offset: 0x001435FE
		public static bool IsAmbiguousContentType(string contentType)
		{
			return ContentHelper.GetContentTypeAmbiguity(contentType) > ContentHelper.ContentTypeAmbiguity.Unambiguous;
		}

		// Token: 0x06005E36 RID: 24118 RVA: 0x00145409 File Offset: 0x00143609
		public static void VerifyIsContentType(Value value)
		{
			if (!value.IsText && !value.IsBinary)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.ContentTypeParameterValidationError, value, null);
			}
		}

		// Token: 0x06005E37 RID: 24119 RVA: 0x00145428 File Offset: 0x00143628
		public static string GetContentType(byte[] preamble, int preambleLength, string proposedContentType)
		{
			ContentHelper.MimeFlags mimeFlags = ContentHelper.MimeFlags.FMFD_ENABLEMIMESNIFFING;
			string text;
			if (ContentHelper.NativeMethods.FindMimeFromData(IntPtr.Zero, null, preamble, preambleLength, proposedContentType, (int)mimeFlags, out text, 0) != 0)
			{
				return proposedContentType;
			}
			return ContentHelper.GetBestContentType(proposedContentType, text);
		}

		// Token: 0x06005E38 RID: 24120 RVA: 0x00145454 File Offset: 0x00143654
		public static string GetContentType(string path)
		{
			string text;
			try
			{
				string contentTypeForExtension = ContentHelper.GetContentTypeForExtension(Path.GetExtension(path));
				if (ContentHelper.IsAmbiguousContentType(contentTypeForExtension))
				{
					using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					{
						byte[] array = new byte[256];
						int num = fileStream.Read(array, 0, array.Length);
						return ContentHelper.GetContentType(array, num, contentTypeForExtension);
					}
				}
				text = contentTypeForExtension;
			}
			catch (Exception ex)
			{
				if (SafeExceptions.IsSafeException(ex))
				{
					throw FileErrors.HandleException(ex, TextValue.New(path));
				}
				throw;
			}
			return text;
		}

		// Token: 0x06005E39 RID: 24121 RVA: 0x001454EC File Offset: 0x001436EC
		public static string GetContentTypeForExtension(string extension)
		{
			if (string.IsNullOrEmpty(extension))
			{
				return null;
			}
			if (extension[0] != '.')
			{
				extension = "." + extension;
			}
			string text;
			if (!ContentHelper.ContentTypes.TryGetValue(extension, out text))
			{
				RegistryKey classesRoot = Registry.ClassesRoot;
				try
				{
					using (RegistryKey registryKey = classesRoot.OpenSubKey(extension))
					{
						if (registryKey != null)
						{
							text = registryKey.GetValue("Content Type") as string;
						}
					}
				}
				catch (ArgumentException)
				{
				}
			}
			return text;
		}

		// Token: 0x040033A5 RID: 13221
		public const string ContentTypeAny = "*/*";

		// Token: 0x040033A6 RID: 13222
		public const string ContentTypeApplicationAccess = "application/msaccess";

		// Token: 0x040033A7 RID: 13223
		public const string ContentTypeApplicationAtom = "application/atom+xml";

		// Token: 0x040033A8 RID: 13224
		public const string ContentTypeApplicationAtomService = "application/atomsvc+xml";

		// Token: 0x040033A9 RID: 13225
		public const string ContentTypeBatch = "multipart/mixed";

		// Token: 0x040033AA RID: 13226
		public const string ContentTypeApplicationExcel = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

		// Token: 0x040033AB RID: 13227
		public const string ContentTypeApplicationExcelBinaryMacroEnabled = "application/vnd.ms-excel.sheet.binary.macroEnabled.12";

		// Token: 0x040033AC RID: 13228
		public const string ContentTypeApplicationExcelMacroEnabled = "application/vnd.ms-excel.sheet.macroEnabled.12";

		// Token: 0x040033AD RID: 13229
		public const string ContentTypeApplicationExcel12 = "application/vnd.ms-excel.12";

		// Token: 0x040033AE RID: 13230
		public const string ContentTypeApplicationExcel2003 = "application/vnd.ms-excel";

		// Token: 0x040033AF RID: 13231
		public const string ContentTypeApplicationImport = "application/x-ms-import";

		// Token: 0x040033B0 RID: 13232
		public const string ContentTypeApplicationJavascript = "application/javascript";

		// Token: 0x040033B1 RID: 13233
		public const string ContentTypeApplicationJson = "application/json";

		// Token: 0x040033B2 RID: 13234
		public const string ContentTypeApplicationJsonLightV3 = "application/json;odata=minimalmetadata";

		// Token: 0x040033B3 RID: 13235
		public const string ContentTypeApplicationJsonLightV4 = "application/json;odata.metadata=minimal";

		// Token: 0x040033B4 RID: 13236
		public const string ContentTypeApplicationJsonVerbose = "application/json;odata=verbose";

		// Token: 0x040033B5 RID: 13237
		public const string ContentTypeApplicationOctetStream = "application/octet-stream";

		// Token: 0x040033B6 RID: 13238
		public const string ContentTypeApplicationXHtmlPlusXml = "application/xhtml+xml";

		// Token: 0x040033B7 RID: 13239
		public const string ContentTypeApplicationXml = "application/xml";

		// Token: 0x040033B8 RID: 13240
		public const string ContentTypeApplicationXParquet = "application/x-parquet";

		// Token: 0x040033B9 RID: 13241
		public const string ContentTypeMultipartMixed = "multipart/mixed";

		// Token: 0x040033BA RID: 13242
		public const string ContentTypeTextCsv = "text/csv";

		// Token: 0x040033BB RID: 13243
		public const string ContentTypeTextHtml = "text/html";

		// Token: 0x040033BC RID: 13244
		public const string ContentTypeTextJavascript = "text/javascript";

		// Token: 0x040033BD RID: 13245
		public const string ContentTypeTextJson = "text/x-json";

		// Token: 0x040033BE RID: 13246
		public const string ContentTypeTextM = "text/x-ms-m";

		// Token: 0x040033BF RID: 13247
		public const string ContentTypeTextPlain = "text/plain";

		// Token: 0x040033C0 RID: 13248
		public const string ContentTypeTextXml = "text/xml";

		// Token: 0x040033C1 RID: 13249
		public const string ContentTypePrefixApplication = "application/";

		// Token: 0x040033C2 RID: 13250
		public const string ContentTypePrefixText = "text/";

		// Token: 0x040033C3 RID: 13251
		private const string ContentTypeValueKey = "Content Type";

		// Token: 0x040033C4 RID: 13252
		public const int DefaultPreambleSize = 4096;

		// Token: 0x040033C5 RID: 13253
		private static Dictionary<string, string> contentTypes;

		// Token: 0x040033C6 RID: 13254
		private static Keys contentTypeKeys;

		// Token: 0x02000D88 RID: 3464
		private enum ContentTypeAmbiguity
		{
			// Token: 0x040033C8 RID: 13256
			Unambiguous,
			// Token: 0x040033C9 RID: 13257
			HtmlXmlAmbiguity,
			// Token: 0x040033CA RID: 13258
			PlainTextAmbiguity,
			// Token: 0x040033CB RID: 13259
			PlainBinaryAmbiguity,
			// Token: 0x040033CC RID: 13260
			UnknownAmbiguity
		}

		// Token: 0x02000D89 RID: 3465
		[Flags]
		private enum MimeFlags
		{
			// Token: 0x040033CE RID: 13262
			FMFD_DEFAULT = 0,
			// Token: 0x040033CF RID: 13263
			FMFD_URLASFILENAME = 1,
			// Token: 0x040033D0 RID: 13264
			FMFD_ENABLEMIMESNIFFING = 2,
			// Token: 0x040033D1 RID: 13265
			FMFD_IGNOREMIMETEXTPLAIN = 4,
			// Token: 0x040033D2 RID: 13266
			FMFD_SERVERMIME = 8,
			// Token: 0x040033D3 RID: 13267
			FMFD_RESPECTTEXTPLAIN = 16,
			// Token: 0x040033D4 RID: 13268
			FMFD_RETURNUPDATEDIMGMIMES = 32
		}

		// Token: 0x02000D8A RID: 3466
		private static class NativeMethods
		{
			// Token: 0x06005E3A RID: 24122
			[DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			public static extern int FindMimeFromData(IntPtr pBC, [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer, int cbSize, [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed, int dwMimeFlags, out string ppwzMimeOut, int dwReserved);
		}
	}
}
