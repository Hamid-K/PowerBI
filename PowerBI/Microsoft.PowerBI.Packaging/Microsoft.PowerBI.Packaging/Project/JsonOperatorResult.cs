using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000067 RID: 103
	public class JsonOperatorResult
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00008428 File Offset: 0x00006628
		internal JsonOperatorResult(JObject normalizedJson, int operandWarningsCount)
		{
			this.NormalizedJObject = normalizedJson;
			this.OperandWarningsCount = operandWarningsCount;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000843E File Offset: 0x0000663E
		public JObject NormalizedJObject { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00008446 File Offset: 0x00006646
		public int OperandWarningsCount { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000844E File Offset: 0x0000664E
		public string NormalizedJson
		{
			get
			{
				if (string.IsNullOrEmpty(this.normalizedJson))
				{
					this.normalizedJson = this.NormalizedJObject.ToString();
				}
				return this.normalizedJson;
			}
		}

		// Token: 0x0400016A RID: 362
		private string normalizedJson;
	}
}
