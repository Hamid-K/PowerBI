using System;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000026 RID: 38
	internal abstract class FacetDescriptionElement : SchemaElement
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0000B70C File Offset: 0x0000990C
		public FacetDescriptionElement(TypeElement type, string name)
			: base(type, name)
		{
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0000B716 File Offset: 0x00009916
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

		// Token: 0x06000664 RID: 1636 RVA: 0x0000B738 File Offset: 0x00009938
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Minimum"))
			{
				this.HandleMinimumAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Maximum"))
			{
				this.HandleMaximumAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "DefaultValue"))
			{
				this.HandleDefaultAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Constant"))
			{
				this.HandleConstantAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000B7AC File Offset: 0x000099AC
		protected void HandleMinimumAttribute(XmlReader reader)
		{
			int num = -1;
			if (base.HandleIntAttribute(reader, ref num))
			{
				this._minValue = new int?(num);
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000B7D4 File Offset: 0x000099D4
		protected void HandleMaximumAttribute(XmlReader reader)
		{
			int num = -1;
			if (base.HandleIntAttribute(reader, ref num))
			{
				this._maxValue = new int?(num);
			}
		}

		// Token: 0x06000667 RID: 1639
		protected abstract void HandleDefaultAttribute(XmlReader reader);

		// Token: 0x06000668 RID: 1640 RVA: 0x0000B7FC File Offset: 0x000099FC
		protected void HandleConstantAttribute(XmlReader reader)
		{
			bool flag = false;
			if (base.HandleBoolAttribute(reader, ref flag))
			{
				this._isConstant = flag;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000669 RID: 1641
		public abstract EdmType FacetType { get; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0000B81D File Offset: 0x00009A1D
		public int? MinValue
		{
			get
			{
				return this._minValue;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0000B825 File Offset: 0x00009A25
		public int? MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0000B82D File Offset: 0x00009A2D
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x0000B835 File Offset: 0x00009A35
		public object DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
			set
			{
				this._defaultValue = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0000B83E File Offset: 0x00009A3E
		public FacetDescription FacetDescription
		{
			get
			{
				return this._facetDescription;
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0000B846 File Offset: 0x00009A46
		internal void CreateAndValidateFacetDescription(string declaringTypeName)
		{
			this._facetDescription = new FacetDescription(this.Name, this.FacetType, this.MinValue, this.MaxValue, this.DefaultValue, this._isConstant, declaringTypeName);
		}

		// Token: 0x04000646 RID: 1606
		private int? _minValue;

		// Token: 0x04000647 RID: 1607
		private int? _maxValue;

		// Token: 0x04000648 RID: 1608
		private object _defaultValue;

		// Token: 0x04000649 RID: 1609
		private bool _isConstant;

		// Token: 0x0400064A RID: 1610
		private FacetDescription _facetDescription;
	}
}
