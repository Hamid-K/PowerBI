using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData
{
	// Token: 0x02000049 RID: 73
	internal sealed class ODataContextUriBuilder
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000901C File Offset: 0x0000721C
		private ODataContextUriBuilder(Uri baseContextUrl, bool throwIfMissingInfo)
		{
			this.baseContextUrl = baseContextUrl;
			this.throwIfMissingInfo = throwIfMissingInfo;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009032 File Offset: 0x00007232
		internal static ODataContextUriBuilder Create(Uri baseContextUrl, bool throwIfMissingInfo)
		{
			if (baseContextUrl == null && throwIfMissingInfo)
			{
				throw new ODataException(Strings.ODataOutputContext_MetadataDocumentUriMissing);
			}
			return new ODataContextUriBuilder(baseContextUrl, throwIfMissingInfo);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009054 File Offset: 0x00007254
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
			if (payloadKind == ODataPayloadKind.EntityReferenceLink)
			{
				return new Uri(this.baseContextUrl, "#$ref");
			}
			if (payloadKind == ODataPayloadKind.EntityReferenceLinks)
			{
				return new Uri(this.baseContextUrl, "#Collection($ref)");
			}
			if (payloadKind == ODataPayloadKind.ServiceDocument)
			{
				return this.baseContextUrl;
			}
			return this.CreateFromContextUrlInfo(contextInfo);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000090E8 File Offset: 0x000072E8
		private Uri CreateFromContextUrlInfo(ODataContextUrlInfo info)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('#');
			if (!string.IsNullOrEmpty(info.ResourcePath))
			{
				stringBuilder.Append(info.ResourcePath);
				if (info.DeltaKind == ODataDeltaKind.None)
				{
					ODataContextUriBuilder.AppendTypeCastAndQueryClause(stringBuilder, info);
				}
			}
			else if (!string.IsNullOrEmpty(info.NavigationPath))
			{
				stringBuilder.Append(info.NavigationPath);
				if (info.DeltaKind == ODataDeltaKind.None || info.DeltaKind == ODataDeltaKind.ResourceSet || info.DeltaKind == ODataDeltaKind.Resource)
				{
					ODataContextUriBuilder.AppendTypeCastAndQueryClause(stringBuilder, info);
				}
				switch (info.DeltaKind)
				{
				case ODataDeltaKind.None:
				case ODataDeltaKind.Resource:
					if (info.IncludeFragmentItemSelector)
					{
						stringBuilder.Append("/$entity");
					}
					break;
				case ODataDeltaKind.ResourceSet:
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

		// Token: 0x0600025A RID: 602 RVA: 0x00009217 File Offset: 0x00007417
		private static void AppendTypeCastAndQueryClause(StringBuilder builder, ODataContextUrlInfo info)
		{
			if (!string.IsNullOrEmpty(info.TypeCast))
			{
				builder.Append('/');
				builder.Append(info.TypeCast);
			}
			if (!string.IsNullOrEmpty(info.QueryClause))
			{
				builder.Append(info.QueryClause);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009256 File Offset: 0x00007456
		private static void ValidateType(ODataContextUrlInfo contextUrlInfo)
		{
			if (string.IsNullOrEmpty(contextUrlInfo.TypeName))
			{
				throw new ODataException(Strings.ODataContextUriBuilder_TypeNameMissingForProperty);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009270 File Offset: 0x00007470
		private static void ValidateCollectionType(ODataContextUrlInfo contextUrlInfo)
		{
			if (string.IsNullOrEmpty(contextUrlInfo.TypeName))
			{
				throw new ODataException(Strings.ODataContextUriBuilder_TypeNameMissingForTopLevelCollection);
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000928C File Offset: 0x0000748C
		private static void ValidateNavigationSource(ODataContextUrlInfo contextUrlInfo)
		{
			if (!contextUrlInfo.HasNavigationSourceInfo)
			{
				if (string.IsNullOrEmpty(contextUrlInfo.TypeName))
				{
					throw new ODataException(Strings.ODataContextUriBuilder_NavigationSourceOrTypeNameMissingForResourceOrResourceSet);
				}
				return;
			}
			else
			{
				if ((!contextUrlInfo.IsUnknownEntitySet && string.IsNullOrEmpty(contextUrlInfo.NavigationPath)) || (contextUrlInfo.IsUnknownEntitySet && string.IsNullOrEmpty(contextUrlInfo.NavigationSource) && string.IsNullOrEmpty(contextUrlInfo.TypeName)))
				{
					throw new ODataException(Strings.ODataContextUriBuilder_NavigationSourceOrTypeNameMissingForResourceOrResourceSet);
				}
				return;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000092FC File Offset: 0x000074FC
		private static void ValidateResourcePath(ODataContextUrlInfo contextUrlInfo)
		{
			if (string.IsNullOrEmpty(contextUrlInfo.ResourcePath))
			{
				throw new ODataException(Strings.ODataContextUriBuilder_ODataUriMissingForIndividualProperty);
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000250D File Offset: 0x0000070D
		private static void ValidateDelta(ODataContextUrlInfo contextUrlInfo)
		{
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009318 File Offset: 0x00007518
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
			dictionary.Add(ODataPayloadKind.Resource, new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateNavigationSource));
			dictionary.Add(ODataPayloadKind.ResourceSet, new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateNavigationSource));
			dictionary.Add(ODataPayloadKind.Delta, new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateDelta));
			ODataContextUriBuilder.ValidationDictionary = dictionary;
		}

		// Token: 0x0400015D RID: 349
		private readonly Uri baseContextUrl;

		// Token: 0x0400015E RID: 350
		private readonly bool throwIfMissingInfo;

		// Token: 0x0400015F RID: 351
		private static readonly Dictionary<ODataPayloadKind, Action<ODataContextUrlInfo>> ValidationDictionary;
	}
}
