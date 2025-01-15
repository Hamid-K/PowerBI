using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012E4 RID: 4836
	internal sealed class RequiredElementAccessValueReference : ElementAccessValueReference
	{
		// Token: 0x0600801F RID: 32799 RVA: 0x001B526E File Offset: 0x001B346E
		public RequiredElementAccessValueReference(IValueReference reference, int index)
			: base(reference, index)
		{
		}

		// Token: 0x06008020 RID: 32800 RVA: 0x001B5278 File Offset: 0x001B3478
		protected override Value GetValue()
		{
			return this.reference.Value.AsList[this.index];
		}
	}
}
