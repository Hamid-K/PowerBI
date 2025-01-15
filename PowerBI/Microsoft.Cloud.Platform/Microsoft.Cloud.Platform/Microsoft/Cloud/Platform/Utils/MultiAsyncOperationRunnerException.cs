using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200025A RID: 602
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class MultiAsyncOperationRunnerException : MonitoredException
	{
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00036778 File Offset: 0x00034978
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder().Append(this.m_exceptionDictionary.Count).Append(" exception").Append((this.m_exceptionDictionary.Count == 1) ? "" : "s")
					.Append(" have been thrown from the async operations executed!");
				if (this.m_exceptionDictionary.Count > 0)
				{
					stringBuilder.Append(" ");
					if (this.m_exceptionDictionary.Count > 32)
					{
						stringBuilder.Append("First 32 messages are:");
					}
					else
					{
						stringBuilder.Append("Messages are:");
					}
					stringBuilder.Append(" [");
					int num = 0;
					foreach (KeyValuePair<string, Exception> keyValuePair in this.m_exceptionDictionary.Where((KeyValuePair<string, Exception> it, int idx) => idx < 32))
					{
						if (num > 0)
						{
							stringBuilder.Append("; ");
						}
						stringBuilder.Append('{').Append('\'').Append(keyValuePair.Key)
							.Append('\'')
							.Append(" => ")
							.Append('\'')
							.Append(keyValuePair.Value.Message)
							.Append('\'')
							.Append('}');
						num++;
					}
					stringBuilder.Append("].");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x000368F8 File Offset: 0x00034AF8
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00036900 File Offset: 0x00034B00
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x00036908 File Offset: 0x00034B08
		public IDictionary<string, Exception> ExceptionDictionary
		{
			get
			{
				return this.m_exceptionDictionary;
			}
			protected set
			{
				this.m_exceptionDictionary = value;
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00036911 File Offset: 0x00034B11
		public MultiAsyncOperationRunnerException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<IDictionary<string, Exception>>();
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00036925 File Offset: 0x00034B25
		public MultiAsyncOperationRunnerException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0003693C File Offset: 0x00034B3C
		public MultiAsyncOperationRunnerException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003695C File Offset: 0x00034B5C
		protected MultiAsyncOperationRunnerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MultiAsyncOperationRunnerException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ExceptionDictionary = (IDictionary<string, Exception>)info.GetValue("MultiAsyncOperationRunnerException_ExceptionDictionary", typeof(IDictionary<string, Exception>));
			}
			catch (SerializationException)
			{
				this.ExceptionDictionary = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("MultiAsyncOperationRunnerException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00036A30 File Offset: 0x00034C30
		public MultiAsyncOperationRunnerException(IDictionary<string, Exception> exceptionDictionary)
		{
			this.ExceptionDictionary = exceptionDictionary;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00036A46 File Offset: 0x00034C46
		public MultiAsyncOperationRunnerException(IDictionary<string, Exception> exceptionDictionary, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ExceptionDictionary = exceptionDictionary;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00036A64 File Offset: 0x00034C64
		public MultiAsyncOperationRunnerException(IDictionary<string, Exception> exceptionDictionary, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ExceptionDictionary = exceptionDictionary;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00036A88 File Offset: 0x00034C88
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00036ABF File Offset: 0x00034CBF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00036AC8 File Offset: 0x00034CC8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MultiAsyncOperationRunnerException))
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<UtilsTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00036B98 File Offset: 0x00034D98
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MultiAsyncOperationRunnerException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("MultiAsyncOperationRunnerException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ExceptionDictionary != null)
			{
				info.AddValue("MultiAsyncOperationRunnerException_ExceptionDictionary", this.ExceptionDictionary, typeof(IDictionary<string, Exception>));
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00036C18 File Offset: 0x00034E18
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? (base.Message ?? string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? (base.Message ?? string.Empty) : (base.Message ?? string.Empty)) });
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00036C78 File Offset: 0x00034E78
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExceptionDictionary={0}", new object[] { (this.ExceptionDictionary != null) ? this.ExceptionDictionary.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExceptionDictionary={0}", new object[] { (this.ExceptionDictionary != null) ? this.ExceptionDictionary.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ExceptionDictionary={0}", new object[] { (this.ExceptionDictionary != null) ? this.ExceptionDictionary.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00036D57 File Offset: 0x00034F57
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00036D60 File Offset: 0x00034F60
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00036D69 File Offset: 0x00034F69
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00036D57 File Offset: 0x00034F57
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00036D74 File Offset: 0x00034F74
		private string ToString(PrivateInformationMarkupKind markupKind)
		{
			string text = "[" + ExceptionsTemplateHelper.MagicLevel.ToString(CultureInfo.CurrentCulture) + "]" + base.GetType().FullName;
			string text2 = this.CreateMessageFromTemplate(markupKind);
			string text3 = text + ": ";
			if (string.IsNullOrEmpty(this.creationMessage))
			{
				text3 += text2;
			}
			else
			{
				if (markupKind == PrivateInformationMarkupKind.Private || markupKind == PrivateInformationMarkupKind.Internal)
				{
					text3 += this.creationMessage.ObfuscatePrivateValue(true);
				}
				else
				{
					text3 += this.creationMessage;
				}
				if (!string.Equals(this.creationMessage, text2))
				{
					text3 = text3 + Environment.NewLine + "  TemplateMessage: " + text2;
				}
			}
			text3 += this.GetPropertiesString(markupKind);
			text3 = text3 + Environment.NewLine + "ExceptionCulprit=" + this.exceptionCulprit.ToString();
			if (base.InnerException != null)
			{
				try
				{
					ExceptionsTemplateHelper.IncrementMagicLevel();
					IMonitoredError monitoredError = base.InnerException as MonitoredException;
					string text4;
					if (markupKind != PrivateInformationMarkupKind.None)
					{
						if (markupKind != PrivateInformationMarkupKind.Internal)
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfPrivate() : monitoredError.ToPrivateString());
							text4 = text4.ObfuscatePrivateValue(true);
						}
						else
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfInternal() : monitoredError.ToInternalString());
						}
					}
					else
					{
						text4 = ((monitoredError == null) ? base.InnerException.ToString() : monitoredError.ToOriginalString());
					}
					text3 = string.Concat(new string[]
					{
						text3,
						" --->",
						Environment.NewLine,
						text4,
						Environment.NewLine,
						"   --- End of inner exception stack trace ---",
						Environment.NewLine,
						"  (",
						text,
						".StackTrace:)"
					});
				}
				finally
				{
					ExceptionsTemplateHelper.DecrementMagicLevel();
				}
			}
			if (this.StackTrace != null)
			{
				text3 = text3 + Environment.NewLine + this.StackTrace;
			}
			return text3;
		}

		// Token: 0x040005F3 RID: 1523
		private string creationMessage;

		// Token: 0x040005F4 RID: 1524
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040005F5 RID: 1525
		private IDictionary<string, Exception> m_exceptionDictionary;
	}
}
