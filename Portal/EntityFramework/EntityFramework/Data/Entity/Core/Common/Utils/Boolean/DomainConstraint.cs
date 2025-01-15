using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200060D RID: 1549
	internal class DomainConstraint<T_Variable, T_Element>
	{
		// Token: 0x06004B68 RID: 19304 RVA: 0x0010A5C0 File Offset: 0x001087C0
		internal DomainConstraint(DomainVariable<T_Variable, T_Element> variable, Set<T_Element> range)
		{
			this._variable = variable;
			this._range = range.AsReadOnly();
			this._hashCode = this._variable.GetHashCode() ^ this._range.GetElementsHashCode();
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x0010A5F8 File Offset: 0x001087F8
		internal DomainConstraint(DomainVariable<T_Variable, T_Element> variable, T_Element element)
			: this(variable, new Set<T_Element>(new T_Element[] { element }).MakeReadOnly())
		{
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06004B6A RID: 19306 RVA: 0x0010A619 File Offset: 0x00108819
		internal DomainVariable<T_Variable, T_Element> Variable
		{
			get
			{
				return this._variable;
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06004B6B RID: 19307 RVA: 0x0010A621 File Offset: 0x00108821
		internal Set<T_Element> Range
		{
			get
			{
				return this._range;
			}
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x0010A629 File Offset: 0x00108829
		internal DomainConstraint<T_Variable, T_Element> InvertDomainConstraint()
		{
			return new DomainConstraint<T_Variable, T_Element>(this._variable, this._variable.Domain.Difference(this._range).AsReadOnly());
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x0010A654 File Offset: 0x00108854
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			DomainConstraint<T_Variable, T_Element> domainConstraint = obj as DomainConstraint<T_Variable, T_Element>;
			return domainConstraint != null && this._hashCode == domainConstraint._hashCode && this._range.SetEquals(domainConstraint._range) && this._variable.Equals(domainConstraint._variable);
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x0010A6A9 File Offset: 0x001088A9
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x0010A6B1 File Offset: 0x001088B1
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0} in [{1}]", new object[] { this._variable, this._range });
		}

		// Token: 0x04001A59 RID: 6745
		private readonly DomainVariable<T_Variable, T_Element> _variable;

		// Token: 0x04001A5A RID: 6746
		private readonly Set<T_Element> _range;

		// Token: 0x04001A5B RID: 6747
		private readonly int _hashCode;
	}
}
