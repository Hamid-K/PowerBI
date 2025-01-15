using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.PowerBI.Packaging.Project;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000034 RID: 52
	internal class V2ExplorationValidator
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00006078 File Offset: 0x00004278
		public static List<ExplorationDeserializationWarning> EnsureJTokenIsPerSchema(JToken jtk, JSchema schema, string filePath, params ErrorType[] treatAsWarnings)
		{
			List<ExplorationDeserializationWarning> list = new List<ExplorationDeserializationWarning>();
			IList<ValidationError> list2;
			if (SchemaExtensions.IsValid(jtk, schema, ref list2))
			{
				return list;
			}
			HashSet<ErrorType> hashSet = new HashSet<ErrorType>(treatAsWarnings);
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (ValidationError validationError in list2)
			{
				string text = V2ExplorationValidator.MakeMessage(validationError);
				if (hashSet.Contains(validationError.ErrorType))
				{
					list.Add(new ExplorationDeserializationWarning(filePath, text));
				}
				else
				{
					if (!flag)
					{
						stringBuilder.AppendLine(("'" + filePath + "':").ToString(CultureInfo.CurrentCulture));
						flag = true;
					}
					stringBuilder.AppendLine(text);
				}
			}
			string text2 = stringBuilder.ToString();
			if (!string.IsNullOrEmpty(text2))
			{
				throw new PBIProjectJsonSchemaValidationException(text2, filePath, list2, PBIProjectException.PBIProjectErrorCode.ObjectNotPerSchema);
			}
			return list;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000615C File Offset: 0x0000435C
		private static string MakeMessage(ValidationError error)
		{
			string text = ((!string.IsNullOrEmpty(error.Message)) ? (error.Message + " ") : "");
			string text2 = ((!string.IsNullOrEmpty(error.Path)) ? ("Path '" + error.Path + "', ") : "");
			return string.Concat(new string[]
			{
				text,
				text2,
				"line ",
				error.LineNumber.ToString(),
				", position ",
				error.LinePosition.ToString(),
				"."
			});
		}
	}
}
