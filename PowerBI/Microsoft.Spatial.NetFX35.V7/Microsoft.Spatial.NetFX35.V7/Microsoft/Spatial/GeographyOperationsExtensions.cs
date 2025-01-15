using System;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200003C RID: 60
	public static class GeographyOperationsExtensions
	{
		// Token: 0x0600016F RID: 367 RVA: 0x000043FC File Offset: 0x000025FC
		public static double? Distance(this Geography operand1, Geography operand2)
		{
			return GeographyOperationsExtensions.OperationsFor(new Geography[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Distance(operand1, operand2));
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000444C File Offset: 0x0000264C
		public static double? Length(this Geography operand)
		{
			return GeographyOperationsExtensions.OperationsFor(new Geography[] { operand }).IfValidReturningNullable((SpatialOperations ops) => ops.Length(operand));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000448C File Offset: 0x0000268C
		public static bool? Intersects(this Geography operand1, Geography operand2)
		{
			return GeographyOperationsExtensions.OperationsFor(new Geography[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Intersects(operand1, operand2));
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000044DB File Offset: 0x000026DB
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
