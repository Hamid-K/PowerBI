using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002F0 RID: 752
	internal abstract class FacetDescriptionElement : SchemaElement
	{
		// Token: 0x060023C7 RID: 9159 RVA: 0x000654DC File Offset: 0x000636DC
		public FacetDescriptionElement(TypeElement type, string name)
			: base(type, name, null)
		{
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000654E7 File Offset: 0x000636E7
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

		// Token: 0x060023C9 RID: 9161 RVA: 0x00065508 File Offset: 0x00063708
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

		// Token: 0x060023CA RID: 9162 RVA: 0x0006557C File Offset: 0x0006377C
		protected void HandleMinimumAttribute(XmlReader reader)
		{
			int num = -1;
			if (base.HandleIntAttribute(reader, ref num))
			{
				this._minValue = new int?(num);
			}
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000655A4 File Offset: 0x000637A4
		protected void HandleMaximumAttribute(XmlReader reader)
		{
			int num = -1;
			if (base.HandleIntAttribute(reader, ref num))
			{
				this._maxValue = new int?(num);
			}
		}

		// Token: 0x060023CC RID: 9164
		protected abstract void HandleDefaultAttribute(XmlReader reader);

		// Token: 0x060023CD RID: 9165 RVA: 0x000655CC File Offset: 0x000637CC
		protected void HandleConstantAttribute(XmlReader reader)
		{
			bool flag = false;
			if (base.HandleBoolAttribute(reader, ref flag))
			{
				this._isConstant = flag;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060023CE RID: 9166
		public abstract EdmType FacetType { get; }

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x000655ED File Offset: 0x000637ED
		public int? MinValue
		{
			get
			{
				return this._minValue;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x000655F5 File Offset: 0x000637F5
		public int? MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060023D1 RID: 9169 RVA: 0x000655FD File Offset: 0x000637FD
		// (set) Token: 0x060023D2 RID: 9170 RVA: 0x00065605 File Offset: 0x00063805
		public object DefaultValue { get; set; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x0006560E File Offset: 0x0006380E
		public FacetDescription FacetDescription
		{
			get
			{
				return this._facetDescription;
			}
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00065616 File Offset: 0x00063816
		internal void CreateAndValidateFacetDescription(string declaringTypeName)
		{
			this._facetDescription = new FacetDescription(this.Name, this.FacetType, this.MinValue, this.MaxValue, this.DefaultValue, this._isConstant, declaringTypeName);
		}

		// Token: 0x04000CC8 RID: 3272
		private int? _minValue;

		// Token: 0x04000CC9 RID: 3273
		private int? _maxValue;

		// Token: 0x04000CCA RID: 3274
		private bool _isConstant;

		// Token: 0x04000CCB RID: 3275
		private FacetDescription _facetDescription;
	}
}
