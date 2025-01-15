using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002076 RID: 8310
	[XmlRoot("Formulas")]
	public class FormulasConfig : XmlRoot
	{
		// Token: 0x0600CB61 RID: 52065 RVA: 0x00288535 File Offset: 0x00286735
		public FormulasConfig()
		{
			this.formulas = new List<FormulaConfig>();
		}

		// Token: 0x170030FA RID: 12538
		// (get) Token: 0x0600CB62 RID: 52066 RVA: 0x00288548 File Offset: 0x00286748
		[XmlArray("Formulas")]
		[XmlArrayItem("Formula")]
		public List<FormulaConfig> Formulas
		{
			get
			{
				return this.formulas;
			}
		}

		// Token: 0x0600CB63 RID: 52067 RVA: 0x00288550 File Offset: 0x00286750
		public bool TryGetPublishedFormulaConfig(out FormulaConfig publishedFormula)
		{
			publishedFormula = null;
			foreach (FormulaConfig formulaConfig in this.Formulas)
			{
				if (formulaConfig.Published)
				{
					if (publishedFormula != null)
					{
						publishedFormula = null;
						return false;
					}
					publishedFormula = formulaConfig;
				}
			}
			return publishedFormula != null;
		}

		// Token: 0x04006741 RID: 26433
		public const string contentType = "text/xml";

		// Token: 0x04006742 RID: 26434
		public const string fileName = "formulas.xml";

		// Token: 0x04006743 RID: 26435
		private readonly List<FormulaConfig> formulas;
	}
}
