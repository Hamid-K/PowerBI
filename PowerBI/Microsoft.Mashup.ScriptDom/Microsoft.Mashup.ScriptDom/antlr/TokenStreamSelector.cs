using System;
using System.Collections;

namespace antlr
{
	// Token: 0x02000031 RID: 49
	internal class TokenStreamSelector : TokenStream
	{
		// Token: 0x06000194 RID: 404 RVA: 0x000059A0 File Offset: 0x00003BA0
		public TokenStreamSelector()
		{
			this.inputStreamNames = new Hashtable();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000059BE File Offset: 0x00003BBE
		public virtual void addInputStream(TokenStream stream, string key)
		{
			this.inputStreamNames[key] = stream;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000059CD File Offset: 0x00003BCD
		public virtual TokenStream getCurrentStream()
		{
			return this.input;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000059D8 File Offset: 0x00003BD8
		public virtual TokenStream getStream(string sname)
		{
			TokenStream tokenStream = (TokenStream)this.inputStreamNames[sname];
			if (tokenStream == null)
			{
				throw new ArgumentException("TokenStream " + sname + " not found");
			}
			return tokenStream;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005A14 File Offset: 0x00003C14
		public virtual IToken nextToken()
		{
			IToken token;
			try
			{
				IL_0000:
				token = this.input.nextToken();
			}
			catch (TokenStreamRetryException)
			{
				goto IL_0000;
			}
			return token;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005A44 File Offset: 0x00003C44
		public virtual TokenStream pop()
		{
			TokenStream tokenStream = (TokenStream)this.streamStack.Pop();
			this.select(tokenStream);
			return tokenStream;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005A6A File Offset: 0x00003C6A
		public virtual void push(TokenStream stream)
		{
			this.streamStack.Push(this.input);
			this.select(stream);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005A84 File Offset: 0x00003C84
		public virtual void push(string sname)
		{
			this.streamStack.Push(this.input);
			this.select(sname);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005A9E File Offset: 0x00003C9E
		public virtual void retry()
		{
			throw new TokenStreamRetryException();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005AA5 File Offset: 0x00003CA5
		public virtual void select(TokenStream stream)
		{
			this.input = stream;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005AAE File Offset: 0x00003CAE
		public virtual void select(string sname)
		{
			this.input = this.getStream(sname);
		}

		// Token: 0x040000A4 RID: 164
		protected internal Hashtable inputStreamNames;

		// Token: 0x040000A5 RID: 165
		protected internal TokenStream input;

		// Token: 0x040000A6 RID: 166
		protected internal Stack streamStack = new Stack();
	}
}
