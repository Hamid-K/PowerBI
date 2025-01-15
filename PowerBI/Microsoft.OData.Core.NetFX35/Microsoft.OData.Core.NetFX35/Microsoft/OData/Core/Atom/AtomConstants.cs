using System;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000010 RID: 16
	internal static class AtomConstants
	{
		// Token: 0x0400001D RID: 29
		internal const string XmlNamespacesNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x0400001E RID: 30
		internal const string XmlNamespace = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x0400001F RID: 31
		internal const string XmlnsNamespacePrefix = "xmlns";

		// Token: 0x04000020 RID: 32
		internal const string XmlNamespacePrefix = "xml";

		// Token: 0x04000021 RID: 33
		internal const string XmlBaseAttributeName = "base";

		// Token: 0x04000022 RID: 34
		internal const string XmlSpaceAttributeName = "space";

		// Token: 0x04000023 RID: 35
		internal const string XmlPreserveSpaceAttributeValue = "preserve";

		// Token: 0x04000024 RID: 36
		internal const string ODataMetadataNamespace = "http://docs.oasis-open.org/odata/ns/metadata";

		// Token: 0x04000025 RID: 37
		internal const string ODataMetadataNamespacePrefix = "m";

		// Token: 0x04000026 RID: 38
		internal const string ODataNamespace = "http://docs.oasis-open.org/odata/ns/data";

		// Token: 0x04000027 RID: 39
		internal const string ODataNamespacePrefix = "d";

		// Token: 0x04000028 RID: 40
		internal const string ODataETagAttributeName = "etag";

		// Token: 0x04000029 RID: 41
		internal const string ODataNullAttributeName = "null";

		// Token: 0x0400002A RID: 42
		internal const string ODataCountElementName = "count";

		// Token: 0x0400002B RID: 43
		internal const string ODataValueElementName = "value";

		// Token: 0x0400002C RID: 44
		internal const string ODataSchemeNamespace = "http://docs.oasis-open.org/odata/ns/scheme";

		// Token: 0x0400002D RID: 45
		internal const string ODataStreamPropertyMediaResourceRelatedLinkRelationPrefix = "http://docs.oasis-open.org/odata/ns/mediaresource/";

		// Token: 0x0400002E RID: 46
		internal const string ODataStreamPropertyEditMediaRelatedLinkRelationPrefix = "http://docs.oasis-open.org/odata/ns/edit-media/";

		// Token: 0x0400002F RID: 47
		internal const string ODataNavigationPropertiesRelatedLinkRelationPrefix = "http://docs.oasis-open.org/odata/ns/related/";

		// Token: 0x04000030 RID: 48
		internal const string ODataNavigationPropertiesAssociationLinkRelationPrefix = "http://docs.oasis-open.org/odata/ns/relatedlinks/";

		// Token: 0x04000031 RID: 49
		internal const string ODataInlineElementName = "inline";

		// Token: 0x04000032 RID: 50
		internal const string ODataErrorElementName = "error";

		// Token: 0x04000033 RID: 51
		internal const string ODataErrorCodeElementName = "code";

		// Token: 0x04000034 RID: 52
		internal const string ODataErrorMessageElementName = "message";

		// Token: 0x04000035 RID: 53
		internal const string ODataInnerErrorElementName = "innererror";

		// Token: 0x04000036 RID: 54
		internal const string ODataInnerErrorMessageElementName = "message";

		// Token: 0x04000037 RID: 55
		internal const string ODataInnerErrorTypeElementName = "type";

		// Token: 0x04000038 RID: 56
		internal const string ODataInnerErrorStackTraceElementName = "stacktrace";

		// Token: 0x04000039 RID: 57
		internal const string ODataInnerErrorInnerErrorElementName = "internalexception";

		// Token: 0x0400003A RID: 58
		internal const string ODataCollectionItemElementName = "element";

		// Token: 0x0400003B RID: 59
		internal const string ODataActionElementName = "action";

		// Token: 0x0400003C RID: 60
		internal const string ODataFunctionElementName = "function";

		// Token: 0x0400003D RID: 61
		internal const string ODataOperationMetadataAttribute = "metadata";

		// Token: 0x0400003E RID: 62
		internal const string ODataOperationTitleAttribute = "title";

		// Token: 0x0400003F RID: 63
		internal const string ODataOperationTargetAttribute = "target";

		// Token: 0x04000040 RID: 64
		internal const string ODataRefElementName = "ref";

		// Token: 0x04000041 RID: 65
		internal const string ODataIdElementName = "id";

		// Token: 0x04000042 RID: 66
		internal const string ODataNextLinkElementName = "next";

		// Token: 0x04000043 RID: 67
		internal const string ODataAnnotationElementName = "annotation";

		// Token: 0x04000044 RID: 68
		internal const string ODataAnnotationTargetAttribute = "target";

		// Token: 0x04000045 RID: 69
		internal const string ODataAnnotationTermAttribute = "term";

		// Token: 0x04000046 RID: 70
		internal const string ODataAnnotationStringAttribute = "string";

		// Token: 0x04000047 RID: 71
		internal const string ODataAnnotationBoolAttribute = "bool";

		// Token: 0x04000048 RID: 72
		internal const string ODataAnnotationDecimalAttribute = "decimal";

		// Token: 0x04000049 RID: 73
		internal const string ODataAnnotationIntAttribute = "int";

		// Token: 0x0400004A RID: 74
		internal const string ODataAnnotationFloatAttribute = "float";

		// Token: 0x0400004B RID: 75
		internal const string ODataNameAttribute = "name";

		// Token: 0x0400004C RID: 76
		internal const string AtomNamespace = "http://www.w3.org/2005/Atom";

		// Token: 0x0400004D RID: 77
		internal const string AtomNamespacePrefix = "";

		// Token: 0x0400004E RID: 78
		internal const string NonEmptyAtomNamespacePrefix = "atom";

		// Token: 0x0400004F RID: 79
		internal const string AtomEntryElementName = "entry";

		// Token: 0x04000050 RID: 80
		internal const string AtomFeedElementName = "feed";

		// Token: 0x04000051 RID: 81
		internal const string AtomContentElementName = "content";

		// Token: 0x04000052 RID: 82
		internal const string AtomTypeAttributeName = "type";

		// Token: 0x04000053 RID: 83
		internal const string AtomPropertiesElementName = "properties";

		// Token: 0x04000054 RID: 84
		internal const string AtomIdElementName = "id";

		// Token: 0x04000055 RID: 85
		internal const string AtomTransientIdFormat = "odata:transient:{{{0}}}";

		// Token: 0x04000056 RID: 86
		internal const string AtomTransientIdRegularExpression = "^odata:transient:{([\\s\\S]*)}$";

		// Token: 0x04000057 RID: 87
		internal const string AtomTitleElementName = "title";

		// Token: 0x04000058 RID: 88
		internal const string AtomSubtitleElementName = "subtitle";

		// Token: 0x04000059 RID: 89
		internal const string AtomSummaryElementName = "summary";

		// Token: 0x0400005A RID: 90
		internal const string AtomPublishedElementName = "published";

		// Token: 0x0400005B RID: 91
		internal const string AtomSourceElementName = "source";

		// Token: 0x0400005C RID: 92
		internal const string AtomRightsElementName = "rights";

		// Token: 0x0400005D RID: 93
		internal const string AtomLogoElementName = "logo";

		// Token: 0x0400005E RID: 94
		internal const string AtomAuthorElementName = "author";

		// Token: 0x0400005F RID: 95
		internal const string AtomAuthorNameElementName = "name";

		// Token: 0x04000060 RID: 96
		internal const string AtomContributorElementName = "contributor";

		// Token: 0x04000061 RID: 97
		internal const string AtomGeneratorElementName = "generator";

		// Token: 0x04000062 RID: 98
		internal const string AtomGeneratorUriAttributeName = "uri";

		// Token: 0x04000063 RID: 99
		internal const string AtomGeneratorVersionAttributeName = "version";

		// Token: 0x04000064 RID: 100
		internal const string AtomIconElementName = "icon";

		// Token: 0x04000065 RID: 101
		internal const string AtomPersonNameElementName = "name";

		// Token: 0x04000066 RID: 102
		internal const string AtomPersonUriElementName = "uri";

		// Token: 0x04000067 RID: 103
		internal const string AtomPersonEmailElementName = "email";

		// Token: 0x04000068 RID: 104
		internal const string AtomServiceDocumentSingletonElementName = "singleton";

		// Token: 0x04000069 RID: 105
		internal const string AtomServiceDocumentFunctionImportElementName = "function-import";

		// Token: 0x0400006A RID: 106
		internal const string AtomUpdatedElementName = "updated";

		// Token: 0x0400006B RID: 107
		internal const string AtomCategoryElementName = "category";

		// Token: 0x0400006C RID: 108
		internal const string AtomCategoryTermAttributeName = "term";

		// Token: 0x0400006D RID: 109
		internal const string AtomCategorySchemeAttributeName = "scheme";

		// Token: 0x0400006E RID: 110
		internal const string AtomCategoryLabelAttributeName = "label";

		// Token: 0x0400006F RID: 111
		internal const string AtomEditRelationAttributeValue = "edit";

		// Token: 0x04000070 RID: 112
		internal const string AtomSelfRelationAttributeValue = "self";

		// Token: 0x04000071 RID: 113
		internal const string AtomContextAttributeValue = "context";

		// Token: 0x04000072 RID: 114
		internal const string AtomLinkElementName = "link";

		// Token: 0x04000073 RID: 115
		internal const string AtomLinkRelationAttributeName = "rel";

		// Token: 0x04000074 RID: 116
		internal const string AtomLinkTypeAttributeName = "type";

		// Token: 0x04000075 RID: 117
		internal const string AtomLinkHrefAttributeName = "href";

		// Token: 0x04000076 RID: 118
		internal const string AtomLinkHrefLangAttributeName = "hreflang";

		// Token: 0x04000077 RID: 119
		internal const string AtomLinkTitleAttributeName = "title";

		// Token: 0x04000078 RID: 120
		internal const string AtomLinkLengthAttributeName = "length";

		// Token: 0x04000079 RID: 121
		internal const string AtomHRefAttributeName = "href";

		// Token: 0x0400007A RID: 122
		internal const string MediaLinkEntryContentSourceAttributeName = "src";

		// Token: 0x0400007B RID: 123
		internal const string AtomEditMediaRelationAttributeValue = "edit-media";

		// Token: 0x0400007C RID: 124
		internal const string AtomNextRelationAttributeValue = "next";

		// Token: 0x0400007D RID: 125
		internal const string AtomDeltaRelationAttributeValue = "http://docs.oasis-open.org/odata/ns/delta";

		// Token: 0x0400007E RID: 126
		internal const string AtomAlternateRelationAttributeValue = "alternate";

		// Token: 0x0400007F RID: 127
		internal const string AtomRelatedRelationAttributeValue = "related";

		// Token: 0x04000080 RID: 128
		internal const string AtomEnclosureRelationAttributeValue = "enclosure";

		// Token: 0x04000081 RID: 129
		internal const string AtomViaRelationAttributeValue = "via";

		// Token: 0x04000082 RID: 130
		internal const string AtomDescribedByRelationAttributeValue = "describedby";

		// Token: 0x04000083 RID: 131
		internal const string AtomServiceRelationAttributeValue = "service";

		// Token: 0x04000084 RID: 132
		internal const string AtomTextConstructTextKind = "text";

		// Token: 0x04000085 RID: 133
		internal const string AtomTextConstructHtmlKind = "html";

		// Token: 0x04000086 RID: 134
		internal const string AtomTextConstructXHtmlKind = "xhtml";

		// Token: 0x04000087 RID: 135
		internal const string AtomWorkspaceDefaultTitle = "Default";

		// Token: 0x04000088 RID: 136
		internal const string AtomTrueLiteral = "true";

		// Token: 0x04000089 RID: 137
		internal const string AtomFalseLiteral = "false";

		// Token: 0x0400008A RID: 138
		internal const string IanaLinkRelationsNamespace = "http://www.iana.org/assignments/relation/";

		// Token: 0x0400008B RID: 139
		internal const string AtomPublishingNamespace = "http://www.w3.org/2007/app";

		// Token: 0x0400008C RID: 140
		internal const string AtomPublishingServiceElementName = "service";

		// Token: 0x0400008D RID: 141
		internal const string AtomPublishingWorkspaceElementName = "workspace";

		// Token: 0x0400008E RID: 142
		internal const string AtomPublishingCollectionElementName = "collection";

		// Token: 0x0400008F RID: 143
		internal const string AtomPublishingCategoriesElementName = "categories";

		// Token: 0x04000090 RID: 144
		internal const string AtomPublishingAcceptElementName = "accept";

		// Token: 0x04000091 RID: 145
		internal const string AtomPublishingFixedAttributeName = "fixed";

		// Token: 0x04000092 RID: 146
		internal const string AtomPublishingFixedYesValue = "yes";

		// Token: 0x04000093 RID: 147
		internal const string AtomPublishingFixedNoValue = "no";

		// Token: 0x04000094 RID: 148
		internal const string GeoRssNamespace = "http://www.georss.org/georss";

		// Token: 0x04000095 RID: 149
		internal const string GeoRssPrefix = "georss";

		// Token: 0x04000096 RID: 150
		internal const string GmlNamespace = "http://www.opengis.net/gml";

		// Token: 0x04000097 RID: 151
		internal const string GmlPrefix = "gml";
	}
}
