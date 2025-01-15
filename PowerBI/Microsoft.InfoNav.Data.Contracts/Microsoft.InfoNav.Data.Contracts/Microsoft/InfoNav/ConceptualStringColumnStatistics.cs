using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000033 RID: 51
	[ImmutableObject(true)]
	public sealed class ConceptualStringColumnStatistics : ConceptualColumnStatistics
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00002B52 File Offset: 0x00000D52
		public ConceptualStringColumnStatistics(int distinctValueCount, PrimitiveValue minValue, PrimitiveValue maxValue, int stringValueMaxLength)
			: base(distinctValueCount, minValue, maxValue)
		{
			this._stringValueMaxLength = stringValueMaxLength;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002B65 File Offset: 0x00000D65
		public int StringValueMaxLength
		{
			get
			{
				return this._stringValueMaxLength;
			}
		}

		// Token: 0x040000E0 RID: 224
		private readonly int _stringValueMaxLength;
	}
}
