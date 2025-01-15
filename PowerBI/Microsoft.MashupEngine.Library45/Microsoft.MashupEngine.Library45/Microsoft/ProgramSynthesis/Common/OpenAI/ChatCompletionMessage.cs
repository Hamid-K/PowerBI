using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006A0 RID: 1696
	public class ChatCompletionMessage : IEquatable<ChatCompletionMessage>
	{
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x00066424 File Offset: 0x00064624
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ChatCompletionMessage);
			}
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x00066430 File Offset: 0x00064630
		public ChatCompletionMessage(string role, string content)
		{
			this.Role = role;
			this.Content = content;
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x00066446 File Offset: 0x00064646
		[JsonProperty("role")]
		public string Role { get; }

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x0006644E File Offset: 0x0006464E
		[JsonProperty("content")]
		public string Content { get; }

		// Token: 0x06002476 RID: 9334 RVA: 0x00066458 File Offset: 0x00064658
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ChatCompletionMessage");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000664A4 File Offset: 0x000646A4
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Role = ");
			builder.Append(this.Role);
			builder.Append(", Content = ");
			builder.Append(this.Content);
			return true;
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000664DE File Offset: 0x000646DE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ChatCompletionMessage left, ChatCompletionMessage right)
		{
			return !(left == right);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000664EA File Offset: 0x000646EA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ChatCompletionMessage left, ChatCompletionMessage right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000664FE File Offset: 0x000646FE
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Role>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Content>k__BackingField);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x0006653E File Offset: 0x0006473E
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ChatCompletionMessage);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x0006654C File Offset: 0x0006474C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ChatCompletionMessage other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Role>k__BackingField, other.<Role>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Content>k__BackingField, other.<Content>k__BackingField));
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000665AD File Offset: 0x000647AD
		[CompilerGenerated]
		protected ChatCompletionMessage([Nullable(1)] ChatCompletionMessage original)
		{
			this.Role = original.<Role>k__BackingField;
			this.Content = original.<Content>k__BackingField;
		}
	}
}
