using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000033 RID: 51
	internal abstract class ModelFunctionTypeElement : FacetEnabledSchemaElement
	{
		// Token: 0x060006EA RID: 1770 RVA: 0x0000CD60 File Offset: 0x0000AF60
		internal ModelFunctionTypeElement(SchemaElement parentElement)
			: base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x060006EB RID: 1771
		internal abstract void WriteIdentity(StringBuilder builder);

		// Token: 0x060006EC RID: 1772
		internal abstract TypeUsage GetTypeUsage();

		// Token: 0x060006ED RID: 1773
		internal abstract bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems);

		// Token: 0x04000668 RID: 1640
		protected TypeUsage _typeUsage;
	}
}
