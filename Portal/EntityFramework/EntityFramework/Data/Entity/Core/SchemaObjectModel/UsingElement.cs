using System;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000304 RID: 772
	internal class UsingElement : SchemaElement
	{
		// Token: 0x0600248E RID: 9358 RVA: 0x00067848 File Offset: 0x00065A48
		internal UsingElement(Schema parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x00067852 File Offset: 0x00065A52
		// (set) Token: 0x06002490 RID: 9360 RVA: 0x0006785A File Offset: 0x00065A5A
		public virtual string Alias { get; private set; }

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x00067863 File Offset: 0x00065A63
		// (set) Token: 0x06002492 RID: 9362 RVA: 0x0006786B File Offset: 0x00065A6B
		public virtual string NamespaceName { get; private set; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x00067874 File Offset: 0x00065A74
		public override string FQName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x00067877 File Offset: 0x00065A77
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

		// Token: 0x06002495 RID: 9365 RVA: 0x00067897 File Offset: 0x00065A97
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

		// Token: 0x06002496 RID: 9366 RVA: 0x000678D4 File Offset: 0x00065AD4
		private void HandleNamespaceAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this.NamespaceName);
			if (returnValue.Succeeded)
			{
				this.NamespaceName = returnValue.Value;
			}
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x00067903 File Offset: 0x00065B03
		private void HandleAliasAttribute(XmlReader reader)
		{
			this.Alias = base.HandleUndottedNameAttribute(reader, this.Alias);
		}
	}
}
