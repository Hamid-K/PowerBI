using System;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000275 RID: 629
	public sealed class ValidationRule<TItem> : ValidationRule where TItem : IEdmElement
	{
		// Token: 0x06000E1D RID: 3613 RVA: 0x0002AEBF File Offset: 0x000290BF
		public ValidationRule(Action<ValidationContext, TItem> validate)
		{
			this.validate = validate;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0002AECE File Offset: 0x000290CE
		internal override Type ValidatedType
		{
			get
			{
				return typeof(TItem);
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0002AEDC File Offset: 0x000290DC
		internal override void Evaluate(ValidationContext context, object item)
		{
			TItem titem = (TItem)((object)item);
			this.validate.Invoke(context, titem);
		}

		// Token: 0x0400069C RID: 1692
		private readonly Action<ValidationContext, TItem> validate;
	}
}
