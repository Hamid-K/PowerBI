using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData
{
	// Token: 0x0200006B RID: 107
	internal sealed class ODataContextUriBuilder
	{
		// Token: 0x060003CF RID: 975 RVA: 0x0000AD56 File Offset: 0x00008F56
		private ODataContextUriBuilder(Uri baseContextUrl, bool throwIfMissingInfo)
		{
			this.baseContextUrl = baseContextUrl;
			this.throwIfMissingInfo = throwIfMissingInfo;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000AD6C File Offset: 0x00008F6C
		internal static ODataContextUriBuilder Create(Uri baseContextUrl, bool throwIfMissingInfo)
		{
			if (baseContextUrl == null && throwIfMissingInfo)
			{
				throw new ODataException(Strings.ODataOutputContext_MetadataDocumentUriMissing);
			}
			return new ODataContextUriBuilder(baseContextUrl, throwIfMissingInfo);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000AD8C File Offset: 0x00008F8C
		internal Uri BuildContextUri(ODataPayloadKind payloadKind, ODataContextUrlInfo contextInfo = null)
		{
			if (this.baseContextUrl == null)
			{
				return null;
			}
			Action<ODataContextUrlInfo> action;
			if (!ODataContextUriBuilder.ValidationDictionary.TryGetValue(payloadKind, out action))
			{
				throw new ODataException(Strings.ODataContextUriBuilder_UnsupportedPayloadKind(payloadKind.ToString()));
			}
			if (action != null && this.throwIfMissingInfo)
			{
				action(contextInfo);
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

		// Token: 0x060003D2 RID: 978 RVA: 0x0000AE20 File Offset: 0x00009020
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
				switch (info.DeltaKind)
				{
				case ODataDeltaKind.ResourceSet:
					return new Uri("#$delta", UriKind.Relative);
				case ODataDeltaKind.DeletedEntry:
					return new Uri("#$deletedEntity", UriKind.Relative);
				case ODataDeltaKind.Link:
					return new Uri("#$link", UriKind.Relative);
				case ODataDeltaKind.DeletedLink:
					return new Uri("#$deletedLink", UriKind.Relative);
				}
				if (string.IsNullOrEmpty(info.TypeName))
				{
					return null;
				}
				stringBuilder.Append(info.TypeName);
			}
			return new Uri(this.baseContextUrl, stringBuilder.ToString());
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000AFB6 File Offset: 0x000091B6
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

		// Token: 0x060003D4 RID: 980 RVA: 0x0000AFF5 File Offset: 0x000091F5
		private static void ValidateType(ODataContextUrlInfo contextUrlInfo)
		{
			if (string.IsNullOrEmpty(contextUrlInfo.TypeName))
			{
				throw new ODataException(Strings.ODataContextUriBuilder_TypeNameMissingForProperty);
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000B00F File Offset: 0x0000920F
		private static void ValidateCollectionType(ODataContextUrlInfo contextUrlInfo)
		{
			if (string.IsNullOrEmpty(contextUrlInfo.TypeName))
			{
				throw new ODataException(Strings.ODataContextUriBuilder_TypeNameMissingForTopLevelCollection);
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000B02C File Offset: 0x0000922C
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

		// Token: 0x060003D7 RID: 983 RVA: 0x0000B09C File Offset: 0x0000929C
		private static void ValidateResourcePath(ODataContextUrlInfo contextUrlInfo)
		{
			if (string.IsNullOrEmpty(contextUrlInfo.ResourcePath))
			{
				throw new ODataException(Strings.ODataContextUriBuilder_ODataUriMissingForIndividualProperty);
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000239D File Offset: 0x0000059D
		private static void ValidateDelta(ODataContextUrlInfo contextUrlInfo)
		{
		}

		// Token: 0x040001BF RID: 447
		private readonly Uri baseContextUrl;

		// Token: 0x040001C0 RID: 448
		private readonly bool throwIfMissingInfo;

		// Token: 0x040001C1 RID: 449
		private static readonly Dictionary<ODataPayloadKind, Action<ODataContextUrlInfo>> ValidationDictionary = new Dictionary<ODataPayloadKind, Action<ODataContextUrlInfo>>(EqualityComparer<ODataPayloadKind>.Default)
		{
			{
				ODataPayloadKind.ServiceDocument,
				null
			},
			{
				ODataPayloadKind.EntityReferenceLink,
				null
			},
			{
				ODataPayloadKind.EntityReferenceLinks,
				null
			},
			{
				ODataPayloadKind.IndividualProperty,
				new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateResourcePath)
			},
			{
				ODataPayloadKind.Collection,
				new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateCollectionType)
			},
			{
				ODataPayloadKind.Property,
				new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateType)
			},
			{
				ODataPayloadKind.Resource,
				new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateNavigationSource)
			},
			{
				ODataPayloadKind.ResourceSet,
				new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateNavigationSource)
			},
			{
				ODataPayloadKind.Delta,
				new Action<ODataContextUrlInfo>(ODataContextUriBuilder.ValidateDelta)
			}
		};
	}
}
