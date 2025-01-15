using System;
using System.Linq;
using Microsoft.Data.Spatial;

namespace Microsoft.Spatial
{
	// Token: 0x02000041 RID: 65
	public static class GeographyOperationsExtensions
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00004E68 File Offset: 0x00003068
		public static double? Distance(this Geography operand1, Geography operand2)
		{
			return GeographyOperationsExtensions.OperationsFor(new Geography[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Distance(operand1, operand2));
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00004ED0 File Offset: 0x000030D0
		public static double? Length(this Geography operand)
		{
			return GeographyOperationsExtensions.OperationsFor(new Geography[] { operand }).IfValidReturningNullable((SpatialOperations ops) => ops.Length(operand));
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00004F30 File Offset: 0x00003130
		public static bool? Intersects(this Geography operand1, Geography operand2)
		{
			return GeographyOperationsExtensions.OperationsFor(new Geography[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Intersects(operand1, operand2));
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00004F87 File Offset: 0x00003187
		private static SpatialOperations OperationsFor(params Geography[] operands)
		{
			if (Enumerable.Any<Geography>(operands, (Geography operand) => operand == null))
			{
				return null;
			}
			return operands[0].Creator.VerifyAndGetNonNullOperations();
		}
	}
}
