using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000121 RID: 289
	internal sealed class PerspectiveTransform : ITransform
	{
		// Token: 0x0600094C RID: 2380 RVA: 0x0003E9EE File Offset: 0x0003CBEE
		internal PerspectiveTransform(Length distance)
		{
			this._distance = distance;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0003EA00 File Offset: 0x0003CC00
		public TransformMatrix ComputeMatrix()
		{
			return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, -1f / this._distance.ToPixel());
		}

		// Token: 0x040008B9 RID: 2233
		private readonly Length _distance;
	}
}
