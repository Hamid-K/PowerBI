using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200014D RID: 333
	public class ConvertSchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent, TSequenceProgramChild, TRegionProgramChild, TRegionChild> : SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> where TSequenceProgramParent : SequenceExtractionProgram<TSequenceProgramParent, TRegionParent> where TRegionProgramParent : RegionExtractionProgram<TRegionProgramParent, TRegionParent> where TRegionParent : IRegion<TRegionParent> where TSequenceProgramChild : SequenceExtractionProgram<TSequenceProgramChild, TRegionChild> where TRegionProgramChild : RegionExtractionProgram<TRegionProgramChild, TRegionChild> where TRegionChild : IRegion<TRegionChild>
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00017097 File Offset: 0x00015297
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x0001709F File Offset: 0x0001529F
		public SchemaElement<TSequenceProgramChild, TRegionProgramChild, TRegionChild> ChildElement { get; set; }

		// Token: 0x06000766 RID: 1894 RVA: 0x000170A8 File Offset: 0x000152A8
		public ConvertSchemaElement(ConvertSchemaElementFactory<TSequenceProgramParent, TRegionProgramParent, TRegionParent, TSequenceProgramChild, TRegionProgramChild, TRegionChild> converter, string name, bool nullable, SchemaElement<TSequenceProgramChild, TRegionProgramChild, TRegionChild> childElement, ExtractionLearner<TSequenceProgramParent, TRegionProgramParent, TRegionParent> learner)
			: base(name, null, nullable, learner)
		{
			this.Converter = converter;
			this.AddChildConverter(childElement);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000170CF File Offset: 0x000152CF
		public ConvertSchemaElement(ConvertSchemaElementFactory<TSequenceProgramParent, TRegionProgramParent, TRegionParent, TSequenceProgramChild, TRegionProgramChild, TRegionChild> converter, string name, SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> parent, bool nullable, ExtractionLearner<TSequenceProgramParent, TRegionProgramParent, TRegionParent> learner)
			: base(name, parent, nullable, learner)
		{
			this.Converter = converter;
			this.ChildElement = null;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000170F6 File Offset: 0x000152F6
		public override void AddChild(SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> element)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000170FD File Offset: 0x000152FD
		public void AddChildConverter(SchemaElement<TSequenceProgramChild, TRegionProgramChild, TRegionChild> element)
		{
			this.ChildElement = element;
			if (element != null)
			{
				element.Parent = null;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00017110 File Offset: 0x00015310
		public override List<SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent>> Children { get; } = new List<SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent>>();

		// Token: 0x0600076B RID: 1899 RVA: 0x00017118 File Offset: 0x00015318
		public override void ResetExecutionCacheAndChildren()
		{
			base.ResetExecutionCacheAndChildren();
			SchemaElement<TSequenceProgramChild, TRegionProgramChild, TRegionChild> childElement = this.ChildElement;
			if (childElement == null)
			{
				return;
			}
			childElement.ResetExecutionCacheAndChildren();
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00017130 File Offset: 0x00015330
		public string FormatCodeTemplate(bool _isSequence)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("<convert name=\"{{:0}}\" reference=\"{{:1}}\" converterName=\"{0}\">{{NI}}{{N:2}}{{ND}}</convert>", new object[] { this.Converter.ConverterName }));
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00017155 File Offset: 0x00015355
		public override T AcceptVisitor<T>(SchemaVisitor<T, TSequenceProgramParent, TRegionProgramParent, TRegionParent> visitor)
		{
			return visitor.VisitConvert<TSequenceProgramChild, TRegionProgramChild, TRegionChild>(this);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001715E File Offset: 0x0001535E
		public override void LearnElementAndChildren(IEnumerable<DocumentSpecInterface> specs, int k = 1, bool learnAll = false)
		{
			SchemaElement<TSequenceProgramChild, TRegionProgramChild, TRegionChild> childElement = this.ChildElement;
			if (childElement == null)
			{
				return;
			}
			childElement.LearnElementAndChildren(specs, k, learnAll);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00017174 File Offset: 0x00015374
		public override TreeElement<TRegionParent> RunTree(TRegionParent s)
		{
			TRegionChild tregionChild = this.Converter.ConvertToChildRegion(s);
			string name = base.Name;
			TRegionChild tregionChild2 = tregionChild;
			SchemaElement<TSequenceProgramChild, TRegionProgramChild, TRegionChild> childElement = this.ChildElement;
			return new ConvertTreeElement<TRegionParent, TRegionChild>(name, tregionChild2, (childElement != null) ? childElement.RunTree(tregionChild) : null);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000171AD File Offset: 0x000153AD
		protected override IEnumerable<TRegionParent> RunWithoutCache(TRegionParent s, int k)
		{
			return new List<TRegionParent> { s };
		}

		// Token: 0x04000341 RID: 833
		public ConvertSchemaElementFactory<TSequenceProgramParent, TRegionProgramParent, TRegionParent, TSequenceProgramChild, TRegionProgramChild, TRegionChild> Converter;
	}
}
