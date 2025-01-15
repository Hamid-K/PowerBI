using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200054E RID: 1358
	public sealed class ModificationFunctionMemberPath : MappingItem
	{
		// Token: 0x060042B0 RID: 17072 RVA: 0x000E584C File Offset: 0x000E3A4C
		public ModificationFunctionMemberPath(IEnumerable<EdmMember> members, AssociationSet associationSet)
		{
			Check.NotNull<IEnumerable<EdmMember>>(members, "members");
			this._members = new ReadOnlyCollection<EdmMember>(new List<EdmMember>(members));
			if (associationSet != null)
			{
				this._associationSetEnd = associationSet.AssociationSetEnds[this.Members[1].Name];
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x060042B1 RID: 17073 RVA: 0x000E58A1 File Offset: 0x000E3AA1
		public ReadOnlyCollection<EdmMember> Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x060042B2 RID: 17074 RVA: 0x000E58A9 File Offset: 0x000E3AA9
		public AssociationSetEnd AssociationSetEnd
		{
			get
			{
				return this._associationSetEnd;
			}
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x000E58B4 File Offset: 0x000E3AB4
		public override string ToString()
		{
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			string text = "{0}{1}";
			object[] array = new object[2];
			int num = 0;
			object obj;
			if (this.AssociationSetEnd != null)
			{
				string text2 = "[";
				AssociationSet parentAssociationSet = this.AssociationSetEnd.ParentAssociationSet;
				obj = text2 + ((parentAssociationSet != null) ? parentAssociationSet.ToString() : null) + "]";
			}
			else
			{
				obj = string.Empty;
			}
			array[num] = obj;
			array[1] = StringUtil.BuildDelimitedList<EdmMember>(this.Members, null, ".");
			return string.Format(invariantCulture, text, array);
		}

		// Token: 0x0400177C RID: 6012
		private readonly ReadOnlyCollection<EdmMember> _members;

		// Token: 0x0400177D RID: 6013
		private readonly AssociationSetEnd _associationSetEnd;
	}
}
