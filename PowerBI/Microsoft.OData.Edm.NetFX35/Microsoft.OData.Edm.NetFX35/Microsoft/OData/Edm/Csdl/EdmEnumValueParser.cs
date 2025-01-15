using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000014 RID: 20
	internal static class EdmEnumValueParser
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000029F0 File Offset: 0x00000BF0
		internal static bool TryParseEnumMember(string value, IEdmModel model, EdmLocation location, out IEnumerable<IEdmEnumMember> result)
		{
			result = null;
			if (value == null || model == null)
			{
				return false;
			}
			bool flag = false;
			string[] array = value.Split(new char[] { ' ' }, 1);
			if (!Enumerable.Any<string>(array))
			{
				return false;
			}
			string text = Enumerable.FirstOrDefault<string>(array[0].Split(new char[] { '/' }));
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
			else if (Enumerable.Count<string>(array) > 1 && (!edmEnumType.IsFlags || !EdmEnumValueParser.IsEnumIntergeType(edmEnumType)))
			{
				return false;
			}
			List<IEdmEnumMember> list = new List<IEdmEnumMember>();
			string[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				string text2 = array2[i];
				string[] path = text2.Split(new char[] { '/' });
				bool flag2;
				if (Enumerable.Count<string>(path) != 2)
				{
					flag2 = false;
				}
				else
				{
					if (!(path[0] != text))
					{
						if (!flag)
						{
							IEdmEnumMember edmEnumMember = Enumerable.SingleOrDefault<IEdmEnumMember>(edmEnumType.Members, (IEdmEnumMember m) => m.Name == path[1]);
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
						i++;
						continue;
					}
					flag2 = false;
				}
				return flag2;
			}
			result = list;
			return true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002B60 File Offset: 0x00000D60
		internal static bool IsEnumIntergeType(IEdmEnumType enumType)
		{
			return enumType.UnderlyingType.PrimitiveKind == EdmPrimitiveTypeKind.Byte || enumType.UnderlyingType.PrimitiveKind == EdmPrimitiveTypeKind.SByte || enumType.UnderlyingType.PrimitiveKind == EdmPrimitiveTypeKind.Int16 || enumType.UnderlyingType.PrimitiveKind == EdmPrimitiveTypeKind.Int32 || enumType.UnderlyingType.PrimitiveKind == EdmPrimitiveTypeKind.Int64;
		}
	}
}
