using System;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public class RowsetSinkBase : IRowsetSink, IRecordUpdate, IRecordContextUpdate
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004788 File Offset: 0x00002988
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00004790 File Offset: 0x00002990
		public string Name { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004799 File Offset: 0x00002999
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000047A1 File Offset: 0x000029A1
		public string DomainName { get; set; }

		// Token: 0x060000BF RID: 191 RVA: 0x000047AA File Offset: 0x000029AA
		public virtual IUpdateContext BeginUpdate()
		{
			return this.BeginUpdate(null);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000047B3 File Offset: 0x000029B3
		public virtual IUpdateContext BeginUpdate(DataTable schemaTable)
		{
			return new RowsetSinkBase.UpdateContextBase
			{
				DomainName = this.DomainName
			};
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000047C6 File Offset: 0x000029C6
		public virtual void AddRecordContext(IUpdateContext context, RecordContext recordContext)
		{
			this.Add(context, recordContext.TokenSequence.Tokens);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000047DA File Offset: 0x000029DA
		public virtual void AddRecord(IUpdateContext context, IDataRecord record)
		{
			this.Add(context, this.Tokenize(context, record));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000047EB File Offset: 0x000029EB
		public virtual void Add(IUpdateContext context, TokenSequence tokenSeq)
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000047F0 File Offset: 0x000029F0
		public virtual TokenSequence Tokenize(IUpdateContext _context, IDataRecord record)
		{
			RowsetSinkBase.UpdateContextBase updateContextBase = (RowsetSinkBase.UpdateContextBase)_context;
			updateContextBase.m_intAllocator.Reset();
			updateContextBase.m_tokenizerContext.Reset();
			return TokenSequence.Create(updateContextBase.m_tokenizer.Tokenize(updateContextBase.m_tokenizerContext, record), updateContextBase.DomainId, updateContextBase.TokenIdProvider, updateContextBase.m_tokenIdSegmentBuilder, updateContextBase.m_intAllocator);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004849 File Offset: 0x00002A49
		public virtual void RemoveRecord(IUpdateContext _context, IDataRecord record)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004850 File Offset: 0x00002A50
		public virtual void RemoveRecordContext(IUpdateContext _context, RecordContext recordContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004858 File Offset: 0x00002A58
		public virtual void EndUpdate(IUpdateContext _context)
		{
			IDisposable disposable = _context as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x02000122 RID: 290
		public class UpdateContextBase : IUpdateContext, IRecordUpdateContextInitialize, IDisposable
		{
			// Token: 0x06000BD2 RID: 3026 RVA: 0x000338CB File Offset: 0x00031ACB
			public virtual void Dispose()
			{
				this.TokenIdProvider = null;
				this.m_tokenizer = null;
				this.m_tokenizerContext = null;
				this.m_tokenIdSegmentBuilder.Reset();
				this.m_intAllocator = null;
				this.m_charSegmentAllocator = null;
			}

			// Token: 0x06000BD3 RID: 3027 RVA: 0x000338FC File Offset: 0x00031AFC
			public virtual void Initialize(IRowsetManager rowsetManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding recordBinding)
			{
				this.TokenIdProvider = tokenIdProvider;
				this.DomainId = domainManager.GetDomainId(this.DomainName);
				this.m_tokenizer = domainManager.GetTokenizer(this.DomainName);
				this.m_tokenizer.Prepare(recordBinding.Schema, recordBinding.GetDomainBinding(this.DomainName), out this.m_tokenizerContext);
			}

			// Token: 0x04000484 RID: 1156
			public string DomainName;

			// Token: 0x04000485 RID: 1157
			public int DomainId;

			// Token: 0x04000486 RID: 1158
			public ITokenIdProvider TokenIdProvider;

			// Token: 0x04000487 RID: 1159
			public IRecordTokenizer m_tokenizer;

			// Token: 0x04000488 RID: 1160
			public TokenizerContext m_tokenizerContext;

			// Token: 0x04000489 RID: 1161
			public ArraySegmentBuilder<int> m_tokenIdSegmentBuilder = new ArraySegmentBuilder<int>();

			// Token: 0x0400048A RID: 1162
			public BlockedSegmentArray<int> m_intAllocator = new BlockedSegmentArray<int>();

			// Token: 0x0400048B RID: 1163
			public BlockedSegmentArray<char> m_charSegmentAllocator = new BlockedSegmentArray<char>();
		}
	}
}
