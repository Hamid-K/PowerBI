using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Util
{
	// Token: 0x02001AED RID: 6893
	internal static class Utilities
	{
		// Token: 0x0600E36E RID: 58222 RVA: 0x003049C0 File Offset: 0x00302BC0
		public static bool IsMissing(object value)
		{
			if (value != null)
			{
				string valStr = value as string;
				if (valStr == null || (!string.IsNullOrWhiteSpace(valStr) && !Utilities.MissingValueMarkers.Any((string mv) => valStr.Equals(mv, StringComparison.OrdinalIgnoreCase))))
				{
					return Utilities.IsNumeric(value) && double.IsNaN(Convert.ToDouble(value));
				}
			}
			return true;
		}

		// Token: 0x0600E36F RID: 58223 RVA: 0x00304A26 File Offset: 0x00302C26
		public static bool IsNa(object value)
		{
			return value == null || (Utilities.IsNumeric(value) && double.IsNaN(Convert.ToDouble(value)));
		}

		// Token: 0x0600E370 RID: 58224 RVA: 0x00304A44 File Offset: 0x00302C44
		public static bool IsDouble(object value)
		{
			bool flag;
			if (value == null)
			{
				flag = false;
			}
			else
			{
				bool flag2;
				switch (Type.GetTypeCode(value.GetType()))
				{
				case TypeCode.Single:
					flag2 = true;
					break;
				case TypeCode.Double:
					flag2 = true;
					break;
				case TypeCode.Decimal:
					flag2 = true;
					break;
				default:
					flag2 = false;
					break;
				}
				flag = flag2;
			}
			return flag;
		}

		// Token: 0x0600E371 RID: 58225 RVA: 0x00304A8C File Offset: 0x00302C8C
		public static bool IsInteger(object value)
		{
			bool flag;
			if (value == null)
			{
				flag = false;
			}
			else
			{
				bool flag2;
				switch (Type.GetTypeCode(value.GetType()))
				{
				case TypeCode.SByte:
					flag2 = true;
					break;
				case TypeCode.Byte:
					flag2 = true;
					break;
				case TypeCode.Int16:
					flag2 = true;
					break;
				case TypeCode.UInt16:
					flag2 = true;
					break;
				case TypeCode.Int32:
					flag2 = true;
					break;
				case TypeCode.UInt32:
					flag2 = true;
					break;
				case TypeCode.Int64:
					flag2 = true;
					break;
				case TypeCode.UInt64:
					flag2 = true;
					break;
				default:
					flag2 = false;
					break;
				}
				flag = flag2;
			}
			return flag;
		}

		// Token: 0x0600E372 RID: 58226 RVA: 0x00304AFB File Offset: 0x00302CFB
		public static bool IsNumeric(object value)
		{
			return Utilities.IsDouble(value) || Utilities.IsInteger(value);
		}

		// Token: 0x0600E373 RID: 58227 RVA: 0x00304B10 File Offset: 0x00302D10
		public static bool IsInteger(IEnumerable<object> data, bool nullAllowed = true)
		{
			return data.All((object d) => (nullAllowed && d == null) || Utilities.IsInteger(d));
		}

		// Token: 0x0600E374 RID: 58228 RVA: 0x00304B3C File Offset: 0x00302D3C
		public static bool IsDouble(IEnumerable<object> data, bool nullAllowed = true)
		{
			return data.All((object d) => (nullAllowed && d == null) || Utilities.IsDouble(d));
		}

		// Token: 0x0600E375 RID: 58229 RVA: 0x00304B68 File Offset: 0x00302D68
		public static bool IsNumeric(IEnumerable<object> data, bool nullAllowed = true)
		{
			return data.All((object d) => (nullAllowed && d == null) || Utilities.IsNumeric(d));
		}

		// Token: 0x0600E376 RID: 58230 RVA: 0x00304B94 File Offset: 0x00302D94
		public static IEnumerable<object> ToCustomString(this IEnumerable<object> columnData)
		{
			return columnData.Select(delegate(object o)
			{
				string text;
				if (o is DateTime)
				{
					text = ((DateTime)o).ToString("yyyy-MM-dd HH:mm:ss");
				}
				else
				{
					text = ((o != null) ? o.ToString() : null);
				}
				return text;
			});
		}

		// Token: 0x0600E377 RID: 58231 RVA: 0x00304BBC File Offset: 0x00302DBC
		public static string Uniquify(string identifier, List<string> existingIdentifiers)
		{
			if (existingIdentifiers.Contains(identifier))
			{
				return (from i in Enumerable.Range(1, 999)
					select string.Format("{0}_{1}", identifier, i)).First((string id) => !existingIdentifiers.Contains(id));
			}
			return identifier;
		}

		// Token: 0x0400560C RID: 22028
		private static readonly List<string> MissingValueMarkers = new List<string> { "na", "n/a", "none" };
	}
}
