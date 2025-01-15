using System;
using System.Text;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x020000A9 RID: 169
	internal static class UriHelper
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x00016578 File Offset: 0x00014778
		internal static string GetTypeNameForUri(Type type, DataServiceContext context)
		{
			type = Nullable.GetUnderlyingType(type) ?? type;
			PrimitiveType primitiveType;
			if (!PrimitiveType.TryGetPrimitiveType(type, out primitiveType))
			{
				return context.ResolveNameFromTypeInternal(type) ?? ClientTypeUtil.GetServerDefinedTypeFullName(type);
			}
			if (primitiveType.HasReverseMapping)
			{
				return primitiveType.EdmTypeName;
			}
			throw new NotSupportedException(Strings.ALinq_CantCastToUnsupportedPrimitive(type.Name));
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000165D0 File Offset: 0x000147D0
		internal static string GetEntityTypeNameForUriAndValidateMaxProtocolVersion(Type type, DataServiceContext context, ref Version uriVersion)
		{
			if (context.MaxProtocolVersionAsVersion < Util.ODataVersion4)
			{
				throw new NotSupportedException(Strings.ALinq_TypeAsNotSupportedForMaxDataServiceVersionLessThan3);
			}
			if (!ClientTypeUtil.TypeOrElementTypeIsEntity(type))
			{
				throw new NotSupportedException(Strings.ALinq_TypeAsArgumentNotEntityType(type.FullName));
			}
			WebUtil.RaiseVersion(ref uriVersion, Util.ODataVersion4);
			return context.ResolveNameFromTypeInternal(type) ?? ClientTypeUtil.GetServerDefinedTypeFullName(type);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00016630 File Offset: 0x00014830
		internal static void AppendTypeSegment(StringBuilder stringBuilder, Type type, DataServiceContext dataServiceContext, bool inPath, ref Version version)
		{
			if (inPath && dataServiceContext.UrlKeyDelimiter == DataServiceUrlKeyDelimiter.Slash)
			{
				stringBuilder.Append('$');
				stringBuilder.Append('/');
			}
			string typeNameForUri = UriHelper.GetTypeNameForUri(type, dataServiceContext);
			stringBuilder.Append(typeNameForUri);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001666F File Offset: 0x0001486F
		internal static bool IsPrimitiveValue(string value)
		{
			return !value.StartsWith("%7B", StringComparison.Ordinal) && !value.StartsWith("%5B", StringComparison.Ordinal);
		}

		// Token: 0x04000243 RID: 579
		internal const char FORWARDSLASH = '/';

		// Token: 0x04000244 RID: 580
		internal const char LEFTPAREN = '(';

		// Token: 0x04000245 RID: 581
		internal const char RIGHTPAREN = ')';

		// Token: 0x04000246 RID: 582
		internal const char QUESTIONMARK = '?';

		// Token: 0x04000247 RID: 583
		internal const char AMPERSAND = '&';

		// Token: 0x04000248 RID: 584
		internal const char EQUALSSIGN = '=';

		// Token: 0x04000249 RID: 585
		internal const char ATSIGN = '@';

		// Token: 0x0400024A RID: 586
		internal const char DOLLARSIGN = '$';

		// Token: 0x0400024B RID: 587
		internal const char SPACE = ' ';

		// Token: 0x0400024C RID: 588
		internal const char COMMA = ',';

		// Token: 0x0400024D RID: 589
		internal const char COLON = ':';

		// Token: 0x0400024E RID: 590
		internal const char SEMICOLON = ';';

		// Token: 0x0400024F RID: 591
		internal const char QUOTE = '\'';

		// Token: 0x04000250 RID: 592
		internal const char ASTERISK = '*';

		// Token: 0x04000251 RID: 593
		internal const string OPTIONTOP = "top";

		// Token: 0x04000252 RID: 594
		internal const string OPTIONSKIP = "skip";

		// Token: 0x04000253 RID: 595
		internal const string OPTIONORDERBY = "orderby";

		// Token: 0x04000254 RID: 596
		internal const string OPTIONFILTER = "filter";

		// Token: 0x04000255 RID: 597
		internal const string OPTIONDESC = "desc";

		// Token: 0x04000256 RID: 598
		internal const string OPTIONEXPAND = "expand";

		// Token: 0x04000257 RID: 599
		internal const string OPTIONCOUNT = "count";

		// Token: 0x04000258 RID: 600
		internal const string OPTIONSELECT = "select";

		// Token: 0x04000259 RID: 601
		internal const string OPTIONFORMAT = "format";

		// Token: 0x0400025A RID: 602
		internal const string COUNTTRUE = "true";

		// Token: 0x0400025B RID: 603
		internal const string COUNT = "count";

		// Token: 0x0400025C RID: 604
		internal const string AND = "and";

		// Token: 0x0400025D RID: 605
		internal const string OR = "or";

		// Token: 0x0400025E RID: 606
		internal const string EQ = "eq";

		// Token: 0x0400025F RID: 607
		internal const string NE = "ne";

		// Token: 0x04000260 RID: 608
		internal const string LT = "lt";

		// Token: 0x04000261 RID: 609
		internal const string LE = "le";

		// Token: 0x04000262 RID: 610
		internal const string GT = "gt";

		// Token: 0x04000263 RID: 611
		internal const string GE = "ge";

		// Token: 0x04000264 RID: 612
		internal const string ADD = "add";

		// Token: 0x04000265 RID: 613
		internal const string SUB = "sub";

		// Token: 0x04000266 RID: 614
		internal const string MUL = "mul";

		// Token: 0x04000267 RID: 615
		internal const string DIV = "div";

		// Token: 0x04000268 RID: 616
		internal const string MOD = "mod";

		// Token: 0x04000269 RID: 617
		internal const string NEGATE = "-";

		// Token: 0x0400026A RID: 618
		internal const string NOT = "not";

		// Token: 0x0400026B RID: 619
		internal const string NULL = "null";

		// Token: 0x0400026C RID: 620
		internal const string ISOF = "isof";

		// Token: 0x0400026D RID: 621
		internal const string CAST = "cast";

		// Token: 0x0400026E RID: 622
		internal const string HAS = "has";

		// Token: 0x0400026F RID: 623
		internal const string ENCODEDATSIGN = "%40";

		// Token: 0x04000270 RID: 624
		internal const string ENCODEDSQUAREBRACKETSIGN = "%5B";

		// Token: 0x04000271 RID: 625
		internal const string ENCODEDBRACESIGN = "%7B";
	}
}
