using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002FE RID: 766
	internal sealed class OnOperation : SchemaElement
	{
		// Token: 0x06002465 RID: 9317 RVA: 0x00066FB9 File Offset: 0x000651B9
		public OnOperation(RelationshipEnd parentElement, Operation operation)
			: base(parentElement, null)
		{
			this.Operation = operation;
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x00066FCA File Offset: 0x000651CA
		// (set) Token: 0x06002467 RID: 9319 RVA: 0x00066FD2 File Offset: 0x000651D2
		public Operation Operation { get; private set; }

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x00066FDB File Offset: 0x000651DB
		// (set) Token: 0x06002469 RID: 9321 RVA: 0x00066FE3 File Offset: 0x000651E3
		public Action Action { get; private set; }

		// Token: 0x0600246A RID: 9322 RVA: 0x00066FEC File Offset: 0x000651EC
		protected override bool ProhibitAttribute(string namespaceUri, string localName)
		{
			if (base.ProhibitAttribute(namespaceUri, localName))
			{
				return true;
			}
			if (namespaceUri == null)
			{
				localName == "Name";
				return false;
			}
			return false;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0006700C File Offset: 0x0006520C
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Action"))
			{
				this.HandleActionAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x00067030 File Offset: 0x00065230
		private void HandleActionAttribute(XmlReader reader)
		{
			string text = reader.Value.Trim();
			if (text != null)
			{
				if (text == "None")
				{
					this.Action = Action.None;
					return;
				}
				if (text == "Cascade")
				{
					this.Action = Action.Cascade;
					return;
				}
			}
			base.AddError(ErrorCode.InvalidAction, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidAction(reader.Value, this.ParentElement.FQName));
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x00067098 File Offset: 0x00065298
		private new RelationshipEnd ParentElement
		{
			get
			{
				return (RelationshipEnd)base.ParentElement;
			}
		}
	}
}
