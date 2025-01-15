using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Microsoft.ReportingServices.Design.RdlModel;

namespace Microsoft.ReportingServices.Design
{
	// Token: 0x02000387 RID: 903
	public sealed class Utils
	{
		// Token: 0x06001DF6 RID: 7670 RVA: 0x000025F4 File Offset: 0x000007F4
		private Utils()
		{
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0007A9A0 File Offset: 0x00078BA0
		public static string[] ReportExpressionSplit(string expression, char separator)
		{
			Utils.ExpressionStatus expressionStatus = Utils.ExpressionStatus.Normal;
			int num = 0;
			ArrayList arrayList = new ArrayList();
			StringBuilder stringBuilder = new StringBuilder();
			int length = expression.Length;
			for (int i = 0; i < length; i++)
			{
				char c = expression[i];
				switch (expressionStatus)
				{
				case Utils.ExpressionStatus.Normal:
					if (c == separator)
					{
						arrayList.Add(stringBuilder.ToString().Trim());
						stringBuilder = new StringBuilder();
					}
					else if (c == '"')
					{
						stringBuilder.Append(c);
						if (i < length - 1)
						{
							if (expression[i + 1] == '"')
							{
								stringBuilder.Append(expression[i + 1]);
								i++;
							}
							else
							{
								expressionStatus = Utils.ExpressionStatus.Quotation;
							}
						}
					}
					else if (c == '(')
					{
						stringBuilder.Append(c);
						num++;
						expressionStatus = Utils.ExpressionStatus.Brace;
					}
					else
					{
						stringBuilder.Append(c);
					}
					break;
				case Utils.ExpressionStatus.Quotation:
					stringBuilder.Append(c);
					if (c == '"')
					{
						expressionStatus = Utils.ExpressionStatus.Normal;
					}
					break;
				case Utils.ExpressionStatus.Brace:
					stringBuilder.Append(c);
					if (c == '"')
					{
						if (i < length - 1)
						{
							if (expression[i + 1] == '"')
							{
								stringBuilder.Append(expression[i + 1]);
								i++;
							}
							else
							{
								expressionStatus = Utils.ExpressionStatus.BraceQuotation;
							}
						}
					}
					else if (c == '(')
					{
						num++;
					}
					else if (c == ')')
					{
						num--;
						if (num == 0)
						{
							expressionStatus = Utils.ExpressionStatus.Normal;
						}
					}
					break;
				case Utils.ExpressionStatus.BraceQuotation:
					stringBuilder.Append(c);
					if (c == '"')
					{
						expressionStatus = Utils.ExpressionStatus.Brace;
					}
					break;
				}
			}
			if (stringBuilder.Length > 0)
			{
				arrayList.Add(stringBuilder.ToString().Trim());
			}
			string[] array = new string[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x0007AB4C File Offset: 0x00078D4C
		public static void ValidateValueRange(string paramName, int value, object min, object max)
		{
			if ((min != null && value < (int)min) || (max != null && value > (int)max))
			{
				string text;
				if (max == null)
				{
					text = SRErrors.InvalidParamGreaterThan(min);
				}
				else if (min == null)
				{
					text = SRErrors.InvalidParamLessThan(max);
				}
				else
				{
					text = SRErrors.InvalidParamBetween(min, max);
				}
				throw new ArgumentOutOfRangeException(paramName, text);
			}
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x0007AB9C File Offset: 0x00078D9C
		public static void ValidateValueRange(string paramName, Unit value, object min, object max)
		{
			double fpixels = value.FPixels;
			if ((min != null && fpixels < ((Unit)min).FPixels) || (max != null && fpixels > ((Unit)max).FPixels))
			{
				string text = ((min != null) ? ((Unit)min).ChangeType(value.Type).ToString() : null);
				string text2 = ((max != null) ? ((Unit)max).ChangeType(value.Type).ToString() : null);
				string text3;
				if (max == null)
				{
					text3 = SRErrors.InvalidParamGreaterThan(text);
				}
				else if (min == null)
				{
					text3 = SRErrors.InvalidParamLessThan(text2);
				}
				else
				{
					text3 = SRErrors.InvalidParamBetween(text, text2);
				}
				throw new ArgumentOutOfRangeException(paramName, text3);
			}
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x0007AC5C File Offset: 0x00078E5C
		public static MIMEType.Enum GetMimeType(global::System.Drawing.Image image)
		{
			ImageFormat rawFormat = image.RawFormat;
			if (rawFormat.Guid == ImageFormat.Bmp.Guid)
			{
				return MIMEType.Enum.Bmp;
			}
			if (rawFormat.Guid == ImageFormat.Gif.Guid)
			{
				return MIMEType.Enum.Gif;
			}
			if (rawFormat.Guid == ImageFormat.Jpeg.Guid)
			{
				return MIMEType.Enum.Jpeg;
			}
			if (rawFormat.Guid == ImageFormat.Png.Guid)
			{
				return MIMEType.Enum.Png;
			}
			return MIMEType.Enum.Invalid;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x0007ACD8 File Offset: 0x00078ED8
		public static byte[] GetStreamBytes(Stream stream)
		{
			byte[] array;
			if (stream.CanSeek)
			{
				int num = (int)stream.Length;
				array = new byte[num];
				stream.Read(array, 0, num);
				return array;
			}
			array = new byte[256];
			int num2 = 0;
			int num3;
			do
			{
				if (array.Length < num2 + 256)
				{
					byte[] array2 = new byte[array.Length * 2];
					Array.Copy(array, array2, array.Length);
					array = array2;
				}
				num3 = stream.Read(array, num2, 256);
				num2 += num3;
			}
			while (num3 != 0);
			return array;
		}

		// Token: 0x02000508 RID: 1288
		private enum ExpressionStatus
		{
			// Token: 0x04001230 RID: 4656
			Normal,
			// Token: 0x04001231 RID: 4657
			Quotation,
			// Token: 0x04001232 RID: 4658
			Brace,
			// Token: 0x04001233 RID: 4659
			BraceQuotation
		}
	}
}
