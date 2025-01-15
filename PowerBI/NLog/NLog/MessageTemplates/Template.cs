using System;
using System.Collections.Generic;
using System.Text;

namespace NLog.MessageTemplates
{
	// Token: 0x02000087 RID: 135
	internal class Template
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00018E59 File Offset: 0x00017059
		public string Value { get; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00018E61 File Offset: 0x00017061
		public Literal[] Literals { get; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00018E69 File Offset: 0x00017069
		public Hole[] Holes { get; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00018E71 File Offset: 0x00017071
		public bool IsPositional { get; }

		// Token: 0x0600097D RID: 2429 RVA: 0x00018E79 File Offset: 0x00017079
		public Template(string template, bool isPositional, List<Literal> literals, List<Hole> holes)
		{
			this.Value = template;
			this.IsPositional = isPositional;
			this.Literals = literals.ToArray();
			this.Holes = holes.ToArray();
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00018EA8 File Offset: 0x000170A8
		public Template(string template, bool isPositional, Literal[] literals, Hole[] holes)
		{
			this.Value = template;
			this.IsPositional = isPositional;
			this.Literals = literals;
			this.Holes = holes;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00018ED0 File Offset: 0x000170D0
		public string Rebuild()
		{
			StringBuilder stringBuilder = new StringBuilder(this.Value.Length);
			int num = 0;
			int num2 = 0;
			foreach (Literal literal in this.Literals)
			{
				stringBuilder.Append(this.Value, num, literal.Print);
				num += literal.Print;
				if (literal.Skip == 0)
				{
					if (num < this.Value.Length)
					{
						stringBuilder.Append(this.Value[num++]);
					}
				}
				else
				{
					num += (int)literal.Skip;
					Template.RebuildHole(stringBuilder, ref this.Holes[num2++]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00018F90 File Offset: 0x00017190
		private static void RebuildHole(StringBuilder sb, ref Hole hole)
		{
			if (hole.CaptureType == CaptureType.Normal)
			{
				sb.Append('{');
			}
			else if (hole.CaptureType == CaptureType.Serialize)
			{
				sb.Append("{@");
			}
			else
			{
				sb.Append("{$");
			}
			sb.Append(hole.Name);
			if (hole.Alignment != 0)
			{
				sb.Append(',').Append(hole.Alignment);
			}
			if (hole.Format != null)
			{
				sb.Append(':').Append(hole.Format.Replace("{", "{{").Replace("}", "}}"));
			}
			sb.Append('}');
		}
	}
}
