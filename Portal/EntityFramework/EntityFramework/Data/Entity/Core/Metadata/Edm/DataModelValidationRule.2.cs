using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200049A RID: 1178
	internal abstract class DataModelValidationRule<TItem> : DataModelValidationRule where TItem : class
	{
		// Token: 0x06003A0A RID: 14858 RVA: 0x000C00F0 File Offset: 0x000BE2F0
		internal DataModelValidationRule(Action<EdmModelValidationContext, TItem> validate)
		{
			this._validate = validate;
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000C00FF File Offset: 0x000BE2FF
		internal override Type ValidatedType
		{
			get
			{
				return typeof(TItem);
			}
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x000C010B File Offset: 0x000BE30B
		internal override void Evaluate(EdmModelValidationContext context, MetadataItem item)
		{
			this._validate(context, item as TItem);
		}

		// Token: 0x0400135E RID: 4958
		protected Action<EdmModelValidationContext, TItem> _validate;
	}
}
