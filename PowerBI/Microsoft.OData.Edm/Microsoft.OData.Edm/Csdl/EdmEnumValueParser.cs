using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x0200014B RID: 331
	internal static class EdmEnumValueParser
	{
		// Token: 0x06000850 RID: 2128 RVA: 0x00015F68 File Offset: 0x00014168
		internal static bool TryParseEnumMember(string value, IEdmModel model, EdmLocation location, out IEnumerable<IEdmEnumMember> result)
		{
			result = null;
			if (value == null || model == null)
			{
				return false;
			}
			bool flag = false;
			string[] array = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (!array.Any<string>())
			{
				return false;
			}
			string text = array[0].Split(new char[] { '/' }).FirstOrDefault<string>();
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			IEdmEnumType edmEnumType = model.FindType(text) as IEdmEnumType;
			if (edmEnumType == null)
			{
				edmEnumType = new UnresolvedEnumType(text, location);
				flag = true;
			}
			else if (array.Count<string>() > 1 && (!edmEnumType.IsFlags || !EdmEnumValueParser.IsEnumIntegerType(edmEnumType)))
			{
				return false;
			}
			List<IEdmEnumMember> list = new List<IEdmEnumMember>();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text2 = array2[i];
				string[] path = text2.Split(new char[] { '/' });
				if (path.Count<string>() != 2)
				{
					return false;
				}
				if (path[0] != text)
				{
					return false;
				}
				if (!flag)
				{
					IEdmEnumMember edmEnumMember = edmEnumType.Members.SingleOrDefault((IEdmEnumMember m) => m.Name == path[1]);
					if (edmEnumMember == null)
					{
						return false;
					}
					list.Add(edmEnumMember);
				}
				else
				{
					list.Add(new UnresolvedEnumMember(path[1], edmEnumType, location));
				}
			}
			result = list;
			return true;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000160B4 File Offset: 0x000142B4
		internal static bool IsEnumIntegerType(IEdmEnumType enumType)
		{
			return enumType.UnderlyingType.PrimitiveKind == EdmPrimitiveTypeKind.Byte || enumType.UnderlyingType.PrimitiveKind == EdmPrimitiveTypeKind.SByte || enumType.UnderlyingType.PrimitiveKind == EdmPrimitiveTypeKind.Int16 || enumType.UnderlyingType.PrimitiveKind == EdmPrimitiveTypeKind.Int32 || enumType.UnderlyingType.PrimitiveKind == EdmPrimitiveTypeKind.Int64;
		}
	}
}
