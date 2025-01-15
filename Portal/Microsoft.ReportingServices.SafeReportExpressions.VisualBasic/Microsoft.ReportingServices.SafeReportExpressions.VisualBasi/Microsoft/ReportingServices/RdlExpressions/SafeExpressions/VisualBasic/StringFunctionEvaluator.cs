using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.VisualBasic
{
	// Token: 0x0200000C RID: 12
	internal class StringFunctionEvaluator : ArithmeticEvaluator, IFunctionEvaluator
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000033B0 File Offset: 0x000015B0
		public object Evaluate(string functionName, List<object> arguments)
		{
			base.ValidateStrongType(arguments);
			string text = functionName.ToUpperInvariant();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1298121364U)
			{
				if (num <= 403239141U)
				{
					if (num <= 138799941U)
					{
						if (num != 137133703U)
						{
							if (num == 138799941U)
							{
								if (Operators.CompareString(text, "MID", false) == 0)
								{
									if (arguments.Count == 2)
									{
										return Strings.Mid(Conversions.ToString(arguments[0]), Conversions.ToInteger(arguments[1]));
									}
									return Strings.Mid(Conversions.ToString(arguments[0]), Conversions.ToInteger(arguments[1]), Conversions.ToInteger(arguments[2]));
								}
							}
						}
						else if (Operators.CompareString(text, "LCASE", false) == 0)
						{
							object[] array;
							bool[] array2;
							object obj = NewLateBinding.LateGet(null, typeof(Strings), "LCase", array = new object[] { arguments[0] }, null, null, array2 = new bool[] { true });
							if (array2[0])
							{
								arguments[0] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
							}
							return obj;
						}
					}
					else if (num != 272375920U)
					{
						if (num == 403239141U)
						{
							if (Operators.CompareString(text, "SPACE", false) == 0)
							{
								return Strings.Space(Conversions.ToInteger(arguments[0]));
							}
						}
					}
					else if (Operators.CompareString(text, "LEFT", false) == 0)
					{
						return Strings.Left(Conversions.ToString(arguments[0]), Conversions.ToInteger(arguments[1]));
					}
				}
				else if (num <= 885080057U)
				{
					if (num != 462935941U)
					{
						if (num == 885080057U)
						{
							if (Operators.CompareString(text, "RTRIM", false) == 0)
							{
								return Strings.RTrim(Conversions.ToString(arguments[0]));
							}
						}
					}
					else if (Operators.CompareString(text, "RIGHT", false) == 0)
					{
						return Strings.Right(Conversions.ToString(arguments[0]), Conversions.ToInteger(arguments[1]));
					}
				}
				else if (num != 1007313916U)
				{
					if (num != 1256504687U)
					{
						if (num == 1298121364U)
						{
							if (Operators.CompareString(text, "CHR", false) == 0)
							{
								return Strings.Chr(Conversions.ToInteger(arguments[0]));
							}
						}
					}
					else if (Operators.CompareString(text, "FORMATDATETIME", false) == 0)
					{
						if (arguments.Count == 1)
						{
							return Strings.FormatDateTime(Conversions.ToDate(arguments[0]), DateFormat.GeneralDate);
						}
						return Strings.FormatDateTime(Conversions.ToDate(arguments[0]), (DateFormat)Conversions.ToInteger(arguments[1]));
					}
				}
				else if (Operators.CompareString(text, "UCASE", false) == 0)
				{
					object[] array;
					bool[] array2;
					object obj2 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array = new object[] { arguments[0] }, null, null, array2 = new bool[] { true });
					if (array2[0])
					{
						arguments[0] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					return obj2;
				}
			}
			else if (num <= 2013082377U)
			{
				if (num <= 1689094666U)
				{
					if (num != 1668566771U)
					{
						if (num == 1689094666U)
						{
							if (Operators.CompareString(text, "ASC", false) == 0)
							{
								object[] array;
								bool[] array2;
								object obj3 = NewLateBinding.LateGet(null, typeof(Strings), "Asc", array = new object[] { arguments[0] }, null, null, array2 = new bool[] { true });
								if (array2[0])
								{
									arguments[0] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
								}
								return obj3;
							}
						}
					}
					else if (Operators.CompareString(text, "LTRIM", false) == 0)
					{
						return Strings.LTrim(Conversions.ToString(arguments[0]));
					}
				}
				else if (num != 1958281977U)
				{
					if (num == 2013082377U)
					{
						if (Operators.CompareString(text, "TRIM", false) == 0)
						{
							return Strings.Trim(Conversions.ToString(arguments[0]));
						}
					}
				}
				else if (Operators.CompareString(text, "JOIN", false) == 0)
				{
					object[] array;
					bool[] array2;
					if (arguments.Count == 1)
					{
						object obj4 = NewLateBinding.LateGet(null, typeof(Strings), "Join", array = new object[] { arguments[0] }, null, null, array2 = new bool[] { true });
						if (array2[0])
						{
							arguments[0] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
						}
						return obj4;
					}
					object obj5 = NewLateBinding.LateGet(null, typeof(Strings), "Join", array = new object[]
					{
						arguments[0],
						arguments[1]
					}, null, null, array2 = new bool[] { true, true });
					if (array2[0])
					{
						arguments[0] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					if (array2[1])
					{
						arguments[1] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[1]));
					}
					return obj5;
				}
			}
			else if (num <= 2428475641U)
			{
				if (num != 2188201123U)
				{
					if (num == 2428475641U)
					{
						if (Operators.CompareString(text, "CHRW", false) == 0)
						{
							return Strings.ChrW(Conversions.ToInteger(arguments[0]));
						}
					}
				}
				else if (Operators.CompareString(text, "REPLACE", false) == 0)
				{
					if (arguments.Count == 3)
					{
						return Strings.Replace(Conversions.ToString(arguments[0]), Conversions.ToString(arguments[1]), Conversions.ToString(arguments[2]), 1, -1, CompareMethod.Binary);
					}
					if (arguments.Count == 4)
					{
						return Strings.Replace(Conversions.ToString(arguments[0]), Conversions.ToString(arguments[1]), Conversions.ToString(arguments[2]), Conversions.ToInteger(arguments[3]), -1, CompareMethod.Binary);
					}
					if (arguments.Count == 5)
					{
						return Strings.Replace(Conversions.ToString(arguments[0]), Conversions.ToString(arguments[1]), Conversions.ToString(arguments[2]), Conversions.ToInteger(arguments[3]), Conversions.ToInteger(arguments[4]), CompareMethod.Binary);
					}
					return Strings.Replace(Conversions.ToString(arguments[0]), Conversions.ToString(arguments[1]), Conversions.ToString(arguments[2]), Conversions.ToInteger(arguments[3]), Conversions.ToInteger(arguments[4]), (CompareMethod)Conversions.ToInteger(arguments[5]));
				}
			}
			else if (num != 2538062546U)
			{
				if (num != 3108329196U)
				{
					if (num == 3660632167U)
					{
						if (Operators.CompareString(text, "ASCW", false) == 0)
						{
							object[] array;
							bool[] array2;
							object obj6 = NewLateBinding.LateGet(null, typeof(Strings), "AscW", array = new object[] { arguments[0] }, null, null, array2 = new bool[] { true });
							if (array2[0])
							{
								arguments[0] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
							}
							return obj6;
						}
					}
				}
				else if (Operators.CompareString(text, "LEN", false) == 0)
				{
					return Strings.Len(RuntimeHelpers.GetObjectValue(arguments[0]));
				}
			}
			else if (Operators.CompareString(text, "FORMAT", false) == 0)
			{
				if (arguments.Count == 1)
				{
					return Strings.Format(RuntimeHelpers.GetObjectValue(arguments[0]), "");
				}
				return Strings.Format(RuntimeHelpers.GetObjectValue(arguments[0]), Conversions.ToString(arguments[1]));
			}
			throw new NotSupportedException(string.Format("Function <{0}> is not supported yet.", functionName));
		}
	}
}
