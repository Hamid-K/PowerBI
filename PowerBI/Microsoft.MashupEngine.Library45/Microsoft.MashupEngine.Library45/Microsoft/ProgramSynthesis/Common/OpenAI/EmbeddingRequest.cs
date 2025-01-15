using System;
using System.Linq;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006AA RID: 1706
	public class EmbeddingRequest
	{
		// Token: 0x06002503 RID: 9475 RVA: 0x00067098 File Offset: 0x00065298
		public EmbeddingRequest(string[] inputs = null, string user = null, string model = null)
		{
			this.MultipleInputs = inputs;
			this.User = user;
			this.Model = model;
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000670B5 File Offset: 0x000652B5
		public EmbeddingRequest(string input, string user = null, string model = null)
			: this(null, user, model)
		{
			this.Input = input;
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000670C7 File Offset: 0x000652C7
		public EmbeddingRequest(EmbeddingRequest basedOn)
			: this(basedOn.MultipleInputs, basedOn.User, basedOn.Model)
		{
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06002506 RID: 9478 RVA: 0x000670E1 File Offset: 0x000652E1
		[JsonProperty("input")]
		public object CompiledInput
		{
			get
			{
				string[] multipleInputs = this.MultipleInputs;
				if (multipleInputs != null && multipleInputs.Length == 1)
				{
					return this.Input;
				}
				return this.MultipleInputs;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x00067104 File Offset: 0x00065304
		// (set) Token: 0x06002508 RID: 9480 RVA: 0x0006710C File Offset: 0x0006530C
		[JsonIgnore]
		public string[] MultipleInputs { get; set; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x00067115 File Offset: 0x00065315
		// (set) Token: 0x0600250A RID: 9482 RVA: 0x00067122 File Offset: 0x00065322
		[JsonIgnore]
		public string Input
		{
			get
			{
				return this.MultipleInputs.FirstOrDefault<string>();
			}
			set
			{
				this.MultipleInputs = new string[] { value };
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x00067134 File Offset: 0x00065334
		// (set) Token: 0x0600250C RID: 9484 RVA: 0x0006713C File Offset: 0x0006533C
		[JsonProperty("user")]
		public string User { get; set; }

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600250D RID: 9485 RVA: 0x00067145 File Offset: 0x00065345
		// (set) Token: 0x0600250E RID: 9486 RVA: 0x0006714D File Offset: 0x0006534D
		[JsonProperty("model")]
		public string Model { get; set; }
	}
}
