using System;
using System.Data.Entity;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000048 RID: 72
	internal sealed class SchemaComplexType : StructuredType
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x00010879 File Offset: 0x0000EA79
		internal SchemaComplexType(Schema parentElement)
			: base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000108A5 File Offset: 0x0000EAA5
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (base.BaseType != null && !(base.BaseType is SchemaComplexType))
			{
				base.AddError(ErrorCode.InvalidBaseType, EdmSchemaErrorSeverity.Error, Strings.InvalidBaseTypeForNestedType(base.BaseType.FQName, this.FQName));
			}
		}
	}
}
