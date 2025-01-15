using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200005B RID: 91
	public static class DataServiceProviderMethods
	{
		// Token: 0x06000241 RID: 577 RVA: 0x0000C7A9 File Offset: 0x0000A9A9
		public static object GetValue(object value, IEdmProperty property)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		public static IEnumerable<T> GetSequenceValue<T>(object value, IEdmProperty property)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000C7B7 File Offset: 0x0000A9B7
		public static object Convert(object value, IEdmTypeReference typeReference)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000C7BE File Offset: 0x0000A9BE
		public static bool TypeIs(object value, IEdmTypeReference typeReference)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000C7C5 File Offset: 0x0000A9C5
		public static int Compare(string left, string right)
		{
			return Comparer<string>.Default.Compare(left, right);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000C7D3 File Offset: 0x0000A9D3
		public static int Compare(bool left, bool right)
		{
			return Comparer<bool>.Default.Compare(left, right);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000C7E1 File Offset: 0x0000A9E1
		public static int Compare(bool? left, bool? right)
		{
			return Comparer<bool?>.Default.Compare(left, right);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000C7EF File Offset: 0x0000A9EF
		public static int Compare(Guid left, Guid right)
		{
			return Comparer<Guid>.Default.Compare(left, right);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000C7FD File Offset: 0x0000A9FD
		public static int Compare(Guid? left, Guid? right)
		{
			return Comparer<Guid?>.Default.Compare(left, right);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000C80C File Offset: 0x0000AA0C
		public static bool AreByteArraysEqual(byte[] left, byte[] right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000C84A File Offset: 0x0000AA4A
		public static bool AreByteArraysNotEqual(byte[] left, byte[] right)
		{
			return !DataServiceProviderMethods.AreByteArraysEqual(left, right);
		}

		// Token: 0x04000214 RID: 532
		internal static readonly MethodInfo GetValueMethodInfo = typeof(DataServiceProviderMethods).GetMethod("GetValue", 24, null, new Type[]
		{
			typeof(object),
			typeof(IEdmProperty)
		}, null);

		// Token: 0x04000215 RID: 533
		internal static readonly MethodInfo GetSequenceValueMethodInfo = typeof(DataServiceProviderMethods).GetMethod("GetSequenceValue", 24, null, new Type[]
		{
			typeof(object),
			typeof(IEdmProperty)
		}, null);

		// Token: 0x04000216 RID: 534
		internal static readonly MethodInfo ConvertMethodInfo = typeof(DataServiceProviderMethods).GetMethod("Convert", 24);

		// Token: 0x04000217 RID: 535
		internal static readonly MethodInfo TypeIsMethodInfo = typeof(DataServiceProviderMethods).GetMethod("TypeIs", 24);

		// Token: 0x04000218 RID: 536
		internal static readonly MethodInfo StringCompareMethodInfo = Enumerable.Single<MethodInfo>(typeof(DataServiceProviderMethods).GetMethods(24), (MethodInfo m) => m.Name == "Compare" && m.GetParameters()[0].ParameterType == typeof(string));

		// Token: 0x04000219 RID: 537
		internal static readonly MethodInfo BoolCompareMethodInfo = Enumerable.Single<MethodInfo>(typeof(DataServiceProviderMethods).GetMethods(24), (MethodInfo m) => m.Name == "Compare" && m.GetParameters()[0].ParameterType == typeof(bool));

		// Token: 0x0400021A RID: 538
		internal static readonly MethodInfo BoolCompareMethodInfoNullable = Enumerable.Single<MethodInfo>(typeof(DataServiceProviderMethods).GetMethods(24), (MethodInfo m) => m.Name == "Compare" && m.GetParameters()[0].ParameterType == typeof(bool?));

		// Token: 0x0400021B RID: 539
		internal static readonly MethodInfo GuidCompareMethodInfo = Enumerable.Single<MethodInfo>(typeof(DataServiceProviderMethods).GetMethods(24), (MethodInfo m) => m.Name == "Compare" && m.GetParameters()[0].ParameterType == typeof(Guid));

		// Token: 0x0400021C RID: 540
		internal static readonly MethodInfo GuidCompareMethodInfoNullable = Enumerable.Single<MethodInfo>(typeof(DataServiceProviderMethods).GetMethods(24), (MethodInfo m) => m.Name == "Compare" && m.GetParameters()[0].ParameterType == typeof(Guid?));

		// Token: 0x0400021D RID: 541
		internal static readonly MethodInfo AreByteArraysEqualMethodInfo = typeof(DataServiceProviderMethods).GetMethod("AreByteArraysEqual", 24);

		// Token: 0x0400021E RID: 542
		internal static readonly MethodInfo AreByteArraysNotEqualMethodInfo = typeof(DataServiceProviderMethods).GetMethod("AreByteArraysNotEqual", 24);
	}
}
