using System;
using System.ComponentModel;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav
{
	// Token: 0x02000014 RID: 20
	[ImmutableObject(true)]
	internal sealed class ConceptualBinSize
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000022A1 File Offset: 0x000004A1
		internal ConceptualBinSize(double value, ConceptualBinUnit unit)
		{
			this._value = value;
			this._unit = unit;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000022B7 File Offset: 0x000004B7
		internal double Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000022BF File Offset: 0x000004BF
		internal ConceptualBinUnit Unit
		{
			get
			{
				return this._unit;
			}
		}

		// Token: 0x04000042 RID: 66
		private readonly double _value;

		// Token: 0x04000043 RID: 67
		private readonly ConceptualBinUnit _unit;
	}
}
