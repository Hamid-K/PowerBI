using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D90 RID: 3472
	internal class CdpaRelationships
	{
		// Token: 0x06005E84 RID: 24196 RVA: 0x001470E3 File Offset: 0x001452E3
		public CdpaRelationships(ListValue relationships)
		{
			this.relationships = new Dictionary<QualifiedName, HashSet<QualifiedName>>();
			this.PopulateRelationships(relationships);
		}

		// Token: 0x06005E85 RID: 24197 RVA: 0x00147100 File Offset: 0x00145300
		public IEnumerable<QualifiedName> GetRelatedAttributes(QualifiedName qualifiedName)
		{
			HashSet<QualifiedName> hashSet;
			if (this.relationships.TryGetValue(qualifiedName, out hashSet))
			{
				return hashSet;
			}
			return EmptyArray<QualifiedName>.Instance;
		}

		// Token: 0x06005E86 RID: 24198 RVA: 0x00147124 File Offset: 0x00145324
		private void PopulateRelationships(ListValue relationships)
		{
			foreach (IValueReference valueReference in relationships)
			{
				RecordValue asRecord = valueReference.Value.AsRecord;
				QualifiedName qualifiedName = QualifiedName.New(asRecord["fromTable"].AsString).Qualify(asRecord["fromColumn"].AsString);
				QualifiedName qualifiedName2 = QualifiedName.New(asRecord["toTable"].AsString).Qualify(asRecord["toColumn"].AsString);
				HashSet<QualifiedName> hashSet;
				if (!this.relationships.TryGetValue(qualifiedName, out hashSet))
				{
					hashSet = new HashSet<QualifiedName>();
					this.relationships.Add(qualifiedName, hashSet);
				}
				HashSet<QualifiedName> hashSet2;
				if (!this.relationships.TryGetValue(qualifiedName2, out hashSet2))
				{
					hashSet2 = new HashSet<QualifiedName>();
					this.relationships.Add(qualifiedName2, hashSet2);
				}
				hashSet.Add(qualifiedName2);
				hashSet2.Add(qualifiedName);
			}
		}

		// Token: 0x040033F0 RID: 13296
		private readonly Dictionary<QualifiedName, HashSet<QualifiedName>> relationships;
	}
}
