using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000EB RID: 235
	internal static class XmlConstants
	{
		// Token: 0x040003AC RID: 940
		internal const string ClrServiceInitializationMethodName = "InitializeService";

		// Token: 0x040003AD RID: 941
		internal const string HttpContentID = "Content-ID";

		// Token: 0x040003AE RID: 942
		internal const string HttpContentLength = "Content-Length";

		// Token: 0x040003AF RID: 943
		internal const string HttpContentType = "Content-Type";

		// Token: 0x040003B0 RID: 944
		internal const string HttpContentDisposition = "Content-Disposition";

		// Token: 0x040003B1 RID: 945
		internal const string HttpODataVersion = "OData-Version";

		// Token: 0x040003B2 RID: 946
		internal const string HttpODataMaxVersion = "OData-MaxVersion";

		// Token: 0x040003B3 RID: 947
		internal const string HttpPrefer = "Prefer";

		// Token: 0x040003B4 RID: 948
		internal const string HttpPreferReturnNoContent = "return=minimal";

		// Token: 0x040003B5 RID: 949
		internal const string HttpPreferReturnContent = "return=representation";

		// Token: 0x040003B6 RID: 950
		internal const string HttpCacheControlNoCache = "no-cache";

		// Token: 0x040003B7 RID: 951
		internal const string HttpCharsetParameter = "charset";

		// Token: 0x040003B8 RID: 952
		internal const string HttpMethodGet = "GET";

		// Token: 0x040003B9 RID: 953
		internal const string HttpMethodPost = "POST";

		// Token: 0x040003BA RID: 954
		internal const string HttpMethodPut = "PUT";

		// Token: 0x040003BB RID: 955
		internal const string HttpMethodDelete = "DELETE";

		// Token: 0x040003BC RID: 956
		internal const string HttpMethodPatch = "PATCH";

		// Token: 0x040003BD RID: 957
		internal const string HttpQueryStringExpand = "$expand";

		// Token: 0x040003BE RID: 958
		internal const string HttpQueryStringFilter = "$filter";

		// Token: 0x040003BF RID: 959
		internal const string HttpQueryStringOrderBy = "$orderby";

		// Token: 0x040003C0 RID: 960
		internal const string HttpQueryStringSkip = "$skip";

		// Token: 0x040003C1 RID: 961
		internal const string HttpQueryStringTop = "$top";

		// Token: 0x040003C2 RID: 962
		internal const string HttpQueryStringQueryCount = "$count";

		// Token: 0x040003C3 RID: 963
		internal const string HttpQueryStringSkipToken = "$skiptoken";

		// Token: 0x040003C4 RID: 964
		internal const string SkipTokenPropertyPrefix = "SkipTokenProperty";

		// Token: 0x040003C5 RID: 965
		internal const string HttpQueryStringSegmentCount = "$count";

		// Token: 0x040003C6 RID: 966
		internal const string HttpQueryStringSelect = "$select";

		// Token: 0x040003C7 RID: 967
		internal const string HttpQueryStringFormat = "$format";

		// Token: 0x040003C8 RID: 968
		internal const string HttpQueryStringCallback = "$callback";

		// Token: 0x040003C9 RID: 969
		internal const string HttpQueryStringId = "$id";

		// Token: 0x040003CA RID: 970
		internal const string HttpQValueParameter = "q";

		// Token: 0x040003CB RID: 971
		internal const string HttpXMethod = "X-HTTP-Method";

		// Token: 0x040003CC RID: 972
		internal const string HttpRequestAccept = "Accept";

		// Token: 0x040003CD RID: 973
		internal const string HttpRequestAcceptCharset = "Accept-Charset";

		// Token: 0x040003CE RID: 974
		internal const string HttpRequestIfMatch = "If-Match";

		// Token: 0x040003CF RID: 975
		internal const string HttpRequestIfNoneMatch = "If-None-Match";

		// Token: 0x040003D0 RID: 976
		internal const string HttpUserAgent = "User-Agent";

		// Token: 0x040003D1 RID: 977
		internal const string HttpMultipartBoundary = "boundary";

		// Token: 0x040003D2 RID: 978
		internal const string XContentTypeOptions = "X-Content-Type-Options";

		// Token: 0x040003D3 RID: 979
		internal const string XContentTypeOptionNoSniff = "nosniff";

		// Token: 0x040003D4 RID: 980
		internal const string HttpMultipartBoundaryBatch = "batch";

		// Token: 0x040003D5 RID: 981
		internal const string HttpMultipartBoundaryChangeSet = "changeset";

		// Token: 0x040003D6 RID: 982
		internal const string HttpResponseAllow = "Allow";

		// Token: 0x040003D7 RID: 983
		internal const string HttpResponseCacheControl = "Cache-Control";

		// Token: 0x040003D8 RID: 984
		internal const string HttpResponseETag = "ETag";

		// Token: 0x040003D9 RID: 985
		internal const string HttpResponseLocation = "Location";

		// Token: 0x040003DA RID: 986
		internal const string HttpODataEntityId = "OData-EntityId";

		// Token: 0x040003DB RID: 987
		internal const string HttpResponseStatusCode = "Status-Code";

		// Token: 0x040003DC RID: 988
		internal const string HttpMultipartBoundaryBatchResponse = "batchresponse";

		// Token: 0x040003DD RID: 989
		internal const string HttpMultipartBoundaryChangesetResponse = "changesetresponse";

		// Token: 0x040003DE RID: 990
		internal const string HttpContentTransferEncoding = "Content-Transfer-Encoding";

		// Token: 0x040003DF RID: 991
		internal const string HttpVersionInBatching = "HTTP/1.1";

		// Token: 0x040003E0 RID: 992
		internal const string HttpAnyETag = "*";

		// Token: 0x040003E1 RID: 993
		internal const string HttpWeakETagPrefix = "W/\"";

		// Token: 0x040003E2 RID: 994
		internal const string HttpAccept = "Accept";

		// Token: 0x040003E3 RID: 995
		internal const string HttpAcceptCharset = "Accept-Charset";

		// Token: 0x040003E4 RID: 996
		internal const string HttpCookie = "Cookie";

		// Token: 0x040003E5 RID: 997
		internal const string HttpSlug = "Slug";

		// Token: 0x040003E6 RID: 998
		internal const string MimeAny = "*/*";

		// Token: 0x040003E7 RID: 999
		internal const string MimeApplicationOctetStream = "application/octet-stream";

		// Token: 0x040003E8 RID: 1000
		internal const string MimeApplicationType = "application";

		// Token: 0x040003E9 RID: 1001
		internal const string MimeJsonSubType = "json";

		// Token: 0x040003EA RID: 1002
		internal const string MimeXmlSubType = "xml";

		// Token: 0x040003EB RID: 1003
		internal const string MimeODataParameterName = "odata.metadata";

		// Token: 0x040003EC RID: 1004
		internal const string MimeMultiPartMixed = "multipart/mixed";

		// Token: 0x040003ED RID: 1005
		internal const string MimeTextPlain = "text/plain";

		// Token: 0x040003EE RID: 1006
		internal const string MimeTextType = "text";

		// Token: 0x040003EF RID: 1007
		internal const string MimeTextXml = "text/xml";

		// Token: 0x040003F0 RID: 1008
		internal const string BatchRequestContentTransferEncoding = "binary";

		// Token: 0x040003F1 RID: 1009
		internal const string MimeTypeUtf8Encoding = ";charset=UTF-8";

		// Token: 0x040003F2 RID: 1010
		internal const string UriHttpAbsolutePrefix = "http://host";

		// Token: 0x040003F3 RID: 1011
		internal const string UriMetadataSegment = "$metadata";

		// Token: 0x040003F4 RID: 1012
		internal const string UriValueSegment = "$value";

		// Token: 0x040003F5 RID: 1013
		internal const string UriBatchSegment = "$batch";

		// Token: 0x040003F6 RID: 1014
		internal const string UriLinkSegment = "$ref";

		// Token: 0x040003F7 RID: 1015
		internal const string UriCountSegment = "$count";

		// Token: 0x040003F8 RID: 1016
		internal const string UriCountTrueOption = "true";

		// Token: 0x040003F9 RID: 1017
		internal const string UriCountFalseOption = "false";

		// Token: 0x040003FA RID: 1018
		internal const string UriFilterSegment = "$filter";

		// Token: 0x040003FB RID: 1019
		internal const string AnyMethodName = "any";

		// Token: 0x040003FC RID: 1020
		internal const string AllMethodName = "all";

		// Token: 0x040003FD RID: 1021
		internal const string ImplicitFilterParameter = "$it";

		// Token: 0x040003FE RID: 1022
		internal const string WcfBinaryElementName = "Binary";

		// Token: 0x040003FF RID: 1023
		internal const string AtomNamespacePrefix = "atom";

		// Token: 0x04000400 RID: 1024
		internal const string AtomContentElementName = "content";

		// Token: 0x04000401 RID: 1025
		internal const string AtomEntryElementName = "entry";

		// Token: 0x04000402 RID: 1026
		internal const string AtomFeedElementName = "feed";

		// Token: 0x04000403 RID: 1027
		internal const string AtomAuthorElementName = "author";

		// Token: 0x04000404 RID: 1028
		internal const string AtomContributorElementName = "contributor";

		// Token: 0x04000405 RID: 1029
		internal const string AtomCategoryElementName = "category";

		// Token: 0x04000406 RID: 1030
		internal const string AtomLinkElementName = "link";

		// Token: 0x04000407 RID: 1031
		internal const string AtomCategorySchemeAttributeName = "scheme";

		// Token: 0x04000408 RID: 1032
		internal const string AtomCategoryTermAttributeName = "term";

		// Token: 0x04000409 RID: 1033
		internal const string AtomIdElementName = "id";

		// Token: 0x0400040A RID: 1034
		internal const string AtomLinkRelationAttributeName = "rel";

		// Token: 0x0400040B RID: 1035
		internal const string AtomContentSrcAttributeName = "src";

		// Token: 0x0400040C RID: 1036
		internal const string AtomLinkNextAttributeString = "next";

		// Token: 0x0400040D RID: 1037
		internal const string SyndAuthorEmail = "SyndicationAuthorEmail";

		// Token: 0x0400040E RID: 1038
		internal const string SyndAuthorName = "SyndicationAuthorName";

		// Token: 0x0400040F RID: 1039
		internal const string SyndAuthorUri = "SyndicationAuthorUri";

		// Token: 0x04000410 RID: 1040
		internal const string SyndPublished = "SyndicationPublished";

		// Token: 0x04000411 RID: 1041
		internal const string SyndRights = "SyndicationRights";

		// Token: 0x04000412 RID: 1042
		internal const string SyndSummary = "SyndicationSummary";

		// Token: 0x04000413 RID: 1043
		internal const string SyndTitle = "SyndicationTitle";

		// Token: 0x04000414 RID: 1044
		internal const string AtomUpdatedElementName = "updated";

		// Token: 0x04000415 RID: 1045
		internal const string SyndContributorEmail = "SyndicationContributorEmail";

		// Token: 0x04000416 RID: 1046
		internal const string SyndContributorName = "SyndicationContributorName";

		// Token: 0x04000417 RID: 1047
		internal const string SyndContributorUri = "SyndicationContributorUri";

		// Token: 0x04000418 RID: 1048
		internal const string SyndUpdated = "SyndicationUpdated";

		// Token: 0x04000419 RID: 1049
		internal const string SyndContentKindPlaintext = "text";

		// Token: 0x0400041A RID: 1050
		internal const string SyndContentKindHtml = "html";

		// Token: 0x0400041B RID: 1051
		internal const string SyndContentKindXHtml = "xhtml";

		// Token: 0x0400041C RID: 1052
		internal const string AtomHRefAttributeName = "href";

		// Token: 0x0400041D RID: 1053
		internal const string AtomHRefLangAttributeName = "hreflang";

		// Token: 0x0400041E RID: 1054
		internal const string AtomSummaryElementName = "summary";

		// Token: 0x0400041F RID: 1055
		internal const string AtomNameElementName = "name";

		// Token: 0x04000420 RID: 1056
		internal const string AtomEmailElementName = "email";

		// Token: 0x04000421 RID: 1057
		internal const string AtomPublishedElementName = "published";

		// Token: 0x04000422 RID: 1058
		internal const string AtomRightsElementName = "rights";

		// Token: 0x04000423 RID: 1059
		internal const string AtomPublishingCollectionElementName = "collection";

		// Token: 0x04000424 RID: 1060
		internal const string AtomPublishingServiceElementName = "service";

		// Token: 0x04000425 RID: 1061
		internal const string AtomPublishingWorkspaceDefaultValue = "Default";

		// Token: 0x04000426 RID: 1062
		internal const string AtomPublishingWorkspaceElementName = "workspace";

		// Token: 0x04000427 RID: 1063
		internal const string AtomTitleElementName = "title";

		// Token: 0x04000428 RID: 1064
		internal const string AtomTypeAttributeName = "type";

		// Token: 0x04000429 RID: 1065
		internal const string AtomSelfRelationAttributeValue = "self";

		// Token: 0x0400042A RID: 1066
		internal const string AtomEditRelationAttributeValue = "edit";

		// Token: 0x0400042B RID: 1067
		internal const string AtomEditMediaRelationAttributeValue = "edit-media";

		// Token: 0x0400042C RID: 1068
		internal const string AtomAlternateRelationAttributeValue = "alternate";

		// Token: 0x0400042D RID: 1069
		internal const string AtomRelatedRelationAttributeValue = "related";

		// Token: 0x0400042E RID: 1070
		internal const string AtomEnclosureRelationAttributeValue = "enclosure";

		// Token: 0x0400042F RID: 1071
		internal const string AtomViaRelationAttributeValue = "via";

		// Token: 0x04000430 RID: 1072
		internal const string AtomDescribedByRelationAttributeValue = "describedby";

		// Token: 0x04000431 RID: 1073
		internal const string AtomServiceRelationAttributeValue = "service";

		// Token: 0x04000432 RID: 1074
		internal const string AtomNullAttributeName = "null";

		// Token: 0x04000433 RID: 1075
		internal const string AtomETagAttributeName = "etag";

		// Token: 0x04000434 RID: 1076
		internal const string AtomInlineElementName = "inline";

		// Token: 0x04000435 RID: 1077
		internal const string AtomPropertiesElementName = "properties";

		// Token: 0x04000436 RID: 1078
		internal const string RowCountElement = "count";

		// Token: 0x04000437 RID: 1079
		internal const string XmlCollectionItemElementName = "element";

		// Token: 0x04000438 RID: 1080
		internal const string XmlErrorElementName = "error";

		// Token: 0x04000439 RID: 1081
		internal const string XmlErrorCodeElementName = "code";

		// Token: 0x0400043A RID: 1082
		internal const string XmlErrorInnerElementName = "innererror";

		// Token: 0x0400043B RID: 1083
		internal const string XmlErrorInternalExceptionElementName = "internalexception";

		// Token: 0x0400043C RID: 1084
		internal const string XmlErrorTypeElementName = "type";

		// Token: 0x0400043D RID: 1085
		internal const string XmlErrorStackTraceElementName = "stacktrace";

		// Token: 0x0400043E RID: 1086
		internal const string XmlErrorMessageElementName = "message";

		// Token: 0x0400043F RID: 1087
		internal const string XmlFalseLiteral = "false";

		// Token: 0x04000440 RID: 1088
		internal const string XmlTrueLiteral = "true";

		// Token: 0x04000441 RID: 1089
		internal const string XmlBaseAttributeName = "base";

		// Token: 0x04000442 RID: 1090
		internal const string XmlSpaceAttributeName = "space";

		// Token: 0x04000443 RID: 1091
		internal const string XmlSpacePreserveValue = "preserve";

		// Token: 0x04000444 RID: 1092
		internal const string XmlBaseAttributeNameWithPrefix = "xml:base";

		// Token: 0x04000445 RID: 1093
		internal const string EdmOasisNamespace = "http://docs.oasis-open.org/odata/ns/edm";

		// Token: 0x04000446 RID: 1094
		internal const string DataWebNamespace = "http://docs.oasis-open.org/odata/ns/data";

		// Token: 0x04000447 RID: 1095
		internal const string DataWebMetadataNamespace = "http://docs.oasis-open.org/odata/ns/metadata";

		// Token: 0x04000448 RID: 1096
		internal const string DataWebRelatedNamespace = "http://docs.oasis-open.org/odata/ns/related/";

		// Token: 0x04000449 RID: 1097
		internal const string DataWebRelatedLinkNamespace = "http://docs.oasis-open.org/odata/ns/relatedlinks/";

		// Token: 0x0400044A RID: 1098
		internal const string DataWebMediaResourceNamespace = "http://docs.oasis-open.org/odata/ns/mediaresource/";

		// Token: 0x0400044B RID: 1099
		internal const string DataWebMediaResourceEditNamespace = "http://docs.oasis-open.org/odata/ns/edit-media/";

		// Token: 0x0400044C RID: 1100
		internal const string DataWebSchemeNamespace = "http://docs.oasis-open.org/odata/ns/scheme";

		// Token: 0x0400044D RID: 1101
		internal const string AppNamespace = "http://www.w3.org/2007/app";

		// Token: 0x0400044E RID: 1102
		internal const string AtomNamespace = "http://www.w3.org/2005/Atom";

		// Token: 0x0400044F RID: 1103
		internal const string XmlnsNamespacePrefix = "xmlns";

		// Token: 0x04000450 RID: 1104
		internal const string XmlNamespacePrefix = "xml";

		// Token: 0x04000451 RID: 1105
		internal const string DataWebNamespacePrefix = "d";

		// Token: 0x04000452 RID: 1106
		internal const string DataWebMetadataNamespacePrefix = "m";

		// Token: 0x04000453 RID: 1107
		internal const string XmlNamespacesNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000454 RID: 1108
		internal const string EdmxNamespace = "http://docs.oasis-open.org/odata/ns/edmx";

		// Token: 0x04000455 RID: 1109
		internal const string EdmxNamespacePrefix = "edmx";

		// Token: 0x04000456 RID: 1110
		internal const string IanaLinkRelationsNamespace = "http://www.iana.org/assignments/relation/";

		// Token: 0x04000457 RID: 1111
		internal const string EmptyNamespace = "";

		// Token: 0x04000458 RID: 1112
		internal const string Association = "Association";

		// Token: 0x04000459 RID: 1113
		internal const string AssociationSet = "AssociationSet";

		// Token: 0x0400045A RID: 1114
		internal const string ComplexType = "ComplexType";

		// Token: 0x0400045B RID: 1115
		internal const string Dependent = "Dependent";

		// Token: 0x0400045C RID: 1116
		internal const string EdmCollectionTypeName = "Collection";

		// Token: 0x0400045D RID: 1117
		internal const string ActualEdmType = "ActualEdmType";

		// Token: 0x0400045E RID: 1118
		internal const string EdmTypeRefElementName = "TypeRef";

		// Token: 0x0400045F RID: 1119
		internal const string EdmEntitySetAttributeName = "EntitySet";

		// Token: 0x04000460 RID: 1120
		internal const string EdmEntitySetPathAttributeName = "EntitySetPath";

		// Token: 0x04000461 RID: 1121
		internal const string EdmBindableAttributeName = "Bindable";

		// Token: 0x04000462 RID: 1122
		internal const string EdmComposableAttributeName = "Composable";

		// Token: 0x04000463 RID: 1123
		internal const string EdmSideEffectingAttributeName = "SideEffecting";

		// Token: 0x04000464 RID: 1124
		internal const string EdmFunctionImportElementName = "FunctionImport";

		// Token: 0x04000465 RID: 1125
		internal const string EdmModeAttributeName = "Mode";

		// Token: 0x04000466 RID: 1126
		internal const string EdmModeInValue = "In";

		// Token: 0x04000467 RID: 1127
		internal const string EdmParameterElementName = "Parameter";

		// Token: 0x04000468 RID: 1128
		internal const string EdmReturnTypeAttributeName = "ReturnType";

		// Token: 0x04000469 RID: 1129
		internal const string ActualReturnTypeAttributeName = "ActualReturnType";

		// Token: 0x0400046A RID: 1130
		internal const string End = "End";

		// Token: 0x0400046B RID: 1131
		internal const string EntityType = "EntityType";

		// Token: 0x0400046C RID: 1132
		internal const string EntityContainer = "EntityContainer";

		// Token: 0x0400046D RID: 1133
		internal const string Key = "Key";

		// Token: 0x0400046E RID: 1134
		internal const string NavigationProperty = "NavigationProperty";

		// Token: 0x0400046F RID: 1135
		internal const string OnDelete = "OnDelete";

		// Token: 0x04000470 RID: 1136
		internal const string Principal = "Principal";

		// Token: 0x04000471 RID: 1137
		internal const string Property = "Property";

		// Token: 0x04000472 RID: 1138
		internal const string PropertyRef = "PropertyRef";

		// Token: 0x04000473 RID: 1139
		internal const string ReferentialConstraint = "ReferentialConstraint";

		// Token: 0x04000474 RID: 1140
		internal const string Role = "Role";

		// Token: 0x04000475 RID: 1141
		internal const string Schema = "Schema";

		// Token: 0x04000476 RID: 1142
		internal const string EdmxElement = "Edmx";

		// Token: 0x04000477 RID: 1143
		internal const string EdmxDataServicesElement = "DataServices";

		// Token: 0x04000478 RID: 1144
		internal const string EdmxVersion = "Version";

		// Token: 0x04000479 RID: 1145
		internal const string EdmxVersionValue = "4.0";

		// Token: 0x0400047A RID: 1146
		internal const string ActionElementName = "action";

		// Token: 0x0400047B RID: 1147
		internal const string FunctionElementName = "function";

		// Token: 0x0400047C RID: 1148
		internal const string ActionMetadataAttributeName = "metadata";

		// Token: 0x0400047D RID: 1149
		internal const string ActionTargetAttributeName = "target";

		// Token: 0x0400047E RID: 1150
		internal const string ActionTitleAttributeName = "title";

		// Token: 0x0400047F RID: 1151
		internal const string BaseType = "BaseType";

		// Token: 0x04000480 RID: 1152
		internal const string EntitySet = "EntitySet";

		// Token: 0x04000481 RID: 1153
		internal const string EntitySetPath = "EntitySetPath";

		// Token: 0x04000482 RID: 1154
		internal const string FromRole = "FromRole";

		// Token: 0x04000483 RID: 1155
		internal const string Abstract = "Abstract";

		// Token: 0x04000484 RID: 1156
		internal const string Multiplicity = "Multiplicity";

		// Token: 0x04000485 RID: 1157
		internal const string Name = "Name";

		// Token: 0x04000486 RID: 1158
		internal const string Namespace = "Namespace";

		// Token: 0x04000487 RID: 1159
		internal const string ToRole = "ToRole";

		// Token: 0x04000488 RID: 1160
		internal const string Type = "Type";

		// Token: 0x04000489 RID: 1161
		internal const string Relationship = "Relationship";

		// Token: 0x0400048A RID: 1162
		internal const string Many = "*";

		// Token: 0x0400048B RID: 1163
		internal const string One = "1";

		// Token: 0x0400048C RID: 1164
		internal const string ZeroOrOne = "0..1";

		// Token: 0x0400048D RID: 1165
		internal const string CsdlNullableAttributeName = "Nullable";

		// Token: 0x0400048E RID: 1166
		internal const string CsdlPrecisionAttributeName = "Precision";

		// Token: 0x0400048F RID: 1167
		internal const string CsdlScaleAttributeName = "Scale";

		// Token: 0x04000490 RID: 1168
		internal const string CsdlMaxLengthAttributeName = "MaxLength";

		// Token: 0x04000491 RID: 1169
		internal const string CsdlFixedLengthAttributeName = "FixedLength";

		// Token: 0x04000492 RID: 1170
		internal const string CsdlUnicodeAttributeName = "Unicode";

		// Token: 0x04000493 RID: 1171
		internal const string CsdlCollationAttributeName = "Collation";

		// Token: 0x04000494 RID: 1172
		internal const string CsdlSridAttributeName = "SRID";

		// Token: 0x04000495 RID: 1173
		internal const string CsdlDefaultValueAttributeName = "DefaultValue";

		// Token: 0x04000496 RID: 1174
		internal const string CsdlMaxLengthAttributeMaxValue = "Max";

		// Token: 0x04000497 RID: 1175
		internal const string DataWebMimeTypeAttributeName = "MimeType";

		// Token: 0x04000498 RID: 1176
		internal const string DataWebOpenTypeAttributeName = "OpenType";

		// Token: 0x04000499 RID: 1177
		internal const string DataWebAccessHasStreamAttribute = "HasStream";

		// Token: 0x0400049A RID: 1178
		internal const string DataWebAccessDefaultStreamPropertyValue = "true";

		// Token: 0x0400049B RID: 1179
		internal const string ServiceOperationHttpMethodName = "HttpMethod";

		// Token: 0x0400049C RID: 1180
		internal const string NextElementName = "next";

		// Token: 0x0400049D RID: 1181
		internal const string JsonError = "error";

		// Token: 0x0400049E RID: 1182
		internal const string JsonErrorCode = "code";

		// Token: 0x0400049F RID: 1183
		internal const string JsonErrorInner = "innererror";

		// Token: 0x040004A0 RID: 1184
		internal const string JsonErrorInternalException = "internalexception";

		// Token: 0x040004A1 RID: 1185
		internal const string JsonErrorMessage = "message";

		// Token: 0x040004A2 RID: 1186
		internal const string JsonErrorStackTrace = "stacktrace";

		// Token: 0x040004A3 RID: 1187
		internal const string JsonErrorType = "type";

		// Token: 0x040004A4 RID: 1188
		internal const string JsonErrorValue = "value";

		// Token: 0x040004A5 RID: 1189
		internal const string EdmNamespace = "Edm";

		// Token: 0x040004A6 RID: 1190
		internal const string EdmBinaryTypeName = "Edm.Binary";

		// Token: 0x040004A7 RID: 1191
		internal const string EdmBooleanTypeName = "Edm.Boolean";

		// Token: 0x040004A8 RID: 1192
		internal const string EdmByteTypeName = "Edm.Byte";

		// Token: 0x040004A9 RID: 1193
		internal const string EdmDecimalTypeName = "Edm.Decimal";

		// Token: 0x040004AA RID: 1194
		internal const string EdmDateTypeName = "Edm.Date";

		// Token: 0x040004AB RID: 1195
		internal const string EdmDoubleTypeName = "Edm.Double";

		// Token: 0x040004AC RID: 1196
		internal const string EdmGuidTypeName = "Edm.Guid";

		// Token: 0x040004AD RID: 1197
		internal const string EdmSingleTypeName = "Edm.Single";

		// Token: 0x040004AE RID: 1198
		internal const string EdmSByteTypeName = "Edm.SByte";

		// Token: 0x040004AF RID: 1199
		internal const string EdmInt16TypeName = "Edm.Int16";

		// Token: 0x040004B0 RID: 1200
		internal const string EdmInt32TypeName = "Edm.Int32";

		// Token: 0x040004B1 RID: 1201
		internal const string EdmInt64TypeName = "Edm.Int64";

		// Token: 0x040004B2 RID: 1202
		internal const string EdmStringTypeName = "Edm.String";

		// Token: 0x040004B3 RID: 1203
		internal const string EdmStreamTypeName = "Edm.Stream";

		// Token: 0x040004B4 RID: 1204
		internal const string EdmTimeOfDayTypeName = "Edm.TimeOfDay";

		// Token: 0x040004B5 RID: 1205
		internal const string CollectionTypeQualifier = "Collection";

		// Token: 0x040004B6 RID: 1206
		internal const string EdmGeographyTypeName = "Edm.Geography";

		// Token: 0x040004B7 RID: 1207
		internal const string EdmPointTypeName = "Edm.GeographyPoint";

		// Token: 0x040004B8 RID: 1208
		internal const string EdmLineStringTypeName = "Edm.GeographyLineString";

		// Token: 0x040004B9 RID: 1209
		internal const string EdmPolygonTypeName = "Edm.GeographyPolygon";

		// Token: 0x040004BA RID: 1210
		internal const string EdmGeographyCollectionTypeName = "Edm.GeographyCollection";

		// Token: 0x040004BB RID: 1211
		internal const string EdmMultiPolygonTypeName = "Edm.GeographyMultiPolygon";

		// Token: 0x040004BC RID: 1212
		internal const string EdmMultiLineStringTypeName = "Edm.GeographyMultiLineString";

		// Token: 0x040004BD RID: 1213
		internal const string EdmMultiPointTypeName = "Edm.GeographyMultiPoint";

		// Token: 0x040004BE RID: 1214
		internal const string EdmGeometryTypeName = "Edm.Geometry";

		// Token: 0x040004BF RID: 1215
		internal const string EdmGeometryPointTypeName = "Edm.GeometryPoint";

		// Token: 0x040004C0 RID: 1216
		internal const string EdmGeometryLineStringTypeName = "Edm.GeometryLineString";

		// Token: 0x040004C1 RID: 1217
		internal const string EdmGeometryPolygonTypeName = "Edm.GeometryPolygon";

		// Token: 0x040004C2 RID: 1218
		internal const string EdmGeometryCollectionTypeName = "Edm.GeometryCollection";

		// Token: 0x040004C3 RID: 1219
		internal const string EdmGeometryMultiPolygonTypeName = "Edm.GeometryMultiPolygon";

		// Token: 0x040004C4 RID: 1220
		internal const string EdmGeometryMultiLineStringTypeName = "Edm.GeometryMultiLineString";

		// Token: 0x040004C5 RID: 1221
		internal const string EdmGeometryMultiPointTypeName = "Edm.GeometryMultiPoint";

		// Token: 0x040004C6 RID: 1222
		internal const string EdmDurationTypeName = "Edm.Duration";

		// Token: 0x040004C7 RID: 1223
		internal const string EdmDateTimeOffsetTypeName = "Edm.DateTimeOffset";

		// Token: 0x040004C8 RID: 1224
		internal const string ODataVersion4Dot0 = "4.0";

		// Token: 0x040004C9 RID: 1225
		internal const string LiteralPrefixBinary = "binary";

		// Token: 0x040004CA RID: 1226
		internal const string LiteralPrefixGeography = "geography";

		// Token: 0x040004CB RID: 1227
		internal const string LiteralPrefixGeometry = "geometry";

		// Token: 0x040004CC RID: 1228
		internal const string LiteralPrefixDuration = "duration";

		// Token: 0x040004CD RID: 1229
		internal const string LiteralSuffixDecimal = "M";

		// Token: 0x040004CE RID: 1230
		internal const string LiteralSuffixInt64 = "L";

		// Token: 0x040004CF RID: 1231
		internal const string LiteralSuffixSingle = "f";

		// Token: 0x040004D0 RID: 1232
		internal const string LiteralSuffixDouble = "D";

		// Token: 0x040004D1 RID: 1233
		internal const string NullLiteralInETag = "null";

		// Token: 0x040004D2 RID: 1234
		internal const string MicrosoftDataServicesRequestUri = "MicrosoftDataServicesRequestUri";

		// Token: 0x040004D3 RID: 1235
		internal const string MicrosoftDataServicesRootUri = "MicrosoftDataServicesRootUri";

		// Token: 0x040004D4 RID: 1236
		internal const string GeoRssNamespace = "http://www.georss.org/georss";

		// Token: 0x040004D5 RID: 1237
		internal const string GeoRssPrefix = "georss";

		// Token: 0x040004D6 RID: 1238
		internal const string GmlNamespace = "http://www.opengis.net/gml";

		// Token: 0x040004D7 RID: 1239
		internal const string GmlPrefix = "gml";

		// Token: 0x040004D8 RID: 1240
		internal const string GeoRssWhere = "where";

		// Token: 0x040004D9 RID: 1241
		internal const string GeoRssPoint = "point";

		// Token: 0x040004DA RID: 1242
		internal const string GeoRssLine = "line";

		// Token: 0x040004DB RID: 1243
		internal const string GmlPosition = "pos";

		// Token: 0x040004DC RID: 1244
		internal const string GmlPositionList = "posList";

		// Token: 0x040004DD RID: 1245
		internal const string GmlLineString = "LineString";
	}
}
