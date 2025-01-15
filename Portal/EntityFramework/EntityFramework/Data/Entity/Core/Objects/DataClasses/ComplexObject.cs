using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000469 RID: 1129
	[DataContract(IsReference = true)]
	[Serializable]
	public abstract class ComplexObject : StructuralObject
	{
		// Token: 0x06003779 RID: 14201 RVA: 0x000B5E14 File Offset: 0x000B4014
		internal void AttachToParent(StructuralObject parent, string parentPropertyName)
		{
			if (this._parent != null)
			{
				throw new InvalidOperationException(Strings.ComplexObject_ComplexObjectAlreadyAttachedToParent);
			}
			this._parent = parent;
			this._parentPropertyName = parentPropertyName;
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000B5E37 File Offset: 0x000B4037
		internal void DetachFromParent()
		{
			this._parent = null;
			this._parentPropertyName = null;
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000B5E47 File Offset: 0x000B4047
		protected sealed override void ReportPropertyChanging(string property)
		{
			Check.NotEmpty(property, "property");
			base.ReportPropertyChanging(property);
			this.ReportComplexPropertyChanging(null, this, property);
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000B5E65 File Offset: 0x000B4065
		protected sealed override void ReportPropertyChanged(string property)
		{
			Check.NotEmpty(property, "property");
			this.ReportComplexPropertyChanged(null, this, property);
			base.ReportPropertyChanged(property);
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x000B5E83 File Offset: 0x000B4083
		internal sealed override bool IsChangeTracked
		{
			get
			{
				return this._parent != null && this._parent.IsChangeTracked;
			}
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000B5E9A File Offset: 0x000B409A
		internal sealed override void ReportComplexPropertyChanging(string entityMemberName, ComplexObject complexObject, string complexMemberName)
		{
			if (this._parent != null)
			{
				this._parent.ReportComplexPropertyChanging(this._parentPropertyName, complexObject, complexMemberName);
			}
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000B5EB7 File Offset: 0x000B40B7
		internal sealed override void ReportComplexPropertyChanged(string entityMemberName, ComplexObject complexObject, string complexMemberName)
		{
			if (this._parent != null)
			{
				this._parent.ReportComplexPropertyChanged(this._parentPropertyName, complexObject, complexMemberName);
			}
		}

		// Token: 0x040012C6 RID: 4806
		private StructuralObject _parent;

		// Token: 0x040012C7 RID: 4807
		private string _parentPropertyName;
	}
}
