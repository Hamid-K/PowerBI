using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.ReportingServices.Library;

namespace Microsoft.ReportingServices.Portal.Services.SystemResources
{
	// Token: 0x02000035 RID: 53
	internal sealed class EmbeddedSystemResourceManager : ISystemResourceManager
	{
		// Token: 0x06000230 RID: 560 RVA: 0x0000EE20 File Offset: 0x0000D020
		public SystemResource Install(Func<Stream> metadataStream, IDictionary<string, Func<Stream>> contentStreams, byte[] packageBytes, string packageName, string typeName, Func<string, ISystemResourcePackageContentValidator> validator, Func<string, string> contentTypeMapper)
		{
			string text;
			string text2;
			string text3;
			IDictionary<string, SystemResourceContentItem> dictionary;
			SystemResourceManager.ParseMetadata(SystemResourceManager.DeserializeMetadata(metadataStream), out text, out text2, out text3, out dictionary);
			EmbeddedSystemResource embeddedSystemResource = new EmbeddedSystemResource
			{
				Name = text3,
				Version = text2,
				TypeName = text,
				PackageName = packageName
			};
			foreach (KeyValuePair<string, SystemResourceContentItem> keyValuePair in dictionary)
			{
				string contentType = keyValuePair.Value.ContentType;
				string text4 = string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					EmbeddedSystemResourceManager.ResourceRoot,
					text,
					"_1c648e99_e3a2_4639_90ee_71fff9f08ee3",
					keyValuePair.Value.Path.Replace('\\', '.')
				});
				embeddedSystemResource.Contents.Add(keyValuePair.Key, new SystemResourceContentItem
				{
					Key = keyValuePair.Key,
					ContentType = contentType,
					Path = text4
				});
				byte[] bytes = EmbeddedSystemResourceManager.GetBytes(text4);
				EmbeddedSystemResourceManager._contentCache.Add(EmbeddedSystemResourceManager.CreateCacheKey(new string[] { text, keyValuePair.Key }), bytes);
			}
			IDictionary<string, byte[]> contentCache = EmbeddedSystemResourceManager._contentCache;
			string[] array = new string[2];
			array[0] = text;
			contentCache.Add(EmbeddedSystemResourceManager.CreateCacheKey(array), packageBytes);
			EmbeddedSystemResourceManager._resourceCache.Add(EmbeddedSystemResourceManager.CreateCacheKey(new string[] { text }), embeddedSystemResource);
			return embeddedSystemResource;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000EF8C File Offset: 0x0000D18C
		public IEnumerable<SystemResource> LoadAll()
		{
			return EmbeddedSystemResourceManager._resourceCache.Values;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000B334 File Offset: 0x00009534
		public bool TryDelete(string typeName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000EF98 File Offset: 0x0000D198
		public bool TryLoadByTypeName(string typeName, out SystemResource systemResource)
		{
			systemResource = null;
			EmbeddedSystemResource embeddedSystemResource;
			if (EmbeddedSystemResourceManager._resourceCache.TryGetValue(typeName.ToLowerInvariant(), out embeddedSystemResource))
			{
				systemResource = embeddedSystemResource;
				return true;
			}
			return false;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
		public bool TryLoadContentItem(string typeName, string key, out byte[] bytes)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw new ArgumentNullException("typeName");
			}
			string text = EmbeddedSystemResourceManager.CreateCacheKey(new string[] { typeName, key });
			return EmbeddedSystemResourceManager._contentCache.TryGetValue(text, out bytes);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00002C7C File Offset: 0x00000E7C
		private EmbeddedSystemResourceManager()
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000F004 File Offset: 0x0000D204
		internal static EmbeddedSystemResourceManager Create()
		{
			if (EmbeddedSystemResourceManager._instance == null)
			{
				object mutex = EmbeddedSystemResourceManager._mutex;
				lock (mutex)
				{
					if (EmbeddedSystemResourceManager._instance == null)
					{
						EmbeddedSystemResourceManager embeddedSystemResourceManager = new EmbeddedSystemResourceManager();
						IEnumerable<string> enumerable = from x in EmbeddedSystemResourceManager.ResourceAssembly.GetManifestResourceNames()
							where x.StartsWith(EmbeddedSystemResourceManager.ResourceRoot, StringComparison.OrdinalIgnoreCase)
							select x;
						if (enumerable.Any<string>())
						{
							IEnumerable<string> enumerable2 = (from x in enumerable
								select x.Substring(EmbeddedSystemResourceManager.ResourceRoot.Length + 1) into x
								select x.Split(new char[] { '.' }).First<string>()).Distinct<string>();
							if (enumerable2.Any<string>())
							{
								using (IEnumerator<string> enumerator = enumerable2.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										string typeName = enumerator.Current;
										IEnumerable<string> enumerable3 = enumerable.Where((string x) => x.StartsWith(string.Format("{0}.{1}.", EmbeddedSystemResourceManager.ResourceRoot, typeName), StringComparison.OrdinalIgnoreCase));
										if (enumerable3.Any<string>())
										{
											string metadataLocation = enumerable3.SingleOrDefault((string x) => string.Equals(x, string.Format("{0}.{1}.{2}", EmbeddedSystemResourceManager.ResourceRoot, typeName, "metadata.xml"), StringComparison.OrdinalIgnoreCase));
											if (metadataLocation != null)
											{
												IEnumerable<string> enumerable4 = enumerable3.Where((string x) => x.StartsWith(string.Format("{0}.{1}.{2}.", EmbeddedSystemResourceManager.ResourceRoot, typeName, "_1c648e99_e3a2_4639_90ee_71fff9f08ee3"), StringComparison.OrdinalIgnoreCase));
												if (enumerable4.Any<string>())
												{
													string text = enumerable3.Where((string x) => !string.Equals(x, metadataLocation, StringComparison.OrdinalIgnoreCase)).Except(enumerable4).SingleOrDefault<string>();
													if (text != null)
													{
														embeddedSystemResourceManager.Install(() => EmbeddedSystemResourceManager.ResourceAssembly.GetManifestResourceStream(metadataLocation), enumerable4.ToDictionary((string x) => x.Substring(string.Format("{0}.{1}.{2}.", EmbeddedSystemResourceManager.ResourceRoot, typeName, "_1c648e99_e3a2_4639_90ee_71fff9f08ee3").Length + 1), (string x) => () => EmbeddedSystemResourceManager.ResourceAssembly.GetManifestResourceStream(x)), EmbeddedSystemResourceManager.GetBytes(text), text.Substring(string.Format("{0}.{1}.", EmbeddedSystemResourceManager.ResourceRoot, typeName).Length), null, null, null);
													}
												}
											}
										}
									}
								}
							}
						}
						EmbeddedSystemResourceManager._instance = embeddedSystemResourceManager;
					}
				}
			}
			return EmbeddedSystemResourceManager._instance;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000F264 File Offset: 0x0000D464
		private static string CreateCacheKey(params string[] names)
		{
			return string.Join(":", names).ToLowerInvariant();
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000F278 File Offset: 0x0000D478
		private static byte[] GetBytes(string location)
		{
			byte[] array2;
			using (Stream manifestResourceStream = EmbeddedSystemResourceManager.ResourceAssembly.GetManifestResourceStream(location))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					byte[] array = new byte[4096];
					int num;
					while ((num = manifestResourceStream.Read(array, 0, array.Length)) > 0)
					{
						memoryStream.Write(array, 0, num);
					}
					array2 = memoryStream.ToArray();
				}
			}
			return array2;
		}

		// Token: 0x040000AA RID: 170
		internal const string ContentFolder = "_1c648e99_e3a2_4639_90ee_71fff9f08ee3";

		// Token: 0x040000AB RID: 171
		internal static readonly Assembly ResourceAssembly = Assembly.GetAssembly(typeof(EmbeddedSystemResourceManager));

		// Token: 0x040000AC RID: 172
		internal static readonly string ResourceRoot = EmbeddedSystemResourceManager.ResourceAssembly.GetName().Name + ".SystemResources";

		// Token: 0x040000AD RID: 173
		private static readonly IDictionary<string, byte[]> _contentCache = new Dictionary<string, byte[]>();

		// Token: 0x040000AE RID: 174
		private static readonly IDictionary<string, EmbeddedSystemResource> _resourceCache = new Dictionary<string, EmbeddedSystemResource>();

		// Token: 0x040000AF RID: 175
		private static readonly object _mutex = new object();

		// Token: 0x040000B0 RID: 176
		private static EmbeddedSystemResourceManager _instance;
	}
}
