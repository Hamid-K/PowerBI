using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Schema.Element
{
	// Token: 0x02000146 RID: 326
	public class SequenceElement<TRegion> : ISchemaElement<TRegion>
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x00016DD7 File Offset: 0x00014FD7
		public SequenceElement(string name, bool isNullable, bool useOutput, ISchemaElement<TRegion> child)
		{
			this.Name = name;
			this.IsNullable = isNullable;
			this.UseOutput = useOutput;
			this.Child = child;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00016DFC File Offset: 0x00014FFC
		public ISchemaElement<TRegion> Child { get; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00016E04 File Offset: 0x00015004
		public string Name { get; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00016E0C File Offset: 0x0001500C
		public bool IsNullable { get; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x00016E14 File Offset: 0x00015014
		public bool UseOutput { get; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00016E1C File Offset: 0x0001501C
		public IReadOnlyList<string> DescendantOutputFields
		{
			get
			{
				if (this._descendantOutputFields != null)
				{
					return this._descendantOutputFields;
				}
				IReadOnlyList<string> readOnlyList;
				if (!this.UseOutput)
				{
					readOnlyList = this.Child.DescendantOutputFields;
				}
				else
				{
					IReadOnlyList<string> readOnlyList2 = new string[] { this.Name }.Concat(this.Child.DescendantOutputFields).ToList<string>();
					readOnlyList = readOnlyList2;
				}
				this._descendantOutputFields = readOnlyList;
				return this._descendantOutputFields;
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00016E7F File Offset: 0x0001507F
		public TTranslation Accept<TTranslation>(SchemaElementVisitor<TTranslation, TRegion> visitor)
		{
			return visitor.VisitSequenceElement(this);
		}

		// Token: 0x04000332 RID: 818
		private IReadOnlyList<string> _descendantOutputFields;
	}
}
