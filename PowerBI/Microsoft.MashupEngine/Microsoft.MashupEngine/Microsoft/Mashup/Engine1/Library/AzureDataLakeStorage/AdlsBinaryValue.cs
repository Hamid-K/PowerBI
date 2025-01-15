using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library.AzureBase;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000ECA RID: 3786
	internal class AdlsBinaryValue : StreamedBinaryValue
	{
		// Token: 0x0600648D RID: 25741 RVA: 0x00158454 File Offset: 0x00156654
		public static BinaryValue New(IEngineHost host, IResource resource, TextValue blobUrl, OptionsRecord options, bool mustNotExistToCreate = false, AdlsVersions.Version version = null)
		{
			BinaryValue binaryValue = AdlsBinaryValue.ContentBinaryValue.New(host, resource, blobUrl, options, mustNotExistToCreate);
			AdlsBinaryValue adlsBinaryValue = new AdlsBinaryValue(binaryValue, version);
			RecordValue recordValue = Library.Record.SelectFields.Invoke(binaryValue.MetaValue, ListValue.New(new Value[]
			{
				TextValue.New("Content.Uri"),
				TextValue.New("Content.Name")
			}), Library.MissingField.Ignore).AsRecord;
			BinaryValue binaryValue2;
			if (adlsBinaryValue.TryGetEffectiveContent(out binaryValue2))
			{
				recordValue = binaryValue2.MetaValue.Concatenate(recordValue).AsRecord;
			}
			return adlsBinaryValue.NewMeta(recordValue).AsBinary.WithExpressionFromValue(adlsBinaryValue);
		}

		// Token: 0x0600648E RID: 25742 RVA: 0x001584E4 File Offset: 0x001566E4
		private AdlsBinaryValue(BinaryValue content, AdlsVersions.Version version)
		{
			this.content = content;
			this.version = version;
		}

		// Token: 0x17001D47 RID: 7495
		// (get) Token: 0x0600648F RID: 25743 RVA: 0x001584FC File Offset: 0x001566FC
		private AdlsBinaryValue.ContentBinaryValue Content
		{
			get
			{
				AdlsBinaryValue.ContentBinaryValue contentBinaryValue;
				this.content.TryGetAs<AdlsBinaryValue.ContentBinaryValue>(out contentBinaryValue);
				return contentBinaryValue;
			}
		}

		// Token: 0x17001D48 RID: 7496
		// (get) Token: 0x06006490 RID: 25744 RVA: 0x00158518 File Offset: 0x00156718
		public OptionsRecord Options
		{
			get
			{
				return this.Content.Options;
			}
		}

		// Token: 0x17001D49 RID: 7497
		// (get) Token: 0x06006491 RID: 25745 RVA: 0x00158525 File Offset: 0x00156725
		public IEngineHost Host
		{
			get
			{
				return this.Content.Host;
			}
		}

		// Token: 0x17001D4A RID: 7498
		// (get) Token: 0x06006492 RID: 25746 RVA: 0x00158532 File Offset: 0x00156732
		public IResource Resource
		{
			get
			{
				return this.Content.Resource;
			}
		}

		// Token: 0x17001D4B RID: 7499
		// (get) Token: 0x06006493 RID: 25747 RVA: 0x0015853F File Offset: 0x0015673F
		public TextValue BlobUrl
		{
			get
			{
				return this.Content.BlobUrl;
			}
		}

		// Token: 0x17001D4C RID: 7500
		// (get) Token: 0x06006494 RID: 25748 RVA: 0x0015854C File Offset: 0x0015674C
		public bool MustNotExistToCreate
		{
			get
			{
				return this.Content.MustNotExistToCreate;
			}
		}

		// Token: 0x17001D4D RID: 7501
		// (get) Token: 0x06006495 RID: 25749 RVA: 0x00158559 File Offset: 0x00156759
		public AdlsVersions.Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x00158561 File Offset: 0x00156761
		public override bool TryGetLength(out long length)
		{
			return this.GetEffectiveContent().TryGetLength(out length);
		}

		// Token: 0x17001D4E RID: 7502
		// (get) Token: 0x06006497 RID: 25751 RVA: 0x0015856F File Offset: 0x0015676F
		public override long Length
		{
			get
			{
				return this.GetEffectiveContent().Length;
			}
		}

		// Token: 0x17001D4F RID: 7503
		// (get) Token: 0x06006498 RID: 25752 RVA: 0x0015857C File Offset: 0x0015677C
		public override IExpression Expression
		{
			get
			{
				IExpression expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(new AzureDataLakeStorageModule.DataLakeContentsFunctionValue(this.Content.Host)), new ConstantExpressionSyntaxNode(this.Content.BlobUrl), new ConstantExpressionSyntaxNode(this.Content.Options.AsRecord));
				if (this.version != null)
				{
					expression = expression.AccessVersion(this.version.Identity);
				}
				return expression;
			}
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x001585E4 File Offset: 0x001567E4
		public override ActionValue Replace(Value value)
		{
			if (this.version != null)
			{
				AdlsBinaryValue otherAdlsBinary;
				if (value.TryGetAs<AdlsBinaryValue>(out otherAdlsBinary))
				{
					return new SimpleBindingActionValue(delegate(FunctionValue binding)
					{
						if (binding != SimpleActionBinding.ReturnNull)
						{
							throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this, null);
						}
						return this.version.CreateVersionedAction(new ReplaceBlobActionValue(this, otherAdlsBinary));
					});
				}
				long num;
				if (value.IsBinary && value.AsBinary.TryGetLength(out num) && num == 0L)
				{
					return new SimpleBindingActionValue(delegate(FunctionValue binding)
					{
						if (binding != SimpleActionBinding.ReturnNull)
						{
							throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this, null);
						}
						return this.version.CreateVersionedAction(new CreateBlobActionValue(this));
					});
				}
			}
			return this.content.Replace(value);
		}

		// Token: 0x0600649A RID: 25754 RVA: 0x00158660 File Offset: 0x00156860
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			if (function.Equals(Library._Value.VersionIdentity))
			{
				result = ((this.version != null) ? TextValue.New(this.version.Identity) : Value.Null);
				return true;
			}
			TableValue tableValue;
			if (function.Equals(Library._Value.Versions) && arguments.Length == 1 && new AdlsValueVersions(AdlsVersions.Instance, this.Host, this.Resource, new string[] { this.BlobUrl.AsString }, delegate(AdlsVersions.Version version)
			{
				version.TrackUrl(this.BlobUrl.AsString, this.Content.ETag);
			}, (AdlsVersions.Version version) => AdlsBinaryValue.New(this.Host, this.Resource, this.BlobUrl, this.Content.Options, this.Content.MustNotExistToCreate, version)).TryCreateTable(out tableValue))
			{
				result = tableValue;
				return true;
			}
			return base.TryInvokeAsArgument(function, arguments, index, out result);
		}

		// Token: 0x0600649B RID: 25755 RVA: 0x0015870D File Offset: 0x0015690D
		public override Stream Open()
		{
			return this.GetEffectiveContent().Open();
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x0015871A File Offset: 0x0015691A
		public override Stream Open(bool preferCanSeek)
		{
			return this.GetEffectiveContent().Open(preferCanSeek);
		}

		// Token: 0x0600649D RID: 25757 RVA: 0x00158728 File Offset: 0x00156928
		public override Stream OpenForWrite()
		{
			if (this.version != null)
			{
				throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this, null);
			}
			return this.content.OpenForWrite();
		}

		// Token: 0x0600649E RID: 25758 RVA: 0x0015874C File Offset: 0x0015694C
		private BinaryValue GetEffectiveContent()
		{
			BinaryValue binaryValue;
			if (this.TryGetEffectiveContent(out binaryValue))
			{
				return binaryValue;
			}
			Message4 message = Strings.HDInsightFailed(this.Resource.Kind, this.BlobUrl, HttpStatusCode.NotFound, Strings.Adls_FileNotFound);
			throw HttpServices.NewDataSourceError<Message4>(this.Host, message, this.Resource, this.BlobUrl);
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x001587A8 File Offset: 0x001569A8
		private bool TryGetEffectiveContent(out BinaryValue content)
		{
			BinaryValue binaryValue;
			if (this.version == null || !this.version.GetBlobReplacements().TryGetValue(this.BlobUrl.AsString, out binaryValue))
			{
				content = this.content;
				return true;
			}
			if (binaryValue != null)
			{
				content = binaryValue;
				return true;
			}
			content = null;
			return false;
		}

		// Token: 0x040036EE RID: 14062
		private readonly BinaryValue content;

		// Token: 0x040036EF RID: 14063
		private readonly AdlsVersions.Version version;

		// Token: 0x02000ECB RID: 3787
		private class ContentBinaryValue : PagedHttpBinaryValue
		{
			// Token: 0x060064A3 RID: 25763 RVA: 0x00158868 File Offset: 0x00156A68
			public static BinaryValue New(IEngineHost host, IResource resource, TextValue blobUrl, OptionsRecord options, bool mustNotExistToCreate)
			{
				AdlsBinaryValue.ContentBinaryValue contentBinaryValue = new AdlsBinaryValue.ContentBinaryValue(host, resource, blobUrl, options, mustNotExistToCreate);
				return contentBinaryValue.NewMeta(DataSource.CreateDataSourceRecordValue(contentBinaryValue.GetContentType(contentBinaryValue.previewBlobRequest, null), contentBinaryValue.GetHeaders(contentBinaryValue.previewBlobRequest), contentBinaryValue.fullBlobRequest, Value.Null, null, null)).AsBinary;
			}

			// Token: 0x060064A4 RID: 25764 RVA: 0x001588B8 File Offset: 0x00156AB8
			private ContentBinaryValue(IEngineHost host, IResource resource, TextValue blobUrl, OptionsRecord options, bool mustNotExistToCreate = false)
				: base(host, resource, blobUrl, options, null, null, null)
			{
				this.mustNotExistToCreate = mustNotExistToCreate;
			}

			// Token: 0x17001D50 RID: 7504
			// (get) Token: 0x060064A5 RID: 25765 RVA: 0x001588E3 File Offset: 0x00156AE3
			public IEngineHost Host
			{
				get
				{
					return this.host;
				}
			}

			// Token: 0x17001D51 RID: 7505
			// (get) Token: 0x060064A6 RID: 25766 RVA: 0x001588EB File Offset: 0x00156AEB
			public IResource Resource
			{
				get
				{
					return this.resource;
				}
			}

			// Token: 0x17001D52 RID: 7506
			// (get) Token: 0x060064A7 RID: 25767 RVA: 0x001588F3 File Offset: 0x00156AF3
			public TextValue BlobUrl
			{
				get
				{
					return this.blobUrl;
				}
			}

			// Token: 0x17001D53 RID: 7507
			// (get) Token: 0x060064A8 RID: 25768 RVA: 0x001588FB File Offset: 0x00156AFB
			public OptionsRecord Options
			{
				get
				{
					return this.options;
				}
			}

			// Token: 0x17001D54 RID: 7508
			// (get) Token: 0x060064A9 RID: 25769 RVA: 0x00158903 File Offset: 0x00156B03
			public bool MustNotExistToCreate
			{
				get
				{
					return this.mustNotExistToCreate;
				}
			}

			// Token: 0x17001D55 RID: 7509
			// (get) Token: 0x060064AA RID: 25770 RVA: 0x0015890B File Offset: 0x00156B0B
			public string ETag
			{
				get
				{
					Value value = base.GetHeaders(this.previewBlobRequest).Value;
					return this.etag;
				}
			}

			// Token: 0x17001D56 RID: 7510
			// (get) Token: 0x060064AB RID: 25771 RVA: 0x00158925 File Offset: 0x00156B25
			protected override PersistentCacheKey CacheKey
			{
				get
				{
					return PersistentCacheKey.AzureStorageBinary;
				}
			}

			// Token: 0x060064AC RID: 25772 RVA: 0x0015892C File Offset: 0x00156B2C
			protected override Request CreateRequest(Value query = null, Value headers = null, Value content = null)
			{
				return AdlsHelper.CreateRequest(this.host, this.resource, this.blobUrl, query, headers, content, this.options.GetBool("IsOneLake", false));
			}

			// Token: 0x060064AD RID: 25773 RVA: 0x00158964 File Offset: 0x00156B64
			public override Stream OpenForWrite()
			{
				AdlsBinaryValue.ContentBinaryValue.CommitLength commitLength = new AdlsBinaryValue.ContentBinaryValue.CommitLength();
				List<Exception> blockErrors = new List<Exception>();
				this.PrepareForBlockUpload();
				ITempPageService tempPageService = this.host.QueryService<ITempPageService>();
				return ParallelWriteBehindStream.NewBuffering(this.blockSize, () => tempPageService.CreatePage((uint)this.blockSize), this.host, this.resource, this.concurrentRequests, delegate(int index, Stream page)
				{
					this.PutBlock(commitLength, blockErrors, index, page);
				}).AfterDispose(delegate
				{
					this.PutBlockList(commitLength, blockErrors);
				});
			}

			// Token: 0x060064AE RID: 25774 RVA: 0x001589F1 File Offset: 0x00156BF1
			protected override Response GetResponse(Request request, int[] ignoredStatusCodes)
			{
				return AzureBaseHelper.GetResponse(request, AdlsBinaryValue.ContentBinaryValue.securityExceptionCreator, ignoredStatusCodes);
			}

			// Token: 0x060064AF RID: 25775 RVA: 0x00158A00 File Offset: 0x00156C00
			private void PrepareForBlockUpload()
			{
				this.flushLength = 0L;
				Request request = this.CreateRequest(RecordValue.New(Keys.New("resource"), new Value[] { TextValue.New("file") }), this.mustNotExistToCreate ? RecordValue.New(Keys.New("If-None-Match"), new Value[] { TextValue.New("*") }) : null, null);
				request.ContentLength = 0L;
				request.Method = "PUT";
				request.UseCache = false;
				request.UseBuffer = false;
				AzureBaseHelper.ExecuteRequest(request, AdlsBinaryValue.ContentBinaryValue.securityExceptionCreator, null);
				this.progress = ProgressService.GetHostProgress(this.host, this.resource.Kind, this.fullBlobRequest.ProgressDataSource);
			}

			// Token: 0x060064B0 RID: 25776 RVA: 0x00158AC0 File Offset: 0x00156CC0
			private void PutBlock(AdlsBinaryValue.ContentBinaryValue.CommitLength commitLength, List<Exception> blockErrors, int index, Stream page)
			{
				List<Exception> list = blockErrors;
				lock (list)
				{
					while (index >= blockErrors.Count)
					{
						blockErrors.Add(null);
					}
				}
				try
				{
					long num = (long)index * (long)this.blockSize;
					this.PutBlock(num, page);
					Interlocked.Add(ref commitLength.length, page.Length);
					this.progress.RecordBytesWritten(page.Length);
				}
				catch (Exception ex)
				{
					list = blockErrors;
					lock (list)
					{
						blockErrors[index] = ex;
					}
					throw;
				}
			}

			// Token: 0x060064B1 RID: 25777 RVA: 0x00158B80 File Offset: 0x00156D80
			private void PutBlock(long position, Stream blockData)
			{
				try
				{
					Request request = this.CreateRequest(RecordValue.New(AdlsHelper.PatchBlockListKeys, new Value[]
					{
						TextValue.New("append"),
						TextValue.New(string.Format(CultureInfo.InvariantCulture, "{0}", position))
					}), Value.Null, new PagedHttpBinaryValue.PutBlockBinaryValue(blockData));
					request.Method = "PATCH";
					request.ContentLength = blockData.Length;
					request.UseCache = false;
					request.UseBuffer = false;
					AzureBaseHelper.ExecuteRequest(request, AdlsBinaryValue.ContentBinaryValue.securityExceptionCreator, null);
				}
				finally
				{
					if (blockData != null)
					{
						((IDisposable)blockData).Dispose();
					}
				}
				Interlocked.Add(ref this.flushLength, blockData.Length);
			}

			// Token: 0x060064B2 RID: 25778 RVA: 0x00158C38 File Offset: 0x00156E38
			private void PutBlockList(AdlsBinaryValue.ContentBinaryValue.CommitLength commitLength, List<Exception> blockErrors)
			{
				lock (blockErrors)
				{
					if (blockErrors.Any((Exception e) => e != null))
					{
						return;
					}
				}
				Interlocked.Read(ref commitLength.length);
				Request request = this.CreateRequest(RecordValue.New(AdlsHelper.FlushBlockListKeys, new Value[]
				{
					TextValue.New("flush"),
					TextValue.New(string.Format(CultureInfo.InvariantCulture, "{0}", this.flushLength)),
					TextValue.New("application/octet-stream")
				}), null, null);
				request.Method = "PATCH";
				request.ContentLength = 0L;
				request.UseCache = false;
				request.UseBuffer = false;
				AzureBaseHelper.ExecuteRequest(request, AdlsBinaryValue.ContentBinaryValue.securityExceptionCreator, null);
				this.progress = null;
			}

			// Token: 0x040036F0 RID: 14064
			private static readonly Request.SecurityExceptionCreator securityExceptionCreator;

			// Token: 0x040036F1 RID: 14065
			private readonly bool mustNotExistToCreate;

			// Token: 0x040036F2 RID: 14066
			private long flushLength;

			// Token: 0x040036F3 RID: 14067
			private IHostProgress progress;

			// Token: 0x02000ECC RID: 3788
			private class CommitLength
			{
				// Token: 0x040036F4 RID: 14068
				public long length;
			}
		}
	}
}
