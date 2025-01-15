using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x02000149 RID: 329
	public class BotSchemaElement<TSequenceProgram, TRegionProgram, TRegion> : SchemaElement<TSequenceProgram, TRegionProgram, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x0600074C RID: 1868 RVA: 0x00016F89 File Offset: 0x00015189
		private BotSchemaElement()
		{
			base.Name = "⊥";
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00016F9C File Offset: 0x0001519C
		public static BotSchemaElement<TSequenceProgram, TRegionProgram, TRegion> Instance { get; } = new BotSchemaElement<TSequenceProgram, TRegionProgram, TRegion>();

		// Token: 0x0600074E RID: 1870 RVA: 0x00016FA3 File Offset: 0x000151A3
		public override void AddChild(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> element)
		{
			throw new Exception(element.ToString() + " should not have been added as a child element of BotSchemaElement.");
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00016FBA File Offset: 0x000151BA
		public override List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> Children
		{
			get
			{
				return new List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>>();
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00016FC1 File Offset: 0x000151C1
		protected override IEnumerable<TRegion> RunWithoutCache(TRegion s, int k)
		{
			return new TRegion[] { s };
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00016FD1 File Offset: 0x000151D1
		public override T AcceptVisitor<T>(SchemaVisitor<T, TSequenceProgram, TRegionProgram, TRegion> visitor)
		{
			return visitor.VisitBot(this);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public override void LearnElementAndChildren(IEnumerable<DocumentSpecInterface> specs, int k = 1, bool learnAll = false)
		{
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00016FDA File Offset: 0x000151DA
		public override TreeElement<TRegion> RunTree(TRegion s)
		{
			return new NullTreeElement<TRegion>(base.Name);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000FFF2 File Offset: 0x0000E1F2
		public override string Serialize(ASTSerializationFormat format)
		{
			throw new InvalidOperationException("BotSchemaElement does not have a string representation");
		}
	}
}
