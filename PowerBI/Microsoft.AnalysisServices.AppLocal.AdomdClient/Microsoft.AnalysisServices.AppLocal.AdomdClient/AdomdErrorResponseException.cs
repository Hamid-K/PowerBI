using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	public sealed class AdomdErrorResponseException : AdomdException
	{
		// Token: 0x06000639 RID: 1593 RVA: 0x00022146 File Offset: 0x00020346
		internal AdomdErrorResponseException(XmlaResultCollection results)
		{
			this.TranslateResultCollection(results);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00022155 File Offset: 0x00020355
		internal AdomdErrorResponseException(XmlaResult result)
		{
			this.TranslateResult(result);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00022164 File Offset: 0x00020364
		internal AdomdErrorResponseException(XmlaError error)
		{
			this.errors = new AdomdErrorCollection();
			this.errors.Add(new AdomdError(error));
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00022188 File Offset: 0x00020388
		internal AdomdErrorResponseException()
		{
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00022190 File Offset: 0x00020390
		internal AdomdErrorResponseException(string message)
			: base(message)
		{
			this.Errors.Add(new AdomdError(0, "adomd.net", message, string.Empty));
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000221B5 File Offset: 0x000203B5
		internal AdomdErrorResponseException(string message, Exception innerException)
			: base(message, innerException)
		{
			this.Errors.Add(new AdomdError(0, "adomd.net", message, string.Empty));
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000221DB File Offset: 0x000203DB
		private AdomdErrorResponseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.errors = (AdomdErrorCollection)info.GetValue("Errors", typeof(AdomdErrorCollection));
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00022205 File Offset: 0x00020405
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Errors", this.errors, typeof(AdomdErrorCollection));
			base.GetObjectData(info, context);
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00022238 File Offset: 0x00020438
		public AdomdErrorCollection Errors
		{
			get
			{
				if (this.errors == null)
				{
					this.errors = new AdomdErrorCollection();
				}
				return this.errors;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00022253 File Offset: 0x00020453
		public int ErrorCode
		{
			get
			{
				if (this.Errors.Count > 0)
				{
					return this.Errors[this.Errors.Count - 1].ErrorCode;
				}
				return 0;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00022282 File Offset: 0x00020482
		public override string Source
		{
			get
			{
				if (this.Errors.Count > 0)
				{
					return this.Errors[this.Errors.Count - 1].Source;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x000222B8 File Offset: 0x000204B8
		public override string Message
		{
			get
			{
				if (this.completeErrorMessage == null)
				{
					if (this.Errors.Count > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = this.Errors.Count - 1; i >= 1; i--)
						{
							stringBuilder.Append(this.Errors[i].Message);
							stringBuilder.Append(Environment.NewLine);
						}
						stringBuilder.Append(this.Errors[0].Message);
						this.completeErrorMessage = stringBuilder.ToString();
					}
					else
					{
						this.completeErrorMessage = XmlaSR.ServerDidNotProvideErrorInfo;
					}
				}
				return this.completeErrorMessage;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x00022357 File Offset: 0x00020557
		public override string HelpLink
		{
			get
			{
				if (this.Errors.Count > 0)
				{
					return this.Errors[this.Errors.Count - 1].HelpLink;
				}
				return string.Empty;
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0002238C File Offset: 0x0002058C
		private void TranslateResultCollection(XmlaResultCollection results)
		{
			foreach (object obj in ((IEnumerable)results))
			{
				XmlaResult xmlaResult = (XmlaResult)obj;
				this.TranslateResult(xmlaResult);
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000223E0 File Offset: 0x000205E0
		private void TranslateResult(XmlaResult result)
		{
			foreach (object obj in ((IEnumerable)result.Messages))
			{
				XmlaError xmlaError = ((XmlaMessage)obj) as XmlaError;
				if (xmlaError != null)
				{
					if (this.errors == null)
					{
						this.errors = new AdomdErrorCollection();
					}
					this.errors.Add(new AdomdError(xmlaError));
				}
			}
		}

		// Token: 0x0400044F RID: 1103
		private const string errorsSerializeName = "Errors";

		// Token: 0x04000450 RID: 1104
		private string completeErrorMessage;

		// Token: 0x04000451 RID: 1105
		private AdomdErrorCollection errors;
	}
}
