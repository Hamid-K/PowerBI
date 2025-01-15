using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav
{
	// Token: 0x0200004C RID: 76
	public sealed class TransformCapability
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00002F04 File Offset: 0x00001104
		public TransformCapability(string algorithmName)
		{
			this.AlgorithmName = algorithmName;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00002F13 File Offset: 0x00001113
		public string AlgorithmName { get; }

		// Token: 0x06000146 RID: 326 RVA: 0x00002F1B File Offset: 0x0000111B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TransformCapability);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00002F29 File Offset: 0x00001129
		public bool Equals(TransformCapability other)
		{
			return QueryValueComparers.TransformAlgorithm.Equals(this.AlgorithmName, (other != null) ? other.AlgorithmName : null);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00002F47 File Offset: 0x00001147
		public override int GetHashCode()
		{
			return QueryValueComparers.TransformAlgorithm.GetHashCode(this.AlgorithmName);
		}
	}
}
