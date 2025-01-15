using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000EF RID: 239
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class BackupStateOperationFailureException : MonitoredException
	{
		// Token: 0x06000AF8 RID: 2808 RVA: 0x000281EC File Offset: 0x000263EC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x000281F4 File Offset: 0x000263F4
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x000281FC File Offset: 0x000263FC
		public string OperationName
		{
			get
			{
				return this.m_operationName;
			}
			protected set
			{
				this.m_operationName = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00028205 File Offset: 0x00026405
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x0002820D File Offset: 0x0002640D
		public string ErrorMessage
		{
			get
			{
				return this.m_errorMessage;
			}
			protected set
			{
				this.m_errorMessage = value;
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00028216 File Offset: 0x00026416
		public BackupStateOperationFailureException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002822F File Offset: 0x0002642F
		public BackupStateOperationFailureException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00028246 File Offset: 0x00026446
		public BackupStateOperationFailureException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00028264 File Offset: 0x00026464
		protected BackupStateOperationFailureException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("BackupStateOperationFailureException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.OperationName = (string)info.GetValue("BackupStateOperationFailureException_OperationName", typeof(string));
			}
			catch (SerializationException)
			{
				this.OperationName = null;
			}
			try
			{
				this.ErrorMessage = (string)info.GetValue("BackupStateOperationFailureException_ErrorMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.ErrorMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("BackupStateOperationFailureException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00028374 File Offset: 0x00026574
		public BackupStateOperationFailureException(string operationName, string errorMessage)
		{
			this.OperationName = operationName;
			this.ErrorMessage = errorMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00028391 File Offset: 0x00026591
		public BackupStateOperationFailureException(string operationName, string errorMessage, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.OperationName = operationName;
			this.ErrorMessage = errorMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000283B6 File Offset: 0x000265B6
		public BackupStateOperationFailureException(string operationName, string errorMessage, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.OperationName = operationName;
			this.ErrorMessage = errorMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000283E4 File Offset: 0x000265E4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002841B File Offset: 0x0002661B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00028424 File Offset: 0x00026624
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(BackupStateOperationFailureException))
			{
				TraceSourceBase<ANCommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ANCommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ANCommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000284F4 File Offset: 0x000266F4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("BackupStateOperationFailureException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("BackupStateOperationFailureException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.OperationName != null)
			{
				info.AddValue("BackupStateOperationFailureException_OperationName", this.OperationName, typeof(string));
			}
			if (this.ErrorMessage != null)
			{
				info.AddValue("BackupStateOperationFailureException_ErrorMessage", this.ErrorMessage, typeof(string));
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00028598 File Offset: 0x00026798
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to execute backup state operation {0}. Error Message : {1}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.OperationName != null) ? this.OperationName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.OperationName != null) ? this.OperationName.MarkIfInternal() : string.Empty) : ((this.OperationName != null) ? this.OperationName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.ErrorMessage != null) ? this.ErrorMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ErrorMessage != null) ? this.ErrorMessage.MarkIfInternal() : string.Empty) : ((this.ErrorMessage != null) ? this.ErrorMessage.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00028672 File Offset: 0x00026872
		public override string Message
		{
			get
			{
				if (!string.IsNullOrEmpty(this.creationMessage))
				{
					return this.creationMessage;
				}
				return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.None);
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00028690 File Offset: 0x00026890
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "OperationName={0}", (this.OperationName != null) ? this.OperationName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "OperationName={0}", (this.OperationName != null) ? this.OperationName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "OperationName={0}", (this.OperationName != null) ? this.OperationName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ErrorMessage={0}", (this.ErrorMessage != null) ? this.ErrorMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ErrorMessage={0}", (this.ErrorMessage != null) ? this.ErrorMessage.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ErrorMessage={0}", (this.ErrorMessage != null) ? this.ErrorMessage.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000287FC File Offset: 0x000269FC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00028805 File Offset: 0x00026A05
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002880E File Offset: 0x00026A0E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x000287FC File Offset: 0x000269FC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00028818 File Offset: 0x00026A18
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

		// Token: 0x040002ED RID: 749
		private string creationMessage;

		// Token: 0x040002EE RID: 750
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040002EF RID: 751
		private string m_operationName;

		// Token: 0x040002F0 RID: 752
		private string m_errorMessage;
	}
}
