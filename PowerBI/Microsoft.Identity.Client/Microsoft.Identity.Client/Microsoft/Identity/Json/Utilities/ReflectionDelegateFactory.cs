using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Json.Serialization;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000066 RID: 102
	internal abstract class ReflectionDelegateFactory
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x00017820 File Offset: 0x00015A20
		[return: Nullable(new byte[] { 0, 0, 2 })]
		public Func<T, object> CreateGet<T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				if (propertyInfo.PropertyType.IsByRef)
				{
					throw new InvalidOperationException("Could not create getter for {0}. ByRef return values are not supported.".FormatWith(CultureInfo.InvariantCulture, propertyInfo));
				}
				return this.CreateGet<T>(propertyInfo);
			}
			else
			{
				FieldInfo fieldInfo = memberInfo as FieldInfo;
				if (fieldInfo != null)
				{
					return this.CreateGet<T>(fieldInfo);
				}
				throw new Exception("Could not create getter for {0}.".FormatWith(CultureInfo.InvariantCulture, memberInfo));
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001788C File Offset: 0x00015A8C
		[return: Nullable(new byte[] { 0, 0, 2 })]
		public Action<T, object> CreateSet<T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				return this.CreateSet<T>(propertyInfo);
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			if (fieldInfo != null)
			{
				return this.CreateSet<T>(fieldInfo);
			}
			throw new Exception("Could not create setter for {0}.".FormatWith(CultureInfo.InvariantCulture, memberInfo));
		}

		// Token: 0x06000581 RID: 1409
		[return: Nullable(new byte[] { 0, 0, 2 })]
		public abstract MethodCall<T, object> CreateMethodCall<T>(MethodBase method);

		// Token: 0x06000582 RID: 1410
		public abstract ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method);

		// Token: 0x06000583 RID: 1411
		public abstract Func<T> CreateDefaultConstructor<T>(Type type);

		// Token: 0x06000584 RID: 1412
		[return: Nullable(new byte[] { 0, 0, 2 })]
		public abstract Func<T, object> CreateGet<T>(PropertyInfo propertyInfo);

		// Token: 0x06000585 RID: 1413
		[return: Nullable(new byte[] { 0, 0, 2 })]
		public abstract Func<T, object> CreateGet<T>(FieldInfo fieldInfo);

		// Token: 0x06000586 RID: 1414
		[return: Nullable(new byte[] { 0, 0, 2 })]
		public abstract Action<T, object> CreateSet<T>(FieldInfo fieldInfo);

		// Token: 0x06000587 RID: 1415
		[return: Nullable(new byte[] { 0, 0, 2 })]
		public abstract Action<T, object> CreateSet<T>(PropertyInfo propertyInfo);
	}
}
