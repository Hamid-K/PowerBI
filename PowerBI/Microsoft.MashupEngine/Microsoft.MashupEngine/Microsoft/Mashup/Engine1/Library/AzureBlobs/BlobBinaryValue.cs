using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library.AzureBase;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureBlobs
{
	// Token: 0x02000EF1 RID: 3825
	internal class BlobBinaryValue : PagedHttpBinaryValue
	{
		// Token: 0x0600656F RID: 25967 RVA: 0x0015C238 File Offset: 0x0015A438
		public static BinaryValue New(IEngineHost host, IResource resource, TextValue blobUrl, OptionsRecord options, Value contentType = null, long? blobSize = null, string eTag = null, string contentEncoding = null)
		{
			BlobBinaryValue blobBinaryValue = new BlobBinaryValue(host, resource, blobUrl, options, contentType, blobSize, eTag, contentEncoding);
			return blobBinaryValue.NewMeta(DataSource.CreateDataSourceRecordValue(blobBinaryValue.GetContentType(blobBinaryValue.previewBlobRequest, null), blobBinaryValue.GetHeaders(blobBinaryValue.previewBlobRequest), blobBinaryValue.fullBlobRequest, Value.Null, null, null)).AsBinary.WithExpressionFromValue(blobBinaryValue);
		}

		// Token: 0x06006570 RID: 25968 RVA: 0x0015C293 File Offset: 0x0015A493
		private BlobBinaryValue(IEngineHost host, IResource resource, TextValue blobUrl, OptionsRecord options, Value contentType = null, long? blobSize = null, string eTag = null, string contentEncoding = null)
			: base(host, resource, blobUrl, options, blobSize, eTag, contentEncoding)
		{
			this.contentType = contentType;
		}

		// Token: 0x17001D84 RID: 7556
		// (get) Token: 0x06006571 RID: 25969 RVA: 0x00158925 File Offset: 0x00156B25
		protected override PersistentCacheKey CacheKey
		{
			get
			{
				return PersistentCacheKey.AzureStorageBinary;
			}
		}

		// Token: 0x17001D85 RID: 7557
		// (get) Token: 0x06006572 RID: 25970 RVA: 0x0015C2AE File Offset: 0x0015A4AE
		protected override string RangeKey
		{
			get
			{
				return "x-ms-range";
			}
		}

		// Token: 0x17001D86 RID: 7558
		// (get) Token: 0x06006573 RID: 25971 RVA: 0x0015C2B5 File Offset: 0x0015A4B5
		public override IExpression Expression
		{
			get
			{
				return new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(new AzureBlobsModule.ContentsFunctionValue(this.host)), new ConstantExpressionSyntaxNode(this.blobUrl), new ConstantExpressionSyntaxNode(this.options.AsRecord));
			}
		}

		// Token: 0x06006574 RID: 25972 RVA: 0x0015C2E7 File Offset: 0x0015A4E7
		protected override Request CreateRequest(Value query = null, Value headers = null, Value content = null)
		{
			return BlobsHelper.CreateRequest(this.host, this.resource, this.blobUrl, query, headers, content);
		}

		// Token: 0x06006575 RID: 25973 RVA: 0x0015C303 File Offset: 0x0015A503
		protected override Response GetResponse(Request request, int[] ignoredStatusCodes)
		{
			return AzureBaseHelper.GetResponse(request, BlobBinaryValue.securityExceptionCreator, ignoredStatusCodes);
		}

		// Token: 0x06006576 RID: 25974 RVA: 0x0015C314 File Offset: 0x0015A514
		public override Stream OpenForWrite()
		{
			ITempPageService tempPageService = this.host.QueryService<ITempPageService>();
			List<string> blocks = new List<string>();
			List<Exception> blockErrors = new List<Exception>();
			return ParallelWriteBehindStream.NewBuffering(this.blockSize, () => tempPageService.CreatePage((uint)this.blockSize), this.host, this.resource, this.concurrentRequests, delegate(int index, Stream page)
			{
				this.PutBlock(blocks, blockErrors, index, page);
			}).AfterDispose(delegate
			{
				this.PutBlockList(blocks, blockErrors);
			});
		}

		// Token: 0x06006577 RID: 25975 RVA: 0x0015C39C File Offset: 0x0015A59C
		private void PutBlock(List<string> blocks, List<Exception> blockErrors, int index, Stream page)
		{
			IUniqueIdService uniqueIdService = this.host.QueryService<IUniqueIdService>();
			string text;
			lock (blocks)
			{
				List<Exception> list = blockErrors;
				lock (list)
				{
					while (index >= blocks.Count)
					{
						blocks.Add(Base64Encoding.GetString(Encoding.UTF8.GetBytes(uniqueIdService.NewUniqueId())));
						blockErrors.Add(null);
					}
					text = blocks[index];
				}
			}
			try
			{
				this.PutBlock(text, page);
			}
			catch (Exception ex)
			{
				List<Exception> list = blockErrors;
				lock (list)
				{
					blockErrors[index] = ex;
				}
				throw;
			}
		}

		// Token: 0x06006578 RID: 25976 RVA: 0x0015C488 File Offset: 0x0015A688
		private void PutBlock(string blockId, Stream blockData)
		{
			try
			{
				Request request = this.CreateRequest(RecordValue.New(BlobBinaryValue.putBlockQueryKeys, new Value[]
				{
					BlobBinaryValue.blockCompValue,
					TextValue.New(blockId)
				}), Value.Null, new PagedHttpBinaryValue.PutBlockBinaryValue(blockData));
				request.Method = "PUT";
				request.ContentLength = blockData.Length;
				request.UseCache = false;
				request.UseBuffer = false;
				AzureBaseHelper.ExecuteRequest(request, BlobBinaryValue.securityExceptionCreator, null);
			}
			finally
			{
				if (blockData != null)
				{
					((IDisposable)blockData).Dispose();
				}
			}
		}

		// Token: 0x06006579 RID: 25977 RVA: 0x0015C518 File Offset: 0x0015A718
		private void PutBlockList(List<string> blocks, List<Exception> blockErrors)
		{
			StringBuilder stringBuilder = new StringBuilder();
			lock (blocks)
			{
				lock (blockErrors)
				{
					int num = blockErrors.Count;
					for (int i = 0; i < blockErrors.Count; i++)
					{
						if (blockErrors[i] != null)
						{
							num = i;
							break;
						}
					}
					stringBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
					stringBuilder.AppendLine("<BlockList>");
					for (int j = 0; j < num; j++)
					{
						stringBuilder.Append("<Latest>");
						stringBuilder.Append(blocks[j]);
						stringBuilder.AppendLine("</Latest>");
					}
					stringBuilder.AppendLine("</BlockList>");
				}
			}
			byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
			Value value = ((this.contentType == null) ? Value.Null : RecordValue.New(Keys.New("x-ms-blob-content-type"), new Value[] { this.contentType.AsText }));
			Request request = this.CreateRequest(BlobBinaryValue.putBlockListQuery, value, BinaryValue.New(bytes));
			request.Method = "PUT";
			request.ContentLength = (long)bytes.Length;
			request.UseCache = false;
			request.UseBuffer = false;
			AzureBaseHelper.ExecuteRequest(request, BlobBinaryValue.securityExceptionCreator, null);
		}

		// Token: 0x04003793 RID: 14227
		private static readonly Request.SecurityExceptionCreator securityExceptionCreator = new Request.SecurityExceptionCreator(BlobsHelper.TryCreateSecurityException);

		// Token: 0x04003794 RID: 14228
		private static readonly RecordValue putBlockListQuery = RecordValue.New(Keys.New("comp"), new Value[] { TextValue.New("blocklist") });

		// Token: 0x04003795 RID: 14229
		private static readonly Keys putBlockQueryKeys = Keys.New("comp", "blockid");

		// Token: 0x04003796 RID: 14230
		private static readonly TextValue blockCompValue = TextValue.New("block");

		// Token: 0x04003797 RID: 14231
		private static readonly int[] ignoredStatusCodes = new int[] { 416 };

		// Token: 0x04003798 RID: 14232
		private Value contentType;
	}
}
