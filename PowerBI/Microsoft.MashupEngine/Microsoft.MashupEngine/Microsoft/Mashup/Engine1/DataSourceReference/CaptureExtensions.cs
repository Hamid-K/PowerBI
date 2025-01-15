using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018BA RID: 6330
	internal static class CaptureExtensions
	{
		// Token: 0x0600A140 RID: 41280 RVA: 0x00217394 File Offset: 0x00215594
		public static bool TrySetAddressString(this IDataSourceLocation location, string addressName, Dictionary<string, IExpression> captures, string captureName)
		{
			string text;
			if (captures.TryGetStringConstant(captureName, out text))
			{
				location.Address[addressName] = text;
				return true;
			}
			return false;
		}
	}
}
