using System;
using System.Linq;
using Microsoft.Data.Spatial;

namespace System.Spatial
{
	// Token: 0x0200003F RID: 63
	public static class GeographyOperationsExtensions
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00004D50 File Offset: 0x00002F50
		public static double? Distance(this Geography operand1, Geography operand2)
		{
			return GeographyOperationsExtensions.OperationsFor(new Geography[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Distance(operand1, operand2));
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00004DB8 File Offset: 0x00002FB8
		public static double? Length(this Geography operand)
		{
			return GeographyOperationsExtensions.OperationsFor(new Geography[] { operand }).IfValidReturningNullable((SpatialOperations ops) => ops.Length(operand));
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00004E18 File Offset: 0x00003018
		public static bool? Intersects(this Geography operand1, Geography operand2)
		{
			return GeographyOperationsExtensions.OperationsFor(new Geography[] { operand1, operand2 }).IfValidReturningNullable((SpatialOperations ops) => ops.Intersects(operand1, operand2));
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00004E6F File Offset: 0x0000306F
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
