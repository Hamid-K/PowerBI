using System;

namespace Microsoft.Data.Edm.Validation
{
	// Token: 0x0200023C RID: 572
	public sealed class ValidationRule<TItem> : ValidationRule where TItem : IEdmElement
	{
		// Token: 0x06000D13 RID: 3347 RVA: 0x000295A9 File Offset: 0x000277A9
		public ValidationRule(Action<ValidationContext, TItem> validate)
		{
			this.validate = validate;
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x000295B8 File Offset: 0x000277B8
		internal override Type ValidatedType
		{
			get
			{
				return typeof(TItem);
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000295C4 File Offset: 0x000277C4
		internal override void Evaluate(ValidationContext context, object item)
		{
			TItem titem = (TItem)((object)item);
			this.validate.Invoke(context, titem);
		}

		// Token: 0x0400067C RID: 1660
		private readonly Action<ValidationContext, TItem> validate;
	}
}
