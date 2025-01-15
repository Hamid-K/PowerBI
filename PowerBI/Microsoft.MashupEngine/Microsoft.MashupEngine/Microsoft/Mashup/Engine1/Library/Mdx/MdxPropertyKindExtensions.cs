using System;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009BA RID: 2490
	internal static class MdxPropertyKindExtensions
	{
		// Token: 0x0600470D RID: 18189 RVA: 0x000EE0E6 File Offset: 0x000EC2E6
		public static CubePropertyKind ToCubePropertyKind(this MdxPropertyKind propertyKind)
		{
			switch (propertyKind)
			{
			case MdxPropertyKind.MemberUniqueName:
				return CubePropertyKind.UniqueId;
			case MdxPropertyKind.MemberCaption:
				return CubePropertyKind.Caption;
			case MdxPropertyKind.UserDefined:
				return CubePropertyKind.UserDefined;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x000EE108 File Offset: 0x000EC308
		public static TextValue ToTextValue(this MdxPropertyKind propertyKind)
		{
			return TextValue.New(propertyKind.ToCubePropertyKind().ToString());
		}
	}
}
