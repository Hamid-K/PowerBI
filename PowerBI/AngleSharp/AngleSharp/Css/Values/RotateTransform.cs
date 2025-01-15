using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000125 RID: 293
	internal sealed class RotateTransform : ITransform
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x0003EDA4 File Offset: 0x0003CFA4
		internal RotateTransform(float x, float y, float z, float angle)
		{
			this._x = x;
			this._y = y;
			this._z = z;
			this._angle = angle;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0003EDC9 File Offset: 0x0003CFC9
		public float X
		{
			get
			{
				return this._x;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0003EDD1 File Offset: 0x0003CFD1
		public float Y
		{
			get
			{
				return this._y;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0003EDD9 File Offset: 0x0003CFD9
		public float Z
		{
			get
			{
				return this._z;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0003EDE1 File Offset: 0x0003CFE1
		public float Angle
		{
			get
			{
				return this._angle;
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0003EDE9 File Offset: 0x0003CFE9
		public static RotateTransform RotateX(float angle)
		{
			return new RotateTransform(1f, 0f, 0f, angle);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0003EE00 File Offset: 0x0003D000
		public static RotateTransform RotateY(float angle)
		{
			return new RotateTransform(0f, 1f, 0f, angle);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0003EE17 File Offset: 0x0003D017
		public static RotateTransform RotateZ(float angle)
		{
			return new RotateTransform(0f, 0f, 1f, angle);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0003EE30 File Offset: 0x0003D030
		public TransformMatrix ComputeMatrix()
		{
			float num = 1f / (float)Math.Sqrt((double)(this._x * this._x + this._y * this._y + this._z * this._z));
			float num2 = (float)Math.Sin((double)this._angle);
			float num3 = (float)Math.Cos((double)this._angle);
			float num4 = this._x * num;
			float num5 = this._y * num;
			float num6 = this._z * num;
			float num7 = 1f - num3;
			return new TransformMatrix(num4 * num4 * num7 + num3, num5 * num4 * num7 - num6 * num2, num6 * num4 * num7 + num5 * num2, num4 * num5 * num7 + num6 * num2, num5 * num5 * num7 + num3, num6 * num5 * num7 - num4 * num2, num4 * num6 * num7 - num5 * num2, num5 * num6 * num7 + num4 * num2, num6 * num6 * num7 + num3, 0f, 0f, 0f, 0f, 0f, 0f);
		}

		// Token: 0x040008CA RID: 2250
		private readonly float _x;

		// Token: 0x040008CB RID: 2251
		private readonly float _y;

		// Token: 0x040008CC RID: 2252
		private readonly float _z;

		// Token: 0x040008CD RID: 2253
		private readonly float _angle;
	}
}
