using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x02000101 RID: 257
	public sealed class ScoreModifier
	{
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0002AAFD File Offset: 0x00028CFD
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x0002AB05 File Offset: 0x00028D05
		public int Offset
		{
			get
			{
				return this.m_offset;
			}
			set
			{
				this.m_offset = value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0002AB0E File Offset: 0x00028D0E
		public FilterCollection Filters
		{
			get
			{
				return this.m_filters;
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002AB16 File Offset: 0x00028D16
		public bool AppliesTo(DsvItem dsvItem)
		{
			return Filter.IsMatch(dsvItem, this.m_filters);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002AB24 File Offset: 0x00028D24
		internal static ScoreModifier FromReader(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			ScoreModifier scoreModifier = new ScoreModifier();
			xr.LoadObject("scoreModifier", new ScoreModifier.ScoreModifierLoader(scoreModifier, objectFactory));
			return scoreModifier;
		}

		// Token: 0x04000554 RID: 1364
		internal const string ScoreModifierElem = "scoreModifier";

		// Token: 0x04000555 RID: 1365
		private const string OffsetAttr = "offset";

		// Token: 0x04000556 RID: 1366
		private int m_offset;

		// Token: 0x04000557 RID: 1367
		private readonly FilterCollection m_filters = new FilterCollection();

		// Token: 0x020001DA RID: 474
		private class ScoreModifierLoader : ModelingXmlLoaderBase<ScoreModifier>
		{
			// Token: 0x060011B0 RID: 4528 RVA: 0x000370C2 File Offset: 0x000352C2
			internal ScoreModifierLoader(ScoreModifier item, IXmlObjectFactory objectFactory)
				: base(item)
			{
				this.m_objectFactory = objectFactory;
			}

			// Token: 0x060011B1 RID: 4529 RVA: 0x000370D2 File Offset: 0x000352D2
			public override bool LoadXmlAttribute(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "offset")
				{
					base.Item.Offset = xr.ReadValueAsInt();
					return true;
				}
				return base.LoadXmlAttribute(xr);
			}

			// Token: 0x060011B2 RID: 4530 RVA: 0x00037108 File Offset: 0x00035308
			public override bool LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "filter")
				{
					base.Item.Filters.Add(this.m_objectFactory.CreateFilter(xr));
					return true;
				}
				return base.LoadXmlElement(xr);
			}

			// Token: 0x0400080A RID: 2058
			private readonly IXmlObjectFactory m_objectFactory;
		}
	}
}
