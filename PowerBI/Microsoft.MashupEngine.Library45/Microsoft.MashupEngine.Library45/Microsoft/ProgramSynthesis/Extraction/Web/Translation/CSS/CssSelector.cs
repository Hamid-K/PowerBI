using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Translation.CSS
{
	// Token: 0x02001195 RID: 4501
	public class CssSelector
	{
		// Token: 0x060085F6 RID: 34294 RVA: 0x001C20A7 File Offset: 0x001C02A7
		public CssSelector()
		{
			this._data = CssSelector.EmptyFrame();
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x060085F7 RID: 34295 RVA: 0x001C20BA File Offset: 0x001C02BA
		public IReadOnlyList<Record<NodeBasedCssSelector, CssSelector.FrameType>> Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x060085F8 RID: 34296 RVA: 0x001C20C2 File Offset: 0x001C02C2
		private static List<Record<NodeBasedCssSelector, CssSelector.FrameType>> EmptyFrame()
		{
			return new List<Record<NodeBasedCssSelector, CssSelector.FrameType>>
			{
				new Record<NodeBasedCssSelector, CssSelector.FrameType>(new NodeBasedCssSelector(), CssSelector.FrameType.None)
			};
		}

		// Token: 0x060085F9 RID: 34297 RVA: 0x001C20DA File Offset: 0x001C02DA
		public NodeBasedCssSelector Last()
		{
			return this._data.Last<Record<NodeBasedCssSelector, CssSelector.FrameType>>().Item1;
		}

		// Token: 0x060085FA RID: 34298 RVA: 0x001C20EC File Offset: 0x001C02EC
		public void AddFrame(CssSelector.FrameType frameType)
		{
			this._data.Add(new Record<NodeBasedCssSelector, CssSelector.FrameType>(new NodeBasedCssSelector(), frameType));
		}

		// Token: 0x060085FB RID: 34299 RVA: 0x001C2104 File Offset: 0x001C0304
		public override string ToString()
		{
			return string.Join(",", this._data.Skip(1).Aggregate(this._data[0].Item1.ToStringCases(), (IEnumerable<string> acc, Record<NodeBasedCssSelector, CssSelector.FrameType> v) => Seq.Of<IEnumerable<string>>(new IEnumerable<string>[]
			{
				acc,
				v.Item1.ToStringCases()
			}).CartesianProduct<string>().Select(delegate(IEnumerable<string> l)
			{
				List<string> list = l.ToList<string>();
				return list[0] + this.GetFrameConnectionString(v.Item2) + list[1];
			})));
		}

		// Token: 0x060085FC RID: 34300 RVA: 0x001C2143 File Offset: 0x001C0343
		private string GetFrameConnectionString(CssSelector.FrameType f)
		{
			switch (f)
			{
			case CssSelector.FrameType.Child:
				return " > ";
			case CssSelector.FrameType.Descendant:
				return " ";
			case CssSelector.FrameType.RightSibling:
				return " + ";
			default:
				return string.Empty;
			}
		}

		// Token: 0x04003740 RID: 14144
		private readonly List<Record<NodeBasedCssSelector, CssSelector.FrameType>> _data;

		// Token: 0x02001196 RID: 4502
		public enum FrameType
		{
			// Token: 0x04003742 RID: 14146
			Child,
			// Token: 0x04003743 RID: 14147
			Descendant,
			// Token: 0x04003744 RID: 14148
			RightSibling,
			// Token: 0x04003745 RID: 14149
			None
		}
	}
}
