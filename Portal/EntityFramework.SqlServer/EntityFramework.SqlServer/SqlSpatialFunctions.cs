using System;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000014 RID: 20
	public static class SqlSpatialFunctions
	{
		// Token: 0x06000189 RID: 393 RVA: 0x000092CF File Offset: 0x000074CF
		[DbFunction("SqlServer", "POINTGEOGRAPHY")]
		public static DbGeography PointGeography(double? latitude, double? longitude, int? spatialReferenceId)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000092DB File Offset: 0x000074DB
		[DbFunction("SqlServer", "ASTEXTZM")]
		public static string AsTextZM(DbGeography geographyValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000092E7 File Offset: 0x000074E7
		[DbFunction("SqlServer", "BUFFERWITHTOLERANCE")]
		public static DbGeography BufferWithTolerance(DbGeography geographyValue, double? distance, double? tolerance, bool? relative)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000092F3 File Offset: 0x000074F3
		[DbFunction("SqlServer", "ENVELOPEANGLE")]
		public static double? EnvelopeAngle(DbGeography geographyValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000092FF File Offset: 0x000074FF
		[DbFunction("SqlServer", "ENVELOPECENTER")]
		public static DbGeography EnvelopeCenter(DbGeography geographyValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000930B File Offset: 0x0000750B
		[DbFunction("SqlServer", "FILTER")]
		public static bool? Filter(DbGeography geographyValue, DbGeography geographyOther)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00009317 File Offset: 0x00007517
		[DbFunction("SqlServer", "INSTANCEOF")]
		public static bool? InstanceOf(DbGeography geographyValue, string geometryTypeName)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00009323 File Offset: 0x00007523
		[DbFunction("SqlServer", "NUMRINGS")]
		public static int? NumRings(DbGeography geographyValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000932F File Offset: 0x0000752F
		[DbFunction("SqlServer", "REDUCE")]
		public static DbGeography Reduce(DbGeography geographyValue, double? tolerance)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000933B File Offset: 0x0000753B
		[DbFunction("SqlServer", "RINGN")]
		public static DbGeography RingN(DbGeography geographyValue, int? index)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009347 File Offset: 0x00007547
		[DbFunction("SqlServer", "POINTGEOMETRY")]
		public static DbGeometry PointGeometry(double? xCoordinate, double? yCoordinate, int? spatialReferenceId)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00009353 File Offset: 0x00007553
		[DbFunction("SqlServer", "ASTEXTZM")]
		public static string AsTextZM(DbGeometry geometryValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000935F File Offset: 0x0000755F
		[DbFunction("SqlServer", "BUFFERWITHTOLERANCE")]
		public static DbGeometry BufferWithTolerance(DbGeometry geometryValue, double? distance, double? tolerance, bool? relative)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000936B File Offset: 0x0000756B
		[DbFunction("SqlServer", "INSTANCEOF")]
		public static bool? InstanceOf(DbGeometry geometryValue, string geometryTypeName)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009377 File Offset: 0x00007577
		[DbFunction("SqlServer", "FILTER")]
		public static bool? Filter(DbGeometry geometryValue, DbGeometry geometryOther)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009383 File Offset: 0x00007583
		[DbFunction("SqlServer", "MAKEVALID")]
		public static DbGeometry MakeValid(DbGeometry geometryValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000938F File Offset: 0x0000758F
		[DbFunction("SqlServer", "REDUCE")]
		public static DbGeometry Reduce(DbGeometry geometryValue, double? tolerance)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}
	}
}
