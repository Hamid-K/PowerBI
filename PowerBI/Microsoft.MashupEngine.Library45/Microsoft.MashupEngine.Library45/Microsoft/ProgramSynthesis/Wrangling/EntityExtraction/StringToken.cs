using System;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction
{
	// Token: 0x020001A3 RID: 419
	public abstract class StringToken : IToken<char, string>, IEquatable<StringToken>
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x0001B3E3 File Offset: 0x000195E3
		protected StringToken(string source, int start, int end)
		{
			this.SourceSequence = source;
			this.StartInSequence = start;
			this.EndInSequence = end;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001B400 File Offset: 0x00019600
		public bool Equals(StringToken other)
		{
			return other != null && (other == this || (this.SourceSequence == other.SourceSequence && this.StartInSequence == other.StartInSequence && this.EndInSequence == other.EndInSequence));
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001B439 File Offset: 0x00019639
		public string SourceSequence { get; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0001B441 File Offset: 0x00019641
		public int StartInSequence { get; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0001B449 File Offset: 0x00019649
		public int EndInSequence { get; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001B454 File Offset: 0x00019654
		public string TokenSubSequence
		{
			get
			{
				string text;
				if ((text = this._value) == null)
				{
					text = (this._value = this.SourceSequence.Substring(this.Start, this.End - this.Start));
				}
				return text;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0001B492 File Offset: 0x00019692
		public string SourceString
		{
			get
			{
				return this.SourceSequence;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0001B49A File Offset: 0x0001969A
		public int Start
		{
			get
			{
				return this.StartInSequence;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0001B4A2 File Offset: 0x000196A2
		public int End
		{
			get
			{
				return this.EndInSequence;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0001B4AA File Offset: 0x000196AA
		public int Length
		{
			get
			{
				return this.End - this.Start;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0001B4B9 File Offset: 0x000196B9
		public string Value
		{
			get
			{
				return this.TokenSubSequence;
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001B4C1 File Offset: 0x000196C1
		public override bool Equals(object other)
		{
			return other != null && (other == this || (!(base.GetType() != other.GetType()) && this.Equals(other as StringToken)));
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001B4F0 File Offset: 0x000196F0
		public override int GetHashCode()
		{
			return (((this.SourceSequence.GetHashCode() * 39451) ^ this.StartInSequence.GetHashCode()) * 33797) ^ this.EndInSequence.GetHashCode();
		}

		// Token: 0x04000466 RID: 1126
		private string _value;
	}
}
