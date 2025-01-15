using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.Core
{
	// Token: 0x0200015B RID: 347
	internal sealed class ODataContextUriBuilder
	{
		// Token: 0x06000CF5 RID: 3317 RVA: 0x00030633 File Offset: 0x0002E833
		private ODataContextUriBuilder(Uri baseContextUrl, bool throwIfMissingInfo)
		{
			this.baseContextUrl = baseContextUrl;
			this.throwIfMissingInfo = throwIfMissingInfo;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00030649 File Offset: 0x0002E849
		internal static ODataContextUriBuilder Create(Uri baseContextUrl, bool throwIfMissingInfo)
		{
			if (baseContextUrl == null && throwIfMissingInfo)
			{
				throw new ODataException(Strings.ODataOutputContext_MetadataDocumentUriMissing);
			}
			return new ODataContextUriBuilder(baseContextUrl, throwIfMissingInfo);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0003066C File Offset: 0x0002E86C
		internal Uri BuildContextUri(ODataPayloadKind payloadKind, ODataContextUrlInfo contextInfo = null)
		{
			if (this.baseContextUrl == null)
			{
				return null;
			}
			Action<ODataContextUrlInfo> action;
			if (!ODataContextUriBuilder.ValidationDictionary.TryGetValue(payloadKind, ref action))
			{
				throw new ODataException(Strings.ODataContextUriBuilder_UnsupportedPayloadKind(payloadKind.ToString()));
			}
			if (action != null && this.throwIfMissingInfo)
			{
				action.Invoke(contextInfo);
			}
			switch (payloadKind)
			{
			case ODataPayloadKind.EntityReferenceLink:
				return new Uri(this.baseContextUrl, "#$ref");
			case ODataPayloadKind.EntityReferenceLinks:
				return new Uri(this.baseContextUrl, "#Collection($ref)");
			default:
				if (payloadKind == ODataPayloadKind.ServiceDocument)
				{
					return this.baseContextUrl;
				}
				return this.CreateFromContextUrlInfo(contextInfo);
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00030708 File Offset: 0x0002E908
		private Uri CreateFromContextUrlInfo(ODataContextUrlInfo info)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('#');
			if (!string.IsNullOrEmpty(info.ResourcePath))
			{
				stringBuilder.Append(info.ResourcePath);
			}
			else if (!string.IsNullOrEmpty(info.NavigationPath))
			{
				stringBuilder.Append(info.NavigationPath);
				if (info.DeltaKind == ODataDeltaKind.None || info.DeltaKind == ODataDeltaKind.Feed || info.DeltaKind == ODataDeltaKind.Entry)
				{
					if (!string.IsNullOrEmpty(info.TypeCast))
					{
						stringBuilder.Append('/');
						stringBuilder.Append(info.TypeCast);
					}
					if (!string.IsNullOrEmpty(info.QueryClause))
					{
						stringBuilder.Append(info.QueryClause);
					}
				}
				switch (info.DeltaKind)
				{
				case ODataDeltaKind.None:
				case ODataDeltaKind.Entry:
					if (info.IncludeFragmentItemSelector)
					{
						stringBuilder.Append("/$entity");
					}
					break;
				case ODataDeltaKind.Feed:
					stringBuilder.Append("/$delta");
					break;
				case ODataDeltaKind.DeletedEntry:
					stringBuilder.Append("/$deletedEntity");
					break;
				case ODataDeltaKind.Link:
					stringBuilder.Append("/$link");
					break;
				case ODataDeltaKind.DeletedLink:
					stringBuilder.Append("/$deletedLink");
					break;
				}
			}
			else
			{
				if (string.IsNullOrEmpty(info.TypeName))
				{
					return null;
				}
				stringBuilder.Append(info.TypeName);
			}
			return new Uri(this.baseContextUrl, stringBuilder.ToString());
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0003085B File Offset: 0x0002EA5B
		private static void ValidateType(ODataContextUrlInfo contextUrlInfo)
		{
			if (string.IsNullOrEmpty(contextUrlInfo.TypeName))
			{
				throw new ODataException(Strings.ODataContextUriBuilder_TypeNameMissingForProperty);
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00030875 File Offset: 0x0002EA75
		private static void ValidateCollectionType(ODataContextUrlInfo contextUrlInfo)
		{
			if (string.IsNullOrEmpty(contextUrlInfo.TypeName))
			{
				throw new ODataException(Strings.ODataContextUriBuilder_TypeNameMissingForTopLevelCollection);
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00030890 File Offset: 0x0002EA90
		private static void ValidateNavigationSource(ODataContextUrlInfo contextUrlInfo)
		{
			if ((!contextUrlInfo.IsUnknownEntitySet && string.IsNullOrEmpty(contextUrlInfo.NavigationPath)) || (contextUrlInfo.IsUnknownEntitySet && string.IsNullOrEmpty(contextUrlInfo.NavigationSource) && string.IsNullOrEmpty(contextUrlInfo.TypeName)))
			{
				throw new ODataException(Strings.ODataContextUriBuilder_NavigationSourceMissingForEntryAndFeed);
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000308DF File Offset: 0x0002EADF
		private static void ValidateResourcePath(ODataContextUrlInfo contextUrlInfo)
		{
			if (string.IsNullOrEmpty(contextUrlInfo.ResourcePath))
			{
				throw new ODataException(Strings.ODataContextUriBuilder_ODataUriMissingForIndividualProperty);
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000308F9 File Offset: 0x0002EAF9
		private static void ValidateDelta(ODataContextUrlInfo contextUrlInfo)
		{
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x000308FC File Offset: 0x0002EAFC
		// Note: this type is marked as 'beforefieldinit'.
		static ODataContextUriBuilder()
		{
			Dictionary<ODataPayloadKind, Action<ODataContextUrlInfo>> dictionary = new Dictionary<ODataPayloadKind, Action<ODataContextUrlInfo>>(EqualityComparer<ODataPayloadKind>.Default);
			dictionary.Add(ODataPayloadKind.ServiceDocument, null);
			dictionary.Add(ODataPayloadKind.EntityReferenceLink, null);
			dictionary.Add(ODataPayloadKind.EntityReferenceLinks, null);
			dictionary.Add(ODataPayloadKind.IndividualProperty, new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateResourcePath));
			dictionary.Add(ODataPayloadKind.Collection, new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateCollectionType));
			dictionary.Add(ODataPayloadKind.Property, new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateType));
			dictionary.Add(ODataPayloadKind.Entry, new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateNavigationSource));
			dictionary.Add(ODataPayloadKind.Feed, new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateNavigationSource));
			dictionary.Add(ODataPayloadKind.Delta, new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateDelta));
			ODataContextUriBuilder.ValidationDictionary = dictionary;
		}

		// Token: 0x04000599 RID: 1433
		private readonly Uri baseContextUrl;

		// Token: 0x0400059A RID: 1434
		private readonly bool throwIfMissingInfo;

		// Token: 0x0400059B RID: 1435
		private static readonly Dictionary<ODataPayloadKind, Action<ODataContextUrlInfo>> ValidationDictionary;
	}
}
