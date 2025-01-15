using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x020000A1 RID: 161
	internal class ReflectionValueProvider : IValueProvider
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x00023B81 File Offset: 0x00021D81
		public ReflectionValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, "memberInfo");
			this._memberInfo = memberInfo;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00023B9C File Offset: 0x00021D9C
		public void SetValue(object target, [Nullable(2)] object value)
		{
			try
			{
				ReflectionUtils.SetMemberValue(this._memberInfo, target, value);
			}
			catch (Exception ex)
			{
				throw new JsonSerializationException("Error setting value to '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), ex);
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00023BF0 File Offset: 0x00021DF0
		[return: Nullable(2)]
		public object GetValue(object target)
		{
			object memberValue;
			try
			{
				PropertyInfo propertyInfo = this._memberInfo as PropertyInfo;
				if (propertyInfo != null && propertyInfo.PropertyType.IsByRef)
				{
					throw new InvalidOperationException("Could not create getter for {0}. ByRef return values are not supported.".FormatWith(CultureInfo.InvariantCulture, propertyInfo));
				}
				memberValue = ReflectionUtils.GetMemberValue(this._memberInfo, target);
			}
			catch (Exception ex)
			{
				throw new JsonSerializationException("Error getting value from '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), ex);
			}
			return memberValue;
		}

		// Token: 0x040002CA RID: 714
		private readonly MemberInfo _memberInfo;
	}
}
