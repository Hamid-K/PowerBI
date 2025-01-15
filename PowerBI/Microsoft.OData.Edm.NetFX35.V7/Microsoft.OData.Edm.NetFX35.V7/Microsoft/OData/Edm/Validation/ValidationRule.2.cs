using System;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x020000D9 RID: 217
	public sealed class ValidationRule<TItem> : ValidationRule where TItem : IEdmElement
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x00010429 File Offset: 0x0000E629
		public ValidationRule(Action<ValidationContext, TItem> validate)
		{
			this.validate = validate;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x00010438 File Offset: 0x0000E638
		internal override Type ValidatedType
		{
			get
			{
				return typeof(TItem);
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00010444 File Offset: 0x0000E644
		internal override void Evaluate(ValidationContext context, object item)
		{
			TItem titem = (TItem)((object)item);
			this.validate.Invoke(context, titem);
		}

		// Token: 0x04000309 RID: 777
		private readonly Action<ValidationContext, TItem> validate;
	}
}
