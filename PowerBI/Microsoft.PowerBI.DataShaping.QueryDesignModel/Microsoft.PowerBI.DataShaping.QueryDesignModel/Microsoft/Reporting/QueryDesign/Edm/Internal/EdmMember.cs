using System;
using System.Xml.Linq;
using Microsoft.Data.Metadata.Edm;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200020C RID: 524
	public abstract class EdmMember : EdmItem, IEquatable<EdmMember>, IEntityMemberItem
	{
		// Token: 0x06001883 RID: 6275 RVA: 0x00043300 File Offset: 0x00041500
		protected EdmMember(StructuralType declaringType, XElement extensionElem)
		{
			this._declaringType = ArgumentValidation.CheckNotNull<StructuralType>(declaringType, "declaringType");
			this._caption = extensionElem.GetStringAttributeOrDefault(Extensions.CaptionAttr, null);
			this._hidden = extensionElem.GetBooleanAttributeOrDefault(Extensions.HiddenAttr, false);
			this._isPrivate = extensionElem.GetBooleanAttributeOrDefault(Extensions.PrivateAttr, false);
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0004335A File Offset: 0x0004155A
		internal StructuralType DeclaringType
		{
			get
			{
				return this._declaringType;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x00043362 File Offset: 0x00041562
		public string Name
		{
			get
			{
				return this.InternalEdmMember.Name;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x0004336F File Offset: 0x0004156F
		public virtual string Caption
		{
			get
			{
				return this._caption ?? this.Name;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x00043381 File Offset: 0x00041581
		public virtual bool Hidden
		{
			get
			{
				return this._hidden;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x00043389 File Offset: 0x00041589
		public virtual bool IsPrivate
		{
			get
			{
				return this._isPrivate;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001889 RID: 6281
		internal abstract EdmMember InternalEdmMember { get; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00043391 File Offset: 0x00041591
		internal sealed override MetadataItem InternalEdmItem
		{
			get
			{
				return this.InternalEdmMember;
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00043399 File Offset: 0x00041599
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EdmMember);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x000433A7 File Offset: 0x000415A7
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x000433B4 File Offset: 0x000415B4
		public bool Equals(EdmMember other)
		{
			return this == other || (other != null && (base.GetType() == other.GetType() && string.Equals(this.Name, other.Name, EdmItem.IdentityComparison)) && this.DeclaringType.Equals(other.DeclaringType));
		}

		// Token: 0x04000D0F RID: 3343
		private readonly StructuralType _declaringType;

		// Token: 0x04000D10 RID: 3344
		private readonly string _caption;

		// Token: 0x04000D11 RID: 3345
		private readonly bool _hidden;

		// Token: 0x04000D12 RID: 3346
		private readonly bool _isPrivate;
	}
}
