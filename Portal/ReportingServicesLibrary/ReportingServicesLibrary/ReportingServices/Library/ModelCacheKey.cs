using System;
using System.Globalization;
using System.Web.Caching;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000177 RID: 375
	internal sealed class ModelCacheKey
	{
		// Token: 0x06000DBC RID: 3516 RVA: 0x00032392 File Offset: 0x00030592
		private ModelCacheKey(ModelCacheInfo cacheInfo, string sessionKey, string modelKey)
		{
			this.m_cacheInfo = cacheInfo;
			this.m_sessionKey = sessionKey;
			this.m_modelKey = modelKey;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000323B0 File Offset: 0x000305B0
		internal static ModelCacheKey Create(ModelCacheInfo cacheInfo, ModelCacheKey.CacheKeyType type)
		{
			string text = ModelCacheKey.MakeSessionKey(cacheInfo);
			string text2;
			if (string.IsNullOrEmpty(cacheInfo.ModelPerspectiveName))
			{
				text2 = string.Join("|", new string[]
				{
					RenderEditRequest.EncodeCacheKeyValue(ModelCacheKey.NormalizeUrlForCacheKey(cacheInfo.ItemPath)),
					RenderEditRequest.EncodeCacheKeyValue(cacheInfo.DataSourceName),
					RenderEditRequest.EncodeCacheKeyValue(cacheInfo.ModelMetadataVersion),
					RenderEditRequest.EncodeCacheKeyValue(type.ToString())
				});
			}
			else
			{
				text2 = string.Join("|", new string[]
				{
					RenderEditRequest.EncodeCacheKeyValue(ModelCacheKey.NormalizeUrlForCacheKey(cacheInfo.ItemPath)),
					RenderEditRequest.EncodeCacheKeyValue(cacheInfo.DataSourceName),
					RenderEditRequest.EncodeCacheKeyValue(cacheInfo.ModelMetadataVersion),
					RenderEditRequest.EncodeCacheKeyValue(cacheInfo.ModelPerspectiveName),
					RenderEditRequest.EncodeCacheKeyValue(type.ToString())
				});
			}
			return new ModelCacheKey(cacheInfo, text, text2);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00032494 File Offset: 0x00030694
		internal ModelCacheKey GetGlobalKey()
		{
			string text = ModelCacheKey.MakeSessionKey(this.m_cacheInfo, ModelCacheScope.Global);
			if (this.m_sessionKey == text)
			{
				return this;
			}
			return new ModelCacheKey(this.m_cacheInfo, text, this.m_modelKey);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000324D0 File Offset: 0x000306D0
		internal static string MakeSessionKey(ModelCacheInfo modelCacheInfo)
		{
			ModelCacheScope modelCacheScope = DataShapeModelCacheManager.DetermineModelCacheScope(modelCacheInfo);
			return ModelCacheKey.MakeSessionKey(modelCacheInfo, modelCacheScope);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000324EB File Offset: 0x000306EB
		private static string MakeSessionKey(ModelCacheInfo modelCacheInfo, ModelCacheScope modelCacheScope)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}|{1}", (modelCacheScope == ModelCacheScope.Global) ? ProcessingContext.ReqContext.DatabaseFullName : modelCacheInfo.Session.MakeCacheKey(), "ModelKey");
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0003251B File Offset: 0x0003071B
		internal IRenderEditSession Session
		{
			get
			{
				return this.m_cacheInfo.Session;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00032528 File Offset: 0x00030728
		internal string SessionKey
		{
			get
			{
				return this.m_sessionKey;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00032530 File Offset: 0x00030730
		internal string ModelKey
		{
			get
			{
				return this.m_modelKey;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00032538 File Offset: 0x00030738
		internal ModelCacheInfo CacheInfo
		{
			get
			{
				return this.m_cacheInfo;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00032540 File Offset: 0x00030740
		internal DateTime AbsoluteExpiration
		{
			get
			{
				return Cache.NoAbsoluteExpiration;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00032547 File Offset: 0x00030747
		internal TimeSpan SlidingExpiration
		{
			get
			{
				return TimeSpan.FromSeconds((double)Globals.Configuration.RdlxSessionTimeout);
			}
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0003255C File Offset: 0x0003075C
		private static string NormalizeUrlForCacheKey(string itemPath)
		{
			string text = "/";
			if (itemPath == string.Empty || itemPath == text)
			{
				return itemPath;
			}
			string text2;
			try
			{
				text2 = new Uri(itemPath).ToString().ToUpperInvariant();
			}
			catch (UriFormatException)
			{
				text2 = itemPath;
			}
			return text2;
		}

		// Token: 0x040005AD RID: 1453
		private readonly ModelCacheInfo m_cacheInfo;

		// Token: 0x040005AE RID: 1454
		private readonly string m_sessionKey;

		// Token: 0x040005AF RID: 1455
		private readonly string m_modelKey;

		// Token: 0x040005B0 RID: 1456
		private const string CacheKeySuffix = "ModelKey";

		// Token: 0x02000475 RID: 1141
		internal enum CacheKeyType
		{
			// Token: 0x04000FE7 RID: 4071
			ModelString,
			// Token: 0x04000FE8 RID: 4072
			ParsedModel
		}
	}
}
