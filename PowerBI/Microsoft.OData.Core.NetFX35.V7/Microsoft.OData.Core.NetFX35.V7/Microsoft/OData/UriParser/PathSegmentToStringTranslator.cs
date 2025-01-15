using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000191 RID: 401
	internal sealed class PathSegmentToStringTranslator : PathSegmentTranslator<string>
	{
		// Token: 0x06001042 RID: 4162 RVA: 0x0002D9E1 File Offset: 0x0002BBE1
		private PathSegmentToStringTranslator()
		{
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0002D9E9 File Offset: 0x0002BBE9
		public override string Translate(TypeSegment segment)
		{
			return segment.EdmType.FullTypeName();
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0002D9F6 File Offset: 0x0002BBF6
		public override string Translate(NavigationPropertySegment segment)
		{
			return segment.NavigationProperty.Name;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0002DA03 File Offset: 0x0002BC03
		public override string Translate(PropertySegment segment)
		{
			return segment.Property.Name;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0002DA10 File Offset: 0x0002BC10
		public override string Translate(OperationSegment segment)
		{
			return segment.Operations.OperationGroupFullName();
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0002DA1D File Offset: 0x0002BC1D
		public override string Translate(OperationImportSegment segment)
		{
			return segment.OperationImports.OperationImportGroupFullName();
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0002DA2A File Offset: 0x0002BC2A
		public override string Translate(DynamicPathSegment segment)
		{
			return segment.Identifier;
		}

		// Token: 0x040008A7 RID: 2215
		internal static readonly PathSegmentToStringTranslator Instance = new PathSegmentToStringTranslator();
	}
}
