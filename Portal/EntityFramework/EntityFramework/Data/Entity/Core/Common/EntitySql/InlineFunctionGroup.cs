using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000659 RID: 1625
	internal sealed class InlineFunctionGroup : MetadataMember
	{
		// Token: 0x06004DF5 RID: 19957 RVA: 0x001186FD File Offset: 0x001168FD
		internal InlineFunctionGroup(string name, IList<InlineFunctionInfo> functionMetadata)
			: base(MetadataMemberClass.InlineFunctionGroup, name)
		{
			this.FunctionMetadata = functionMetadata;
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06004DF6 RID: 19958 RVA: 0x0011870E File Offset: 0x0011690E
		internal override string MetadataMemberClassName
		{
			get
			{
				return InlineFunctionGroup.InlineFunctionGroupClassName;
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06004DF7 RID: 19959 RVA: 0x00118715 File Offset: 0x00116915
		internal static string InlineFunctionGroupClassName
		{
			get
			{
				return Strings.LocalizedInlineFunction;
			}
		}

		// Token: 0x04001C44 RID: 7236
		internal readonly IList<InlineFunctionInfo> FunctionMetadata;
	}
}
