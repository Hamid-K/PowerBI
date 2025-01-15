using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000132 RID: 306
	public class FieldRelationshipAnnotation
	{
		// Token: 0x060007F5 RID: 2037 RVA: 0x00010773 File Offset: 0x0000E973
		public FieldRelationshipAnnotation(IConceptualColumn relatedToSource, IReadOnlyList<IConceptualColumn> relatedToFields, IReadOnlyList<IConceptualColumn> allFieldsOnPath)
		{
			this._relatedToSource = relatedToSource;
			this._relatedToFields = relatedToFields;
			this._allFieldsOnPath = allFieldsOnPath;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00010790 File Offset: 0x0000E990
		protected FieldRelationshipAnnotation()
		{
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00010798 File Offset: 0x0000E998
		public IReadOnlyList<IConceptualColumn> RelatedToFields
		{
			get
			{
				return this._relatedToFields;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x000107A0 File Offset: 0x0000E9A0
		public IConceptualColumn RelatedToSource
		{
			get
			{
				return this._relatedToSource;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x000107A8 File Offset: 0x0000E9A8
		public IReadOnlyList<IConceptualColumn> AllFieldsOnPath
		{
			get
			{
				return this._allFieldsOnPath;
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000107B0 File Offset: 0x0000E9B0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.RelatedToSource != null)
			{
				stringBuilder.Append("\tRelatedToSource:").Append(this.RelatedToSource.Name).AppendLine();
			}
			if (!this.RelatedToFields.IsNullOrEmpty<IConceptualColumn>())
			{
				this.BuildList(this.RelatedToFields, "RelatedToFields", stringBuilder);
			}
			if (!this.AllFieldsOnPath.IsNullOrEmpty<IConceptualColumn>())
			{
				this.BuildList(this.AllFieldsOnPath, "AllFieldsOnPath", stringBuilder);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00010830 File Offset: 0x0000EA30
		private void BuildList(IReadOnlyList<IConceptualColumn> fields, string name, StringBuilder builder)
		{
			builder.AppendFormat("\t{0}:", name);
			Func<IConceptualColumn, string> func = (IConceptualColumn prop) => prop.Name;
			builder.AppendMany(fields.OrderBy((IConceptualColumn f) => f.Name), ",", func);
			builder.Append(Environment.NewLine);
		}

		// Token: 0x040003AB RID: 939
		protected IReadOnlyList<IConceptualColumn> _relatedToFields;

		// Token: 0x040003AC RID: 940
		protected IConceptualColumn _relatedToSource;

		// Token: 0x040003AD RID: 941
		protected IReadOnlyList<IConceptualColumn> _allFieldsOnPath;
	}
}
