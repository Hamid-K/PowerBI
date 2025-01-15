using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000677 RID: 1655
	public class TraceContainers
	{
		// Token: 0x060037A3 RID: 14243 RVA: 0x000BB9E4 File Offset: 0x000B9BE4
		public static List<bool> GetUsabilityOfContainers()
		{
			List<bool> list = new List<bool>();
			List<Type> allContainers = TraceContainers.GetAllContainers();
			Type[] array = new Type[] { typeof(string) };
			string[] array2 = new string[1];
			Type[] array3 = new Type[]
			{
				typeof(string),
				typeof(string)
			};
			string[] array4 = new string[] { null, "DummyInstanceName" };
			foreach (Type type in allContainers)
			{
				Type[] array5;
				string[] array6;
				if ((bool)type.GetProperty("ConstructorNeedsInstanceName").GetValue(null, null))
				{
					array5 = array3;
					array6 = array4;
				}
				else
				{
					array5 = array;
					array6 = array2;
				}
				ConstructorInfo constructor = type.GetConstructor(array5);
				object[] array7 = array6;
				ITraceContainer traceContainer = (TraceContainer)constructor.Invoke(array7);
				list.Add(traceContainer.CanBeUsed);
			}
			return list;
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000BBAD4 File Offset: 0x000B9CD4
		public static List<string> GetAllContainerNames()
		{
			List<string> list = new List<string>();
			List<Type> allContainers = TraceContainers.GetAllContainers();
			Type[] array = new Type[] { typeof(string) };
			string[] array2 = new string[1];
			Type[] array3 = new Type[]
			{
				typeof(string),
				typeof(string)
			};
			string[] array4 = new string[] { null, "DummyInstanceName" };
			foreach (Type type in allContainers)
			{
				Type[] array5;
				string[] array6;
				if ((bool)type.GetProperty("ConstructorNeedsInstanceName").GetValue(null, null))
				{
					array5 = array3;
					array6 = array4;
				}
				else
				{
					array5 = array;
					array6 = array2;
				}
				ConstructorInfo constructor = type.GetConstructor(array5);
				object[] array7 = array6;
				ITraceContainer traceContainer = (TraceContainer)constructor.Invoke(array7);
				list.Add(traceContainer.Name);
			}
			return list;
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000BBBC4 File Offset: 0x000B9DC4
		public static List<string> GetAllContainerDisplayNames()
		{
			List<string> list = new List<string>();
			List<Type> allContainers = TraceContainers.GetAllContainers();
			Type[] array = new Type[] { typeof(string) };
			string[] array2 = new string[1];
			Type[] array3 = new Type[]
			{
				typeof(string),
				typeof(string)
			};
			string[] array4 = new string[] { null, "DummyInstanceName" };
			foreach (Type type in allContainers)
			{
				Type[] array5;
				string[] array6;
				if ((bool)type.GetProperty("ConstructorNeedsInstanceName").GetValue(null, null))
				{
					array5 = array3;
					array6 = array4;
				}
				else
				{
					array5 = array;
					array6 = array2;
				}
				ConstructorInfo constructor = type.GetConstructor(array5);
				object[] array7 = array6;
				ITraceContainer traceContainer = (TraceContainer)constructor.Invoke(array7);
				list.Add(traceContainer.DisplayName);
			}
			return list;
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000BBCB4 File Offset: 0x000B9EB4
		public static List<Type> GetAllContainers()
		{
			List<Type> list = new List<Type>();
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (type.IsClass && !(type.GetInterface("ITraceContainer") == null))
				{
					list.Add(type);
				}
			}
			int num = Globals.HighestContainerIdentifier + 1;
			List<Type> list2 = new List<Type>(num);
			for (int j = 0; j < num; j++)
			{
				list2.Add(null);
			}
			Type[] array = new Type[] { typeof(string) };
			string[] array2 = new string[1];
			foreach (Type type2 in list)
			{
				ConstructorInfo constructor = type2.GetConstructor(array);
				object[] array3 = array2;
				int identifier = ((TraceContainer)constructor.Invoke(array3)).Identifier;
				list2[identifier] = type2;
			}
			return list2;
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000BBDBC File Offset: 0x000B9FBC
		public static int[] InitializeIntArrayToMinus1(int numberOfElements)
		{
			int[] array = new int[numberOfElements];
			for (int i = 0; i < numberOfElements; i++)
			{
				array[i] = -1;
			}
			return array;
		}
	}
}
