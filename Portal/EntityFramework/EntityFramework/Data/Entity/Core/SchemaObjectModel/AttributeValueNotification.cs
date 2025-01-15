using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000319 RID: 793
	// (Invoke) Token: 0x060025C8 RID: 9672
	internal delegate void AttributeValueNotification(string token, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError);
}
