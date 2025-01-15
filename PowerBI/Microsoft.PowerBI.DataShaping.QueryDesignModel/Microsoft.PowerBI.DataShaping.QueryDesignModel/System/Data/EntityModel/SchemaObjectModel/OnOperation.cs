using System;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000035 RID: 53
	internal sealed class OnOperation : SchemaElement
	{
		// Token: 0x060006FA RID: 1786 RVA: 0x0000CFC9 File Offset: 0x0000B1C9
		public OnOperation(RelationshipEnd parentElement, Operation operation)
			: base(parentElement)
		{
			this.Operation = operation;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0000CFD9 File Offset: 0x0000B1D9
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x0000CFE1 File Offset: 0x0000B1E1
		public Operation Operation
		{
			get
			{
				return this._Operation;
			}
			private set
			{
				this._Operation = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0000CFEA File Offset: 0x0000B1EA
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x0000CFF2 File Offset: 0x0000B1F2
		public Action Action
		{
			get
			{
				return this._Action;
			}
			private set
			{
				this._Action = value;
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0000CFFB File Offset: 0x0000B1FB
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

		// Token: 0x06000700 RID: 1792 RVA: 0x0000D01B File Offset: 0x0000B21B
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

		// Token: 0x06000701 RID: 1793 RVA: 0x0000D040 File Offset: 0x0000B240
		private void HandleActionAttribute(XmlReader reader)
		{
			string text = reader.Value.Trim();
			if (text == "None")
			{
				this.Action = Action.None;
				return;
			}
			if (!(text == "Cascade"))
			{
				base.AddError(ErrorCode.InvalidAction, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidAction(reader.Value, this.ParentElement.FQName));
				return;
			}
			this.Action = Action.Cascade;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0000D0A5 File Offset: 0x0000B2A5
		private new RelationshipEnd ParentElement
		{
			get
			{
				return (RelationshipEnd)base.ParentElement;
			}
		}

		// Token: 0x0400066F RID: 1647
		private Operation _Operation;

		// Token: 0x04000670 RID: 1648
		private Action _Action;
	}
}
