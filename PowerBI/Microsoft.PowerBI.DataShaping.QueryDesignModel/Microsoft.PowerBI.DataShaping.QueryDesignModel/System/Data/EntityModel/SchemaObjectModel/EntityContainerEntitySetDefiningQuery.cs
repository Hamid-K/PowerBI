using System;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000021 RID: 33
	internal sealed class EntityContainerEntitySetDefiningQuery : SchemaElement
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x0000AF84 File Offset: 0x00009184
		public EntityContainerEntitySetDefiningQuery(EntityContainerEntitySet parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0000AF8D File Offset: 0x0000918D
		public string Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0000AF95 File Offset: 0x00009195
		protected override bool HandleText(XmlReader reader)
		{
			this._query = reader.Value;
			return true;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000AFA4 File Offset: 0x000091A4
		internal override void Validate()
		{
			base.Validate();
			if (string.IsNullOrEmpty(this._query))
			{
				base.AddError(ErrorCode.EmptyDefiningQuery, EdmSchemaErrorSeverity.Error, Strings.EmptyDefiningQuery);
			}
		}

		// Token: 0x040005B5 RID: 1461
		private string _query;
	}
}
