using System;
using System.Text;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000394 RID: 916
	public sealed class KeyRange
	{
		// Token: 0x060013B5 RID: 5045 RVA: 0x00070238 File Offset: 0x0006E438
		public static KeyRange Parse(string str)
		{
			KeyRange keyRange = new KeyRange();
			if (keyRange.TryParse(str))
			{
				return keyRange;
			}
			return null;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00070258 File Offset: 0x0006E458
		private bool TryParse(string str)
		{
			int num = str.IndexOf('-');
			if (num < 0)
			{
				if (!ulong.TryParse(str, out this.min))
				{
					return false;
				}
				this.contiguous = false;
				return true;
			}
			else
			{
				if (!ulong.TryParse(str.Substring(0, num), out this.min))
				{
					return false;
				}
				string text = str.Substring(num + 1);
				if (string.IsNullOrEmpty(text) || text == "*")
				{
					return true;
				}
				ulong num2;
				if (!ulong.TryParse(text, out num2))
				{
					return false;
				}
				this.max = new ulong?(num2);
				return true;
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x000702DC File Offset: 0x0006E4DC
		public bool TryUnparse(StringBuilder sb)
		{
			if (!this.contiguous && this.max != null)
			{
				return false;
			}
			sb.Append(this.min);
			if (!this.contiguous)
			{
				return true;
			}
			if (this.max != null)
			{
				sb.Append('-').Append(this.max);
			}
			else if (this.contiguous)
			{
				sb.Append("-*");
			}
			return true;
		}

		// Token: 0x04000B76 RID: 2934
		[Argument(0, HelpText = "First index in the range")]
		public ulong min;

		// Token: 0x04000B77 RID: 2935
		[Argument(0, HelpText = "Last index in the range")]
		public ulong? max;

		// Token: 0x04000B78 RID: 2936
		[Argument(0, HelpText = "Whether the key is contiguous")]
		public bool contiguous = true;
	}
}
