using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Semantics
{
	// Token: 0x02001177 RID: 4471
	public interface IDomNode
	{
		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x060084E7 RID: 34023
		HtmlDoc Document { get; }

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x060084E8 RID: 34024
		string NodeName { get; }

		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x060084E9 RID: 34025
		int Index { get; }

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x060084EA RID: 34026
		int IndexFromLast { get; }

		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x060084EB RID: 34027
		string[] Classes { get; }

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x060084EC RID: 34028
		HashSet<string> LeafNodes { get; }

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x060084ED RID: 34029
		string Id { get; }

		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x060084EE RID: 34030
		string InnerText { get; }

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x060084EF RID: 34031
		string TrimmedInnerText { get; }

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x060084F0 RID: 34032
		string NormalizedInnerText { get; }

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x060084F1 RID: 34033
		int ChildrenCount { get; }

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x060084F2 RID: 34034
		string Title { get; }

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x060084F3 RID: 34035
		int Start { get; }

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x060084F4 RID: 34036
		int End { get; }

		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x060084F5 RID: 34037
		IDomNode Parent { get; }

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x060084F6 RID: 34038
		DomNode NextSibling { get; }

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x060084F7 RID: 34039
		string SpecificSelector { get; }

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x060084F8 RID: 34040
		string NodeNameClassSelector { get; }

		// Token: 0x060084F9 RID: 34041
		string GetOuterHtml();

		// Token: 0x060084FA RID: 34042
		bool HasMinimalText();

		// Token: 0x060084FB RID: 34043
		string GetVisibleTextContent();

		// Token: 0x060084FC RID: 34044
		int GetNodeHeight();

		// Token: 0x060084FD RID: 34045
		string GetAttribute(string attrib);

		// Token: 0x060084FE RID: 34046
		IEnumerable<string> GetAttributes();

		// Token: 0x060084FF RID: 34047
		IEnumerable<IDomNode> GetChildren();

		// Token: 0x06008500 RID: 34048
		IEnumerable<IDomNode> GetDescendants(bool includeSelf = true);

		// Token: 0x06008501 RID: 34049
		bool Contains(IDomNode other);

		// Token: 0x06008502 RID: 34050
		IEnumerable<IDomNode> GetYoungerSiblings();

		// Token: 0x06008503 RID: 34051
		IEnumerable<IDomNode> GetOlderSiblings();

		// Token: 0x06008504 RID: 34052
		bool IsAncestor(IDomNode n);

		// Token: 0x06008505 RID: 34053
		WebRegion ToWebRegion();

		// Token: 0x06008506 RID: 34054
		string GetStyle(string name);

		// Token: 0x06008507 RID: 34055
		IEnumerable<string> GetStyles();

		// Token: 0x06008508 RID: 34056
		List<DomNode> GetDescendantsByCss(string selector);
	}
}
