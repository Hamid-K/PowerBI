using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Privacy;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000DA RID: 218
	public static class GatewayExceptionUtils
	{
		// Token: 0x020000EB RID: 235
		[NullableContext(1)]
		[Nullable(0)]
		public static class InnerExceptionCreator
		{
			// Token: 0x0600113F RID: 4415 RVA: 0x00046DF4 File Offset: 0x00044FF4
			public static Exception GetInnerException(Exception inner)
			{
				if (inner == null)
				{
					return null;
				}
				GatewayPipelineException ex = inner as GatewayPipelineException;
				if (ex != null)
				{
					return ex;
				}
				if (inner.GetType().GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, GatewayExceptionUtils.InnerExceptionCreator.s_serializedCtorSignature, null) != null)
				{
					return inner;
				}
				return GatewayExceptionUtils.InnerExceptionCreator.CreateGatewayPipelineWrapperException(inner, null);
			}

			// Token: 0x06001140 RID: 4416 RVA: 0x00046E38 File Offset: 0x00045038
			public static GatewayPipelineException GetPipelineInnerException(Exception inner, GatewayExceptionUtils.InnerExceptionCreator.PowerBIErrorDetailsCreator errorDetailsCreator = null)
			{
				if (inner == null)
				{
					return null;
				}
				GatewayPipelineException ex = inner as GatewayPipelineException;
				if (ex != null)
				{
					return ex;
				}
				return GatewayExceptionUtils.InnerExceptionCreator.CreateGatewayPipelineWrapperException(inner, errorDetailsCreator);
			}

			// Token: 0x06001141 RID: 4417 RVA: 0x00046E60 File Offset: 0x00045060
			public static string GetExceptionErrorShortName(Exception e)
			{
				if (e == null)
				{
					return string.Empty;
				}
				GatewayPipelineException ex = e as GatewayPipelineException;
				if (ex != null)
				{
					return ex.ExceptionErrorShortName;
				}
				return GatewayExceptionUtils.InnerExceptionCreator.BuildNonPipelineExceptionErrorShortName(e);
			}

			// Token: 0x06001142 RID: 4418 RVA: 0x00046E90 File Offset: 0x00045090
			public static string BuildExceptionErrorShortName(IList<string> components)
			{
				switch (components.Count)
				{
				case 1:
					return components[0];
				case 3:
				{
					IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
					string text = "{0}[{1}={2}]";
					object[] array = components.ToArray<string>();
					return string.Format(invariantCulture, text, array);
				}
				case 5:
				{
					IFormatProvider invariantCulture2 = CultureInfo.InvariantCulture;
					string text2 = "{0}[{1}={2},{3}={4}]";
					object[] array = components.ToArray<string>();
					return string.Format(invariantCulture2, text2, array);
				}
				case 7:
				{
					IFormatProvider invariantCulture3 = CultureInfo.InvariantCulture;
					string text3 = "{0}[{1}={2},{3}={4},{5}={6}]";
					object[] array = components.ToArray<string>();
					return string.Format(invariantCulture3, text3, array);
				}
				case 9:
				{
					IFormatProvider invariantCulture4 = CultureInfo.InvariantCulture;
					string text4 = "{0}[{1}={2},{3}={4},{5}={6},{7}={8}]";
					object[] array = components.ToArray<string>();
					return string.Format(invariantCulture4, text4, array);
				}
				case 11:
				{
					IFormatProvider invariantCulture5 = CultureInfo.InvariantCulture;
					string text5 = "{0}[{1}={2},{3}={4},{5}={6},{7}={8},{9}={10}]";
					object[] array = components.ToArray<string>();
					return string.Format(invariantCulture5, text5, array);
				}
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(components[0]);
				stringBuilder.Append("[");
				for (int i = 1; i < components.Count; i++)
				{
					if (i > 1)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(components[i]);
					stringBuilder.Append("=");
					i++;
					if (i < components.Count)
					{
						stringBuilder.Append(components[i]);
					}
					else
					{
						stringBuilder.Append("<<Value missing>>");
					}
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}

			// Token: 0x06001143 RID: 4419 RVA: 0x00046FF4 File Offset: 0x000451F4
			private static GatewayPipelineWrapperException CreateGatewayPipelineWrapperException(Exception e, GatewayExceptionUtils.InnerExceptionCreator.PowerBIErrorDetailsCreator errorDetailsCreator)
			{
				RuntimeChecks.CheckValue(e, "e");
				GatewayPipelineException ex = null;
				if (e.InnerException != null)
				{
					ex = GatewayExceptionUtils.InnerExceptionCreator.CreateGatewayPipelineWrapperException(e.InnerException, errorDetailsCreator);
				}
				GatewayPipelineWrapperException ex2 = new GatewayPipelineWrapperException(e.GetType().Name, e.Message, e.ToString(), e.StackTrace, string.Empty, ex, GatewayExceptionUtils.InnerExceptionCreator.CreatePowerBIErrorDetails(e, errorDetailsCreator).ToArray<PowerBIErrorDetail>());
				IDictionary data = e.Data;
				if (data != null && data.Count > 0)
				{
					foreach (object obj in data)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						ex2.Data.Add(dictionaryEntry.Key, dictionaryEntry.Value);
					}
				}
				return ex2;
			}

			// Token: 0x06001144 RID: 4420 RVA: 0x000470CC File Offset: 0x000452CC
			private static IList<PowerBIErrorDetail> CreatePowerBIErrorDetails(Exception e, GatewayExceptionUtils.InnerExceptionCreator.PowerBIErrorDetailsCreator errorDetailsCreator)
			{
				List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
				if (errorDetailsCreator != null)
				{
					errorDetailsCreator(list, e);
				}
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				for (int i = 0; i < list.Count; i++)
				{
					string nameCode = list[i].NameCode;
					if (!(nameCode == "DM_ErrorDetailNameCode_UnderlyingErrorMessage"))
					{
						if (!(nameCode == "DM_ErrorDetailNameCode_UnderlyingHResult"))
						{
							if (!(nameCode == "DM_ErrorDetailNameCode_UnderlyingErrorCode"))
							{
								if (nameCode == "DM_ErrorDetailNameCode_UnderlyingNativeErrorCode")
								{
									flag4 = true;
								}
							}
							else
							{
								flag3 = true;
							}
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag = true;
					}
				}
				if (!flag)
				{
					PowerBIErrorDetail powerBIErrorDetail = ErrorDetailExtensions.CreateForEmbeddedValue("DM_ErrorDetailNameCode_UnderlyingErrorMessage", e.Message.MarkAsCustomerContent());
					list.Add(powerBIErrorDetail);
				}
				if (!flag2)
				{
					PowerBIErrorDetail powerBIErrorDetail2 = ErrorDetailExtensions.CreateForEmbeddedValue("DM_ErrorDetailNameCode_UnderlyingHResult", e.HResult.ToString());
					list.Add(powerBIErrorDetail2);
				}
				if (!flag3)
				{
					ExternalException ex = e as ExternalException;
					if (ex != null)
					{
						PowerBIErrorDetail powerBIErrorDetail3 = ErrorDetailExtensions.CreateForEmbeddedValue("DM_ErrorDetailNameCode_UnderlyingErrorCode", ex.ErrorCode.ToString());
						list.Add(powerBIErrorDetail3);
					}
				}
				if (!flag4)
				{
					Win32Exception ex2 = e as Win32Exception;
					if (ex2 != null)
					{
						PowerBIErrorDetail powerBIErrorDetail4 = ErrorDetailExtensions.CreateForEmbeddedValue("DM_ErrorDetailNameCode_UnderlyingNativeErrorCode", ex2.NativeErrorCode.ToString());
						list.Add(powerBIErrorDetail4);
					}
				}
				list.Sort((PowerBIErrorDetail x, PowerBIErrorDetail y) => string.CompareOrdinal(x.NameCode, y.NameCode));
				return list;
			}

			// Token: 0x06001145 RID: 4421 RVA: 0x00047238 File Offset: 0x00045438
			private static string BuildNonPipelineExceptionErrorShortName(Exception e)
			{
				RuntimeChecks.CheckValue(e, "e");
				List<string> list = new List<string>();
				list.Add(e.GetType().Name);
				list.Add("HResult");
				list.Add(e.HResult.ToString());
				ExternalException ex = e as ExternalException;
				if (ex != null)
				{
					list.Add("ErrorCode");
					list.Add(ex.ErrorCode.ToString());
				}
				Win32Exception ex2 = e as Win32Exception;
				if (ex2 != null)
				{
					list.Add("NativeErrorCode");
					list.Add(ex2.NativeErrorCode.ToString());
				}
				return GatewayExceptionUtils.InnerExceptionCreator.BuildExceptionErrorShortName(list);
			}

			// Token: 0x0400037B RID: 891
			private static readonly Type[] s_serializedCtorSignature = new Type[]
			{
				typeof(SerializationInfo),
				typeof(StreamingContext)
			};

			// Token: 0x020000F6 RID: 246
			// (Invoke) Token: 0x0600115D RID: 4445
			[NullableContext(0)]
			public delegate void PowerBIErrorDetailsCreator(IList<PowerBIErrorDetail> errorDetails, Exception e);
		}

		// Token: 0x020000EC RID: 236
		public static class InnerExceptionStringCreator
		{
			// Token: 0x06001147 RID: 4423 RVA: 0x00047308 File Offset: 0x00045508
			[NullableContext(1)]
			public static string CreateInnerExceptionStack(Exception inner)
			{
				string text = string.Empty;
				while (inner != null)
				{
					if (text.Length > 0)
					{
						text += " > ";
					}
					text += inner.GetType().FullName;
					inner = inner.InnerException;
				}
				if (text.Length > 0)
				{
					text = "Inner exception chain: " + text + Environment.NewLine;
				}
				return text;
			}
		}

		// Token: 0x020000ED RID: 237
		public static class ExceptionsTemplateHelper
		{
			// Token: 0x1700014F RID: 335
			// (get) Token: 0x06001148 RID: 4424 RVA: 0x0004736A File Offset: 0x0004556A
			public static int MagicLevel
			{
				get
				{
					return GatewayExceptionUtils.ExceptionsTemplateHelper.ts_magicLevel;
				}
			}

			// Token: 0x06001149 RID: 4425 RVA: 0x00047371 File Offset: 0x00045571
			public static void IncrementMagicLevel()
			{
				GatewayExceptionUtils.ExceptionsTemplateHelper.ts_magicLevel++;
			}

			// Token: 0x0600114A RID: 4426 RVA: 0x0004737F File Offset: 0x0004557F
			public static void DecrementMagicLevel()
			{
				GatewayExceptionUtils.ExceptionsTemplateHelper.ts_magicLevel--;
			}

			// Token: 0x0400037C RID: 892
			[ThreadStatic]
			private static int ts_magicLevel;
		}

		// Token: 0x020000EE RID: 238
		public static class CompileCheck
		{
			// Token: 0x0600114B RID: 4427 RVA: 0x0004738D File Offset: 0x0004558D
			[NullableContext(1)]
			public static void IsValidReferenceField<T>() where T : class
			{
			}

			// Token: 0x0600114C RID: 4428 RVA: 0x0004738F File Offset: 0x0004558F
			public static void IsValidValueField<T>() where T : struct
			{
			}
		}
	}
}
