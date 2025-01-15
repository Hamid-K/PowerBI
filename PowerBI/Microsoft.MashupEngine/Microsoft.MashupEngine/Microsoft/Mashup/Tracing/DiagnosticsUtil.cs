using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Tracing
{
	// Token: 0x020020B0 RID: 8368
	internal static class DiagnosticsUtil
	{
		// Token: 0x0600CCD4 RID: 52436 RVA: 0x0028B540 File Offset: 0x00289740
		public static void WriteException(TraceStringWriter writer, Exception exception, bool markMessageAndDataAsPii = false)
		{
			ExternalException ex = exception as ExternalException;
			if (ex != null)
			{
				writer.Write("HRESULT: 0x{0:x}", ex.ErrorCode);
			}
			if (exception != null)
			{
				writer.WriteLine("Exception:");
				writer.WriteLine("ExceptionType: {0}", exception.GetType().AssemblyQualifiedName);
				ValueException2 valueException = exception as ValueException2;
				writer.Write("Message: ");
				if (valueException == null || string.IsNullOrEmpty(valueException.MessageFormatString) || valueException.MessageFormatIsPii)
				{
					if (markMessageAndDataAsPii)
					{
						writer.BeginPii();
					}
					writer.Write("{0}", exception.Message);
					if (markMessageAndDataAsPii)
					{
						writer.EndPii();
					}
				}
				else
				{
					IListValue messageParameters = valueException.MessageParameters;
					int num = ((messageParameters != null) ? messageParameters.Count : 0);
					IValue[] array = new IValue[num];
					string[] array2 = new string[array.Length];
					if (num > 0)
					{
						int num2 = 0;
						foreach (IValueReference2 valueReference in valueException.MessageParameters)
						{
							IValue value = valueReference.Value;
							array[num2] = value;
							array2[num2] = value.ToSource();
							num2++;
						}
					}
					string text = ValueException2.ConvertToCSharpFormatSpecifier(valueException.MessageFormatString);
					int num3 = 0;
					int num4 = text.IndexOf('{');
					while (num4 >= 0 && num4 < text.Length - 1)
					{
						if (num4 > num3)
						{
							writer.Write(text.Substring(num3, num4 - num3));
						}
						if (text[num4 + 1] == '{')
						{
							num3 = num4 + 1;
							if (num3 + 1 >= text.Length)
							{
								break;
							}
							num4 = text.IndexOf('{', num3 + 1);
						}
						else
						{
							int num5 = text.IndexOf('}', num4);
							if (num5 == -1)
							{
								break;
							}
							int num6 = text.IndexOfAny(new char[] { '}', ':', ',' }, num4);
							int num7;
							if (!int.TryParse(text.Substring(num4 + 1, num6 - num4 - 1), out num7) || num7 >= array.Length)
							{
								num4 = text.IndexOf('{', num4 + 1);
							}
							else
							{
								bool flag = ValueException2.IsPii(array[num7]);
								if (flag)
								{
									writer.BeginPii();
								}
								writer.Write(array2[num7]);
								if (flag)
								{
									writer.EndPii();
								}
								num3 = num5 + 1;
								num4 = text.IndexOf('{', num3);
							}
						}
					}
					if (num4 != text.Length - 1)
					{
						writer.Write(text.Substring(num3));
					}
				}
				writer.WriteLine();
				writer.WriteLine("StackTrace:\n{0}", exception.StackTrace);
				writer.WriteLine();
				if (exception.Data != null && exception.Data.Count > 0)
				{
					writer.WriteLine("DataItems");
					if (markMessageAndDataAsPii)
					{
						writer.BeginPii();
					}
					foreach (object obj in exception.Data)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						writer.WriteLine("Data");
						writer.WriteLine("Key: {0}", dictionaryEntry.Key.ToString());
						writer.WriteLine("Value: {0}", (dictionaryEntry.Value ?? "").ToString());
						writer.WriteLine();
					}
					if (markMessageAndDataAsPii)
					{
						writer.EndPii();
					}
					writer.WriteLine();
				}
				if (TracingHandlers.GetExceptionDetails != null)
				{
					string text2 = TracingHandlers.GetExceptionDetails(exception);
					if (text2 != null)
					{
						if (valueException == null || valueException.DetailIsPii)
						{
							if (markMessageAndDataAsPii)
							{
								writer.BeginPii();
							}
							writer.WriteLine("Detail: {0}", text2);
							if (markMessageAndDataAsPii)
							{
								writer.EndPii();
							}
						}
						else
						{
							writer.WriteLine("Detail: {0}", text2);
						}
					}
				}
				if (exception.InnerException != null)
				{
					writer.WriteLine("InnerException");
					DiagnosticsUtil.WriteException(writer, exception.InnerException, markMessageAndDataAsPii);
					writer.WriteLine();
				}
			}
			writer.WriteLine();
			writer.Flush();
		}

		// Token: 0x040067B3 RID: 26547
		public static readonly Version EvaluatorVersionNumber = new Version(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
	}
}
