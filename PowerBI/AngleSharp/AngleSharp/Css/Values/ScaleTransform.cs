using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000126 RID: 294
	internal sealed class ScaleTransform : ITransform
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x0003EF3D File Offset: 0x0003D13D
		internal ScaleTransform(float sx, float sy, float sz)
		{
			this._sx = sx;
			this._sy = sy;
			this._sz = sz;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0003EF5C File Offset: 0x0003D15C
		public TransformMatrix ComputeMatrix()
		{
			return new TransformMatrix(this._sx, 0f, 0f, 0f, this._sy, 0f, 0f, 0f, this._sz, 0f, 0f, 0f, 0f, 0f, 0f);
		}

		// Token: 0x040008CE RID: 2254
		private readonly float _sx;

		// Token: 0x040008CF RID: 2255
		private readonly float _sy;

		// Token: 0x040008D0 RID: 2256
		private readonly float _sz;
	}
}
