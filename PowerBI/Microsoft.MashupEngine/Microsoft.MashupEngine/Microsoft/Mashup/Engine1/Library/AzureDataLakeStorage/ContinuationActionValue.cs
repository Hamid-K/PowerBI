using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.AzureBase;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000EC4 RID: 3780
	internal abstract class ContinuationActionValue : ActionValue
	{
		// Token: 0x06006462 RID: 25698 RVA: 0x00157EF0 File Offset: 0x001560F0
		public override Value Execute()
		{
			RecordValue recordValue = this.BaseQuery;
			for (;;)
			{
				string text = null;
				TextValue textValue = AdlsHelper.DetermineVersionToUse(this.Host, AdlsHelper.August2020Version);
				Request request = AzureBaseHelper.CreateRequest(this.Host, this.RequestResource, TextValue.New(this.TargetUri), textValue, recordValue, this.Headers, null, this.IsOneLake);
				request.Method = this.Method;
				request.ContentLength = 0L;
				AdlsHelper.GetResponse(request, ContinuationActionValue.expectedStatusCodes, false, out text);
				if (string.IsNullOrEmpty(text))
				{
					break;
				}
				recordValue = recordValue.Concatenate(RecordValue.New(Keys.New("continuation"), new Value[] { TextValue.New(text) })).AsRecord;
			}
			return Value.Null;
		}

		// Token: 0x17001D27 RID: 7463
		// (get) Token: 0x06006463 RID: 25699
		protected abstract IEngineHost Host { get; }

		// Token: 0x17001D28 RID: 7464
		// (get) Token: 0x06006464 RID: 25700
		protected abstract IResource RequestResource { get; }

		// Token: 0x17001D29 RID: 7465
		// (get) Token: 0x06006465 RID: 25701
		protected abstract string TargetUri { get; }

		// Token: 0x17001D2A RID: 7466
		// (get) Token: 0x06006466 RID: 25702 RVA: 0x00019E61 File Offset: 0x00018061
		protected virtual RecordValue BaseQuery
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x17001D2B RID: 7467
		// (get) Token: 0x06006467 RID: 25703 RVA: 0x00019E42 File Offset: 0x00018042
		protected virtual Value Headers
		{
			get
			{
				return Value.Null;
			}
		}

		// Token: 0x17001D2C RID: 7468
		// (get) Token: 0x06006468 RID: 25704
		protected abstract string Method { get; }

		// Token: 0x17001D2D RID: 7469
		// (get) Token: 0x06006469 RID: 25705 RVA: 0x00002105 File Offset: 0x00000305
		protected virtual bool IsOneLake
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040036E4 RID: 14052
		private static readonly int[] expectedStatusCodes = new int[] { 400, 404, 409, 412, 500, 503 };
	}
}
