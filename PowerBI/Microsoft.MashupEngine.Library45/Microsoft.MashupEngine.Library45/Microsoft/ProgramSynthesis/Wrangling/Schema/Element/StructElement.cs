using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Schema.Element
{
	// Token: 0x02000147 RID: 327
	public class StructElement<TRegion> : ISchemaElement<TRegion>
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x00016E88 File Offset: 0x00015088
		public TTranslation Accept<TTranslation>(SchemaElementVisitor<TTranslation, TRegion> visitor)
		{
			return visitor.VisitStructElement(this);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00016E91 File Offset: 0x00015091
		public StructElement(string name, bool isNullable)
		{
			this.Name = name;
			this.IsNullable = isNullable;
			this.UseOutput = true;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00016EAE File Offset: 0x000150AE
		public StructElement(string name, bool isNullable, bool useOutput, IEnumerable<ISchemaElement<TRegion>> children)
			: this(name, isNullable)
		{
			this.UseOutput = useOutput;
			this.Children = children;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00016EC7 File Offset: 0x000150C7
		public IEnumerable<ISchemaElement<TRegion>> Children { get; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00016ECF File Offset: 0x000150CF
		public string Name { get; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00016ED7 File Offset: 0x000150D7
		public bool IsNullable { get; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00016EDF File Offset: 0x000150DF
		public bool UseOutput { get; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00016EE8 File Offset: 0x000150E8
		public IReadOnlyList<string> DescendantOutputFields
		{
			get
			{
				if (this._descendantOutputFields != null)
				{
					return this._descendantOutputFields;
				}
				IEnumerable<ISchemaElement<TRegion>> children = this.Children;
				IEnumerable<string> enumerable;
				if (children == null)
				{
					enumerable = null;
				}
				else
				{
					enumerable = children.SelectMany((ISchemaElement<TRegion> c) => c.DescendantOutputFields);
				}
				IEnumerable<string> enumerable2 = enumerable ?? Enumerable.Empty<string>();
				this._descendantOutputFields = ((!this.UseOutput) ? enumerable2.ToList<string>() : new string[] { this.Name }.Concat(enumerable2).ToList<string>());
				return this._descendantOutputFields;
			}
		}

		// Token: 0x04000337 RID: 823
		private IReadOnlyList<string> _descendantOutputFields;
	}
}
