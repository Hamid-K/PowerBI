using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200020E RID: 526
	public static class ExtendedEnum
	{
		// Token: 0x06000DE1 RID: 3553 RVA: 0x00030CBF File Offset: 0x0002EEBF
		public static IEnumerable<T> GetValues<T>()
		{
			return Enum.GetValues(typeof(T)).Cast<T>();
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00030CD5 File Offset: 0x0002EED5
		public static T Parse<T>(string value)
		{
			return ExtendedEnum.Parse<T>(value, ExtendedEnum.ParseOptions.None);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00030CDE File Offset: 0x0002EEDE
		public static T Parse<T>(string value, ExtendedEnum.ParseOptions options)
		{
			return (T)((object)Enum.Parse(typeof(T), value, options.HasFlag(ExtendedEnum.ParseOptions.IgnoreCase)));
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00030D08 File Offset: 0x0002EF08
		public static bool TryParse<T>(string value, out T enumMember)
		{
			bool flag;
			try
			{
				enumMember = (T)((object)Enum.Parse(typeof(T), value));
				flag = true;
			}
			catch (ArgumentException)
			{
				enumMember = default(T);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00030D54 File Offset: 0x0002EF54
		public static IEnumerable<T> GetFlags<T>(T enumValue)
		{
			IEnumerable<T> enumerable = null;
			Type underlyingType = Enum.GetUnderlyingType(typeof(T));
			if (underlyingType == typeof(int))
			{
				int value2 = Convert.ToInt32(enumValue, CultureInfo.InvariantCulture);
				enumerable = (from int v in ExtendedEnum.GetValues<T>()
					where (long)(v & value2) != 0L
					select v).Cast<T>();
			}
			else if (underlyingType == typeof(long))
			{
				long value = Convert.ToInt64(enumValue);
				enumerable = (from long v in ExtendedEnum.GetValues<T>()
					where (v & value) != 0L
					select v).Cast<T>();
			}
			ExtendedDiagnostics.EnsureNotNull<IEnumerable<T>>(enumerable, "GetFlags does not support enums of type: {0}".FormatWithInvariantCulture(new object[] { underlyingType.Name }));
			return enumerable;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00030E30 File Offset: 0x0002F030
		public static string GetSerializationStringValue<TEnum>(TEnum enumValue) where TEnum : struct
		{
			Type typeFromHandle = typeof(TEnum);
			ExtendedDiagnostics.EnsureArgumentNotNull<Attribute>(typeFromHandle.GetCustomAttributes().FirstOrDefault((Attribute attrib) => attrib.GetType() == typeof(DataContractAttribute)), "dataContractAttribute");
			MemberInfo memberInfo = typeFromHandle.GetMember(enumValue.ToString()).FirstOrDefault<MemberInfo>();
			ExtendedDiagnostics.EnsureArgumentNotNull<MemberInfo>(memberInfo, "enumValueMember");
			Attribute attribute = memberInfo.GetCustomAttributes().FirstOrDefault((Attribute attrib) => attrib.GetType() == typeof(EnumMemberAttribute));
			ExtendedDiagnostics.EnsureArgumentNotNull<Attribute>(attribute, "enumMemberAttribute");
			return ((EnumMemberAttribute)attribute).Value;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00030EDC File Offset: 0x0002F0DC
		public static TEnum GetFromSerializedStringValue<TEnum>(string serializedEnumValue) where TEnum : struct
		{
			Type typeFromHandle = typeof(TEnum);
			ExtendedDiagnostics.EnsureArgumentNotNull<Attribute>(typeFromHandle.GetCustomAttributes().FirstOrDefault((Attribute attrib) => attrib.GetType() == typeof(DataContractAttribute)), "dataContractAttribute");
			foreach (object obj in Enum.GetValues(typeof(TEnum)))
			{
				MemberInfo memberInfo = typeFromHandle.GetMember(obj.ToString()).FirstOrDefault<MemberInfo>();
				ExtendedDiagnostics.EnsureArgumentNotNull<MemberInfo>(memberInfo, "enumValueMember");
				Attribute attribute = memberInfo.GetCustomAttributes().FirstOrDefault((Attribute attrib) => attrib.GetType() == typeof(EnumMemberAttribute));
				if (attribute != null && string.Equals(serializedEnumValue, ((EnumMemberAttribute)attribute).Value))
				{
					return (TEnum)((object)obj);
				}
			}
			return default(TEnum);
		}

		// Token: 0x020006B3 RID: 1715
		[Flags]
		public enum ParseOptions
		{
			// Token: 0x040012FD RID: 4861
			None = 0,
			// Token: 0x040012FE RID: 4862
			IgnoreCase = 1
		}
	}
}
