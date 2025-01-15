using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core
{
	// Token: 0x02000158 RID: 344
	public static class ODataConstants
	{
		// Token: 0x0400056A RID: 1386
		public const string MethodGet = "GET";

		// Token: 0x0400056B RID: 1387
		public const string MethodPost = "POST";

		// Token: 0x0400056C RID: 1388
		public const string MethodPut = "PUT";

		// Token: 0x0400056D RID: 1389
		public const string MethodDelete = "DELETE";

		// Token: 0x0400056E RID: 1390
		public const string MethodPatch = "PATCH";

		// Token: 0x0400056F RID: 1391
		public const string ContentTypeHeader = "Content-Type";

		// Token: 0x04000570 RID: 1392
		public const string ODataVersionHeader = "OData-Version";

		// Token: 0x04000571 RID: 1393
		public const string ContentIdHeader = "Content-ID";

		// Token: 0x04000572 RID: 1394
		internal const string ContentLengthHeader = "Content-Length";

		// Token: 0x04000573 RID: 1395
		internal const string HttpQValueParameter = "q";

		// Token: 0x04000574 RID: 1396
		internal const string HttpVersionInBatching = "HTTP/1.1";

		// Token: 0x04000575 RID: 1397
		internal const string HttpVersionInAsync = "HTTP/1.1";

		// Token: 0x04000576 RID: 1398
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Charset", Justification = "Member name chosen based on HTTP header name.")]
		internal const string Charset = "charset";

		// Token: 0x04000577 RID: 1399
		internal const string HttpMultipartBoundary = "boundary";

		// Token: 0x04000578 RID: 1400
		internal const string ContentTransferEncoding = "Content-Transfer-Encoding";

		// Token: 0x04000579 RID: 1401
		internal const string BatchContentTransferEncoding = "binary";

		// Token: 0x0400057A RID: 1402
		internal const ODataVersion ODataDefaultProtocolVersion = ODataVersion.V4;

		// Token: 0x0400057B RID: 1403
		internal const string BatchRequestBoundaryTemplate = "batch_{0}";

		// Token: 0x0400057C RID: 1404
		internal const string BatchResponseBoundaryTemplate = "batchresponse_{0}";

		// Token: 0x0400057D RID: 1405
		internal const string RequestChangeSetBoundaryTemplate = "changeset_{0}";

		// Token: 0x0400057E RID: 1406
		internal const string ResponseChangeSetBoundaryTemplate = "changesetresponse_{0}";

		// Token: 0x0400057F RID: 1407
		internal const string HttpWeakETagPrefix = "W/\"";

		// Token: 0x04000580 RID: 1408
		internal const string HttpWeakETagSuffix = "\"";

		// Token: 0x04000581 RID: 1409
		internal const int DefaultMaxRecursionDepth = 100;

		// Token: 0x04000582 RID: 1410
		internal const long DefaultMaxReadMessageSize = 1048576L;

		// Token: 0x04000583 RID: 1411
		internal const int DefaultMaxPartsPerBatch = 100;

		// Token: 0x04000584 RID: 1412
		internal const int DefulatMaxOperationsPerChangeset = 1000;

		// Token: 0x04000585 RID: 1413
		internal const string UriSegmentSeparator = "/";

		// Token: 0x04000586 RID: 1414
		internal const char UriSegmentSeparatorChar = '/';

		// Token: 0x04000587 RID: 1415
		internal const string EntityReferenceSegmentName = "$ref";

		// Token: 0x04000588 RID: 1416
		internal const string EntityReferenceCollectionSegmentName = "Collection";

		// Token: 0x04000589 RID: 1417
		internal const string DefaultStreamSegmentName = "$value";

		// Token: 0x0400058A RID: 1418
		internal const string TypeNamePrefix = "#";

		// Token: 0x0400058B RID: 1419
		internal const string UriMetadataSegment = "$metadata";

		// Token: 0x0400058C RID: 1420
		internal const string CollectionOfEntityReferencesContextUrlSegment = "#Collection($ref)";

		// Token: 0x0400058D RID: 1421
		internal const string SingleEntityReferencesContextUrlSegment = "#$ref";

		// Token: 0x0400058E RID: 1422
		internal const char ContextUriFragmentIndicator = '#';

		// Token: 0x0400058F RID: 1423
		internal const string ContextUriFragmentItemSelector = "/$entity";

		// Token: 0x04000590 RID: 1424
		internal const char ContextUriProjectionStart = '(';

		// Token: 0x04000591 RID: 1425
		internal const char ContextUriProjectionEnd = ')';

		// Token: 0x04000592 RID: 1426
		internal const string ContextUriProjectionPropertySeparator = ",";

		// Token: 0x04000593 RID: 1427
		internal const string ContextUriFragmentNull = "Edm.Null";

		// Token: 0x04000594 RID: 1428
		internal const string ContextUriFragmentUntyped = "Edm.Untyped";

		// Token: 0x04000595 RID: 1429
		internal const string ContextUriDeltaFeed = "/$delta";

		// Token: 0x04000596 RID: 1430
		internal const string ContextUriDeletedEntry = "/$deletedEntity";

		// Token: 0x04000597 RID: 1431
		internal const string ContextUriDeltaLink = "/$link";

		// Token: 0x04000598 RID: 1432
		internal const string ContextUriDeletedLink = "/$deletedLink";
	}
}
