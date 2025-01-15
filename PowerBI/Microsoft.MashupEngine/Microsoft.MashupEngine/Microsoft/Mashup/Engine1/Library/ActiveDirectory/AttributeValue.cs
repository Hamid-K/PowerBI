using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FFD RID: 4093
	internal class AttributeValue
	{
		// Token: 0x17001EB1 RID: 7857
		// (get) Token: 0x06006B4A RID: 27466 RVA: 0x00171799 File Offset: 0x0016F999
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06006B4B RID: 27467 RVA: 0x001717A1 File Offset: 0x0016F9A1
		public AttributeValue(string value)
		{
			this.value = value;
		}

		// Token: 0x06006B4C RID: 27468 RVA: 0x001717B0 File Offset: 0x0016F9B0
		public static AttributeValue New(bool value)
		{
			if (!value)
			{
				return AttributeValue.False;
			}
			return AttributeValue.True;
		}

		// Token: 0x06006B4D RID: 27469 RVA: 0x001717C0 File Offset: 0x0016F9C0
		public static AttributeValue New(long value)
		{
			return new AttributeValue(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06006B4E RID: 27470 RVA: 0x001717D4 File Offset: 0x0016F9D4
		public static string Escape(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c <= '*')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '(':
							stringBuilder.Append("\\28");
							break;
						case ')':
							stringBuilder.Append("\\29");
							break;
						case '*':
							stringBuilder.Append("\\2a");
							break;
						default:
							goto IL_0094;
						}
					}
					else
					{
						stringBuilder.Append("\\00");
					}
				}
				else if (c != '/')
				{
					if (c != '\\')
					{
						goto IL_0094;
					}
					stringBuilder.Append("\\5c");
				}
				else
				{
					stringBuilder.Append("\\2f");
				}
				IL_009C:
				i++;
				continue;
				IL_0094:
				stringBuilder.Append(c);
				goto IL_009C;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006B4F RID: 27471 RVA: 0x00171893 File Offset: 0x0016FA93
		public static AttributeValue Escaped(string value)
		{
			return new AttributeValue(AttributeValue.Escape(value));
		}

		// Token: 0x04003BAB RID: 15275
		public const string Wildcard = "*";

		// Token: 0x04003BAC RID: 15276
		public static readonly AttributeValue None = new AttributeValue("");

		// Token: 0x04003BAD RID: 15277
		public static readonly AttributeValue Any = new AttributeValue("*");

		// Token: 0x04003BAE RID: 15278
		public static readonly AttributeValue True = new AttributeValue("TRUE");

		// Token: 0x04003BAF RID: 15279
		public static readonly AttributeValue False = new AttributeValue("FALSE");

		// Token: 0x04003BB0 RID: 15280
		private readonly string value;
	}
}
