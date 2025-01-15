using System;
using System.Collections.Generic;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000401 RID: 1025
	internal abstract class PropertyFunc
	{
		// Token: 0x060023DB RID: 9179 RVA: 0x0006E0A4 File Offset: 0x0006C2A4
		private static Dictionary<string, PropertyFunc> CreateFuncTable()
		{
			Dictionary<string, PropertyFunc> dictionary = new Dictionary<string, PropertyFunc>();
			PropertyFunc.Set(dictionary, NotFunc.Singleton);
			PropertyFunc.Set(dictionary, OrFunc.Singleton);
			PropertyFunc.Set(dictionary, AndFunc.Singleton);
			PropertyFunc.Set(dictionary, OrFunc.Singleton);
			PropertyFunc.Set(dictionary, ComparisonFunc.EQ);
			PropertyFunc.Set(dictionary, ComparisonFunc.NE);
			PropertyFunc.Set(dictionary, ComparisonFunc.GT);
			PropertyFunc.Set(dictionary, ComparisonFunc.GE);
			PropertyFunc.Set(dictionary, ComparisonFunc.LT);
			PropertyFunc.Set(dictionary, ComparisonFunc.LE);
			PropertyFunc.Set(dictionary, MatchFunc.Singleton);
			PropertyFunc.Set(dictionary, GetFunc.Singleton);
			return dictionary;
		}

		// Token: 0x060023DD RID: 9181
		public abstract object Invoke(IReadablePropertyContext context, object[] args);

		// Token: 0x060023DE RID: 9182 RVA: 0x0006E13C File Offset: 0x0006C33C
		internal virtual object Invoke(IReadablePropertyContext context, FuncArguments args)
		{
			object[] array;
			if (args != null)
			{
				array = new object[args.Count];
				for (int i = 0; i < args.Count; i++)
				{
					array[i] = args[i].Eval(context);
				}
			}
			else
			{
				array = null;
			}
			return this.Invoke(context, array);
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00008948 File Offset: 0x00006B48
		public virtual PropertyFunc Bind(FuncArguments args)
		{
			return this;
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x0006E185 File Offset: 0x0006C385
		private static void Set(Dictionary<string, PropertyFunc> table, PropertyFunc func)
		{
			table[func.ToString()] = func;
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x0006E194 File Offset: 0x0006C394
		public static void Add(PropertyFunc func)
		{
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			lock (PropertyFunc.s_tableLock)
			{
				Dictionary<string, PropertyFunc> dictionary = new Dictionary<string, PropertyFunc>(PropertyFunc.s_table);
				PropertyFunc.Set(dictionary, func);
				PropertyFunc.s_table = dictionary;
			}
			EventLogWriter.WriteVerbose<PropertyFunc>("Trace", "Adding function {0}", func);
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x0006E1FC File Offset: 0x0006C3FC
		public static PropertyFunc Get(string name)
		{
			PropertyFunc propertyFunc;
			if (PropertyFunc.s_table.TryGetValue(name, out propertyFunc))
			{
				return propertyFunc;
			}
			return (PropertyFunc)Utility.CreateInstanceByReflection(name);
		}

		// Token: 0x04001639 RID: 5689
		private static object s_tableLock = new object();

		// Token: 0x0400163A RID: 5690
		private static Dictionary<string, PropertyFunc> s_table = PropertyFunc.CreateFuncTable();
	}
}
