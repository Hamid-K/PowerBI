using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200065D RID: 1629
	internal sealed class MetadataFunctionGroup : MetadataMember
	{
		// Token: 0x06004DFF RID: 19967 RVA: 0x0011877E File Offset: 0x0011697E
		internal MetadataFunctionGroup(string name, IList<EdmFunction> functionMetadata)
			: base(MetadataMemberClass.FunctionGroup, name)
		{
			this.FunctionMetadata = functionMetadata;
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06004E00 RID: 19968 RVA: 0x0011878F File Offset: 0x0011698F
		internal override string MetadataMemberClassName
		{
			get
			{
				return MetadataFunctionGroup.FunctionGroupClassName;
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06004E01 RID: 19969 RVA: 0x00118796 File Offset: 0x00116996
		internal static string FunctionGroupClassName
		{
			get
			{
				return Strings.LocalizedFunction;
			}
		}

		// Token: 0x04001C49 RID: 7241
		internal readonly IList<EdmFunction> FunctionMetadata;
	}
}
