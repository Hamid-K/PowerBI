using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200005D RID: 93
	public static class PrimitiveValueFactory
	{
		// Token: 0x0600017E RID: 382 RVA: 0x000031DC File Offset: 0x000013DC
		public static bool TryCreateFromObject(object value, out PrimitiveValue primitiveValue)
		{
			if (value == null)
			{
				primitiveValue = PrimitiveValue.Null;
				return true;
			}
			if (value is int)
			{
				int num = (int)value;
				primitiveValue = (long)num;
				return true;
			}
			if (value is long)
			{
				long num2 = (long)value;
				primitiveValue = num2;
				return true;
			}
			if (value is double)
			{
				double num3 = (double)value;
				primitiveValue = num3;
				return true;
			}
			if (value is decimal)
			{
				decimal num4 = (decimal)value;
				primitiveValue = num4;
				return true;
			}
			string text = value as string;
			if (text != null)
			{
				primitiveValue = text;
				return true;
			}
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				primitiveValue = dateTime;
				return true;
			}
			if (value is bool)
			{
				bool flag = (bool)value;
				primitiveValue = flag;
				return true;
			}
			byte[] array = value as byte[];
			if (array == null)
			{
				primitiveValue = null;
				return false;
			}
			primitiveValue = array;
			return true;
		}
	}
}
