using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.PushDataService
{
	// Token: 0x0200011F RID: 287
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ProcessAddFailedException : PushDataServiceException
	{
		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003AF1C File Offset: 0x0003911C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0003AF24 File Offset: 0x00039124
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x0003AF2C File Offset: 0x0003912C
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
			protected set
			{
				this.m_databaseName = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0003AF35 File Offset: 0x00039135
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x0003AF3D File Offset: 0x0003913D
		public bool ErrorMessageIsRetriable
		{
			get
			{
				return this.m_errorMessageIsRetriable;
			}
			protected set
			{
				this.m_errorMessageIsRetriable = value;
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003AF46 File Offset: 0x00039146
		public ProcessAddFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidValueField<bool>();
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0003AF5F File Offset: 0x0003915F
		public ProcessAddFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0003AF76 File Offset: 0x00039176
		public ProcessAddFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003AF94 File Offset: 0x00039194
		protected ProcessAddFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ProcessAddFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("ProcessAddFailedException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			this.ErrorMessageIsRetriable = (bool)info.GetValue("ProcessAddFailedException_ErrorMessageIsRetriable", typeof(bool));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ProcessAddFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0003B088 File Offset: 0x00039288
		public ProcessAddFailedException(string databaseName, bool errorMessageIsRetriable)
		{
			this.DatabaseName = databaseName;
			this.ErrorMessageIsRetriable = errorMessageIsRetriable;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003B0A5 File Offset: 0x000392A5
		public ProcessAddFailedException(string databaseName, bool errorMessageIsRetriable, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.ErrorMessageIsRetriable = errorMessageIsRetriable;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003B0CA File Offset: 0x000392CA
		public ProcessAddFailedException(string databaseName, bool errorMessageIsRetriable, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.ErrorMessageIsRetriable = errorMessageIsRetriable;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003B0F8 File Offset: 0x000392F8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003B12F File Offset: 0x0003932F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003B138 File Offset: 0x00039338
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ProcessAddFailedException))
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

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003B208 File Offset: 0x00039408
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ProcessAddFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ProcessAddFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseName != null)
			{
				info.AddValue("ProcessAddFailedException_DatabaseName", this.DatabaseName, typeof(string));
			}
			info.AddValue("ProcessAddFailedException_ErrorMessageIsRetriable", this.ErrorMessageIsRetriable, typeof(bool));
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003B2A8 File Offset: 0x000394A8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "ProcessAdd for '{0}' failed", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0003B323 File Offset: 0x00039523
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

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003B340 File Offset: 0x00039540
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ErrorMessageIsRetriable={0}", this.ErrorMessageIsRetriable.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ErrorMessageIsRetriable={0}", this.ErrorMessageIsRetriable.ToString()) : string.Format(CultureInfo.CurrentCulture, "ErrorMessageIsRetriable={0}", this.ErrorMessageIsRetriable.ToString())));
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003B482 File Offset: 0x00039682
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003B48B File Offset: 0x0003968B
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0003B494 File Offset: 0x00039694
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0003B482 File Offset: 0x00039682
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0003B4A0 File Offset: 0x000396A0
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

		// Token: 0x04000381 RID: 897
		private string creationMessage;

		// Token: 0x04000382 RID: 898
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000383 RID: 899
		private string m_databaseName;

		// Token: 0x04000384 RID: 900
		private bool m_errorMessageIsRetriable;
	}
}
