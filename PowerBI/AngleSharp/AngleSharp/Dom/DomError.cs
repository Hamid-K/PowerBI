using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200016B RID: 363
	[DomName("DOMError")]
	public enum DomError : byte
	{
		// Token: 0x04000996 RID: 2454
		[DomDescription("The index is not in the allowed range.")]
		[DomName("INDEX_SIZE_ERR")]
		IndexSizeError = 1,
		// Token: 0x04000997 RID: 2455
		[DomDescription("The size of the string is invalid.")]
		[DomName("DOMSTRING_SIZE_ERR")]
		[DomHistorical]
		DomStringSize,
		// Token: 0x04000998 RID: 2456
		[DomDescription("The operation would yield an incorrect node tree.")]
		[DomName("HIERARCHY_REQUEST_ERR")]
		HierarchyRequest,
		// Token: 0x04000999 RID: 2457
		[DomDescription("The object is in the wrong document.")]
		[DomName("WRONG_DOCUMENT_ERR")]
		WrongDocument,
		// Token: 0x0400099A RID: 2458
		[DomDescription("Invalid character detected.")]
		[DomName("INVALID_CHARACTER_ERR")]
		InvalidCharacter,
		// Token: 0x0400099B RID: 2459
		[DomDescription("The data is allowed for this object.")]
		[DomName("NO_DATA_ALLOWED_ERR")]
		[DomHistorical]
		NoDataAllowed,
		// Token: 0x0400099C RID: 2460
		[DomDescription("The object can not be modified.")]
		[DomName("NO_MODIFICATION_ALLOWED_ERR")]
		NoModificationAllowed,
		// Token: 0x0400099D RID: 2461
		[DomDescription("The object can not be found here.")]
		[DomName("NOT_FOUND_ERR")]
		NotFound,
		// Token: 0x0400099E RID: 2462
		[DomDescription("The operation is not supported.")]
		[DomName("NOT_SUPPORTED_ERR")]
		NotSupported,
		// Token: 0x0400099F RID: 2463
		[DomDescription("The element is already in-use.")]
		[DomName("INUSE_ATTRIBUTE_ERR")]
		[DomHistorical]
		InUse,
		// Token: 0x040009A0 RID: 2464
		[DomDescription("The object is in an invalid state.")]
		[DomName("INVALID_STATE_ERR")]
		InvalidState,
		// Token: 0x040009A1 RID: 2465
		[DomDescription("The string did not match the expected pattern.")]
		[DomName("SYNTAX_ERR")]
		Syntax,
		// Token: 0x040009A2 RID: 2466
		[DomDescription("The object can not be modified in this way.")]
		[DomName("INVALID_MODIFICATION_ERR")]
		InvalidModification,
		// Token: 0x040009A3 RID: 2467
		[DomDescription("The operation is not allowed by namespaces in XML.")]
		[DomName("NAMESPACE_ERR")]
		Namespace,
		// Token: 0x040009A4 RID: 2468
		[DomDescription("The object does not support the operation or argument.")]
		[DomName("INVALID_ACCESS_ERR")]
		InvalidAccess,
		// Token: 0x040009A5 RID: 2469
		[DomDescription("The validation failed.")]
		[DomName("VALIDATION_ERR")]
		Validation = 15,
		// Token: 0x040009A6 RID: 2470
		[DomDescription("The provided argument type is invalid.")]
		[DomName("TYPE_MISMATCH_ERR")]
		[DomHistorical]
		TypeMismatch = 17,
		// Token: 0x040009A7 RID: 2471
		[DomDescription("The operation is insecure.")]
		[DomName("SECURITY_ERR")]
		Security,
		// Token: 0x040009A8 RID: 2472
		[DomDescription("A network error occurred.")]
		[DomName("NETWORK_ERR")]
		Network,
		// Token: 0x040009A9 RID: 2473
		[DomDescription("The operation was aborted.")]
		[DomName("ABORT_ERR")]
		Abort,
		// Token: 0x040009AA RID: 2474
		[DomDescription("The given URL does not match another URL.")]
		[DomName("URL_MISMATCH_ERR")]
		UrlMismatch,
		// Token: 0x040009AB RID: 2475
		[DomDescription("The quota has been exceeded.")]
		[DomName("QUOTA_EXCEEDED_ERR")]
		QuotaExceeded,
		// Token: 0x040009AC RID: 2476
		[DomDescription("The operation timed out.")]
		[DomName("TIMEOUT_ERR")]
		Timeout,
		// Token: 0x040009AD RID: 2477
		[DomDescription("The supplied node is incorrect or has an incorrect ancestor for this operation.")]
		[DomName("INVALID_NODE_TYPE_ERR")]
		InvalidNodeType,
		// Token: 0x040009AE RID: 2478
		[DomDescription("The object can not be cloned.")]
		[DomName("DATA_CLONE_ERR")]
		DataClone
	}
}
