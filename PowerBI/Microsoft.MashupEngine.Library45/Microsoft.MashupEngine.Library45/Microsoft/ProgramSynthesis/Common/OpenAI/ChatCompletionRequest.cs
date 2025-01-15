using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006A1 RID: 1697
	public class ChatCompletionRequest : IEquatable<ChatCompletionRequest>
	{
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x000665CD File Offset: 0x000647CD
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ChatCompletionRequest);
			}
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000665DC File Offset: 0x000647DC
		public ChatCompletionRequest(IReadOnlyList<ChatCompletionMessage> messages = null, string model = null, double? temperature = null, double? topP = null, int? n = null, bool? stream = null, IReadOnlyList<string> stop = null, int? maxTokens = null, double? presencePenalty = null, double? frequencePenalty = null)
		{
			this.Messages = messages;
			this.Model = model;
			this.Temperature = temperature;
			this.TopP = topP;
			this.N = n;
			this.Stream = stream;
			this.Stop = stop;
			this.MaxTokens = maxTokens;
			this.PresencePenalty = presencePenalty;
			this.FrequencyPenalty = frequencePenalty;
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x0006663C File Offset: 0x0006483C
		// (set) Token: 0x06002482 RID: 9346 RVA: 0x00066644 File Offset: 0x00064844
		[JsonProperty("messages")]
		public IReadOnlyList<ChatCompletionMessage> Messages { get; set; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x0006664D File Offset: 0x0006484D
		// (set) Token: 0x06002484 RID: 9348 RVA: 0x00066655 File Offset: 0x00064855
		[JsonProperty("model")]
		public string Model { get; set; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x0006665E File Offset: 0x0006485E
		// (set) Token: 0x06002486 RID: 9350 RVA: 0x00066666 File Offset: 0x00064866
		[JsonProperty("temperature")]
		public double? Temperature { get; set; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x0006666F File Offset: 0x0006486F
		// (set) Token: 0x06002488 RID: 9352 RVA: 0x00066677 File Offset: 0x00064877
		[JsonProperty("top_p")]
		public double? TopP { get; set; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x00066680 File Offset: 0x00064880
		// (set) Token: 0x0600248A RID: 9354 RVA: 0x00066688 File Offset: 0x00064888
		[JsonProperty("n")]
		public int? N { get; set; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600248B RID: 9355 RVA: 0x00066691 File Offset: 0x00064891
		// (set) Token: 0x0600248C RID: 9356 RVA: 0x00066699 File Offset: 0x00064899
		[JsonProperty("stream")]
		public bool? Stream { get; set; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x000666A2 File Offset: 0x000648A2
		// (set) Token: 0x0600248E RID: 9358 RVA: 0x000666AA File Offset: 0x000648AA
		[JsonProperty("stop")]
		public IReadOnlyList<string> Stop { get; set; }

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x000666B3 File Offset: 0x000648B3
		// (set) Token: 0x06002490 RID: 9360 RVA: 0x000666BB File Offset: 0x000648BB
		[JsonProperty("max_tokens")]
		public int? MaxTokens { get; set; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x000666C4 File Offset: 0x000648C4
		// (set) Token: 0x06002492 RID: 9362 RVA: 0x000666CC File Offset: 0x000648CC
		[JsonProperty("presence_penalty")]
		public double? PresencePenalty { get; set; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x000666D5 File Offset: 0x000648D5
		// (set) Token: 0x06002494 RID: 9364 RVA: 0x000666DD File Offset: 0x000648DD
		[JsonProperty("frequency_penalty")]
		public double? FrequencyPenalty { get; set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x000666E6 File Offset: 0x000648E6
		// (set) Token: 0x06002496 RID: 9366 RVA: 0x000666EE File Offset: 0x000648EE
		[JsonProperty("logit_bias")]
		public IReadOnlyDictionary<string, int> LogitBias { get; set; }

		// Token: 0x06002497 RID: 9367 RVA: 0x000666F8 File Offset: 0x000648F8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ChatCompletionRequest");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x00066744 File Offset: 0x00064944
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Messages = ");
			builder.Append(this.Messages);
			builder.Append(", Model = ");
			builder.Append(this.Model);
			builder.Append(", Temperature = ");
			builder.Append(this.Temperature.ToString());
			builder.Append(", TopP = ");
			builder.Append(this.TopP.ToString());
			builder.Append(", N = ");
			builder.Append(this.N.ToString());
			builder.Append(", Stream = ");
			builder.Append(this.Stream.ToString());
			builder.Append(", Stop = ");
			builder.Append(this.Stop);
			builder.Append(", MaxTokens = ");
			builder.Append(this.MaxTokens.ToString());
			builder.Append(", PresencePenalty = ");
			builder.Append(this.PresencePenalty.ToString());
			builder.Append(", FrequencyPenalty = ");
			builder.Append(this.FrequencyPenalty.ToString());
			builder.Append(", LogitBias = ");
			builder.Append(this.LogitBias);
			return true;
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000668CC File Offset: 0x00064ACC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ChatCompletionRequest left, ChatCompletionRequest right)
		{
			return !(left == right);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000668D8 File Offset: 0x00064AD8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ChatCompletionRequest left, ChatCompletionRequest right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000668EC File Offset: 0x00064AEC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<ChatCompletionMessage>>.Default.GetHashCode(this.<Messages>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Model>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<Temperature>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<TopP>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<N>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<Stream>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Stop>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<MaxTokens>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<PresencePenalty>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<FrequencyPenalty>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, int>>.Default.GetHashCode(this.<LogitBias>k__BackingField);
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x00066A06 File Offset: 0x00064C06
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ChatCompletionRequest);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x00066A14 File Offset: 0x00064C14
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ChatCompletionRequest other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<ChatCompletionMessage>>.Default.Equals(this.<Messages>k__BackingField, other.<Messages>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Model>k__BackingField, other.<Model>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<Temperature>k__BackingField, other.<Temperature>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<TopP>k__BackingField, other.<TopP>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<N>k__BackingField, other.<N>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<Stream>k__BackingField, other.<Stream>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Stop>k__BackingField, other.<Stop>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<MaxTokens>k__BackingField, other.<MaxTokens>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<PresencePenalty>k__BackingField, other.<PresencePenalty>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<FrequencyPenalty>k__BackingField, other.<FrequencyPenalty>k__BackingField) && EqualityComparer<IReadOnlyDictionary<string, int>>.Default.Equals(this.<LogitBias>k__BackingField, other.<LogitBias>k__BackingField));
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x00066B68 File Offset: 0x00064D68
		[CompilerGenerated]
		protected ChatCompletionRequest([Nullable(1)] ChatCompletionRequest original)
		{
			this.Messages = original.<Messages>k__BackingField;
			this.Model = original.<Model>k__BackingField;
			this.Temperature = original.<Temperature>k__BackingField;
			this.TopP = original.<TopP>k__BackingField;
			this.N = original.<N>k__BackingField;
			this.Stream = original.<Stream>k__BackingField;
			this.Stop = original.<Stop>k__BackingField;
			this.MaxTokens = original.<MaxTokens>k__BackingField;
			this.PresencePenalty = original.<PresencePenalty>k__BackingField;
			this.FrequencyPenalty = original.<FrequencyPenalty>k__BackingField;
			this.LogitBias = original.<LogitBias>k__BackingField;
		}
	}
}
