using System;
using Microsoft.Data.Common;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000050 RID: 80
	// (Invoke) Token: 0x06000842 RID: 2114
	internal delegate DbProviderManifest ProviderManifestNeeded(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError);
}
