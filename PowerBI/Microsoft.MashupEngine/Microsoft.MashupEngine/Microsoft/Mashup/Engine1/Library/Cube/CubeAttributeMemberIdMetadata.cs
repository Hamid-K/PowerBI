using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CC9 RID: 3273
	internal static class CubeAttributeMemberIdMetadata
	{
		// Token: 0x06005883 RID: 22659 RVA: 0x0013551A File Offset: 0x0013371A
		public static Value AddMemberIdentifier(Value value, string identifier)
		{
			if (identifier != null)
			{
				value = value.NewMeta(RecordValue.New(CubeAttributeMemberIdMetadata.MemberMetadataKeys, new Value[] { TextValue.NewOrNull(identifier) }));
			}
			return value;
		}

		// Token: 0x06005884 RID: 22660 RVA: 0x00135541 File Offset: 0x00133741
		public static TypeValue AddColumnMetadata(TypeValue columnType)
		{
			return columnType.NewMeta(CubeAttributeMemberIdMetadata.levelColumnMeta).AsType;
		}

		// Token: 0x040031DD RID: 12765
		private static readonly RecordValue levelColumnMeta = RecordValue.New(Keys.New("Cube.HasAttributeMemberIds"), new Value[] { LogicalValue.True });

		// Token: 0x040031DE RID: 12766
		private static readonly Keys MemberMetadataKeys = Keys.New("Cube.AttributeMemberId");
	}
}
