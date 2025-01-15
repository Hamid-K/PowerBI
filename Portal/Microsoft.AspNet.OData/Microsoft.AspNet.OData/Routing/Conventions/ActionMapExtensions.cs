using System;
using Microsoft.AspNet.OData.Interfaces;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x0200009F RID: 159
	internal static class ActionMapExtensions
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x000122F8 File Offset: 0x000104F8
		public static string FindMatchingAction(this IWebApiActionMap actionMap, params string[] targetActionNames)
		{
			foreach (string text in targetActionNames)
			{
				if (actionMap.Contains(text))
				{
					return text;
				}
			}
			return null;
		}
	}
}
