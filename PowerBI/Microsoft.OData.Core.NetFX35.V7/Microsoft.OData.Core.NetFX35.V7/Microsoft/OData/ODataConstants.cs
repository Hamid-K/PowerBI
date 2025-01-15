using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData
{
	// Token: 0x02000047 RID: 71
	public static class ODataConstants
	{
		// Token: 0x0400012F RID: 303
		public const string MethodGet = "GET";

		// Token: 0x04000130 RID: 304
		public const string MethodPost = "POST";

		// Token: 0x04000131 RID: 305
		public const string MethodPut = "PUT";

		// Token: 0x04000132 RID: 306
		public const string MethodDelete = "DELETE";

		// Token: 0x04000133 RID: 307
		public const string MethodPatch = "PATCH";

		// Token: 0x04000134 RID: 308
		public const string ContentTypeHeader = "Content-Type";

		// Token: 0x04000135 RID: 309
		public const string ODataVersionHeader = "OData-Version";

		// Token: 0x04000136 RID: 310
		public const string ContentIdHeader = "Content-ID";

		// Token: 0x04000137 RID: 311
		internal const string ContentLengthHeader = "Content-Length";

		// Token: 0x04000138 RID: 312
		internal const string HttpQValueParameter = "q";

		// Token: 0x04000139 RID: 313
		internal const string HttpVersionInBatching = "HTTP/1.1";

		// Token: 0x0400013A RID: 314
		internal const string HttpVersionInAsync = "HTTP/1.1";

		// Token: 0x0400013B RID: 315
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Charset", Justification = "Member name chosen based on HTTP header name.")]
		internal const string Charset = "charset";

		// Token: 0x0400013C RID: 316
		internal const string HttpMultipartBoundary = "boundary";

		// Token: 0x0400013D RID: 317
		internal const string ContentTransferEncoding = "Content-Transfer-Encoding";

		// Token: 0x0400013E RID: 318
		internal const string BatchContentTransferEncoding = "binary";

		// Token: 0x0400013F RID: 319
		internal const ODataVersion ODataDefaultProtocolVersion = ODataVersion.V4;

		// Token: 0x04000140 RID: 320
		internal const string BatchRequestBoundaryTemplate = "batch_{0}";

		// Token: 0x04000141 RID: 321
		internal const string BatchResponseBoundaryTemplate = "batchresponse_{0}";

		// Token: 0x04000142 RID: 322
		internal const string RequestChangeSetBoundaryTemplate = "changeset_{0}";

		// Token: 0x04000143 RID: 323
		internal const string ResponseChangeSetBoundaryTemplate = "changesetresponse_{0}";

		// Token: 0x04000144 RID: 324
		internal const string HttpWeakETagPrefix = "W/\"";

		// Token: 0x04000145 RID: 325
		internal const string HttpWeakETagSuffix = "\"";

		// Token: 0x04000146 RID: 326
		internal const int DefaultMaxRecursionDepth = 100;

		// Token: 0x04000147 RID: 327
		internal const long DefaultMaxReadMessageSize = 1048576L;

		// Token: 0x04000148 RID: 328
		internal const int DefaultMaxPartsPerBatch = 100;

		// Token: 0x04000149 RID: 329
		internal const int DefulatMaxOperationsPerChangeset = 1000;

		// Token: 0x0400014A RID: 330
		internal const string UriSegmentSeparator = "/";

		// Token: 0x0400014B RID: 331
		internal const char UriSegmentSeparatorChar = '/';

		// Token: 0x0400014C RID: 332
		internal const string EntityReferenceSegmentName = "$ref";

		// Token: 0x0400014D RID: 333
		internal const string CollectionPrefix = "Collection";

		// Token: 0x0400014E RID: 334
		internal const string DefaultStreamSegmentName = "$value";

		// Token: 0x0400014F RID: 335
		internal const string TypeNamePrefix = "#";

		// Token: 0x04000150 RID: 336
		internal const string UriMetadataSegment = "$metadata";

		// Token: 0x04000151 RID: 337
		internal const string CollectionOfEntityReferencesContextUrlSegment = "#Collection($ref)";

		// Token: 0x04000152 RID: 338
		internal const string SingleEntityReferencesContextUrlSegment = "#$ref";

		// Token: 0x04000153 RID: 339
		internal const char ContextUriFragmentIndicator = '#';

		// Token: 0x04000154 RID: 340
		internal const string ContextUriFragmentItemSelector = "/$entity";

		// Token: 0x04000155 RID: 341
		internal const char ContextUriProjectionStart = '(';

		// Token: 0x04000156 RID: 342
		internal const char ContextUriProjectionEnd = ')';

		// Token: 0x04000157 RID: 343
		internal const string ContextUriProjectionPropertySeparator = ",";

		// Token: 0x04000158 RID: 344
		internal const string ContextUriFragmentUntyped = "Edm.Untyped";

		// Token: 0x04000159 RID: 345
		internal const string ContextUriDeltaResourceSet = "/$delta";

		// Token: 0x0400015A RID: 346
		internal const string ContextUriDeletedEntry = "/$deletedEntity";

		// Token: 0x0400015B RID: 347
		internal const string ContextUriDeltaLink = "/$link";

		// Token: 0x0400015C RID: 348
		internal const string ContextUriDeletedLink = "/$deletedLink";
	}
}
