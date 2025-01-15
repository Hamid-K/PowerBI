using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities
{
	// Token: 0x02000C33 RID: 3123
	[NullableContext(1)]
	[Nullable(0)]
	public static class BoundsRotationUtils
	{
		// Token: 0x06005098 RID: 20632 RVA: 0x000FD20F File Offset: 0x000FB40F
		[return: Nullable(new byte[] { 0, 1 })]
		public static Bounds<PixelUnit> Rotate([Nullable(new byte[] { 0, 1 })] this Bounds<PixelUnit> bounds, float angle, bool inPlace)
		{
			if (Math.Abs(angle) < 0.01f)
			{
				return bounds;
			}
			return (inPlace ? TransformationMatrix.Matrix.CreateRotate(angle, bounds.Center) : TransformationMatrix.Matrix.CreateRotate(angle)).TransformBoundingBox(bounds);
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x000FD240 File Offset: 0x000FB440
		public static IReadOnlyList<Vector<PixelUnit>> RotatedBoundingPoints([Nullable(new byte[] { 0, 1 })] this Bounds<PixelUnit> bounds, float angle, bool inPlace)
		{
			TransformationMatrix.Matrix matrix = (inPlace ? TransformationMatrix.Matrix.CreateRotate(angle, bounds.Center) : TransformationMatrix.Matrix.CreateRotate(angle));
			return OrdinalUtilities.Ordinals.Select(new Func<Ordinal, Vector<PixelUnit>>(bounds.Corner)).Select(new Func<Vector<PixelUnit>, Vector<PixelUnit>>(matrix.TransformPoint)).ToList<Vector<PixelUnit>>();
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x000FD298 File Offset: 0x000FB498
		[return: Nullable(new byte[] { 0, 1 })]
		public static Bounds<PixelUnit> Unrotate([Nullable(new byte[] { 0, 1 })] this Bounds<PixelUnit> bounds, TransformationMatrix matrix, double originalHeight, bool inPlace = false)
		{
			if (!matrix.IsRotated)
			{
				return bounds;
			}
			DoubleVector<PixelUnit> doubleVector = (inPlace ? bounds.Center : matrix.JustReverseRotation.TransformPoint(bounds.Center));
			return bounds.Unrotate(matrix.RotationAngle, originalHeight, doubleVector);
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x000FD2DC File Offset: 0x000FB4DC
		[return: Nullable(new byte[] { 0, 1 })]
		public static Bounds<PixelUnit> Unrotate([Nullable(new byte[] { 0, 1 })] this Bounds<PixelUnit> bounds, float angle, double originalHeight, DoubleVector<PixelUnit> unrotatedCenter)
		{
			if (Math.Abs(angle) < 0.01f)
			{
				return bounds;
			}
			double num = Math.Sin((double)angle);
			double num2 = Math.Cos((double)angle);
			bool flag = Math.Abs(num) < 0.009999999776482582;
			AxisAligned<double> originalSize;
			if (Math.Abs(num2) < 0.009999999776482582)
			{
				originalSize = new AxisAligned<double>((Axis a) => (double)bounds[a.Perpendicular()].Size());
			}
			else if (flag)
			{
				originalSize = new AxisAligned<double>((Axis a) => (double)bounds[a].Size());
			}
			else
			{
				double num3 = ((Math.Abs(num2) > Math.Abs(num)) ? (((double)bounds.Width() - originalHeight * Math.Abs(num)) / Math.Abs(num2)) : (((double)bounds.Height() - originalHeight * Math.Abs(num2)) / Math.Abs(num)));
				originalSize = new AxisAligned<double>(num3, originalHeight);
			}
			return new Bounds<PixelUnit>(delegate(Axis axis)
			{
				double num4 = Math.Max(0.0, originalSize[axis] - 1.0) / 2.0;
				return new Range<PixelUnit>((int)Math.Round(unrotatedCenter[axis] - num4), (int)Math.Round(unrotatedCenter[axis] + num4));
			});
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x000FD3E4 File Offset: 0x000FB5E4
		public static IReadOnlyList<Vector<PixelUnit>> BoundingPointsIfRotatedBy([Nullable(new byte[] { 0, 1 })] this Bounds<PixelUnit> bounds, float angle, double originalHeight)
		{
			return bounds.Unrotate(angle, originalHeight, bounds.Center).RotatedBoundingPoints(angle, true);
		}
	}
}
