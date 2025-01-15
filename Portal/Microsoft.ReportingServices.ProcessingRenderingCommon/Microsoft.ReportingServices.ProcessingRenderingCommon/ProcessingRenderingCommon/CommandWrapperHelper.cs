using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ProcessingRenderingCommon
{
	// Token: 0x020000CD RID: 205
	public sealed class CommandWrapperHelper
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x00012DDD File Offset: 0x00010FDD
		internal static string WriteMultiValueParametersIntoCommandText(global::System.Data.IDbCommand command, ParameterCollectionWrapper parameters, int sysParameterCount, ServerType serverType)
		{
			return CommandWrapperHelper.InternalWriteMultiValueParametersIntoCommandText(command, CommandWrapperHelper.ParameterCollectionToIEnumerable(parameters).ToList<Microsoft.ReportingServices.DataProcessing.IDataParameter>(), sysParameterCount, serverType);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00012DF2 File Offset: 0x00010FF2
		internal static IEnumerable<Microsoft.ReportingServices.DataProcessing.IDataParameter> ParameterCollectionToIEnumerable(ParameterCollectionWrapper parameters)
		{
			if (parameters != null)
			{
				foreach (ParameterWrapper parameterWrapper in parameters)
				{
					yield return parameterWrapper;
				}
				ParameterCollectionWrapper.Enumerator enumerator = null;
			}
			yield break;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00012E04 File Offset: 0x00011004
		public static string InternalWriteMultiValueParametersIntoCommandText(global::System.Data.IDbCommand command, IEnumerable<Microsoft.ReportingServices.DataProcessing.IDataParameter> parameters, int sysParameterCount, ServerType serverType)
		{
			if (0 >= sysParameterCount)
			{
				return command.CommandText;
			}
			StringBuilder stringBuilder = null;
			RegexOptions regexOptions = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline;
			string text = Regex.Escape("-+()#,:&*/\\^<=>");
			string text2 = "([" + text + "\\s])";
			string text3 = "([" + text + "\\s]|$)";
			bool flag = global::System.Data.CommandType.StoredProcedure == command.CommandType;
			if (!flag)
			{
				stringBuilder = new StringBuilder(command.CommandText);
			}
			int num = 0;
			foreach (Microsoft.ReportingServices.DataProcessing.IDataParameter dataParameter in parameters)
			{
				IDataMultiValueParameter dataMultiValueParameter = dataParameter as IDataMultiValueParameter;
				if (dataMultiValueParameter != null)
				{
					if (dataMultiValueParameter.ShouldMakeExpandable(serverType))
					{
						dataMultiValueParameter.Values = new object[] { dataMultiValueParameter.Value };
					}
					if (dataMultiValueParameter.Values != null && dataMultiValueParameter.Values != null && dataMultiValueParameter.Values.Length != 0)
					{
						string text4 = CommandWrapperHelper.GenerateStringFromMultiValue(dataMultiValueParameter, flag, serverType);
						if (flag)
						{
							dataParameter.Value = text4;
						}
						else
						{
							string text5 = "(?<ParameterName>" + Regex.Escape(dataMultiValueParameter.ParameterName) + ")";
							MatchCollection matchCollection = new Regex(text2 + text5 + text3, regexOptions).Matches(stringBuilder.ToString());
							if (matchCollection.Count > 0)
							{
								if (StringComparer.InvariantCultureIgnoreCase.Compare(dataMultiValueParameter.ParameterName, "?") == 0)
								{
									if (num < matchCollection.Count)
									{
										string text6 = matchCollection[num].Result("${ParameterName}");
										if (text6 != null && 1 == text6.Length)
										{
											stringBuilder.Remove(matchCollection[num].Index + 1, text6.Length);
											stringBuilder.Insert(matchCollection[num].Index + 1, text4);
										}
									}
								}
								else
								{
									for (int i = matchCollection.Count - 1; i >= 0; i--)
									{
										string text7 = matchCollection[i].Result("${ParameterName}");
										if (text7 != null && 1 < text7.Length)
										{
											RSTrace.DataExtensionTracer.Assert(text7.Length == dataMultiValueParameter.ParameterName.Length);
											stringBuilder.Remove(matchCollection[i].Index + 1, text7.Length);
											stringBuilder.Insert(matchCollection[i].Index + 1, text4);
										}
									}
								}
							}
							if (RSTrace.DataExtensionTracer.TraceVerbose)
							{
								RSTrace.DataExtensionTracer.Trace(TraceLevel.Verbose, "Query rewrite (removed parameter): " + dataMultiValueParameter.ParameterName);
							}
							command.Parameters.RemoveAt(dataMultiValueParameter.ParameterName);
						}
					}
					else if (dataMultiValueParameter.Values == null && StringComparer.InvariantCultureIgnoreCase.Compare(dataMultiValueParameter.ParameterName, "?") == 0)
					{
						num++;
					}
				}
			}
			if (!flag)
			{
				if (RSTrace.DataExtensionTracer.TraceVerbose)
				{
					RSTrace.DataExtensionTracer.Trace(TraceLevel.Verbose, "Query rewrite (original query): " + command.CommandText);
					RSTrace.DataExtensionTracer.Trace(TraceLevel.Verbose, "Query rewrite (rewritten query): " + stringBuilder.ToString());
				}
				return stringBuilder.ToString();
			}
			return command.CommandText;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00013150 File Offset: 0x00011350
		public static string GenerateStringFromMultiValue(IDataMultiValueParameter parameter, bool isStoredProcedure, ServerType serverType)
		{
			RSTrace.DataExtensionTracer.Assert(parameter != null);
			StringBuilder stringBuilder = new StringBuilder();
			int num = parameter.Values.Length;
			for (int i = 0; i < num; i++)
			{
				object obj = parameter.Values[i];
				if (obj == null)
				{
					throw new InvalidOperationException("Multi value query parameters cannot contain NULL");
				}
				if (obj is byte[])
				{
					throw new InvalidOperationException("Multi value query parameters cannot contain byte arrays");
				}
				StringBuilder stringBuilder2;
				if (obj is byte || obj is sbyte)
				{
					stringBuilder2 = new StringBuilder(((int)obj).ToString(CultureInfo.InvariantCulture));
				}
				else if (obj is bool)
				{
					stringBuilder2 = new StringBuilder(((bool)obj) ? "1" : "0");
				}
				else if (obj is DateTime && serverType == ServerType.Teradata)
				{
					stringBuilder2 = new StringBuilder(((IFormattable)obj).ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
				}
				else if (obj is IFormattable)
				{
					stringBuilder2 = new StringBuilder(((IFormattable)obj).ToString(null, CultureInfo.InvariantCulture));
				}
				else
				{
					stringBuilder2 = new StringBuilder(obj.ToString());
				}
				if (serverType != ServerType.AnalysisServices)
				{
					stringBuilder2.Replace("'", "''");
					if (!isStoredProcedure)
					{
						if (obj is string || obj is char)
						{
							if (serverType == ServerType.Teradata)
							{
								stringBuilder2.Insert(0, "'");
							}
							else
							{
								stringBuilder2.Insert(0, "N'");
							}
							stringBuilder2.Append("'");
						}
						else if (obj is DateTime)
						{
							if (serverType == ServerType.SQLServer || serverType == ServerType.SQLServerDataWarehouse)
							{
								stringBuilder2.Insert(0, "'");
								stringBuilder2.Append("'");
							}
							else if (serverType == ServerType.Teradata)
							{
								stringBuilder2.Insert(0, "TIMESTAMP '");
								stringBuilder2.Append("'");
							}
							else
							{
								stringBuilder2.Insert(0, "TO_DATE('");
								stringBuilder2.Append("','MM/DD/YYYY HH24:MI:SS')");
							}
						}
					}
				}
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(stringBuilder2.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
