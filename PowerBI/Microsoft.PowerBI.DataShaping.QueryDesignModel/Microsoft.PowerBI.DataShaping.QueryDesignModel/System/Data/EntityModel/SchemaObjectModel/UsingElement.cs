using System;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200003A RID: 58
	internal class UsingElement : SchemaElement
	{
		// Token: 0x0600071D RID: 1821 RVA: 0x0000D684 File Offset: 0x0000B884
		internal UsingElement(Schema parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0000D68D File Offset: 0x0000B88D
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0000D695 File Offset: 0x0000B895
		public virtual string Alias
		{
			get
			{
				return this._alias;
			}
			private set
			{
				this._alias = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0000D69E File Offset: 0x0000B89E
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0000D6A6 File Offset: 0x0000B8A6
		public virtual string NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
			private set
			{
				this._namespaceName = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0000D6AF File Offset: 0x0000B8AF
		public override string FQName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0000D6B2 File Offset: 0x0000B8B2
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

		// Token: 0x06000724 RID: 1828 RVA: 0x0000D6D2 File Offset: 0x0000B8D2
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Namespace"))
			{
				this.HandleNamespaceAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Alias"))
			{
				this.HandleAliasAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000D70C File Offset: 0x0000B90C
		private void HandleNamespaceAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this.NamespaceName, null);
			if (returnValue.Succeeded)
			{
				this.NamespaceName = returnValue.Value;
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000D73C File Offset: 0x0000B93C
		private void HandleAliasAttribute(XmlReader reader)
		{
			this.Alias = base.HandleUndottedNameAttribute(reader, this.Alias);
		}

		// Token: 0x04000677 RID: 1655
		private string _alias;

		// Token: 0x04000678 RID: 1656
		private string _namespaceName;
	}
}
