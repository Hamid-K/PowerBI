using System;
using System.Linq;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006A6 RID: 1702
	public class CompletionRequest
	{
		// Token: 0x060024C6 RID: 9414 RVA: 0x00066D20 File Offset: 0x00064F20
		public CompletionRequest(string[] prompts = null, string model = null, int? max_tokens = null, double? temperature = null, double? top_p = null, int? numOutputs = null, double? presencePenalty = null, double? frequencyPenalty = null, int? logProbs = null, string[] stopSequences = null)
		{
			this.MultiplePrompts = prompts;
			this.Model = model;
			this.MaxTokens = max_tokens;
			this.Temperature = temperature;
			this.TopP = top_p;
			this.NumChoicesPerPrompt = numOutputs;
			this.PresencePenalty = presencePenalty;
			this.FrequencyPenalty = frequencyPenalty;
			this.Logprobs = logProbs;
			this.MultipleStopSequences = stopSequences;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x00066D80 File Offset: 0x00064F80
		public CompletionRequest(string prompt, string model = null, int? max_tokens = null, double? temperature = null, double? top_p = null, int? numOutputs = null, double? presencePenalty = null, double? frequencyPenalty = null, int? logProbs = null, string[] stopSequences = null)
			: this(new string[] { prompt }, model, max_tokens, temperature, top_p, numOutputs, presencePenalty, frequencyPenalty, logProbs, stopSequences)
		{
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x00066DB0 File Offset: 0x00064FB0
		public CompletionRequest(CompletionRequest basedOn)
			: this(basedOn.MultiplePrompts, basedOn.Model, basedOn.MaxTokens, basedOn.Temperature, basedOn.TopP, basedOn.NumChoicesPerPrompt, basedOn.PresencePenalty, basedOn.FrequencyPenalty, basedOn.Logprobs, basedOn.MultipleStopSequences)
		{
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060024C9 RID: 9417 RVA: 0x00066DFF File Offset: 0x00064FFF
		[JsonProperty("prompt")]
		public object CompiledPrompt
		{
			get
			{
				string[] multiplePrompts = this.MultiplePrompts;
				if (multiplePrompts != null && multiplePrompts.Length == 1)
				{
					return this.Prompt;
				}
				return this.MultiplePrompts;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x00066E22 File Offset: 0x00065022
		// (set) Token: 0x060024CB RID: 9419 RVA: 0x00066E2A File Offset: 0x0006502A
		[JsonIgnore]
		public string[] MultiplePrompts { get; set; }

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x00066E33 File Offset: 0x00065033
		// (set) Token: 0x060024CD RID: 9421 RVA: 0x00066E46 File Offset: 0x00065046
		[JsonIgnore]
		public string Prompt
		{
			get
			{
				string[] multiplePrompts = this.MultiplePrompts;
				if (multiplePrompts == null)
				{
					return null;
				}
				return multiplePrompts.FirstOrDefault<string>();
			}
			set
			{
				this.MultiplePrompts = new string[] { value };
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x00066E58 File Offset: 0x00065058
		// (set) Token: 0x060024CF RID: 9423 RVA: 0x00066E60 File Offset: 0x00065060
		[JsonProperty("model")]
		public string Model { get; set; }

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x00066E69 File Offset: 0x00065069
		// (set) Token: 0x060024D1 RID: 9425 RVA: 0x00066E71 File Offset: 0x00065071
		[JsonProperty("max_tokens")]
		public int? MaxTokens { get; set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x00066E7A File Offset: 0x0006507A
		// (set) Token: 0x060024D3 RID: 9427 RVA: 0x00066E82 File Offset: 0x00065082
		[JsonProperty("temperature")]
		public double? Temperature { get; set; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x00066E8B File Offset: 0x0006508B
		// (set) Token: 0x060024D5 RID: 9429 RVA: 0x00066E93 File Offset: 0x00065093
		[JsonProperty("top_p")]
		public double? TopP { get; set; }

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x00066E9C File Offset: 0x0006509C
		// (set) Token: 0x060024D7 RID: 9431 RVA: 0x00066EA4 File Offset: 0x000650A4
		[JsonProperty("presence_penalty")]
		public double? PresencePenalty { get; set; }

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x00066EAD File Offset: 0x000650AD
		// (set) Token: 0x060024D9 RID: 9433 RVA: 0x00066EB5 File Offset: 0x000650B5
		[JsonProperty("frequency_penalty")]
		public double? FrequencyPenalty { get; set; }

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x00066EBE File Offset: 0x000650BE
		// (set) Token: 0x060024DB RID: 9435 RVA: 0x00066EC6 File Offset: 0x000650C6
		[JsonProperty("n")]
		public int? NumChoicesPerPrompt { get; set; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x00066ECF File Offset: 0x000650CF
		// (set) Token: 0x060024DD RID: 9437 RVA: 0x00066ED7 File Offset: 0x000650D7
		[JsonProperty("stream")]
		public bool Stream { get; set; }

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x00066EE0 File Offset: 0x000650E0
		// (set) Token: 0x060024DF RID: 9439 RVA: 0x00066EE8 File Offset: 0x000650E8
		[JsonProperty("logprobs")]
		public int? Logprobs { get; set; }

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x00066EF1 File Offset: 0x000650F1
		[JsonProperty("stop")]
		public object CompiledStop
		{
			get
			{
				string[] multipleStopSequences = this.MultipleStopSequences;
				if (multipleStopSequences != null && multipleStopSequences.Length == 1)
				{
					return this.StopSequence;
				}
				string[] multipleStopSequences2 = this.MultipleStopSequences;
				if (multipleStopSequences2 != null && multipleStopSequences2.Length != 0)
				{
					return this.MultipleStopSequences;
				}
				return null;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x00066F29 File Offset: 0x00065129
		// (set) Token: 0x060024E2 RID: 9442 RVA: 0x00066F31 File Offset: 0x00065131
		[JsonIgnore]
		public string[] MultipleStopSequences { get; set; }

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060024E3 RID: 9443 RVA: 0x00066F3A File Offset: 0x0006513A
		// (set) Token: 0x060024E4 RID: 9444 RVA: 0x00066F53 File Offset: 0x00065153
		[JsonIgnore]
		public string StopSequence
		{
			get
			{
				string[] multipleStopSequences = this.MultipleStopSequences;
				return ((multipleStopSequences != null) ? multipleStopSequences.FirstOrDefault<string>() : null) ?? null;
			}
			set
			{
				if (value != null)
				{
					this.MultipleStopSequences = new string[] { value };
				}
			}
		}
	}
}
