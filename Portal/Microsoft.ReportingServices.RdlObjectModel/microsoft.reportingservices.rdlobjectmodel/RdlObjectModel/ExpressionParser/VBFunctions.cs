using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002BE RID: 702
	internal static class VBFunctions
	{
		// Token: 0x060015B4 RID: 5556 RVA: 0x00032ECA File Offset: 0x000310CA
		public static char CChar(object o)
		{
			return Convert.ToChar(o, CultureInfo.CurrentCulture);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00032ED7 File Offset: 0x000310D7
		public static short CShort(object o)
		{
			return RDLUtil.ConvertToInt16(o);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00032EDF File Offset: 0x000310DF
		public static int CUShort(object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00032EE6 File Offset: 0x000310E6
		public static decimal CDec(object o)
		{
			return RDLUtil.ConvertToDecimal(o);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00032EEE File Offset: 0x000310EE
		public static object CObj(object o)
		{
			return Convert.ToBoolean(o, CultureInfo.CurrentCulture);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00032F00 File Offset: 0x00031100
		public static bool CBool(object o)
		{
			return RDLUtil.ConvertToBoolean(o);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00032F08 File Offset: 0x00031108
		public static byte CByte(object o)
		{
			return RDLUtil.ConvertToByte(o);
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00032F10 File Offset: 0x00031110
		public static short CSByte(object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00032F17 File Offset: 0x00031117
		public static DateTime CDate(object o)
		{
			return RDLUtil.ConvertToDateTime(o);
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00032F1F File Offset: 0x0003111F
		public static int CInt(object o)
		{
			return RDLUtil.ConvertToInt32(o);
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00032F27 File Offset: 0x00031127
		public static long CUInt(object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00032F2E File Offset: 0x0003112E
		public static long CLng(object o)
		{
			return RDLUtil.ConvertToInt64(o);
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00032F36 File Offset: 0x00031136
		public static decimal CULng(object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00032F3D File Offset: 0x0003113D
		public static float CSng(object o)
		{
			return RDLUtil.ConvertToSingle(o);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00032F45 File Offset: 0x00031145
		public static string CStr(object o)
		{
			return Convert.ToString(o, CultureInfo.CurrentCulture);
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00032F52 File Offset: 0x00031152
		public static double CDbl(object o)
		{
			return RDLUtil.ConvertToDouble(o);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00032F5A File Offset: 0x0003115A
		public static object DirectCast(object o, string typeName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00032F61 File Offset: 0x00031161
		public static object TryCast(object o, string typeName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00032F68 File Offset: 0x00031168
		public static object CType(object o, string typeName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00032F6F File Offset: 0x0003116F
		public static Type GetType(string typeName)
		{
			throw new NotImplementedException();
		}
	}
}
