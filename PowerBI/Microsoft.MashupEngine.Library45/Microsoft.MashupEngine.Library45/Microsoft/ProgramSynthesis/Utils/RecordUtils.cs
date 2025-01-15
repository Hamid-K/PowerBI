using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004F2 RID: 1266
	public static class RecordUtils
	{
		// Token: 0x06001C21 RID: 7201 RVA: 0x0005426C File Offset: 0x0005246C
		public static object GetRecordItem(this object record, int index)
		{
			Dictionary<Type, Func<object, object>[]> recordGetters = RecordUtils.RecordGetters;
			bool flag = false;
			object obj;
			try
			{
				Monitor.Enter(recordGetters, ref flag);
				Type t = record.GetType();
				Func<object, object>[] getters;
				obj = RecordUtils.RecordGetters.GetOrAdd(t, delegate(Type _)
				{
					getters = new Func<object, object>[t.GetGenericArguments().Length];
					for (int i = 0; i < getters.Length; i++)
					{
						getters[i] = t.GetField("Item" + (i + 1).ToString()).ToDelegateFieldLoad();
					}
					return getters;
				})[index](record);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(recordGetters);
				}
			}
			return obj;
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000542E0 File Offset: 0x000524E0
		public static MethodInfo GetRecordCreator(params Type[] types)
		{
			return RecordUtils.RecordCreators[types.Length].MakeGenericMethod(types);
		}

		// Token: 0x04000DB3 RID: 3507
		private static readonly Dictionary<Type, Func<object, object>[]> RecordGetters = new Dictionary<Type, Func<object, object>[]>();

		// Token: 0x04000DB4 RID: 3508
		private static readonly Dictionary<int, MethodInfo> RecordCreators = (from m in typeof(Record).GetMethods(BindingFlags.Static | BindingFlags.Public)
			where m.Name == "Create"
			select m).ToDictionary((MethodInfo m) => m.GetParameters().Length, (MethodInfo m) => m);
	}
}
