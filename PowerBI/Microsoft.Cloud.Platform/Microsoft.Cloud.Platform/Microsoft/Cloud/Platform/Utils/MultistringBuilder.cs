using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200025F RID: 607
	public sealed class MultistringBuilder
	{
		// Token: 0x06001007 RID: 4103 RVA: 0x0000460D File Offset: 0x0000280D
		private MultistringBuilder()
		{
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00037260 File Offset: 0x00035460
		[CanBeNull]
		public static string Build(IList<string> list)
		{
			StringBuilder stringBuilder = null;
			foreach (string text in list)
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(text);
				}
				else
				{
					stringBuilder.Append('\0');
					stringBuilder.Append(text);
				}
			}
			if (stringBuilder == null)
			{
				return null;
			}
			stringBuilder.Append('\0');
			stringBuilder.Append('\0');
			return stringBuilder.ToString();
		}
	}
}
