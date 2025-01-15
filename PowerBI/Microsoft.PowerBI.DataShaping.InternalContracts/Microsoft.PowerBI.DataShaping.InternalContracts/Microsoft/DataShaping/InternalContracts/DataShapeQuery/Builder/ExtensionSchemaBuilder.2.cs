using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000E5 RID: 229
	internal class ExtensionSchemaBuilder<TParent> : BuilderBase<ExtensionSchema, TParent>
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x0000DADD File Offset: 0x0000BCDD
		internal ExtensionSchemaBuilder(TParent parent, ExtensionSchema activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0000DAE7 File Offset: 0x0000BCE7
		public ExtensionSchemaBuilder<TParent> WithName(string name)
		{
			base.ActiveObject.Name = name;
			return this;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		public ExtensionEntityBuilder<ExtensionSchemaBuilder<TParent>> WithEntity(string name = null, string extends = null)
		{
			if (base.ActiveObject.Entities == null)
			{
				base.ActiveObject.Entities = new List<ExtensionEntity>();
			}
			ExtensionEntity extensionEntity = new ExtensionEntity();
			base.ActiveObject.Entities.Add(extensionEntity);
			if (name != null)
			{
				extensionEntity.Name = name;
			}
			if (extends != null)
			{
				extensionEntity.Extends = extends;
			}
			return new ExtensionEntityBuilder<ExtensionSchemaBuilder<TParent>>(this, extensionEntity);
		}
	}
}
