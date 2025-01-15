using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200031A RID: 794
	// (Invoke) Token: 0x060025CC RID: 9676
	internal delegate DbProviderManifest ProviderManifestNeeded(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError);
}
