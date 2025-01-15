using System;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x0200044D RID: 1101
	public class BacktrackingLineSearch : IDiffLineSearch
	{
		// Token: 0x060016DF RID: 5855 RVA: 0x00085789 File Offset: 0x00083989
		public BacktrackingLineSearch(float c1 = 0.0001f)
		{
			this._c1 = c1;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000857B0 File Offset: 0x000839B0
		public float Minimize(DiffFunc1D F, float initVal, float initDeriv)
		{
			Contracts.Check(initDeriv < 0f, "Cannot search in direction of ascent!");
			this._step *= 2f;
			for (;;)
			{
				float num2;
				float num = F(this._step, out num2);
				if (num <= initVal + this._c1 * this._step * initDeriv)
				{
					break;
				}
				this._step /= 2f;
			}
			return this._step;
		}

		// Token: 0x04000E04 RID: 3588
		private float _step = 1f;

		// Token: 0x04000E05 RID: 3589
		private float _c1 = 0.0001f;
	}
}
