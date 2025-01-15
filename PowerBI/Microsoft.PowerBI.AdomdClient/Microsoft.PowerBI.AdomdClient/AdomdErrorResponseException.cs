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
		// Token: 0x0600062C RID: 1580 RVA: 0x00021E16 File Offset: 0x00020016
		internal AdomdErrorResponseException(XmlaResultCollection results)
		{
			this.TranslateResultCollection(results);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00021E25 File Offset: 0x00020025
		internal AdomdErrorResponseException(XmlaResult result)
		{
			this.TranslateResult(result);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00021E34 File Offset: 0x00020034
		internal AdomdErrorResponseException(XmlaError error)
		{
			this.errors = new AdomdErrorCollection();
			this.errors.Add(new AdomdError(error));
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00021E58 File Offset: 0x00020058
		internal AdomdErrorResponseException()
		{
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00021E60 File Offset: 0x00020060
		internal AdomdErrorResponseException(string message)
			: base(message)
		{
			this.Errors.Add(new AdomdError(0, "adomd.net", message, string.Empty));
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00021E85 File Offset: 0x00020085
		internal AdomdErrorResponseException(string message, Exception innerException)
			: base(message, innerException)
		{
			this.Errors.Add(new AdomdError(0, "adomd.net", message, string.Empty));
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00021EAB File Offset: 0x000200AB
		private AdomdErrorResponseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.errors = (AdomdErrorCollection)info.GetValue("Errors", typeof(AdomdErrorCollection));
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00021ED5 File Offset: 0x000200D5
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

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00021F08 File Offset: 0x00020108
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

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00021F23 File Offset: 0x00020123
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

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00021F52 File Offset: 0x00020152
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

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00021F88 File Offset: 0x00020188
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

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00022027 File Offset: 0x00020227
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

		// Token: 0x06000639 RID: 1593 RVA: 0x0002205C File Offset: 0x0002025C
		private void TranslateResultCollection(XmlaResultCollection results)
		{
			foreach (object obj in ((IEnumerable)results))
			{
				XmlaResult xmlaResult = (XmlaResult)obj;
				this.TranslateResult(xmlaResult);
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000220B0 File Offset: 0x000202B0
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

		// Token: 0x04000442 RID: 1090
		private const string errorsSerializeName = "Errors";

		// Token: 0x04000443 RID: 1091
		private string completeErrorMessage;

		// Token: 0x04000444 RID: 1092
		private AdomdErrorCollection errors;
	}
}
