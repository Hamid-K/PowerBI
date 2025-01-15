using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001694 RID: 5780
	internal static class MessageUtils
	{
		// Token: 0x06009249 RID: 37449 RVA: 0x001E53F0 File Offset: 0x001E35F0
		public static Value MarshalValue(object obj)
		{
			if (obj is IPiiFree)
			{
				return ((IPiiFree)obj).Value.NewMeta(ValueException.NonPii);
			}
			if (obj is Value && ((Value)obj).IsText)
			{
				return ((Value)obj).AsText;
			}
			if (obj is Enum)
			{
				return TextValue.New(obj.ToString()).NewMeta(ValueException.NonPii);
			}
			Value value;
			if (ValueMarshaller.TryMarshalFromClr(obj, out value))
			{
				return value;
			}
			return TextValue.New(obj.ToString());
		}

		// Token: 0x0600924A RID: 37450 RVA: 0x001E5474 File Offset: 0x001E3674
		public static Value[] MarshalValue(object[] objects)
		{
			Value[] array = new Value[objects.Length];
			for (int i = 0; i < objects.Length; i++)
			{
				array[i] = MessageUtils.MarshalValue(objects[i]);
			}
			return array;
		}
	}
}
