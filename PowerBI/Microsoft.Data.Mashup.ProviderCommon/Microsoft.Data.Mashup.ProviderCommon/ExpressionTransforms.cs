using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using Microsoft.Mashup.EngineHost;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x0200000A RID: 10
	internal static class ExpressionTransforms
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000029A8 File Offset: 0x00000BA8
		public static string EnvironmentParameter
		{
			get
			{
				return ExpressionTransforms.environmentParameter;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000029AF File Offset: 0x00000BAF
		public static string ApplyResultTransform(string expression, string resultTransform)
		{
			return string.Format(CultureInfo.InvariantCulture, "{1}({0})", expression, resultTransform);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000029C2 File Offset: 0x00000BC2
		public static string ApplyTransformForColumnInfo(string expression)
		{
			return string.Format(CultureInfo.InvariantCulture, "let Top0 = (v) => if (v is table) then Table.FirstN(v, 0) else v in Top0({0})", expression);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000029D4 File Offset: 0x00000BD4
		public static string GetParameterSet(IList<IParameter> parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			for (int i = 0; i < parameters.Count; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				string text = parameters[i].ParameterName ?? (ExpressionTransforms.parameterPrefix + i.ToString());
				stringBuilder.Append(MashupEngines.Version1.EscapeIdentifier(text));
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A59 File Offset: 0x00000C59
		public static string ApplyEnvironmentRecordTransform(ProviderContext providerContext, string expression, IList<IParameter> parameters, IEnumerable<string> resourceNames)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} => let {1} = {2} in {3}", new object[]
			{
				ExpressionTransforms.GetParameterSet(parameters),
				ExpressionTransforms.environmentParameter,
				ExpressionTransforms.CreateEnvironmentRecord(providerContext, resourceNames, parameters),
				expression
			});
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A90 File Offset: 0x00000C90
		private static string CreateEnvironmentRecord(ProviderContext providerContext, IEnumerable<string> resourceNames, IList<IParameter> parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("let DatabaseObject.From = (v) => if (v is function or v is action) then v else Table.FromValue(v) in ");
			stringBuilder.Append("Value.ReplaceType(");
			ExpressionTransforms.AppendEnvironmentRecord(providerContext, stringBuilder, resourceNames, parameters, false);
			stringBuilder.Append(", type ");
			ExpressionTransforms.AppendEnvironmentRecord(providerContext, stringBuilder, resourceNames, parameters, true);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002AF0 File Offset: 0x00000CF0
		private static void AppendEnvironmentRecord(ProviderContext providerContext, StringBuilder sb, IEnumerable<string> resourceNames, IList<IParameter> parameters, bool forType)
		{
			sb.Append("[");
			HashSet<string> hashSet = null;
			bool flag = true;
			for (int i = 0; i < parameters.Count; i++)
			{
				if (!flag)
				{
					sb.Append(", ");
				}
				flag = false;
				IParameter parameter = parameters[i];
				string text = MashupEngines.Version1.EscapeIdentifier(parameter.ParameterName ?? (ExpressionTransforms.parameterPrefix + i.ToString()));
				if (forType)
				{
					if (parameter.TypeWasSet && parameter.DbType != DbType.Object)
					{
						sb.AppendFormat(CultureInfo.InvariantCulture, "{0} = {1}", text, ExpressionTransforms.GetMType(providerContext, parameter.DbType));
					}
					else
					{
						sb.AppendFormat(CultureInfo.InvariantCulture, "{0} = Value.Type({0})", text);
					}
				}
				else
				{
					sb.AppendFormat(CultureInfo.InvariantCulture, "{0} = {0}", text);
				}
				if (parameter.ParameterName != null)
				{
					if (hashSet == null)
					{
						hashSet = new HashSet<string>();
					}
					hashSet.Add(parameter.ParameterName);
				}
			}
			foreach (string text2 in resourceNames)
			{
				if (hashSet == null || !hashSet.Contains(text2))
				{
					if (!flag)
					{
						sb.Append(", ");
					}
					flag = false;
					string text3 = MashupEngines.Version1.EscapeIdentifier(text2);
					sb.AppendFormat(CultureInfo.InvariantCulture, "{0} = {1}DatabaseObject.From({0}){2}", text3, forType ? "Value.Type(" : "", forType ? ")" : "");
				}
			}
			sb.Append("]");
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002C88 File Offset: 0x00000E88
		private static string GetMType(ProviderContext providerContext, DbType dbType)
		{
			switch (dbType)
			{
			case DbType.AnsiString:
			case DbType.String:
			case DbType.AnsiStringFixedLength:
			case DbType.StringFixedLength:
				return "Text.Type";
			case DbType.Binary:
				return "Binary.Type";
			case DbType.Boolean:
				return "Logical.Type";
			case DbType.Currency:
				return "Currency.Type";
			case DbType.Date:
				return "Date.Type";
			case DbType.DateTime:
			case DbType.DateTime2:
				return "DateTime.Type";
			case DbType.Decimal:
				return "Decimal.Type";
			case DbType.Double:
				return "Double.Type";
			case DbType.Int16:
				return "Int16.Type";
			case DbType.Int32:
				return "Int32.Type";
			case DbType.Int64:
				return "Int64.Type";
			case DbType.SByte:
				return "Int8.Type";
			case DbType.Single:
				return "Single.Type";
			case DbType.Time:
				return "Time.Type";
			case DbType.VarNumeric:
				return "Number.Type";
			case DbType.DateTimeOffset:
				return "DateTimeZone.Type";
			}
			throw providerContext.CreateMashupKindException(ProviderErrorStrings.TypeNotSupported(dbType));
		}

		// Token: 0x04000019 RID: 25
		private static readonly string parameterPrefix = "_" + Guid.NewGuid().ToString("N") + "_";

		// Token: 0x0400001A RID: 26
		private static readonly string environmentParameter = ExpressionTransforms.parameterPrefix + "env";
	}
}
