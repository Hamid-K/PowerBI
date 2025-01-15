using System;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000536 RID: 1334
	public abstract class EventCategoryAttribute : Attribute
	{
		// Token: 0x060028CE RID: 10446 RVA: 0x00092644 File Offset: 0x00090844
		public static string GetCategoryViewName<TAttribute>() where TAttribute : EventCategoryAttribute
		{
			string text = typeof(TAttribute).Name;
			if (text.EndsWith("Attribute", StringComparison.Ordinal))
			{
				text = text.Substring(0, text.Length - "Attribute".Length);
			}
			return "V" + text;
		}
	}
}
