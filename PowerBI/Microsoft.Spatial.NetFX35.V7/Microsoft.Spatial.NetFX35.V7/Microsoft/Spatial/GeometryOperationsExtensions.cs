using System;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200003D RID: 61
	public static class GeometryOperationsExtensions
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00004514 File Offset: 0x00002714
		public static double? Distance(this Geometry operand1, Geometry operand2)
		{
			return GeometryOperationsExtensions.OperationsFor(new Geometry[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Distance(operand1, operand2));
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004564 File Offset: 0x00002764
		public static double? Length(this Geometry operand)
		{
			return GeometryOperationsExtensions.OperationsFor(new Geometry[] { operand }).IfValidReturningNullable((SpatialOperations ops) => ops.Length(operand));
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000045A4 File Offset: 0x000027A4
		public static bool? Intersects(this Geometry operand1, Geometry operand2)
		{
			return GeometryOperationsExtensions.OperationsFor(new Geometry[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Intersects(operand1, operand2));
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000045F3 File Offset: 0x000027F3
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
