using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200060F RID: 1551
	internal class DomainVariable<T_Variable, T_Element>
	{
		// Token: 0x06004B74 RID: 19316 RVA: 0x0010A834 File Offset: 0x00108A34
		internal DomainVariable(T_Variable identifier, Set<T_Element> domain, IEqualityComparer<T_Variable> identifierComparer)
		{
			this._identifier = identifier;
			this._domain = domain.AsReadOnly();
			this._identifierComparer = identifierComparer ?? EqualityComparer<T_Variable>.Default;
			int elementsHashCode = this._domain.GetElementsHashCode();
			int hashCode = this._identifierComparer.GetHashCode(this._identifier);
			this._hashCode = elementsHashCode ^ hashCode;
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x0010A891 File Offset: 0x00108A91
		internal DomainVariable(T_Variable identifier, Set<T_Element> domain)
			: this(identifier, domain, null)
		{
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06004B76 RID: 19318 RVA: 0x0010A89C File Offset: 0x00108A9C
		internal T_Variable Identifier
		{
			get
			{
				return this._identifier;
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06004B77 RID: 19319 RVA: 0x0010A8A4 File Offset: 0x00108AA4
		internal Set<T_Element> Domain
		{
			get
			{
				return this._domain;
			}
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x0010A8AC File Offset: 0x00108AAC
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x0010A8B4 File Offset: 0x00108AB4
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			DomainVariable<T_Variable, T_Element> domainVariable = obj as DomainVariable<T_Variable, T_Element>;
			return domainVariable != null && this._hashCode == domainVariable._hashCode && this._identifierComparer.Equals(this._identifier, domainVariable._identifier) && this._domain.SetEquals(domainVariable._domain);
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x0010A910 File Offset: 0x00108B10
		public override string ToString()
		{
			string text = "{0}{{{1}}}";
			object[] array = new object[2];
			int num = 0;
			T_Variable identifier = this._identifier;
			array[num] = identifier.ToString();
			array[1] = this._domain;
			return StringUtil.FormatInvariant(text, array);
		}

		// Token: 0x04001A5E RID: 6750
		private readonly T_Variable _identifier;

		// Token: 0x04001A5F RID: 6751
		private readonly Set<T_Element> _domain;

		// Token: 0x04001A60 RID: 6752
		private readonly int _hashCode;

		// Token: 0x04001A61 RID: 6753
		private readonly IEqualityComparer<T_Variable> _identifierComparer;
	}
}
