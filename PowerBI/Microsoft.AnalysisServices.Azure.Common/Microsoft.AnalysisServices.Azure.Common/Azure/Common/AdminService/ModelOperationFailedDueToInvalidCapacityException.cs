using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x02000135 RID: 309
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ModelOperationFailedDueToInvalidCapacityException : AdminProvisioningServiceException
	{
		// Token: 0x060010A1 RID: 4257 RVA: 0x000438D8 File Offset: 0x00041AD8
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x000438E0 File Offset: 0x00041AE0
		public ModelOperationFailedDueToInvalidCapacityException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x000438EF File Offset: 0x00041AEF
		public ModelOperationFailedDueToInvalidCapacityException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00043906 File Offset: 0x00041B06
		public ModelOperationFailedDueToInvalidCapacityException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00043924 File Offset: 0x00041B24
		protected ModelOperationFailedDueToInvalidCapacityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ModelOperationFailedDueToInvalidCapacityException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ModelOperationFailedDueToInvalidCapacityException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x000439C0 File Offset: 0x00041BC0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			this.exceptionCulprit = ExceptionCulprit.User;
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x000439FE File Offset: 0x00041BFE
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00043A08 File Offset: 0x00041C08
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ModelOperationFailedDueToInvalidCapacityException))
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

		// Token: 0x060010A9 RID: 4265 RVA: 0x00043AD8 File Offset: 0x00041CD8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ModelOperationFailedDueToInvalidCapacityException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ModelOperationFailedDueToInvalidCapacityException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00043B33 File Offset: 0x00041D33
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Model operation failed due to user error.", Array.Empty<object>());
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x00043B49 File Offset: 0x00041D49
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

		// Token: 0x060010AC RID: 4268 RVA: 0x000429E2 File Offset: 0x00040BE2
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00043B66 File Offset: 0x00041D66
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00043B6F File Offset: 0x00041D6F
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00043B78 File Offset: 0x00041D78
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00043B66 File Offset: 0x00041D66
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00043B84 File Offset: 0x00041D84
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

		// Token: 0x040003C6 RID: 966
		private string creationMessage;

		// Token: 0x040003C7 RID: 967
		private ExceptionCulprit exceptionCulprit;
	}
}
