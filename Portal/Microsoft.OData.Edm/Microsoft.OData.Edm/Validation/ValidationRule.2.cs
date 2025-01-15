using System;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000148 RID: 328
	public sealed class ValidationRule<TItem> : ValidationRule where TItem : IEdmElement
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x00015A25 File Offset: 0x00013C25
		public ValidationRule(Action<ValidationContext, TItem> validate)
		{
			this.validate = validate;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00015A34 File Offset: 0x00013C34
		internal override Type ValidatedType
		{
			get
			{
				return typeof(TItem);
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00015A40 File Offset: 0x00013C40
		internal override void Evaluate(ValidationContext context, object item)
		{
			TItem titem = (TItem)((object)item);
			this.validate(context, titem);
		}

		// Token: 0x040003F9 RID: 1017
		private readonly Action<ValidationContext, TItem> validate;
	}
}
