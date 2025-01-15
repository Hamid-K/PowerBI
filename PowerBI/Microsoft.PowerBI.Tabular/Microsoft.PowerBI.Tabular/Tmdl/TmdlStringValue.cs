using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Tabular.Extensions;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x0200014B RID: 331
	internal sealed class TmdlStringValue : TmdlValue
	{
		// Token: 0x06001551 RID: 5457 RVA: 0x0008F644 File Offset: 0x0008D844
		public TmdlStringValue(string[] lines, TmdlStringFormat format = TmdlStringFormat.Block, bool useExpressionSemantics = true)
			: base(TmdlValueType.String, string.Join(Environment.NewLine, lines), format == TmdlStringFormat.Block, true)
		{
			this.lines = new TmdlStringValue.ReadOnlyStringArrayWrapper(lines);
			this.Format = format;
			this.UseExpressionSemantics = useExpressionSemantics;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0008F677 File Offset: 0x0008D877
		public TmdlStringValue(string value, string rawvalue = null, bool useExpressionSemantics = false)
			: base(TmdlValueType.String, rawvalue ?? value, false, true)
		{
			this.lines = new TmdlStringValue.ReadOnlyStringArrayWrapper(value);
			this.Format = TmdlStringFormat.Inline;
			this.UseExpressionSemantics = useExpressionSemantics;
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0008F6A2 File Offset: 0x0008D8A2
		public static TmdlValue Null { get; } = new TmdlStringValue(null, null, false);

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0008F6A9 File Offset: 0x0008D8A9
		public static TmdlValue Empty { get; } = new TmdlStringValue(string.Empty, null, false);

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x0008F6B0 File Offset: 0x0008D8B0
		public override object Value
		{
			get
			{
				return this.GetValueImpl();
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0008F6B8 File Offset: 0x0008D8B8
		public TmdlStringFormat Format { get; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0008F6C0 File Offset: 0x0008D8C0
		public bool UseExpressionSemantics { get; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0008F6C8 File Offset: 0x0008D8C8
		public IReadOnlyList<string> Lines
		{
			get
			{
				return this.lines;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0008F6D5 File Offset: 0x0008D8D5
		internal bool ContainsExpressionThatRequiresQuotes
		{
			get
			{
				if (this.containsExpressionThatRequiresQuotes == null)
				{
					this.containsExpressionThatRequiresQuotes = new bool?(this.CheckIfContainsExpressionThatRequiresQuotes());
				}
				return this.containsExpressionThatRequiresQuotes.Value;
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0008F700 File Offset: 0x0008D900
		internal void UpdateLines(string[] lines)
		{
			this.lines = new TmdlStringValue.ReadOnlyStringArrayWrapper(lines);
			this.containsExpressionThatRequiresQuotes = null;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0008F71C File Offset: 0x0008D91C
		private protected override void WriteValue(ITmdlWriter writer)
		{
			if (this.Format == TmdlStringFormat.Inline)
			{
				string text = this.GetValueImpl() ?? string.Empty;
				if (!this.UseExpressionSemantics)
				{
					int length = text.Length;
					if (length != 0)
					{
						if (length != 1)
						{
							if (char.IsWhiteSpace(text, 0) || char.IsWhiteSpace(text, length - 1) || (text[0] == '"' && text[length - 1] == '"'))
							{
								text = string.Format("\"{0}\"", text.EscapeString('"'));
							}
						}
						else if (text[0] == '"')
						{
							text = "\"\"\"\"";
						}
						else if (char.IsWhiteSpace(text, 0))
						{
							text = string.Format("\"{0}\"", text[0]);
						}
					}
					else
					{
						text = "\"\"";
					}
				}
				writer.Write(text, Array.Empty<object>());
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0008F7E8 File Offset: 0x0008D9E8
		private protected override void WriteBody(ITmdlWriter writer)
		{
			if (this.Format == TmdlStringFormat.Block)
			{
				foreach (string text in this.Lines)
				{
					writer.WriteLine(text, Array.Empty<object>());
				}
			}
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0008F844 File Offset: 0x0008DA44
		private string GetValueImpl()
		{
			if (this.Lines.Count != 1)
			{
				return this.Lines.JoinLines(null);
			}
			return this.Lines[0];
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0008F870 File Offset: 0x0008DA70
		private bool CheckIfContainsExpressionThatRequiresQuotes()
		{
			if (!this.UseExpressionSemantics || this.Format == TmdlStringFormat.Inline)
			{
				return false;
			}
			int count = this.lines.Count;
			if (count == 0)
			{
				return true;
			}
			if (count == 1)
			{
				return string.IsNullOrEmpty(this.lines[0]) || char.IsWhiteSpace(this.lines[0], 0) || char.IsWhiteSpace(this.lines[0], this.lines[0].Length - 1);
			}
			if (string.IsNullOrWhiteSpace(this.lines[this.lines.Count - 1]))
			{
				return true;
			}
			for (int i = 0; i < this.lines.Count; i++)
			{
				if (!string.IsNullOrEmpty(this.lines[i]) && char.IsWhiteSpace(this.lines[i], this.lines[i].Length - 1))
				{
					return true;
				}
			}
			int num = 0;
			while (num < this.lines.Count && string.IsNullOrEmpty(this.lines[num]))
			{
				num++;
			}
			char c = this.lines[num][0];
			if (char.IsWhiteSpace(c))
			{
				IndentationType indentationType;
				int num2;
				Indentation.IdentifyLeadingIndentation(this.lines[num], out indentationType, out num2);
				bool flag = true;
				for (int j = num + 1; j < this.lines.Count; j++)
				{
					if (!string.IsNullOrEmpty(this.lines[j]))
					{
						int num3;
						Indentation.IdentifyLeadingIndentation(this.lines[j], out indentationType, out num3);
						if (num3 < num2)
						{
							return true;
						}
						if (this.lines[j][0] != c)
						{
							flag = false;
						}
					}
				}
				return flag;
			}
			return false;
		}

		// Token: 0x040003AE RID: 942
		public const string ExpressionQuoteMarker = "```";

		// Token: 0x040003AF RID: 943
		private TmdlStringValue.ReadOnlyStringArrayWrapper lines;

		// Token: 0x040003B0 RID: 944
		private bool? containsExpressionThatRequiresQuotes;

		// Token: 0x0200032D RID: 813
		private struct ReadOnlyStringArrayWrapper : IReadOnlyList<string>, IReadOnlyCollection<string>, IEnumerable<string>, IEnumerable
		{
			// Token: 0x06002528 RID: 9512 RVA: 0x000E78BB File Offset: 0x000E5ABB
			public ReadOnlyStringArrayWrapper(string item)
			{
				this.items = new string[] { item };
			}

			// Token: 0x06002529 RID: 9513 RVA: 0x000E78CD File Offset: 0x000E5ACD
			public ReadOnlyStringArrayWrapper(string[] items)
			{
				this.items = items;
			}

			// Token: 0x1700078D RID: 1933
			// (get) Token: 0x0600252A RID: 9514 RVA: 0x000E78D6 File Offset: 0x000E5AD6
			public int Count
			{
				get
				{
					return this.items.Length;
				}
			}

			// Token: 0x1700078E RID: 1934
			public string this[int index]
			{
				get
				{
					return this.items[index];
				}
			}

			// Token: 0x0600252C RID: 9516 RVA: 0x000E78EA File Offset: 0x000E5AEA
			public IEnumerator<string> GetEnumerator()
			{
				return this.GetItems().GetEnumerator();
			}

			// Token: 0x0600252D RID: 9517 RVA: 0x000E78F7 File Offset: 0x000E5AF7
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetItems().GetEnumerator();
			}

			// Token: 0x0600252E RID: 9518 RVA: 0x000E7904 File Offset: 0x000E5B04
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private IEnumerable<string> GetItems()
			{
				int num;
				for (int i = 0; i < this.items.Length; i = num + 1)
				{
					yield return this.items[i];
					num = i;
				}
				yield break;
			}

			// Token: 0x04000DEF RID: 3567
			private readonly string[] items;
		}
	}
}
