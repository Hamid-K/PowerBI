using System;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200002A RID: 42
	internal sealed class FunctionCommandText : SchemaElement
	{
		// Token: 0x060006B4 RID: 1716 RVA: 0x0000C7AD File Offset: 0x0000A9AD
		public FunctionCommandText(Function parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0000C7B6 File Offset: 0x0000A9B6
		public string CommandText
		{
			get
			{
				return this._commandText;
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000C7BE File Offset: 0x0000A9BE
		protected override bool HandleText(XmlReader reader)
		{
			this._commandText = reader.Value;
			return true;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000C7CD File Offset: 0x0000A9CD
		internal override void Validate()
		{
			base.Validate();
			if (string.IsNullOrEmpty(this._commandText))
			{
				base.AddError(ErrorCode.EmptyCommandText, EdmSchemaErrorSeverity.Error, Strings.EmptyCommandText);
			}
		}

		// Token: 0x04000662 RID: 1634
		private string _commandText;
	}
}
