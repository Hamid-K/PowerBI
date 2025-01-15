using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000096 RID: 150
	internal sealed class StateIndicator
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000CE4A File Offset: 0x0000B04A
		internal StateIndicator(string value)
		{
			this._value = new Expression(value);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000CE5E File Offset: 0x0000B05E
		internal Expression Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040001F0 RID: 496
		private readonly Expression _value;
	}
}
