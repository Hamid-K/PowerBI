using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002F4 RID: 756
	internal sealed class FunctionCommandText : SchemaElement
	{
		// Token: 0x06002417 RID: 9239 RVA: 0x0006630E File Offset: 0x0006450E
		public FunctionCommandText(Function parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x00066318 File Offset: 0x00064518
		public string CommandText
		{
			get
			{
				return this._commandText;
			}
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x00066320 File Offset: 0x00064520
		protected override bool HandleText(XmlReader reader)
		{
			this._commandText = reader.Value;
			return true;
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x0006632F File Offset: 0x0006452F
		internal override void Validate()
		{
			base.Validate();
			if (string.IsNullOrEmpty(this._commandText))
			{
				base.AddError(ErrorCode.EmptyCommandText, EdmSchemaErrorSeverity.Error, Strings.EmptyCommandText);
			}
		}

		// Token: 0x04000CE1 RID: 3297
		private string _commandText;
	}
}
