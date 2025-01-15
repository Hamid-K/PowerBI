using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012F8 RID: 4856
	internal sealed class RequiredFieldAccessValueReference : FieldAccessValueReference
	{
		// Token: 0x06008070 RID: 32880 RVA: 0x001B687C File Offset: 0x001B4A7C
		public RequiredFieldAccessValueReference(IValueReference reference, string field)
			: base(reference, field)
		{
		}

		// Token: 0x06008071 RID: 32881 RVA: 0x001B6886 File Offset: 0x001B4A86
		protected override Value GetValue()
		{
			return this.reference.Value.AsRecord[this.field];
		}
	}
}
