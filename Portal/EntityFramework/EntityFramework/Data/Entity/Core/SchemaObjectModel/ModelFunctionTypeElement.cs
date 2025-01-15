using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002FC RID: 764
	internal abstract class ModelFunctionTypeElement : FacetEnabledSchemaElement
	{
		// Token: 0x06002455 RID: 9301 RVA: 0x00066D40 File Offset: 0x00064F40
		internal ModelFunctionTypeElement(SchemaElement parentElement)
			: base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x06002456 RID: 9302
		internal abstract void WriteIdentity(StringBuilder builder);

		// Token: 0x06002457 RID: 9303
		internal abstract TypeUsage GetTypeUsage();

		// Token: 0x06002458 RID: 9304
		internal abstract bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems);

		// Token: 0x04000CEC RID: 3308
		protected TypeUsage _typeUsage;
	}
}
