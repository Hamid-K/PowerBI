using System;
using System.Collections.Generic;
using System.Text;

namespace NLog.MessageTemplates
{
	// Token: 0x0200008B RID: 139
	internal static class TemplateRenderer
	{
		// Token: 0x0600099C RID: 2460 RVA: 0x0001984C File Offset: 0x00017A4C
		public static void Render(this string template, IFormatProvider formatProvider, object[] parameters, bool forceTemplateRenderer, StringBuilder sb, out IList<MessageTemplateParameter> messageTemplateParameters)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			messageTemplateParameters = null;
			int length = sb.Length;
			TemplateEnumerator templateEnumerator = new TemplateEnumerator(template);
			while (templateEnumerator.MoveNext())
			{
				if (num2 == 0 && !forceTemplateRenderer)
				{
					LiteralHole literalHole = templateEnumerator.Current;
					if (literalHole.MaybePositionalTemplate && sb.Length == length)
					{
						sb.AppendFormat(formatProvider, template, parameters);
						return;
					}
				}
				Literal literal = templateEnumerator.Current.Literal;
				sb.Append(template, num, literal.Print);
				num += literal.Print;
				if (literal.Skip == 0)
				{
					num++;
				}
				else
				{
					num += (int)literal.Skip;
					Hole hole = templateEnumerator.Current.Hole;
					if (hole.Alignment != 0)
					{
						num3 = sb.Length;
					}
					if (hole.Index != -1 && messageTemplateParameters == null)
					{
						num2++;
						TemplateRenderer.RenderHole(sb, hole, formatProvider, parameters[(int)hole.Index], true);
					}
					else
					{
						object obj = parameters[num2];
						if (messageTemplateParameters == null)
						{
							messageTemplateParameters = new MessageTemplateParameter[parameters.Length];
							if (num2 != 0)
							{
								templateEnumerator = new TemplateEnumerator(template);
								sb.Length = length;
								num2 = 0;
								num = 0;
								continue;
							}
						}
						messageTemplateParameters[num2++] = new MessageTemplateParameter(hole.Name, obj, hole.Format, hole.CaptureType);
						TemplateRenderer.RenderHole(sb, hole, formatProvider, obj, false);
					}
					if (hole.Alignment != 0)
					{
						TemplateRenderer.RenderPadding(sb, (int)hole.Alignment, num3);
					}
				}
			}
			if (messageTemplateParameters != null && num2 != messageTemplateParameters.Count)
			{
				MessageTemplateParameter[] array = new MessageTemplateParameter[num2];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = messageTemplateParameters[i];
				}
				messageTemplateParameters = array;
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00019A00 File Offset: 0x00017C00
		public static void Render(this Template template, StringBuilder sb, IFormatProvider formatProvider, object[] parameters)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (Literal literal in template.Literals)
			{
				sb.Append(template.Value, num, literal.Print);
				num += literal.Print;
				if (literal.Skip == 0)
				{
					num++;
				}
				else
				{
					num += (int)literal.Skip;
					Hole hole = template.Holes[num2];
					if (hole.Alignment != 0)
					{
						num3 = sb.Length;
					}
					object obj = (template.IsPositional ? parameters[(int)hole.Index] : parameters[num2]);
					num2++;
					TemplateRenderer.RenderHole(sb, hole, formatProvider, obj, template.IsPositional);
					if (hole.Alignment != 0)
					{
						TemplateRenderer.RenderPadding(sb, (int)hole.Alignment, num3);
					}
				}
			}
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00019AD6 File Offset: 0x00017CD6
		private static void RenderHole(StringBuilder sb, Hole hole, IFormatProvider formatProvider, object value, bool legacy = false)
		{
			TemplateRenderer.RenderHole(sb, hole.CaptureType, hole.Format, formatProvider, value, legacy);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00019AEE File Offset: 0x00017CEE
		public static void RenderHole(StringBuilder sb, CaptureType captureType, string holeFormat, IFormatProvider formatProvider, object value, bool legacy = false)
		{
			if (value == null)
			{
				sb.Append("NULL");
				return;
			}
			if (captureType == CaptureType.Normal && legacy)
			{
				ValueFormatter.FormatToString(value, holeFormat, formatProvider, sb);
				return;
			}
			ValueFormatter.Instance.FormatValue(value, holeFormat, captureType, formatProvider, sb);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00019B28 File Offset: 0x00017D28
		private static void RenderPadding(StringBuilder sb, int holeAlignment, int holeStartPosition)
		{
			int num = sb.Length - holeStartPosition;
			int num2 = Math.Abs(holeAlignment) - num;
			if (num2 > 0)
			{
				if (holeAlignment < 0 || num == 0)
				{
					sb.Append(' ', num2);
					return;
				}
				string text = sb.ToString(holeStartPosition, num);
				sb.Length = holeStartPosition;
				sb.Append(' ', num2);
				sb.Append(text);
			}
		}
	}
}
