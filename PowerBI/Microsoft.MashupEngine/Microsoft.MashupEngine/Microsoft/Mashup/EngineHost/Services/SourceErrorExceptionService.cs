using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B3B RID: 6971
	internal sealed class SourceErrorExceptionService : ISourceErrorExceptionService
	{
		// Token: 0x0600AE70 RID: 44656 RVA: 0x0023B79D File Offset: 0x0023999D
		public SourceErrorExceptionService(IEngine engine, IPartitionedDocument document)
		{
			this.engine = engine;
			this.document = document;
		}

		// Token: 0x0600AE71 RID: 44657 RVA: 0x0023B7B4 File Offset: 0x002399B4
		public bool TryGetSourceErrorException(IPartitionKey partitionKey, IError error, out ValueException2 exception)
		{
			if (this.IsErrorInPartition(error.Location, partitionKey))
			{
				string[] array;
				if (partitionKey == null)
				{
					array = new string[0];
				}
				else
				{
					(array = new string[1])[0] = this.document.GetPartitionSection(partitionKey);
				}
				string[] array2 = array;
				string text;
				string text2;
				string text3;
				if (SourceError.TryGetErrorReason(error, array2, out text, out text2, out text3))
				{
					exception = this.engine.Exception(this.engine.ExceptionRecord(this.engine.Text(text2), this.engine.Text(text3), this.engine.Null));
					this.engine.AddStackTrace(exception, new SourceLocation[] { error.Location });
					return true;
				}
			}
			exception = null;
			return false;
		}

		// Token: 0x0600AE72 RID: 44658 RVA: 0x0023B860 File Offset: 0x00239A60
		private bool IsErrorInPartition(SourceLocation location, IPartitionKey partitionKey)
		{
			if (partitionKey != null)
			{
				ITranslateSourceLocation translateSourceLocation = location.Document as ITranslateSourceLocation;
				if (translateSourceLocation != null)
				{
					location = translateSourceLocation.TranslateSourceLocation(location);
				}
				int num;
				int num2;
				string partitionSectionOffsetAndLength = this.document.GetPartitionSectionOffsetAndLength(partitionKey, out num, out num2);
				if (partitionSectionOffsetAndLength != null && partitionSectionOffsetAndLength == location.Document.UniqueID)
				{
					IPackageSection section = this.document.Package.GetSection(partitionSectionOffsetAndLength);
					ITokens tokens = this.engine.Tokenize(section.Text);
					int offset = tokens.GetOffset(location.Range.Start);
					int offset2 = tokens.GetOffset(location.Range.End);
					return offset >= num && offset2 <= num + num2;
				}
			}
			return false;
		}

		// Token: 0x040059F8 RID: 23032
		private readonly IEngine engine;

		// Token: 0x040059F9 RID: 23033
		private readonly IPartitionedDocument document;
	}
}
