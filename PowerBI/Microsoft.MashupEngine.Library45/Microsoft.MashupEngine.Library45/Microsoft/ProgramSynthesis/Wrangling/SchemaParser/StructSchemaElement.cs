using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x02000163 RID: 355
	public class StructSchemaElement<TSequenceProgram, TRegionProgram, TRegion> : SchemaElement<TSequenceProgram, TRegionProgram, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00018DBB File Offset: 0x00016FBB
		public List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> Members { get; }

		// Token: 0x060007F1 RID: 2033 RVA: 0x00018DC4 File Offset: 0x00016FC4
		public StructSchemaElement(string name, List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> members, bool nullable, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner)
			: base(name, null, nullable, learner)
		{
			this.Members = members;
			foreach (SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement in this.Members)
			{
				schemaElement.Parent = this;
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00018E28 File Offset: 0x00017028
		public StructSchemaElement(string name, SchemaElement<TSequenceProgram, TRegionProgram, TRegion> parent, bool nullable, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner)
			: base(name, parent, nullable, learner)
		{
			this.Members = new List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>>();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00018E40 File Offset: 0x00017040
		public override void AddChild(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> element)
		{
			this.Members.Add(element);
			element.Parent = this;
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00018E55 File Offset: 0x00017055
		public override List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> Children
		{
			get
			{
				return this.Members;
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00018E5D File Offset: 0x0001705D
		public override T AcceptVisitor<T>(SchemaVisitor<T, TSequenceProgram, TRegionProgram, TRegion> visitor)
		{
			return visitor.VisitStruct(this);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00018E68 File Offset: 0x00017068
		public override void LearnElementAndChildren(IEnumerable<DocumentSpecInterface> specs, int k = 1, bool learnAll = false)
		{
			bool flag = specs.All((DocumentSpecInterface spec) => !spec.PositiveContainsKey(base.Name));
			if (flag && this.Members[0].IsNullable)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Learning fails. Struct {0} needs examples because its first member is nullable.", new object[] { base.Name })));
			}
			base.ComputeReferencedElement();
			if (!flag)
			{
				base.LearnElementField(specs, k, learnAll);
			}
			foreach (SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement in this.Children)
			{
				schemaElement.LearnElementAndChildren(specs, k, learnAll);
			}
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00018F1C File Offset: 0x0001711C
		protected override IEnumerable<TRegion> RunWithoutCache(TRegion s, int k)
		{
			if (base.Programs.Count <= k)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Execution fails. Element {0} does not have {1} programs.", new object[] { base.Name, k })));
			}
			return base.RunWithoutCache(s, k);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00018F6C File Offset: 0x0001716C
		public override TreeElement<TRegion> RunTree(TRegion s)
		{
			IEnumerable<TRegion> enumerable = base.Run(s, 0);
			TRegion[] array = (enumerable as TRegion[]) ?? ((enumerable != null) ? enumerable.ToArray<TRegion>() : null);
			if (array == null || !array.IsAny<TRegion>())
			{
				return new NullTreeElement<TRegion>(base.Name);
			}
			return this.RunTreeBoundaries(s, array);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00018FB8 File Offset: 0x000171B8
		public TreeElement<TRegion> RunTreeBoundaries(TRegion s, IEnumerable<TRegion> run)
		{
			TRegion[] array = (run as TRegion[]) ?? run.ToArray<TRegion>();
			List<TreeElement<TRegion>> list = new List<TreeElement<TRegion>>(array.Length);
			TRegion[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				TRegion structBoundary = array2[i];
				if (structBoundary == null)
				{
					list.Add(new NullTreeElement<TRegion>(base.Name));
				}
				else
				{
					IEnumerable<TreeElement<TRegion>> enumerable = from member in this.Members
						let tree = this.IsChildDefiningImplicitBoundaries(member) ? StructSchemaElement<TSequenceProgram, TRegionProgram, TRegion>.FindExplicitElement<TRegion>(member, member.RunTree(s), structBoundary) : member.RunTree(structBoundary)
						select tree;
					list.Add(new StructTreeElement<TRegion>(base.Name, structBoundary, enumerable.ToList<TreeElement<TRegion>>()));
				}
			}
			if (base.ExtractionKind == ExtractionKind.Sequence)
			{
				return new SequenceTreeElement<TRegion>(null, s, list);
			}
			return list[0];
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000190C0 File Offset: 0x000172C0
		private static TreeElement<T> FindExplicitElement<T>(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> element, TreeElement<T> tree, T boundaries) where T : IRegion<T>
		{
			if (tree is NullTreeElement<T>)
			{
				return tree;
			}
			if (element is SequenceSchemaElement<TSequenceProgram, TRegionProgram, TRegion>)
			{
				throw new Exception("This case is not supported yet. It means that an implicit struct is delimited by a sequence, which has no meaning for the moment.");
			}
			FieldTreeElement<T> fieldTreeElement = tree as FieldTreeElement<T>;
			if (fieldTreeElement != null && fieldTreeElement.Name == element.Name && boundaries.Contains(fieldTreeElement.Region))
			{
				return tree;
			}
			SequenceTreeElement<T> sequenceTreeElement = tree as SequenceTreeElement<T>;
			TreeElement<T> treeElement;
			if (sequenceTreeElement != null)
			{
				if ((treeElement = sequenceTreeElement.Children.FirstOrDefault((TreeElement<T> fte) => fte.IsInside(boundaries))) == null)
				{
					return new NullTreeElement<T>(element.Name);
				}
			}
			else
			{
				treeElement = new NullTreeElement<T>(element.Name);
			}
			return treeElement;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001916C File Offset: 0x0001736C
		public bool IsChildDefiningImplicitBoundaries(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> member)
		{
			return !base.Programs.IsAny<IExtractionProgram<TRegion>>() && this.Members.FirstOrDefault<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>>() == member && member != null && (member.Programs.IsAny<IExtractionProgram<TRegion>>() || (member.Children.IsAny<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>>() && member is StructSchemaElement<TSequenceProgram, TRegionProgram, TRegion> && ((StructSchemaElement<TSequenceProgram, TRegionProgram, TRegion>)member).IsChildDefiningImplicitBoundaries(member.Children.First<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>>())));
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000191D8 File Offset: 0x000173D8
		protected override TRegion[] RemoveIntersectingRegions(TRegion[] eleRunArray)
		{
			TRegion[] array = eleRunArray.Where((TRegion x) => x != null).Distinct<TRegion>().ToArray<TRegion>();
			return this.RemoveIntersectingRegionsRecursive(array);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001921C File Offset: 0x0001741C
		private TRegion[] RemoveIntersectingRegionsRecursive(TRegion[] newArray)
		{
			if (!newArray.IsAny<TRegion>())
			{
				return newArray;
			}
			TRegion tregion = newArray[0];
			for (int i = 1; i < newArray.Length; i++)
			{
				if (newArray[i].IntersectNonEmpty(tregion))
				{
					newArray = newArray.Take(i).Concat(newArray.Skip(i + 1)).ToArray<TRegion>();
					return this.RemoveIntersectingRegionsRecursive(newArray);
				}
				tregion = newArray[i];
			}
			return newArray;
		}
	}
}
