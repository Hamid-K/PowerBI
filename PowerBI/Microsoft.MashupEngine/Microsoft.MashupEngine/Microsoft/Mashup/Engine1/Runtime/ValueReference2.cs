using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016B8 RID: 5816
	internal class ValueReference2 : IValueReference2
	{
		// Token: 0x060093EE RID: 37870 RVA: 0x001E8646 File Offset: 0x001E6846
		public ValueReference2(IValueReference valueReference)
		{
			this.valueReference = valueReference;
		}

		// Token: 0x170026F2 RID: 9970
		// (get) Token: 0x060093EF RID: 37871 RVA: 0x001E8655 File Offset: 0x001E6855
		public bool Evaluated
		{
			get
			{
				return this.valueReference.Evaluated;
			}
		}

		// Token: 0x170026F3 RID: 9971
		// (get) Token: 0x060093F0 RID: 37872 RVA: 0x001E8662 File Offset: 0x001E6862
		public IValue Value
		{
			get
			{
				return this.valueReference.Value;
			}
		}

		// Token: 0x04004EE3 RID: 20195
		private IValueReference valueReference;
	}
}
