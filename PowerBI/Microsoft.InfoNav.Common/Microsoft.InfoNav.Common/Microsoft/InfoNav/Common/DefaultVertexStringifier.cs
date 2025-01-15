using System;
using System.Globalization;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200004A RID: 74
	public sealed class DefaultVertexStringifier<TVertex> : IVertexStringifier<TVertex>
	{
		// Token: 0x0600033E RID: 830 RVA: 0x00009443 File Offset: 0x00007643
		public string VertexToString(TVertex vertex)
		{
			return Convert.ToString(vertex, CultureInfo.InvariantCulture);
		}
	}
}
