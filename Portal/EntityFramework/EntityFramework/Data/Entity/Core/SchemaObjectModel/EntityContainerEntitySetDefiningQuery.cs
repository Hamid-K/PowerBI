using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002EB RID: 747
	internal sealed class EntityContainerEntitySetDefiningQuery : SchemaElement
	{
		// Token: 0x060023A3 RID: 9123 RVA: 0x00064D10 File Offset: 0x00062F10
		public EntityContainerEntitySetDefiningQuery(EntityContainerEntitySet parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x00064D1A File Offset: 0x00062F1A
		public string Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x00064D22 File Offset: 0x00062F22
		protected override bool HandleText(XmlReader reader)
		{
			this._query = reader.Value;
			return true;
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x00064D31 File Offset: 0x00062F31
		internal override void Validate()
		{
			base.Validate();
			if (string.IsNullOrEmpty(this._query))
			{
				base.AddError(ErrorCode.EmptyDefiningQuery, EdmSchemaErrorSeverity.Error, Strings.EmptyDefiningQuery);
			}
		}

		// Token: 0x04000C28 RID: 3112
		private string _query;
	}
}
