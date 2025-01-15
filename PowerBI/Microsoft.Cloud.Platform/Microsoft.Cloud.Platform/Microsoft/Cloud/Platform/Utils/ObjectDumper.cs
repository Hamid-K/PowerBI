using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000265 RID: 613
	public static class ObjectDumper
	{
		// Token: 0x0600101C RID: 4124 RVA: 0x00037498 File Offset: 0x00035698
		public static string Dump(object what)
		{
			ExtendedStringBuilder extendedStringBuilder = new ExtendedStringBuilder(4, 0, null);
			ObjectDumper.Dump(what, extendedStringBuilder);
			return extendedStringBuilder.ToString();
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x000374BC File Offset: 0x000356BC
		public static void Dump(object what, ExtendedStringBuilder esb)
		{
			HashSet<object> hashSet = new HashSet<object>();
			ObjectDumper.Dump(what, esb, hashSet, 5);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x000374D8 File Offset: 0x000356D8
		private static void Dump(object what, ExtendedStringBuilder esb, HashSet<object> alreadySeen, int recursionLevel)
		{
			if (what == null)
			{
				esb.AppendLine("Null");
				return;
			}
			if (recursionLevel == 0)
			{
				esb.AppendLine("ObjectDumper.Dump(): Evaluation stopped at max recusrion depth");
				return;
			}
			if (!what.GetType().IsValueType)
			{
				if (alreadySeen.Contains(what))
				{
					esb.AppendLine("(Already seen)");
					return;
				}
				alreadySeen.Add(what);
			}
			recursionLevel--;
			if (ObjectDumper.JustUseToString(what))
			{
				esb.AppendLine(what.ToString());
				return;
			}
			IEnumerable enumerable = what as IEnumerable;
			if (enumerable != null)
			{
				int num = 0;
				foreach (object obj in enumerable)
				{
					esb.AppendLine("item[" + num + "]");
					num++;
					esb.Indent();
					ObjectDumper.Dump(obj, esb, alreadySeen, recursionLevel);
					esb.Unindent();
				}
				return;
			}
			esb.AppendLine("ToString()=" + what.ToString());
			Type type = what.GetType();
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				object obj2 = null;
				try
				{
					obj2 = propertyInfo.GetValue(what, null);
				}
				catch
				{
				}
				ObjectDumper.DumpPropertyOrField(propertyInfo.Name, obj2, esb, alreadySeen, recursionLevel);
			}
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				object obj3 = null;
				try
				{
					obj3 = fieldInfo.GetValue(what);
				}
				catch
				{
				}
				ObjectDumper.DumpPropertyOrField(fieldInfo.Name, obj3, esb, alreadySeen, recursionLevel);
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00037688 File Offset: 0x00035888
		private static void DumpPropertyOrField(string name, object value, ExtendedStringBuilder esb, HashSet<object> alreadySeen, int recursionLevel)
		{
			if (name.StartsWith("<", StringComparison.Ordinal))
			{
				return;
			}
			if (value == null)
			{
				esb.AppendLine(name + "=null");
				return;
			}
			if (ObjectDumper.JustUseToString(value))
			{
				esb.AppendLine(name + "=" + value.ToString());
				return;
			}
			esb.AppendLine(name + "=");
			esb.Indent();
			ObjectDumper.Dump(value, esb, alreadySeen, recursionLevel);
			esb.Unindent();
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00037700 File Offset: 0x00035900
		private static bool JustUseToString(object what)
		{
			Type type = what.GetType();
			return ObjectDumper.s_justUseToStringTypes.Contains(what.GetType()) || type.IsEnum || what is IObjectDumperUsesToString;
		}

		// Token: 0x04000601 RID: 1537
		private static HashSet<Type> s_justUseToStringTypes = new HashSet<Type>
		{
			typeof(byte),
			typeof(short),
			typeof(int),
			typeof(long),
			typeof(IntPtr),
			typeof(ushort),
			typeof(uint),
			typeof(ulong),
			typeof(UIntPtr),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(bool),
			typeof(DateTime),
			typeof(TimeSpan),
			typeof(DateTimeKind),
			typeof(DateTimeOffset),
			typeof(char),
			typeof(string),
			typeof(StringBuilder),
			typeof(Encoding),
			typeof(Guid),
			typeof(Type)
		};
	}
}
