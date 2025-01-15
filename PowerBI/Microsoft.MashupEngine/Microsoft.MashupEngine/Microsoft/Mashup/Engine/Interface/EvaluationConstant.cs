using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200001B RID: 27
	public struct EvaluationConstant
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002CEB File Offset: 0x00000EEB
		public EvaluationConstant(string name, string value, bool isPii)
		{
			this = default(EvaluationConstant);
			this.Name = name;
			this.Value = value;
			this.IsPii = isPii;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002D09 File Offset: 0x00000F09
		public string Name { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002D11 File Offset: 0x00000F11
		public string Value { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002D19 File Offset: 0x00000F19
		public bool IsPii { get; }
	}
}
