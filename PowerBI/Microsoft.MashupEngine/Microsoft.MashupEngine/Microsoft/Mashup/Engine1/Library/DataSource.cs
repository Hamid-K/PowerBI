using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Content;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.SharePoint;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library
{
	// Token: 0x02000242 RID: 578
	internal static class DataSource
	{
		// Token: 0x06001907 RID: 6407 RVA: 0x0003125C File Offset: 0x0002F45C
		public static bool IsSerializedTextType(TypeValue type)
		{
			Value value;
			return type.TypeKind == ValueKind.Text && type.TryGetMetaField("Serialized.Text", out value) && value.IsLogical && value.AsBoolean;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00031291 File Offset: 0x0002F491
		private static IValueReference CreateContentNameValue(Lazy<Request> request)
		{
			return new DelayedValue(() => TextValue.New(request.Value.Uri.Host));
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x000312AF File Offset: 0x0002F4AF
		private static Value CreateContentUriValue(IResource resource, Uri contentUri, TextValue initialUri = null, string cacheKey = null, Func<IDisposable> impersonate = null)
		{
			string absoluteUri = contentUri.AbsoluteUri;
			string text = ((initialUri != null) ? initialUri.AsString : null);
			Func<IDisposable> func = impersonate;
			if (impersonate == null && (func = DataSource.<>c.<>9__15_0) == null)
			{
				func = (DataSource.<>c.<>9__15_0 = () => null);
			}
			return DataSource.CreateContentUriValue(resource, absoluteUri, text, cacheKey, func);
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x000312EF File Offset: 0x0002F4EF
		private static IValueReference CreateContentUriValue(Lazy<Request> request, Lazy<string> cacheKey)
		{
			return new DelayedValue(() => DataSource.CreateContentUriValue(request.Value.RequestResource, request.Value.Uri, request.Value.InitialUri, cacheKey.Value, null));
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00031314 File Offset: 0x0002F514
		private static Value CreateContentUriValue(IResource resource, string contentUri, string initialUri, string cacheKey, Func<IDisposable> impersonate)
		{
			return new DataSource.ContentUriFunctionValue(resource, contentUri, initialUri, cacheKey, impersonate);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00031324 File Offset: 0x0002F524
		private static bool TryGetContentUriMetaValue(Value source, out DataSource.ContentUriFunctionValue contentValue)
		{
			Value value;
			if (source.TryGetMetaField("Content.Uri", out value))
			{
				contentValue = value as DataSource.ContentUriFunctionValue;
				return contentValue != null;
			}
			contentValue = null;
			return false;
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00031354 File Offset: 0x0002F554
		public static RecordValue CreateLocalDataSourceRecordValue(string filePath, Func<IDisposable> impersonate)
		{
			return RecordValue.New(DataSource.DataSourceKeys, new IValueReference[]
			{
				new DataSource.ContentValueReference(filePath, impersonate),
				DataSource.CreateContentUriValue(Resource.New("File", filePath), filePath, null, null, impersonate),
				TextValue.New(Path.GetFileName(filePath)),
				RecordValue.Empty,
				Value.Null,
				Value.Null
			});
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000313B8 File Offset: 0x0002F5B8
		public static RecordValue CreateDataSourceRecordValue(IValueReference contentType, IValueReference headers, Request request, Value webContentsOptions, string cacheKey = null, IValueReference responseStatus = null)
		{
			return RecordValue.New(DataSource.DataSourceKeys, new IValueReference[]
			{
				contentType,
				DataSource.CreateContentUriValue(request.RequestResource, request.Uri, request.InitialUri, cacheKey, null),
				TextValue.New(request.Uri.Host),
				headers,
				webContentsOptions,
				responseStatus ?? Value.Null
			});
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0003141F File Offset: 0x0002F61F
		public static RecordValue CreateDataSourceRecordValue(IValueReference contentType, IValueReference headers, Lazy<Request> request, Value webContentsOptions, Lazy<string> cacheKey, IValueReference responseStatus = null)
		{
			return RecordValue.New(DataSource.DataSourceKeys, new IValueReference[]
			{
				contentType,
				DataSource.CreateContentUriValue(request, cacheKey),
				DataSource.CreateContentNameValue(request),
				headers,
				webContentsOptions,
				responseStatus ?? Value.Null
			});
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00031460 File Offset: 0x0002F660
		public static RecordValue CreateDataSourceRecordValue(IValueReference contentType, Uri contentUri, string contentName, IValueReference headers)
		{
			return RecordValue.New(DataSource.DataSourceKeys, new IValueReference[]
			{
				contentType,
				DataSource.CreateContentUriValue(null, contentUri, null, null, null),
				TextValue.New(contentName),
				headers,
				Value.Null,
				Value.Null
			});
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x000314AB File Offset: 0x0002F6AB
		public static RecordValue CreateDataSourceRecordValue(IValueReference contentType)
		{
			return RecordValue.New(DataSource.DataSourceKeys, new IValueReference[]
			{
				contentType,
				Value.Null,
				TextValue.Empty,
				Value.Null,
				Value.Null,
				Value.Null
			});
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x000314EC File Offset: 0x0002F6EC
		public static string GetDigest(Value contents)
		{
			string @string;
			using (HashAlgorithm hashAlgorithm = CryptoAlgorithmFactory.CreateSHA256Algorithm())
			{
				if (contents.IsBinary)
				{
					using (Stream stream = contents.AsBinary.Open())
					{
						hashAlgorithm.ComputeHash(stream);
						goto IL_0047;
					}
				}
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				hashAlgorithm.ComputeHash(utf8Encoding.GetBytes(contents.AsString));
				IL_0047:
				@string = Base64Encoding.GetString(hashAlgorithm.Hash);
			}
			return @string;
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00031578 File Offset: 0x0002F778
		public static bool TryGetSourceKey(Value source, out string sourceKey)
		{
			DataSource.ContentUriFunctionValue contentUriFunctionValue;
			if (DataSource.TryGetContentUriMetaValue(source, out contentUriFunctionValue))
			{
				if (contentUriFunctionValue.CacheKey != null)
				{
					sourceKey = contentUriFunctionValue.CacheKey;
					return true;
				}
				Uri uri;
				if (Uri.TryCreate(contentUriFunctionValue.FilePath, UriKind.Absolute, out uri))
				{
					sourceKey = uri.AbsoluteUri;
					return true;
				}
			}
			sourceKey = null;
			return false;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x000315C0 File Offset: 0x0002F7C0
		public static bool TryGetLastWriteTimeUtc(Value source, out DateTime lastWriteTimeUtc)
		{
			DataSource.ContentUriFunctionValue contentUriFunctionValue;
			if (DataSource.TryGetContentUriMetaValue(source, out contentUriFunctionValue))
			{
				return contentUriFunctionValue.TryGetLastWriteTimeUtc(out lastWriteTimeUtc);
			}
			lastWriteTimeUtc = default(DateTime);
			return false;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x000315E8 File Offset: 0x0002F7E8
		public static string GetSourceKeyOrDigest(Value source, out string keyType)
		{
			string digest;
			if (DataSource.TryGetSourceKey(source, out digest))
			{
				keyType = "Source";
			}
			else
			{
				keyType = "Digest";
				digest = DataSource.GetDigest(source);
			}
			return digest;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00031618 File Offset: 0x0002F818
		public static bool TryGetFileExtensionAndResource(BinaryValue source, out string extension, out IResource resource)
		{
			DataSource.ContentUriFunctionValue contentUriFunctionValue;
			Uri uri;
			if (DataSource.TryGetContentUriFunctionValue(source, out contentUriFunctionValue) && Uri.TryCreate(contentUriFunctionValue.FilePath, UriKind.Absolute, out uri))
			{
				resource = contentUriFunctionValue.Resource;
				string text;
				if (uri.IsFile)
				{
					text = contentUriFunctionValue.FilePath;
				}
				else if (!SharePointFile.TryGetFilePath(TextValue.New(uri.AbsoluteUri), out text))
				{
					text = uri.AbsoluteUri;
				}
				string extension2 = Path.GetExtension(text);
				if (extension2.Length > 1)
				{
					extension = extension2;
					return true;
				}
			}
			resource = null;
			extension = null;
			return false;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00031690 File Offset: 0x0002F890
		public static bool TryGetSourceFilePath(BinaryValue source, bool supportsImpersonation, out string filePath, out Func<IDisposable> impersonate)
		{
			DataSource.ContentUriFunctionValue contentUriFunctionValue;
			Uri uri;
			if (DataSource.TryGetContentUriFunctionValue(source, out contentUriFunctionValue) && Uri.TryCreate(contentUriFunctionValue.FilePath, UriKind.Absolute, out uri) && uri.IsFile && (supportsImpersonation || !DataSource.RequiresImpersonation(contentUriFunctionValue.Impersonate)))
			{
				filePath = Resource.FileNormalizer.GetNormalizer().GetLocalPath(contentUriFunctionValue.FilePath);
				impersonate = contentUriFunctionValue.Impersonate;
				return true;
			}
			filePath = null;
			impersonate = null;
			return false;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x000316F4 File Offset: 0x0002F8F4
		public static string GetSourceFilePath(IEngineHost host, BinaryValue source, string extension, bool supportsImpersonation, out bool isTempFile, out Func<IDisposable> impersonate)
		{
			string text;
			if (DataSource.TryGetSourceFilePath(source, supportsImpersonation, out text, out impersonate))
			{
				isTempFile = false;
				return text;
			}
			isTempFile = true;
			impersonate = () => null;
			return DataSource.SaveToTempFile(host, source, extension);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00031744 File Offset: 0x0002F944
		public static string GetSourceUrl(Value source)
		{
			Uri uri;
			if (DataSource.TryGetContentUri(source, out uri) && uri.Scheme != "pack" && uri.Scheme != Uri.UriSchemeFile)
			{
				return uri.AbsoluteUri;
			}
			return null;
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00031787 File Offset: 0x0002F987
		public static void EnsureNotHtmlResponse(Value source)
		{
			if (DataSource.IsHtmlResponse(source))
			{
				throw ValueException.NewDataSourceError<Message0>(Strings.WebContentsHtmlNotExpected, Value.Null, null);
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x000317A4 File Offset: 0x0002F9A4
		public static bool TryGetContentUri(Value contents, out Uri contentUri)
		{
			DataSource.ContentUriFunctionValue contentUriFunctionValue;
			if (DataSource.TryGetContentUriFunctionValue(contents, out contentUriFunctionValue))
			{
				return Uri.TryCreate(contentUriFunctionValue.FilePath, UriKind.Absolute, out contentUri);
			}
			contentUri = null;
			return false;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x000317D0 File Offset: 0x0002F9D0
		public static bool TryGetInitialUri(Value contents, out string initialUri)
		{
			DataSource.ContentUriFunctionValue contentUriFunctionValue;
			if (DataSource.TryGetContentUriFunctionValue(contents, out contentUriFunctionValue) && contentUriFunctionValue.InitialUri != null)
			{
				initialUri = contentUriFunctionValue.InitialUri;
				return true;
			}
			initialUri = null;
			return false;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00031800 File Offset: 0x0002FA00
		private static bool TryGetContentUriFunctionValue(Value contents, out DataSource.ContentUriFunctionValue contentValue)
		{
			Value value;
			if (contents.TryGetMetaField("Content.Uri", out value))
			{
				contentValue = value as DataSource.ContentUriFunctionValue;
				return contentValue != null;
			}
			contentValue = null;
			return false;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00031830 File Offset: 0x0002FA30
		private static string GetResponseContentType(Value source)
		{
			Value value;
			if (source.TryGetMetaField("Headers", out value) && value.IsRecord && value.AsRecord.TryGetValue("Content-Type", out value) && value.IsText)
			{
				return value.AsString;
			}
			return null;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00031878 File Offset: 0x0002FA78
		private static bool IsHtmlResponse(Value source)
		{
			string responseContentType = DataSource.GetResponseContentType(source);
			if (string.IsNullOrEmpty(responseContentType))
			{
				return false;
			}
			return responseContentType.Split(new char[] { ';' }).Any((string s) => s.Trim().Equals("text/html", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x000318CC File Offset: 0x0002FACC
		private static string SaveToTempFile(IEngineHost host, BinaryValue source, string extension)
		{
			string text2;
			using (IHostTrace hostTrace = TracingService.CreateTrace(host, "DataSource/SaveToTempFile", TraceEventType.Information, null))
			{
				ITempDirectoryService tempDirectoryService = host.QueryService<ITempDirectoryService>();
				string text = null;
				try
				{
					using (Stream stream = tempDirectoryService.CreateFile(extension, FileAccess.Write, out text))
					{
						byte[] asBytes = source.AsBinary.AsBytes;
						stream.Write(asBytes, 0, asBytes.Length);
					}
					text2 = text;
				}
				catch (Exception ex)
				{
					SafeExceptions.TraceIsSafeException(hostTrace, ex);
					if (text != null)
					{
						FileOperations.TryDelete(host, text, null);
					}
					throw;
				}
			}
			return text2;
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00031974 File Offset: 0x0002FB74
		private static bool RequiresImpersonation(Func<IDisposable> impersonate)
		{
			bool flag;
			using (IDisposable disposable = impersonate())
			{
				flag = disposable != null;
			}
			return flag;
		}

		// Token: 0x040006A9 RID: 1705
		public const string ContentType = "Content.Type";

		// Token: 0x040006AA RID: 1706
		public const string Headers = "Headers";

		// Token: 0x040006AB RID: 1707
		public const string RequestOptions = "Request.Options";

		// Token: 0x040006AC RID: 1708
		public const string ResponseStatus = "Response.Status";

		// Token: 0x040006AD RID: 1709
		public const string UriSchemePack = "pack";

		// Token: 0x040006AE RID: 1710
		public const string ContentName = "Content.Name";

		// Token: 0x040006AF RID: 1711
		public const string ContentUri = "Content.Uri";

		// Token: 0x040006B0 RID: 1712
		public const string SerializedText = "Serialized.Text";

		// Token: 0x040006B1 RID: 1713
		private static readonly Keys DataSourceKeys = Keys.New(new string[] { "Content.Type", "Content.Uri", "Content.Name", "Headers", "Request.Options", "Response.Status" });

		// Token: 0x040006B2 RID: 1714
		private static readonly Keys SerializedTextKeys = Keys.New("Serialized.Text");

		// Token: 0x040006B3 RID: 1715
		private static readonly RecordValue SerializedTextMeta = RecordValue.New(DataSource.SerializedTextKeys, new Value[] { LogicalValue.True });

		// Token: 0x040006B4 RID: 1716
		public static readonly TypeValue SerializedTextType = TypeValue.Text.NewMeta(DataSource.SerializedTextMeta).AsType;

		// Token: 0x040006B5 RID: 1717
		public static readonly TypeValue NullableSerializedTextType = NullableTypeValue.Text.NewMeta(DataSource.SerializedTextMeta).AsType;

		// Token: 0x02000243 RID: 579
		private class ContentUriFunctionValue : NativeFunctionValue0
		{
			// Token: 0x06001923 RID: 6435 RVA: 0x00031A57 File Offset: 0x0002FC57
			public ContentUriFunctionValue(IResource resource, string filePath, string initialUri, string cacheKey, Func<IDisposable> impersonate)
			{
				this.resource = resource;
				this.filePath = filePath;
				this.cacheKey = cacheKey;
				this.initialUri = initialUri;
				this.impersonate = impersonate;
			}

			// Token: 0x17000CA1 RID: 3233
			// (get) Token: 0x06001924 RID: 6436 RVA: 0x00031A84 File Offset: 0x0002FC84
			public string FilePath
			{
				get
				{
					return this.filePath;
				}
			}

			// Token: 0x17000CA2 RID: 3234
			// (get) Token: 0x06001925 RID: 6437 RVA: 0x00031A8C File Offset: 0x0002FC8C
			public string CacheKey
			{
				get
				{
					return this.cacheKey;
				}
			}

			// Token: 0x17000CA3 RID: 3235
			// (get) Token: 0x06001926 RID: 6438 RVA: 0x00031A94 File Offset: 0x0002FC94
			public string InitialUri
			{
				get
				{
					return this.initialUri;
				}
			}

			// Token: 0x17000CA4 RID: 3236
			// (get) Token: 0x06001927 RID: 6439 RVA: 0x00031A9C File Offset: 0x0002FC9C
			public IResource Resource
			{
				get
				{
					return this.resource;
				}
			}

			// Token: 0x06001928 RID: 6440 RVA: 0x00031AA4 File Offset: 0x0002FCA4
			public bool TryGetLastWriteTimeUtc(out DateTime lastWriteTimeUtc)
			{
				using (this.impersonate())
				{
					if (File.Exists(this.filePath))
					{
						lastWriteTimeUtc = File.GetLastWriteTimeUtc(this.filePath);
						return true;
					}
				}
				lastWriteTimeUtc = default(DateTime);
				return false;
			}

			// Token: 0x17000CA5 RID: 3237
			// (get) Token: 0x06001929 RID: 6441 RVA: 0x00031B08 File Offset: 0x0002FD08
			public Func<IDisposable> Impersonate
			{
				get
				{
					return this.impersonate;
				}
			}

			// Token: 0x0600192A RID: 6442 RVA: 0x00031B10 File Offset: 0x0002FD10
			public override Value Invoke()
			{
				return TextValue.New(this.filePath);
			}

			// Token: 0x040006B6 RID: 1718
			private readonly IResource resource;

			// Token: 0x040006B7 RID: 1719
			private readonly string filePath;

			// Token: 0x040006B8 RID: 1720
			private readonly string cacheKey;

			// Token: 0x040006B9 RID: 1721
			private readonly string initialUri;

			// Token: 0x040006BA RID: 1722
			private readonly Func<IDisposable> impersonate;
		}

		// Token: 0x02000244 RID: 580
		private sealed class ContentValueReference : IValueReference
		{
			// Token: 0x0600192B RID: 6443 RVA: 0x00031B1D File Offset: 0x0002FD1D
			public ContentValueReference(string filePath, Func<IDisposable> impersonate)
			{
				this.filePath = filePath;
				this.impersonate = impersonate;
			}

			// Token: 0x17000CA6 RID: 3238
			// (get) Token: 0x0600192C RID: 6444 RVA: 0x00002105 File Offset: 0x00000305
			public bool Evaluated
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000CA7 RID: 3239
			// (get) Token: 0x0600192D RID: 6445 RVA: 0x00031B34 File Offset: 0x0002FD34
			public Value Value
			{
				get
				{
					Value value;
					try
					{
						using (this.impersonate())
						{
							string contentType = ContentHelper.GetContentType(this.filePath);
							value = ((contentType == null) ? TextValue.Empty : TextValue.New(contentType));
						}
					}
					catch (Exception ex)
					{
						throw FileErrors.HandleException(ex, TextValue.New(this.filePath));
					}
					return value;
				}
			}

			// Token: 0x040006BB RID: 1723
			private readonly string filePath;

			// Token: 0x040006BC RID: 1724
			private readonly Func<IDisposable> impersonate;
		}
	}
}
