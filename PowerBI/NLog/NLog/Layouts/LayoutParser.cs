using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NLog.Common;
using NLog.Conditions;
using NLog.Config;
using NLog.Internal;
using NLog.LayoutRenderers;
using NLog.LayoutRenderers.Wrappers;

namespace NLog.Layouts
{
	// Token: 0x020000A8 RID: 168
	internal static class LayoutParser
	{
		// Token: 0x06000AD6 RID: 2774 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
		internal static LayoutRenderer[] CompileLayout(ConfigurationItemFactory configurationItemFactory, SimpleStringReader sr, bool isNested, out string text)
		{
			List<LayoutRenderer> list = new List<LayoutRenderer>();
			StringBuilder stringBuilder = new StringBuilder();
			int position = sr.Position;
			int num;
			while ((num = sr.Peek()) != -1)
			{
				if (isNested)
				{
					if (num == 92)
					{
						sr.Read();
						int num2 = sr.Peek();
						if (LayoutParser.EndOfLayout(num2))
						{
							sr.Read();
							stringBuilder.Append((char)num2);
							continue;
						}
						stringBuilder.Append('\\');
						continue;
					}
					else if (LayoutParser.EndOfLayout(num))
					{
						break;
					}
				}
				sr.Read();
				if (num == 36 && sr.Peek() == 123)
				{
					LayoutParser.AddLiteral(stringBuilder, list);
					LayoutRenderer layoutRenderer = LayoutParser.ParseLayoutRenderer(configurationItemFactory, sr);
					if (LayoutParser.CanBeConvertedToLiteral(layoutRenderer))
					{
						layoutRenderer = LayoutParser.ConvertToLiteral(layoutRenderer);
					}
					list.Add(layoutRenderer);
				}
				else
				{
					stringBuilder.Append((char)num);
				}
			}
			LayoutParser.AddLiteral(stringBuilder, list);
			int position2 = sr.Position;
			LayoutParser.MergeLiterals(list);
			text = sr.Substring(position, position2);
			return list.ToArray();
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0001BFDA File Offset: 0x0001A1DA
		private static void AddLiteral(StringBuilder literalBuf, List<LayoutRenderer> result)
		{
			if (literalBuf.Length > 0)
			{
				result.Add(new LiteralLayoutRenderer(literalBuf.ToString()));
				literalBuf.Length = 0;
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0001BFFD File Offset: 0x0001A1FD
		private static bool EndOfLayout(int ch)
		{
			return ch == 125 || ch == 58;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0001C00C File Offset: 0x0001A20C
		private static string ParseLayoutRendererName(SimpleStringReader sr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			while ((num = sr.Peek()) != -1 && num != 58 && num != 125)
			{
				stringBuilder.Append((char)num);
				sr.Read();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0001C04C File Offset: 0x0001A24C
		private static string ParseParameterName(SimpleStringReader sr)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			int num2;
			while ((num2 = sr.Peek()) != -1 && ((num2 != 61 && num2 != 125 && num2 != 58) || num != 0))
			{
				if (num2 == 36)
				{
					sr.Read();
					stringBuilder.Append('$');
					if (sr.Peek() == 123)
					{
						stringBuilder.Append('{');
						num++;
						sr.Read();
					}
				}
				else
				{
					if (num2 == 125)
					{
						num--;
					}
					if (num2 == 92)
					{
						sr.Read();
						if (num != 0)
						{
							stringBuilder.Append((char)num2);
						}
						stringBuilder.Append((char)sr.Read());
					}
					else
					{
						stringBuilder.Append((char)num2);
						sr.Read();
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0001C108 File Offset: 0x0001A308
		private static string ParseParameterValue(SimpleStringReader sr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			while ((num = sr.Peek()) != -1 && num != 58 && num != 125)
			{
				if (num == 92)
				{
					sr.Read();
					char c = (char)sr.Peek();
					if (c <= '\\')
					{
						if (c <= '0')
						{
							if (c != '"' && c != '\'')
							{
								if (c != '0')
								{
									continue;
								}
								sr.Read();
								stringBuilder.Append('\0');
								continue;
							}
						}
						else if (c != ':')
						{
							if (c == 'U')
							{
								sr.Read();
								char unicode = LayoutParser.GetUnicode(sr, 8);
								stringBuilder.Append(unicode);
								continue;
							}
							if (c != '\\')
							{
								continue;
							}
						}
					}
					else if (c <= 'f')
					{
						if (c == 'a')
						{
							sr.Read();
							stringBuilder.Append('\a');
							continue;
						}
						if (c == 'b')
						{
							sr.Read();
							stringBuilder.Append('\b');
							continue;
						}
						if (c != 'f')
						{
							continue;
						}
						sr.Read();
						stringBuilder.Append('\f');
						continue;
					}
					else
					{
						switch (c)
						{
						case 'n':
							sr.Read();
							stringBuilder.Append('\n');
							continue;
						case 'o':
						case 'p':
						case 'q':
						case 's':
						case 'w':
							continue;
						case 'r':
							sr.Read();
							stringBuilder.Append('\r');
							continue;
						case 't':
							sr.Read();
							stringBuilder.Append('\t');
							continue;
						case 'u':
						{
							sr.Read();
							char unicode2 = LayoutParser.GetUnicode(sr, 4);
							stringBuilder.Append(unicode2);
							continue;
						}
						case 'v':
							sr.Read();
							stringBuilder.Append('\v');
							continue;
						case 'x':
						{
							sr.Read();
							char unicode3 = LayoutParser.GetUnicode(sr, 4);
							stringBuilder.Append(unicode3);
							continue;
						}
						default:
							if (c != '{' && c != '}')
							{
								continue;
							}
							break;
						}
					}
					sr.Read();
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append((char)num);
					sr.Read();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0001C30C File Offset: 0x0001A50C
		private static char GetUnicode(SimpleStringReader sr, int maxDigits)
		{
			int num = 0;
			for (int i = 0; i < maxDigits; i++)
			{
				int num2 = sr.Peek();
				if (num2 >= 48 && num2 <= 57)
				{
					num2 -= 48;
				}
				else if (num2 >= 97 && num2 <= 102)
				{
					num2 = num2 - 97 + 10;
				}
				else
				{
					if (num2 < 65 || num2 > 70)
					{
						break;
					}
					num2 = num2 - 65 + 10;
				}
				sr.Read();
				num = num * 16 + num2;
			}
			return (char)num;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0001C378 File Offset: 0x0001A578
		private static LayoutRenderer ParseLayoutRenderer(ConfigurationItemFactory configurationItemFactory, SimpleStringReader stringReader)
		{
			int num = stringReader.Read();
			string text = LayoutParser.ParseLayoutRendererName(stringReader);
			LayoutRenderer layoutRenderer = LayoutParser.GetLayoutRenderer(configurationItemFactory, text);
			Dictionary<Type, LayoutRenderer> dictionary = new Dictionary<Type, LayoutRenderer>();
			List<LayoutRenderer> list = new List<LayoutRenderer>();
			num = stringReader.Read();
			while (num != -1 && num != 125)
			{
				string text2 = LayoutParser.ParseParameterName(stringReader).Trim();
				if (stringReader.Peek() == 61)
				{
					stringReader.Read();
					LayoutRenderer layoutRenderer2 = layoutRenderer;
					PropertyInfo propertyInfo;
					Type type;
					if (!PropertyHelper.TryGetPropertyInfo(layoutRenderer, text2, out propertyInfo) && configurationItemFactory.AmbientProperties.TryGetDefinition(text2, out type))
					{
						LayoutRenderer layoutRenderer3;
						if (!dictionary.TryGetValue(type, out layoutRenderer3))
						{
							layoutRenderer3 = configurationItemFactory.AmbientProperties.CreateInstance(text2);
							dictionary[type] = layoutRenderer3;
							list.Add(layoutRenderer3);
						}
						if (!PropertyHelper.TryGetPropertyInfo(layoutRenderer3, text2, out propertyInfo))
						{
							propertyInfo = null;
						}
						else
						{
							layoutRenderer2 = layoutRenderer3;
						}
					}
					if (propertyInfo == null)
					{
						LayoutParser.ParseParameterValue(stringReader);
					}
					else if (typeof(Layout).IsAssignableFrom(propertyInfo.PropertyType))
					{
						SimpleLayout simpleLayout = new SimpleLayout();
						string text3;
						LayoutRenderer[] array = LayoutParser.CompileLayout(configurationItemFactory, stringReader, true, out text3);
						simpleLayout.SetRenderers(array, text3);
						propertyInfo.SetValue(layoutRenderer2, simpleLayout, null);
					}
					else if (typeof(ConditionExpression).IsAssignableFrom(propertyInfo.PropertyType))
					{
						ConditionExpression conditionExpression = ConditionParser.ParseExpression(stringReader, configurationItemFactory);
						propertyInfo.SetValue(layoutRenderer2, conditionExpression, null);
					}
					else
					{
						string text4 = LayoutParser.ParseParameterValue(stringReader);
						PropertyHelper.SetPropertyFromString(layoutRenderer2, text2, text4, configurationItemFactory);
					}
				}
				else
				{
					LayoutParser.SetDefaultPropertyValue(configurationItemFactory, layoutRenderer, text2);
				}
				num = stringReader.Read();
			}
			return LayoutParser.ApplyWrappers(configurationItemFactory, layoutRenderer, list);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001C500 File Offset: 0x0001A700
		private static LayoutRenderer GetLayoutRenderer(ConfigurationItemFactory configurationItemFactory, string name)
		{
			LayoutRenderer layoutRenderer;
			try
			{
				layoutRenderer = configurationItemFactory.LayoutRenderers.CreateInstance(name);
			}
			catch (Exception ex)
			{
				if (LogManager.ThrowConfigExceptions ?? LogManager.ThrowExceptions)
				{
					throw;
				}
				InternalLogger.Error(ex, "Error parsing layout {0} will be ignored.", new object[] { name });
				layoutRenderer = new LiteralLayoutRenderer(string.Empty);
			}
			return layoutRenderer;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0001C570 File Offset: 0x0001A770
		private static void SetDefaultPropertyValue(ConfigurationItemFactory configurationItemFactory, LayoutRenderer layoutRenderer, string parameterName)
		{
			PropertyInfo propertyInfo;
			if (!PropertyHelper.TryGetPropertyInfo(layoutRenderer, string.Empty, out propertyInfo))
			{
				InternalLogger.Warn<string>("{0} has no default property", layoutRenderer.GetType().FullName);
				return;
			}
			if (typeof(SimpleLayout) == propertyInfo.PropertyType)
			{
				propertyInfo.SetValue(layoutRenderer, new SimpleLayout(parameterName), null);
				return;
			}
			PropertyHelper.SetPropertyFromString(layoutRenderer, propertyInfo.Name, parameterName, configurationItemFactory);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001C5D8 File Offset: 0x0001A7D8
		private static LayoutRenderer ApplyWrappers(ConfigurationItemFactory configurationItemFactory, LayoutRenderer lr, List<LayoutRenderer> orderedWrappers)
		{
			for (int i = orderedWrappers.Count - 1; i >= 0; i--)
			{
				WrapperLayoutRendererBase wrapperLayoutRendererBase = (WrapperLayoutRendererBase)orderedWrappers[i];
				InternalLogger.Trace<string, string>("Wrapping {0} with {1}", lr.GetType().Name, wrapperLayoutRendererBase.GetType().Name);
				if (LayoutParser.CanBeConvertedToLiteral(lr))
				{
					lr = LayoutParser.ConvertToLiteral(lr);
				}
				wrapperLayoutRendererBase.Inner = new SimpleLayout(new LayoutRenderer[] { lr }, string.Empty, configurationItemFactory);
				lr = wrapperLayoutRendererBase;
			}
			return lr;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001C654 File Offset: 0x0001A854
		private static bool CanBeConvertedToLiteral(LayoutRenderer lr)
		{
			foreach (IRenderable renderable in ObjectGraphScanner.FindReachableObjects<IRenderable>(true, new object[] { lr }))
			{
				if (!(renderable.GetType() == typeof(SimpleLayout)) && !renderable.GetType().IsDefined(typeof(AppDomainFixedOutputAttribute), false))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001C6E0 File Offset: 0x0001A8E0
		private static void MergeLiterals(List<LayoutRenderer> list)
		{
			int num = 0;
			while (num + 1 < list.Count)
			{
				LiteralLayoutRenderer literalLayoutRenderer = list[num] as LiteralLayoutRenderer;
				LiteralLayoutRenderer literalLayoutRenderer2 = list[num + 1] as LiteralLayoutRenderer;
				if (literalLayoutRenderer != null && literalLayoutRenderer2 != null)
				{
					LiteralLayoutRenderer literalLayoutRenderer3 = literalLayoutRenderer;
					literalLayoutRenderer3.Text += literalLayoutRenderer2.Text;
					list.RemoveAt(num + 1);
				}
				else
				{
					num++;
				}
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001C744 File Offset: 0x0001A944
		private static LayoutRenderer ConvertToLiteral(LayoutRenderer renderer)
		{
			return new LiteralLayoutRenderer(renderer.Render(LogEventInfo.CreateNullEvent()));
		}
	}
}
