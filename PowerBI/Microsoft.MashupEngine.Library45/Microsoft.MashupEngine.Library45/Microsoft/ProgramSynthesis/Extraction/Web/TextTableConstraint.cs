using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FD0 RID: 4048
	public class TextTableConstraint : ConstraintOnInput<WebRegion, IEnumerable<IEnumerable<string>>>
	{
		// Token: 0x06006FA5 RID: 28581 RVA: 0x0016C894 File Offset: 0x0016AA94
		public TextTableConstraint(WebRegion input, List<List<CellExample>> cellExamples = null)
			: base(input, false)
		{
			IReadOnlyList<IReadOnlyList<string>> readOnlyList;
			if (cellExamples == null)
			{
				readOnlyList = null;
			}
			else
			{
				readOnlyList = cellExamples.Select((List<CellExample> c) => c.Select((CellExample e) => e.Value).ToList<string>()).ToList<List<string>>();
			}
			this.ColumnExamples = readOnlyList;
			IReadOnlyList<IReadOnlyList<int>> readOnlyList2;
			if (cellExamples == null)
			{
				readOnlyList2 = null;
			}
			else
			{
				readOnlyList2 = cellExamples.Select((List<CellExample> c) => c.Select((CellExample e) => e.NodeIndex).ToList<int>()).ToList<List<int>>();
			}
			this.NodeIndexes = readOnlyList2;
			IReadOnlyList<IReadOnlyList<bool>> readOnlyList3;
			if (cellExamples == null)
			{
				readOnlyList3 = null;
			}
			else
			{
				readOnlyList3 = cellExamples.Select((List<CellExample> c) => c.Select((CellExample e) => e.IsSoft).ToList<bool>()).ToList<List<bool>>();
			}
			this.SoftConstraints = readOnlyList3;
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06006FA6 RID: 28582 RVA: 0x0016C94B File Offset: 0x0016AB4B
		public IReadOnlyList<IReadOnlyList<string>> ColumnExamples { get; }

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x06006FA7 RID: 28583 RVA: 0x0016C953 File Offset: 0x0016AB53
		public IReadOnlyList<IReadOnlyList<int>> NodeIndexes { get; }

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x06006FA8 RID: 28584 RVA: 0x0016C95B File Offset: 0x0016AB5B
		public IReadOnlyList<IReadOnlyList<bool>> SoftConstraints { get; }

		// Token: 0x06006FA9 RID: 28585 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<WebRegion, IEnumerable<IEnumerable<string>>> program)
		{
			return true;
		}

		// Token: 0x06006FAA RID: 28586 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<WebRegion, IEnumerable<IEnumerable<string>>> program, IEnumerable<WebRegion> inputs, IReadOnlyCollection<Constraint<WebRegion, IEnumerable<IEnumerable<string>>>> allConstraints)
		{
			return true;
		}

		// Token: 0x06006FAB RID: 28587 RVA: 0x0016C964 File Offset: 0x0016AB64
		public override bool ConflictsWith(Constraint<WebRegion, IEnumerable<IEnumerable<string>>> other)
		{
			TextTableConstraint textTableConstraint = other as TextTableConstraint;
			return textTableConstraint != null && !this.Equals(textTableConstraint);
		}

		// Token: 0x06006FAC RID: 28588 RVA: 0x0016C990 File Offset: 0x0016AB90
		public bool Equals(TextTableConstraint other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (!base.Input.Equals(other.Input))
			{
				return false;
			}
			IReadOnlyList<IReadOnlyList<string>> columnExamples = this.ColumnExamples;
			int? num = ((columnExamples != null) ? new int?(columnExamples.Count) : null);
			IReadOnlyList<IReadOnlyList<string>> columnExamples2 = other.ColumnExamples;
			int? num2 = ((columnExamples2 != null) ? new int?(columnExamples2.Count) : null);
			if (!((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))))
			{
				return false;
			}
			IReadOnlyList<IReadOnlyList<int>> nodeIndexes = this.NodeIndexes;
			num2 = ((nodeIndexes != null) ? new int?(nodeIndexes.Count) : null);
			IReadOnlyList<IReadOnlyList<int>> nodeIndexes2 = other.NodeIndexes;
			num = ((nodeIndexes2 != null) ? new int?(nodeIndexes2.Count) : null);
			if (!((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null))))
			{
				return false;
			}
			IReadOnlyList<IReadOnlyList<bool>> softConstraints = this.SoftConstraints;
			num = ((softConstraints != null) ? new int?(softConstraints.Count) : null);
			IReadOnlyList<IReadOnlyList<bool>> softConstraints2 = other.SoftConstraints;
			num2 = ((softConstraints2 != null) ? new int?(softConstraints2.Count) : null);
			if (!((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))))
			{
				return false;
			}
			if (this.ColumnExamples != null)
			{
				for (int i = 0; i < this.ColumnExamples.Count; i++)
				{
					if (!this.ColumnExamples[i].SequenceEqual(other.ColumnExamples[i]))
					{
						return false;
					}
				}
				for (int j = 0; j < this.NodeIndexes.Count; j++)
				{
					if (!this.NodeIndexes[j].SequenceEqual(other.NodeIndexes[j]))
					{
						return false;
					}
				}
				for (int k = 0; k < this.SoftConstraints.Count; k++)
				{
					if (!this.SoftConstraints[k].SequenceEqual(other.SoftConstraints[k]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06006FAD RID: 28589 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<WebRegion, IEnumerable<IEnumerable<string>>> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06006FAE RID: 28590 RVA: 0x0016CBA9 File Offset: 0x0016ADA9
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((TextTableConstraint)obj)));
		}

		// Token: 0x06006FAF RID: 28591 RVA: 0x0016CBD8 File Offset: 0x0016ADD8
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + base.Input.GetHashCode();
			if (this.ColumnExamples != null)
			{
				foreach (IReadOnlyList<string> readOnlyList in this.ColumnExamples)
				{
					num = num * 23 + readOnlyList.OrderDependentHashCode<string>();
				}
				foreach (IReadOnlyList<int> readOnlyList2 in this.NodeIndexes)
				{
					num = num * 23 + readOnlyList2.OrderDependentHashCode<int>();
				}
				foreach (IReadOnlyList<bool> readOnlyList3 in this.SoftConstraints)
				{
					num = num * 23 + readOnlyList3.OrderDependentHashCode<bool>();
				}
			}
			return num;
		}

		// Token: 0x06006FB0 RID: 28592 RVA: 0x0016CCD8 File Offset: 0x0016AED8
		public override string ToString()
		{
			return "TextTableConstraint(Columns=[" + string.Join("; ", this.ColumnExamples.Zip(this.NodeIndexes, this.SoftConstraints, (IReadOnlyList<string> examples, IReadOnlyList<int> nodes, IReadOnlyList<bool> isSofts) => "[" + string.Join(", ", examples.Zip(nodes, isSofts, (string ex, int node, bool isSoft) => string.Concat(new string[]
			{
				"{",
				ex.ToLiteral(null),
				(node > -1) ? (", nodeId=" + node.ToString()) : "",
				isSoft ? " (soft)" : "",
				"}"
			}))) + "]")) + "])";
		}

		// Token: 0x04003092 RID: 12434
		private static readonly Regex WhitespaceRegex = new Regex("\\s+", RegexOptions.Compiled);
	}
}
