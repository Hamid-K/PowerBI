using System;

namespace AngleSharp.Css
{
	// Token: 0x02000110 RID: 272
	public sealed class TransformMatrix : IEquatable<TransformMatrix>
	{
		// Token: 0x060008A1 RID: 2209 RVA: 0x0003CC1D File Offset: 0x0003AE1D
		private TransformMatrix()
		{
			this._matrix = new float[4, 4];
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0003CC34 File Offset: 0x0003AE34
		public TransformMatrix(float[] values)
			: this()
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length != 16)
			{
				throw new ArgumentException("You need to provide 16 (4x4) values.", "values");
			}
			int i = 0;
			int num = 0;
			while (i < 4)
			{
				int j = 0;
				while (j < 4)
				{
					this._matrix[j, i] = values[num];
					j++;
					num++;
				}
				i++;
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0003CC9C File Offset: 0x0003AE9C
		public TransformMatrix(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33, float tx, float ty, float tz, float px, float py, float pz)
			: this()
		{
			this._matrix[0, 0] = m11;
			this._matrix[0, 1] = m12;
			this._matrix[0, 2] = m13;
			this._matrix[1, 0] = m21;
			this._matrix[1, 1] = m22;
			this._matrix[1, 2] = m23;
			this._matrix[2, 0] = m31;
			this._matrix[2, 1] = m32;
			this._matrix[2, 2] = m33;
			this._matrix[0, 3] = tx;
			this._matrix[1, 3] = ty;
			this._matrix[2, 3] = tz;
			this._matrix[3, 0] = px;
			this._matrix[3, 1] = py;
			this._matrix[3, 2] = pz;
			this._matrix[3, 3] = 1f;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0003CD9F File Offset: 0x0003AF9F
		public float M11
		{
			get
			{
				return this._matrix[0, 0];
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0003CDAE File Offset: 0x0003AFAE
		public float M12
		{
			get
			{
				return this._matrix[0, 1];
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0003CDBD File Offset: 0x0003AFBD
		public float M13
		{
			get
			{
				return this._matrix[0, 2];
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0003CDCC File Offset: 0x0003AFCC
		public float M21
		{
			get
			{
				return this._matrix[1, 0];
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0003CDDB File Offset: 0x0003AFDB
		public float M22
		{
			get
			{
				return this._matrix[1, 1];
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0003CDEA File Offset: 0x0003AFEA
		public float M23
		{
			get
			{
				return this._matrix[1, 2];
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0003CDF9 File Offset: 0x0003AFF9
		public float M31
		{
			get
			{
				return this._matrix[2, 0];
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0003CE08 File Offset: 0x0003B008
		public float M32
		{
			get
			{
				return this._matrix[2, 1];
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0003CE17 File Offset: 0x0003B017
		public float M33
		{
			get
			{
				return this._matrix[2, 2];
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0003CE26 File Offset: 0x0003B026
		public float Tx
		{
			get
			{
				return this._matrix[0, 3];
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0003CE35 File Offset: 0x0003B035
		public float Ty
		{
			get
			{
				return this._matrix[1, 3];
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0003CE44 File Offset: 0x0003B044
		public float Tz
		{
			get
			{
				return this._matrix[2, 3];
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0003CE54 File Offset: 0x0003B054
		public bool Equals(TransformMatrix other)
		{
			float[,] matrix = this._matrix;
			float[,] matrix2 = other._matrix;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					if (matrix[i, j] != matrix2[i, j])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0003CE9C File Offset: 0x0003B09C
		public override bool Equals(object obj)
		{
			TransformMatrix transformMatrix = obj as TransformMatrix;
			return transformMatrix != null && this.Equals(transformMatrix);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0003CEBC File Offset: 0x0003B0BC
		public override int GetHashCode()
		{
			float num = 0f;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					num += this._matrix[i, j] * (float)(4 * i + j);
				}
			}
			return (int)num;
		}

		// Token: 0x04000867 RID: 2151
		public static readonly TransformMatrix Zero = new TransformMatrix();

		// Token: 0x04000868 RID: 2152
		public static readonly TransformMatrix One = new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, 0f);

		// Token: 0x04000869 RID: 2153
		private readonly float[,] _matrix;
	}
}
