using System;
using System.Globalization;
using System.Reflection;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200040E RID: 1038
	internal class GetFunc : PropertyFunc
	{
		// Token: 0x06002415 RID: 9237 RVA: 0x0006E2B0 File Offset: 0x0006C4B0
		private GetFunc()
		{
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x0006E9EC File Offset: 0x0006CBEC
		public override object Invoke(IReadablePropertyContext context, object[] args)
		{
			if (args.Length != 1)
			{
				throw new ArgumentException("Invalid arguments to GetFunc");
			}
			string text = (string)args[0];
			object obj = context;
			while (obj != null && !string.IsNullOrEmpty(text))
			{
				int num = 0;
				int i;
				for (i = 0; i < text.Length; i++)
				{
					if (text[i] == '[')
					{
						num++;
					}
					else if (text[i] == ']')
					{
						num--;
						if (num < 0)
						{
							num = int.MinValue;
						}
					}
					else if (text[i] == '.' && num == 0)
					{
						break;
					}
				}
				string text2;
				if (i < text.Length)
				{
					text2 = text.Substring(0, i);
					text = text.Substring(i + 1);
				}
				else
				{
					text2 = text;
					text = null;
				}
				IReadablePropertyContext readablePropertyContext = obj as IReadablePropertyContext;
				if (readablePropertyContext != null)
				{
					obj = readablePropertyContext[text2];
				}
				else
				{
					Type type = obj.GetType();
					if (text2.EndsWith("]", StringComparison.Ordinal))
					{
						i = text2.LastIndexOf('[');
						if (i < 0)
						{
							throw new ArgumentException("Invalid property name");
						}
						string text3 = text2.Substring(0, i);
						string text4 = text2.Substring(i + 1, text2.Length - i - 2);
						obj = GetFunc.InvokeMethod(type, text3, text4, obj);
					}
					else
					{
						obj = type.InvokeMember(text2, BindingFlags.GetProperty, null, obj, null, CultureInfo.InvariantCulture);
					}
				}
			}
			return obj;
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x0006EB30 File Offset: 0x0006CD30
		private static object InvokeMethod(Type type, string methodName, string parameters, object obj)
		{
			string[] array = parameters.Split(GetFunc.s_delimiters, StringSplitOptions.RemoveEmptyEntries);
			foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public))
			{
				if (!(methodInfo.Name != methodName))
				{
					ParameterInfo[] parameters2 = methodInfo.GetParameters();
					if (parameters2.Length == array.Length)
					{
						object[] array2 = new object[array.Length];
						int num = 0;
						while (num < array.Length && GetFunc.Convert(parameters2[num].ParameterType, array[num], out array2[num]))
						{
							num++;
						}
						if (num == array.Length)
						{
							return type.InvokeMember(methodName, BindingFlags.InvokeMethod, null, obj, array2, CultureInfo.InvariantCulture);
						}
					}
				}
			}
			throw new ArgumentException("Unable to find matching method:" + methodName);
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x0006EBF8 File Offset: 0x0006CDF8
		private static bool Convert(Type type, string value, out object result)
		{
			if (type == typeof(string))
			{
				result = value;
				return true;
			}
			try
			{
				MethodInfo method = type.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, GetFunc.s_stringType, null);
				if (method != null)
				{
					result = method.Invoke(null, new object[] { value });
					return true;
				}
				ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, GetFunc.s_stringType, null);
				if (constructor != null)
				{
					result = constructor.Invoke(new object[] { value });
					return true;
				}
			}
			catch (TargetInvocationException)
			{
			}
			result = null;
			return false;
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x0006EC94 File Offset: 0x0006CE94
		public override string ToString()
		{
			return "get";
		}

		// Token: 0x04001651 RID: 5713
		public static readonly GetFunc Singleton = new GetFunc();

		// Token: 0x04001652 RID: 5714
		private static readonly char[] s_delimiters = new char[] { ',', ' ' };

		// Token: 0x04001653 RID: 5715
		private static readonly Type[] s_stringType = new Type[] { typeof(string) };
	}
}
