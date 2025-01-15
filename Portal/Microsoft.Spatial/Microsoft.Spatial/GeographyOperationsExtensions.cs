using System;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000041 RID: 65
	public static class GeographyOperationsExtensions
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x000050D0 File Offset: 0x000032D0
		public static double? Distance(this Geography operand1, Geography operand2)
		{
			return GeographyOperationsExtensions.OperationsFor(new Geography[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Distance(operand1, operand2));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005120 File Offset: 0x00003320
		public static double? Length(this Geography operand)
		{
			return GeographyOperationsExtensions.OperationsFor(new Geography[] { operand }).IfValidReturningNullable((SpatialOperations ops) => ops.Length(operand));
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00005160 File Offset: 0x00003360
		public static bool? Intersects(this Geography operand1, Geography operand2)
		{
			return GeographyOperationsExtensions.OperationsFor(new Geography[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Intersects(operand1, operand2));
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000051AF File Offset: 0x000033AF
		private static SpatialOperations OperationsFor(params Geography[] operands)
		{
			if (operands.Any((Geography operand) => operand == null))
			{
				return null;
			}
			return operands[0].Creator.VerifyAndGetNonNullOperations();
		}
	}
}
