using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x02000162 RID: 354
	public class SequenceSchemaElement<TSequenceProgram, TRegionProgram, TRegion> : SchemaElement<TSequenceProgram, TRegionProgram, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00018C88 File Offset: 0x00016E88
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x00018C90 File Offset: 0x00016E90
		public SchemaElement<TSequenceProgram, TRegionProgram, TRegion> Element { get; private set; }

		// Token: 0x060007E8 RID: 2024 RVA: 0x00018C99 File Offset: 0x00016E99
		public SequenceSchemaElement(string name, SchemaElement<TSequenceProgram, TRegionProgram, TRegion> child, bool nullable, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner)
			: base(name, null, nullable, learner)
		{
			this.Element = child;
			child.Parent = this;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00018CB4 File Offset: 0x00016EB4
		public SequenceSchemaElement(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> parent, string name, bool nullable, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner)
			: base(name, parent, nullable, learner)
		{
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00018CC1 File Offset: 0x00016EC1
		public override void AddChild(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> element)
		{
			if (this.Element == null)
			{
				this.Element = element;
				return;
			}
			throw new Exception("Cannot add more than one child to a Sequence. Perhaps you should add this element to the struct.");
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00018CDD File Offset: 0x00016EDD
		public override List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> Children
		{
			get
			{
				return new List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> { this.Element };
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00018CF0 File Offset: 0x00016EF0
		protected override IEnumerable<TRegion> RunWithoutCache(TRegion s, int k)
		{
			IEnumerable<TRegion> enumerable = this.Element.Run(s, k);
			TRegion[] array = (enumerable as TRegion[]) ?? enumerable.ToArray<TRegion>();
			if (array.IsAny<TRegion>())
			{
				base.ExecutionCache[Tuple.Create<TRegion, int>(s, k)] = array;
			}
			return array;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00018D38 File Offset: 0x00016F38
		public override T AcceptVisitor<T>(SchemaVisitor<T, TSequenceProgram, TRegionProgram, TRegion> visitor)
		{
			return visitor.VisitSequence(this);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00018D44 File Offset: 0x00016F44
		public override void LearnElementAndChildren(IEnumerable<DocumentSpecInterface> specs, int k = 1, bool learnAll = false)
		{
			SchemaElement<TSequenceProgram, TRegionProgram, TRegion> element = this.Element;
			if (element is SequenceSchemaElement<TSequenceProgram, TRegionProgram, TRegion>)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Learning fails. A sequence element ({0}) cannot be directly nested in another sequence element ({1}).", new object[]
				{
					element.Name,
					element.Parent.Name
				})));
			}
			base.ComputeReferencedElement();
			element.LearnElementAndChildren(specs, k, learnAll);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00018DA1 File Offset: 0x00016FA1
		public override TreeElement<TRegion> RunTree(TRegion s)
		{
			TreeElement<TRegion> treeElement = this.Element.RunTree(s);
			treeElement.Name = base.Name;
			return treeElement;
		}
	}
}
