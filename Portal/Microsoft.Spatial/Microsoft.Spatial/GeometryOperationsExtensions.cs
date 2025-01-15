using System;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000042 RID: 66
	public static class GeometryOperationsExtensions
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x000051E8 File Offset: 0x000033E8
		public static double? Distance(this Geometry operand1, Geometry operand2)
		{
			return GeometryOperationsExtensions.OperationsFor(new Geometry[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Distance(operand1, operand2));
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005238 File Offset: 0x00003438
		public static double? Length(this Geometry operand)
		{
			return GeometryOperationsExtensions.OperationsFor(new Geometry[] { operand }).IfValidReturningNullable((SpatialOperations ops) => ops.Length(operand));
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005278 File Offset: 0x00003478
		public static bool? Intersects(this Geometry operand1, Geometry operand2)
		{
			return GeometryOperationsExtensions.OperationsFor(new Geometry[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Intersects(operand1, operand2));
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000052C7 File Offset: 0x000034C7
		private static SpatialOperations OperationsFor(params Geometry[] operands)
		{
			if (operands.Any((Geometry operand) => operand == null))
			{
				return null;
			}
			return operands[0].Creator.VerifyAndGetNonNullOperations();
		}
	}
}
