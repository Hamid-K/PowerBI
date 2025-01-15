using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x0200011E RID: 286
	internal sealed class MatrixTransform : ITransform
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x0003E6CE File Offset: 0x0003C8CE
		internal MatrixTransform(float[] values)
		{
			this._values = values;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0003E6DD File Offset: 0x0003C8DD
		public TransformMatrix ComputeMatrix()
		{
			return new TransformMatrix(this._values);
		}

		// Token: 0x040008AF RID: 2223
		private readonly float[] _values;
	}
}
