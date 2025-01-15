using System;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DAA RID: 3498
	internal static class CdpaListType
	{
		// Token: 0x06005F2E RID: 24366 RVA: 0x0014826C File Offset: 0x0014646C
		public static string ToItemType(string listType)
		{
			if (listType == "string-list")
			{
				return "string";
			}
			if (listType == "number-list")
			{
				return "number";
			}
			if (!(listType == "date-time-list"))
			{
				throw new NotSupportedException();
			}
			return "date-time";
		}

		// Token: 0x06005F2F RID: 24367 RVA: 0x001482BC File Offset: 0x001464BC
		public static string FromItemType(string itemType)
		{
			if (itemType == "string")
			{
				return "string-list";
			}
			if (itemType == "number")
			{
				return "number-list";
			}
			if (!(itemType == "date-time"))
			{
				throw new NotSupportedException();
			}
			return "date-time-list";
		}

		// Token: 0x04003438 RID: 13368
		public const string String = "string-list";

		// Token: 0x04003439 RID: 13369
		public const string Number = "number-list";

		// Token: 0x0400343A RID: 13370
		public const string DateTime = "date-time-list";
	}
}
