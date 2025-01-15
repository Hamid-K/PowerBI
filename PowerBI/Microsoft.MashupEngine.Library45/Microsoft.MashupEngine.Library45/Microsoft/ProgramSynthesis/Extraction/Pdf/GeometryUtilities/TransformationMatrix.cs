using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities
{
	// Token: 0x02000C4A RID: 3146
	[NullableContext(1)]
	[Nullable(0)]
	public class TransformationMatrix
	{
		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06005124 RID: 20772 RVA: 0x000FED42 File Offset: 0x000FCF42
		public bool IsRotated { get; }

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06005125 RID: 20773 RVA: 0x000FED4A File Offset: 0x000FCF4A
		public bool IsRotatedByRightAngle { get; }

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06005126 RID: 20774 RVA: 0x000FED52 File Offset: 0x000FCF52
		public bool IsRotatedByEvenRightAngle { get; }

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06005127 RID: 20775 RVA: 0x000FED5A File Offset: 0x000FCF5A
		public float RotationAngle { get; }

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06005128 RID: 20776 RVA: 0x000FED62 File Offset: 0x000FCF62
		public float XScale
		{
			get
			{
				return this.WithoutRotation._matrix._m00;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06005129 RID: 20777 RVA: 0x000FED74 File Offset: 0x000FCF74
		public bool IsFlippedHorizontally { get; }

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x0600512A RID: 20778 RVA: 0x000FED7C File Offset: 0x000FCF7C
		public Direction? DownDirection { get; }

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x0600512B RID: 20779 RVA: 0x000FED84 File Offset: 0x000FCF84
		[Nullable(new byte[] { 0, 1 })]
		public Range<PixelUnit> GlyphApparentVerticalRange
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x0600512C RID: 20780 RVA: 0x000FED8C File Offset: 0x000FCF8C
		public TransformationMatrix WithoutRotation { get; }

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x0600512D RID: 20781 RVA: 0x000FED94 File Offset: 0x000FCF94
		public TransformationMatrix.Matrix JustRotation { get; }

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x0600512E RID: 20782 RVA: 0x000FED9C File Offset: 0x000FCF9C
		public TransformationMatrix.Matrix JustReverseRotation { get; }

		// Token: 0x0600512F RID: 20783 RVA: 0x000FEDA4 File Offset: 0x000FCFA4
		private TransformationMatrix(TransformationMatrix.Matrix matrix, bool withoutRotation = false)
		{
			this._matrix = matrix;
			TransformationMatrix.Matrix matrix2 = matrix;
			double num = Math.Atan2((double)matrix._m01, (double)matrix._m00);
			if (MathUtils.NormalizeAngle(Math.Atan2((double)matrix._m11, (double)matrix._m10) - num) < 3.141592653589793)
			{
				this.IsFlippedHorizontally = true;
				matrix2 *= TransformationMatrix.Matrix.FlipHorizontally;
			}
			float m = matrix2._m00;
			float m2 = matrix2._m01;
			this.IsRotated = false;
			this.IsRotatedByRightAngle = false;
			this.RotationAngle = 0f;
			bool flag = Math.Abs(m) > 0.01f;
			bool flag2 = Math.Abs(m2) > 0.01f;
			if (flag || flag2)
			{
				float num2 = (float)Math.Atan2((double)m2, (double)m);
				if (Math.Abs(num2) > 0.01f)
				{
					this.IsRotated = true;
					this.IsRotatedByRightAngle = !flag || !flag2;
					this.IsRotatedByEvenRightAngle = !flag2;
					this.RotationAngle = (float)MathUtils.NormalizeAngle((double)num2);
					if (this.IsFlippedHorizontally)
					{
						this.RotationAngle -= 6.2831855f;
					}
				}
			}
			if (withoutRotation || !this.IsRotated)
			{
				this.JustRotation = (this.JustReverseRotation = TransformationMatrix.Matrix.Identity);
				this.WithoutRotation = this;
				this.DownDirection = new Direction?(Direction.Down);
			}
			else
			{
				this.JustRotation = TransformationMatrix.Matrix.CreateRotate(this.RotationAngle);
				this.JustReverseRotation = TransformationMatrix.Matrix.CreateRotate(-this.RotationAngle);
				this.WithoutRotation = new TransformationMatrix(this._matrix * this.JustReverseRotation, true);
				if (this.IsRotatedByRightAngle)
				{
					if (this.IsRotatedByEvenRightAngle)
					{
						if (matrix._m00 < 0f)
						{
							this.DownDirection = new Direction?(Direction.Up);
						}
						else
						{
							this.DownDirection = new Direction?(Direction.Down);
						}
					}
					else if (matrix._m01 > 0f)
					{
						this.DownDirection = new Direction?(Direction.Left);
					}
					else
					{
						this.DownDirection = new Direction?(Direction.Right);
					}
				}
			}
			this.GlyphApparentVerticalRange = Range<PixelUnit>.CreateUnordered((int)Math.Round((double)this._matrix._m21), (int)Math.Round((double)(this._matrix._m11 + this._matrix._m21)));
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x000FEFD3 File Offset: 0x000FD1D3
		public static TransformationMatrix Create(TransformationMatrix.Matrix matrix)
		{
			return new TransformationMatrix(matrix, false);
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x000FEFDC File Offset: 0x000FD1DC
		public static TransformationMatrix Create(float m00, float m01, float m10, float m11, float m20, float m21)
		{
			return new TransformationMatrix(new TransformationMatrix.Matrix(m00, m01, m10, m11, m20, m21), false);
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x000FEFF4 File Offset: 0x000FD1F4
		public override string ToString()
		{
			return string.Format("Transformation(angle={0} deg, XScale={1}, HFlip={2}, {3})", new object[]
			{
				MathUtils.ToDegrees(this.RotationAngle),
				this.XScale,
				this.IsFlippedHorizontally,
				this._matrix
			});
		}

		// Token: 0x040023D2 RID: 9170
		public const float AngleEpsilon = 0.01f;

		// Token: 0x040023D3 RID: 9171
		public const float XScaleEpsilon = 0.01f;

		// Token: 0x040023D4 RID: 9172
		public const float MatrixEpsilon = 0.01f;

		// Token: 0x040023D5 RID: 9173
		public const float SinEpsilon = 0.01f;

		// Token: 0x040023D6 RID: 9174
		public readonly TransformationMatrix.Matrix _matrix;

		// Token: 0x02000C4B RID: 3147
		[Nullable(0)]
		public class Matrix
		{
			// Token: 0x06005133 RID: 20787 RVA: 0x000FF049 File Offset: 0x000FD249
			public Matrix(float m00, float m01, float m10, float m11, float m20, float m21)
			{
				this._m00 = m00;
				this._m01 = m01;
				this._m10 = m10;
				this._m11 = m11;
				this._m20 = m20;
				this._m21 = m21;
			}

			// Token: 0x17000E83 RID: 3715
			// (get) Token: 0x06005134 RID: 20788 RVA: 0x000FF07E File Offset: 0x000FD27E
			public static TransformationMatrix.Matrix Identity { get; } = new TransformationMatrix.Matrix(1f, 0f, 0f, 1f, 0f, 0f);

			// Token: 0x17000E84 RID: 3716
			// (get) Token: 0x06005135 RID: 20789 RVA: 0x000FF085 File Offset: 0x000FD285
			public static TransformationMatrix.Matrix FlipVertically { get; } = new TransformationMatrix.Matrix(1f, 0f, 0f, -1f, 0f, 0f);

			// Token: 0x17000E85 RID: 3717
			// (get) Token: 0x06005136 RID: 20790 RVA: 0x000FF08C File Offset: 0x000FD28C
			public static TransformationMatrix.Matrix FlipHorizontally { get; } = new TransformationMatrix.Matrix(-1f, 0f, 0f, 1f, 0f, 0f);

			// Token: 0x06005137 RID: 20791 RVA: 0x000FF094 File Offset: 0x000FD294
			public static TransformationMatrix.Matrix operator *(TransformationMatrix.Matrix a, TransformationMatrix.Matrix b)
			{
				return new TransformationMatrix.Matrix(a._m00 * b._m00 + a._m01 * b._m10, a._m00 * b._m01 + a._m01 * b._m11, a._m10 * b._m00 + a._m11 * b._m10, a._m10 * b._m01 + a._m11 * b._m11, a._m20 * b._m00 + a._m21 * b._m10 + b._m20, a._m20 * b._m01 + a._m21 * b._m11 + b._m21);
			}

			// Token: 0x06005138 RID: 20792 RVA: 0x000FF158 File Offset: 0x000FD358
			public bool Equals(TransformationMatrix.Matrix b)
			{
				return this._m00 == b._m00 && this._m01 == b._m01 && this._m10 == b._m10 && this._m11 == b._m11 && this._m20 == b._m20 && this._m21 == b._m21;
			}

			// Token: 0x06005139 RID: 20793 RVA: 0x000FF1BC File Offset: 0x000FD3BC
			public DoubleVector<PixelUnit> TransformPoint(DoubleVector<PixelUnit> pt)
			{
				return new DoubleVector<PixelUnit>(pt.X * (double)this._m00 + pt.Y * (double)this._m10 + (double)this._m20, pt.X * (double)this._m01 + pt.Y * (double)this._m11 + (double)this._m21);
			}

			// Token: 0x0600513A RID: 20794 RVA: 0x000FF218 File Offset: 0x000FD418
			public Vector<PixelUnit> TransformPoint(Vector<PixelUnit> pt)
			{
				return this.TransformPoint(new DoubleVector<PixelUnit>(pt)).RoundToVector();
			}

			// Token: 0x0600513B RID: 20795 RVA: 0x000FF22B File Offset: 0x000FD42B
			public static TransformationMatrix.Matrix CreateRotate(float angle)
			{
				return new TransformationMatrix.Matrix((float)Math.Cos((double)angle), (float)Math.Sin((double)angle), (float)(-(float)Math.Sin((double)angle)), (float)Math.Cos((double)angle), 0f, 0f);
			}

			// Token: 0x0600513C RID: 20796 RVA: 0x000FF260 File Offset: 0x000FD460
			public static TransformationMatrix.Matrix CreateRotate(float angle, DoubleVector<PixelUnit> rotateAround)
			{
				return new TransformationMatrix.Matrix((float)Math.Cos((double)angle), (float)Math.Sin((double)angle), (float)(-(float)Math.Sin((double)angle)), (float)Math.Cos((double)angle), (float)(rotateAround.X - Math.Cos((double)angle) * rotateAround.X + Math.Sin((double)angle) * rotateAround.Y), (float)(rotateAround.Y - Math.Sin((double)angle) * rotateAround.X - Math.Cos((double)angle) * rotateAround.Y));
			}

			// Token: 0x0600513D RID: 20797 RVA: 0x000FF2DD File Offset: 0x000FD4DD
			public static TransformationMatrix.Matrix CreateRotate(float angle, Vector<PixelUnit> rotateAround)
			{
				return TransformationMatrix.Matrix.CreateRotate(angle, new DoubleVector<PixelUnit>(rotateAround));
			}

			// Token: 0x0600513E RID: 20798 RVA: 0x000FF2EB File Offset: 0x000FD4EB
			public static TransformationMatrix.Matrix CreateTranslation(float tx, float ty)
			{
				return new TransformationMatrix.Matrix(1f, 0f, 0f, 1f, tx, ty);
			}

			// Token: 0x0600513F RID: 20799 RVA: 0x000FF308 File Offset: 0x000FD508
			public static TransformationMatrix.Matrix CreateScale(float sx, float sy)
			{
				return new TransformationMatrix.Matrix(sx, 0f, 0f, sy, 0f, 0f);
			}

			// Token: 0x06005140 RID: 20800 RVA: 0x000FF328 File Offset: 0x000FD528
			[return: Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit> TransformBoundingBox([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> box)
			{
				int num = int.MaxValue;
				int num2 = int.MinValue;
				int num3 = int.MaxValue;
				int num4 = int.MinValue;
				foreach (Ordinal ordinal in OrdinalUtilities.Ordinals)
				{
					Vector<PixelUnit> vector = this.TransformPoint(box.Corner(ordinal));
					num = Math.Min(num, vector.Y);
					num2 = Math.Max(num2, vector.Y);
					num3 = Math.Min(num3, vector.X);
					num4 = Math.Max(num4, vector.X);
				}
				return new Bounds<PixelUnit>(num3, num4, num, num2);
			}

			// Token: 0x06005141 RID: 20801 RVA: 0x000FF3E0 File Offset: 0x000FD5E0
			public override string ToString()
			{
				return string.Format("[[{0}, {1}], [{2}, {3}], [{4}, {5}]]", new object[] { this._m00, this._m01, this._m10, this._m11, this._m20, this._m21 });
			}

			// Token: 0x040023E1 RID: 9185
			public readonly float _m00;

			// Token: 0x040023E2 RID: 9186
			public readonly float _m01;

			// Token: 0x040023E3 RID: 9187
			public readonly float _m10;

			// Token: 0x040023E4 RID: 9188
			public readonly float _m11;

			// Token: 0x040023E5 RID: 9189
			public readonly float _m20;

			// Token: 0x040023E6 RID: 9190
			public readonly float _m21;
		}
	}
}
