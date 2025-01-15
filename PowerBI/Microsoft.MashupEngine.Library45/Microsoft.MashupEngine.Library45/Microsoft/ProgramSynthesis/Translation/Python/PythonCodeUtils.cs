using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Translation.Python
{
	// Token: 0x0200030A RID: 778
	public static class PythonCodeUtils
	{
		// Token: 0x060010E2 RID: 4322 RVA: 0x00030653 File Offset: 0x0002E853
		internal static SSALiteral MkPyLiteral(bool b)
		{
			if (!b)
			{
				return PythonCodeUtils.PyFalseLiteral;
			}
			return PythonCodeUtils.PyTrueLiteral;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00030663 File Offset: 0x0002E863
		internal static SSALiteral MkPyLiteral(int k)
		{
			return new SSALiteral(typeof(int), string.Format("{0}", k));
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00030684 File Offset: 0x0002E884
		public static SSARValue PythonConstantExpressionEvaluator(SSAFunctionApplication app)
		{
			string functionName = app.FunctionName;
			IReadOnlyList<SSAValue> functionArguments = app.FunctionArguments;
			if (functionName == "operators.__and__")
			{
				SSAValue[] array = functionArguments.Where((SSAValue x) => !x.Equals(PythonCodeUtils.PyTrueLiteral)).ToArray<SSAValue>();
				if (array.Length == functionArguments.Count)
				{
					return app;
				}
				if (array.Length == 0)
				{
					return PythonCodeUtils.PyTrueLiteral;
				}
				if (array.Length == 1)
				{
					return (SSARValue)array[0];
				}
				return new SSAFunctionApplication(app.ValueType, functionName, array, app.IsFunctionLocal);
			}
			else
			{
				if (!(functionName == "operators.__or__"))
				{
					if (functionName == "operators.__ite__")
					{
						if (functionArguments[0].Equals(PythonCodeUtils.PyTrueLiteral))
						{
							SSARValue ssarvalue = functionArguments[1] as SSARValue;
							if (ssarvalue != null)
							{
								return ssarvalue;
							}
						}
						if (functionArguments[0].Equals(PythonCodeUtils.PyFalseLiteral))
						{
							SSARValue ssarvalue2 = functionArguments[2] as SSARValue;
							if (ssarvalue2 != null)
							{
								return ssarvalue2;
							}
						}
					}
					else if (functionArguments.All((SSAValue x) => x is SSALiteral) && functionName.StartsWith("operators."))
					{
						List<string> list = (from SSALiteral x in functionArguments
							select x.LiteralString).ToList<string>();
						if (functionName == "operators.__equals__" || functionName == "operators.__not_equals__")
						{
							bool flag = list[0].Equals(list[1]);
							return PythonCodeUtils.MkPyLiteral((functionName == "operators.__equals__") ? flag : (!flag));
						}
						int num;
						int num2;
						if (list.Count == 2 && int.TryParse(list[0], out num) && int.TryParse(list[1], out num2))
						{
							if (functionName == "operators.__lte__")
							{
								return PythonCodeUtils.MkPyLiteral(num <= num2);
							}
							if (functionName == "operators.__minus__")
							{
								return PythonCodeUtils.MkPyLiteral(num - num2);
							}
							if (functionName == "operators.__mod__")
							{
								return PythonCodeUtils.MkPyLiteral(num % num2);
							}
							if (!(functionName == "operators.__add__"))
							{
								return app;
							}
							return PythonCodeUtils.MkPyLiteral(num + num2);
						}
					}
					return app;
				}
				SSAValue[] array2 = functionArguments.Where((SSAValue x) => !x.Equals(PythonCodeUtils.PyFalseLiteral)).ToArray<SSAValue>();
				if (array2.Length == functionArguments.Count)
				{
					return app;
				}
				if (array2.Length == 0)
				{
					return PythonCodeUtils.PyFalseLiteral;
				}
				if (array2.Length == 1)
				{
					return (SSARValue)array2[0];
				}
				return new SSAFunctionApplication(app.ValueType, functionName, array2, app.IsFunctionLocal);
			}
		}

		// Token: 0x0400082F RID: 2095
		internal static SSALiteral PyTrueLiteral = new SSALiteral(typeof(bool), "True");

		// Token: 0x04000830 RID: 2096
		internal static SSALiteral PyFalseLiteral = new SSALiteral(typeof(bool), "False");
	}
}
