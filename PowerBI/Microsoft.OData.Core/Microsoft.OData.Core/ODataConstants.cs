using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData
{
	// Token: 0x02000069 RID: 105
	public static class ODataConstants
	{
		// Token: 0x0400018B RID: 395
		public const string MethodGet = "GET";

		// Token: 0x0400018C RID: 396
		public const string MethodPost = "POST";

		// Token: 0x0400018D RID: 397
		public const string MethodPut = "PUT";

		// Token: 0x0400018E RID: 398
		public const string MethodDelete = "DELETE";

		// Token: 0x0400018F RID: 399
		public const string MethodPatch = "PATCH";

		// Token: 0x04000190 RID: 400
		public const string ContentTypeHeader = "Content-Type";

		// Token: 0x04000191 RID: 401
		public const string ODataVersionHeader = "OData-Version";

		// Token: 0x04000192 RID: 402
		public const string ContentIdHeader = "Content-ID";

		// Token: 0x04000193 RID: 403
		internal const string ContentLengthHeader = "Content-Length";

		// Token: 0x04000194 RID: 404
		internal const string HttpQValueParameter = "q";

		// Token: 0x04000195 RID: 405
		internal const string HttpVersionInBatching = "HTTP/1.1";

		// Token: 0x04000196 RID: 406
		internal const string HttpVersionInAsync = "HTTP/1.1";

		// Token: 0x04000197 RID: 407
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Charset", Justification = "Member name chosen based on HTTP header name.")]
		internal const string Charset = "charset";

		// Token: 0x04000198 RID: 408
		internal const string HttpMultipartBoundary = "boundary";

		// Token: 0x04000199 RID: 409
		internal const string ContentTransferEncoding = "Content-Transfer-Encoding";

		// Token: 0x0400019A RID: 410
		internal const string BatchContentTransferEncoding = "binary";

		// Token: 0x0400019B RID: 411
		internal const ODataVersion ODataDefaultProtocolVersion = ODataVersion.V4;

		// Token: 0x0400019C RID: 412
		internal const string BatchRequestBoundaryTemplate = "batch_{0}";

		// Token: 0x0400019D RID: 413
		internal const string BatchResponseBoundaryTemplate = "batchresponse_{0}";

		// Token: 0x0400019E RID: 414
		internal const string RequestChangeSetBoundaryTemplate = "changeset_{0}";

		// Token: 0x0400019F RID: 415
		internal const string ResponseChangeSetBoundaryTemplate = "changesetresponse_{0}";

		// Token: 0x040001A0 RID: 416
		internal const string HttpWeakETagPrefix = "W/\"";

		// Token: 0x040001A1 RID: 417
		internal const string HttpWeakETagSuffix = "\"";

		// Token: 0x040001A2 RID: 418
		internal const int DefaultMaxRecursionDepth = 100;

		// Token: 0x040001A3 RID: 419
		internal const long DefaultMaxReadMessageSize = 1048576L;

		// Token: 0x040001A4 RID: 420
		internal const int DefaultMaxPartsPerBatch = 100;

		// Token: 0x040001A5 RID: 421
		internal const int DefaultMaxOperationsPerChangeset = 1000;

		// Token: 0x040001A6 RID: 422
		internal const string UriSegmentSeparator = "/";

		// Token: 0x040001A7 RID: 423
		internal const char UriSegmentSeparatorChar = '/';

		// Token: 0x040001A8 RID: 424
		internal const string EntityReferenceSegmentName = "$ref";

		// Token: 0x040001A9 RID: 425
		internal const string CollectionPrefix = "Collection";

		// Token: 0x040001AA RID: 426
		internal const string DefaultStreamSegmentName = "$value";

		// Token: 0x040001AB RID: 427
		internal const string TypeNamePrefix = "#";

		// Token: 0x040001AC RID: 428
		internal const string UriMetadataSegment = "$metadata";

		// Token: 0x040001AD RID: 429
		internal const string ODataPrefix = "odata";

		// Token: 0x040001AE RID: 430
		internal const string CollectionOfEntityReferencesContextUrlSegment = "#Collection($ref)";

		// Token: 0x040001AF RID: 431
		internal const string SingleEntityReferencesContextUrlSegment = "#$ref";

		// Token: 0x040001B0 RID: 432
		internal const char ContextUriFragmentIndicator = '#';

		// Token: 0x040001B1 RID: 433
		internal const string ContextUriFragmentItemSelector = "/$entity";

		// Token: 0x040001B2 RID: 434
		internal const char ContextUriProjectionStart = '(';

		// Token: 0x040001B3 RID: 435
		internal const char ContextUriProjectionEnd = ')';

		// Token: 0x040001B4 RID: 436
		internal const string ContextUriProjectionPropertySeparator = ",";

		// Token: 0x040001B5 RID: 437
		internal const string ContextUriFragmentNull = "Edm.Null";

		// Token: 0x040001B6 RID: 438
		internal const string ContextUriFragmentUntyped = "Edm.Untyped";

		// Token: 0x040001B7 RID: 439
		internal const string DeltaResourceSet = "$delta";

		// Token: 0x040001B8 RID: 440
		internal const string ContextUriDeltaResourceSet = "/$delta";

		// Token: 0x040001B9 RID: 441
		internal const string DeletedEntry = "$deletedEntity";

		// Token: 0x040001BA RID: 442
		internal const string ContextUriDeletedEntry = "/$deletedEntity";

		// Token: 0x040001BB RID: 443
		internal const string DeltaLink = "$link";

		// Token: 0x040001BC RID: 444
		internal const string ContextUriDeltaLink = "/$link";

		// Token: 0x040001BD RID: 445
		internal const string DeletedLink = "$deletedLink";

		// Token: 0x040001BE RID: 446
		internal const string ContextUriDeletedLink = "/$deletedLink";
	}
}
