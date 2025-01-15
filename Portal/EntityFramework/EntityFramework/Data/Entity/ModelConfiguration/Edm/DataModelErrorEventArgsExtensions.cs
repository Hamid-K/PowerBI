using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000161 RID: 353
	internal static class DataModelErrorEventArgsExtensions
	{
		// Token: 0x06001629 RID: 5673 RVA: 0x0003A0B4 File Offset: 0x000382B4
		public static string ToErrorMessage(this IEnumerable<DataModelErrorEventArgs> validationErrors)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(Strings.ValidationHeader);
			stringBuilder.AppendLine();
			foreach (DataModelErrorEventArgs dataModelErrorEventArgs in validationErrors)
			{
				stringBuilder.AppendLine(Strings.ValidationItemFormat(dataModelErrorEventArgs.Item, dataModelErrorEventArgs.PropertyName, dataModelErrorEventArgs.ErrorMessage));
			}
			return stringBuilder.ToString();
		}
	}
}
