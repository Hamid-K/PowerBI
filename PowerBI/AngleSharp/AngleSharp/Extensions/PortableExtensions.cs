using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace AngleSharp.Extensions
{
	// Token: 0x020000F1 RID: 241
	internal static class PortableExtensions
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x00035464 File Offset: 0x00033664
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string ConvertFromUtf32(this int utf32)
		{
			return char.ConvertFromUtf32(utf32);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0003546C File Offset: 0x0003366C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ConvertToUtf32(this string s, int index)
		{
			return char.ConvertToUtf32(s, index);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00035475 File Offset: 0x00033675
		public static Task Delay(this CancellationToken token, int timeout)
		{
			return Task.Delay(Math.Max(timeout, 4), token);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00035484 File Offset: 0x00033684
		public static bool Implements<T>(this Type type)
		{
			return type.GetTypeInfo().ImplementedInterfaces.Contains(typeof(T));
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000354A0 File Offset: 0x000336A0
		public static PropertyInfo[] GetProperties(this Type type)
		{
			return type.GetRuntimeProperties().ToArray<PropertyInfo>();
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000354AD File Offset: 0x000336AD
		public static ConstructorInfo[] GetConstructors(this Type type)
		{
			return type.GetTypeInfo().DeclaredConstructors.Where((ConstructorInfo c) => c.IsPublic && !c.IsStatic).ToArray<ConstructorInfo>();
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000354E3 File Offset: 0x000336E3
		public static FieldInfo GetField(this Type type, string name)
		{
			return type.GetTypeInfo().GetDeclaredField(name);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000354F1 File Offset: 0x000336F1
		public static PropertyInfo GetProperty(this Type type, string name)
		{
			return type.GetTypeInfo().GetDeclaredProperty(name);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000354FF File Offset: 0x000336FF
		public static bool IsAbstractClass(this Type type)
		{
			return type.GetTypeInfo().IsAbstract;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0003550C File Offset: 0x0003370C
		public static Type[] GetTypes(this Assembly assembly)
		{
			return assembly.DefinedTypes.Select((TypeInfo t) => t.AsType()).ToArray<Type>();
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0003553D File Offset: 0x0003373D
		public static Assembly GetAssembly(this Type type)
		{
			return type.GetTypeInfo().Assembly;
		}
	}
}
