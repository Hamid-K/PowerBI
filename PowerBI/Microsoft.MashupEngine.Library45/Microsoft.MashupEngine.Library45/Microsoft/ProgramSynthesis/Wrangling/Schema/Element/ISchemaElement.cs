using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.Schema.Element
{
	// Token: 0x02000144 RID: 324
	public interface ISchemaElement<TRegion>
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000732 RID: 1842
		string Name { get; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000733 RID: 1843
		bool IsNullable { get; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000734 RID: 1844
		bool UseOutput { get; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000735 RID: 1845
		IReadOnlyList<string> DescendantOutputFields { get; }

		// Token: 0x06000736 RID: 1846
		TTranslation Accept<TTranslation>(SchemaElementVisitor<TTranslation, TRegion> visitor);
	}
}
