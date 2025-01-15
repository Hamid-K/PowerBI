using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001EE RID: 494
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class LoadableObjectNotFoundException : DynamicLoaderException
	{
		// Token: 0x06000CDD RID: 3293 RVA: 0x0002D088 File Offset: 0x0002B288
		internal LoadableObjectNotFoundException(Type type)
			: base(type.Assembly.FullName, type.FullName)
		{
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002D0A1 File Offset: 0x0002B2A1
		internal LoadableObjectNotFoundException(string assembly, string type, Exception ex)
			: base(assembly, type, null, ex)
		{
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002D0AD File Offset: 0x0002B2AD
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002D0B5 File Offset: 0x0002B2B5
		public LoadableObjectNotFoundException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002D0C4 File Offset: 0x0002B2C4
		public LoadableObjectNotFoundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002D0DB File Offset: 0x0002B2DB
		public LoadableObjectNotFoundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0002D0F8 File Offset: 0x0002B2F8
		protected LoadableObjectNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("LoadableObjectNotFoundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("LoadableObjectNotFoundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002D194 File Offset: 0x0002B394
		public LoadableObjectNotFoundException(string assembly, string loadType)
			: base(assembly, loadType)
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002D1A5 File Offset: 0x0002B3A5
		public LoadableObjectNotFoundException(string assembly, string loadType, string message, Exception innerException)
			: base(assembly, loadType, message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002D1C5 File Offset: 0x0002B3C5
		public LoadableObjectNotFoundException(string assembly, string loadType, string message)
			: base(assembly, loadType, message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002D1E0 File Offset: 0x0002B3E0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002D217 File Offset: 0x0002B417
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002D220 File Offset: 0x0002B420
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(LoadableObjectNotFoundException))
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

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002D2F0 File Offset: 0x0002B4F0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("LoadableObjectNotFoundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("LoadableObjectNotFoundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002D34C File Offset: 0x0002B54C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Type '{0}' could not be found in assembly '{1}', or said assembly could not be found", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((base.LoadType != null) ? base.LoadType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((base.LoadType != null) ? base.LoadType.MarkIfInternal() : string.Empty) : ((base.LoadType != null) ? base.LoadType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((base.Assembly != null) ? base.Assembly.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((base.Assembly != null) ? base.Assembly.MarkIfInternal() : string.Empty) : ((base.Assembly != null) ? base.Assembly.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0002D432 File Offset: 0x0002B632
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

		// Token: 0x06000CED RID: 3309 RVA: 0x0002D44F File Offset: 0x0002B64F
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002D45E File Offset: 0x0002B65E
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0002D467 File Offset: 0x0002B667
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002D470 File Offset: 0x0002B670
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002D45E File Offset: 0x0002B65E
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0002D47C File Offset: 0x0002B67C
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

		// Token: 0x040004FF RID: 1279
		private string creationMessage;

		// Token: 0x04000500 RID: 1280
		private ExceptionCulprit exceptionCulprit;
	}
}
