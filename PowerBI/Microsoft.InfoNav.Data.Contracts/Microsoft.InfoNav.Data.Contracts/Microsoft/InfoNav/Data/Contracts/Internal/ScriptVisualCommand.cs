using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001D6 RID: 470
	[DataContract(Name = "ScriptVisualCommand", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ScriptVisualCommand : IEquatable<ScriptVisualCommand>
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00018959 File Offset: 0x00016B59
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x00018961 File Offset: 0x00016B61
		[DataMember(IsRequired = true, Order = 10)]
		public string RenderingEngine { get; set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0001896A File Offset: 0x00016B6A
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x00018972 File Offset: 0x00016B72
		[DataMember(IsRequired = true, Order = 20)]
		public string Script { get; set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0001897B File Offset: 0x00016B7B
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x00018983 File Offset: 0x00016B83
		[DataMember(IsRequired = true, Order = 30)]
		public double ViewportWidthPx { get; set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0001898C File Offset: 0x00016B8C
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x00018994 File Offset: 0x00016B94
		[DataMember(IsRequired = true, Order = 40)]
		public double ViewportHeightPx { get; set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0001899D File Offset: 0x00016B9D
		// (set) Token: 0x06000C92 RID: 3218 RVA: 0x000189A5 File Offset: 0x00016BA5
		[DataMember(IsRequired = true, Order = 50)]
		public ScriptInput ScriptInput { get; set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x000189AE File Offset: 0x00016BAE
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x000189B6 File Offset: 0x00016BB6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public string ScriptOutputType { get; set; }

		// Token: 0x06000C95 RID: 3221 RVA: 0x000189C0 File Offset: 0x00016BC0
		public static bool TryValidate(ScriptVisualCommand scriptVisualCommand, out IEnumerable<ValidationResult> validationResults)
		{
			validationResults = null;
			if (scriptVisualCommand == null)
			{
				validationResults = ScriptVisualCommand.CreateValidationResult("There is no ScriptVisualCommand");
			}
			else if (string.IsNullOrWhiteSpace(scriptVisualCommand.RenderingEngine))
			{
				validationResults = ScriptVisualCommand.CreateValidationResult("There is no RenderingEngine");
			}
			else if (string.IsNullOrWhiteSpace(scriptVisualCommand.Script))
			{
				validationResults = ScriptVisualCommand.CreateValidationResult("There is no Script");
			}
			else if (scriptVisualCommand.ViewportHeightPx <= 0.0)
			{
				validationResults = ScriptVisualCommand.CreateValidationResult("Invalid ViewportHeightPx");
			}
			else if (scriptVisualCommand.ViewportWidthPx <= 0.0)
			{
				validationResults = ScriptVisualCommand.CreateValidationResult("Invalid ViewportWidthPx");
			}
			return validationResults == null;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00018A5E File Offset: 0x00016C5E
		private static IEnumerable<ValidationResult> CreateValidationResult(string errorMessage)
		{
			return new ValidationResult[]
			{
				new ValidationResult(errorMessage)
			};
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00018A70 File Offset: 0x00016C70
		public bool Equals(ScriptVisualCommand other)
		{
			bool? flag = Util.AreEqual<ScriptVisualCommand>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.ViewportHeightPx == other.ViewportHeightPx && this.ViewportWidthPx == other.ViewportWidthPx && this.ScriptInput == other.ScriptInput && string.Equals(this.Script, other.Script, StringComparison.Ordinal) && string.Equals(this.RenderingEngine, other.RenderingEngine, StringComparison.Ordinal) && string.Equals(this.ScriptOutputType, other.ScriptOutputType, StringComparison.Ordinal);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00018B01 File Offset: 0x00016D01
		public override bool Equals(object other)
		{
			return this.Equals(other as ScriptVisualCommand);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00018B10 File Offset: 0x00016D10
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.ViewportHeightPx.GetHashCode(), this.ViewportWidthPx.GetHashCode(), (this.ScriptInput != null) ? this.ScriptInput.GetHashCode() : 0, (this.Script != null) ? this.Script.GetHashCode() : 0, (this.RenderingEngine != null) ? this.RenderingEngine.GetHashCode() : 0, (this.ScriptOutputType != null) ? this.ScriptOutputType.GetHashCode() : 0);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00018B9C File Offset: 0x00016D9C
		public static bool operator ==(ScriptVisualCommand left, ScriptVisualCommand right)
		{
			bool? flag = Util.AreEqual<ScriptVisualCommand>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00018BC9 File Offset: 0x00016DC9
		public static bool operator !=(ScriptVisualCommand left, ScriptVisualCommand right)
		{
			return !(left == right);
		}
	}
}
