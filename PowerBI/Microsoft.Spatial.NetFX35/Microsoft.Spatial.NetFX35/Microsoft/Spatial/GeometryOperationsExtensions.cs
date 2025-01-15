using System;
using System.Linq;
using Microsoft.Data.Spatial;

namespace Microsoft.Spatial
{
	// Token: 0x02000042 RID: 66
	public static class GeometryOperationsExtensions
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00004FDC File Offset: 0x000031DC
		public static double? Distance(this Geometry operand1, Geometry operand2)
		{
			return GeometryOperationsExtensions.OperationsFor(new Geometry[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Distance(operand1, operand2));
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005044 File Offset: 0x00003244
		public static double? Length(this Geometry operand)
		{
			return GeometryOperationsExtensions.OperationsFor(new Geometry[] { operand }).IfValidReturningNullable((SpatialOperations ops) => ops.Length(operand));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000050A4 File Offset: 0x000032A4
		public static bool? Intersects(this Geometry operand1, Geometry operand2)
		{
			return GeometryOperationsExtensions.OperationsFor(new Geometry[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Intersects(operand1, operand2));
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000050FB File Offset: 0x000032FB
		private static SpatialOperations OperationsFor(params Geometry[] operands)
		{
			if (Enumerable.Any<Geometry>(operands, (Geometry operand) => operand == null))
			{
				return null;
			}
			return operands[0].Creator.VerifyAndGetNonNullOperations();
		}
	}
}
